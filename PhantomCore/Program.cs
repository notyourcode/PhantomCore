using System;
using System.IO;
using System.Linq;
using System.Reflection;
using OpenQA.Selenium;
using OpenQA.Selenium.PhantomJS;
using System.Runtime.InteropServices;

namespace PhantomCore
{
    internal class Program
    {
        private static readonly DirectoryInfo RuntimeDirectory = Directory.GetParent(Assembly.GetEntryAssembly().Location);

        // Ignore Selenium's PhantomJS deprecation warning
#pragma warning disable CS0618
        static void Main(string[] args)
        {
            var phantomExecutable = GetPhantomJsExecutableLocation();

            using (var phantomService = PhantomJSDriverService.CreateDefaultService(phantomExecutable.DirectoryName, phantomExecutable.Name))
            {
                //TODO: Debug/Diagnostic output supression does not work on Linux through this method; find an alternative.
                phantomService.HideCommandPromptWindow = true;
                phantomService.SuppressInitialDiagnosticInformation = true;

                using (var phantomDriver = new PhantomJSDriver(phantomService))
                {
                    ScrapePage(phantomDriver);
                }
            }
        }
#pragma warning restore CS0618

        /// <summary>
        /// Scrapes AskUbuntu.com and prints all the homepage question names 
        /// </summary>
        /// <param name="driver">IWebDriver to use for scraping</param>
        private static void ScrapePage(IWebDriver driver)
        {
            Console.WriteLine("Navigating to page");
            driver.Navigate().GoToUrl("https://www.askubuntu.com");

            Console.WriteLine("Parsing questions");
            var questions = driver.FindElements(By.CssSelector("a.question-hyperlink"));

            Console.WriteLine("Parsing complete");

            Console.WriteLine("Writing all homepage questions");
            foreach (var question in questions)
            {
                Console.WriteLine(question.Text);
            }
        }

        /// <summary>
        /// Determines the platform-specific location of the phantomjs executable
        /// </summary>
        /// <returns>A FileInfo object pointing to the executable</returns>
        private static FileInfo GetPhantomJsExecutableLocation()
        {
            string executableName;
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                executableName = "phantomjs.exe";
            }
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
            {
                executableName = "phantomjs";
            }
            else
            {
                throw new NotSupportedException("The current OS is not supported.");
            }
            return new FileInfo(Path.Combine(RuntimeDirectory.FullName, executableName));
        }
    }
}
