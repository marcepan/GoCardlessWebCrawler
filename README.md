# WebCrawler

Simple WebCrawler written in C#

## Install
The application needs to be built using Visual Studio (from version 14) using .Net 4.5. All necessary packages will be downloaded from `nuget` automatically on the build. No further actions are required.

## Run
Application can be run directly from VS or by using created exe file.
Important setting are stored in `App.config` file

The application will display the full output of processing in `JSON` format to `STDOUT`

### Config
 - `DefaultUrl` - URL to process
 - `BlackedExtensions` - blacklisted extensions of files which we don't want to process
 - `PlayNice` - tells if we want to respect informations from `Robots.txt` - should be always `true` 
