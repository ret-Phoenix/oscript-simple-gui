using ScriptEngine.HostedScript;
using ScriptEngine.Machine;
using ScriptEngine.Machine.Contexts;

namespace oscriptGUI
{
    [SystemEnum("КодВозвратаДиалога", "DialogReturnCode")]
    public class DialogReturnCodeEnum : EnumerationContext
    {
        private DialogReturnCodeEnum(TypeDescriptor typeRepresentation, TypeDescriptor valuesType)
            : base(typeRepresentation, valuesType)
        {

        }

        public static DialogReturnCodeEnum CreateInstance()
        {
            DialogReturnCodeEnum instance;
            var type = TypeManager.RegisterType("ПеречислениеКодВозвратаДиалога", typeof(DialogReturnCodeEnum));
            var enumValueType = TypeManager.RegisterType("КодВозвратаДиалога", typeof(CLREnumValueWrapper<InnerDialogReturnCodeEnum>));

            instance = new DialogReturnCodeEnum(type, enumValueType);

            instance.AddValue("Да", "Yes", new CLREnumValueWrapper<InnerDialogReturnCodeEnum>(instance, InnerDialogReturnCodeEnum.Yes));
            instance.AddValue("Нет", "No", new CLREnumValueWrapper<InnerDialogReturnCodeEnum>(instance, InnerDialogReturnCodeEnum.No));
            instance.AddValue("ОК", "OK", new CLREnumValueWrapper<InnerDialogReturnCodeEnum>(instance, InnerDialogReturnCodeEnum.OK));
            instance.AddValue("Отмена", "Cancel", new CLREnumValueWrapper<InnerDialogReturnCodeEnum>(instance, InnerDialogReturnCodeEnum.Cancel));
            instance.AddValue("Повторить", "Retry", new CLREnumValueWrapper<InnerDialogReturnCodeEnum>(instance, InnerDialogReturnCodeEnum.Retry));
            instance.AddValue("Прервать", "Abort", new CLREnumValueWrapper<InnerDialogReturnCodeEnum>(instance, InnerDialogReturnCodeEnum.Abort));
            instance.AddValue("Пропустить", "Ignore", new CLREnumValueWrapper<InnerDialogReturnCodeEnum>(instance, InnerDialogReturnCodeEnum.Ignore));
            instance.AddValue("Таймаут", "Timeout", new CLREnumValueWrapper<InnerDialogReturnCodeEnum>(instance, InnerDialogReturnCodeEnum.Timeout));

            return instance;
        }
    }

    public enum InnerDialogReturnCodeEnum
    {
        Yes,
        No,
        OK,
        Cancel,
        Retry,
        Abort,
        Ignore,
        Timeout
    }
}
