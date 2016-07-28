using System;
using ScriptEngine.Machine.Contexts;
using ScriptEngine.Machine;

using System.Windows.Forms;


namespace oscriptGUI
{
    [ContextClass("Форма", "Form")]
    public class ManagedForm : AutoContext<ManagedForm>, IValue, IFormElement, IElementsContainer
    {
        private string _version;
        private Form _form;
        private Elements _elements;
        private FormFieldType _formFieldType;
        private FormGroupType _formGroupType;
        private TitleLocation _titleLocation;

        private string _name;

        public ManagedForm()
        {
            this._version = "0.0.0.1";
            this._form = new Form();
            this._elements = new Elements(this, _form);
            this._formFieldType = new FormFieldType();
            this._formGroupType = new FormGroupType();
            this._titleLocation = new TitleLocation();

            this._name = "";
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


        [ContextProperty("Заголовок", "Caption")]
        public string Caption
        {
            get { return _form.Text; }
            set { _form.Text = value; }
        }

        [ContextProperty("Родитель", "Parent")]
        public IValue Parent
        {
            get { return ValueFactory.Create(); }
        }

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

        [ContextMethod("Показать", "Show")]
        public void Show()
        {
            _form.ShowDialog();
        }

        /// <summary>
        /// Используется для прохождения тестов. Если открывать не модально сразу закроется и будут доступны другие методы
        /// </summary>
        [ContextMethod("ПоказатьНеМодально", "ShowNotModal")]
        public void ShowNotModal()
        {
            _form.Show();
        }


        [ContextProperty("Элементы", "Items")]
        public Elements Items
        {
            get { return _elements; }
        }

        [ScriptConstructor]
        public static IRuntimeContextInstance Constructor()
        {
            return new ManagedForm();
        }

        [ContextMethod("УстановитьДействие", "SetAction")]
        public void setAction(IRuntimeContextInstance contex, string eventName, string methodName)
        {
            if (eventName == "Нажатие")
            {
                //((Button)this._item).Click += BtnClick;
                //this._thisScript = contex;
                //this._methodName = methodName;
            }
        }

        [ContextMethod("ПолучитьДействие", "GetAction")]
        public string GetAction(string eventName)
        {
            return String.Empty;
        }


    }
}
