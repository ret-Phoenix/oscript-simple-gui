/*
 * Создано в Visual Studio.
 * Пользователь: alexkmbk
 * Дата: 14.10.2016
 * 
 */

using ScriptEngine.Machine.Contexts;
using System.Windows.Forms;
using System;
using ScriptEngine.Machine;
using System.Threading;
using ScriptEngine.HostedScript.Library;

namespace oscriptGUI
{

    [ContextClass("ДиалогВыбораФайла", "FileDialog")]
    public class FileDialog : AutoContext<FileDialog>
    {
        InnerFileDialogModeEnum _mode;
        string[] _fileNames;

        /// <summary>
        /// Конструктор с заданным режимом работы.
        /// </summary>
        /// <param name="mode">Режим работы.</param>
        public FileDialog(CLREnumValueWrapper<InnerFileDialogModeEnum> mode)
        {
            _mode = (InnerFileDialogModeEnum)mode.UnderlyingObject;
            Multiselect = false;
            Preview = true;

        }

        [ScriptConstructor]
        public static IRuntimeContextInstance Constructor(CLREnumValueWrapper<InnerFileDialogModeEnum> mode)
        {
            return new FileDialog(mode);
        }

        /// <summary>
        /// Заголовок
        /// </summary>
        [ContextProperty("Заголовок", "Title")]
        public string Title
        {
            get; set;
        }

        /// <summary>
        /// Фильтр
        /// </summary>
        [ContextProperty("Фильтр", "Filter")]
        public string Filter
        {
            get; set;
        }

        /// <summary>
        /// Фильтр
        /// </summary>
        [ContextProperty("ПолноеИмяФайла", "FullFileName")]
        public string FullFileName
        {
            get; set;
        }

        [ContextProperty("Каталог", "Directory")]
        public string Directory
        {
            get; set;
        }

        [ContextProperty("Расширение", "DefaultExt")]
        public string DefaultExt
        {
            get; set;
        }

        [ContextProperty("МножественныйВыбор", "Multiselect")]
        public bool Multiselect
        {
            get; set;
        }

        [ContextProperty("ВыбранныеФайлы", "SelectedFiles")]
        public IValue SelectedFiles
        {
            get {
                ArrayImpl array = new ArrayImpl();
                foreach (var fileName in _fileNames)
                {                  
                    array.Add(ValueFactory.Create(fileName));
                }
                return array;
            }
        }

        [ContextProperty("Режим", "Mode")]
        public IValue Mode
        {
            get
            {
                return GlobalsManager.GetEnum<FileDialogModeEnum>()[_mode.ToString()];
            }
        }

        [ContextProperty("ИндексФильтра", "FilterIndex")]
        public int FilterIndex
        {
            get; set;
        }

        [ContextProperty("ПредварительныйПросмотр", "Preview")]
        public bool Preview
        {
            get; set;
        }

        [ContextProperty("ПроверятьСуществованиеФайла", "CheckFileExists")]
        public bool CheckFileExists
        {
            get; set;
        }

        
        /// <summary>
        /// Открывает окно диалога выбора файла.
        /// </summary>
        [ContextMethod("Выбрать", "Choose")]
        [STAThread]
        public bool Choose()
        {
            bool returnVal = false;

            // Пришлось открывать окно в отдельном потоке, иначе окно не появлялось
            // посмотрел решение здесь - http://stackoverflow.com/a/36916460/2134488
            Thread t = new Thread(() =>
            {
                if (_mode == InnerFileDialogModeEnum.ChooseDirectory)
                {
                    FolderBrowserDialog folderDialog = new FolderBrowserDialog();
                    folderDialog.Description = Title;
                    if (string.IsNullOrEmpty(Directory))
                        folderDialog.SelectedPath = Directory;

                    
                    if (folderDialog.ShowDialog() == DialogResult.OK) { 
                        returnVal = true;
                        Directory = folderDialog.SelectedPath;
                        _fileNames = new string[1] { Directory };
                    }
                }
                else if (_mode == InnerFileDialogModeEnum.Open)
                {
                    OpenFileDialog openFileDialog = new OpenFileDialog();
                    openFileDialog.Title = Title;
                    openFileDialog.Filter = Filter;
                    openFileDialog.FilterIndex = FilterIndex;
                    openFileDialog.CheckFileExists = CheckFileExists;
                    // нет специального свойства для отключения предварительного просмотра
                    // посоветовали использовать свойство AutoUpgradeEnabled, но это костыль
                    //см. http://stackoverflow.com/a/10895858/2134488
                    openFileDialog.AutoUpgradeEnabled = Preview;
                    if (string.IsNullOrEmpty(Directory))
                        openFileDialog.InitialDirectory = Directory;
                    if (string.IsNullOrEmpty(DefaultExt))
                        openFileDialog.DefaultExt = DefaultExt;

                    openFileDialog.Multiselect = Multiselect;

                    if (openFileDialog.ShowDialog() == DialogResult.OK) { 
                        returnVal = true;
                        FullFileName = openFileDialog.FileName;
                        _fileNames = openFileDialog.FileNames;
                    }
                }
                else
                {
                    SaveFileDialog saveFileDialog = new SaveFileDialog();
                    saveFileDialog.Title = Title;
                    saveFileDialog.Filter = Filter;
                    saveFileDialog.FilterIndex = FilterIndex;
                    saveFileDialog.CheckFileExists = CheckFileExists;
                    if (string.IsNullOrEmpty(Directory))
                        saveFileDialog.InitialDirectory = Directory;
                    if (string.IsNullOrEmpty(DefaultExt))
                        saveFileDialog.DefaultExt = DefaultExt;

                    if (saveFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        returnVal = true;
                        FullFileName = saveFileDialog.FileName;
                        _fileNames = new string[1] { FullFileName };
                    }
                }
            });
            t.SetApartmentState(ApartmentState.STA);
            t.Start();
            t.Join();

            return returnVal;
        }
    }
}
