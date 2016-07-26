using System;
using ScriptEngine.Machine.Contexts;
using ScriptEngine.Machine;

using System.Windows.Forms;


namespace oscriptcomponent
{
    [ContextClass("ПростойГУИ", "SimpleGUI")]
    public class SimpleGUI : AutoContext<SimpleGUI>
    {
        private string _version;

        public SimpleGUI()
        {
            this._version = "0.0.0.1";
        }

        [ContextProperty("Версия", "Version")]
        public string version
        {
            get { return _version; }
        }

        // можем переопределить строковое отображение наших объектов
        public override string ToString()
        {
            return "SimpleGUI";
        }

        [ScriptConstructor]
        public static IRuntimeContextInstance Constructor()
        {
            return new SimpleGUI();
        }

        [ContextMethod("СоздатьФорму", "CreateForm")]
        public SimpleForm CreateForm()
        {
            return new SimpleForm();
        }

        [ContextMethod("ПоказатьФорму", "ShowNewForm")]
        public void ShowNewForm()
        {
            Form frm = new Form();
            frm.ShowDialog();
        }

    }
}
