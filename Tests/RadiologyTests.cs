using System;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using YakshaCSharpPL1.PageObjects;

namespace YakshaCSharpPL1.Tests
{
    internal class RadiologyTests
    {
        [TestFixture]
        public class WebFormTests
        {
            private IWebDriver? _driver;
            private RadiologyPage? _radiologyPage;
            private VerifyTests _verify;

            [SetUp]
            public void Setup()
            {
                _driver = new ChromeDriver();
                _driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromMilliseconds(30000);
                _driver.Manage().Window.Maximize();
                _radiologyPage = new RadiologyPage(_driver);
                _verify = new VerifyTests(_driver);
            }

            [TearDown]
            public void TearDown()
            {
                _driver.Quit();
            }

            /// <summary>
            /// This test verifies that the Radiology tab is displayed on the page.
            /// </summary>
            /// <remarks>
            /// Test Execution Order: 1  
            /// Preconditions:  
            /// - The user must have valid credentials.  
            /// - The Radiology module should be accessible.  
            /// Steps:  
            /// 1. Navigate to the application page.  
            /// 2. Log in with valid credentials.  
            /// 3. Check if the Radiology tab is displayed.  
            /// Expected Result:  
            /// - The Radiology tab should be visible on the UI.
            /// </remarks>
            [Test, Order(1)]
            public void VerifyRadiologyTabIsDisplayed()
            {
                _radiologyPage.GoToPage();
                _radiologyPage.LoginWithValidCredentials();
                _radiologyPage.VerifyRadiologyTabIsDisplayed();
                _verify.VerifyTestOne();

            }

            /// <summary>
            /// This test verifies that all Radiology sub-modules are visible on the page.
            /// </summary>
            /// <remarks>
            /// Test Execution Order: 2  
            /// Preconditions:  
            /// - The user must have valid credentials.  
            /// - The Radiology module should be accessible.  
            /// Steps:  
            /// 1. Navigate to the Radiology page.  
            /// 2. Log in with valid credentials.  
            /// 3. Verify the visibility of all expected sub-modules.  
            /// Expected Result:  
            /// - The sub-modules (List Requests, List Reports, Edit Doctors, Ward Billing) should be visible.
            /// </remarks>
            [Test, Order(2)]
            public void VerifyRadiologySubModules()
            {
                _radiologyPage.GoToPage();
                _radiologyPage.LoginWithValidCredentials();
                _radiologyPage.VerifyRadiologySubModules();
                _verify.VerifyTestTwo();
            }

            /// <summary>
            /// This test verifies that the user can successfully navigate to the Radiology page.
            /// </summary>
            /// <remarks>
            /// Test Execution Order: 3  
            /// Preconditions:  
            /// - The user must have valid credentials.  
            /// - The Radiology module should be accessible.  
            /// Steps:  
            /// 1. Navigate to the Radiology page.  
            /// 2. Log in with valid credentials.  
            /// 3. Verify that the page loads successfully and the expected elements are present.  
            /// Expected Result:  
            /// - The Radiology page should be displayed correctly.
            /// </remarks>
            [Test, Order(3)]
            public void NavigateToRadiologyPageAndVerify()
            {
                _radiologyPage.GoToPage();
                _radiologyPage.LoginWithValidCredentials();
                _radiologyPage.NavigateToRadiologyPage();
                _verify.VerifyTestThree();
            }

            /// <summary>
            /// This test verifies that all required components in the "List Requests" section of the Radiology module are visible.
            /// </summary>
            /// <remarks>
            /// Test Execution Order: 4  
            /// Preconditions:  
            /// - The user must have valid credentials.  
            /// - The Radiology page should be accessible.  
            /// Steps:  
            /// 1. Navigate to the Radiology page.  
            /// 2. Log in with valid credentials.  
            /// 3. Open the "List Requests" section.  
            /// 4. Verify the visibility of key UI components such as buttons, headers, filters, and date fields.  
            /// Expected Result:  
            /// - All required components should be visible on the "List Requests" page.
            /// </remarks>
            [Test, Order(4)]
            public void VerifyListRequestsComponentsAreVisible()
            {
                _radiologyPage.GoToPage();
                _radiologyPage.LoginWithValidCredentials();
                _radiologyPage.VerifyListRequestsComponentsAreVisible();
                _verify.VerifyTestFour();
            }

            /// <summary>
            /// This test verifies navigation to different sub-modules within the Radiology section and validates their URLs.
            /// </summary>
            /// <remarks>
            /// Test Execution Order: 5  
            /// Preconditions:  
            /// - The user must have valid credentials.  
            /// - The Radiology page should be accessible.  
            /// Steps:  
            /// 1. Navigate to the Radiology page.  
            /// 2. Log in with valid credentials.  
            /// 3. Click on each sub-module (List Reports, Edit Doctors, Ward Billing, List Requests).  
            /// 4. Validate that the user is navigated to the correct URL for each sub-module.  
            /// Expected Result:  
            /// - The URLs of each sub-module should match the expected values.
            /// </remarks>
            [Test, Order(5)]
            public void NavigateToSubModulesAndVerifyUrl()
            {
                _radiologyPage.GoToPage();
                _radiologyPage.LoginWithValidCredentials();
                _radiologyPage.NavigateToSubModulesAndVerifyUrl();
                _verify.VerifyTestFive();
            }

            /// <summary>
            /// This test verifies that the tooltip text of the star icon matches the expected value.
            /// </summary>
            /// <remarks>
            /// Test Execution Order: 6  
            /// Preconditions:  
            /// - The user must have valid credentials.  
            /// - The Radiology page should be accessible.  
            /// Steps:  
            /// 1. Navigate to the Radiology page.  
            /// 2. Log in with valid credentials.  
            /// 3. Hover over the star icon to view its tooltip.  
            /// 4. Retrieve the actual tooltip text.  
            /// 5. Compare it with the expected tooltip text.  
            /// Expected Result:  
            /// - The tooltip text of the star icon should match the expected value.
            /// </remarks>
            [Test, Order(6)]
            public void VerifyTooltipTextOfStarIcon()
            {
                _radiologyPage.GoToPage();
                _radiologyPage.LoginWithValidCredentials();
                _radiologyPage.VerifyTooltipTextOfStarIcon();
                _verify.VerifyTestSix();
            }

            /// <summary>
            /// This test verifies that the applied date range remains unchanged after enabling the star icon.
            /// </summary>
            /// <remarks>
            /// Test Execution Order: 7  
            /// Preconditions:  
            /// - The user must have valid credentials.  
            /// - The Radiology page should be accessible.  
            /// Steps:  
            /// 1. Navigate to the Radiology page.  
            /// 2. Log in with valid credentials.  
            /// 3. Apply a specific date range.  
            /// 4. Enable the star icon to remember the selected date range.  
            /// 5. Refresh the page and navigate back to the List Requests section.  
            /// 6. Verify that the previously applied date range is retained.  
            /// Expected Result:  
            /// - The applied date range should remain unchanged after enabling the star icon.  
            /// </remarks>
            [Test, Order(7)]
            public void VerifyAppliedDateRangeRemainsAfterEnablingStarIcon()
            {
                _radiologyPage.GoToPage();
                _radiologyPage.LoginWithValidCredentials();
                _radiologyPage.VerifyAppliedDateRangeRemainsAfterEnablingStarIcon();
                _verify.VerifyTestSeven();
            }

            /// <summary>
            /// This test verifies that the displayed data aligns with the selected predefined date range.
            /// </summary>
            /// <remarks>
            /// Test Execution Order: 8  
            /// Preconditions:  
            /// - The user must have valid credentials.  
            /// - The Radiology page should be accessible.  
            /// Steps:  
            /// 1. Navigate to the Radiology page.  
            /// 2. Log in with valid credentials.  
            /// 3. Select a predefined date range from the dropdown.  
            /// 4. Verify that the data displayed falls within the selected date range.  
            /// Expected Result:  
            /// - The records displayed should match the selected date range.  
            /// </remarks>
            [Test, Order(8)]
            public void VerifyDataAsPerSelectedPredefinedDateRange()
            {
                _radiologyPage.GoToPage();
                _radiologyPage.LoginWithValidCredentials();
                _radiologyPage.VerifyDataAsPerSelectedPredefinedDateRange();
                _verify.VerifyUserIsOnCorrectURL("/Radiology/ImagingRequisitionList");
            }

            /// <summary>
            /// This test applies a filter on the Radiology page and verifies that the displayed results match the applied filter.
            /// </summary>
            /// <remarks>
            /// Test Execution Order: 9  
            /// Preconditions:  
            /// - The user must have valid credentials.  
            /// - The Radiology page should be accessible.  
            /// Steps:  
            /// 1. Navigate to the Radiology page.  
            /// 2. Log in with valid credentials.  
            /// 3. Apply a random filter.  
            /// 4. Verify that the displayed data corresponds to the applied filter.  
            /// Expected Result:  
            /// - The filtered results should correctly reflect the applied filter criteria.  
            /// </remarks>
            [Test, Order(9)]
            public void ApplyAndVerifyFilter()
            {
                _radiologyPage.GoToPage();
                _radiologyPage.LoginWithValidCredentials();
                _radiologyPage.ApplyAndVerifyRandomFilter();
                _verify.VerifyTestNine();
            }

            /// <summary>
            /// This test verifies that a validation message appears when attempting to cancel without providing remarks.
            /// </summary>
            /// <remarks>
            /// Test Execution Order: 10  
            /// Preconditions:  
            /// - The user must have valid credentials.  
            /// - The Radiology page should be accessible.  
            /// Steps:  
            /// 1. Navigate to the Radiology page.  
            /// 2. Log in with valid credentials.  
            /// 3. Attempt to cancel a request without entering remarks.  
            /// 4. Verify that a validation message is displayed.  
            /// Expected Result:  
            /// - The system should prevent cancellation without remarks and show an appropriate validation message.  
            /// </remarks>
            [Test, Order(10)]
            public void VerifyValidationOnCancellationWithoutRemarks()
            {
                _radiologyPage.GoToPage();
                _radiologyPage.LoginWithValidCredentials();
                _radiologyPage.VerifyValidationOnCancellationWithoutRemarks();
                _verify.VerifyTestTen();
            }

            /// <summary>
            /// This test verifies that all rows in the result contain the searched keyword.
            /// </summary>
            /// <remarks>
            /// Test Execution Order: 11  
            /// Preconditions:  
            /// - The user must have valid credentials.  
            /// - The Radiology page should be accessible.  
            /// Steps:  
            /// 1. Navigate to the Radiology page.  
            /// 2. Log in with valid credentials.  
            /// 3. Perform a search using a specific keyword.  
            /// 4. Validate that all displayed rows contain the searched keyword.  
            /// Expected Result:  
            /// - The search results should only include rows where the keyword is present.  
            /// </remarks>
            [Test, Order(11)]
            public void VerifyRowsContainTheSearchedKeyword()
            {
                _radiologyPage.GoToPage();
                _radiologyPage.LoginWithValidCredentials();
                _radiologyPage.VerifyRowsContainTheSearchedKeyword();
                _verify.VerifyTestEleven();
            }

            /// <summary>
            /// Test to verify that a new order can be added from the Doctor module.
            /// </summary>
            /// <remarks>
            /// Preconditions:
            /// - User must be logged in with valid credentials.
            /// - The Radiology page must be accessible.
            /// 
            /// Test Steps:
            /// 1. Navigate to the Radiology page.
            /// 2. Login with valid credentials.
            /// 3. Add a new order from the Doctor module.
            /// 4. Verify that the order is successfully added.
            /// 
            /// Expected Result:
            /// - The new order should be created and verified successfully.
            /// </remarks>
            [Test, Order(12)]
            public void AddNewOrderFromDoctorModule() {
                _radiologyPage.GoToPage();
                _radiologyPage.LoginWithValidCredentials();
                _radiologyPage.AddNewOrderFromDoctorModule();
                _verify.VerifyTestTwelve();
            }

            /// <summary>
            /// This test case verifies the 'Film Type Required' validation message.
            /// </summary>
            /// <remarks>
            /// Test Execution Order: 12  
            /// Preconditions:  
            /// - The user must have valid credentials.  
            /// - The Radiology page should be accessible.  
            /// Steps:  
            /// 1. Navigate to the Radiology page.  
            /// 2. Log in with valid credentials.  
            /// 3. Attempt to proceed without selecting a film type.  
            /// 4. Verify that the required validation message is displayed.  
            /// Expected Result:  
            /// - The system should display a validation error indicating that the film type is required.  
            /// </remarks>
            [Test, Order(13)]
            public void VerifyFilmTypeRequiredValidation()
            {
                _radiologyPage.GoToPage();
                _radiologyPage.LoginWithValidCredentials();
                _radiologyPage.VerifyFilmTypeRequiredValidation();
                _verify.VerifyTestThirteen();
            }

            /// <summary>
            /// This test case verifies that updating a scan displays a success message.
            /// </summary>
            /// <remarks>
            /// Test Execution Order: 13  
            /// Preconditions:  
            /// - The user must have valid credentials.  
            /// - The Radiology page should be accessible.  
            /// - The scan to be updated should exist.  
            /// Steps:  
            /// 1. Navigate to the Radiology page.  
            /// 2. Log in with valid credentials.  
            /// 3. Update an existing scan.  
            /// 4. Verify that a success message is displayed.  
            /// Expected Result:  
            /// - A success message should appear after updating the scan.  
            /// </remarks>
            [Test, Order(14)]
            public void UpdateScanAndVerifySuccessMessagea()
            {
                _radiologyPage.GoToPage();
                _radiologyPage.LoginWithValidCredentials();
                _radiologyPage.UpdateScanAndVerifySuccessMessagea();
                _verify.VerifyTestFourteen();
            }

            /// <summary>
            /// This test case verifies the confirmation alert when closing the "Add Report" modal without entering any data.
            /// </summary>
            /// <remarks>
            /// Test Execution Order: 14  
            /// Preconditions:  
            /// - The user must have valid credentials.  
            /// - The Radiology page should be accessible.  
            /// - The "Add Report" modal should be available.  
            /// Steps:  
            /// 1. Navigate to the Radiology page.  
            /// 2. Log in with valid credentials.  
            /// 3. Open the "Add Report" modal.  
            /// 4. Attempt to close the modal without making any entry.  
            /// 5. Verify the confirmation alert is displayed.  
            /// Expected Result:  
            /// - A confirmation alert should appear when closing the modal without entering data.  
            /// </remarks>
            [Test, Order(15)]
            public void VerifyConfirmationAlertOnClosingAddReportModalWithoutEntry()
            {
                _radiologyPage.GoToPage();
                _radiologyPage.LoginWithValidCredentials();
                _radiologyPage.VerifyConfirmationAlertOnClosingAddReportModalWithoutEntry();
                _verify.VerifyTestFifteen();
            }

            /// <summary>
            /// This test case logs into the Radiology page, adds a report, and verifies the addition.
            /// </summary>
            /// <remarks>
            /// Test Execution Order: 15  
            /// Preconditions:  
            /// - The user must have valid credentials.  
            /// - The Radiology page should be accessible.  
            /// Steps:  
            /// 1. Navigate to the Radiology page.  
            /// 2. Log in with valid credentials.  
            /// 3. Add a new report.  
            /// 4. Verify the report has been successfully added.  
            /// Expected Result:  
            /// - The report is added successfully and appears in the Radiology system.  
            /// </remarks>
            [Test, Order(16)]
            public void AddReportAndVerify()
            {
                _radiologyPage.GoToPage();
                _radiologyPage.LoginWithValidCredentials();
                _radiologyPage.AddReportAndVerify();
                _verify.VerifyTestSixteen();
            }
        }
    }
}
