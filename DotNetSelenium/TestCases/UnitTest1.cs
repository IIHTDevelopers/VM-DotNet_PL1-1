using System;
using NUnit.Framework;
using DotNetSelenium.PageObjects;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using CoreUtilities;
using DotNetSelenium.Listeners;
using DotNetSelenium.Utilities;
using Newtonsoft.Json.Linq;

namespace DotNetSelenium.TestCases
{
    [TestFixture]
    public class Tests : TestListener
    {

        private IWebDriver? driver;
        //private TestBase testBase;
        private LoginPage? loginPage;
        private AppointmentPage? appointmentPage;
        private OperationTheatrePage operationTheatrePage;
        private DoctorPage doctorPage;
        private ProcurementPage procurementPage;
        private UtilitiesPage utilitiesPage;
        private AdminPage adminPage;
        private PatientPage patientPage;
        private IncentivePage incentivePage;
        private SettingsPage settingsPage;
        private SubstorePage substorePage;
        private ADTPage adtPage;

        [SetUp]
        public void Setup()
        {
            driver = new ChromeDriver();
            driver.Navigate().GoToUrl("https://healthapp.yaksha.com/");
            driver.Manage().Window.Maximize();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(30);

            // Initialize the pages
            loginPage = new LoginPage(driver);
            appointmentPage = new AppointmentPage(driver);
            operationTheatrePage = new OperationTheatrePage(driver);
            doctorPage = new DoctorPage(driver);
            procurementPage = new ProcurementPage(driver);
            utilitiesPage = new UtilitiesPage(driver);
            adminPage = new AdminPage(driver);
            patientPage = new PatientPage(driver);
            incentivePage = new IncentivePage(driver);
            settingsPage = new SettingsPage(driver);
            substorePage = new SubstorePage(driver);
            adtPage = new ADTPage(driver);

            // Perform login before every test
            loginPage.PerformLogin();
        }

        [Test]
        public void TestVerifyVisitTypeDropdown()
        {
            // Calling the method to verify visit type dropdown functionality
            bool isVisitType = appointmentPage.VerifyVisitTypeDropdown();
            Assert.That(isVisitType, Is.True);
            VerifyVisitType(driver);
        }

        [Test]
        public void Test_HandleOtBookingAlert()
        {
            String actualAlertMessage = operationTheatrePage.HandleOtBookingAlert();
            Assert.That(actualAlertMessage, Does.Contain("Patient not Selected! Please Select the patient first!"),
                "Unexpected alert message.");
            OTBookingModalIsDisplayed(driver);
        }

        [Test]
        public void Test_VerifyPatientOverview()
        {
            JObject testData = TestDataReader.LoadJson("PatientName.json");
            string patientName = testData["PatientNames"][0]["Patient1"].ToString();

            String actualPatientheaadingText = doctorPage.VerifyPatientOverview();
            Assert.That(actualPatientheaadingText, Is.EqualTo(patientName.ToLower()), "Patient name does not match.");
            VerifyUserIsOnCorrectURL(driver, "Doctors/PatientOverviewMain/PatientOverview");
        }

        [Test]
        public void Test_AddProgressNoteForPatient()
        {
            String actualSuccessMessage = doctorPage.AddProgressNoteForPatient();
            Assert.That(actualSuccessMessage, Does.Contain("Progress Note Template added."),
                    "Success message does not match.");
            VerifyUserIsOnCorrectURL(driver, "Doctors/PatientOverviewMain/NotesSummary/NotesList");
        }

        [Test]
        public void Test_AddCurrencyAndVerify()
        {
            bool isCurrencyAdded = procurementPage.AddCurrencyAndVerify();
            Assert.That(isCurrencyAdded, Is.True, "Currency was not added successfully.");
            VerifyUserIsOnCorrectURL(driver, "ProcurementMain/Settings/CurrencyList");
        }

        [Test]
        public void Test_VerifyMandatoryFieldsWarning()
        {
            String actualPopupMessage = utilitiesPage.VerifyMandatoryFieldsWarning();
            Assert.AreEqual(actualPopupMessage, "Please fill all the mandatory fields.");
            VerifyUserIsOnCorrectURL(driver, "/Utilities/SchemeRefund");
        }

        [Test]
        public void Test_VerifyUserProfileNavigation()
        {
            String actualHeaderText = adminPage.VerifyUserProfileNavigation();
            Assert.AreEqual("User Profile", actualHeaderText, "User Profile page did not load as expected.");
            VerifyUserIsOnCorrectURL(driver, "Employee/ProfileMain/UserProfile");
        }

        [Test]
        public void Test_UploadProfilePicture()
        {
            bool isImageUploaded = patientPage.UploadProfilePicture();
            Assert.That(isImageUploaded, Is.True, "Image was not uploaded successfully.");
            VerifyImageIsUploaded(driver);
        }

        [Test]
        public void Test_EditTDSForEmployee()
        {
            bool isTdsValueUpdated = incentivePage.EditTDSForEmployee();
            Assert.That(isTdsValueUpdated, Is.True, "TDS value was not updated successfully.");
            VerifyTdsTest(driver);
        }

        [Test]
        public void Test_TogglePriceCategoryStatus()
        {
            String toggleActivateMsg = settingsPage.TogglePriceCategoryStatus();
            Assert.AreEqual(toggleActivateMsg, "Activated.");
            VerifyUserIsOnCorrectURL(driver, "Settings/PriceCategory");
        }

        [Test]
        public void Test_VerifyNavigationBetweenSubmodules()
        {
            bool isUserNavigatedSuccessfully = substorePage.VerifyNavigationBetweenSubmodules();
            Assert.That(isUserNavigatedSuccessfully, Is.True, "User was not navigated successfully.");
            VerifyUserIsOnCorrectURL(driver, "Inventory/Return");
        }

        [Test]
        public void Test_VerifyTooltipText()
        {
            String tooltipText = substorePage.VerifyTooltipText();
            Assert.That(tooltipText, Does.Contain("To change, you can always click here."),
                    "Tooltip message does not match.");
            IsTooltipDisplayed(driver);
        }

        [Test]
        public void Test_CaptureInventoryRequisitionScreenshot()
        {
            substorePage.CaptureInventoryRequisitionScreenshot();
            VerifyUserIsOnCorrectURL(driver, "Inventory/InventoryRequisitionList");
        }

        [Test]
        public void Test_VerifyFieldLevelErrorMessage()
        {
            bool isErrorMessageDisplayed = adtPage.VerifyFieldLevelErrorMessage();
            Assert.That(isErrorMessageDisplayed, Is.True, "Error message was not dislpayed successfully.");
            VerifyErrorMessage(driver);
        }

        [Test]
        public void Test_VerifyLogoutFunctionality()
        {
            bool isLogoutMessageDisplayed = loginPage.VerifyLogoutFunctionality();
            Assert.That(isLogoutMessageDisplayed, Is.True, "Logout message was not dislpayed successfully.");
            VerifyUserIsLoggedOut(driver);
        }

        [TearDown]
        public void TearDown()
        {
            if (driver != null)
            {
                driver.Quit();
                driver.Dispose();
                driver = null;
            }
        }


        /**
 * ------------------------------------------------------Helper Methods----------------------------------------------------
 */

        public void VerifyUserIsLoggedIn(IWebDriver driver)
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(20));
            wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//li[@class='dropdown dropdown-user']")));
            Assert.IsTrue(driver.FindElement(By.XPath("//li[@class='dropdown dropdown-user']")).Displayed);
        }

        public void VerifyUserIsLoggedOut(IWebDriver driver)
        {
            Assert.IsTrue(driver.FindElement(By.Id("login")).Displayed);
        }

        public void VerifyVisitType(IWebDriver driver)
        {
            var tableElements = driver.FindElements(By.CssSelector("div[col-id='AppointmentType']"));
            Assert.Greater(tableElements.Count, 1);
        }

        public void VerifyUserIsOnCorrectURL(IWebDriver driver, string expectedURL)
        {
            string actualURL = driver.Url;
            Assert.IsTrue(actualURL.Contains(expectedURL));
        }

        public void VerifyImageIsUploaded(IWebDriver driver)
        {
            Assert.IsTrue(driver.FindElement(By.CssSelector("div.wrapper img")).Displayed);
        }

        public void IsTooltipDisplayed(IWebDriver driver)
        {
            Assert.IsTrue(driver.FindElement(By.CssSelector("div.modal-content")).Displayed);
        }

        public void VerifyErrorMessage(IWebDriver driver)
        {
            Assert.IsTrue(driver.FindElement(By.XPath("//span[text()='Select doctor from the list.']")).Displayed);
        }

        public void OTBookingModalIsDisplayed(IWebDriver driver)
        {
            Assert.IsTrue(driver.FindElement(By.CssSelector("div.modelbox-div")).Displayed);
        }

        public void VerifyIfRecordsArePresent(IWebDriver driver)
        {
            var records = driver.FindElements(By.CssSelector("div[col-id='PatientName']"));
            Assert.Greater(records.Count, 1);
        }

        public void VerifyTdsTest(IWebDriver driver)
        {
            var patientNames = driver.FindElements(By.CssSelector("div[col-id='FullName']"));
            Assert.IsTrue(patientNames[1].Text.Contains("Rakesh"));
        }


    }
}