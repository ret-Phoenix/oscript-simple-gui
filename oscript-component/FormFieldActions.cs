using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ScriptEngine.Machine.Contexts;
using ScriptEngine.Machine;


namespace oscriptGUI
{
    [ContextClass("ПолеФормыДействия", "FormFieldActions")]
    class FormFieldActions
    {
        [ContextProperty("ПриИзменении", "OnChange")]
        public static string OnChange { get { return "ПриИзменении"; } }

        //[ContextProperty("Очистка", "Clearing")]
        //public static string Clearing { get { return "Очистка"; } }

    }
}
