using ScriptEngine.Machine.Contexts;
using ScriptEngine.Machine;

namespace oscriptGUI
{

    /// <summary>
    /// Позиция окна при открытии
    /// </summary>
    [SystemEnum("ПозицияОкнаПриОткрытии", "FormStartPosition")]
    class FormStartPositionEnum : EnumerationContext
    {
        const string CenterParent = "ЦентрРодительского";
        const string CenterScreen = "ЦентрЭкрана";
        const string Manual = "Ручное";

        //const string WindowsDefaultBounds = "WindowsDefaultBounds";
        //const string WindowsDefaultLocation = "WindowsDefaultLocation";

        public static FormStartPositionEnum CreateInstance()
        {
            return EnumContextHelper.CreateEnumInstance<FormStartPositionEnum>((t, v) => new FormStartPositionEnum(t, v));
        }

        public FormStartPositionEnum(TypeDescriptor typeRepresentation, TypeDescriptor valuesType) : base(typeRepresentation, valuesType)
        {
        }

        /// <summary>
        /// Центр родителя
        /// </summary>
        [EnumValue(CenterParent)]
        public EnumerationValue centerParent
        {
            get { return this[CenterParent]; }
        }

        /// <summary>
        /// Центр экрана
        /// </summary>
        [EnumValue(CenterScreen)]
        public EnumerationValue centerScreen
        {
            get { return this[CenterScreen]; }
        }

        /// <summary>
        /// Ручное
        /// </summary>
        [EnumValue(Manual)]
        public EnumerationValue manual
        {
            get { return this[Manual]; }
        }

    }

    /// <summary>
    /// Состояние окна при открытии
    /// </summary>
    [SystemEnum("СостояниеОкна", "WindowState")]
    class WindowStateEnum : EnumerationContext
    {
        const string Maximized = "Развернутое";
        const string Minimized = "Свернутое";
        const string Normal = "Обычное";

        //const string WindowsDefaultBounds = "WindowsDefaultBounds";
        //const string WindowsDefaultLocation = "WindowsDefaultLocation";

        public static WindowStateEnum CreateInstance()
        {
            return EnumContextHelper.CreateEnumInstance<WindowStateEnum>((t, v) => new WindowStateEnum(t, v));
        }

        public WindowStateEnum(TypeDescriptor typeRepresentation, TypeDescriptor valuesType) : base(typeRepresentation, valuesType)
        {
        }

        /// <summary>
        /// Развернуто
        /// </summary>
        [EnumValue(Maximized)]
        public EnumerationValue maximized
        {
            get { return this[Maximized]; }
        }

        /// <summary>
        /// Свернуто
        /// </summary>
        [EnumValue(Minimized)]
        public EnumerationValue minimized
        {
            get { return this[Minimized]; }
        }

        /// <summary>
        /// Обычное
        /// </summary>
        [EnumValue(Normal)]
        public EnumerationValue normal
        {
            get { return this[Normal]; }
        }

    }


}
