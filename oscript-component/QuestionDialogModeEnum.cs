using ScriptEngine.HostedScript;
using ScriptEngine.Machine;
using ScriptEngine.Machine.Contexts;

namespace oscriptGUI
{
    /// <summary>
    /// Режимы диалогов
    /// </summary>
    [SystemEnum("РежимДиалогаВопрос", "QuestionDialogMode")]
    public class QuestionDialogModeEnum : EnumerationContext
    {
        private QuestionDialogModeEnum(TypeDescriptor typeRepresentation, TypeDescriptor valuesType)
            : base(typeRepresentation, valuesType)
        {

        }

        /// <summary>
        /// Инициализация
        /// </summary>
        /// <returns></returns>
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

    /// <summary>
    /// Режимы кнопок
    /// </summary>
    public enum InnerQuestionDialogModeEnum
    {
        /// <summary>
        /// Кнопки: Да + Нет
        /// </summary>
        YesNo,
        /// <summary>
        /// Кнопки: Да + Нет + Отмена
        /// </summary>
        YesNoCancel,
        /// <summary>
        /// Кнопки: Ок
        /// </summary>
        OK,
        /// <summary>
        /// Кнопки: Ок + Отмена
        /// </summary>
        OKCancel,
        /// <summary>
        /// Кнопки: Повторить + Отмена
        /// </summary>
        RetryCancel,
        /// <summary>
        /// Кнопки: Отменить + Повторить + Игнорировать
        /// </summary>
        AbortRetryIgnore
    }
}
