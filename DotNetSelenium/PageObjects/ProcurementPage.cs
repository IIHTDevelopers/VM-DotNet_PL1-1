﻿using System;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System.Threading;

namespace DotNetSelenium.PageObjects
{
    public class ProcurementPage
    {
        private IWebDriver driver;
        private WebDriverWait wait;

        public ProcurementPage(IWebDriver driver)
        {
            this.driver = driver;
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(20));
        }

        // Locators
        private By ProcurementLink => By.CssSelector("a[href='#/ProcurementMain']");
        private By Settings => By.XPath("//a[contains(text(),'Settings')]");
        private By CurrencySubTab => By.CssSelector("a[routerlink='CurrencyList']");
        private By AddCurrencyButton1 => By.CssSelector("input[value='Add Currency']");
        private By AddCurrencyButton2 => By.CssSelector("input#AddCurrency");
        private By CurrencyCode => By.CssSelector("input#CurrencyCode");
        private By CurrencyDescriptionField => By.CssSelector("input#Description");
        private By SearchBar => By.CssSelector("input#quickFilterInput");
        private By CurrencyCodeColumn => By.XPath("(//div[@col-id='CurrencyCode'])[2]");

        /**
         * @Test5
         * @description This method navigates to the Purchase Request page, accesses the Currency Settings,
         *              adds a new currency with a unique code and description, and verifies that the new
         *              currency is successfully added to the table.
         *
         * @expected
         * The new currency should be added successfully and displayed in the table with the correct currency
         * code and description.
         * Steps:
         * 1. Click on the "Procurement" link to open the procurement module.
         * 2. Navigate to the "Settings" tab and then to the "Currency" sub-tab.
         * 3. Click the "Add Currency" button to open the currency input form.
         * 4. Enter a unique currency code and a test description.
         * 5. Click the "Add Currency" button to submit the form.
         * 6. Use the search bar to find the newly added currency.
         * 7. Verify that the currency appears in the table with the correct code.
         *
         * @returns True if the currency is added and visible in the table; otherwise, the test fails.
         */
        public bool AddCurrencyAndVerify()
        {
            string uniqueCurrencyCode = "CURR_" + new Random().Next(1000, 9999); // Generate a unique currency code
            string description = "Test Currency Description";

            // Navigate to the Currency Settings
            wait.Until(ExpectedConditions.ElementToBeClickable(ProcurementLink)).Click();
            wait.Until(ExpectedConditions.ElementToBeClickable(Settings)).Click();
            wait.Until(ExpectedConditions.ElementToBeClickable(CurrencySubTab)).Click();

            // Click "Add Currency" button
            wait.Until(ExpectedConditions.ElementToBeClickable(AddCurrencyButton1)).Click();

            // Fill in currency details
            wait.Until(ExpectedConditions.ElementIsVisible(CurrencyCode)).SendKeys(uniqueCurrencyCode);
            wait.Until(ExpectedConditions.ElementIsVisible(CurrencyDescriptionField)).SendKeys(description);

            // Click "Add Currency"
            wait.Until(ExpectedConditions.ElementToBeClickable(AddCurrencyButton2)).Click();

            // Wait for table to load
            Thread.Sleep(2000);

            // Search for the added currency
            wait.Until(ExpectedConditions.ElementIsVisible(SearchBar)).SendKeys(uniqueCurrencyCode);
            Thread.Sleep(2000);

            // Verify newly added currency is in the table
            IWebElement currencyRow = wait.Until(ExpectedConditions.ElementIsVisible(CurrencyCodeColumn));
            return currencyRow.Text.Trim().Equals(uniqueCurrencyCode, StringComparison.OrdinalIgnoreCase);
        }
    }
}
