using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechTalk.SpecFlow;

namespace SeleniumTestAutomationFramework.Utilities
{
    class SeleniumHelper
    {
        public static IWebDriver driver;

        //launch browser in computer
        public static void launchBrowser(string browserType)
        {
            switch (browserType.ToLower())
            {
                case "chrome":
                    driver = new ChromeDriver(@"C:\chromedriver");
                    break;
                case "firefox":
                    driver = new FirefoxDriver("");
                    break;
                default:
                    driver = null;
                    break;
            }
        }

        //maximize browser window
        public static void maximizeBrowser()
        {
            driver.Manage().Window.Maximize();
        }

        //navigate to given url
        public static void navigateToUrl(string url)
        {
            driver.Navigate().GoToUrl(url);
        }

        //return current driver instance
        public static IWebDriver getDriver()
        {
            return driver;
        }

        //wait until element is visible
        public static bool waitForElementIsVisible(IWebDriver driver, IWebElement element, int seconds)
        {
            try
            {
                var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(seconds));
                wait.Until(d => (bool)(element as IWebElement).Displayed);
                return true;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        //take a screenshot and save
        public static void CaptureScreenshotAndSave(string location)
        {
            IWebDriver driver = SeleniumHelper.getDriver();
            Screenshot screenshot = ((ITakesScreenshot)driver).GetScreenshot();
            screenshot.SaveAsFile(location, ScreenshotImageFormat.Png);
        }

        //adding screenshots to stepimagecontext
        public static List<StepImageContext> AddScreenshotToContext(List<StepImageContext> imageContext, string ScreenshotPathWithName, string StepInformation)
        {
            if (!ScreenshotPathWithName.Equals(""))
            {
                SeleniumHelper.CaptureScreenshotAndSave(FrameworkUtility.GetTestReportDirectory() + ScreenshotPathWithName);
            }
            StepImageContext step = new StepImageContext();
            step.ScreenshotPathWithName = ScreenshotPathWithName;
            step.StepInformation = StepInformation;
            imageContext.Add(step);

            return imageContext;
        }
    }
}
