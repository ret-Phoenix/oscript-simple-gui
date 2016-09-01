using ScriptEngine.HostedScript.Library;
using ScriptEngine.Machine.Contexts;
using TreeViewColumnsProject;

namespace oscriptGUI
{
    [ContextClass("ДеревоФормыСтрока", "FormTreeRow")]
    class FormTreeRow: AutoContext<FormTreeRow>
    {

        private TreeViewColumns _item;

        public FormTreeRow(TreeViewColumns itm)
        {
            _item = itm;
        }

        /// <summary>
        /// Получает уровень строки дерева значений.
        /// </summary>
        [ContextProperty("Уровень", "Level")]
        public int Level
        {
            get
            {
                if (_item.TreeView.SelectedNode == null)
                {
                    return -1;
                }
                return _item.TreeView.SelectedNode.Level;
            }
        }

        /// <summary>
        /// Получает уровень строки дерева значений.
        /// </summary>
        [ContextProperty("Родитель", "Parent")]
        public FormTreeRow Parent
        {
            get
            {
                return new FormTreeRow(_item);
            }
        }

        /// <summary>
        /// Получает уровень строки дерева значений.
        /// </summary>
        [ContextProperty("Строки", "Rows")]
        public ArrayImpl rows
        {
            get
            {
                return new ArrayImpl();
            }
        }

    }
}
