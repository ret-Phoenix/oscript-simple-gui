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

        public DataTableProvider()
        {
            _valueTable = new ValueTable();
        }

        private void setProviderValueTable()
        {
            _dataTable.Clear();

            

            //foreach (ValueTableColumn VTCol in _valueTable.Columns)
            //{
            //    _dataTable.Columns.Add(VTCol.Name);
            //}
        }

        [ContextProperty("Источник", "Source")]
        public ValueTable Height
        {
            get { return _valueTable; }
            set { _valueTable = value; setProviderValueTable(); }
        }
    }
}
