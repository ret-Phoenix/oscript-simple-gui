using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ScriptEngine.Machine.Contexts;
using ScriptEngine.Machine;


namespace oscriptGUI
{
    interface IElementsContainer : IValue
    {
        Elements Items { get; }
    }
}
