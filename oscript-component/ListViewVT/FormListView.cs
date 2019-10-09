using ScriptEngine.HostedScript.Library;
using ScriptEngine.HostedScript.Library.ValueTable;
using ScriptEngine.Machine;
using ScriptEngine.Machine.Contexts;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Windows.Forms;


namespace oscriptGUI.ListViewVT
{
    [ContextClass("ПредставлениеСписка", "ListView")]
    public class FormListView : AutoContext<FormListView>, IFormElement
    {

        private IRuntimeContextInstance _scriptDblClick;
        private string _methodNameDblClick;

        private IRuntimeContextInstance _scriptOnChoice;
        private string _methodOnChoice;

        private Panel _panelMainContainer;
        private Panel _panelTitleContainer;
        private Panel _panelControlContainer;
        private Label _label;
        private Control _item;

        private string _name;
        private bool _visible;
        private bool _enabled;
        private string _title;
        private IElementsContainer _parent;
        private Control _parentControl;
        private int _titleLocation;
        private IRuntimeContextInstance _thisScript;
        private string _methodName;

        private int _columnWithImageIndex;
        private FormListViewImagePack _smallImages;
        private FormListViewImagePack _largeImages;


        private BindingSource _bindingSource;
        private DataTableProvider _dataTable;
        private ArrayImpl _columns = new ArrayImpl();

        private FormListViewView _viewStyle;

        private IRuntimeContextInstance _scriptOnKeyDown;
        private string _methodOnKeyDown;

        private int _keyCodeDown;
        private bool _AltDown = false;
        private bool _CtrlDown = false;
        private bool _ShiftDown = false;


        public FormListView(Control parentCntrl)
        {
            this._name = "";
            this._visible = true;
            this._enabled = true;
            this._title = "";
            this._parent = null;
            this._parentControl = parentCntrl;

            this._methodName = "";
            this._thisScript = null;

            this._methodNameDblClick = "";
            this._scriptDblClick = null;

            this._methodOnChoice = "";
            this._scriptOnChoice = null;

            this._methodOnKeyDown = "";
            this._scriptOnKeyDown = null;


            _bindingSource = new BindingSource();
            _dataTable = new DataTableProvider();
            _item = new ListView();
            _viewStyle = new FormListViewView();

            ((ListView)_item).View = (View)1;

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

            this._parentControl.Controls.Add(_panelMainContainer);
            _panelMainContainer.BringToFront();

            this.createFormFieldByType();

            _columnWithImageIndex = 0;
            _smallImages = new FormListViewImagePack();
            _largeImages = new FormListViewImagePack();

            ((ListView)_item).SmallImageList = _smallImages.list();

            ((ListView)_item).StateImageList = _smallImages.list();

            ((ListView)_item).LargeImageList = _largeImages.list();
        }

        public override string ToString()
        {
            return "ПредставлениеСписка";
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

        private void createFormFieldByType()
        {
            ((ListView)this._item).MinimumSize = new Size(100, 100);
            ((ListView)this._item).AutoSize = true;
            ((ListView)this._item).Dock = DockStyle.Fill;
            
            _panelControlContainer.Controls.Add(this._item);
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

        #region WorkWithActions

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

            ReflectorContext reflector = new ReflectorContext();
            reflector.CallMethod(script, method, null);
        }

        private void FormFieldDblClick(object sender, EventArgs e)
        {
            runAction(this._scriptDblClick, this._methodNameDblClick);
        }


        private void FormFieldOnChoice(object sender, KeyPressEventArgs e)
        {

            if (e.KeyChar == (char)Keys.Enter)
            {
                runAction(this._scriptOnChoice, this._methodOnChoice);
            }
        }

        void FormFieldOnKeyDown(object sender, KeyEventArgs e)
        {
            _keyCodeDown = (int)e.KeyCode;

            _AltDown = e.Alt;
            _CtrlDown = e.Control;
            _ShiftDown = e.Shift;

            runAction(this._scriptOnKeyDown, this._methodOnKeyDown);
        }


        /// <summary>
        /// Установить обработчик события
        /// Возможные события:
        /// - ПриВыборе - При нажатии Enter
        /// - ПриДвойномКлике - Обработка двойного клика
        /// </summary>
        /// <param name="contex">Ссылка на скрипт в котором находится обработчик события</param>
        /// <param name="eventName">Имя обрабатываемого события.</param>
        /// <param name="methodName">Имя метода обработчика события</param>
        /// <example>
        /// Поле1.УстановитьДействие(ЭтотОбъект, "ПриДвойномКлике", "ПриДвойномКлике");
        /// Поле1.УстановитьДействие(ЭтотОбъект, "ПриВыборе", "ПриВыборе");
        /// </example>
        [ContextMethod("УстановитьДействие", "SetAction")]
        public void setAction(IRuntimeContextInstance contex, string eventName, string methodName)
        {
            if (eventName == "ПриДвойномКлике")
            {
                ((ListView)_item).DoubleClick -= FormFieldDblClick;
                ((ListView)_item).DoubleClick += FormFieldDblClick;

                this._scriptDblClick = contex;
                this._methodNameDblClick = methodName;

            }
            else if (eventName == "ПриВыборе")
            {
                (_item).KeyPress -= FormFieldOnChoice;
                (_item).KeyPress += FormFieldOnChoice;

                this._scriptOnChoice = contex;
                this._methodOnChoice = methodName;
            }
            else if (eventName == "ПриНажатииНаКлавишу")
            {
                (_item).KeyDown -= FormFieldOnKeyDown;
                (_item).KeyDown += FormFieldOnKeyDown;

                this._scriptOnKeyDown = contex;
                this._methodOnKeyDown = methodName;

            }

        }

        /// <summary>
        /// Получает имя установленного обработчика события.
        /// </summary>
        /// <param name="eventName">Имя события</param>
        /// <returns>Имя метода обработчика события</returns>
        /// <example>
        /// Список.УстановитьДействие(ЭтотОбъект, "ПриВыборе", "ПриВыбореСтроки");
        /// Список.ПолучитьДействие("ПриВыборе");
        /// // вернет: "ПриВыбореСтроки"
        /// </example>
        [ContextMethod("ПолучитьДействие", "GetAction")]
        public string GetAction(string eventName)
        {
            if (eventName == "ПриДвойномКлике")
            {
                return "" + this._scriptDblClick.ToString() + ":" + this._methodNameDblClick;
            }
            else if (eventName == "ПриВыборе")
            {
                return "" + this._scriptOnChoice.ToString() + ":" + this._methodOnChoice;
            }
            else if (eventName == "ПриНажатииНаКлавишу")
            {
                return "" + this._scriptOnKeyDown.ToString() + ":" + this._methodOnKeyDown;
            }
            return "";
        }
        #endregion

        #region ОбщиеМетодыСвойства
        /// <summary>
        /// Имя элемента
        /// </summary>
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

        /// <summary>
        /// Управление видимостью
        /// </summary>
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

        /// <summary>
        /// Управление доступностью
        /// </summary>
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

        /// <summary>
        /// Заголовок к полю. Пустая строка означает автоматическое определение. Для отключения вывода заголовка следует установить свойство ПоложениеЗаголовка в значение Нет.
        /// </summary>
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
                    this._label.Text = this.Name;
                }
            }
        }

        /// <summary>
        /// Определяет положение заголовка относительно поля в макете формы. 
        /// Следует заметить, что для отключения вывода заголовка следует установить это свойство в значение Нет. 
        /// Свойство Заголовок, содержащее пустую строку, означает автоматическое определение заголовка, а не ее отключение.
        /// </summary>
        /// <value>ПоложениеЗаголовкаЭлементаФормы</value>
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
        /// Высота
        /// </summary>
        [ContextProperty("Высота", "Height")]
        public int Height
        {
            get { return _item.Height; }
            set
            {
                _panelMainContainer.Height = value;
            }
        }

        /// <summary>
        /// Ширина
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
            get { return _panelMainContainer.Dock.GetHashCode(); }
            set
            {
                _panelMainContainer.Dock = (DockStyle)value;
            }
        }

        #endregion


        #region СпециальныеМетодыСвойства

        private void setData()
        {

            ((ListView)_item).BeginUpdate();

            ((ListView)_item).Clear();

            var data = _dataTable.getData();

            // Создадим колонки
            var cols = data.Columns;
            foreach (DataColumn col in cols)
            {
                ((ListView)_item).Columns.Add(col.Caption, col.MaxLength);
            }

            foreach (DataRow row in data.Rows)
            {
                ListViewItem item = new ListViewItem(row[0].ToString());
                int imageColIndex = Convert.ToInt32(_columnWithImageIndex);
                if (imageColIndex != -1)
                {
                    item.ImageIndex = Convert.ToInt32(row[imageColIndex]);
                }
                for (int i = 1; i < data.Columns.Count; i++)
                {
                    item.SubItems.Add(row[i].ToString());
                }
                ((ListView)_item).Items.Add(item);
            }
            ((ListView)_item).EndUpdate();
        }

        private void getListViewColumns()
        {
            var cols = ((ListView)_item).Columns;

            foreach (ColumnHeader col in cols)
            {
                _columns.Add(new ListViewColumn(((ListView)_item), _columns.Count()));
            }

        }

        /// <summary>
        /// Провайдер с данными.
        /// </summary>
        [ContextProperty("ПутьКДанным", "DataPath")]
        public DataTableProvider DataPath
        {
            get { return _dataTable; }
            set
            {
                _dataTable = value;
                setData();
                getListViewColumns();
            }
        }

        /// <summary>
        /// Содержит массив номеров выделенных строк.
        /// </summary>
        /// <value>ArrayImpl</value>
        [ContextProperty("ВыделенныеСтроки", "SelectedRows")]
        public ArrayImpl SelectedRows
        {
            get
            {
                ArrayImpl list1 = new ArrayImpl();

                var dg = ((ListView)_item);
                var sr = dg.SelectedItems;
                foreach (ListViewItem el in sr)
                {
                    list1.Add(ValueFactory.Create(el.Index));
                }

                return list1;
            }
        }

        /// <summary>
        /// Номер текущей строки таблицы.
        /// </summary>
        [ContextProperty("ТекущаяСтрока", "CurrentRow")]
        public int CurrentRow
        {
            get {
                
                return ((ListView)_item).FocusedItem.Index;
            }
            set
            {
                ((ListView)_item).Items[value].Selected = true;
            }
        }

        ///// <summary>
        ///// Представляет доступ к текущим данным (данным текущей строки).
        ///// </summary>
        //[ContextProperty("ТекущиеДанные", "CurrentData")]
        //public ArrayImpl CurrentData
        //{
        //    get
        //    {
        //        throw new RuntimeException("Не реализовано");
                
        //        //ValueTable tbl = _dataTable.Source;
        //        //return tbl.Get(((ListView)_item).Items);
        //    }
        //}

        /// <summary>
        /// Обновляет данные в таблице.
        /// </summary>
        [ContextMethod("Обновить", "Refresh")]
        public void Refresh()
        {
            _dataTable.Refresh();
        }

        /// <summary>
        /// Отображать список без изображений
        /// </summary>
        [ContextMethod("БезИзображений", "WithoutImages")]
        public void WithoutImages()
        {
            ((ListView)_item).SmallImageList = null;
            ((ListView)_item).LargeImageList = null;
            ((ListView)_item).StateImageList = null;
        }

        /// <summary>
        /// Колонки таблицы.
        /// </summary>
        /// <value>Массив КолонкаПредставленияСписка</value>
        [ContextProperty("Колонки", "Columns")]
        public ArrayImpl Columns
        {
            get { return _columns; }
        }

        /// <summary>
        /// Представление
        /// </summary>
        /// <value>Представление</value>
        [ContextProperty("Представление", "View")]
        public int View
        {
            get { return (int)((ListView)_item).View; }
            set { ((ListView)_item).View = (View)value; }
        }

        /// <summary>
        /// Отображать чекбокс
        /// </summary>
        [ContextProperty("Пометки", "CheckBoxes")]
        public bool CheckBoxes
        {
            get { return ((ListView)_item).CheckBoxes; }
            set { ((ListView)_item).CheckBoxes = value; }
        }

        /// <summary>
        /// Номера строк помеченных элементов
        /// </summary>
        [ContextProperty("НомераПомеченныхЭлементов", "CheckedIndices")]
        public ArrayImpl CheckedIndices
        {
            get {
                ArrayImpl list1 = new ArrayImpl();

                var dg = ((ListView)_item);
                var sr = dg.CheckedItems;
                foreach (ListViewItem el in sr)
                {
                    list1.Add(ValueFactory.Create(el.Index));
                }

                return list1;
            }
        }

        //TODO: CheckedItems

        /// <summary>
        /// Выделенная строка
        /// </summary>
        [ContextProperty("ВыделеннаяСтрока")]
        public ValueTableRow FocusedItem
        {
            get
            {
                var dg = ((ListView)_item);
                ValueTable tbl = _dataTable.Source;
                return tbl.Get(dg.FocusedItem.Index);
            }
        }

        /// <summary>
        /// Отображать сетку (в режиме таблицы)
        /// </summary>
        [ContextProperty("ОтображатьСетку")]
        public bool GridLines
        {
            get { return ((ListView)_item).GridLines; }
            set { ((ListView)_item).GridLines = value; }
        }

        //TODO: Groups

        /// <summary>
        /// Не отображать выделенный при потере фокуса
        /// </summary>
        [ContextProperty("СкрыватьВыделенный")]
        public bool HideSelection
        {
            get { return ((ListView)_item).HideSelection; }
            set { ((ListView)_item).HideSelection = value; }
        }

        //TODO: HeaderStyle
        //TODO: HotTracking
        //TODO: HoverSelection
        //TODO: LabelEdit
        //TODO: LabelWrap

        /// <summary>
        /// Выделять всю строку
        /// </summary>
        [ContextProperty("ВыделятьВсюСтроку")]
        public bool FullRowSelect
        {
            get { return ((ListView)_item).FullRowSelect; }
            set { ((ListView)_item).FullRowSelect = value; }
        }


        /// <summary>
        /// Доступно множественное выделение
        /// </summary>
        [ContextProperty("МножественноеВыделение")]
        public bool MultiSelect
        {
            get { return ((ListView)_item).MultiSelect; }
            set { ((ListView)_item).MultiSelect = value; }
        }

        //TODO: ShowGroups
        //TODO: ShowItemToolTips
        //TODO: StateImageList
        //TODO: TileSize
        //TODO: TopItem

        /// <summary>
        /// Виды представлений:
        /// - Таблица
        /// - БольшиеЗначки
        /// - МаленькиеЗначки
        /// - Список
        /// - Плитка
        /// </summary>
        [ContextProperty("ВидыПредставлений")]
        public FormListViewView ViewStyles
        {
            get
            {
                return _viewStyle;
            }
        }

        /// <summary>
        /// Меленькие изображения
        /// </summary>
        [ContextProperty("МаленькиеИзображения")]
        public FormListViewImagePack SmallImages
        {
            get
            {
                return _smallImages;
            }
            set
            {
                ((ListView)_item).SmallImageList = value.list();
            }
        }

        /// <summary>
        /// Большие изображения
        /// </summary>
        [ContextProperty("БольшиеИзображения")]
        public FormListViewImagePack LargeImages
        {
            get
            {
                return _largeImages;
            }
            set
            {
                ((ListView)_item).LargeImageList = value.list();
            }
        }


        /// <summary>
        /// Номер колонки с индексом изображения. Если ничего не задано - 0
        /// </summary>
        [ContextProperty("КолонкаСИндексомИзображения")]
        public int ColumnWithImageIndex
        {
            get
            {
                return _columnWithImageIndex;
            }
            set
            {
                _columnWithImageIndex = value;
            }
        }


        /// <summary>
        /// Код нажатой клавиши
        /// </summary>
        [ContextProperty("КодНажатойКлавиши", "KeyCodeDown")]
        public int KeyCodeDown
        {
            get { return _keyCodeDown; }
        }

        /// <summary>
        /// Нажат альт
        /// </summary>
        [ContextProperty("НажатАльт", "AltDown")]
        public bool AltDown
        {
            get { return _AltDown; }
        }

        /// <summary>
        /// Нажат контрол
        /// </summary>
        [ContextProperty("НажатКонтрол", "CtrlDown")]
        public bool CtrlDown
        {
            get { return _CtrlDown; }
        }

        /// <summary>
        /// Нажат шифт
        /// </summary>
        [ContextProperty("НажатШифт", "ShiftDown")]
        public bool ShiftDown
        {
            get { return _ShiftDown; }
        }

        #endregion
    }
}
