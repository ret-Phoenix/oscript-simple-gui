using ScriptEngine.Machine;
using ScriptEngine.Machine.Contexts;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace oscriptGUI.ListViewVT
{
    [ContextClass("Изображения", "Images")]
    public class FormListViewImagePack : AutoContext<FormListViewImagePack>
    {

        private readonly ImageList _imageList = new ImageList();

        public FormListViewImagePack() {

        }

        public override string ToString()
        {
            return "Изображения";
        }

        [ContextMethod("Добавить")]
        public void Add(string imagePath)
        {
            System.Drawing.Icon _val = new System.Drawing.Icon(imagePath);
            _imageList.Images.Add(_val);
        }

        [ContextMethod("Очистить")]
        public void Clear(string imagePath)
        {
            _imageList.Images.Clear();
        }

        public ImageList list()
        {
            return _imageList;
        }
    }
}
