/*
 * Создано в SharpDevelop.
 * Пользователь: phoen
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
	public class FormField : AutoContext<FormField>, IValue
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
        private IValue _parent;
        private Control _parentControl;
        private bool _readOnly;

        private int _titleLocation;

        private int _formFieldType;

        private IValue _value;

        private FormFieldType FieldType;
        private ScriptEngine.HostedScript.Library.MapImpl _choiceList;


        public FormField(Control parentCntrl) 
		{

            FieldType = new FormFieldType();

            
            //this._frm = frm;

            this._name = "";
            this._visible = true;
            this._enabled = true;
            this._title = "";
            //this._toolTip = "";
            this._parent = ValueFactory.Create();
            this._readOnly = false;
            this._parentControl = parentCntrl;
            this._choiceList = null;

            this._item = new TextBox();

            //# По умолчанию поле ввода (обычный TextBox)
            this._formFieldType = 0;

            

            //# Создаем контейнер для элемента формы
            _panelMainContainer = new Panel();
            _panelTitleContainer = new Panel();
            _panelControlContainer = new Panel();

            
            _panelMainContainer.Controls.Add(_panelControlContainer);
            _panelMainContainer.Controls.Add(_panelTitleContainer);

            _panelMainContainer.Dock = DockStyle.Top;
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
                    _panelControlContainer.MinimumSize = new Size(100,100);
                    break;
                case (int)EnumFormFieldType.ComboBox:
                    newItem = new ComboBox();
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
                    _panelTitleContainer.Dock = DockStyle. Left;
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
            set {
                this._formFieldType = value;
                this.createFormFieldByType();
            }
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
                this._panelMainContainer.Visible = this._visible;
            }
        }		

        [ContextProperty("Доступность", "Enabled")]
        public bool Enabled
        {
            get { return this._enabled; }
            set {
                this._enabled = value;
                this._panelMainContainer.Enabled = this._enabled;
            }
        }	

        [ContextProperty("Заголовок", "Title")]
        public string Title
        {
            get { return this._title; }
            set {
                this._title = value;
                this._label.Text = this._title;
                if (this._label.Text.Trim() == String.Empty)
                {
                    this._panelTitleContainer.Visible = false;
                } else
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
                this._choiceList = value;
                ((ComboBox)this._item).DataSource = new BindingSource(ChoiceList, null);
                ((ComboBox)this._item).DisplayMember = "Key";
                ((ComboBox)this._item).ValueMember = "Value";

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

        //      [ContextProperty("Родитель", "Parent")]
        //public IValue  Parent
        //{
        //          get { return this._parent; }
        //          set { this._parent = value; }
        //}

        [ContextProperty("ТолькоПросмотр", "ReadOnly")]
        public bool ReadOnly
        {
            get { return this._readOnly; }
            set {
                this._readOnly = value;
                this.setPropertyReadOnly();
            }
        }


    }
}
