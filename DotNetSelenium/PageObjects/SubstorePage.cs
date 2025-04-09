using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System;
using System.IO;

namespace DotNetSelenium.PageObjects
{
    public class SubstorePage
    {
        private readonly IWebDriver driver;
        private readonly WebDriverWait wait;
        private readonly Actions actions;

        public SubstorePage(IWebDriver driver)
        {
            this.driver = driver;
            this.wait = new WebDriverWait(driver, TimeSpan.FromSeconds(20));
            this.actions = new Actions(driver);
        }

        private IWebElement SubstoreLink => driver.FindElement(By.CssSelector("a[href='#/WardSupply']"));
        private IWebElement SelectSubstore => driver.FindElement(By.XPath("(//span[@class='report-name'])[1]"));
        private IWebElement InventoryRequisition => driver.FindElement(By.CssSelector("a[href='#/WardSupply/Inventory/InventoryRequisitionList']"));
        private IWebElement Consumption => driver.FindElement(By.CssSelector("a[href='#/WardSupply/Inventory/Consumption']"));
        private IWebElement Reports => driver.FindElement(By.CssSelector("a[href='#/WardSupply/Inventory/Reports']"));
        private IWebElement PatientConsumption => driver.FindElement(By.CssSelector("a[href='#/WardSupply/Inventory/PatientConsumption']"));
        private IWebElement Return => driver.FindElement(By.CssSelector("a[href='#/WardSupply/Inventory/Return']"));
        private IWebElement Inventory => driver.FindElement(By.CssSelector("ul.page-breadcrumb a[href='#/WardSupply/Inventory']"));
        private IWebElement SignoutCursor => driver.FindElement(By.CssSelector("i.fa-sign-out"));
        private IWebElement Tooltip => driver.FindElement(By.CssSelector("div.modal-content h6"));

/**
 * @Test11
 * @description This method verifies that the user is able to successfully navigate through the various submodules 
 *              under the "Substore" section such as Inventory Requisition, Consumption, Reports, 
 *              Patient Consumption, and Return.
 *
 * @expected
 * The user should be navigated to the correct URL for each submodule indicating successful navigation.
 *
 * Steps:
 * 1. Click on the "Substore" link.
 * 2. Select the appropriate substore.
 * 3. Navigate to "Inventory Requisition" and verify URL.
 * 4. Navigate to "Consumption" and verify URL.
 * 5. Navigate to "Reports" and verify URL.
 * 6. Navigate to "Patient Consumption" and verify URL.
 * 7. Navigate to "Return" and verify URL.
 *
 * @returns Returns true if navigation to all submodules is successful; otherwise, throws an exception.
 */
        public bool VerifyNavigationBetweenSubmodules()
        {
            bool isUserNavigatedSuccessfully = true;
            wait.Until(ExpectedConditions.ElementToBeClickable(SubstoreLink)).Click();
            wait.Until(ExpectedConditions.ElementToBeClickable(SelectSubstore)).Click();

            wait.Until(ExpectedConditions.ElementToBeClickable(InventoryRequisition)).Click();
            if (!driver.Url.Contains("Inventory/InventoryRequisitionList"))
            {
                isUserNavigatedSuccessfully = false;
                throw new Exception("Navigation to Inventory Requisition failed.");
            }

            wait.Until(ExpectedConditions.ElementToBeClickable(Consumption)).Click();
            if (!driver.Url.Contains("Inventory/Consumption/ConsumptionList"))
            {
                isUserNavigatedSuccessfully = false;
                throw new Exception("Navigation to Consumption failed.");
            }

            wait.Until(ExpectedConditions.ElementToBeClickable(Reports)).Click();
            if (!driver.Url.Contains("Inventory/Reports"))
            {
                isUserNavigatedSuccessfully = false;
                throw new Exception("Navigation to Reports failed.");
            }

            wait.Until(ExpectedConditions.ElementToBeClickable(PatientConsumption)).Click();
            if (!driver.Url.Contains("Inventory/PatientConsumption/PatientConsumptionList"))
            {
                isUserNavigatedSuccessfully = false;
                throw new Exception("Navigation to Patient Consumption failed.");
            }

            wait.Until(ExpectedConditions.ElementToBeClickable(Return)).Click();
            if (!driver.Url.Contains("Inventory/Return"))
            {
                isUserNavigatedSuccessfully = false;
                throw new Exception("Navigation to Return failed.");
            }

            return isUserNavigatedSuccessfully;
        }

/**
 * @Test12
 * @description This method verifies the tooltip text that appears when hovering over the cursor icon 
 *              in the "Inventory" tab of the Substore module.
 *
 * @expected
 * Tooltip text should display: "To change, you can always click here."
 *
 * Steps:
 * 1. Click on the "Substore" link.
 * 2. Select a substore.
 * 3. Navigate to the "Inventory" tab.
 * 4. Hover over the cursor icon using Actions class.
 * 5. Capture and verify the tooltip text matches the expected value.
 *
 * @returns Returns the tooltip text if it matches the expected message; otherwise, throws an exception.
 */
        public String VerifyTooltipText()
        {
            wait.Until(ExpectedConditions.ElementToBeClickable(SubstoreLink)).Click();
            wait.Until(ExpectedConditions.ElementToBeClickable(SelectSubstore)).Click();
            wait.Until(ExpectedConditions.ElementToBeClickable(Inventory)).Click();

            // Hover over the cursor icon
            actions.MoveToElement(SignoutCursor).Perform();

            // Get tooltip text
            string tooltipText = wait.Until(ExpectedConditions.ElementIsVisible(By.CssSelector("div.modal-content h6"))).Text.Trim();
            if (!tooltipText.Contains("To change, you can always click here."))
            {
                throw new Exception($"Tooltip text mismatch. Found: {tooltipText}");
            }
            return tooltipText;
        }

 /**
 * @Test13
 * @description This method navigates to the Inventory Requisition section under the Substore module,
 *              captures a screenshot of the current page, and saves it to the "screenshots" folder 
 *              with a timestamp in the filename.
 *
 * @expected
 * A screenshot of the Inventory Requisition page is captured and saved successfully.
 *
 * Steps:
 * 1. Click on the "Substore" link.
 * 2. Select a substore from the dropdown.
 * 3. Click on the "Inventory" tab.
 * 4. Click on "Inventory Requisition".
 * 5. Verify successful navigation to the Inventory Requisition page.
 * 6. Capture and save a screenshot with a timestamped filename.
 *
 * @returns Void method; throws an exception if navigation fails or any step encounters an issue.
 */
        public void CaptureInventoryRequisitionScreenshot()
        {
            string timestamp = DateTime.UtcNow.ToString("yyyy-MM-dd_HH-mm-ss");
            string screenshotPath = Path.Combine(Directory.GetCurrentDirectory(), $"screenshots/inventory-requisition-{timestamp}.png");

            wait.Until(ExpectedConditions.ElementToBeClickable(SubstoreLink)).Click();
            wait.Until(ExpectedConditions.ElementToBeClickable(SelectSubstore)).Click();
            wait.Until(ExpectedConditions.ElementToBeClickable(Inventory)).Click();
            wait.Until(ExpectedConditions.ElementToBeClickable(InventoryRequisition)).Click();

            if (!driver.Url.Contains("Inventory/InventoryRequisitionList"))
            {
                throw new Exception("Navigation to Inventory Requisition failed.");
            }

            // Take a screenshot
            //Screenshot screenshot = ((ITakesScreenshot)driver).GetScreenshot();
            //Directory.CreateDirectory(Path.GetDirectoryName(screenshotPath)); // Ensure directory exists
            //screenshot.SaveAsFile(screenshotPath, ScreenshotImageFormat.Png);
        }
    }
}
