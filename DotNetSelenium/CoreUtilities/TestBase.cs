using System;
using System.Collections.Generic;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
//using WebDriverManager;
//using WebDriverManager.DriverConfigs.Impl;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace CoreUtilities
{
    public class TestBase
    {
        public IWebDriver driver;

        public void Initialize(Dictionary<string, string> config)
        {
            try
            {
                string browser = config["browser"];
                Console.WriteLine("Borwser name --> " + browser);

                if (browser.Equals("Chrome", StringComparison.OrdinalIgnoreCase))
                {
                    //new DriverManager().SetUpDriver(new ChromeConfig());
                    driver = new ChromeDriver();
                }
                else
                {
                    throw new Exception("Not a valid browser. Select a valid browser like Chrome.");
                }

                driver.Manage().Window.Maximize();
                driver.Manage().Cookies.DeleteAllCookies();
                driver.Navigate().GoToUrl(config["url"]);
                driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(Convert.ToInt32(config["implicitwaittime"]));
            }
            catch (Exception ex)
            {
                throw new Exception($"Error initializing the browser: {ex.Message}");
            }
        }
    }
}
