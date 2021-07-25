using System;

namespace Demo.Microservice.App.Common.Util
{
    public static class AppUtils
    {
        public static string NormalizeExamYearName(string examYearName)
        {
            if (string.IsNullOrWhiteSpace(examYearName))
            {
                return string.Empty;
            }

            if (examYearName.Contains("AY", StringComparison.OrdinalIgnoreCase))
            {
                return examYearName.Replace("AY", string.Empty, StringComparison.OrdinalIgnoreCase).Trim();
            }
            else
            {
                return examYearName.Trim();
            }
        }
    }
}
