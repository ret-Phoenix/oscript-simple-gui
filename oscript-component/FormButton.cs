using System;
using ScriptEngine.Machine.Contexts;
using ScriptEngine.Machine;
using System.Drawing;
using System.Windows.Forms;

namespace oscriptGUI
{
    [ContextClass("КнопкаФормы", "FormButton")]
    class FormButton : AutoContext<FormButton>, IValue
    {
        private Control _item;
        private Control _parentControl;
        private Panel _panel;

        private string _name;
        private bool _visible;
        private bool _enabled;
        private string _title;
        //private string _toolTip;
        private IValue _parent;
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
            this._parent = ValueFactory.Create();

            if (this._parentControl is Form)
            {
                _panel.Dock = DockStyle.Top;
                _panel.AutoSize = true;
                _panel.Controls.Add(this._item);
                this._parentControl.Controls.Add(_panel);
                _panel.BringToFront();
            }
            else
            {
                this._parentControl.Controls.Add(this._item);
                this._item.BringToFront();
            }

            
            
        }

        public void BtnClick(object sender, EventArgs e)
        {
            ScriptEngine.HostedScript.Library.ReflectorContext reflector = new ScriptEngine.HostedScript.Library.ReflectorContext();
            reflector.CallMethod(this._thisScript, this._methodName, null);
        }


        [ContextMethod("КнопкаНажатие", "ButtonClick")]
        public void ButtonClick(IRuntimeContextInstance script, string methodName)
        {
            this._thisScript = script;
            this._methodName = methodName;
        }

        [ContextProperty("Имя", "Name")]
        public string Name
        {
            get { return this._name; }
            set { this._name = value; }
        }

        [ContextProperty("Видимость", "Visible")]
        public bool Visible
        {
            get { return this._visible; }
            set {
                this._visible = value;
                if (this._parentControl is Form)
                {
                    this._panel.Visible = value;
                } else
                {
                    this._item.Visible = value;
                }
            }
        }

        [ContextProperty("Доступность", "Enabled")]
        public bool Enabled
        {
            get { return this._enabled; }
            set {
                this._enabled = value;
                if (this._parentControl is Form)
                {
                    this._panel.Enabled = value;
                }
                else
                {
                    this._item.Enabled = value;
                }

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

        //[ContextProperty("Подсказка", "ToolTip")]
        //public string ToolTip
        //{
        //    get { return this._toolTip; }
        //    set { this._toolTip = value; }
        //}

        [ContextProperty("Родитель", "Parent")]
        public IValue Parent
        {
            get { return this._parent; }
            set { this._parent = value; }
        }


    }
}
