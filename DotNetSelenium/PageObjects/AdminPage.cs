using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using NUnit.Framework;

namespace DotNetSelenium.PageObjects
{
    public class AdminPage
    {
        private readonly IWebDriver driver;
        private readonly WebDriverWait wait;

        public AdminPage(IWebDriver driver)
        {
            this.driver = driver;
            this.wait = new WebDriverWait(driver, TimeSpan.FromSeconds(20));
        }

        private IWebElement AdminDropdown => wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//li[@class='dropdown dropdown-user']")));
        private IWebElement MyProfileOption => driver.FindElement(By.CssSelector("a[routerlink='Employee/ProfileMain']"));
        private IWebElement UserProfileHeader => driver.FindElement(By.CssSelector("a[routerlink='UserProfile']"));

        /**
        *@Test7
        * Navigates to the "User Profile" page via the Admin dropdown and verifies successful navigation.
        *
        * Steps:
        * 1. Clicks on the Admin dropdown in the header.
        * 2. Selects the "My Profile" option from the dropdown menu.
        * 3. Waits for the User Profile page to load.
        * 4. Retrieves and returns the header text of the User Profile page for validation.
        *
        * @returns The header text from the User Profile page, used to assert successful navigation.
        */
        public String VerifyUserProfileNavigation()
        {
            // Click on Admin dropdown
            AdminDropdown.Click();

            // Select "My Profile" option
            wait.Until(ExpectedConditions.ElementToBeClickable(MyProfileOption)).Click();

            // Wait for User Profile page to load
            wait.Until(ExpectedConditions.ElementIsVisible(By.CssSelector("a[routerlink='UserProfile']")));

            // Verify that the User Profile page is displayed
            string headerText = UserProfileHeader.Text.Trim();
            return headerText;
        }
    }

}
