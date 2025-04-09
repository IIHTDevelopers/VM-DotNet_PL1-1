using System;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using Newtonsoft.Json.Linq;
using System.IO;
using System.Threading;
using DotNetSelenium.Utilities;

namespace DotNetSelenium.PageObjects
{
    public class DoctorPage
    {
        private IWebDriver driver;
        private WebDriverWait wait;
        private JObject testData;

        public DoctorPage(IWebDriver driver)
        {
            this.driver = driver;
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(20));
        }

        // Locators
        public By DoctorLink => By.CssSelector("a[href='#/Doctors']");
        public By InPatientTab => By.CssSelector("ul.page-breadcrumb a[href='#/Doctors/InPatientDepartment']");
        public By SearchBox => By.CssSelector("input#quickFilterInput");
        public By ActionsPreviewIcon => By.CssSelector("a[title='Preview']");
        public By PatientNameHeading => By.CssSelector("h1.pat-name-hd");
        public By NotesSection => By.CssSelector("a[href='#/Doctors/PatientOverviewMain/NotesSummary']");
        public By AddNotesButton => By.XPath("//a[text()='Add Notes']");
        public By TemplateDropdown => By.CssSelector("input[value-property-name='TemplateName']");
        public By SubjectiveNotesField => By.XPath("//label[text()='Subjective Notes']/../div/textarea");
        public By SuccessConfirmationPopup => By.XPath("//p[contains(text(),'Success')]/../p[contains(text(),'Progress Note Template added.')]");
        public By SaveNotesButton => By.XPath("//button[contains(text(),'Save')]");
        public By NoteType => By.CssSelector("input[placeholder='Select Note Type']");

        /**
        * @Test3
        * @description Searches for a specific patient in the In-Patient list and verifies that their overview page is displayed.
        *
        * Steps:
        * 1. Reads the patient name from a JSON test data file.
        * 2. Clicks on the Doctor module and navigates to the In-Patient tab.
        * 3. Locates the appropriate search box and enters the patient name.
        * 4. Waits briefly and clicks the preview icon from the Actions column.
        * 5. Retrieves and returns the patient name heading from the overview page for validation.
        *
        * @returns The patient name displayed on the overview page, converted to lowercase and trimmed.
        */
        public String VerifyPatientOverview()
        {
            JObject testData = TestDataReader.LoadJson("PatientName.json");
            string patientName = testData["PatientNames"][0]["Patient1"].ToString();
            wait.Until(ExpectedConditions.ElementToBeClickable(DoctorLink)).Click();
            wait.Until(ExpectedConditions.ElementToBeClickable(InPatientTab)).Click();

            // Search for the patient
            var searchBoxes = driver.FindElements(SearchBox);
            foreach (var searchBox in searchBoxes)
            {
                if (searchBox.Displayed)
                {
                    searchBox.SendKeys(patientName);
                    break;
                }
            }
            Thread.Sleep(2000); // Equivalent to Playwright's waitForTimeout(2000)

            // Click on the preview icon under Actions
            wait.Until(ExpectedConditions.ElementToBeClickable(ActionsPreviewIcon)).Click();

            // Verify patient name
            IWebElement patientHeading = wait.Until(ExpectedConditions.ElementIsVisible(PatientNameHeading));
            String actualPatientHeadingText = patientHeading.Text.ToLower().Trim();

            return actualPatientHeadingText;
        }

        /**
        * @Test4
        * @description
        * Adds a "Progress Note" for a specified patient from the In-Patient list and verifies the success message.
        *
        * Steps:
        * 1. Reads the patient name from the JSON test data file.
        * 2. Navigates to the Doctor module and selects the In-Patient tab.
        * 3. Searches for the patient by name and opens their preview page.
        * 4. Navigates to the Notes section and clicks the Add Notes button.
        * 5. Selects "Progress Note" as the note type and template.
        * 6. Enters a sample subjective note in the input field.
        * 7. Saves the note and verifies the success confirmation message.
        *
        * @returns The text of the success confirmation popup after saving the note.
        */
        public String AddProgressNoteForPatient()
        {
            JObject testData = TestDataReader.LoadJson("PatientName.json");
            string patientName = testData["PatientNames"][1]["Patient2"].ToString();
            wait.Until(ExpectedConditions.ElementToBeClickable(DoctorLink)).Click();
            wait.Until(ExpectedConditions.ElementToBeClickable(InPatientTab)).Click();

            // Search for the patient
            var searchBoxes = driver.FindElements(SearchBox);
            foreach (var searchBox in searchBoxes)
            {
                if (searchBox.Displayed)
                {
                    searchBox.SendKeys(patientName);
                    break;
                }
            }
            Thread.Sleep(2000);

            // Click on the preview icon under Actions
            wait.Until(ExpectedConditions.ElementToBeClickable(ActionsPreviewIcon)).Click();

            // Click on Notes section
            wait.Until(ExpectedConditions.ElementToBeClickable(NotesSection)).Click();

            // Click on Add Notes button
            wait.Until(ExpectedConditions.ElementToBeClickable(AddNotesButton)).Click();

            wait.Until(ExpectedConditions.ElementToBeClickable(NoteType)).Click();
            driver.FindElement(NoteType).SendKeys("Progress Note");
            driver.FindElement(NoteType).SendKeys(Keys.Enter);

            // Select "Progress Note" from the Template dropdown
            wait.Until(ExpectedConditions.ElementToBeClickable(TemplateDropdown)).Click();
            driver.FindElement(TemplateDropdown).SendKeys("Progress Note");
            driver.FindElement(TemplateDropdown).SendKeys(Keys.Enter);

            // Enter subjective notes
            Thread.Sleep(1000);
            driver.FindElement(SubjectiveNotesField).SendKeys("Test Notes");
            Thread.Sleep(1000);

            // Click Save
            wait.Until(ExpectedConditions.ElementToBeClickable(SaveNotesButton)).Click();
            Thread.Sleep(2000);

            string successMessage = "";
            // Verify success confirmation popup
            if (driver.FindElements(SuccessConfirmationPopup).Count > 0)
            {
                successMessage = driver.FindElement(SuccessConfirmationPopup).Text;
            }
            return successMessage;
        }
    }
}
