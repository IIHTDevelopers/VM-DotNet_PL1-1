using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNetSelenium.Utilities
{
    public class TestCaseResultDto
    {
        public string MethodName { get; set; }
        public string MethodType { get; set; }
        public int ActualScore { get; set; }
        public int EarnedScore { get; set; }
        public string Status { get; set; }
        public bool IsMandatory { get; set; }
        public string ErrorMessage { get; set; }

        public TestCaseResultDto(string methodName, string methodType, int actualScore, int earnedScore, string status,
            bool isMandatory, string errorMessage)
        {
            MethodName = methodName;
            MethodType = methodType;
            ActualScore = actualScore;
            EarnedScore = earnedScore;
            Status = status;
            IsMandatory = isMandatory;
            ErrorMessage = errorMessage;
        }
    }
}

