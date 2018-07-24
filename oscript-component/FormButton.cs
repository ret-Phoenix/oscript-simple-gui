using System;
using ScriptEngine.Machine.Contexts;
using ScriptEngine.Machine;
using System.Drawing;
using System.Windows.Forms;

namespace oscriptGUI
{
    [ContextClass("КнопкаФормы", "FormButton")]
    public class FormButton : AutoContext<FormButton>, IFormElement
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

        /// <summary>
        /// Установить обработчик события.
        /// Возможные события:
        /// - Нажатие - Нажатие на кнопку
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
            if (eventName == "Нажатие")
            {
                ((Button)this._item).Click -= BtnClick;
                ((Button)this._item).Click += BtnClick;
                this._thisScript = contex;
                this._methodName = methodName;
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
            return "" + ((ScriptInformationContext)this._thisScript).Source + ":" + this._methodName;
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

        /// <summary>
        /// Содержит ссылку на родительский элемент. <see cref="FormGroup"/>
        /// </summary>
        /// <value>ГруппаФормы, Форма</value>
        [ContextProperty("Родитель", "Parent")]
        public IValue Parent
        {
            get { return this._parent; }
        }


        /// <summary>
        /// Уникальное имя элемента
        /// </summary>
        [ContextProperty("Имя", "Name")]
        public string Name
        {
            get { return this._name; }
            set {

                this._parent.Items.renameElement(this._name, value);
                this._name = value;
            }
        }

        /// <summary>
        /// Управление видимостью кнопки
        /// </summary>
        [ContextProperty("Видимость", "Visible")]
        public bool Visible
        {
            get { return this._visible; }
            set {
                this._visible = value;
                this._panel.Visible = value;
            }
        }

        /// <summary>
        /// Управление доступностью
        /// </summary>
        [ContextProperty("Доступность", "Enabled")]
        public bool Enabled
        {
            get { return this._enabled; }
            set {
                this._enabled = value;
                this._panel.Enabled = value;
            }
        }

        /// <summary>
        /// Надпись на кнопке
        /// </summary>
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

        /// <summary>
        /// Высота кнопки
        /// </summary>
        [ContextProperty("Высота", "Height")]
        public int Height
        {
            get { return _item.Height; }
            set { _item.Height = value; }
        }

        /// <summary>
        /// Ширина кнопки
        /// </summary>
        [ContextProperty("Ширина", "Width")]
        public int Width
        {
            get { return _item.Width; }
            set { _item.Width = value; }
        }

        /// <summary>
        /// Автоматический размер
        /// </summary>
        [ContextProperty("АвтоматическийРазмер", "AutoSize")]
        public bool AutoSize
        {
            get { return _item.AutoSize; }
            set { _item.AutoSize = value; }
        }

        /// <summary>
        /// Вариант закрепления. <see cref="FormControlDockStyle"/>
        /// </summary>
        /// <value>СтильЗакрепления</value>
        [ContextProperty("Закрепление", "Dock")]
        public int Dock
        {
            get { return _panel.Dock.GetHashCode(); }
            set
            {
                _panel.Dock = (DockStyle)value;
            }
        }

    }
}
