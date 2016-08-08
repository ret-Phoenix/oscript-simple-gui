using ScriptEngine.Machine.Contexts;


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
