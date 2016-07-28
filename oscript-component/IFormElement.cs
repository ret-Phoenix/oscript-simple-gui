using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ScriptEngine.Machine.Contexts;
using ScriptEngine.Machine;
using System.Windows.Forms;

namespace oscriptGUI
{
    interface IFormElement : IValue
    {
        void setParent(IValue parent);
        IValue Parent { get; }
        string Name { get; set; }
        Control getBaseControl();
        Control getControl();

        void setAction(IRuntimeContextInstance contex, string eventName, string methodName);
        string GetAction(string eventName);
    }
}
