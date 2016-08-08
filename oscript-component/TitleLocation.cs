using ScriptEngine.Machine.Contexts;


namespace oscriptGUI
{
    enum EnumTitleLocation : int
    {
        Auto = 0,
        Top = 1,
        Left = 2,
        None = 3,
        Bottom = 5,
        Right = 6,
    }

    [ContextClass("ПоложениеЗаголовкаЭлементаФормы", "FormItemTitleLocation")]
    public class TitleLocation : AutoContext<TitleLocation>
    {

        [ContextProperty("Авто", "Auto")]
        public int Auto
        {
            get { return (int)EnumTitleLocation.Auto; }
        }

        [ContextProperty("Верх", "Top")]
        public int Top
        {
            get { return (int)EnumTitleLocation.Top; }
        }

        [ContextProperty("Лево", "Left")]
        public int Left
        {
            get { return (int)EnumTitleLocation.Left; }
        }

        [ContextProperty("Нет", "None")]
        public int None
        {
            get { return (int)EnumTitleLocation.None; }
        }

        [ContextProperty("Низ", "Bottom")]
        public int Bottom
        {
            get { return (int)EnumTitleLocation.Bottom; }
        }

        [ContextProperty("Право", "Right")]
        public int Right
        {
            get { return (int)EnumTitleLocation.Right; }
        }


    }

}
