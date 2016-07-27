using System;
using ScriptEngine.Machine.Contexts;
using ScriptEngine.Machine;

using System.Windows.Forms;


namespace oscriptGUI
{
    [ContextClass("Форма", "Form")]
    public class ManagedForm : AutoContext<ManagedForm>, IValue
    {
        private string _version;
        private Form _form;
        private Elements _elements;
        private FormFieldType _formFieldType;
        private FormGroupType _formGroupType;
        private TitleLocation _titleLocation;

        public ManagedForm()
        {
            this._version = "0.0.0.1";
            this._form = new Form();
            this._elements = new Elements(_form);
            this._formFieldType = new FormFieldType();
            this._formGroupType = new FormGroupType();
            this._titleLocation = new TitleLocation();
        }

        //[ContextProperty("Версия", "Version")]
        //public string version
        //{
        //    get { return _version; }
        //}

        // можем переопределить строковое отображение наших объектов
        public override string ToString()
        {
            return "Form";
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

    }
}
