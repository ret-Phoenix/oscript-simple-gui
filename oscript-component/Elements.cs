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

namespace oscriptGUI
{
    /// <summary>
    /// Description of SFormElements.
    /// </summary>
    [ContextClass("Элементы", "Elements")]
    public class Elements : AutoContext<Elements>
    {
        private Form _frm;

        //private List<FormElement> _elements;
        Dictionary<string, IValue> _elements = new Dictionary<string, IValue>();

        public Elements(Form frm)
        {
            _frm = frm;
        }

        public override string ToString()
        {
            return "Элементы";
        }

        /// <summary>
        /// Получает количество элементов коллекции.
        /// </summary>
        /// <returns><typeparam name="Число"></typeparam></returns>
        [ContextMethod("Количество", "Count")]
        public int Count()
        {
            return _elements.Count;
        }

        /// <summary>
        /// Осуществляет поиск элемента управления с заданным именем.
        /// </summary>
        [ContextMethod("Найти", "Find")]
        public IValue Find(string elementName)
        {
            if (_elements.ContainsKey(elementName))
            {
                return _elements[elementName];
            }
            return ValueFactory.Create();
        }

        [ContextMethod("Удалить", "Delete")]
        public void Delete(IValue Element)
        {
            //TODO: Сделал топорно, надо придумать как правильно
            string ElName = ((FormField)Element).Name;
            _elements.Remove(ElName);
            Control CurControl = ((FormField)Element).getBaseControl();
            CurControl.Controls.Clear();
            CurControl.Dispose();
            CurControl = null;
            //TODO: А нужно ли очищать входящий IValue Element, если да - то как?
            Element = ValueFactory.Create();
        }

        [ContextMethod("Переместить", "Move")]
        public void Move(IValue Element, IValue ParentElement, IValue BeforeElement)
        {

            Control CurControl = ((IFormElement)Element).getBaseControl();

            Control CurBeforeControl = null;
            if (BeforeElement != ValueFactory.Create())
            {
                CurBeforeControl = ((IFormElement)BeforeElement).getBaseControl();
            }

            Control CurParentControl = _frm;
            if (ParentElement != ValueFactory.Create())
            {
                CurParentControl = ((IFormElement)ParentElement).getBaseControl();

                if (((IFormElement)Element).Parent == ((IFormElement)ParentElement))
                {
                    int CurIndex = CurParentControl.Controls.GetChildIndex(CurControl);
                    int BefIndex = CurParentControl.Controls.GetChildIndex(CurBeforeControl);
                    CurParentControl.Controls.SetChildIndex(CurControl, BefIndex);
                }
                else
                {
                    int BefIndex = -1;
                    if (BeforeElement != ValueFactory.Create())
                    {
                        BefIndex = CurParentControl.Controls.GetChildIndex(CurBeforeControl);
                    }

                    CurControl.Parent = CurParentControl;
                    ((IFormElement)Element).setParent(ParentElement);
                    CurParentControl.Controls.SetChildIndex(CurControl, BefIndex + 1);
                }
            }
            else
            {
                    CurControl.Parent = CurParentControl;
                    ((IFormElement)Element).setParent(ParentElement);
                    CurParentControl.Controls.SetChildIndex(CurControl, 0);

            }

        }


        [ContextMethod("Добавить", "Add")]
        public IValue add(string ElementName, string ElementType, IValue ElementParent)
        {

            Control parentCntrl = (this._frm);
            if (ElementParent != ValueFactory.Create())
            {
                parentCntrl = ((IFormElement)ElementParent).getControl();
            }

            IValue newItem = null;
            if (ElementType.ToUpper() == ("ГруппаФормы").ToUpper())
            {
                newItem = new FormGroup(parentCntrl);
            }

            if (ElementType.ToUpper() == ("ПолеФормы").ToUpper())
            {
                newItem = new FormField(parentCntrl);
            }

            if (ElementType.ToUpper() == ("КнопкаФормы").ToUpper())
            {
                newItem = new FormButton(parentCntrl);
            }

            ((IFormElement)newItem).Name = ElementName;
            ((IFormElement)newItem).setParent(ElementParent);


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
