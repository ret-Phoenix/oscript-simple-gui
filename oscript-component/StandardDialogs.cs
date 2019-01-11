/*
 * Создано в Visual Studio.
 * Пользователь: alexkmbk
 * Дата: 10.09.2016
 * 
 */

using ScriptEngine.Machine.Contexts;
using System.Windows.Forms;
using System;
using ScriptEngine.Machine;
using System.Drawing;
using System.Collections.Generic;
using System.Globalization;

namespace oscriptGUI
{
    //[GlobalContext(Category = "Процедуры и функции интерактивной работы")]
    [ContextClass("СтандартныеДиалоги", "StandardDialogs")]
    public class StandardDialogs : AutoContext<StandardDialogs> //GlobalContextBase<StandardDialogs>
    {
        /// <summary>
        /// Отображает диалог с предупреждением.
        /// </summary>
        /// <param name="messageText">Текст предупреждения.</param>
        /// <param name="timeOut">Таймаут.</param>
        /// <param name="title">Заголовок.</param>
        [ContextMethod("Предупреждение", "DoMessageBox")]
        public void DoMessageBox(string messageText, int timeOut = 0, string title = "1Script")
        {
            MessageBoxWithTimeOut.Show(messageText, timeOut, title);
        }

        /// <summary>
        /// Отображает диалог с предупреждением.
        /// </summary>
        /// <param name="queryText">Текст задаваемого вопроса.</param>
        /// <param name="buttons">Состав и текст кнопок.</param>
        /// <param name="timeOut">Таймаут.</param>
        /// <param name="defaultButton">Кнопка по умолчанию.</param>
        /// <param name="title">Заголовок.</param>
        /// <param name="timeOutButton">Кнопка таймаута.</param>
        [ContextMethod("Вопрос", "DoQueryBox")]
        public IValue DoQueryBox(string queryText, CLREnumValueWrapper<InnerQuestionDialogModeEnum> buttons, int timeOut = 0, CLREnumValueWrapper<InnerDialogReturnCodeEnum> defaultButton = null, string title = "1Script", CLREnumValueWrapper<InnerDialogReturnCodeEnum>  timeOutButton = null)
        {
            // почему-то значение по умолчанию title = "1Script" здесь не срабатывает
            if (string.IsNullOrEmpty(title))
            {
                title = "1Script";
            }

            return QueryBoxWithTimeOut.Show(queryText, buttons, timeOut, defaultButton, title, timeOutButton);
            }

        //public static IAttachableContext CreateInstance()
        //{
        //    return new StandardDialogs();
        //}

        /// <summary>
        /// 
        /// Вызывает диалог для ввода числа.
        /// </summary>
        ///
        /// <param name="Number">
        /// Имя доступной в модуле переменной. В эту переменную будет помещено введенное число. Начальное значение переменной будет использовано в качестве начального значения в диалоге. </param>
        /// <param name="Tooltip">
        /// Текст заголовка окна диалога ввода числа. Может использоваться в качестве подсказки пользователю.
        /// Значение по умолчанию: Пустая строка. </param>
        /// <param name="Length">
        /// Длина вводимого числа включая дробную часть (символы разделителей не учитываются).
        /// Значение по умолчанию: 0. </param>
        /// <param name="Precision">
        /// Количество знаков в дробной части вводимого числа.
        /// Значение по умолчанию: 0. </param>
        ///
        /// <returns name="Boolean">
        /// Значения данного типа имеют два значения Истина и Ложь, задаваемых соответствующими литералами. 
        /// Значения данного типа возвращаются в качестве результата вычисления логических выражений.</returns>
        ///
        [ContextMethod("ВвестиЧисло", "InputNumber")]
        public bool InputNumber([ByRef] IVariable Number, string Tooltip = null, int Length = 0, int Precision = 0)
        {
            return InputNumberDialog.Show(Number, Tooltip, Length, Precision);
        }

        /// <summary>
        /// Вызывает диалог для ввода строки.
        /// </summary>
        ///
        /// <param name="String">
        /// Доступная в модуле переменная. В эту переменную будет помещена введенная в диалоге строка. Начальное значение переменной будет использовано в качестве начального значения в диалоге. </param>
        /// <param name="Tooltip">
        /// Текст заголовка окна диалога ввода строки. Может использоваться в качестве подсказки пользователю.
        /// Значение по умолчанию: Пустая строка. </param>
        /// <param name="Length">
        /// Длина вводимой строки. Если параметр не указан, то строка неограниченной длины.
        /// Значение по умолчанию: 0. </param>
        /// <param name="Multiline">
        /// Определяет режим ввода многострочного текста: Истина - ввод многострочного текста с разделителями строк; Ложь - ввод простой строки.
        /// Значение по умолчанию: Ложь. </param>
        /// <returns name="Boolean">
        /// Значения данного типа имеют два значения Истина и Ложь, задаваемых соответствующими литералами. 
        /// Значения данного типа возвращаются в качестве результата вычисления логических выражений.</returns>
        ///
        [ContextMethod("ВвестиСтроку", "InputString")]
        public bool InputString([ByRef] IVariable ValueString, string Tooltip = null, int Length = 0, bool Multiline = false)
        {
            return InputStringDialog.Show(ValueString, Tooltip, Length, Multiline);
        }

    }

    // Класс для отображения окна диалога с возможностью указания таймаута
    //
    public class MessageBoxWithTimeOut
    {
        System.Threading.Timer _timeoutTimer;
        Form _form;
        Button _okButton;
        Label _msgLabel;

        MessageBoxWithTimeOut(string messageText, int timeOut, string title)
        {
            _form = new Form();

            _form.Height = 140;
            _form.Width = 200;
            _form.Text = title;
            _form.StartPosition = FormStartPosition.CenterScreen;
            _form.MinimumSize = new Size(200, 140);
            _form.MaximizeBox = false;
            _form.MinimizeBox = false;

            // Label
            _msgLabel = new Label();
            _msgLabel.Text = messageText;
            _msgLabel.Padding = new Padding(20, 20, 20, 20);
            _msgLabel.TextAlign = ContentAlignment.MiddleCenter;
            _msgLabel.AutoSize = true;

            Panel panel = new Panel();
            panel.Dock = DockStyle.Top;
            panel.AutoSize = true;
            panel.Controls.Add(_msgLabel);
            _form.Controls.Add(panel);
            panel.BringToFront();

            // Button
            _okButton = new Button();
            _okButton.Click += OKClick;
            _okButton.Text = "OK";
            _okButton.MinimumSize = new Size(100, 21);
            _okButton.AutoSize = true;
            _okButton.Anchor = AnchorStyles.None;
            _okButton.Left = (_form.ClientSize.Width - _okButton.Width) / 2;
            _okButton.Margin = new Padding(10, 20, 10, 20);

            panel = new Panel();
            panel.Height = 20;
            panel.Dock = DockStyle.Bottom;
            panel.AutoSize = true;
            panel.Controls.Add(_okButton);
            _form.Controls.Add(panel);
            panel.BringToFront();

            using (Graphics g = _form.CreateGraphics())
            {
                SizeF size = g.MeasureString(messageText, _msgLabel.Font);
                _form.Width = (int)Math.Ceiling(size.Width) + _msgLabel.Padding.Horizontal;
            }

            if (timeOut != 0)
            {
                _timeoutTimer = new System.Threading.Timer(OnTimerElapsed,
            null, timeOut * 1000, System.Threading.Timeout.Infinite);
            }
            using (_timeoutTimer)
                _form.ShowDialog(null);
        }

        public static void Show(string messageText, int timeOut, string title)
        {
            new MessageBoxWithTimeOut(messageText, timeOut, title);
        }

        void OnTimerElapsed(object state)
        {
            _form.BeginInvoke(new Action(()=>_form.Close()));
            _timeoutTimer.Dispose();
        }

        public void OKClick(object sender, EventArgs e)
        {
            _form.Close();
        }
    }

    // Класс для отображения окна диалога с вопросом с возможностью указания таймаута
    //
    public class QueryBoxWithTimeOut
    {
        static int BUTTON_MIN_WIDTH = 100;
        static int BUTTON_PADDING = 5;

        private System.Threading.Timer _timeoutTimer;
        private System.Threading.Timer _tickTimer;
        private int _timeOut;
        private int _timeCounter;

        private Form _form;
        private Label _queryLabel;
        private Dictionary<string, InnerDialogReturnCodeEnum> _formButtons;
        private InnerDialogReturnCodeEnum _defaultButton;
        private InnerDialogReturnCodeEnum _timeOutButton;
        private TableLayoutPanel _buttonsPanel;

        public IValue answer; // значение переменной устанавливается после закрытия диалога

         QueryBoxWithTimeOut(string queryText, CLREnumValueWrapper<InnerQuestionDialogModeEnum> buttons, int timeOut, CLREnumValueWrapper<InnerDialogReturnCodeEnum> defaultButton, string title, CLREnumValueWrapper<InnerDialogReturnCodeEnum> timeOutButton)
        {
            _timeOut = timeOut;
            _timeCounter = _timeOut;

           _form = new Form();

            _form.Height = 140;
            _form.Width = 200;
            _form.Text = title;
            _form.StartPosition = FormStartPosition.CenterScreen;
            _form.MinimumSize = new Size(200, 140);
            _form.MaximizeBox = false;
            _form.MinimizeBox = false;
            _form.ControlBox = false;
            _form.FormBorderStyle = FormBorderStyle.FixedDialog;

            TableLayoutPanel CommonPanel = new TableLayoutPanel();
            CommonPanel.ColumnCount = 1;
            CommonPanel.RowCount = 2;
            CommonPanel.AutoSize = true;
            _form.Controls.Add(CommonPanel);

            // Label
            _queryLabel = new Label();
            _queryLabel.Text = queryText;
            _queryLabel.Padding = new Padding(10, 10, 10, 10);
            _queryLabel.TextAlign = ContentAlignment.MiddleCenter;
            _queryLabel.AutoSize = true;

            Panel panel = new Panel();
            panel.Dock = DockStyle.Top;
            panel.AutoSize = true;
            panel.Controls.Add(_queryLabel);
            CommonPanel.Controls.Add(panel,0,0);
            panel.BringToFront();
            panel.Anchor = AnchorStyles.None;

            // Buttons

            _formButtons = new Dictionary<string, InnerDialogReturnCodeEnum>();

            _buttonsPanel = new TableLayoutPanel();

            _buttonsPanel.RowStyles.Clear();
            _buttonsPanel.ColumnStyles.Clear();

            _buttonsPanel.Height = 20;
            _buttonsPanel.Padding = new Padding(20,0,20,0);
            _buttonsPanel.Dock = DockStyle.Bottom;
            _buttonsPanel.AutoSize = true;
            _buttonsPanel.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            _buttonsPanel.Anchor = AnchorStyles.None;
            CommonPanel.Controls.Add(_buttonsPanel,0,1);
            _buttonsPanel.BringToFront();

            _buttonsPanel.RowCount = 1;
            switch ((InnerQuestionDialogModeEnum)buttons.UnderlyingObject)
            {
                case InnerQuestionDialogModeEnum.YesNo:
                    AddButton(_buttonsPanel, "Yes", "Да", InnerDialogReturnCodeEnum.Yes);
                    AddButton(_buttonsPanel, "No", "Нет", InnerDialogReturnCodeEnum.No);
                    break;
                case InnerQuestionDialogModeEnum.YesNoCancel:
                    AddButton(_buttonsPanel, "Yes", "Да", InnerDialogReturnCodeEnum.Yes);
                    AddButton(_buttonsPanel, "No", "Нет", InnerDialogReturnCodeEnum.No);
                    AddButton(_buttonsPanel, "Cancel", "Отмена", InnerDialogReturnCodeEnum.Cancel);
                    break;
                case InnerQuestionDialogModeEnum.OK:
                    AddButton(_buttonsPanel, "OK", "ОК", InnerDialogReturnCodeEnum.OK);
                    break;
                case InnerQuestionDialogModeEnum.OKCancel:
                    AddButton(_buttonsPanel, "OK", "ОК", InnerDialogReturnCodeEnum.OK);
                    AddButton(_buttonsPanel, "Cancel", "Отмена", InnerDialogReturnCodeEnum.Cancel);
                    break;
                case InnerQuestionDialogModeEnum.RetryCancel:
                    AddButton(_buttonsPanel, "Retry", "Повторить", InnerDialogReturnCodeEnum.Retry);
                    AddButton(_buttonsPanel, "Cancel", "Отмена", InnerDialogReturnCodeEnum.Cancel);
                    break;
                case InnerQuestionDialogModeEnum.AbortRetryIgnore:
                    AddButton(_buttonsPanel, "Abort", "Прервать", InnerDialogReturnCodeEnum.Abort);
                    AddButton(_buttonsPanel, "Retry", "Повторить", InnerDialogReturnCodeEnum.Retry);
                    AddButton(_buttonsPanel, "Ignore", "Пропустить", InnerDialogReturnCodeEnum.Ignore);
                    break;
                default:
                    Console.WriteLine("Передано неизвестное значение типа РежимДиалогаВопрос");
                    return;
            }

            _buttonsPanel.ColumnCount = _buttonsPanel.Controls.Count;

            string timeOutButtonName = "";

            if (_buttonsPanel.Controls.Count > 0)
            {

                if (defaultButton == null)
                {
                    _defaultButton = _formButtons[_buttonsPanel.Controls[0].Name];
                }
                else _defaultButton = _formButtons[defaultButton.UnderlyingObject.ToString()];

                if (timeOutButton == null)
                {
                    _timeOutButton = _formButtons[_buttonsPanel.Controls[0].Name];
                    timeOutButtonName = _buttonsPanel.Controls[0].Text;
                }
                else {
                    _timeOutButton = _formButtons[timeOutButton.UnderlyingObject.ToString()];
                    timeOutButtonName = _buttonsPanel.Controls.Find(timeOutButton.UnderlyingObject.ToString(), false)[0].Text;

                }
            }

            using (Graphics g = _form.CreateGraphics())
            {
                SizeF size = g.MeasureString(queryText, _queryLabel.Font);
                // С механизмом выравнивания элементов надо что-то делать, слишком много химии и волшебных чисел
                _form.Width = Math.Max((int)Math.Ceiling(size.Width) + _queryLabel.Padding.Horizontal, _formButtons.Count * (BUTTON_MIN_WIDTH + BUTTON_PADDING*2) + (timeOut > 0 ? BUTTON_MIN_WIDTH/2 + 60: BUTTON_MIN_WIDTH / 2));
            }

            if (_timeOut != 0)
            {
                // Создадим два таймера,
                //один для срабатывания события окончания ожидания
                _timeoutTimer = new System.Threading.Timer(OnTimerElapsed,
            null, _timeOut * 1000, System.Threading.Timeout.Infinite);

                // и еще один, для посекундного оповещения об истичении времени
                _tickTimer = new System.Threading.Timer(OnTickTimer, timeOutButtonName, 0, 1000);
            }

            // Установим фокус на кнопку по умолчанию
            Control defaulButtonControl = _buttonsPanel.Controls.Find(_defaultButton.ToString(), false)[0];
            defaulButtonControl.Focus();
            defaulButtonControl.Select();

            using (_timeoutTimer)
                _form.ShowDialog(null);

        }

        private int AddButton(TableLayoutPanel panel, string name, string text, InnerDialogReturnCodeEnum ReturnCode)
        {
            Button button = new Button();
            button.Click += buttonClick;
            button.Text = text;
            button.Name = name;
            button.MinimumSize = new Size(BUTTON_MIN_WIDTH, 20);
            button.AutoSize = true;
            button.Anchor = AnchorStyles.None;
            button.Margin = new Padding(BUTTON_PADDING, 20, BUTTON_PADDING, 20);

            panel.Controls.Add(button, _formButtons.Count, 0);

            _formButtons.Add(name, ReturnCode);
            return button.Width + button.Margin.Horizontal;
        }

        private void buttonClick(object sender, EventArgs e)
        {
            answer = GlobalsManager.GetEnum<DialogReturnCodeEnum>()[((Button) sender).Name];
            _form.Close();
        }


        public static IValue Show(string queryText, CLREnumValueWrapper<InnerQuestionDialogModeEnum> buttons, int timeOut, CLREnumValueWrapper<InnerDialogReturnCodeEnum> defaultButton, string title, CLREnumValueWrapper<InnerDialogReturnCodeEnum> timeOutButton)
        {
            QueryBoxWithTimeOut queryBox = new QueryBoxWithTimeOut(queryText, buttons, timeOut, defaultButton, title, timeOutButton);
            return queryBox.answer;
        }

        void OnTimerElapsed(object state)
        {
            answer = GlobalsManager.GetEnum<DialogReturnCodeEnum>()[_defaultButton.ToString()];
            _form.Close();
            _timeoutTimer.Dispose();
        }

        void OnTickTimer(object state)
        {
            if (_timeCounter > 0)
                _timeCounter = _timeCounter - 1;
            Control[] foundControls = _buttonsPanel.Controls.Find(_timeOutButton.ToString(), false);
            if (foundControls.Length > 0)
            {
                Control timeOutButton = foundControls[0];
                if (_timeCounter != 0)
                    timeOutButton.Text = (string)state + " (Осталось " + _timeCounter + " сек.)";
                else
                    timeOutButton.Text = (string)state;
            }
        }

    }

    // Класс для отображения окна диалога для ввода номера
    //
    public class InputNumberDialog
    {
        Form _form;
        Button _okButton;
        Button _cancelButton;
        TextBox _ValueField;
        Label _Label;
        bool isOK;
        decimal ResultNumber;
        char NumberDecimalSeparator;

        InputNumberDialog(string Number, string Tooltip = null, int Length = 0, int Precision = 0)
        {

            // Gets a NumberFormatInfo associated with the en-US culture.
            NumberFormatInfo nfi = CultureInfo.CurrentCulture.NumberFormat;

            // Displays the same value with a blank as the separator.
            NumberDecimalSeparator = nfi.NumberDecimalSeparator[0];

            isOK = false;

            int formfWidth = 300;

            _form = new Form();

            _form.AutoSize = false;
            _form.Height = 200;
            if (Tooltip == null || Tooltip.Length == 0)
                _form.Text = "Please enter a number";
            else
                _form.Text = Tooltip;
            _form.StartPosition = FormStartPosition.CenterScreen;
            _form.MaximizeBox = false;
            _form.MinimizeBox = false;
            _form.FormBorderStyle = FormBorderStyle.FixedDialog;

            // Label
            _Label = new Label();
            _Label.Text = _form.Text;
            _Label.Padding = new Padding(20, 20, 20, 20);
            _Label.TextAlign = ContentAlignment.MiddleCenter;
            _Label.AutoSize = true;
            _Label.Font = new Font(FontFamily.GenericSansSerif, 14, FontStyle.Bold);

            using (Graphics g = _form.CreateGraphics())
            {
                SizeF size = g.MeasureString(_Label.Text, _Label.Font);
                formfWidth = Math.Max((int)Math.Ceiling(size.Width) + _Label.Padding.Horizontal, formfWidth);
                formfWidth = Math.Min(formfWidth, 600);
            }

            _form.Width = formfWidth;
            _form.MinimumSize = new Size(formfWidth, 200);

            TableLayoutPanel CommonPanel = new TableLayoutPanel();
            CommonPanel.ColumnCount = 1;
            CommonPanel.RowCount = 3;
            CommonPanel.AutoSize = true;
            _form.Controls.Add(CommonPanel);

            Panel panel = new Panel();
            panel.Dock = DockStyle.Top;
            panel.AutoSize = true;
            panel.Controls.Add(_Label);
            CommonPanel.Controls.Add(panel);
            panel.BringToFront();

            // OK Button
            _okButton = new Button();
            _okButton.Click += OKClick;
            _okButton.Text = "OK";
            _okButton.MinimumSize = new Size(100, 21);
            _okButton.AutoSize = true;
            _okButton.Anchor = AnchorStyles.None;
            _okButton.Left = (_form.Width - _okButton.Width * 2) / 2;
            _okButton.Margin = new Padding(10, 20, 10, 20);

            // Cancel Button
            _cancelButton = new Button();
            _cancelButton.Click += CancelClick;
            _cancelButton.Text = "Cancel";
            _cancelButton.MinimumSize = new Size(100, 21);
            _cancelButton.AutoSize = true;
            _cancelButton.Anchor = AnchorStyles.None;
            _cancelButton.Left = _okButton.Left + _cancelButton.Width + 5;
            _cancelButton.Margin = new Padding(10, 20, 10, 20);

            // Text field

            _ValueField = new TextBox();
            _ValueField.Text = Number;
            _ValueField.Width = formfWidth - 20;
            _ValueField.KeyPress += ValueField_KeyPress;

            panel = new Panel();
            panel.Height = 20;
            panel.Width = formfWidth;
            panel.Dock = DockStyle.Bottom;
            panel.AutoSize = true;
            panel.Controls.Add(_ValueField);
            panel.BringToFront();
            CommonPanel.Controls.Add(panel);


            panel = new Panel();
            panel.Height = 20;
            panel.Width = formfWidth;
            panel.Dock = DockStyle.Bottom;
            panel.AutoSize = true;
            panel.Controls.Add(_okButton);
            panel.Controls.Add(_cancelButton);
            panel.BringToFront();
            CommonPanel.Controls.Add(panel);


            _form.ShowDialog(null);
        }

        public static bool Show(IVariable Value, string Tooltip = null, int Length = 0, int Precision = 0)
        {
            InputNumberDialog dlg = new InputNumberDialog(Value.Value.AsString(), Tooltip, Length, Precision);
            Value.Value = ValueFactory.Create(dlg.ResultNumber);
            return dlg.isOK;
        }

        public void OKClick(object sender, EventArgs e)
        {
            isOK = true;
            ResultNumber = Convert.ToDecimal(_ValueField.Text);
            _form.Close();
        }
        public void CancelClick(object sender, EventArgs e)
        {
            isOK = false;
            _form.Close();
        }

        private void ValueField_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !IsOKForDecimalTextBox(e.KeyChar, (TextBox)sender);
        }

        private bool IsOKForDecimalTextBox(char theCharacter, TextBox theTextBox)
        {
            // Only allow control characters, digits, plus and minus signs.
            // Only allow ONE plus sign.
            // Only allow ONE minus sign.
            // Only allow the plus or minus sign as the FIRST character.
            // Only allow ONE decimal point.
            // Do NOT allow decimal point or digits BEFORE any plus or minus sign.

            if (
                !char.IsControl(theCharacter)
                && !char.IsDigit(theCharacter)
                && (theCharacter != NumberDecimalSeparator)
                && (theCharacter != ',')
                && (theCharacter != '-')
                && (theCharacter != '+')
            )
            {
                // Then it is NOT a character we want allowed in the text box.
                return false;
            }



            // Only allow one decimal point.
            if (theCharacter == NumberDecimalSeparator
                && theTextBox.Text.IndexOf(NumberDecimalSeparator) > -1)
            {
                // Then there is already a decimal point in the text box.
                return false;
            }

            // Only allow one minus sign.
            if (theCharacter == '-'
                && theTextBox.Text.IndexOf('-') > -1)
            {
                // Then there is already a minus sign in the text box.
                return false;
            }

            // Only allow one plus sign.
            if (theCharacter == '+'
                && theTextBox.Text.IndexOf('+') > -1)
            {
                // Then there is already a plus sign in the text box.
                return false;
            }

            // Only allow one plus sign OR minus sign, but not both.
            if (
                (
                    (theCharacter == '-')
                    || (theCharacter == '+')
                )
                &&
                (
                    (theTextBox.Text.IndexOf('-') > -1)
                    ||
                    (theTextBox.Text.IndexOf('+') > -1)
                )
                )
            {
                // Then the user is trying to enter a plus or minus sign and
                // there is ALREADY a plus or minus sign in the text box.
                return false;
            }

            // Only allow a minus or plus sign at the first character position.
            if (
                (
                    (theCharacter == '-')
                    || (theCharacter == '+')
                )
                && theTextBox.SelectionStart != 0
                )
            {
                // Then the user is trying to enter a minus or plus sign at some position 
                // OTHER than the first character position in the text box.
                return false;
            }

            // Only allow digits and decimal point AFTER any existing plus or minus sign
            if (
                    (
                        // Is digit or decimal point
                        char.IsDigit(theCharacter)
                        ||
                        (theCharacter == NumberDecimalSeparator)
                    )
                    &&
                    (
                        // A plus or minus sign EXISTS
                        (theTextBox.Text.IndexOf('-') > -1)
                        ||
                        (theTextBox.Text.IndexOf('+') > -1)
                    )
                    &&
                        // Attempting to put the character at the beginning of the field.
                        theTextBox.SelectionStart == 0
                )
            {
                // Then the user is trying to enter a digit or decimal point in front of a minus or plus sign.
                return false;
            }

            // Otherwise the character is perfectly fine for a decimal value and the character
            // may indeed be placed at the current insertion position.
            return true;
        }
    }


    // Класс для отображения окна диалога для ввода номера
    //
    public class InputStringDialog
    {
        Form _form;
        Button _okButton;
        Button _cancelButton;
        TextBox _ValueField;
        Label _Label;
        bool isOK;
        string ResultString;

        InputStringDialog(string String, string Tooltip = null, int Length = 0, bool Multiline = false)
        {
            isOK = false;
            ResultString = String;

            int formfWidth = 300;
            int formfHeight = 200;

            if (Multiline)
                formfHeight = 300;

            _form = new Form();

            _form.AutoSize = false;
            _form.Height = formfHeight;
            if (Tooltip == null || Tooltip.Length == 0)
                _form.Text = "Please enter a string";
            else
                _form.Text = Tooltip;
            _form.StartPosition = FormStartPosition.CenterScreen;
            _form.MaximizeBox = false;
            _form.MinimizeBox = false;

            // Label
            _Label = new Label();
            _Label.Text = _form.Text;
            _Label.Padding = new Padding(20, 20, 20, 20);
            _Label.TextAlign = ContentAlignment.MiddleCenter;
            _Label.AutoSize = true;
            _Label.Font = new Font(FontFamily.GenericSansSerif, 14, FontStyle.Bold);

            using (Graphics g = _form.CreateGraphics())
            {
                SizeF size = g.MeasureString(_Label.Text, _Label.Font);
                formfWidth = Math.Max((int)Math.Ceiling(size.Width) + _Label.Padding.Horizontal, formfWidth);
                formfWidth = Math.Min(formfWidth, 600);
            }

            _form.Width = formfWidth;
            _form.MinimumSize = new Size(formfWidth, formfHeight);

            TableLayoutPanel CommonPanel = new TableLayoutPanel();
            CommonPanel.ColumnCount = 1;
            CommonPanel.RowCount = 3;
            CommonPanel.AutoSize = true;
            _form.Controls.Add(CommonPanel);

            Panel panel = new Panel();
            panel.Dock = DockStyle.Top;
            panel.AutoSize = true;
            panel.Controls.Add(_Label);
            CommonPanel.Controls.Add(panel);
            panel.BringToFront();

            // OK Button
            _okButton = new Button();
            _okButton.Click += OKClick;
            _okButton.Text = "OK";
            _okButton.MinimumSize = new Size(100, 21);
            _okButton.AutoSize = true;
            _okButton.Anchor = AnchorStyles.None;
            _okButton.Left = (_form.Width - _okButton.Width*2) / 2;
            _okButton.Margin = new Padding(10, 20, 10, 20);

            // Cancel Button
            _cancelButton = new Button();
            _cancelButton.Click += CancelClick;
            _cancelButton.Text = "Cancel";
            _cancelButton.MinimumSize = new Size(100, 21);
            _cancelButton.AutoSize = true;
            _cancelButton.Anchor = AnchorStyles.None;
            _cancelButton.Left = _okButton.Left + _cancelButton.Width + 5;
            _cancelButton.Margin = new Padding(10, 20, 10, 20);

            // Text field

            _ValueField = new TextBox();
            if (Multiline)
            {
                _ValueField.Height = 100;
                _ValueField.ScrollBars = ScrollBars.Vertical;
            }
            _ValueField.Text = String;
            _ValueField.Width = formfWidth - 20;
            _ValueField.Multiline = Multiline;
            if (Length != 0)
                _ValueField.MaxLength = Length;

            panel = new Panel();
            panel.Height = 20;
            panel.Width = formfWidth;
            panel.Dock = DockStyle.Bottom;
            panel.AutoSize = true;
            panel.Controls.Add(_ValueField);
            panel.BringToFront();
            CommonPanel.Controls.Add(panel);


            panel = new Panel();
            panel.Height = 20;
            panel.Width = formfWidth;
            panel.Dock = DockStyle.Bottom;
            panel.AutoSize = true;
            panel.Controls.Add(_okButton);
            panel.Controls.Add(_cancelButton);
            panel.BringToFront();
            CommonPanel.Controls.Add(panel);


            _form.ShowDialog(null);
        }

        public static bool Show(IVariable Value, string Tooltip = null, int Length = 0, bool Multiline = false)
        {
            InputStringDialog dlg = new InputStringDialog(Value.Value.AsString(), Tooltip, Length, Multiline);
            Value.Value = ValueFactory.Create(dlg.ResultString);
            return dlg.isOK;
        }

        public void OKClick(object sender, EventArgs e)
        {
            isOK = true;
            ResultString = _ValueField.Text;
            _form.Close();
        }
        public void CancelClick(object sender, EventArgs e)
        {
            isOK = false;
            _form.Close();
        }
    }
}
