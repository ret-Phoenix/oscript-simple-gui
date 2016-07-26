using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ScriptEngine.Machine.Contexts;
using ScriptEngine.Machine;


namespace oscriptcomponent
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

    [ContextClass("ПоложениеЗаголовка", "TitleLocation")]
    public class TitleLocation : AutoContext<TitleLocation>, IValue
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
