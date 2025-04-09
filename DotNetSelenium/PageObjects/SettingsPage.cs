using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System;

namespace DotNetSelenium.PageObjects
{
    public class SettingsPage
    {
        private readonly IWebDriver driver;
        private readonly WebDriverWait wait;

        public SettingsPage(IWebDriver driver)
        {
            this.driver = driver;
            this.wait = new WebDriverWait(driver, TimeSpan.FromSeconds(20));
        }

        private IWebElement SettingsLink => driver.FindElement(By.CssSelector("a[href='#/Settings']"));
        private IWebElement MoreDropdown => driver.FindElement(By.XPath("//a[contains(text(),'More...')]"));
        private IWebElement PriceCategoryTab => driver.FindElement(By.CssSelector("ul.dropdown-menu a[href='#/Settings/PriceCategory']"));

        private IWebElement GetDisableButton(string code) =>
            driver.FindElement(By.XPath($"//div[text()='{code}']/../div/span/a[@danphe-grid-action='deactivatePriceCategorySetting']"));

        private IWebElement GetEnableButton(string code) =>
            driver.FindElement(By.XPath($"//div[text()='{code}']/../div/span/a[@danphe-grid-action='activatePriceCategorySetting']"));

        private IWebElement ActivateSuccessMessage => driver.FindElement(By.XPath("//p[contains(text(),'success')]/../p[text()='Activated.']"));
        private IWebElement DeactivateSuccessMessage => driver.FindElement(By.XPath("//p[contains(text(),'success')]/../p[text()='Deactivated.']"));

/**
 * @Test10
 * @description This method disables and then re-enables a price category (e.g., "NHIF-1") via the Price Category tab,
 *              verifying that appropriate success messages are displayed for both actions.
 * @expected
 * Success messages "Deactivated." and "Activated." should be displayed after each corresponding action.
 *
 * Steps:
 * 1. Click on the "Settings" link to navigate to the settings module.
 * 2. Click on the "More..." dropdown and select the "Price Category" tab.
 * 3. Click on the "Disable" button for the code "NHIF-1".
 * 4. Verify that the message "Deactivated." appears after disabling.
 * 5. Click on the "Enable" button for the same code.
 * 6. Verify that the message "Activated." appears after enabling.
 *
 * @returns Returns the final activation message if successful; otherwise, the test throws an exception.
 */
        public String TogglePriceCategoryStatus()
        {
            // Step 1: Navigate to Settings
            wait.Until(ExpectedConditions.ElementToBeClickable(SettingsLink)).Click();

            // Step 2: Open "More..." dropdown and click "Price Category" tab
            wait.Until(ExpectedConditions.ElementToBeClickable(MoreDropdown)).Click();
            wait.Until(ExpectedConditions.ElementToBeClickable(PriceCategoryTab)).Click();

            // Step 3: Disable the specified code (e.g., NHIF-1)
            string priceCategoryCode = "NHIF-1";
            wait.Until(ExpectedConditions.ElementToBeClickable(GetDisableButton(priceCategoryCode))).Click();

            // Step 4: Verify "Deactivated." success message
            string deactivateMessage = wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//p[contains(text(),'success')]/../p[text()='Deactivated.']"))).Text.Trim();
            if (!deactivateMessage.Equals("Deactivated."))
            {
                throw new Exception($"Expected 'Deactivated.' message not found. Found: {deactivateMessage}");
            }

            // Step 5: Enable the same code
            wait.Until(ExpectedConditions.ElementToBeClickable(GetEnableButton(priceCategoryCode))).Click();

            // Step 6: Verify "Activated." success message
            string activateMessage = wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//p[contains(text(),'success')]/../p[text()='Activated.']"))).Text.Trim();
            if (!activateMessage.Equals("Activated."))
            {
                throw new Exception($"Expected 'Activated.' message not found. Found: {activateMessage}");
            }
            return activateMessage;
        }
    }

}
