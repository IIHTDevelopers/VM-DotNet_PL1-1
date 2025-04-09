using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System;
using System.IO;

namespace DotNetSelenium.PageObjects
{
    public class PatientPage
    {
        private readonly IWebDriver driver;
        private readonly WebDriverWait wait;

        public PatientPage(IWebDriver driver)
        {
            this.driver = driver;
            this.wait = new WebDriverWait(driver, TimeSpan.FromSeconds(20));
        }

        private IWebElement PatientLink => driver.FindElement(By.CssSelector("a[href='#/Patient']"));
        private IWebElement RegisterPatient => driver.FindElement(By.CssSelector("ul.page-breadcrumb a[href='#/Patient/RegisterPatient']"));
        private IWebElement ProfilePictureIcon => driver.FindElement(By.CssSelector("a[title='Profile Picture']"));
        private IWebElement NewPhotoButton => driver.FindElement(By.XPath("//button[contains(text(),'New Photo')]"));
        private IWebElement UploadButton => driver.FindElement(By.CssSelector("label[for='fileFromLocalDisk']"));
        private IWebElement DoneButton => driver.FindElement(By.XPath("//button[text()='Done']"));
        private IWebElement UploadedImg => driver.FindElement(By.CssSelector("div.wrapper img"));

/**
 * @Test8
 * @description : This method uploads a profile picture for a patient by navigating to the "Register Patient" tab
 *              and completing the image upload workflow.
 * @expected : The uploaded profile image should be visible on the patient registration screen, confirming success.
 *
 * Steps:
 * 1. Click on the "Patient" link from the navigation menu.
 * 2. Select the "Register Patient" tab to open the registration section.
 * 3. Click on the profile picture icon followed by the "New Photo" button.
 * 4. Upload a test image from the predefined directory.
 * 5. Wait for the image to upload and click the "Done" button to finalize.
 * 6. Verify that the uploaded image is displayed on the UI.
 *
 * @returns : True if the image is displayed after upload; otherwise, throws an exception.
 */
        public bool UploadProfilePicture()
        {
            string imagePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "TestImage", "UploadImage.png");

            // Click on "Patient" link
            wait.Until(ExpectedConditions.ElementToBeClickable(PatientLink)).Click();

            // Click on "Register Patient" tab
            wait.Until(ExpectedConditions.ElementToBeClickable(RegisterPatient)).Click();

            // Click on "Profile Picture" icon
            wait.Until(ExpectedConditions.ElementToBeClickable(ProfilePictureIcon)).Click();

            // Click on "New Photo" button
            wait.Until(ExpectedConditions.ElementToBeClickable(NewPhotoButton)).Click();

            // Upload image
            IWebElement fileInput = driver.FindElement(By.CssSelector("input[type='file']"));
            fileInput.SendKeys(imagePath);

            // Wait for upload to complete
            System.Threading.Thread.Sleep(2000);

            // Click on "Done" button
            wait.Until(ExpectedConditions.ElementToBeClickable(DoneButton)).Click();

            // Verify success confirmation or image upload
            bool isImageDisplayed = wait.Until(ExpectedConditions.ElementIsVisible(By.CssSelector("div.wrapper img"))).Displayed;
            if (!isImageDisplayed)
            {
                throw new Exception("Profile picture upload failed!");
            }
            return isImageDisplayed;
        }
    }
}
