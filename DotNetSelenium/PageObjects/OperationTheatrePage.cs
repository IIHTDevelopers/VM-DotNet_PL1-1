using System;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;

namespace DotNetSelenium.PageObjects
{
    public class OperationTheatrePage
    {
        private IWebDriver driver;
        private WebDriverWait wait;

        public OperationTheatrePage(IWebDriver driver)
        {
            this.driver = driver;
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(20));
        }

        // Locators for Operation Theatre Page Elements
        public By OperationTheatreLink => By.CssSelector("a[href='#/OperationTheatre']");
        public By NewOtBookingButton => By.XPath("//button[contains(text(),'New OT Booking')]");
        public By AddNewOtButton => By.CssSelector("input[value='Add New OT']");
        public By ModalHeading => By.CssSelector("div.modelbox-div");

 /**
 * @Test2
 * @description : Attempts to book an OT (Operation Theatre) without selecting a patient and verifies the alert message.
 *
 * Steps:
 * 1. Navigates to the Operation Theatre module.
 * 2. Clicks on the "New OT Booking" button to open the booking modal.
 * 3. Validates that the OT booking modal is displayed.
 * 4. Clicks the "Add New OT" button without selecting a patient.
 * 5. Waits for the browser alert and captures the alert message.
 * 6. Accepts the alert to close it.
 *
 * @returns : The text of the alert message shown when no patient is selected during OT booking.
 */
        public String HandleOtBookingAlert()
        {
            // Click on the Operation Theatre link
            wait.Until(ExpectedConditions.ElementToBeClickable(OperationTheatreLink)).Click();

            // Click on the "New OT Booking" button
            wait.Until(ExpectedConditions.ElementToBeClickable(NewOtBookingButton)).Click();

            // Verify the modal is displayed
            IWebElement modalElement = wait.Until(ExpectedConditions.ElementIsVisible(ModalHeading));
            Assert.That(modalElement.Displayed, Is.True, "Modal heading is not displayed.");

            // Click on the "Add New OT" button
            wait.Until(ExpectedConditions.ElementToBeClickable(AddNewOtButton)).Click();

            // Wait for and handle the alert
            IAlert alert = wait.Until(ExpectedConditions.AlertIsPresent());
            string alertMessage = alert.Text;
            Console.WriteLine("Alert Message: " + alertMessage);
            alert.Accept();

            // Return the alert message
            return alertMessage;
        }
    }
}
