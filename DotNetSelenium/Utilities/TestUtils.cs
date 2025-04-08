using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace DotNetSelenium.Utilities
{
    public static class TestUtils
    {
        public const string TEXT_RESET = "\u001b[0m";
        public const string RED_BOLD_BRIGHT = "\u001b[1;91m";
        public const string GREEN_BOLD_BRIGHT = "\u001b[1;92m";
        public const string YELLOW_BOLD_BRIGHT = "\u001b[1;93m";
        public const string BLUE_BOLD_BRIGHT = "\u001b[1;94m";

        public static string TestResult { get; set; }
        public static int Total { get; private set; } = 0;
        public static int Passed { get; private set; } = 0;
        public static int Failed { get; private set; } = 0;

        public static FileInfo BusinessTestFile = new FileInfo("./output_revised.txt");
        public static FileInfo BoundaryTestFile = new FileInfo("./output_boundary_revised.txt");
        public static FileInfo ExceptionTestFile = new FileInfo("./output_exception_revised.txt");

        public static readonly string GUID = "6ed39465-d6d3-4ec4-b27d-1dcb870b2992";
        public static string CustomData;
        private static readonly string API_URL = "https://compiler.techademy.com/v1/mfa-results/push";
        static TestUtils()
        {
            BusinessTestFile.Delete();
            BoundaryTestFile.Delete();
            ExceptionTestFile.Delete();
        }

        private static string ReadData(string filePath)
        {
            try
            {
                return File.ReadAllText(filePath, Encoding.UTF8);
            }
            catch (IOException e)
            {
                Console.WriteLine($"Error reading file: {e.Message}");
                return string.Empty;
            }
        }

        public static async Task YakshaAssert(string testName, object result, FileInfo file)
        {
            var testResults = new TestResults();
            //var testCaseResults = new Dictionary<string, TestCaseResultDto>();
            Dictionary<string, TestCaseResultDto> testCaseResults = new Dictionary<string, TestCaseResultDto>();

            CustomData = ReadData("../../../../../custom.ih");
            string resultStatus = result.ToString().Equals("true", StringComparison.OrdinalIgnoreCase) ? "Passed" : "Failed";
            int resultScore = resultStatus == "Passed" ? 1 : 0;


            try
            {
                string testType = "functional";
                if (file.Name.Contains("boundary")) testType = "boundary";
                if (file.Name.Contains("exception")) testType = "exception";

                testCaseResults[GUID] = new TestCaseResultDto(testName, testType, 1, resultScore, resultStatus, true, "");
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error processing test case: {e.Message}");
            }

            testResults.TestCaseResults = testCaseResults;
            testResults.CustomData = CustomData;

            await SendTestResultsToAPI(testResults);
            Console.WriteLine("Completed API method");

            Total++;
            Console.Write($"\n{BLUE_BOLD_BRIGHT}=> Test For : {testName} : ");

            if (resultStatus == "Passed")
            {
                Console.WriteLine($"{GREEN_BOLD_BRIGHT}PASSED{TEXT_RESET}");
                Passed++;
            }
            else
            {
                Console.WriteLine($"{RED_BOLD_BRIGHT}FAILED{TEXT_RESET}");
                Failed++;
            }
        }

        private static async Task SendTestResultsToAPI(TestResults testResults)
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    // 🚀 Ensure TestCaseResults is NOT a string
                    if (testResults.TestCaseResults is string)
                    {
                        Console.WriteLine("❌ ERROR: TestCaseResults is still a string! Fixing it...");
                    }

                    // Debug print for TestCaseResults object
                    //Console.WriteLine("Raw TestCaseResults Object:");
                    //Console.WriteLine(Newtonsoft.Json.JsonConvert.SerializeObject(testResults.TestCaseResults, Formatting.Indented));

                    // 🚀 Ensure proper JSON serialization
                    string jsonData = Newtonsoft.Json.JsonConvert.SerializeObject(testResults, Formatting.Indented);
                    //Console.WriteLine("Serialized JSON Data:");
                    //Console.WriteLine(jsonData);

                    var content = new StringContent(jsonData, Encoding.UTF8, "application/json");

                    //Console.WriteLine("Sending HTTP POST request to: " + API_URL);

                    HttpResponseMessage response = await client.PostAsync(API_URL, content);

                    string responseContent = await response.Content.ReadAsStringAsync(); // Read response

                    if (response.IsSuccessStatusCode)
                    {
                        Console.WriteLine("✅ Test results sent successfully!");
                        Console.WriteLine($"Response: {responseContent}");
                    }
                    else
                    {
                        Console.WriteLine($"❌ Failed to send test results. Status: {response.StatusCode}");
                        Console.WriteLine($"Response Content: {responseContent}");
                    }

                    // 🚀 Debug API response
                    Console.WriteLine("Response Status Code: " + response.StatusCode);
                    Console.WriteLine("Response Headers: " + response.Headers);

                    // Read and print the response content
                    string responseBody = await response.Content.ReadAsStringAsync();
                    Console.WriteLine("Response Body: " + responseBody);

                    if (response.IsSuccessStatusCode)
                    {
                        Console.WriteLine("✅ Test results sent successfully!");
                    }
                    else
                    {
                        Console.WriteLine($"❌ Failed to send test results. Status: {response.StatusCode}");
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine($"❌ Error sending test results: {e.Message}");
                    Console.WriteLine($"StackTrace: {e.StackTrace}");
                }
            }
        }



        public static void TestReport()
        {
            Console.WriteLine($"\n{BLUE_BOLD_BRIGHT}TEST CASES EVALUATED : {Total}{TEXT_RESET}");
            Console.WriteLine($"{GREEN_BOLD_BRIGHT}PASSED : {Passed}{TEXT_RESET}");
            Console.WriteLine($"{RED_BOLD_BRIGHT}FAILED : {Failed}{TEXT_RESET}");
        }

        public static string CurrentTest()
        {
            return new System.Diagnostics.StackTrace().GetFrame(1).GetMethod().Name;
        }

        public static string AsJsonString(object obj)
        {
            try
            {
                return JsonConvert.SerializeObject(obj, Formatting.Indented);
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error converting to JSON: {e.Message}");
                return string.Empty;
            }
        }
    }

    
}
