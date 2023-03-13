using System;
using System.Runtime.InteropServices;
using UnityEngine;

namespace pkpy
{
    internal static class Bindings
    {
#if !UNITY_EDITOR && UNITY_IOS
        private const string _libName = "__Internal";
#else
        private const string _libName = "pocketpy";
#endif
        [DllImport(_libName)]
        internal static extern void pkpy_delete(IntPtr p);
        [DllImport(_libName)]
        internal static extern IntPtr pkpy_new_repl(IntPtr vm);
        [DllImport(_libName)]
        internal static extern bool pkpy_repl_input(IntPtr r, string line);
        [DllImport(_libName)]
        internal static extern IntPtr pkpy_new_vm(bool use_stdio);
        [DllImport(_libName)]
        internal static extern void pkpy_vm_add_module(IntPtr vm, string name, string source);
        [DllImport(_libName)]
        internal static extern string pkpy_vm_bind(IntPtr vm, string mod, string name, int ret_code);
        [DllImport(_libName)]
        internal static extern string pkpy_vm_eval(IntPtr vm, string source);
        [DllImport(_libName)]
        internal static extern void pkpy_vm_exec(IntPtr vm, string source);
        [DllImport(_libName)]
        internal static extern string pkpy_vm_get_global(IntPtr vm, string name);
        [DllImport(_libName)]
        internal static extern string pkpy_vm_read_output(IntPtr vm);

        [DllImport(_libName)]
        internal static extern void pkpy_setup_callbacks(FFI.f_int_t f_int, FFI.f_float_t f_float, FFI.f_bool_t f_bool, FFI.f_str_t f_str, FFI.f_None_t f_None);
    }
}

namespace pkpy
{
    public struct PyOutput
    {
        public string stdout;
        public string stderr;

        public PyOutput(string stdout, string stderr)
        {
            this.stdout = stdout;
            this.stderr = stderr;
        }
    }

    public class VM
    {
        public IntPtr pointer { get; private set; }
        private static bool firstNew = true;

        public VM()
        {
            pointer = Bindings.pkpy_new_vm(false);
            if (!firstNew) return;
            Bindings.pkpy_setup_callbacks(FFI.invoke_f_int, FFI.invoke_f_float, FFI.invoke_f_bool, FFI.invoke_f_str, FFI.invoke_f_None);
            firstNew = false;
        }

        public PyOutput read_output()
        {
            var _o = Bindings.pkpy_vm_read_output(pointer);
            return JsonUtility.FromJson<PyOutput>(_o);
        }

        public void dispose()
        {
            Bindings.pkpy_delete(pointer);
        }

        /// <summary>
        /// Add a source module into a virtual machine.
        /// </summary>
        public void add_module(string name, string source)
        {
            Bindings.pkpy_vm_add_module(pointer, name, source);
        }


        /// <summary>
        /// Evaluate an expression.  Return `__repr__` of the result. If there is any error, return `nullptr`.
        /// </summary>
        public string eval(string source)
        {
            return Bindings.pkpy_vm_eval(pointer, source);
        }

        /// <summary>
        /// Run a given source on a virtual machine.
        /// </summary>
        public void exec(string source)
        {
            Bindings.pkpy_vm_exec(pointer, source);
        }

        /// <summary>
        /// Get a global variable of a virtual machine.  Return `__repr__` of the result. If the variable is not found, return `nullptr`.
        /// </summary>
        public string get_global(string name)
        {
            return Bindings.pkpy_vm_get_global(pointer, name);
        }


        public void bind(string mod, string name, System.Action f)
        {
            FFI.register(Bindings.pkpy_vm_bind(pointer, mod, name, 'N'), f);
        }
        public void bind<T1>(string mod, string name, System.Action<T1> f)
        {
            FFI.register(Bindings.pkpy_vm_bind(pointer, mod, name, 'N'), f);
        }
        public void bind<T1, T2>(string mod, string name, System.Action<T1, T2> f)
        {
            FFI.register(Bindings.pkpy_vm_bind(pointer, mod, name, 'N'), f);
        }
        public void bind<T1, T2, T3>(string mod, string name, System.Action<T1, T2, T3> f)
        {
            FFI.register(Bindings.pkpy_vm_bind(pointer, mod, name, 'N'), f);
        }
        public void bind<T1, T2, T3, T4>(string mod, string name, System.Action<T1, T2, T3, T4> f)
        {
            FFI.register(Bindings.pkpy_vm_bind(pointer, mod, name, 'N'), f);
        }
        public void bind<TResult>(string mod, string name, System.Func<TResult> f)
        {
            FFI.register(Bindings.pkpy_vm_bind(pointer, mod, name, FFI.t_code<TResult>()), f);
        }
        public void bind<T1, TResult>(string mod, string name, System.Func<T1, TResult> f)
        {
            FFI.register(Bindings.pkpy_vm_bind(pointer, mod, name, FFI.t_code<TResult>()), f);
        }
        public void bind<T1, T2, TResult>(string mod, string name, System.Func<T1, T2, TResult> f)
        {
            FFI.register(Bindings.pkpy_vm_bind(pointer, mod, name, FFI.t_code<TResult>()), f);
        }
        public void bind<T1, T2, T3, TResult>(string mod, string name, System.Func<T1, T2, T3, TResult> f)
        {
            FFI.register(Bindings.pkpy_vm_bind(pointer, mod, name, FFI.t_code<TResult>()), f);
        }
        public void bind<T1, T2, T3, T4, TResult>(string mod, string name, System.Func<T1, T2, T3, T4, TResult> f)
        {
            FFI.register(Bindings.pkpy_vm_bind(pointer, mod, name, FFI.t_code<TResult>()), f);
        }
    }
}

namespace pkpy
{
    public class REPL
    {
        public IntPtr pointer { get; private set; }
        public VM vm { get; private set; }

        public REPL(VM vm)
        {
            this.vm = vm;
            pointer = Bindings.pkpy_new_repl(vm.pointer);
        }

        public void dispose()
        {
            Bindings.pkpy_delete(pointer);
        }

        /// <summary>
        /// Input a source line to an interactive console. Return true if need more lines.
        /// </summary>
        public bool input(string line)
        {
            return Bindings.pkpy_repl_input(pointer, line);
        }

    }
}

