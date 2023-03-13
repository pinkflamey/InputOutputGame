using System.Collections.Generic;

namespace pkpy
{
    internal static class FFI
    {
        static Dictionary<string, System.Delegate> mappings = new Dictionary<string, System.Delegate>();

        static object parse(string obj)
        {
            if (obj == "true") return true;
            if (obj == "false") return false;
            if (long.TryParse(obj, out long r1)) return r1;
            if (double.TryParse(obj, out double r2)) return r2;
            if (obj.Length >= 2 && obj[0] == '"' && obj[obj.Length - 1] == '"')
            {
                obj = obj.Substring(1, obj.Length - 2);
                return System.Text.RegularExpressions.Regex.Unescape(obj);
            }
            throw new System.Exception("Cannot parse string: " + obj);
        }

        static object invoke_f_any(string s)
        {
            var parts = s.Split(' ');
            List<object> args = new List<object>();
            for (int i = 1; i < parts.Length; i++) args.Add(parse(parts[i]));
            var f = mappings[parts[0]];
            return f.DynamicInvoke(args.ToArray());
        }

        public delegate void f_None_t(string _);
        [AOT.MonoPInvokeCallback(typeof(f_None_t))]
        public static void invoke_f_None(string s) => invoke_f_any(s);

        public delegate long f_int_t(string _);
        [AOT.MonoPInvokeCallback(typeof(f_int_t))]
        public static long invoke_f_int(string s) => (long)invoke_f_any(s);

        public delegate double f_float_t(string _);
        [AOT.MonoPInvokeCallback(typeof(f_float_t))]
        public static double invoke_f_float(string s) => (double)invoke_f_any(s);

        public delegate bool f_bool_t(string _);
        [AOT.MonoPInvokeCallback(typeof(f_bool_t))]
        public static bool invoke_f_bool(string s) => (bool)invoke_f_any(s);

        public delegate string f_str_t(string _);
        [AOT.MonoPInvokeCallback(typeof(f_str_t))]
        public static string invoke_f_str(string s) => (string)invoke_f_any(s);

        public static void register(string key, System.Delegate value)
        {
            if (key == null) throw new System.ArgumentNullException("key");
            if (value == null) throw new System.ArgumentNullException("value");
            mappings.Add(key, value);
        }

        public static int t_code<T>()
        {
            if (typeof(T) == typeof(long)) return 'i';
            if (typeof(T) == typeof(double)) return 'f';
            if (typeof(T) == typeof(bool)) return 'b';
            if (typeof(T) == typeof(string)) return 's';
            throw new System.Exception("Type must be long/double/bool/string");
        }
    }
}