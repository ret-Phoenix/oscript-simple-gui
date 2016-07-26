/*
 * Создано в SharpDevelop.
 * Пользователь: phoen
 * Дата: 23.07.2016
 * Время: 14:42
 * 
 * Для изменения этого шаблона используйте меню "Инструменты | Параметры | Кодирование | Стандартные заголовки".
 */
using System;
using ScriptEngine.Machine.Contexts;
using ScriptEngine.Machine;
using System.Collections.Generic;
using System.Windows.Forms;

namespace oscriptcomponent
{
	/// <summary>
	/// Description of SFormElements.
	/// </summary>
	[ContextClass("Элементы", "Elements")]
	public class SimpleFormElements : AutoContext<SimpleFormElements>
	{
        /// <summary>
        /// Ссылка на форму, элементы которой обрабатываются текущим классом.
        /// </summary>
        /// <param name="_frm"></param>
        //private SimpleForm _frm;
        private Form _frm;

        //private List<FormElement> _elements;
        Dictionary<string, IValue> _elements = new Dictionary<string, IValue>();
		
		public SimpleFormElements(Form frm)
		{
            _frm = frm;
		}

        //        [ContextMethod("Индекс", "Index")]
        //        public int Index()
        //        {
        //        	return -100;
        //        }


        //            //TextBox tb = new TextBox();
        //            //_form.Container.Add(tb);
        //
        //            ComboBox combo = new ComboBox();
        //            combo.Items.Add("11111");
        //            /* если необходимо добавить сразу несколько значений, то:
        //             * string[] mas=new string[]{"111","222","333"};
        //             * combo.Items.AddRange(mas);*/
        //            //combo.Location = new Point(300, 200);
        //            combo.Width = 50;
        //            _form.Controls.Add(combo);
        //
        //SFormElementGroup dd = new SFormElementGroup();

        [ContextMethod("Количество", "Count")]
        public int Count()
        {
        	return _elements.Count;
        }
        
        [ContextMethod("Найти", "Find")]
        public IValue Find(string elementName)
        {
            if (_elements.ContainsKey(elementName)) {
            	return _elements[elementName];	
            }
            return ValueFactory.Create();
            //return null;
        }
        
        [ContextMethod("Добавить", "Add")]
        public IValue add(string ElementName, string ElementType, IValue ElementParent)
        {

            Control parentCntrl;
            if (ElementParent == ValueFactory.Create())
            {
                parentCntrl = (this._frm);
            }
            else
            {
                parentCntrl = ((SimpleFormElementGroup)ElementParent).getControl();
            }

            IValue newItem = null;
        	if (ElementType.ToUpper() == ("ГруппаФормы").ToUpper()) {
        		newItem = new SimpleFormElementGroup(parentCntrl);
                ((SimpleFormElementGroup)newItem).Name = ElementName;
            }
        	
        	if (ElementType.ToUpper() == ("ПолеФормы").ToUpper()) {

        		newItem = new SimpleFormElementFormField(parentCntrl);
                ((SimpleFormElementFormField)newItem).Name = ElementName;

            }

        	if (ElementType.ToUpper() == ("КнопкаФормы").ToUpper()) {
                newItem = new SimpleFormFormButton(parentCntrl);
                ((SimpleFormFormButton)newItem).Name = ElementName;
            }

            _elements.Add(ElementName, newItem);
        	return newItem;
        }

//        [ContextMethod("Получить", "Get")]
//        public SFormElement getElement(int index)
//        {
//        	if (index <= this.Count()) {
//        		this._elements.E
//        	} else {
//        		return null;
//        	}
//        }  
        
	}
}
