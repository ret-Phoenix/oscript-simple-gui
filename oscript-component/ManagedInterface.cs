using ScriptEngine.Machine.Contexts;
using ScriptEngine.Machine;


namespace oscriptGUI
{
    /// <summary>
    /// Фабрика для работы с формами
    /// </summary>
    [ContextClass("УправляемыйИнтерфейс", "ManagedInterface")]
    class ManagedInterface : AutoContext<ManagedInterface>
    {
        private string _version;

        private FormFieldType _formFieldType;
        private FormGroupType _formGroupType;
        private TitleLocation _titleLocation;
        private FormControlDockStyle _formControlDockStyle;


        public ManagedInterface()
        {
            this._version = "0.0.0.1";
            this._formFieldType = new FormFieldType();
            this._formGroupType = new FormGroupType();
            this._titleLocation = new TitleLocation();
            this._formControlDockStyle = new FormControlDockStyle();

        }

        /// <summary>
        /// Номер версии библиотеки
        /// </summary>
        [ContextProperty("Версия", "Version")]
        public string version
        {
            get { return _version; }
        }

        /// <summary>
        /// Системное перечисление. Вариант закрепления. <see cref="FormControlDockStyle"/>
        /// </summary>
        /// <value>СтильЗакрепления</value>
        [ContextProperty("СтильЗакрепления", "DockStyle")]
        public IValue FormControlDockStyle
        {
            get { return _formControlDockStyle; }
        }

        /// <summary>
        /// Системное перечисление.
        /// </summary>
        /// <value>ВидПоляФормы</value>
        [ContextProperty("ВидПоляФормы", "FormFieldType")]
        public IValue FormFieldType
        {
            get { return _formFieldType; }
        }

        /// <summary>
        /// Системное перечисление.
        /// </summary>
        /// <value>ВидГруппыФормы</value>
        [ContextProperty("ВидГруппыФормы", "FormGroupType")]
        public IValue FormGroupType
        {
            get { return _formGroupType; }
        }

        /// <summary>
        /// Системное перечисление.
        /// </summary>
        /// <value>ПоложениеЗаголовка</value>
        [ContextProperty("ПоложениеЗаголовка", "TitleLocation")]
        public IValue TitleLocation
        {
            get { return _titleLocation; }
        }

        // можем переопределить строковое отображение наших объектов
        public override string ToString()
        {
            return "УправляемыйИнтерфейс";
        }

        [ScriptConstructor]
        public static IRuntimeContextInstance Constructor()
        {
            return new ManagedInterface();
        }

        /// <summary>
        /// Создать форму
        /// </summary>
        /// <returns>УправляемаяФорма</returns>
        [ContextMethod("СоздатьФорму", "CreateForm")]
        public ManagedForm CreateForm()
        {
            return new ManagedForm();
        }


    }
}
