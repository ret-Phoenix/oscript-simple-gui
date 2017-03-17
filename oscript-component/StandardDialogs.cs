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

namespace oscriptGUI
{
    // [GlobalContext(Category = "Процедуры и функции интерактивной работы")]
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

}
