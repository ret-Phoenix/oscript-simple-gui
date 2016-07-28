using System;
using ScriptEngine.Machine.Contexts;
using ScriptEngine.Machine;
using System.Drawing;
using System.Windows.Forms;

namespace oscriptGUI
{
    [ContextClass("КнопкаФормы", "FormButton")]
    class FormButton : AutoContext<FormButton>, IValue, IFormElement
    {
        private Control _item;
        private Control _parentControl;
        private Panel _panel;

        private string _name;
        private bool _visible;
        private bool _enabled;
        private string _title;
        //private string _toolTip;
        private IElementsContainer _parent;
        private IRuntimeContextInstance _thisScript;
        private string _methodName;

        public FormButton(Control parentCntrl)
        {
            this._item = new Button();
            ((Button)this._item).Click += BtnClick;

            this._item.MinimumSize = new Size(100, 21);
            this._item.AutoSize = true;

            this._parentControl = parentCntrl;
            this._panel = new Panel();

            this._name = "";
            this._visible = true;
            this._enabled = true;
            this._title = "";
            this._parent = null;
            this._methodName = "";
            this._thisScript = null;

            this._panel.Dock = DockStyle.Top;
            this._panel.AutoSize = true;
            this._panel.Controls.Add(this._item);
            this._parentControl.Controls.Add(_panel);
            this._panel.BringToFront();
        }


        public override string ToString()
        {
            return "КнопкаФормы";
        }

        [ContextMethod("УстановитьДействие", "SetAction")]
        public void setAction(IRuntimeContextInstance contex, string eventName, string methodName)
        {
            if (eventName == "Нажатие")
            {
                ((Button)this._item).Click -= BtnClick;
                ((Button)this._item).Click += BtnClick;
                this._thisScript = contex;
                this._methodName = methodName;
            }
        }

        [ContextMethod("ПолучитьДействие", "GetAction")]
        public string GetAction(string eventName)
        {
            return "" + this._thisScript.ToString() + ":" + this._methodName;
        }

        private void BtnClick(object sender, EventArgs e)
        {
            if (_thisScript == null)
            {
                return;
            }

            if (_methodName.Trim() == String.Empty)
            {
                return;
            }

            ScriptEngine.HostedScript.Library.ReflectorContext reflector = new ScriptEngine.HostedScript.Library.ReflectorContext();
            reflector.CallMethod(this._thisScript, this._methodName, null);

        }


        [ContextMethod("КнопкаНажатие", "ButtonClick")]
        public void ButtonClick(IRuntimeContextInstance script, string methodName)
        {
            Console.WriteLine("Deprecated: ButtonClick. Use: SetAction");
            this._thisScript = script;
            this._methodName = methodName;
        }

        public Control getBaseControl()
        {
            return _panel;
        }

        public Control getControl()
        {
            return _item;
        }

        public void setParent(IValue parent)
        {
            _parent = (IElementsContainer)parent;
        }

        [ContextProperty("Родитель", "Parent")]
        public IValue Parent
        {
            get { return this._parent; }
        }


        [ContextProperty("Имя", "Name")]
        public string Name
        {
            get { return this._name; }
            set {

                this._parent.Items.renameElement(this._name, value);
                this._name = value;
            }
        }

        [ContextProperty("Видимость", "Visible")]
        public bool Visible
        {
            get { return this._visible; }
            set {
                this._visible = value;
                this._panel.Visible = value;
            }
        }

        [ContextProperty("Доступность", "Enabled")]
        public bool Enabled
        {
            get { return this._enabled; }
            set {
                this._enabled = value;
                this._panel.Enabled = value;
            }
        }

        [ContextProperty("Заголовок", "Title")]
        public string Title
        {
            get { return this._title; }
            set
            {
                this._title = value;
                ((Button)this._item).Text = this._title;
            }
        }
    }
}
