using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualBasic.FileIO;
using Microsoft.VisualStudio.TestPlatform.ObjectModel.Adapter;
using Newtonsoft.Json;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.DevTools.V130.Page;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;

namespace YakshaCSharpPL1.PageObjects
{
    internal class RadiologyPage
    {
        private readonly IWebDriver _driver;
        private readonly string _url = "https://healthapp.yaksha.com/";
        private WebDriverWait wait;

        public RadiologyPage(IWebDriver driver)
        {
            _driver = driver;
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(20));
        }

        public void GoToPage()
        {
            _driver.Navigate().GoToUrl(_url);
        }

        private IWebElement UsernameField => _driver.FindElement(By.Name("Username"));
        private IWebElement PasswordField => _driver.FindElement(By.Name("password"));
        private IWebElement SignInBtn => _driver.FindElement(By.Id("login"));
        private IWebElement RadiologyTab => _driver.FindElement(By.CssSelector("[href='#/Radiology']"));
        private IWebElement DoctorTab => _driver.FindElement(By.CssSelector("[href='#/Doctors']"));
        private IWebElement ListRequestsBtn => _driver.FindElement(By.CssSelector("[class='page-breadcrumb'] [href='#/Radiology/ImagingRequisitionList']"));
        private IWebElement ListReportsBtn => _driver.FindElement(By.CssSelector("[class='page-breadcrumb'] [href='#/Radiology/ImagingReportsList']"));
        private IWebElement EditDoctorsBtn => _driver.FindElement(By.CssSelector("[class='page-breadcrumb'] [href='#/Radiology/EditDoctors']"));
        private IWebElement WardBillingBtn => _driver.FindElement(By.CssSelector("[class='page-breadcrumb'] [href='#/Radiology/InpatientList']"));
        private IWebElement InPatientDepartment => _driver.FindElement(By.CssSelector("[class='page-breadcrumb '] [href='#/Doctors/InPatientDepartment']"));
        private IWebElement ListRequestsTitle => _driver.FindElement(By.XPath("//div[@class='caption custom-caption']/span[contains(text(),'Active Imaging Request')]"));
        private IWebElement ListReportsTitle => _driver.FindElement(By.XPath("//span[contains(text(),'Imaging Reports of All Patients')]"));
        private IWebElement EditDoctorsTitle => _driver.FindElement(By.XPath("//div[@class='caption']/span[contains(text(),'Edit Doctors')]"));
        private IWebElement OkBtn => _driver.FindElement(By.XPath("//button[contains(text(),'OK')]"));
        private IWebElement FirstBtn => _driver.FindElement(By.XPath("//button[contains(text(),'First')]"));
        private IWebElement PreviousBtn => _driver.FindElement(By.XPath("//button[contains(text(),'Previous')]"));
        private IWebElement NextBtn => _driver.FindElement(By.XPath("//button[contains(text(),'Next')]"));
        private IWebElement LastBtn => _driver.FindElement(By.XPath("//button[contains(text(),'Last')]"));
        private IWebElement HeaderRequestedOn => _driver.FindElement(By.XPath("//div[@col-id='CreatedOn' and contains(@class,'header')]"));
        private IWebElement HeaderHospitalNumber => _driver.FindElement(By.XPath("//div[@col-id='Patient.PatientCode' and contains(@class,'header')]"));
        private IWebElement HeaderPatientName => _driver.FindElement(By.XPath("//div[@col-id='Patient.ShortName' and contains(@class,'header')]"));
        private IWebElement HeaderAge => _driver.FindElement(By.XPath("//div[@col-id='0' and contains(@class,'header')]"));
        private IWebElement HeaderPrescriber => _driver.FindElement(By.XPath("//div[@col-id='PrescriberName' and contains(@class,'header')]"));
        private IWebElement HeaderType => _driver.FindElement(By.XPath("//div[@col-id='ImagingTypeName' and contains(@class,'header')]"));
        private IWebElement HeaderImagingName => _driver.FindElement(By.XPath("//div[@col-id='ImagingItemName' and contains(@class,'header')]"));
        private IWebElement HeaderInsurance => _driver.FindElement(By.XPath("//div[@col-id='1' and contains(@class,'header')]"));
        private IWebElement HeaderAction => _driver.FindElement(By.XPath("//div[@col-id='2' and contains(@class,'header')]"));
        private IWebElement FromDateCalendarInput => _driver.FindElement(By.XPath("(//div/input[@id='date'])[1]"));
        private IWebElement ToDateCalendarInput => _driver.FindElement(By.XPath("(//div/input[@id='date'])[2]"));
        private IWebElement SearchField => _driver.FindElement(By.Id("quickFilterInput"));
        private IWebElement ResultCountIndicator => _driver.FindElement(By.XPath("//div[@class='page-items']"));
        private IWebElement StarIcon => _driver.FindElement(By.CssSelector("[title='Remember this Date']"));
        private IWebElement PreDefinedDateRangeDropdown => _driver.FindElement(By.XPath("//div[contains(@class,'dropdown')]"));
        private List<IWebElement> PreDefinedDateRangeOptions => _driver.FindElements(By.XPath("//div[contains(@class,'dropdown')] //li/a")).ToList();
        private List<IWebElement> AllRequestedOnCells => _driver.FindElements(By.XPath("//div[@col-id='CreatedOn' and not(contains(@class,'header'))]/span")).ToList();
        private List<IWebElement> AllImagingTypeCells => _driver.FindElements(By.XPath("//div[@col-id='ImagingTypeName' and not(contains(@class,'header'))]")).ToList();
        private IWebElement FilterDropdown => _driver.FindElement(By.XPath("//b[text()='Filter']/..//select"));
        private IWebElement FromRowCount => _driver.FindElement(By.CssSelector("[ref='lbFirstRowOnPage']"));
        private IWebElement ToRowCount => _driver.FindElement(By.CssSelector("[ref='lbLastRowOnPage']"));
        private IWebElement TotalRowCount => _driver.FindElement(By.CssSelector("[ref='lbRecordCount']"));
        private IWebElement CurrentPageCount => _driver.FindElement(By.CssSelector("[ref='lbCurrent']"));
        private IWebElement TotalPageCount => _driver.FindElement(By.CssSelector("[ref='lbTotal']"));
        private IWebElement CancelButton => _driver.FindElement(By.XPath("(//button[text()='Cancel'])[1]"));
        private IWebElement MainMessage => _driver.FindElement(By.XPath("//p[contains(@class,'main-message')]"));
        private List<IWebElement> AllRows => _driver.FindElements(By.XPath("//div[@ref='eCenterContainer']/div[@row-id]")).ToList();
        private IWebElement FirstScanDoneBtn => _driver.FindElement(By.XPath("(//a[contains(text(),'Scan Done')])[1]"));
        private IWebElement FirstAddReportBtn => _driver.FindElement(By.XPath("(//a[@danphe-grid-action='show-add-report'])[1]"));
        private IWebElement DoneButtonInScanModal => _driver.FindElement(By.XPath("//button[contains(text(),'Done')]"));
        private IWebElement FilmTypeInputField => _driver.FindElement(By.CssSelector("[placeholder='Select The Film Type']"));
        private IWebElement CrossIconCancelButton => _driver.FindElement(By.CssSelector("[title='Cancel']"));
        private IWebElement FirstLabIcon => _driver.FindElement(By.XPath("(//a[@title='Labs'])[1]"));
        private IWebElement OrderTypeSelectDropdown => _driver.FindElement(By.XPath("//option[@value='Lab']/.."));
        private IWebElement SearchOrderItemsField => _driver.FindElement(By.CssSelector("[placeholder='search order items']"));
        private IWebElement ProceedButton => _driver.FindElement(By.XPath("//button[contains(text(),'Proceed')]"));
        private IWebElement SignButton => _driver.FindElement(By.XPath("//button[contains(text(),'Sign')]"));

        /*
         * This method performs a login using valid credentials.
         *
         * Steps:
         * 1. Enter the username in the username field.
         * 2. Enter the password in the password field.
         * 3. Click the Sign-In button.
         *
         * Expected Result:
         * - The user should be successfully logged in.
         * - The application should navigate to the dashboard or home page.
         *
         * Exception Handling:
         * - If elements are not found, a NoSuchElementException may be thrown.
         * - If login fails, additional validation (such as checking for error messages) should be implemented.
         */
        public void LoginWithValidCredentials()
        {
            String username = "admin", password = "pass123";
            UsernameField.SendKeys(username);
            PasswordField.SendKeys(password);
            SignInBtn.Click();
        }

        /*
         * This method navigates to the Radiology page and ensures that the page has fully loaded.
         *
         * Steps:
         * 1. Click on the "Radiology" tab.
         * 2. Wait until the "Active Imaging Request" label is visible on the page.
         *
         * Expected Result:
         * - The Radiology page loads successfully, and the "Active Imaging Request" element is present.
         *
         * Exception Handling:
         * - If the element is not found within 10 seconds, a TimeoutException is thrown.
         * - If the page does not load correctly, the test will fail due to element absence.
         */
        public void NavigateToRadiologyPage()
        {
            RadiologyTab.Click();
            WebDriverWait wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));
            wait.Until(drv => drv.FindElement(By.XPath("//span[text()='Active Imaging Request']")));
        }

        /*  
         * This method verifies the visibility of the "Radiology" tab on the UI.  
         *  
         * Steps:  
         * 1. Check if the "Radiology" tab is displayed.  
         *  
         * Assertion:  
         * - Ensures that the "Radiology" tab is visible.  
         *  
         * Exceptions:  
         * - If the element is not found, a NoSuchElementException may occur.  
         * - If the element is present but not visible, an assertion failure will be triggered.  
         */
        public void VerifyRadiologyTabIsDisplayed()
        {
            Assert.That(RadiologyTab.Displayed, Is.True, "Radiology Tab is not displayed");
        }

        /*  
         * This method verifies the visibility of key sub-modules under the "Radiology" section.  
         *  
         * Steps:  
         * 1. Click on the "Radiology" tab to open the module.  
         * 2. Check the presence of essential sub-module buttons:  
         *    - List Requests  
         *    - List Reports  
         *    - Edit Doctors  
         *    - Ward Billing  
         *  
         * Assertions:  
         * - Ensures that each expected button is displayed on the UI.  
         *  
         * Exceptions:  
         * - If an element is not found, a NoSuchElementException may occur.  
         * - If an element is present but not visible, an assertion failure will be triggered.  
         */
        public void VerifyRadiologySubModules()
        {
            RadiologyTab.Click();
            Assert.That(ListRequestsBtn.Displayed, Is.True, "List Request Button is not visible.");
            Assert.That(ListReportsBtn.Displayed, Is.True, "List Reports Button is not visible.");
            Assert.That(EditDoctorsBtn.Displayed, Is.True, "Edit Doctors Button is not visible.");
            Assert.That(WardBillingBtn.Displayed, Is.True, "Ward Billing Button is not visible.");
        }

        /*  
         * This method verifies that all required UI components on the "List Requests" page  
         * are visible after navigation.  
         *  
         * Steps:  
         * 1. Click on the "Radiology" tab.  
         * 2. Click on the "List Requests" button.  
         * 3. Validate the presence of various UI elements such as buttons, headers, input fields, and indicators.  
         *  
         * Assertions:  
         * - Ensures that each required element is visible on the page.  
         *  
         * Exceptions:  
         * - If an element is not found, a NoSuchElementException may occur.  
         * - If an element is present but not visible, an assertion failure will be triggered.  
         */
        public void VerifyListRequestsComponentsAreVisible()
        {
            RadiologyTab.Click();
            ListRequestsBtn.Click();
            Assert.That(ListRequestsTitle.Displayed, Is.True, "'Active Imaging Request' title is not visible.");
            Assert.That(OkBtn.Displayed, Is.True, "'OK' button is not visible.");
            Assert.That(FirstBtn.Displayed, Is.True, "'First' button is not visible.");
            Assert.That(PreviousBtn.Displayed, Is.True, "'Previous' button is not visible.");
            Assert.That(NextBtn.Displayed, Is.True, "'Nex' button is not visible.");
            Assert.That(LastBtn.Displayed, Is.True, "'Last' button is not visible.");
            Assert.That(HeaderRequestedOn.Displayed, Is.True, "'Requested On' header is not visible.");
            Assert.That(HeaderHospitalNumber.Displayed, Is.True, "'Hospital Number' header is not visible.");
            Assert.That(HeaderPatientName.Displayed, Is.True, "'Patient Name' header is not visible.");
            Assert.That(HeaderAge.Displayed, Is.True, "'Age' header is not visible.");
            Assert.That(HeaderPrescriber.Displayed, Is.True, "'Prescriber' header is not visible.");
            Assert.That(HeaderType.Displayed, Is.True, "'Type' header is not visible.");
            Assert.That(HeaderImagingName.Displayed, Is.True, "'Imaging Name' header is not visible.");
            Assert.That(HeaderInsurance.Displayed, Is.True, "'Insurance' header is not visible.");
            Assert.That(HeaderAction.Displayed, Is.True, "'Action' header is not visible.");
            Assert.That(FromDateCalendarInput.Displayed, Is.True, "'From' date field is not visible.");
            Assert.That(ToDateCalendarInput.Displayed, Is.True, " is not visible.");
            Assert.That(SearchField.Displayed, Is.True, "OK button is not visible.");
            Assert.That(ResultCountIndicator.Displayed, Is.True, "OK button is not visible.");
            Assert.That(StarIcon.Displayed, Is.True, "OK button is not visible.");
            Assert.That(PreDefinedDateRangeDropdown.Displayed, Is.True, "OK button is not visible.");
            Assert.That(FilterDropdown.Displayed, Is.True, "OK button is not visible.");
            Assert.That(FromRowCount.Displayed, Is.True, "OK button is not visible.");
            Assert.That(ToRowCount.Displayed, Is.True, "OK button is not visible.");
            Assert.That(TotalRowCount.Displayed, Is.True, "OK button is not visible.");
            Assert.That(CurrentPageCount.Displayed, Is.True, "OK button is not visible.");
            Assert.That(TotalPageCount.Displayed, Is.True, "OK button is not visible.");
        }

        /*  
         * This method navigates through various sub-modules under the Radiology section  
         * and verifies that the correct URLs are loaded after each navigation.  
         *  
         * Steps:  
         * 1. Click on the "List Reports" button and verify the URL.  
         * 2. Click on the "Edit Doctors" button and verify the URL.  
         * 3. Click on the "Ward Billing" button and verify the URL.  
         * 4. Click on the "List Requests" button and verify the URL.  
         *  
         * Assertions:  
         * - Ensures that the user is navigated to the correct pages by validating the URL.  
         *  
         * Exceptions:  
         * - If the elements are not found, a NoSuchElementException may occur.  
         * - If the URL does not match the expected value, an assertion failure will be triggered.  
         */
        public void NavigateToSubModulesAndVerifyUrl()
        {
            RadiologyTab.Click();
            ListReportsBtn.Click();
            WebDriverWait wait = new WebDriverWait(_driver, TimeSpan.FromMilliseconds(100));
            wait.Until(x => x.Url.Contains("ImagingReportsList"));
            Assert.That(_driver.Url, Is.EqualTo("https://healthapp.yaksha.com/Home/Index#/Radiology/ImagingReportsList"), "User is not navigated to List Reports");
            EditDoctorsBtn.Click();
            wait.Until(x => x.Url.Contains("EditDoctors"));
            Assert.That(_driver.Url, Is.EqualTo("https://healthapp.yaksha.com/Home/Index#/Radiology/EditDoctors"), "User is not navigated to List Reports");
            WardBillingBtn.Click();
            wait.Until(x => x.Url.Contains("InpatientList"));
            Assert.That(_driver.Url, Is.EqualTo("https://healthapp.yaksha.com/Home/Index#/Radiology/InpatientList"), "User is not navigated to Ward Billing");
            ListRequestsBtn.Click();
            wait.Until(x => x.Url.Contains("ImagingRequisitionList"));
            Assert.That(_driver.Url, Is.EqualTo("https://healthapp.yaksha.com/Home/Index#/Radiology/ImagingRequisitionList"), "User is not navigated to List Requests");
        }

        /*  
         * This method verifies that the tooltip text of the Star icon matches the expected value.  
         *  
         * Steps:  
         * 1. Navigate to the Radiology tab and open the "List Requests" section.  
         * 2. Retrieve the tooltip text of the Star icon using the `title` attribute.  
         * 3. Compare the actual tooltip text with the expected value.  
         *  
         * Assertions:  
         * - Ensures that the Star icon displays the correct tooltip text.  
         *  
         * Exceptions:  
         * - If the Star icon is not found, a NoSuchElementException may be thrown.  
         * - If the attribute "title" is missing, the assertion may fail.  
         */
        public void VerifyTooltipTextOfStarIcon()
        {
            //String ExpectedTooltipText = "Remember this Date"; // this is hardcoded : This value should come from json
            // Read values from JSON
            string ExpectedTooltipText = JsonReader("Radiology", "ExpectedTooltipText");
            RadiologyTab.Click();
            ListRequestsBtn.Click();
            String ActualTooltipText = StarIcon.GetAttribute("title");
            Assert.That(ExpectedTooltipText, Is.EqualTo(ActualTooltipText), "Actual Tooltip text does not match with the Expected Tooltip Text");
        }

        /*  
         * This method verifies that the applied date range remains unchanged after enabling the Star icon.  
         *  
         * Steps:  
         * 1. Navigate to the Radiology tab and open the "List Requests" section.  
         * 2. Apply a predefined date range using the `ApplyDateRange` method.  
         * 3. Scroll down and enable the Star icon.  
         * 4. Click "OK" to confirm the selection.  
         * 5. Refresh the page and reopen the "List Requests" section.  
         * 6. Retrieve and reformat the applied "From Date" and "To Date" values.  
         * 7. Validate that the actual date values match the initially applied values.  
         *  
         * Assertions:  
         * - Ensures that the applied "From Date" remains unchanged.  
         * - Ensures that the applied "To Date" remains unchanged.  
         *  
         * Exceptions:  
         * - If elements are not found, a NoSuchElementException may be thrown.  
         * - If data takes time to load, consider replacing Thread.Sleep with an explicit wait.  
         */
        public void VerifyAppliedDateRangeRemainsAfterEnablingStarIcon()
        {
            string AppliedFromDate = JsonReader("Radiology", "AppliedFromDate");
            string AppliedToDate = JsonReader("Radiology", "AppliedToDate");
            RadiologyTab.Click();
            ListRequestsBtn.Click();
            ApplyDateRange(AppliedFromDate, AppliedToDate);
            IJavaScriptExecutor js = (IJavaScriptExecutor)_driver;
            js.ExecuteScript("window.scrollBy(0, 100);");
            StarIcon.Click();
            OkBtn.Click();
            _driver.Navigate().Refresh();
            RadiologyTab.Click();
            ListRequestsBtn.Click();
            String ActualFromDate = ReformatDate(FromDateCalendarInput.GetAttribute("value"));
            Console.WriteLine("The Actual From Date is this: " + ActualFromDate);
            String ActualToDate = ReformatDate(ToDateCalendarInput.GetAttribute("value"));
            Console.WriteLine("The Actual To Date is this: " + ActualToDate);
            Assert.That(ActualFromDate, Is.EqualTo(AppliedFromDate), "Applied From date does not match the Actual From date");
            Assert.That(ActualToDate, Is.EqualTo(AppliedToDate), "Applied To date does not match the Actual To date");
        }

        /*  
         * This method verifies that the data displayed corresponds to the selected predefined date range.  
         *  
         * Steps:  
         * 1. Navigate to the Radiology tab and open the "List Requests" section.  
         * 2. Open the predefined date range dropdown and print available options.  
         * 3. Randomly select a predefined date range option and apply it.  
         * 4. Click "OK" to confirm the selection and wait for the results to load.  
         * 5. Retrieve and reformat the applied "From Date" and "To Date" values.  
         * 6. Verify that all "Requested On" dates fall within the selected range.  
         *  
         * Assertions:  
         * - Ensures that all displayed "Requested On" dates are within the applied date range.  
         *  
         * Exceptions:  
         * - If elements are not found, a NoSuchElementException may be thrown.  
         * - If data takes time to load, consider replacing Thread.Sleep with an explicit wait.  
         */
        public void VerifyDataAsPerSelectedPredefinedDateRange()
        {
            Random random = new Random();
            RadiologyTab.Click();
            ListRequestsBtn.Click();
            PreDefinedDateRangeDropdown.Click();
            Console.WriteLine($"These are the Options: {PreDefinedDateRangeOptions[0].Text}, {PreDefinedDateRangeOptions[1].Text}, {PreDefinedDateRangeOptions[2].Text}");
            IWebElement SelectedPredefinedOption = PreDefinedDateRangeOptions[random.Next(PreDefinedDateRangeOptions.Count)];
            Console.WriteLine("This is the Selected Predefined option: " + SelectedPredefinedOption);
            SelectedPredefinedOption.Click();
            OkBtn.Click();
            Thread.Sleep(7000);
            String ActualFromDate = ReformatWithHiphen(FromDateCalendarInput.GetAttribute("value"));
            String ActualToDate = ReformatWithHiphen(ToDateCalendarInput.GetAttribute("value"));
            bool isWithinRange = VerifyRequestedOnDatesAreWithinRange(ActualFromDate, ActualToDate);
            Assert.That(isWithinRange, Is.True, "One or more 'Requested On' Dates are not within the Applied Date Range");
        }

        /*  
         * This method applies a random filter to the imaging requests and verifies  
         * that the displayed records match the selected filter.  
         *  
         * Steps:  
         * 1. Define a date range from "01-01-2020" to "01-01-2025".  
         * 2. Click the Radiology tab and navigate to the "List Requests" section.  
         * 3. Apply the defined date range filter.  
         * 4. Retrieve all available filter options from the dropdown.  
         * 5. Randomly select one filter option and apply it.  
         * 6. Click "OK" to apply the filter.  
         * 7. Wait for the results to load.  
         * 8. If records exist, verify that all displayed imaging type values match the selected filter.  
         * 9. If no records exist, print a message to the console.  
         *  
         * Assertions:  
         * - Ensures that each displayed imaging type matches the selected filter option.  
         *  
         * Exceptions:  
         * - If elements are not found, a NoSuchElementException may be thrown.  
         * - If the page takes time to update, consider replacing Thread.Sleep with an explicit wait.  
         */
        public void ApplyAndVerifyRandomFilter()
        {
            Random random = new Random();
            //String AppliedFromDate = "01012020";
            //String AppliedToDate = "01012025";
            string AppliedFromDate = JsonReader("Radiology", "AppliedFromDate");
            string AppliedToDate = JsonReader("Radiology", "AppliedToDate");
            RadiologyTab.Click();
            ListRequestsBtn.Click();
            ApplyDateRange(AppliedFromDate, AppliedToDate);
            SelectElement select = new SelectElement(FilterDropdown);
            List<IWebElement> FilterOptions = _driver.FindElements(By.XPath("//b[text()='Filter']/..//select/option")).ToList();
            String FilterOptionToSelect = FilterOptions[random.Next(FilterOptions.Count)].Text;
            select.SelectByText(FilterOptionToSelect);
            OkBtn.Click();
            Thread.Sleep(10000);
            if (AllImagingTypeCells.Count > 0)
            {
                foreach (IWebElement cell in AllImagingTypeCells)
                {
                    String cellText = cell.Text;
                    Assert.That(cellText, Is.EqualTo(FilterOptionToSelect.Trim()));
                }
            }
            else
            {
                Console.WriteLine("No record exists!");
            }
        }

        /*  
         * This method verifies that an appropriate validation message appears when  
         * attempting to cancel without providing remarks.  
         *  
         * Steps:  
         * 1. Clicks on the Radiology tab.  
         * 2. Clicks on the "Ward Billing" button to navigate to the relevant section.  
         * 3. Enters the search keyword ("Devid8 Roy8") in the search field.  
         * 4. Clicks the "View Details" button for the first matching record.  
         * 5. Clicks the "Cancel" button without entering remarks.  
         * 6. Verifies that the validation message "Please Write Cancellation Remarks" appears.  
         *  
         * Assertions:  
         * - Ensures that the displayed validation message matches the expected text.  
         *  
         * Exceptions:  
         * - If elements are not found, a NoSuchElementException may be thrown.  
         * - If the page takes time to load, consider using explicit waits instead of direct clicks.  
         */
        public void VerifyValidationOnCancellationWithoutRemarks()
        {
            RadiologyTab.Click();
            WardBillingBtn.Click();
            string KeywordToSearch = JsonReader("Radiology", "PatientName");
            SearchField.SendKeys(KeywordToSearch);
            IWebElement ViewButton = _driver.FindElement(By.XPath("(//a[@danphe-grid-action='ViewDetails'])[1]"));
            ViewButton.Click();
            CancelButton.Click();
            Assert.That(MainMessage.Text, Is.EqualTo("Please Write Cancellation Remarks"), "Actual alert message does not match with the expected");
        }

        /*  
         * This method verifies that all displayed rows contain the searched keyword  
         * after performing a search operation.  
         *  
         * Steps:  
         * 1. Clicks on the Radiology tab.  
         * 2. Clicks on the "Ward Billing" button to navigate to the relevant section.  
         * 3. Enters the predefined keyword ("H2400018") into the search field.  
         * 4. Waits briefly to allow search results to load.  
         * 5. Iterates through all displayed rows and asserts that each contains the keyword.  
         *  
         * Assertions:  
         * - Ensures that every row contains the searched keyword.  
         *  
         * Exceptions:  
         * - If elements are not found, a NoSuchElementException may be thrown.  
         * - If the search does not update in time, consider replacing Thread.Sleep with an explicit wait.  
         */
        public void VerifyRowsContainTheSearchedKeyword() {
            string KeywordToSearch = JsonReader("Radiology", "KeywordToSearch");
            RadiologyTab.Click();
            WardBillingBtn.Click();
            SearchField.SendKeys(KeywordToSearch);
            Thread.Sleep(100);
            foreach (IWebElement Row in AllRows) {
                Assert.That(KeywordToSearch, Is.SubsetOf(Row.Text), "The searched keyword is not included in the Row");
            }
        }

        /*  
         * This method adds a new order from the Doctor module by selecting an order type  
         * and proceeding with the order placement.  
         *  
         * Steps:  
         * 1. Clicks on the Doctor tab to navigate to the module.  
         * 2. Clicks on the "In-Patient Department" to access the patient's records.  
         * 3. Clicks on the first available lab icon to begin the order process.  
         * 4. Selects "Imaging" as the order type from the dropdown menu.  
         * 5. Clicks on the search field and initiates a search by pressing Enter.  
         * 6. Clicks the "Proceed" button to confirm the selection.  
         * 7. Clicks the "Sign" button to complete the order process.  
         *  
         * Assertions:  
         * - No explicit assertions in this method; validations should be handled in a separate verification method.  
         *  
         * Exceptions:  
         * - If elements are not found, a NoSuchElementException may be thrown.  
         * - If the dropdown does not populate, ensure the element is properly loaded before selection.  
         */
        public void AddNewOrderFromDoctorModule() {
            DoctorTab.Click();
            InPatientDepartment.Click();
            Thread.Sleep(2000);
            FirstLabIcon.Click();
            Thread.Sleep(5000); 
            SelectElement select = new SelectElement(OrderTypeSelectDropdown);
            select.SelectByText("Imaging");
            Thread.Sleep(2000); 
            SearchOrderItemsField.Click();
            //SearchOrderItemsField.SendKeys(Keys.Enter);

            SearchOrderItemsField.Clear();

            // Enter text character by character with a delay
            foreach (char ch in "USG Chest")
            {
                SearchOrderItemsField.SendKeys(ch.ToString());
                Thread.Sleep(1000); // 1-second delay
            }

            // Press Enter after typing
            SearchOrderItemsField.SendKeys(Keys.Enter);

            ProceedButton.Click();
            Thread.Sleep(1000);
            SignButton.Click();
            Thread.Sleep(2000);
        }

        /*  
         * This method verifies that the "Film Type" field triggers a required validation  
         * when attempting to submit without selecting a value.  
         *  
         * Steps:  
         * 1. Clicks on the Radiology tab.  
         * 2. Clicks on the "List Requests" button to display available scans.  
         * 3. Clicks the first "Scan Done" button to open the scan update modal.  
         * 4. Clicks the "Done" button without selecting a film type.  
         * 5. Retrieves the "class" attribute of the Film Type input field.  
         * 6. Verifies that the class attribute contains "invalid," indicating validation failure.  
         *  
         * Assertions:  
         * - Ensures that the "invalid" class is present, confirming the required validation.  
         *  
         * Exceptions:  
         * - If elements are not found, a NoSuchElementException may be thrown.  
         */
        public void VerifyFilmTypeRequiredValidation()
        {
            RadiologyTab.Click();
            ListRequestsBtn.Click();
            FirstScanDoneBtn.Click();
            DoneButtonInScanModal.Click();
            String classValue = FilmTypeInputField.GetAttribute("class");
            Assert.That("invalid", Is.SubsetOf(classValue), "The Film Type required validation didn't appear");
        }

        /*  
         * This method updates the scan details and verifies the success message.  
         *  
         * Steps:  
         * 1. Clicks on the Radiology tab.  
         * 2. Clicks on the "List Requests" button to display the available scans.  
         * 3. Clicks the first "Scan Done" button to open the scan update modal.  
         * 4. Selects a film type by navigating through options using keyboard keys.  
         * 5. Clicks the "Done" button to save the scan update.  
         * 6. Verifies that the success message "Scan detail Updated" is displayed.  
         *  
         * Assertions:  
         * - Ensures that the success message matches the expected text.  
         *  
         * Exceptions:  
         * - If elements are not found, a NoSuchElementException may be thrown.  
         */
        public void UpdateScanAndVerifySuccessMessagea()
        {
            RadiologyTab.Click();
            ListRequestsBtn.Click();
            FirstScanDoneBtn.Click();
            FilmTypeInputField.Click();
            FilmTypeInputField.SendKeys(Keys.ArrowUp);
            FilmTypeInputField.SendKeys(Keys.Enter);
            DoneButtonInScanModal.Click();
            Assert.That(MainMessage.Text, Is.EqualTo("Scan detail Updated"), "The success message didn't appear");
        }

        /*  
         * This method verifies the confirmation alert when attempting to close  
         * the "Add Report" modal without making any entry.  
         *  
         * Steps:  
         * 1. Clicks on the Radiology tab.  
         * 2. Clicks on the "List Requests" button.  
         * 3. Opens the "Add Report" modal by clicking the first "Add Report" button.  
         * 4. Clicks the cancel (cross) button to close the modal.  
         * 5. Captures the alert message and verifies its correctness.  
         * 6. Accepts the alert to proceed with closing the modal.  
         *  
         * Assertions:  
         * - Ensures that the confirmation alert message matches the expected text.  
         *  
         * Exceptions:  
         * - If the alert is not present, NoAlertPresentException may be thrown.  
         */
        public void VerifyConfirmationAlertOnClosingAddReportModalWithoutEntry()
        {
            RadiologyTab.Click();
            ListRequestsBtn.Click();
            FirstAddReportBtn.Click();
            CrossIconCancelButton.Click();
            IAlert alert = _driver.SwitchTo().Alert();
            Assert.That(alert.Text, Is.EqualTo("Changes will be discarded. Do you want to close anyway?"), "The actual alert message does not match the expected");
            alert.Accept();
        }

        /*  
         * This method performs the following steps:  
         * 1. Clicks on the Radiology tab.  
         * 2. Clicks on the "List Requests" button to display available reports.  
         * 3. Clicks on the first "Add Report" button.  
         * 4. Finds and clicks the "Save" button after ensuring it is in view.  
         * 5. Verifies that the success message matches the expected text.  
         *  
         * Assertions:  
         * - Checks if the main message displayed after saving is "Report Added Successfully".  
         *  
         * Exceptions:  
         * - If elements are not found, this method may throw a NoSuchElementException.  
         */
        public void AddReportAndVerify()
        {
            RadiologyTab.Click();
            ListRequestsBtn.Click();
            FirstAddReportBtn.Click();
            IWebElement SaveBtn = _driver.FindElement(By.CssSelector("[value='Save']"));
            IJavaScriptExecutor js = (IJavaScriptExecutor)_driver;
            js.ExecuteScript("arguments[0].scrollIntoView(true);", SaveBtn);
            SaveBtn.Click();
            Assert.That(MainMessage.Text, Is.EqualTo("Report Added Successfully"), "The actual alert message does not match the expected " + MainMessage.Text + "");
        }

        /*  
         * This method verifies that all "Requested On" dates displayed in the UI  
         * fall within the specified date range.  
         *  
         * The method iterates through each date element in "AllRequestedOnCells",  
         * parses the date, and checks if it lies within the given range.  
         *  
         * Parameters:  
         * fromDate - The start date of the range (string format).  
         * toDate   - The end date of the range (string format).  
         *  
         * Returns:  
         * true  - If all dates are within the specified range.  
         * false - If any date is outside the range or if an invalid date format is encountered.  
         */
        public bool VerifyRequestedOnDatesAreWithinRange(string fromDate, string toDate)
        {
            DateTime startDate = DateTime.Parse(fromDate);
            DateTime endDate = DateTime.Parse(toDate);

            foreach (IWebElement cell in AllRequestedOnCells)
            {
                DateTime cellDate;
                Console.WriteLine(DateTime.TryParse(cell.Text, out cellDate));
                if (DateTime.TryParse(cell.Text, out cellDate))
                {
                    if (cellDate < startDate || cellDate > endDate)
                    {
                        Console.WriteLine($"Date {cellDate.ToString("yyyy-MM-dd")} is out of range.");
                        return false;
                    }
                }
                else
                {
                    Console.WriteLine($"Invalid date format: {cell.Text}");
                    return false;
                }
            }
            return true;
        }

        /*  
         * This method reformats a given date string by rearranging its parts  
         * and removing hyphens.  
         *  
         * Expected input format: "DD-MM-YYYY"  
         * Expected output format: "YYYYMMDD"  
         *  
         * Parameters:  
         * date - The date string to be reformatted.  
         *  
         * Returns:  
         * A reformatted date string in "YYYYMMDD" format.  
         */
        static string ReformatDate(string date)
        {
            string[] parts = date.Split('-');
            return $"{parts[2]}{parts[1]}{parts[0]}";
        }

        /*  
         * This method reformats a given date string by rearranging its parts  
         * and inserting hyphens in the new format.  
         *  
         * Expected input format: "DD-MM-YYYY"  
         * Expected output format: "YYYY-MM-DD"  
         *  
         * Parameters:  
         * date - The date string to be reformatted.  
         *  
         * Returns:  
         * A reformatted date string in "YYYY-MM-DD" format.  
         */
        static string ReformatWithHiphen(string date)
        {
            string[] parts = date.Split('-');
            return $"{parts[2]}-{parts[1]}-{parts[0]}";
        }

        /*  
         * This method applies a date range by entering the given  
         * start and end dates into the respective calendar input fields.  
         *  
         * Parameters:  
         * FromDate - The start date to be entered.  
         * ToDate   - The end date to be entered.  
         */
        public void ApplyDateRange(String FromDate, String ToDate)
        {
            FromDateCalendarInput.SendKeys(FromDate);
            ToDateCalendarInput.SendKeys(ToDate);
            Thread.Sleep(10000);
        }

        /*  
         * Method to read the json data
         */
        public string JsonReader(string section, string key)
        {
            // Traverse from bin directory back to project root
            string projectRoot = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName;
            string jsonFilePath = Path.Combine(projectRoot, "TestData", "jsondata.json");

            // Read and parse JSON file
            var jsonData = JsonConvert.DeserializeObject<Dictionary<string, Dictionary<string, string>>>(File.ReadAllText(jsonFilePath));

            // Return the value based on section and key
            return jsonData.TryGetValue(section, out var sectionData) && sectionData.TryGetValue(key, out var value)
                ? value
                : throw new KeyNotFoundException($"Key '{key}' not found in section '{section}'");
        }

    }
}