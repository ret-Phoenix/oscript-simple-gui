using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ScriptEngine.Machine.Contexts;
using ScriptEngine.Machine;

namespace oscriptcomponent
{

    enum EnumFormGroupType : int
    {
        UsualGroup = 0,
        Page = 1,
        Pages = 2,
    }

    [ContextClass("ВидГруппыФормы", "FormGroupType")]
    class FormGroupType : AutoContext<FormGroupType>, IValue
    {
        [ContextProperty("ОбычнаяГруппа", "UsualGroup")]
        public int UsualGroup
        {
            get { return (int)EnumFormGroupType.UsualGroup; }
        }

        [ContextProperty("Страница", "Page")]
        public int Page
        {
            get { return (int)EnumFormGroupType.Page; }
        }

        [ContextProperty("Страницы", "Pages")]
        public int Pages
        {
            get { return (int)EnumFormGroupType.Pages; }
        }

    }
}
