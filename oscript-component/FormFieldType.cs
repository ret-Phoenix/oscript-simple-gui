using System;
using ScriptEngine.Machine.Contexts;
using ScriptEngine.Machine;
using System.Collections.Generic;

namespace oscriptGUI
{

    enum EnumFormFieldType : int
    {
        InputField = 0,
        HTMLDocumentField = 1,
        ProgressBarField = 2,
        CalendarField = 3,
        //PictureField = 4,
        LabelField = 5,
        CheckBoxField = 6,
        TextDocumentField = 7,
        ComboBox = 8,
        ListBox = 9
    }

    /// <summary>
    /// Вид поля формы
    /// </summary>
    [ContextClass("ВидПоляФормы", "FormFieldType")]
    public class FormFieldType : AutoContext<FormFieldType>, IValue
    {

        /// <summary>
        ///     Однострочное, текстовое поле
        /// </summary>
        /// <remarks>
        ///     <name>ПолеВвода</name>
        ///     <name_en>InputField</name_en>
        /// </remarks>
        [ContextProperty("ПолеВвода", "InputField")]
        public int InputField
        {
            get { return (int)EnumFormFieldType.InputField; }
        }

        /// <summary>
        /// ПолеHTMLДокумента - WebBrowser
        /// </summary>
        [ContextProperty("ПолеHTMLДокумента", "HTMLDocumentField")]
        public int HTMLDocumentField
        {
            get { return (int)EnumFormFieldType.HTMLDocumentField; }
        }

        /// <summary>
        /// ProgressBar
        /// </summary>
        [ContextProperty("ПолеИндикатора", "ProgressBarField")]
        public int ProgressBarField
        {
            get { return (int)EnumFormFieldType.ProgressBarField; }
        }

        /// <summary>
        /// Поле для выбора даты/времени
        /// </summary>
        [ContextProperty("ПолеКалендаря", "CalendarField")]
        public int CalendarField
        {
            get { return (int)EnumFormFieldType.CalendarField; }
        }

        //[ContextProperty("ПолеКартинки", "PictureField")]
        //public int PictureField
        //{
        //    //get { return 4; }
        //    get { return (int)EnumFormFieldType.PictureField; }
        //}

        /// <summary>
        /// Надпись
        /// </summary>
        [ContextProperty("ПолеНадписи", "LabelField")]
        public int LabelField
        {
            get { return (int)EnumFormFieldType.LabelField; }
        }

        /// <summary>
        /// Флажок
        /// </summary>
        [ContextProperty("ПолеФлажка", "CheckBoxField")]
        public int CheckBoxField
        {
            get { return (int)EnumFormFieldType.CheckBoxField; }
        }

        /// <summary>
        /// Многострочный текст
        /// </summary>
        [ContextProperty("ПолеТекстовогоДокумента", "TextDocumentField")]
        public int TextDocumentField
        {
            get { return (int)EnumFormFieldType.TextDocumentField; }
        }

        /// <summary>
        /// Поле со списком
        /// </summary>
        [ContextProperty("ПолеСоСписком", "ComboBox")]
        public int ComboBox
        {
            get { return (int)EnumFormFieldType.ComboBox; }
        }

        /// <summary>
        /// Поле списка
        /// </summary>
        [ContextProperty("ПолеСписка", "ListBox")]
        public int ListBox
        {
            get { return (int)EnumFormFieldType.ListBox; }
        }


    }

}
