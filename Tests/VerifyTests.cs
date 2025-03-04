using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using NUnit.Framework;
using OpenQA.Selenium;

namespace YakshaCSharpPL1.Tests
{
    internal class VerifyTests
    {
        private IWebDriver? _driver;
        public VerifyTests(IWebDriver driver)
        {
            _driver = driver;
        }

        String FilePath = "D:/Projects/C# Projects/1-YakshaCSharpPL1/PageObjects/RadiologyPage.cs";

        public IWebDriver Driver { get; }

        public void VerifyTestOne()
        {
            //String FilePath = "C:/Users/testu/Desktop/Office Projects/Yaksha/CSharp/YakshaCSharpPL1/YakshaCSharpPL1/PageObjects/RadiologyPage.cs";
            //String filePath = "D:/Projects/C# Projects/1-YakshaCSharpPL1/PageObjects/RadiologyPage.cs";

            // Get the base directory of the project dynamically
            //string basePath = AppDomain.CurrentDomain.BaseDirectory;
            //Console.WriteLine("Base path >> " + basePath);

            //// Navigate to the required file (relative to the project directory)
            //string filePath = Path.Combine(basePath, "PageObjects", "RadiologyPage.cs");

            String MethodName = "VerifyRadiologyTabIsDisplayed";
            List<string> Keywords = new List<string> { "Displayed" };
            Assert.That(CheckMethodContainsList(FilePath, MethodName, Keywords), Is.True, "Test 2 failed");
        }

        public void VerifyTestTwo() {
            //String FilePath = "C:/Users/testu/Desktop/Office Projects/Yaksha/CSharp/YakshaCSharpPL1/YakshaCSharpPL1/PageObjects/RadiologyPage.cs";
            String MethodName = "VerifyRadiologySubModules";
            List<string> Keywords = new List<string> { "Displayed"};
            Assert.That(CheckMethodContainsList(FilePath, MethodName, Keywords), Is.True, "Test 2 failed");
        }

        public void VerifyTestThree()
        {
            Assert.That(_driver.Url, Is.EqualTo("https://healthapp.yaksha.com/Home/Index#/Radiology/ImagingRequisitionList"), "Test 3 failed");
        }

        public void VerifyTestFour() {
            //String FilePath = "C:/Users/testu/Desktop/Office Projects/Yaksha/CSharp/YakshaCSharpPL1/YakshaCSharpPL1/PageObjects/RadiologyPage.cs";
            String MethodName = "VerifyListRequestsComponentsAreVisible";
            List<string> Keywords = new List<string> {
                "ListRequestsTitle.Displayed",
                "OkBtn.Displayed",
                "FirstBtn.Displayed",
                "PreviousBtn.Displayed",
                "PreviousBtn.Displayed",
                "NextBtn.Displayed",
                "LastBtn.Displayed",
                "HeaderRequestedOn.Displayed",
                "HeaderHospitalNumber.Displayed",
                "HeaderPatientName.Displayed",
                "HeaderAge.Displayed",
                "HeaderPrescriber.Displayed",
                "HeaderType.Displayed",
                "HeaderImagingName.Displayed",
                "HeaderInsurance.Displayed",
                "HeaderAction.Displayed",
                "FromDateCalendarInput.Displayed",
                "ToDateCalendarInput.Displayed",
                "SearchField.Displayed",
                "ResultCountIndicator.Displayed",
                "StarIcon.Displayed",
                "PreDefinedDateRangeDropdown.Displayed",
                "FilterDropdown.Displayed",
                "FromRowCount.Displayed",
                "ToRowCount.Displayed",
                "TotalRowCount.Displayed",
                "CurrentPageCount.Displayed",
                "TotalPageCount.Displayed"
            };
            Assert.That(CheckMethodContainsList(FilePath, MethodName, Keywords), Is.True, "Test 4 failed");
        }

        public void VerifyTestFive() {
            //String FilePath = "C:/Users/testu/Desktop/Office Projects/Yaksha/CSharp/YakshaCSharpPL1/YakshaCSharpPL1/PageObjects/RadiologyPage.cs";
            String MethodName = "NavigateToSubModulesAndVerifyUrl";
            List<string> Keywords = new List<string> {
                "ListReportsBtn.Click",
                "EditDoctorsBtn.Click",
                "WardBillingBtn.Click",
                "ListRequestsBtn.Click",
                "Radiology/ImagingReportsList",
                "Radiology/EditDoctors",
                "Radiology/InpatientList",
                "Radiology/ImagingRequisitionList",
                "Url"
            };
            Assert.That(CheckMethodContainsList(FilePath, MethodName, Keywords), Is.True, "Test 5 failed");
        }

        public void VerifyTestSix() {
            //String FilePath = "C:/Users/testu/Desktop/Office Projects/Yaksha/CSharp/YakshaCSharpPL1/YakshaCSharpPL1/PageObjects/RadiologyPage.cs";
            String MethodName = "VerifyTooltipTextOfStarIcon";
            List<string> Keywords = new List<string> {
                "StarIcon.GetAttribute"
            };
            Assert.That(CheckMethodContainsList(FilePath, MethodName, Keywords), Is.True, "Test 6 failed");
        }

        public void VerifyTestSeven() {
            //String FilePath = "C:/Users/testu/Desktop/Office Projects/Yaksha/CSharp/YakshaCSharpPL1/YakshaCSharpPL1/PageObjects/RadiologyPage.cs";
            String MethodName = "VerifyAppliedDateRangeRemainsAfterEnablingStarIcon";
            List<string> Keywords = new List<string> {
                "StarIcon.Click",
                "FromDateCalendarInput.GetAttribute",
                "ToDateCalendarInput.GetAttribute"
            };
            Assert.That(CheckMethodContainsList(FilePath, MethodName, Keywords), Is.True, "Test 7 failed");
        }

        public void VerifyUserIsOnCorrectURL(String expectedURL)
        {
            string actualURL = _driver.Url;
            Assert.That(actualURL.Contains(expectedURL), Is.True);
        }

        public void VerifyTestNine() {
            //String FilePath = "C:/Users/testu/Desktop/Office Projects/Yaksha/CSharp/YakshaCSharpPL1/YakshaCSharpPL1/PageObjects/RadiologyPage.cs";
            String MethodName = "ApplyAndVerifyRandomFilter";
            List<string> Keywords = new List<string> {
                "select",
                "select.",
                "OkBtn.Click",
                "Text"
            };
            Assert.That(CheckMethodContainsList(FilePath, MethodName, Keywords), Is.True, "Test 9 failed");
        }

        public void VerifyTestTen() {
            //String FilePath = "C:/Users/testu/Desktop/Office Projects/Yaksha/CSharp/YakshaCSharpPL1/YakshaCSharpPL1/PageObjects/RadiologyPage.cs";
            String MethodName = "VerifyValidationOnCancellationWithoutRemarks";
            List<string> Keywords = new List<string> {
                "ViewButton.Click",
                "CancelButton.Click",
                "WardBillingBtn.Click",
            };
            Assert.That(CheckMethodContainsList(FilePath, MethodName, Keywords), Is.True, "Test 10 failed");
        }

        public void VerifyTestEleven() {
            //String FilePath = "C:/Users/testu/Desktop/Office Projects/Yaksha/CSharp/YakshaCSharpPL1/YakshaCSharpPL1/PageObjects/RadiologyPage.cs";
            String MethodName = "VerifyRowsContainTheSearchedKeyword";
            List<string> Keywords = new List<string> {
                "WardBillingBtn.Click",
                "SearchField.SendKeys",
                "Text",
            };
            Assert.That(CheckMethodContainsList(FilePath, MethodName, Keywords), Is.True, "Test 11 failed");
        }

        public void VerifyTestThirteen() {
            //String FilePath = "C:/Users/testu/Desktop/Office Projects/Yaksha/CSharp/YakshaCSharpPL1/YakshaCSharpPL1/PageObjects/RadiologyPage.cs";
            String MethodName = "VerifyFilmTypeRequiredValidation";
            List<string> Keywords = new List<string> {
                "GetAttribute"
            };
            Assert.That(CheckMethodContainsList(FilePath, MethodName, Keywords), Is.True, "Test 13 failed");
        }

        public void VerifyTestFourteen() {
            //String FilePath = "C:/Users/testu/Desktop/Office Projects/Yaksha/CSharp/YakshaCSharpPL1/YakshaCSharpPL1/PageObjects/RadiologyPage.cs";
            String MethodName = "UpdateScanAndVerifySuccessMessagea";
            List<string> Keywords = new List<string> {
                "Click",
                "SendKeys"
            };
            Assert.That(CheckMethodContainsList(FilePath, MethodName, Keywords), Is.True, "Test 14 failed");
        }

        public void VerifyTestFifteen() {
            //String FilePath = "C:/Users/testu/Desktop/Office Projects/Yaksha/CSharp/YakshaCSharpPL1/YakshaCSharpPL1/PageObjects/RadiologyPage.cs";
            String MethodName = "VerifyConfirmationAlertOnClosingAddReportModalWithoutEntry";
            List<string> Keywords = new List<string> {
                "SwitchTo",
                "Alert",
                "alert.Text"
            };
            Assert.That(CheckMethodContainsList(FilePath, MethodName, Keywords), Is.True, "Test 15 failed");
        }

        public void VerifyTestSixteen() {
            //String FilePath = "C:/Users/testu/Desktop/Office Projects/Yaksha/CSharp/YakshaCSharpPL1/YakshaCSharpPL1/PageObjects/RadiologyPage.cs";
            String MethodName = "AddReportAndVerify";
            List<string> Keywords = new List<string> {
                "Click",
                ".Text"
            };
            Assert.That(CheckMethodContainsList(FilePath, MethodName, Keywords), Is.True, "Test 16 failed");
        }

        public void VerifyTestTwelve() {
            Assert.That(_driver.FindElement(By.XPath("//p[contains(@class,'main-message')]")).Text, Is.EqualTo("Imaging and lab order add successfully"), "Test 12 failed.");
        }

        static bool CheckMethodContainsList(string filePath, string methodName, List<string> list)
        {
            if (!File.Exists(filePath))
            {
                Console.WriteLine("File not found.");
                return false;
            }

            string fileContent = File.ReadAllText(filePath);
            string pattern = $@"{methodName}\s*\(.*?\)\s*{{(.*?)}}";
            Match match = Regex.Match(fileContent, pattern, RegexOptions.Singleline);

            if (!match.Success)
            {
                Console.WriteLine("Method not found.");
                return false;
            }

            string methodBody = match.Groups[1].Value;
            foreach (string item in list)
            {
                if (!methodBody.Contains(item))
                {
                    return false;
                }
            }
            return true;
        }
    }
}
