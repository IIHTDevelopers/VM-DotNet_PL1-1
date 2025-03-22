using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AventStack.ExtentReports.Reporter;
using AventStack.ExtentReports;

namespace DotNetSelenium.Utilities
{
    public class ExtentReportManager
    {
        private static ExtentReports extent;
        private static string reportFileName = "ExtentReports-Test-Report.html";
        private static string windowsPath = Directory.GetCurrentDirectory() + "\\TestReport";
        private static string macPath = Directory.GetCurrentDirectory() + "/TestReport";
        private static string winReportFileLoc = windowsPath + "\\" + reportFileName;
        private static string macReportFileLoc = macPath + "/" + reportFileName;
        public static ExtentTest extentTest;

        public static ExtentReports GetInstance()
        {
            if (extent == null)
            {
                CreateInstance();
            }
            return extent;
        }

        public static ExtentReports CreateInstance()
        {
            string fileName = GetReportFileLocation();
            var sparkReporter = new ExtentSparkReporter(fileName);
            sparkReporter.Config.DocumentTitle = "Automation Test Report";
            sparkReporter.Config.ReportName = "Test Execution Report";

            extent = new ExtentReports();
            extent.AttachReporter(sparkReporter);

            return extent;
        }

        private static string GetReportFileLocation()
        {
            string reportFileLocation;
            if (Environment.OSVersion.Platform == PlatformID.Win32NT)
            {
                reportFileLocation = winReportFileLoc;
                CreateReportPath(windowsPath);
                Console.WriteLine("ExtentReport Path for WINDOWS: " + windowsPath);
            }
            else
            {
                reportFileLocation = macReportFileLoc;
                CreateReportPath(macPath);
                Console.WriteLine("ExtentReport Path for MAC/Linux: " + macPath);
            }
            return reportFileLocation;
        }

        private static void CreateReportPath(string path)
        {
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
                Console.WriteLine("Directory created: " + path);
            }
            else
            {
                Console.WriteLine("Directory already exists: " + path);
            }
        }
    }
}
