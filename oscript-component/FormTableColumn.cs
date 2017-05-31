using ScriptEngine.Machine.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace oscriptGUI
{
    /// <summary>
    /// Колонка элемента ТаблицаФормы
    /// </summary>
    [ContextClass("КолонкаТаблицыФормы", "FormTableColumn")]
    public class FormTableColumn : AutoContext<FormTableColumn>
    {
        private DataGridView _grid;
        private int _pos;

        public FormTableColumn(DataGridView grid, int pos)
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
        /// Видимость
        /// </summary>
        [ContextProperty("Видимость", "Visible")]
        public bool Visible
        {
            get { return _grid.Columns[_pos].Visible; }
            set { _grid.Columns[_pos].Visible = value;  }
        }

        /// <summary>
        /// Заголовок
        /// </summary>
        [ContextProperty("Заголовок", "Title")]
        public string Title
        {
            get { return _grid.Columns[_pos].HeaderText; }
            set { _grid.Columns[_pos].HeaderText = value;  }
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
