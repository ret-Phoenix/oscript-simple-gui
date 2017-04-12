using System;
using ScriptEngine.Machine.Contexts;
using ScriptEngine.Machine;

using System.Windows.Forms;
using System.Drawing;

namespace oscriptGUI
{
    /// <summary>
    /// Основной контейнер для отображения.
    /// Используется для доступа к свойствам, методам и событиям управляемой формы в целом, а также к коллекции ее элементов управления. 
    /// </summary>
    [ContextClass("Форма", "Form")]
    public class ManagedForm : AutoContext<ManagedForm>, IFormElement, IElementsContainer
    {
        //private string _version;
        private Form _form;
        private Elements _elements;
        private FormFieldType _formFieldType;
        private FormGroupType _formGroupType;
        private TitleLocation _titleLocation;

        private string _name;

        private IRuntimeContextInstance _thisScriptOnShown;
        private string _methodNameOnShown;

        private IRuntimeContextInstance _thisScriptOnClose;
        private string _methodNameOnClose;

        //private IRuntimeContextInstance _thisScriptOnCreated;
        //private string _methodNameOnCreated;


        public ManagedForm()
        {
            //this._version = "0.0.0.1";
            this._form = new Form();
            this._form.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            this._form.MinimumSize = new Size(50, 50);
            this._elements = new Elements(this, _form);
            this._formFieldType = new FormFieldType();
            this._formGroupType = new FormGroupType();
            this._titleLocation = new TitleLocation();

            this._name = "";

            this._methodNameOnShown = "";
            this._thisScriptOnShown = null;

            this._methodNameOnClose = "";
            this._thisScriptOnClose = null;

            //this._methodNameOnCreated = "";
            //this._thisScriptOnCreated = null;

        }

        //[ContextProperty("Версия", "Version")]
        //public string version
        //{
        //    get { return _version; }
        //}

        // можем переопределить строковое отображение наших объектов
        public override string ToString()
        {
            return "УправляемаяФорма";
        }

        /// <summary>
        /// Системное перечисление
        /// </summary>
        /// <value>ВидПоляФормы</value>
        [ContextProperty("ВидПоляФормы", "FormFieldType")]
        public IValue FormFieldType
        {
            get { return _formFieldType; }
        }

        /// <summary>
        /// Системное перечисление
        /// </summary>
        /// <value>ВидГруппыФормы</value>
        [ContextProperty("ВидГруппыФормы", "FormGroupType")]
        public IValue FormGroupType
        {
            get { return _formGroupType; }
        }

        /// <summary>
        /// Системное перечисление
        /// </summary>
        /// <value>ПоложениеЗаголовкаЭлементаФормы</value>
        [ContextProperty("ПоложениеЗаголовка", "TitleLocation")]
        public IValue TitleLocation
        {
            get { return _titleLocation; }
        }


        /// <summary>
        /// Заголовок формы
        /// </summary>
        [ContextProperty("Заголовок", "Caption")]
        public string Caption
        {
            get { return _form.Text; }
            set { _form.Text = value; }
        }

        /// <summary>
        /// Родитель формы. Всегда Неопределено
        /// </summary>
        [ContextProperty("Родитель", "Parent")]
        public IValue Parent
        {
            get { return ValueFactory.Create(); }
        }

        /// <summary>
        /// Имя формы
        /// </summary>
        [ContextProperty("Имя", "Name")]
        public string Name
        {
            get { return this._name; }
            set { this._name = value; }
        }

        public void setParent(IValue parent)
        {
            return;
        }


        public Control getBaseControl()
        {
            return _form;
        }

        public Control getControl()
        {
            return _form;
        }

        /// <summary>
        /// Показать форму (показывает модально)
        /// </summary>
        [ContextMethod("Показать", "Show")]
        public void Show()
        {
            _form.ShowDialog(null);
        }

        /// <summary>
        /// Используется для прохождения тестов. Если открывать не модально сразу закроется и будут доступны другие методы
        /// </summary>
        [ContextMethod("ПоказатьНеМодально", "ShowNotModal")]
        public void ShowNotModal()
        {
            _form.Show();
        }

        /// <summary>
        /// Закрыть форму
        /// </summary>
        [ContextMethod("Закрыть", "Close")]
        public void Close()
        {
            _form.Close();
        }


        /// <summary>
        /// Содержит коллекцию подчиненных элементов группы.
        /// </summary>
        /// <value>Элементы</value>
        [ContextProperty("Элементы", "Items")]
        public Elements Items
        {
            get { return _elements; }
        }

        /// <summary>
        /// Автоматический размер
        /// </summary>
        [ContextProperty("АвтоматическийРазмер", "AutoSize")]
        public bool AutoSize
        {
            get { return _form.AutoSize; }
            set { _form.AutoSize = value;  }
        }

        /// <summary>
        /// Высота
        /// </summary>
        [ContextProperty("Высота", "Height")]
        public int Height
        {
            get { return _form.Height; }
            set { _form.Height = value; }
        }

        /// <summary>
        /// Ширина
        /// </summary>
        [ContextProperty("Ширина", "Width")]
        public int Width
        {
            get { return _form.Width; }
            set { _form.Width = value; }
        }


        [ScriptConstructor]
        public static IRuntimeContextInstance Constructor()
        {
            return new ManagedForm();
        }

        //private void runAction()
        //{
        //    if (_thisScript == null)
        //    {
        //        return;
        //    }

        //    if (_methodName.Trim() == String.Empty)
        //    {
        //        return;
        //    }

        //    ScriptEngine.HostedScript.Library.ReflectorContext reflector = new ScriptEngine.HostedScript.Library.ReflectorContext();
        //    reflector.CallMethod(this._thisScript, this._methodName, null);
        //}

        private void runAction(IRuntimeContextInstance script, string method)
        {
            if (script == null)
            {
                return;
            }

            if (method.Trim() == String.Empty)
            {
                return;
            }

            ScriptEngine.HostedScript.Library.ReflectorContext reflector = new ScriptEngine.HostedScript.Library.ReflectorContext();
            reflector.CallMethod(script, method, null);
        }

        private void OnFormShown(object sender, EventArgs e)
        {
            runAction(_thisScriptOnShown, _methodNameOnShown);
        }

        private void OnFormClose(object sender, EventArgs e)
        {
            runAction(_thisScriptOnClose, _methodNameOnClose);
        }

        /// <summary>
        /// Установить обработчик события.
        /// Возможные события:
        /// - ПриОткрытии - При первом открытии формы
        /// - ПриЗакрытии - При закрытии формы
        /// </summary>
        /// <param name="contex">Ссылка на скрипт в котором находится обработчик события</param>
        /// <param name="eventName">Имя обрабатываемого события</param>
        /// <param name="methodName">Имя метода обработчика события</param>
        /// <example>
        ///  Форма.УстановитьДействие(ЭтотОбъект, "ПриОткрытии", "ПриОткрытииФормы");
        /// </example>
        [ContextMethod("УстановитьДействие", "SetAction")]
        public void setAction(IRuntimeContextInstance contex, string eventName, string methodName)
        {
            //if (eventName == "ПриСоздании")
            //{
            //    this._form.HandleCreated -= OnFormCreated;
            //    this._form.HandleCreated += OnFormCreated;
            //    this._thisScriptOnCreated = contex;
            //    this._methodNameOnCreated = methodName;

            //}

            if (eventName == "ПриОткрытии")
            {
                this._form.Shown -= OnFormShown;
                this._form.Shown += OnFormShown;
                this._thisScriptOnShown = contex;
                this._methodNameOnShown = methodName;

            }
            else if (eventName == "ПриЗакрытии")
            {
                this._form.FormClosing -= OnFormClose;
                this._form.FormClosing += OnFormClose;

                this._thisScriptOnClose = contex;
                this._methodNameOnClose = methodName;

            }


        }

        /// <summary>
        /// Получает имя установленного обработчика события.
        /// </summary>
        /// <param name="eventName">Имя события</param>
        /// <returns>Имя метода обработчика события</returns>
        /// <example>
        /// Форма.УстановитьДействие(ЭтотОбъект, "ПриОткрытии", "ПриОткрытииФормы");
        /// Форма.ПолучитьДействие("ПриОткрытии");
        /// // вернет: "ПриОткрытииФормы"
        /// </example>
        [ContextMethod("ПолучитьДействие", "GetAction")]
        public string GetAction(string eventName)
        {
            return String.Empty;
        }

        /// <summary>
        /// Неиспользуется. Создан для реализации интерфейса
        /// </summary>
        [ContextProperty("Закрепление", "Dock")]
        public int Dock
        {
            get { return 0; }
            set {; }
        }

        private void setCurrentItem(IValue curItm)
        {
            if (curItm.ToString() == "ПолеФормы")
            {
                ((FormField)curItm).getControl().Focus();
            }
        }

        /// <summary>
        /// Устанавливает текущий активный элемент формы.
        /// </summary>
        /// <value>ПолеФормы, ГруппаФормы, КнопкаФормы</value>
        [ContextProperty("ТекущийЭлемент", "CurrentItem")]
        public IValue CurrentItem
        {
            //get { return 0; }
            set { setCurrentItem(value);}
        }


    }
}
