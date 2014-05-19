using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;

namespace CaptureTheFlag
{
    public static class DebugLogger
    {
        [Conditional("DEBUG")]
        public static void WriteLine(Type aClass, MethodBase aMethod)
        {
            WriteLine(aClass, aMethod, "");
        }

        [Conditional("DEBUG")]
        public static void WriteLine(Type aClass, MethodBase aMethod, string format, params object[] args)
        {
            WriteLine(aClass, aMethod, String.Format(format, args));
        }

        [Conditional("DEBUG")]
        public static void WriteLine(Type aClass, MethodBase aMethod, string message)
        {
            List<String> parameters = new List<String>();
            foreach (ParameterInfo parameterInfo in aMethod.GetParameters())
            {
                parameters.Add(String.Format("{0} {1}", parameterInfo.ParameterType.Name, parameterInfo.Name));
            }
            String paramsString = String.Join<String>(", ", parameters);
            Debug.WriteLine("{0}.{1}({2}){3}", aClass.Name, aMethod.Name, paramsString, String.IsNullOrEmpty(message) ? message : String.Format(" : {0}", message));
        }

        [Conditional("DEBUG")]
        public static void WriteLine(string message)
        {
            Debug.WriteLine(message);
        }

        //[Conditional("DEBUG")]
        //public static void WriteLine(string format, params object[] args)
        //{
        //    Debug.WriteLine(format, args);
        //}

        [Conditional("DEBUG")]
        public static void WriteLineIf(bool condition, string message)
        {
            Debug.WriteLineIf(condition, message);
        }
    }
}
