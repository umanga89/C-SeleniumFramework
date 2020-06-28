using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using SeleniumTestAutomationFramework.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeleniumTestAutomationFramework.Pages
{
    class GoogleHomePage
    {
        public GoogleHomePage(IWebDriver driver)
        {
            PageFactory.InitElements(driver, this);
        }

        [FindsBy(How = How.XPath, Using = "//input[@name='q']")]
        private IWebElement searchTextBox;

        [FindsBy(How = How.XPath, Using = "//div[@jscontroller='tg8oTe']//input[@name='btnK']")]
        private IWebElement searchButton;
        public void EnterSearchText(string text)
        {
            searchTextBox.SendKeys(text);
        }
        public void ClickSearchButton()
        {
            searchButton.Click();
        }
        public bool isSearchTextBoxDisplayed(int seconds)
        {
            try
            {
                return SeleniumHelper.waitForElementIsVisible(SeleniumHelper.getDriver(), searchTextBox, seconds); ;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
