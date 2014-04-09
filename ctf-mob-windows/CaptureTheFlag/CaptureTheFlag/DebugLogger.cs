using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;

namespace CaptureTheFlag
{
    public static class DebugLogger
    {
        public static void WriteLine(String message, Type aClass, MethodBase aMethod)
        {
            List<String> parameters = new List<String>();
            foreach(ParameterInfo parameterInfo in aMethod.GetParameters())
            {
                parameters.Add(String.Format("{0} {1}", parameterInfo.ParameterType.Name, parameterInfo.Name));
            }
            String paramsString = String.Join<String>(", ", parameters);
            Debug.WriteLine("{0}.{1}({2}){3}", aClass.Name, aMethod.Name, paramsString, String.IsNullOrEmpty(message) ? message : String.Format(" : {0}", message));
        }
    }
}
