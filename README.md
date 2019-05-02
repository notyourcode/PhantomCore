# PhantomCore

Example of a Cross-Platform phantomjs based webscraper.

Scrapes the [AskUbuntu](https://www.askubuntu.com) homepage for all `question-hyperlink` elements.

## Requirements:
* Supported Windows or Linux environment (AMD64)
  * Tested on Windows 10 and Ubuntu 18.04
  * For x86, ARM, or other architectures you will need to adjust runtime identifiers and supply the appropriate PhantomJs executables. Your mileage may vary!
* .NET Core 2.2 SDK
* May require firewall rules to connect properly, especially on Windows.

## Notes:
* Locked to [Selenium.WebDriver 3.13.0](https://www.nuget.org/packages/Selenium.WebDriver/3.13.0) since it is the latest version with PhantomJs support **and** .NET Standard support for use in .NET Core
* PhantomJs 2.1.1 binaries from [phantomjs.org](http://phantomjs.org/download.html) are already included in source control for Linux and Windows (64-bit)

## Troubleshooting:
* "Permission Denied" exceptions on Linux when trying to open/start the PhantomJs service/driver 
  * Most likely due to the included binary not being executable
  * `chmod +x phantomjs` on the executable should fix this

## Known Bugs:
* Debug/Diagnostic output suppression does not work on Linux (yet)

## Authors

* **Chris Sekira**

## License

This project is licensed under the MIT License - see the [LICENSE.md](LICENSE.md) file for details