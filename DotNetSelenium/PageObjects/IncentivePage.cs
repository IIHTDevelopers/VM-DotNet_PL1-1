using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System;
using System.IO;
using Newtonsoft.Json.Linq;
using DotNetSelenium.Utilities;

namespace DotNetSelenium.PageObjects
{
    public class IncentivePage
    {
        private readonly IWebDriver driver;
        private readonly WebDriverWait wait;

        public IncentivePage(IWebDriver driver)
        {
            this.driver = driver;
            this.wait = new WebDriverWait(driver, TimeSpan.FromSeconds(20));
        }

        private IWebElement IncentiveLink => driver.FindElement(By.CssSelector("a[href='#/Incentive']"));
        private IWebElement SettingsTab => driver.FindElement(By.CssSelector("ul[class='page-breadcrumb'] a[href='#/Incentive/Setting']"));
        private IWebElement SearchBar => driver.FindElement(By.CssSelector("input#quickFilterInput"));
        private IWebElement EditTDSButton => driver.FindElement(By.CssSelector("a[danphe-grid-action='edit-tds']"));
        private IWebElement EditTDSModal => driver.FindElement(By.CssSelector("div.modal[title='Edit TDS Percent']"));
        private IWebElement TDSInputField => driver.FindElement(By.CssSelector("input[type='number']"));
        private IWebElement UpdateTDSButton => driver.FindElement(By.CssSelector("button#btn_GroupDistribution"));
        private IWebElement TDSValueInTable => driver.FindElement(By.CssSelector("div[col-id='TDSPercent']"));


/**
*@Test9
@description : Edits the TDS percentage for a specific employee and verifies that the updated value is reflected in the UI.
 *
 * Steps:
 * 1. Reads the employee name from a JSON test data file.
 * 2. Navigates to the Incentive module and selects the Settings tab.
 * 3. Searches for the employee using the search bar.
 * 4. Clicks the "Edit TDS%" button to open the TDS input modal.
 * 5. Clears the existing value and enters a new random TDS percentage.
 * 6. Clicks the "Update TDS" button to save changes.
 * 7. Repeats the search and retrieves the updated TDS value from the table.
 * 8. Verifies that the displayed TDS value matches the newly entered value.
 *
 * @returns : True if the updated TDS percentage is correctly reflected; otherwise, throws an exception.
 */
        public bool EditTDSForEmployee()
        {
            // Read JSON file for employee names
            JObject testData = TestDataReader.LoadJson("PatientName.json");
            string patientName = testData["PatientNames"][2]["Patient3"].ToString() ?? ""; ;
            int updatedTDS = new Random().Next(1, 99);
            bool isTdsValueUpdated = true;

            // Step 1: Click on Incentive link
            wait.Until(ExpectedConditions.ElementToBeClickable(IncentiveLink)).Click();

            // Step 2: Click on the "Settings" tab
            wait.Until(ExpectedConditions.ElementToBeClickable(SettingsTab)).Click();

            // Step 3: Search for employee name
            SearchBar.SendKeys(patientName);
            System.Threading.Thread.Sleep(100);  // Simulating {delay: 100} from Playwright

            // Step 4: Click "Edit TDS%" button
            wait.Until(ExpectedConditions.ElementToBeClickable(EditTDSButton)).Click();

            // Step 5: Wait for modal & enter TDS value
            //wait.Until(ExpectedConditions.ElementIsVisible(By.CssSelector("div.modal[title='Edit TDS Percent']")));
            TDSInputField.Clear();
            TDSInputField.SendKeys(updatedTDS.ToString());

            // Step 6: Click on "Update TDS" button
            wait.Until(ExpectedConditions.ElementToBeClickable(UpdateTDSButton)).Click();

            // Step 7: Refresh search & validate updated TDS
            SearchBar.Clear();
            SearchBar.SendKeys(patientName);
            System.Threading.Thread.Sleep(2000); // Wait for table to update

            // Step 8: Verify updated TDS% value
            string displayedTDS = wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("(//div[@col-id='TDSPercent'])[2]"))).Text.Trim();
            if (!displayedTDS.Equals(updatedTDS.ToString()))
            {
                throw new Exception($"TDS value mismatch! Expected: {updatedTDS}, Found: {displayedTDS}");
            }
            return isTdsValueUpdated;
        }
    }
}
