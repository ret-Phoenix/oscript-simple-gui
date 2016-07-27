using System;
using ScriptEngine.Machine.Contexts;
using ScriptEngine.Machine;

using System.Windows.Forms;


namespace oscriptGUI
{
    /// <summary>
    /// Фабрика для работы с формами
    /// </summary>
    //[ContextClass("УправляемыйИнтерфейс", "ManagedGUI")]
    [ContextClass("ПростойГУИ", "SimpleGUI")]
    public class ManagedGUI : AutoContext<ManagedGUI>
    {
        private string _version;

        public ManagedGUI()
        {
            this._version = "0.0.0.1";
        }

        /// <summary>
        /// Номер версии библиотеки
        /// </summary>
        [ContextProperty("Версия", "Version")]
        public string version
        {
            get { return _version; }
        }

        // можем переопределить строковое отображение наших объектов
        public override string ToString()
        {
            return "SimpleGUI";
        }

        [ScriptConstructor]
        public static IRuntimeContextInstance Constructor()
        {
            return new ManagedGUI();
        }

        /// <summary>
        /// Создать форму
        /// </summary>
        /// <returns><see>SimpleForm</see> Возращает форму</returns>
        [ContextMethod("СоздатьФорму", "CreateForm")]
        public ManagedForm CreateForm()
        {
            return new ManagedForm();
        }

        ///// <summary>
        ///// Показать форму (модально)
        ///// </summary>
        //[ContextMethod("ПоказатьФорму", "ShowNewForm")]
        //public void ShowNewForm()
        //{
        //    Form frm = new Form();
        //    frm.ShowDialog();
        //}

    }
}
