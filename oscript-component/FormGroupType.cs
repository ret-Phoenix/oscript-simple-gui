using ScriptEngine.Machine.Contexts;

namespace oscriptGUI
{

    enum EnumFormGroupType : int
    {
        UsualGroup = 0,
        Page = 1,
        Pages = 2,
    }

    [ContextClass("ВидГруппыФормы", "FormGroupType")]
    public class FormGroupType : AutoContext<FormGroupType>
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
