# WatchHistoryExporter

A .NET Playwright console application to export personal TV Time watch history before the service shutdown.

## Purpose

TV Time is shutting down, and this project aims to help users export their personal watch history data before the service becomes unavailable.

The application uses browser automation to authenticate through the official TV Time website and captures the data returned to the user during normal usage.

## Current Status

This project is currently in the proof-of-concept phase.

The current implementation:

- Opens TV Time using Playwright
- Allows the user to authenticate manually
- Captures TV Time API responses
- Identifies structured JSON data returned by TV Time
- Prepares the foundation for generating a clean personal data export

## Planned Improvements

- Map TV Time API responses into a structured export format
- Export movies, TV shows, and episodes
- Preserve watch dates and personal metadata
- Generate a single export file (JSON/CSV)
- Improve automatic data collection after authentication
- Provide a simpler user workflow requiring minimal interaction

## Technologies

- .NET 8
- C#
- Microsoft Playwright
- System.Text.Json

## Requirements

- .NET 8 SDK
- A TV Time account
- A supported browser installed by Playwright

## Usage

1. Clone the repository

2. Install Playwright browsers:

3. Run the application:

4. Follow the instructions displayed in the console.

5. The application will collect TV Time data locally.

## Disclaimer

This project is intended only for users exporting their own personal data.

It is not affiliated with, endorsed by, or connected to TV Time.

Users are responsible for complying with the terms of service and applicable laws when using this tool.

## Why this exists

This project was created as a personal data preservation effort in response to the announced TV Time shutdown.