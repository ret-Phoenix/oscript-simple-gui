/*
 * Создано в SharpDevelop.
 * Пользователь: phoen
 * Дата: 24.07.2016
 * Время: 14:33
 * 
 * Для изменения этого шаблона используйте меню "Инструменты | Параметры | Кодирование | Стандартные заголовки".
 */
using System.Windows.Forms;
using ScriptEngine.Machine.Contexts;
using ScriptEngine.Machine;
using System.Drawing;

namespace oscriptGUI
{
    /// <summary>
    /// Элемент формы, предназначенный для визуальной и/или логической группировки элементов.
    /// </summary>
    [ContextClass("ГруппаФормы", "FormGroup")]
    public class FormGroup : AutoContext<FormGroup>, IValue, IFormElement, IElementsContainer
    {
        private Control _item;
        private int _formGroupType;
        private Control _parentControl;

        private string _name;
        private bool _visible;
        private bool _enabled;
        private string _title;
        //private string _toolTip;
        private IValue _parent;
        private Elements _elements;

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="parentCntrl"></param>
        public FormGroup(Control parentCntrl)
        {
            this._item = new GroupBox();
            this._formGroupType = (int)EnumFormGroupType.UsualGroup;
            this._parentControl = parentCntrl;

            this._name = "";
            this._visible = true;
            this._enabled = true;
            this._title = "";
            //this._toolTip = "";
            this._parent = ValueFactory.Create();

            this._elements = new Elements(this, _item);

        }


        public override string ToString()
        {
            return "ГруппаФормы";
        }
        /// <summary>
        /// Получение ссылки на элемент формы.
        /// </summary>
        /// <returns></returns>
        public Control getControl()
        {
            return this._item;
        }
        //[ScriptConstructor]
        //public static IRuntimeContextInstance Constructor()
        //{
        //    return new SimpleFormElementGroup();
        //}

        //[ContextMethod("ПодчиненныеЭлементы", "ChildItems")]
        //public SimpleFormElements ChildItems()
        //{
        //    return new SimpleFormElements(null);
        //}

        public Control getBaseControl()
        {
            return _item;
        }

        public void setParent(IValue parent)
        {
            _parent = parent;
        }


        [ContextProperty("Родитель", "Parent")]
        public IValue Parent
        {
            get { return this._parent; }
            //   set { this._parent = value; }
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
            set { this._visible = value; }
        }

        [ContextProperty("Доступность", "Enabled")]
        public bool Enabled
        {
            get { return this._enabled; }
            set { this._enabled = value; }
        }

        [ContextProperty("Заголовок", "Title")]
        public string Title
        {
            get { return this._title; }
            set {
                this._title = value;
                setTitle();
            }
        }

        //[ContextProperty("Подсказка", "ToolTip")]
        //public string ToolTip
        //{
        //    get { return this._toolTip; }
        //    set { this._toolTip = value; }
        //}

        //[ContextProperty("Родитель", "Parent")]
        //public IValue Parent
        //{
        //    get { return this._parent; }
        //    set { this._parent = value; }
        //}

        [ContextProperty("Вид", "Type")]
        public int ControlType
        {
            get { return this._formGroupType; }
            set
            {
                this._formGroupType = value;
                this.createFormFieldByType();
            }
        }

        [ContextProperty("Элементы", "Items")]
        public Elements Items
        {
            get { return _elements; }
        }

        /////////////////////////////////////////////////////////////
        /// Методы обработки свойств

        private void createFormFieldByType()
        {
            Control newItem;
            switch (this._formGroupType)
            {
                case (int)EnumFormGroupType.UsualGroup:
                    newItem = new GroupBox();
                    ((GroupBox)newItem).AutoSizeMode = AutoSizeMode.GrowAndShrink;
                    break;
                case (int)EnumFormGroupType.Page:
                    newItem = new TabPage();
                    ((TabPage)newItem).AutoSizeMode = AutoSizeMode.GrowAndShrink;
                    break;
                case (int)EnumFormGroupType.Pages:
                    newItem = new TabControl();
                    break;
                default:
                    newItem = new GroupBox();
                    ((GroupBox)newItem).AutoSizeMode = AutoSizeMode.GrowAndShrink;
                    break;
            }

            this._item = newItem;

            if (this._formGroupType == (int)EnumFormGroupType.Page)
            {
                ((TabControl)this._parentControl).TabPages.Add((TabPage)this._item);
            }
            else
            {
                newItem.AutoSize = true;
                newItem.Dock = DockStyle.Top;
                newItem.MinimumSize = new Size(100, 21);
                newItem.AutoSize = true;

                this._parentControl.Controls.Add(this._item);
            }

            
            this._item.BringToFront();

        }

        private void setTitle()
        {
            switch (this._formGroupType)
            {
                case (int)EnumFormGroupType.UsualGroup:
                    ((GroupBox)this._item).Text = this._title;
                    break;
                case (int)EnumFormGroupType.Page:
                    ((TabPage)this._item).Text = this._title;
                    break;
                case (int)EnumFormGroupType.Pages:
                    break;
                default:
                    ((GroupBox)this._item).Text = this._title;
                    break;
            }
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
            return string.Empty;
        }

        [ContextProperty("Высота", "Height")]
        public int Height
        {
            get { return _item.Height; }
            set { _item.Height = value; }
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
            get { return _item.Dock.GetHashCode() ; }
            set {
                _item.Dock = (DockStyle)value;
            }
        }

    }
}
