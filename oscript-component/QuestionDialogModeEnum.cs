using ScriptEngine.HostedScript;
using ScriptEngine.Machine;
using ScriptEngine.Machine.Contexts;

namespace oscriptGUI
{
    [SystemEnum("РежимДиалогаВопрос", "QuestionDialogMode")]
   public class QuestionDialogModeEnum : EnumerationContext
    {
        private QuestionDialogModeEnum(TypeDescriptor typeRepresentation, TypeDescriptor valuesType)
            : base(typeRepresentation, valuesType)
        {

        }

        public static QuestionDialogModeEnum CreateInstance()
        {
            QuestionDialogModeEnum instance;
            var type = TypeManager.RegisterType("ПеречислениеРежимДиалогаВопрос", typeof(QuestionDialogModeEnum));
            var enumValueType = TypeManager.RegisterType("РежимДиалогаВопрос", typeof(CLREnumValueWrapper<InnerQuestionDialogModeEnum>));

            instance = new QuestionDialogModeEnum(type, enumValueType);

            instance.AddValue("ДаНет", "YesNo", new CLREnumValueWrapper<InnerQuestionDialogModeEnum>(instance, InnerQuestionDialogModeEnum.YesNo));
            instance.AddValue("ДаНетОтмена", "YesNoCancel", new CLREnumValueWrapper<InnerQuestionDialogModeEnum>(instance, InnerQuestionDialogModeEnum.YesNoCancel));
            instance.AddValue("ОК", "OK", new CLREnumValueWrapper<InnerQuestionDialogModeEnum>(instance, InnerQuestionDialogModeEnum.OK));
            instance.AddValue("ОКОтмена", "OKCancel", new CLREnumValueWrapper<InnerQuestionDialogModeEnum>(instance, InnerQuestionDialogModeEnum.OKCancel));
            instance.AddValue("ПовторитьОтмена", "RetryCancel", new CLREnumValueWrapper<InnerQuestionDialogModeEnum>(instance, InnerQuestionDialogModeEnum.RetryCancel));
            instance.AddValue("ПрерватьПовторитьПропустить", "AbortRetryIgnore", new CLREnumValueWrapper<InnerQuestionDialogModeEnum>(instance, InnerQuestionDialogModeEnum.AbortRetryIgnore));

            return instance;
        }
    }

    public enum InnerQuestionDialogModeEnum
    {
        YesNo,
        YesNoCancel,
        OK,
        OKCancel,
        RetryCancel,
        AbortRetryIgnore
    }
}
