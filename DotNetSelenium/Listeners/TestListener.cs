using System;
using System.Diagnostics;
using System.IO;
using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;
using NUnit.Framework;
using NUnit.Framework.Interfaces;
using DotNetSelenium.Utilities;

namespace DotNetSelenium.Listeners
{
    public class TestListener
    {
        private static ExtentReports extent;
        private static ExtentTest test;
        private static string reportFilePath = Path.Combine(Directory.GetCurrentDirectory(), "ExtentReport.html");

        [OneTimeSetUp]
        public void Setup()
        {
            var htmlReporter = new ExtentSparkReporter(reportFilePath);
            extent = new ExtentReports();
            extent.AttachReporter(htmlReporter);
            Console.WriteLine("Extent Reports initialized!");
        }

        [SetUp]
        public void BeforeTest()
        {
            test = extent.CreateTest(TestContext.CurrentContext.Test.Name);
            Console.WriteLine(TestContext.CurrentContext.Test.Name + " started!");
        }

        [TearDown]
        public void AfterTest()
        {
            var result = TestContext.CurrentContext.Result.Outcome.Status;

            if (result == TestStatus.Passed)
            {
                test.Pass("Test passed");
                Console.WriteLine("Inside after test");
                Console.WriteLine(TestContext.CurrentContext.Test.Name + " passed!");
                TestUtils.YakshaAssert(TestContext.CurrentContext.Test.Name, true, TestUtils.BusinessTestFile);
            }
            else if (result == TestStatus.Failed)
            {
                test.Fail(TestContext.CurrentContext.Result.Message);
                Console.WriteLine(TestContext.CurrentContext.Test.Name + " failed!");
                TestUtils.YakshaAssert(TestContext.CurrentContext.Test.Name, false, TestUtils.BusinessTestFile);
            }
            else if (result == TestStatus.Skipped)
            {
                test.Skip("Test skipped");
                Console.WriteLine(TestContext.CurrentContext.Test.Name + " skipped!");
                TestUtils.YakshaAssert(TestContext.CurrentContext.Test.Name, false, TestUtils.BusinessTestFile);
            }
        }

        [OneTimeTearDown]
        public void TearDown()
        {
            extent.Flush();
            Console.WriteLine("Extent Reports closed!");

            // Automatically open the report in the default browser
            try
            {
                Process.Start(new ProcessStartInfo(reportFilePath) { UseShellExecute = true });
                Console.WriteLine("Extent Report opened successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error opening Extent Report: " + ex.Message);
            }
        }
    }
}
