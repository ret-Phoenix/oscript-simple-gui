/*
 * Создано в SharpDevelop.
 * Пользователь: ret-Phoenix
 * Дата: 23.07.2016
 * Время: 14:42
 * 
 * Для изменения этого шаблона используйте меню "Инструменты | Параметры | Кодирование | Стандартные заголовки".
 */
using ScriptEngine.Machine.Contexts;
using ScriptEngine.Machine;
using System.Collections.Generic;
using System.Windows.Forms;

namespace oscriptGUI
{
    /// <summary>
    /// Содержит коллекцию подчиненных элементов.
    /// </summary>
    [ContextClass("Элементы", "Elements")]
    public class Elements : AutoContext<Elements>, ICollectionContext
    {
        private Control _frm;
        private IValue _parent;

        //private List<FormElement> _elements;
        private Dictionary<string, IValue> _elements = new Dictionary<string, IValue>();

        public Elements(IValue parent, Control frm)
        {
            _frm = frm;
            _parent = parent;
        }

        public override string ToString()
        {
            return "Элементы";
        }


        #region Enumerator

        public IEnumerator<IValue> GetEnumerator()
        {
            foreach (var item in _elements)
            {
                //yield return new KeyAndValueImpl(ValueFactory.Create(item.Key), item.Value);
                yield return item.Value;
            }
        }

        public CollectionEnumerator GetManagedIterator()
        {
            return new CollectionEnumerator(GetEnumerator());
        }

        #region IEnumerable<IValue> Members

        #endregion

        #region IEnumerable Members

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        #endregion

        #endregion Enumerator
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

        /// <summary>
        /// Удаляет элемент из коллекции.
        /// </summary>
        /// <param name="Element"><see cref="FormButton"/>, <see cref="FormField"/>, <see cref="FormGroup"/> Удаляемый элемент.</param>
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

        /// <summary>
        /// Перемещает элемент коллекции.
        /// </summary>
        /// <param name="Element">Перемещаемый элемент.</param>
        /// <param name="ParentElement">Новый родитель элемента. Может совпадать со старым.</param>
        /// <param name="BeforeElement">Элемент, перед которым нужно разместить перемещаемый элемент. Если не задан, то перемещается в конец коллекции.</param>
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
                    this.MoveInElementsLists(((IFormElement)Element).Parent, ParentElement, Element);
                    ((IFormElement)Element).setParent(ParentElement);
                    CurParentControl.Controls.SetChildIndex(CurControl, BefIndex + 1);
                }
            }
            else
            {
                CurControl.Parent = CurParentControl;
                this.MoveInElementsLists(((IFormElement)Element).Parent, ParentElement, Element);
                ((IFormElement)Element).setParent(ParentElement);
                CurParentControl.Controls.SetChildIndex(CurControl, 0);
                
            }

        }


        /// <summary>
        /// Вставляет элемент в коллекцию элементов.
        /// </summary>
        /// <param name="ElementName">Уникальное имя добавляемого элемента.</param>
        /// <param name="ElementType">Тип добавляемого элемента.</param>
        /// <param name="ElementParent">Родитель для добавляемого элемента. Если не указан, то вставляется на верхний уровень.</param>
        /// <returns>Ссылка на созданый элемент <see cref="FormField"/>, <see cref="FormButton"/>, <see cref="FormGroup"/></returns>
        [ContextMethod("Добавить", "Add")]
        public IValue add(string ElementName, string ElementType, IValue ElementParent)
        {

            Control parentCntrl = (this._frm);
            if (ElementParent != ValueFactory.Create())
            {
                parentCntrl = ((IFormElement)ElementParent).getControl();
            }
            else
            {
                ElementParent = _parent;
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

            if (ElementType.ToUpper() == ("ТаблицаФормы").ToUpper())
            {
                newItem = new FormTable(parentCntrl);
            }

            ((IFormElement)newItem).setParent(ElementParent);
            ((IFormElement)newItem).Name = ElementName;

            ((IElementsContainer)ElementParent).Items.AddElement(ElementName, newItem);
            //_elements.Add(ElementName, newItem);
            return newItem;
        }

        #region ЗаплаткаДляПеремещений
        public void AddElement(string ElementName, IValue newItem)
        {
            _elements.Add(ElementName, newItem);
        }

        public void DelElementByName(string ElementName)
        {
            _elements.Remove(ElementName);
        }


        public void MoveInElementsLists(IValue oldParent, IValue newParent, IValue Element)
        {
            string ElementName = ((IFormElement)Element).Name;
            ((IElementsContainer)oldParent).Items.DelElementByName(ElementName);
            ((IElementsContainer)newParent).Items.AddElement(ElementName, Element);
        }
        #endregion ЗаплаткаДляПеремещений

        public void renameElement(string oldName, string newName)
        {
            var element = Find(oldName);
            if (element == ValueFactory.Create())
            {
                return;
            }
            _elements.Remove(oldName);
            _elements.Add(newName, element);
        }
    }
}
