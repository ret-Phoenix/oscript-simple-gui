using ScriptEngine.Machine.Contexts;
using ScriptEngine.Machine;
using System.Drawing;
using System.Windows.Forms;

namespace oscriptGUI
{

    [ContextClass("СтильЗакрепления", "DockStyle")]
    class FormControlDockStyle : AutoContext<FormControlDockStyle>, IValue
    {

        [ContextProperty("Нет", "None")]
        public int None
        {
            get { return 0; }
        }

        [ContextProperty("Верх", "Top")]
        public int Top
        {
            get { return 1; }
        }

        [ContextProperty("Низ", "Bottom")]
        public int Bottom
        {
            get { return 2; }
        }

        [ContextProperty("Лево", "Left")]
        public int Left
        {
            get { return 3; }
        }


        [ContextProperty("Право", "Right")]
        public int Right
        {
            get { return 4; }
        }

        [ContextProperty("Заполнение", "Fill")]
        public int Fill
        {
            get { return 5; }
        }



    }
}
