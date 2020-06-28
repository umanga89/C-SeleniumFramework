using NUnit.Framework;
using SeleniumTestAutomationFramework.Pages;
using SeleniumTestAutomationFramework.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechTalk.SpecFlow;

namespace SeleniumTestAutomationFramework.Steps
{
    [Binding]
    class GoogleHomeSteps
    {
        GoogleHomePage googleHomePage;
        private List<StepImageContext> _StepImageContext;

        [Given(@"I've launched (.*) browser")]
        public void GivenIVeLauchedChromeBrowser(string browser)
        {
            _StepImageContext = new List<StepImageContext>();
            SeleniumHelper.launchBrowser(AppConfigManager.GetBrowserConfigForKey("browser"));

            Hooks.testLog.Info(AppConfigManager.GetBrowserConfigForKey("browser") + " browser is launched");

            SeleniumHelper.maximizeBrowser();
            _StepImageContext = SeleniumHelper.AddScreenshotToContext(_StepImageContext, "ChromeBrowser.png", "User has opend chrome browser");
            ScenarioContext.Current.Add("StepImageContext", _StepImageContext);
        }

        [Given(@"I'm in google home page")]
        public void GivenIMInGoogleHomePage()
        {
            try
            {
                _StepImageContext = new List<StepImageContext>();
                SeleniumHelper.navigateToUrl("https:\\www.google.com");
                googleHomePage = new GoogleHomePage(SeleniumHelper.getDriver());
                _StepImageContext = SeleniumHelper.AddScreenshotToContext(_StepImageContext, "GoogleHomePage.png", "User is in google home page");
                Assert.IsTrue(googleHomePage.isSearchTextBoxDisplayed(10));
                Hooks.testLog.Pass("Navigated to google url");
                ScenarioContext.Current.Add("StepImageContext", _StepImageContext);
            }
            catch (Exception ex)
            {
                //Hooks.testLog.Fail(ex);
                //throw ex;
                Hooks.LogErrorWithScreenshot(ex, _StepImageContext);
            }
        }

        [When(@"I enter (.*) in search bar and click on Google Search button")]
        public void WhenIEnterGoogleInSearchBarAndClickOnGoogleSearchButton(string searchKeyword)
        {
            try
            {
                googleHomePage = new GoogleHomePage(SeleniumHelper.getDriver());
                googleHomePage.EnterSearchText(searchKeyword);
                _StepImageContext = new List<StepImageContext>();
                _StepImageContext = SeleniumHelper.AddScreenshotToContext(_StepImageContext, "EnterSearchCriteria.png", "User is entering search criteria");
                googleHomePage.ClickSearchButton();
                _StepImageContext = SeleniumHelper.AddScreenshotToContext(_StepImageContext, "ClickOnSearchButton.png", "User is clicking search button");
                ScenarioContext.Current.Add("StepImageContext", _StepImageContext);
            }
            catch (Exception ex)
            {
                //Hooks.testLog.Fail(ex);
                //throw ex;
                Hooks.LogErrorWithScreenshot(ex, _StepImageContext);
            }
        }

        [Then(@"I can see search results")]
        public void ThenICanSeeSearchResults()
        {
            ScenarioContext.Current.Pending();
        }

        [Given(@"I'm seeing search results for 'Google'")]
        public void GivenIGoogle()
        {
            ScenarioContext.Current.Pending();
        }

        [Then(@"I can see new page is displayed")]
        public void ThenICanSeeNewPageIsDisplayed()
        {
            ScenarioContext.Current.Pending();
        }

        [When(@"I click on first search results link")]
        public void WhenIClickOnFirstSearchResultsLink()
        {
            ScenarioContext.Current.Pending();
        }
    }
}
