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

        private IRuntimeContextInstance _thisScriptOnShown;
        private string _methodNameOnShown;

        private IRuntimeContextInstance _thisScriptOnClose;
        private string _methodNameOnClose;

        public ManagedForm()
        {
            this._version = "0.0.0.1";
            this._form = new Form();
            this._elements = new Elements(this, _form);
            this._formFieldType = new FormFieldType();
            this._formGroupType = new FormGroupType();
            this._titleLocation = new TitleLocation();

            this._name = "";

            this._methodNameOnShown = "";
            this._thisScriptOnShown = null;

            this._methodNameOnClose = "";
            this._thisScriptOnClose = null;

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

        [ContextMethod("Закрыть", "Close")]
        public void Close()
        {
            _form.Close();
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


        [ContextMethod("УстановитьДействие", "SetAction")]
        public void setAction(IRuntimeContextInstance contex, string eventName, string methodName)
        {
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

        [ContextMethod("ПолучитьДействие", "GetAction")]
        public string GetAction(string eventName)
        {
            return String.Empty;
        }


    }
}
