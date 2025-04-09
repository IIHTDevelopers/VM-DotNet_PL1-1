using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System;
using System.Collections.Generic;

namespace DotNetSelenium.PageObjects
{
    public class AppointmentPage
    {
        private IWebDriver driver;
        private WebDriverWait wait;

        public AppointmentPage(IWebDriver driver)
        {
            this.driver = driver;
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(20));
        }

        // Locators for Appointment Page Elements
        public By AppointmentLink => By.CssSelector("a[href='#/Appointment']");
        public By CounterItem => By.XPath("//div[@class='counter-item']");
        public By AppointmentBookingList => By.CssSelector("ul.page-breadcrumb li a[href='#/Appointment/ListAppointment']");
        public By VisitTypeDropdown => By.CssSelector("select[name='VistType']");
        public By FromDate => By.XPath("(//input[@id='date'])[1]");
        public By ShowPatient => By.XPath("//button[contains(text(),'Show Patient')]");
        public By VisitTypeColumn => By.XPath("//div[text()=\"New\"]");



/**
 * @Test1
 * Selects "New Patient" from the Visit Type dropdown and verifies that all results reflect "New Visit" in the Visit Type column.
 *
 * Steps:
 * 1. Clicks on the Appointment link and selects the first available counter item (if present).
 * 2. Navigates to the Appointment Booking List page.
 * 3. Selects "New Patient" from the Visit Type dropdown.
 * 4. Sets the From Date to "01-01-2024" to filter results.
 * 5. Clicks the "Show Patient" button to retrieve appointment data.
 * 6. Validates that the Visit Type column displays only "New Visit" entries.
 *
 * @returns True if all entries in the Visit Type column contain "New"; otherwise, returns false.
 */
        public bool VerifyVisitTypeDropdown()
        {
            bool isVisitType = false;
            // Click on the Appointment link
            wait.Until(ExpectedConditions.ElementToBeClickable(AppointmentLink)).Click();

            // Wait and check if counter items are available
            System.Threading.Thread.Sleep(10000);
            var counterItems = driver.FindElements(CounterItem);
            int counterCount = counterItems.Count;
            Console.WriteLine("Counter count is " + counterCount);

            // If there are counter items, click the first one and navigate to the Appointment link again
            if (counterCount > 0)
            {
                counterItems[0].Click();
                wait.Until(ExpectedConditions.ElementToBeClickable(AppointmentLink)).Click();
            }
            else
            {
                Console.WriteLine("No counter items available");
            }

            // Click on the Appointment Booking List
            wait.Until(ExpectedConditions.ElementToBeClickable(AppointmentBookingList)).Click();

            // Select "New Patient" from the dropdown
            new SelectElement(driver.FindElement(VisitTypeDropdown)).SelectByText("New Patient");

            // Select "January 2024" in the FROM date field
            IWebElement fromDateField = driver.FindElement(FromDate);
            fromDateField.Clear();
            fromDateField.SendKeys("01-01-2024");

            // Click the "Show Patient" button
            driver.FindElement(ShowPatient).Click();
            System.Threading.Thread.Sleep(5000);

            // Validate that the "Visit Type" column contains only "New Visit"
            IList<IWebElement> visitTypeCells = driver.FindElements(VisitTypeColumn);
            int visitTypeCount = visitTypeCells.Count;
            Console.WriteLine($"Visit count >> {visitTypeCount}");

            for (int i = 1; i < visitTypeCount; i++)
            {
                string visitTypeText = visitTypeCells[i].Text.Trim();
                if (visitTypeText.Contains("New"))
                {
                    isVisitType = true;
                }
                else
                {
                    isVisitType = false;
                }
            }
            return isVisitType;
        }
    }
}
