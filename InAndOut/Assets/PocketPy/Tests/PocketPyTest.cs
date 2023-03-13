using System.Linq;
using UnityEngine;

namespace pkpy
{
    public class PocketPyTest : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {
            // Create a virtual machine
            pkpy.VM vm = new pkpy.VM();

            // Create a list
            vm.exec("a = [1, 2, 3]");

            // Eval the sum of the list
            string result = vm.eval("sum(a)");
            Debug.Log(result);   // 6

            // Print to the standard output
            vm.exec("print(a)");
            pkpy.PyOutput o = vm.read_output();
            Debug.Log(o.stdout); // [1, 2, 3]

            // Create a binding
            vm.bind("builtins", "test", (double x) => x + 1);
            Debug.Log(vm.eval("test(3.14)")); // '4.14'

            OtherTests(vm);
        }

        void OtherTests(pkpy.VM vm)
        {
            vm.bind("builtins", "test", (string x) => string.Join("|", x.Split(',')));
            Debug.Log(vm.eval("test('1,2,3,4')")); // '4.14'
        }
    }
}