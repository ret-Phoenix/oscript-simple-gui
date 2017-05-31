using ScriptEngine.HostedScript;
using ScriptEngine.Machine;
using ScriptEngine.Machine.Contexts;

namespace oscriptGUI
{
    [SystemEnum("РежимДиалогаВыбораФайла", "FileDialogMode")]
    public class FileDialogModeEnum : EnumerationContext
    {
        private FileDialogModeEnum(TypeDescriptor typeRepresentation, TypeDescriptor valuesType)
            : base(typeRepresentation, valuesType)
        {

        }

        public static FileDialogModeEnum CreateInstance()
        {
            FileDialogModeEnum instance;
            var type = TypeManager.RegisterType("ПеречислениеРежимДиалогаВыбораФайла", typeof(FileDialogModeEnum));
            var enumValueType = TypeManager.RegisterType("РежимДиалогаВыбораФайла", typeof(CLREnumValueWrapper<InnerFileDialogModeEnum>));

            instance = new FileDialogModeEnum(type, enumValueType);

            instance.AddValue("ВыборКаталога", "ChooseDirectory", new CLREnumValueWrapper<InnerFileDialogModeEnum>(instance, InnerFileDialogModeEnum.ChooseDirectory));
            instance.AddValue("Открытие", "Open", new CLREnumValueWrapper<InnerFileDialogModeEnum>(instance, InnerFileDialogModeEnum.Open));
            instance.AddValue("Сохранение", "Save", new CLREnumValueWrapper<InnerFileDialogModeEnum>(instance, InnerFileDialogModeEnum.Save));

            return instance;
        }
    }

    public enum InnerFileDialogModeEnum
    {
        ChooseDirectory,
        Open,
        Save
    }
}
