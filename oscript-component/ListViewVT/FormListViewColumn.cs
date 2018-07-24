using ScriptEngine.Machine.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace oscriptGUI.ListViewVT
{
    /// <summary>
    /// Колонка элемента ПредставлениеСписка
    /// </summary>
    [ContextClass("КолонкаПредставленияСписка", "ListViewColumn")]
    public class ListViewColumn : AutoContext<ListViewColumn>
    {
        private ListView _grid;
        private int _pos;

        public ListViewColumn(ListView grid, int pos)
        {

            _grid = grid;
            _pos = pos;
        }

        public override string ToString()
        {
            return "КолонкаПредставленияСписка";
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
            set { _grid.Columns[_pos].Text = value;  }
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
