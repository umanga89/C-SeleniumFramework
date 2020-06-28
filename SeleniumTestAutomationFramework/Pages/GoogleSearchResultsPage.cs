using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeleniumTestAutomationFramework.Pages
{
    class GoogleSearchResultsPage
    {
        public GoogleSearchResultsPage(IWebDriver driver)
        {
            PageFactory.InitElements(driver, this);
        }

        [FindsBy(How = How.XPath, Using = "//*[Span='Google']")]
        private IWebElement searchTextBox;

        public void EnterSearchText(string text)
        {
            searchTextBox.SendKeys(text);
        }
    }
}
