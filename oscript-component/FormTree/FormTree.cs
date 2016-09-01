using ScriptEngine.HostedScript.Library;
using ScriptEngine.HostedScript.Library.ValueTree;
using ScriptEngine.Machine;
using ScriptEngine.Machine.Contexts;
using System;
using System.Drawing;
using System.Windows.Forms;
using TreeViewColumnsProject;

namespace oscriptGUI
{
    [ContextClass("ДеревоФормы", "FormTree")]
    class FormTree: AutoContext<FormTree>, IFormElement
    {
        private Panel _panelMainContainer;
        private Panel _panelTitleContainer;
        private Panel _panelControlContainer;
        private Label _label;
        private TreeViewColumns _item;

        private string _name;
        private bool _visible;
        private bool _enabled;
        private string _title;
        private IElementsContainer _parent;
        private Control _parentControl;
        private int _titleLocation;
        private IRuntimeContextInstance _thisScript;
        private string _methodName;

        private IRuntimeContextInstance _thisScriptDblClick;
        private string _methodNameDblClick;

        private IRuntimeContextInstance _scriptOnChoice;
        private string _methodOnChoice;

        //private BindingSource _bindingSource;
        private DataTableProvider _dataTable;
        private ArrayImpl _columns = new ArrayImpl();

        public FormTree(Control parentCntrl)
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
            this._thisScriptDblClick = null;

            this._methodOnChoice = "";
            this._scriptOnChoice = null;

            _dataTable = new DataTableProvider();
            _item = new TreeViewColumns();

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

        }

        public override string ToString()
        {
            return "ДеревоФормы";
        }

        /// <summary>
        /// Возвращает основной контейнер элемента (Panel на котором лежит сам контрол и декорация к нему)
        /// </summary>
        /// <returns>Control - Panel</returns>
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
            _item.MinimumSize = new Size(100, 100);

            _item.AutoSize = true;
            _item.Dock = DockStyle.Fill;
            _item.TreeView.HideSelection = false;
            _item.TreeView.DrawMode = TreeViewDrawMode.OwnerDrawAll;
            _item.TreeView.FullRowSelect = false;

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

        //# Блок по работе с событиями

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

        private void FormFieldValueChanged(object sender, EventArgs e)
        {
            runAction(this._thisScript, this._methodName);
        }

        private void FormFieldDblClick(object sender, EventArgs e)
        {
            runAction(this._thisScriptDblClick, this._methodNameDblClick);
        }


        private void FormFieldOnChoice(object sender, KeyPressEventArgs e)
        {

            if (e.KeyChar == (char)Keys.Enter)
            {
                runAction(this._scriptOnChoice, this._methodOnChoice);
            }
        }

        /// <summary>
        /// Установить обработчик события
        /// Возможные события:
        /// - ПриВыбореСтроки - Обработка события изменения значения
        /// - ПриВыборе - При нажатии Enter
        /// - ПриДвойномКлике - Обработка двойного клика
        /// </summary>
        /// <param name="contex">Ссылка на скрипт в котором находится обработчик события</param>
        /// <param name="eventName">Имя обрабатываемого события.</param>
        /// <param name="methodName">Имя метода обработчика события</param>
        /// <example>
        /// Поле1.УстановитьДействие(ЭтотОбъект, "ПриВыбореСтроки", "ПриВыбореСтроки");
        /// Поле1.УстановитьДействие(ЭтотОбъект, "ПриДвойномКлике", "ПриДвойномКлике");
        /// Поле1.УстановитьДействие(ЭтотОбъект, "ПриВыборе", "ПриВыборе");
        /// </example>
        [ContextMethod("УстановитьДействие", "SetAction")]
        public void setAction(IRuntimeContextInstance contex, string eventName, string methodName)
        {
            if (eventName == "ПриВыбореСтроки")
            {

                this._thisScript = contex;
                this._methodName = methodName;

                _item.TreeView.AfterSelect -= FormFieldValueChanged;
                _item.TreeView.AfterSelect += FormFieldValueChanged;

            }
            else if (eventName == "ПриДвойномКлике")
            {
                _item.TreeView.DoubleClick -= FormFieldDblClick;
                _item.TreeView.DoubleClick += FormFieldDblClick;

                this._thisScriptDblClick = contex;
                this._methodNameDblClick = methodName;

            }
            else if (eventName == "ПриВыборе")
            {
                _item.TreeView.KeyPress -= FormFieldOnChoice;
                _item.TreeView.KeyPress += FormFieldOnChoice;

                this._scriptOnChoice = contex;
                this._methodOnChoice = methodName;
            }

        }

        /// <summary>
        /// Получает имя установленного обработчика события.
        /// </summary>
        /// <param name="eventName">Имя события</param>
        /// <returns>Имя метода обработчика события</returns>
        /// <example>
        /// Форма.УстановитьДействие(ЭтотОбъект, "ПриВыборе", "ПриВыбореЯчейки");
        /// Форма.ПолучитьДействие("ПриВыборе");
        /// // вернет: "ПриВыбореЯчейки"
        /// </example>
        [ContextMethod("ПолучитьДействие", "GetAction")]
        public string GetAction(string eventName)
        {
            if (eventName == "ПриВыбореСтроки")
            {
                return "" + this._thisScript.ToString() + ":" + this._methodName;
            }
            else if (eventName == "ПриДвойномКлике")
            {
                return "" + this._thisScriptDblClick.ToString() + ":" + this._methodNameDblClick;
            }
            else if (eventName == "ПриВыборе")
            {
                return "" + this._scriptOnChoice.ToString() + ":" + this._methodOnChoice;
            }
            return "";
            //return "GetAction: Action not supported - " + eventName;
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
            get { return _item.Dock.GetHashCode(); }
            set
            {
                _panelMainContainer.Dock = (DockStyle)value;
            }
        }

        private void addRow(ValueTreeRowCollection VTreeRowsCol, TreeNode parentNode)
        {
            foreach (ValueTreeRow VTRow in VTreeRowsCol)
            {
                
                string[] strData = new string[(int)_dataTable.SourceTree.Columns.Count()];

                int i = -1;
                for (int col = 1; col < _dataTable.SourceTree.Columns.Count(); col++)
                {
                    i++;
                    strData[i] = VTRow.Get(col).ToString();
                }

                TreeNode treeNode = new TreeNode(VTRow.Get(0).ToString());

                treeNode.Tag = strData;
                if (parentNode == null)
                {
                    _item.TreeView.Nodes.Add(treeNode);
                }
                else
                {
                    parentNode.Nodes.Add(treeNode);
                }
                addRow(VTRow.Rows, treeNode);

            }

        }

        private void setData()
        {
            ValueTree ValTree = _dataTable.SourceTree;

            _item.Columns.Clear();
            _item.TreeView.CheckBoxes = false;
            _item.TreeView.Nodes.Clear();

            foreach (ValueTreeColumn VTCol in ValTree.Columns)
            {
                _item.Columns.Add(VTCol.Name);
            }

            addRow(ValTree.Rows, null);

        }

        private void getColumns()
        {
            var cols = _item.Columns;

            foreach (ColumnHeader col in cols)
            {
                _columns.Add(new FormTreeColumn(_item, col.Index));
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
                getColumns();
            }
        }


        /// <summary>
        /// Номер текущей строки таблицы.
        /// </summary>
        [ContextProperty("ТекущаяСтрока", "CurrentRow")]
        public int CurrentRow
        {
            get {
                if (_item.TreeView.SelectedNode == null)
                {
                    return 0;
                }
                return _item.TreeView.SelectedNode.Index;
            }
            set {
                _item.TreeView.SelectedNode = _item.TreeView.Nodes[value];
            }
        }

        /// <summary>
        /// Представляет доступ к текущим данным (данным текущей строки).
        /// Возвращает соответствие с текстовым представлением данных.
        /// </summary>
        /// <value>Соответствие</value>
        [ContextProperty("ТекущиеДанные", "CurrentData")]
        public MapImpl CurrentData
        {
            get
            {
                MapImpl data = new MapImpl();

                if (_item.TreeView.SelectedNode == null)
                {
                    //Console.WriteLine("empty value");
                    return data;
                }

                data.Insert(ValueFactory.Create(((FormTreeColumn)Columns.Get(0)).Title), ValueFactory.Create(_item.TreeView.SelectedNode.Text));

                string[] vals = (string[])_item.TreeView.SelectedNode.Tag;

                for (int col=1; col < Columns.Count(); col++)
                {
                    data.Insert(ValueFactory.Create(((FormTreeColumn)Columns.Get(col)).Title), ValueFactory.Create(vals[col - 1]));
                }

                return data;
            }
        }

        /// <summary>
        /// Обновляет данные в таблице.
        /// </summary>
        [ContextMethod("Обновить", "Refresh")]
        public void Refresh()
        {
            setData();
            //_dataTable.Refresh();
            //DataPath = _dataTable;
        }

        /// <summary>
        /// Колонки таблицы.
        /// </summary>
        /// <value>Массив <see cref="FormTreeColumn"/></value>
        [ContextProperty("Колонки", "Columns")]
        public ArrayImpl Columns
        {
            get { return _columns; }
        }

    }
}
