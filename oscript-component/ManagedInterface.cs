using ScriptEngine.Machine.Contexts;
using ScriptEngine.Machine;


namespace oscriptGUI
{
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

        [ContextProperty("СтильЗакрепления", "DockStyle")]
        public IValue FormControlDockStyle
        {
            get { return _formControlDockStyle; }
        }

        [ContextProperty("ВидПоляФормы", "FormFieldType")]
        public IValue FormFieldType
        {
            get { return _formFieldType; }
        }

        [ContextProperty("ВидГруппыФормы", "FormGroupType")]
        public IValue FormGroupType
        {
            get { return _formGroupType; }
        }


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
        /// <returns><see>SimpleForm</see> Возращает форму</returns>
        [ContextMethod("СоздатьФорму", "CreateForm")]
        public ManagedForm CreateForm()
        {
            return new ManagedForm();
        }


    }
}
