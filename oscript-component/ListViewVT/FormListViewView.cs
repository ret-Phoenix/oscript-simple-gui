using ScriptEngine.Machine;
using ScriptEngine.Machine.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace oscriptGUI.ListViewVT
{

    [ContextClass("Представление", "View")]
    public sealed class FormListViewView : AutoContext<FormListViewView>
    {
        [ContextProperty("БольшиеЗначки")]
        public int LargeIcon { get { return 0; } }

        [ContextProperty("Таблица")]
        public int Details { get { return 1; } }

        [ContextProperty("МаленькиеЗначки")]
        public int SmallIcon { get { return 2; } }

        [ContextProperty("Список")]
        public int List { get { return 3; } }

        [ContextProperty("Плитка")]
        public int Tile { get { return 4; } }
    }



}
