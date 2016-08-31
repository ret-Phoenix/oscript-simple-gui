using ScriptEngine.Machine;
using ScriptEngine.Machine.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TreeViewColumnsProject;
using static System.Windows.Forms.ListView;

namespace oscriptGUI
{
    [ContextClass("КолонкаДерева", "FormTreeColumn")]
    class FormTreeColumn: AutoContext<FormTreeColumn>
    {
        private TreeViewColumns _grid;
        private int _pos;
        private ColumnHeaderCollection _cols;

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

        /////// <summary>
        /////// Видимость
        /////// </summary>
        ////[ContextProperty("Видимость", "Visible")]
        ////public bool Visible
        ////{
        ////    get { return true;  }
        ////    //get { return _grid.Columns[_pos].ListView.Columns[0].vi; }
        ////    //set { _grid.Columns[_pos].Visible = value; }
        ////}

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
