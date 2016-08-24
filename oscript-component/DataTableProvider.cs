using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ScriptEngine.Machine;
using ScriptEngine.Machine.Contexts;
using ScriptEngine.HostedScript.Library.ValueTable;
using System.Data;

namespace oscriptGUI
{
    [ContextClass("Провайдер", "Provider")]
    class DataTableProvider : AutoContext<DataTableProvider>
    {
        private ValueTable _valueTable;
        private DataTable _dataTable;

        [ScriptConstructor]
        public static IRuntimeContextInstance Constructor()
        {
            return new DataTableProvider();
        }

        public DataTableProvider()
        {
            _valueTable = new ValueTable();
            _dataTable = new DataTable();
        }

        public DataTable getData()
        {
            return _dataTable;
        }

        private void setProviderValueTable()
        {
            _dataTable.Clear();
            _dataTable.Columns.Clear();

            foreach (ValueTableColumn VTCol in _valueTable.Columns)
            {
                _dataTable.Columns.Add(VTCol.Name);
            }


            DataRow row;
            foreach (ValueTableRow VTRow in _valueTable)
            {
                row = _dataTable.NewRow();
                foreach (ValueTableColumn VTCol in _valueTable.Columns)
                {
                    row[VTCol.Name] = VTRow.Get(VTCol);
                }
                _dataTable.Rows.Add(row);
            }
        }

        public void Refresh()
        {
            setProviderValueTable();
        }

        [ContextProperty("Источник", "Source")]
        public ValueTable Source
        {
            get { return _valueTable; }
            set { _valueTable = value; setProviderValueTable(); }
        }
    }
}
