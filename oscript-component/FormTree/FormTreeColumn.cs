using ScriptEngine.Machine.Contexts;
using TreeViewColumnsProject;

namespace oscriptGUI
{
    [ContextClass("КолонкаДерева", "FormTreeColumn")]
    class FormTreeColumn: AutoContext<FormTreeColumn>
    {
        private TreeViewColumns _grid;
        private int _pos;

        public FormTreeColumn(TreeViewColumns grid, int pos)
        {

            _grid = grid;
            _pos = pos;
        }

        /// <summary>
        /// Ширина
        /// </summary>
        [ContextProperty("Ширина", "Width")]
        public int Width
        {
            get { return _grid.Columns[_pos].Width; }
            set { _grid.Columns[_pos].Width = value; }
        }

        /// <summary>
        /// Заголовок
        /// </summary>
        [ContextProperty("Заголовок", "Title")]
        public string Title
        {
            get { return _grid.Columns[_pos].Text; }
            set { _grid.Columns[_pos].Text = value; }
        }

        /// <summary>
        /// Номер колонки
        /// </summary>
        [ContextProperty("Индекс", "Index")]
        public int Index
        {
            get { return _pos; }
        }


    }
}
