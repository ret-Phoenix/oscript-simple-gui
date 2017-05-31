// This is an open source non-commercial project. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++ and C#: http://www.viva64.com

using ScriptEngine.Machine.Contexts;
using ScriptEngine.Machine;


namespace oscriptGUI
{
    /// <summary>
    /// Фабрика для работы с формами
    /// </summary>
    [ContextClass("УправляемыйИнтерфейс", "ManagedInterface")]
    public class ManagedInterface : AutoContext<ManagedInterface>
    {
        private readonly string _version;

        private readonly FormFieldType _formFieldType;
        private readonly FormGroupType _formGroupType;
        private readonly TitleLocation _titleLocation;
        private readonly StandardDialogs _standardDialogs;
        private readonly FormControlDockStyle _formControlDockStyle;


        public ManagedInterface()
        {
            this._version = "0.0.0.1";
            this._formFieldType = new FormFieldType();
            this._formGroupType = new FormGroupType();
            this._titleLocation = new TitleLocation();
            this._formControlDockStyle = new FormControlDockStyle();
            this._standardDialogs = new StandardDialogs();

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

        /// <summary>
        /// Системное перечисление.
        /// </summary>
        /// <value>ПоложениеЗаголовка</value>
        [ContextProperty("СтандартныеДиалоги", "StandardDialogs")]
        public IValue StandardDialogs
        {
            get { return _standardDialogs; }
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
