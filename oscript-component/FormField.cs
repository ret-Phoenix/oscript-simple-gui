/*
 * Создано в SharpDevelop.
 * Пользователь: ret-Phoenix
 * Дата: 25.07.2016
 * Время: 0:39
 * 
 * Для изменения этого шаблона используйте меню "Инструменты | Параметры | Кодирование | Стандартные заголовки".
 */
using System;
using System.Drawing;
using System.Windows.Forms;
using ScriptEngine.Machine.Contexts;
using ScriptEngine.Machine;

namespace oscriptGUI
{


    /// <summary>
    /// Description of SFormElementFormField.
    /// </summary>
    [ContextClass("ПолеФормы", "FormField")]
    public class FormField : AutoContext<FormField>, IFormElement
    {

        // private IValue _frm;

        private Panel _panelMainContainer;
        private Panel _panelTitleContainer;
        private Panel _panelControlContainer;
        private Label _label;
        private Control _item;

        private string _name;
        private bool _visible;
        private bool _enabled;
        private string _title;
        //private string _toolTip;
        private IElementsContainer _parent;
        private Control _parentControl;
        private bool _readOnly;

        private int _titleLocation;

        private int _formFieldType;

        private IValue _value;

        private FormFieldType FieldType;
        private ScriptEngine.HostedScript.Library.MapImpl _choiceList;

        private IRuntimeContextInstance _thisScript;
        private string _methodName;

        private IRuntimeContextInstance _thisScriptDblClick;
        private string _methodNameDblClick;


        public FormField(Control parentCntrl)
        {

            FieldType = new FormFieldType();

            this._name = "";
            this._visible = true;
            this._enabled = true;
            this._title = "";
            //this._toolTip = "";
            this._parent = null;
            this._readOnly = false;
            this._parentControl = parentCntrl;
            this._choiceList = null;

            this._item = new TextBox();

            this._methodName = "";
            this._thisScript = null;

            this._methodNameDblClick = "";
            this._thisScriptDblClick = null;

            //# По умолчанию поле ввода (обычный TextBox)
            this._formFieldType = 0;

            //# Создаем контейнер для элемента формы
            _panelMainContainer = new Panel();
            _panelTitleContainer = new Panel();
            _panelControlContainer = new Panel();


            _panelMainContainer.Controls.Add(_panelControlContainer);
            _panelMainContainer.Controls.Add(_panelTitleContainer);

            _panelMainContainer.Dock = DockStyle.Fill;
            _panelMainContainer.MinimumSize = new Size(150, 22);
            _panelMainContainer.AutoSize = true;
            _panelMainContainer.AutoSizeMode = AutoSizeMode.GrowAndShrink;

            _panelTitleContainer.Dock = DockStyle.Left;
            _panelTitleContainer.MinimumSize = new Size(50, 21);
            _panelTitleContainer.AutoSize = true;
            _panelTitleContainer.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            _label = new Label();
            _panelTitleContainer.Controls.Add(_label);
            _label.AutoSize = true;
            _label.Dock = DockStyle.Fill;


            //# Установка параметров панели для поля с данными
            _panelControlContainer.Dock = DockStyle.Fill;
            _panelControlContainer.MinimumSize = new Size(100, 21);
            _panelControlContainer.AutoSize = true;
            _panelControlContainer.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            //_panelControlContainer.Controls.Add(this._item);

            this._parentControl.Controls.Add(_panelMainContainer);
            _panelMainContainer.BringToFront();

            //this.createFormFieldByType();

        }

        public override string ToString()
        {
            return "ПолеФормы";
        }

        public Control getBaseControl()
        {
            return _panelMainContainer;
        }

        public Control getControl()
        {
            return _item;
        }

        public void setParent(IValue parent)
        {
            _parent = (IElementsContainer)parent;
        }
        //[ScriptConstructor]
        //public static IRuntimeContextInstance Constructor()
        //{
        //    return new SimpleFormElementFormField();
        //}

        private void createFormFieldByType()
        {
            Control newItem;
            switch (this._formFieldType)
            {
                case (int)EnumFormFieldType.InputField:
                    newItem = new TextBox();
                    break;
                case (int)EnumFormFieldType.HTMLDocumentField:
                    newItem = new WebBrowser();
                    break;
                case (int)EnumFormFieldType.CalendarField:
                    newItem = new DateTimePicker();
                    break;
                case (int)EnumFormFieldType.CheckBoxField:
                    newItem = new CheckBox();
                    break;
                case (int)EnumFormFieldType.LabelField:
                    newItem = new Label();
                    break;
                //case (int)EnumFormFieldType.PictureField:
                //    newItem = new PictureBox();
                //    break;
                case (int)EnumFormFieldType.ProgressBarField:
                    newItem = new ProgressBar();
                    break;
                case (int)EnumFormFieldType.TextDocumentField:
                    newItem = new TextBox();
                    ((TextBox)newItem).Multiline = true;
                    _panelControlContainer.MinimumSize = new Size(100, 100);
                    break;
                case (int)EnumFormFieldType.ComboBox:
                    newItem = new ComboBox();
                    break;
                case (int)EnumFormFieldType.ListBox:
                    newItem = new ListBox();
                    ((ListBox)newItem).ScrollAlwaysVisible = true;
                    ((ListBox)newItem).MinimumSize =  new Size(100, 100);
                    break;
                default:
                    newItem = new TextBox();
                    break;
            }

            newItem.AutoSize = true;
            newItem.Dock = DockStyle.Fill;
            this._item = newItem;
            _panelControlContainer.Controls.Add(this._item);
        }


        private void setControlValue()
        {
            switch (this._formFieldType)
            {
                case (int)EnumFormFieldType.InputField:
                    ((TextBox)this._item).Text = this._value.ToString();
                    break;
                case (int)EnumFormFieldType.CalendarField:
                    ((DateTimePicker)this._item).Value = this._value.AsDate();
                    break;
                case (int)EnumFormFieldType.CheckBoxField:
                    ((CheckBox)this._item).Checked = this._value.AsBoolean();
                    break;
                case (int)EnumFormFieldType.HTMLDocumentField:
                    ((WebBrowser)this._item).DocumentText = this._value.AsString();
                    break;
                case (int)EnumFormFieldType.LabelField:
                    ((Label)this._item).Text = this._value.AsString();
                    break;
                //case (int)EnumFormFieldType.PictureField:
                //    ((PictureBox)this._item).p = this._value.AsString();
                //    break;
                case (int)EnumFormFieldType.ProgressBarField:
                    ((ProgressBar)this._item).Value = (int)this._value.AsNumber();
                    break;
                case (int)EnumFormFieldType.TextDocumentField:
                    ((TextBox)this._item).Text = this._value.ToString();
                    break;
                case (int)EnumFormFieldType.ComboBox:
                    ((ComboBox)this._item).SelectedValue = this._value;
                    break;
                case (int)EnumFormFieldType.ListBox:
                    ((ListBox)this._item).SelectedValue = this._value;
                    break;
                default:
                    this._item.Text = this._value.ToString();
                    break;
            }
        }

        private IValue getControlValue()
        {

            switch (this._formFieldType)
            {
                case (int)EnumFormFieldType.ComboBox:
                    return ValueFactory.Create(((ComboBox)this._item).SelectedValue.ToString());
                case (int)EnumFormFieldType.ListBox:
                    return ValueFactory.Create(((ListBox)this._item).SelectedValue.ToString());
                case (int)EnumFormFieldType.ProgressBarField:
                    return ValueFactory.Create(((ProgressBar)this._item).Value);
                case (int)EnumFormFieldType.CalendarField:
                    return ValueFactory.Create(((DateTimePicker)this._item).Value);
                case (int)EnumFormFieldType.CheckBoxField:
                    return ValueFactory.Create(((CheckBox)this._item).Checked);
                //case (int)EnumFormFieldType.ComboBox:
                //    return ValueFactory.Create(((ComboBox)this._item).SelectedValue);
                default:
                    return ValueFactory.Create(this._item.Text);
            }
            //return ValueFactory.Create();
        }

        private void setPropertyReadOnly()
        {

            switch (this._formFieldType)
            {
                case 0:
                    ((TextBox)_item).ReadOnly = this._readOnly;
                    break;
                default:
                    break;
            }
        }

        private void setTitleLocation()
        {
            switch (this._titleLocation)
            {
                case (int)EnumTitleLocation.Auto:
                    _panelTitleContainer.Dock = DockStyle.Left;
                    break;
                case (int)EnumTitleLocation.Bottom:
                    _panelTitleContainer.Dock = DockStyle.Bottom;
                    break;
                case (int)EnumTitleLocation.Left:
                    _panelTitleContainer.Dock = DockStyle.Left;
                    break;
                case (int)EnumTitleLocation.None:
                    _panelTitleContainer.Visible = false;
                    break;
                case (int)EnumTitleLocation.Right:
                    _panelTitleContainer.Dock = DockStyle.Right;
                    break;
                case (int)EnumTitleLocation.Top:
                    _panelTitleContainer.Dock = DockStyle.Top;
                    break;
            }
        }

        //public void Dispose()
        //{
        //    throw new NotImplementedException();
        //}

        [ContextProperty("Значение", "Value")]
        public IValue Value
        {
            get
            {
                return getControlValue();
            }
            set
            {
                this._value = value;
                setControlValue();

            }
        }

        [ContextProperty("Вид", "Type")]
        public int ControlType
        {
            get { return this._formFieldType; }
            set
            {
                this._formFieldType = value;
                this.createFormFieldByType();
            }
        }

        [ContextProperty("Имя", "Name")]
        public string Name
        {
            get { return this._name; }
            set
            {
                this._parent.Items.renameElement(this._name, value);
                this._name = value;
            }
        }

        [ContextProperty("Видимость", "Visible")]
        public bool Visible
        {
            get { return this._visible; }
            set
            {
                this._visible = value;
                this._panelMainContainer.Visible = this._visible;
            }
        }

        [ContextProperty("Доступность", "Enabled")]
        public bool Enabled
        {
            get { return this._enabled; }
            set
            {
                this._enabled = value;
                this._panelMainContainer.Enabled = this._enabled;
            }
        }

        [ContextProperty("Заголовок", "Title")]
        public string Title
        {
            get { return this._title; }
            set
            {
                this._title = value;
                this._label.Text = this._title;
                if (this._label.Text.Trim() == String.Empty)
                {
                    this._panelTitleContainer.Visible = false;
                }
                else
                {
                    this._panelTitleContainer.Visible = true;
                }
            }
        }

        [ContextProperty("ПоложениеЗаголовка", "TitleLocation")]
        public int TitleLocation
        {
            get { return this._titleLocation; }
            set
            {
                this._titleLocation = value;
                this.setTitleLocation();
            }
        }

        [ContextProperty("СписокВыбора", "ChoiceList")]
        public ScriptEngine.HostedScript.Library.MapImpl ChoiceList
        {
            get { return this._choiceList; }
            set
            {
                if (this._formFieldType == (int)EnumFormFieldType.ListBox)
                {
                    this._choiceList = value;
                    ((ListBox)this._item).DataSource = new BindingSource(ChoiceList, null);
                    ((ListBox)this._item).DisplayMember = "Key";
                    ((ListBox)this._item).ValueMember = "Value";

                }
                else
                {


                    this._choiceList = value;
                    ((ComboBox)this._item).DataSource = new BindingSource(ChoiceList, null);
                    ((ComboBox)this._item).DisplayMember = "Key";
                    ((ComboBox)this._item).ValueMember = "Value";
                }
            }
        }


        //[ContextProperty("Подсказка", "ToolTip")]
        //public string ToolTip
        //{
        //    get { return this._toolTip; }
        //    set {
        //        this._toolTip = value;
        //    }
        //}

        [ContextProperty("Родитель", "Parent")]
        public IValue Parent
        {
            get { return this._parent; }
        }

        [ContextProperty("ТолькоПросмотр", "ReadOnly")]
        public bool ReadOnly
        {
            get { return this._readOnly; }
            set
            {
                this._readOnly = value;
                this.setPropertyReadOnly();
            }
        }


        //# Блок по работе с событиями

        private void runAction()
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

        private void FormFieldValueChanged(object sender, EventArgs e)
        {
            runAction();
        }

        private void FormFieldDblClick(object sender, EventArgs e)
        {
            runAction();
        }


        [ContextMethod("УстановитьДействие", "SetAction")]
        public void setAction(IRuntimeContextInstance contex, string eventName, string methodName)
        {
            if (eventName == "ПриИзменении")
            {

                switch (this._formFieldType)
                {
                    case (int)EnumFormFieldType.ComboBox:
                        {
                            ((ComboBox)_item).SelectedValueChanged -= FormFieldValueChanged;
                            ((ComboBox)_item).SelectedValueChanged += FormFieldValueChanged;
                            break;
                        }
                    case (int)EnumFormFieldType.ListBox:
                        {
                            ((ListBox)_item).SelectedValueChanged -= FormFieldValueChanged;
                            ((ListBox)_item).SelectedValueChanged += FormFieldValueChanged;
                            break;
                        }

                    //return ValueFactory.Create(((ComboBox)this._item).SelectedValue.ToString());
                    case (int)EnumFormFieldType.ProgressBarField:
                        {
                            Console.WriteLine("ProgressBarField - Disabled setAction");
                            break;
                        }
                    case (int)EnumFormFieldType.CalendarField:
                        {
                            ((DateTimePicker)_item).ValueChanged -= FormFieldValueChanged;
                            ((DateTimePicker)_item).ValueChanged += FormFieldValueChanged;
                            break;
                        }
                    case (int)EnumFormFieldType.CheckBoxField:
                        {
                            ((CheckBox)_item).CheckedChanged -= FormFieldValueChanged;
                            ((CheckBox)_item).CheckedChanged += FormFieldValueChanged;
                            break;
                        }

                    default:
                        {
                            _item.TextChanged -= FormFieldValueChanged;
                            _item.TextChanged += FormFieldValueChanged;
                            break;
                        }

                }

                this._thisScript = contex;
                this._methodName = methodName;
            }
            else if (eventName == "ПриДвойномКлике")
            {
                switch (this._formFieldType)
                {
                    case (int)EnumFormFieldType.ListBox:
                        {
                            ((ListBox)_item).DoubleClick -= FormFieldDblClick;
                            ((ListBox)_item).DoubleClick += FormFieldDblClick;
                            break;
                        }

                }
                this._thisScriptDblClick = contex;
                this._methodNameDblClick = methodName;

            }
        }

        [ContextMethod("ПолучитьДействие", "GetAction")]
        public string GetAction(string eventName)
        {
            if (eventName == "ПриИзменении")
            {
                return "" + this._thisScript.ToString() + ":" + this._methodName;
            }
            else if (eventName == "ПриДвойномКлике")
            {
                return "" + this._thisScriptDblClick.ToString() + ":" + this._methodNameDblClick;
            }
            return "";
            //return "GetAction: Action not supported - " + eventName;
        }

        [ContextProperty("Высота", "Height")]
        public int Height
        {
            get { return _item.Height; }
            set {
                switch (this._formFieldType)
                {
                    case (int)EnumFormFieldType.ListBox:
                        _panelMainContainer.Height = value;
                        break;
                    default:
                        _item.Height = value;
                        break;
                }
            }
        }

        [ContextProperty("Ширина", "Width")]
        public int Width
        {
            get { return _item.Width; }
            set { _item.Width = value; }
        }

        [ContextProperty("АвтоматическийРазмер", "AutoSize")]
        public bool AutoSize
        {
            get { return _item.AutoSize; }
            set { _item.AutoSize = value; }
        }

        [ContextProperty("Закрепление", "Dock")]
        public int Dock
        {
            get { return _item.Dock.GetHashCode(); }
            set
            {
                switch (this._formFieldType)
                {
                    case (int)EnumFormFieldType.ListBox:
                        _panelMainContainer.Dock = (DockStyle)value;
                        break;
                    default:
                        _item.Dock = (DockStyle)value;
                        break;
                }

                
            }
        }

    }
}
