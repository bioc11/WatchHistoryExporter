using Microsoft.Playwright;
using System.Text.Json;

Console.WriteLine("Starting WatchHistoryExporter...");

using var playwright = await Playwright.CreateAsync();

await using var browser = await playwright.Chromium.LaunchAsync(
    new BrowserTypeLaunchOptions
    {
        Headless = false
    });

var context = await browser.NewContextAsync();

var page = await context.NewPageAsync();

var apiResponses = new List<CapturedResponse>();

page.Response += async (_, response) =>
{
    try
    {
        var url = response.Url;

        if (!url.Contains("api2.tozelabs.com") &&
            !url.Contains("/sidecar"))
        {
            return;
        }

        if (!response.Headers.TryGetValue(
                "content-type",
                out var contentType))
        {
            return;
        }

        if (!contentType.Contains("application/json"))
        {
            return;
        }

        Console.WriteLine();
        Console.WriteLine($"Captured:");
        Console.WriteLine(url);

        var body = await response.TextAsync();

        apiResponses.Add(new CapturedResponse
        {
            Url = url,
            Body = body
        });
    }
    catch (Exception ex)
    {
        Console.WriteLine(
            $"Could not capture response: {ex.Message}");
    }
};


await page.GotoAsync("https://www.tvtime.com/");


Console.WriteLine("Waiting for consent...");

try
{
    await page.GetByRole(AriaRole.Button, new()
    {
        Name = "Consent"
    }).ClickAsync();
}
catch
{
    Console.WriteLine("Consent button not found.");
}


Console.WriteLine("Opening login...");

await page.GetByRole(AriaRole.Link, new PageGetByRoleOptions
{
    Name = "Log in"
}).ClickAsync();


Console.WriteLine();
Console.WriteLine("--------------------------------");
Console.WriteLine("LOGIN REQUIRED");
Console.WriteLine("--------------------------------");
Console.WriteLine("Please log in to your TV Time account.");
Console.WriteLine("Use email, Google, or any another available method.");
Console.WriteLine();
Console.WriteLine("After login:");
Console.WriteLine("1. Open your profile");
Console.WriteLine("2. Open watched shows/history");
Console.WriteLine("3. Open movies/history etc.");
Console.WriteLine("Make sure you scroll down to load all content.");
Console.WriteLine();
Console.WriteLine("Press ENTER when finished.");
Console.WriteLine("--------------------------------");


Console.ReadLine();


Console.WriteLine();
Console.WriteLine("Saving captured data...");

Directory.CreateDirectory("export");


var counter = 1;

foreach (var response in apiResponses)
{
    var fileName =
        $"export/response_{counter}.json";

    await File.WriteAllTextAsync(
        fileName,
        response.Body);


    await File.WriteAllTextAsync(
        $"export/response_{counter}_url.txt",
        response.Url);


    counter++;
}


Console.WriteLine();
Console.WriteLine(
    $"Export completed. Captured {apiResponses.Count} responses.");

Console.WriteLine(
    "Files saved in the 'export' folder.");


await context.CloseAsync();


public class CapturedResponse
{
    public string Url { get; set; } = "";
    public string Body { get; set; } = "";
}

