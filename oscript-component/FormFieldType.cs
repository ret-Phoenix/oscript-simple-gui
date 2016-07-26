using System;
using ScriptEngine.Machine.Contexts;
using ScriptEngine.Machine;
using System.Collections.Generic;

namespace oscriptcomponent
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
        ComboBox = 8
    }

    [ContextClass("ВидПоляФормы", "FormFieldType")]
    public class FormFieldType : AutoContext<FormFieldType>, IValue
    {
        
        //[ScriptConstructor]
        //public static IRuntimeContextInstance Constructor()
        //{
        //    return new SimpleForm();
        //}

        [ContextProperty("ПолеВвода", "InputField")]
        public int InputField
        {
            get { return (int)EnumFormFieldType.InputField; }
            //get { return EnumFormFieldType.InputField; }
        }

        [ContextProperty("ПолеHTMLДокумента", "HTMLDocumentField")]
        public int HTMLDocumentField
        {
            //get { return EnumFormFieldType.HTMLDocumentField; }
            get { return (int)EnumFormFieldType.HTMLDocumentField; }
        }

        [ContextProperty("ПолеИндикатора", "ProgressBarField")]
        public int ProgressBarField
        {
            get { return (int)EnumFormFieldType.ProgressBarField; }
        }

        [ContextProperty("ПолеКалендаря", "CalendarField")]
        public int CalendarField
        {
            //get { return 3; }
            get { return (int)EnumFormFieldType.CalendarField; }
        }

        //[ContextProperty("ПолеКартинки", "PictureField")]
        //public int PictureField
        //{
        //    //get { return 4; }
        //    get { return (int)EnumFormFieldType.PictureField; }
        //}

        [ContextProperty("ПолеНадписи", "LabelField")]
        public int LabelField
        {
            //get { return 5; }
            get { return (int)EnumFormFieldType.LabelField; }
        }

        [ContextProperty("ПолеФлажка", "CheckBoxField")]
        public int CheckBoxField
        {
            //get { return 6; }
            get { return (int)EnumFormFieldType.CheckBoxField; }
        }

        [ContextProperty("ПолеТекстовогоДокумента", "TextDocumentField")]
        public int TextDocumentField
        {
            //get { return 7; }
            get { return (int)EnumFormFieldType.TextDocumentField; }
        }

        [ContextProperty("ПолеСоСписком", "ComboBox")]
        public int ComboBox
        {
            get { return (int)EnumFormFieldType.ComboBox; }
        }


    }

}
