﻿using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Ketchup.Core.Route.Template
{
    public class RoutePatternParser
    {
        public static string Parse(string routeTemplet, string service, string method)
        {
            StringBuilder result = new StringBuilder();
            var parameters = routeTemplet.Split(@"/");
            foreach (var parameter in parameters)
            {
                var param = GetParameters(parameter).FirstOrDefault();
                if (param == null)
                {
                    result.Append(parameter);
                }
                else if (service.EndsWith(param))
                {
                    result.Append(service.Substring(1, service.Length - param.Length - 1));
                }
                else if (param == "Method")
                {
                    result.Append(method);
                }
                result.Append("/");
            }

            return result.Append(method).ToString().ToLower();
        }

        public static string Parse(string routeTemplet, string service)
        {
            StringBuilder result = new StringBuilder();
            var parameters = routeTemplet.Split(@"/");
            foreach (var parameter in parameters)
            {
                var param = GetParameters(parameter).FirstOrDefault();
                if (param == null)
                {
                    result.Append(parameter);
                }
                else if (service.EndsWith(param))
                {
                    result.Append(service.Substring(1, service.Length - param.Length - 1));
                }
                result.Append("/");
            }

            return result.ToString().TrimEnd('/').ToLower();
        }

        private static List<string> GetParameters(string text)
        {
            var matchVale = new List<string>();
            string Reg = @"(?<={)[^{}]*(?=})";
            string key = string.Empty;
            foreach (Match m in Regex.Matches(text, Reg))
            {
                matchVale.Add(m.Value);
            }
            return matchVale;
        }
    }
}
