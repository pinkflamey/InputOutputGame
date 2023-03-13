using UnityEngine;
using UnityEngine.UI;

namespace pkpy
{
    public class PocketPyNumberTest : MonoBehaviour
    {
        Text text;
        pkpy.VM vm;

        // Start is called before the first frame update
        void Start()
        {
            text = GetComponent<Text>();
            Application.targetFrameRate = 60;
            vm = new pkpy.VM();
            vm.exec("a = 0");
        }

        // Update is called once per frame
        void Update()
        {
            vm.exec("a += 1");
            text.text = vm.eval("a");
        }
    }
}