using ScriptEngine.Machine;
using ScriptEngine.Machine.Contexts;
using System.Windows.Forms;

namespace oscriptGUI
{
    [ContextClass("УведомлениеВТрее", "NotifyInTray")]
    class NotifyInTray : AutoContext<NotifyInTray>
    {

        private readonly NotifyIcon notifyIcon; 
        private string _icon;
        
        public NotifyInTray()
        {
            notifyIcon = new NotifyIcon();
            notifyIcon.Icon = Properties.Resources.gtkabout;
            _icon = "";
        }

        [ScriptConstructor]
        public static IRuntimeContextInstance Constructor()
        {
            return new NotifyInTray();
        }

        /// <summary>
        /// Заголовок уведомления
        /// </summary>
        [ContextProperty("Заголовок", "Title")]
        public string Title { get; set; }

        /// <summary>
        /// Текст уведомления в трее
        /// </summary>
        [ContextProperty("Текст", "Text")]
        public string Text { get; set; }

        /// <summary>
        /// Срок отображения сообщения в трее. Время в секундах.
        /// </summary>
        [ContextProperty("Таймаут", "Timeout")]
        public int Timeout { get; set; }

        /// <summary>
        /// Иконка в трее, если не указана - берется по умолчанию из ресурсов. Можно указать путь к файлу со своей иконкой.
        /// </summary>
        [ContextProperty("Иконка", "Icon")]
        public string Icon {
            get { return _icon; }
            set {

                if (value == "")
                {
                    notifyIcon.Icon = Properties.Resources.gtkabout;
                }
                else
                {
                    notifyIcon.Icon = new System.Drawing.Icon(value);
                }
                _icon = value;
            }
        }

        /// <summary>
        /// Показывает уведомление в трее
        /// </summary>
        [ContextMethod("Показать", "Show")]
        public void Show()
        {
            notifyIcon.Visible = true;

            if (Title != null)
            {
                notifyIcon.BalloonTipTitle = Title;
            }

            if (Text != null)
            {
                notifyIcon.BalloonTipText = Text;
            }
            
            notifyIcon.ShowBalloonTip(Timeout);
        }

    }
}
