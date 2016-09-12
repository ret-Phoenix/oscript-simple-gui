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

namespace oscriptGUI
{

    [ContextClass("СтандартныеДиалоги", "StandardDialogs")]
    public class StandardDialogs : AutoContext<StandardDialogs>
    //    [GlobalContext(Category = "Процедуры и функции интерактивной работы")]
    //    public class StandardDialogs : GlobalContextBase<StandardDialogs>
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

        /*    public static IAttachableContext CreateInstance()
            {
                return new StandardDialogs();
            }*/

        [ScriptConstructor]
        public static IRuntimeContextInstance Constructor()
        {
            return new StandardDialogs();
        }

    }

    public class MessageBoxWithTimeOut
    {
        System.Threading.Timer _timeoutTimer;
        //ManagedForm form;
        Form _form;
        //Control formControl;
        Button _okButton;
        Label _msgLabel;

        MessageBoxWithTimeOut(string messageText, int timeOut, string title)
        {
            _form = new Form();

            //form = new ManagedForm();
            //formControl = form.getControl();
            _form.Height = 140;
            _form.Width = 200;
            _form.Text = title;
            _form.SizeChanged += FormSizeChanged;
            _form.StartPosition = FormStartPosition.CenterScreen;

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
            _form.Close();
            _timeoutTimer.Dispose();
        }

        public void OKClick(object sender, EventArgs e)
        {
            _form.Close();
        }

        void FormSizeChanged(object sender, EventArgs e)
        {
            _okButton.Left = (_form.ClientSize.Width - _okButton.Width - _okButton.Margin.Left) / 2;
        }
    }


}
