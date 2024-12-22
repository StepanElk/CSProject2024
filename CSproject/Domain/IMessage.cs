using CSproject.Infrastructure;

namespace CSproject.Domain
{
    public interface IMessage
    {
        Person Sender { get; }
        bool Status { get; }
        DateTime SendDate { get; }
        public string Photo { get; } // URL
        Dictionary<string, bool> Reactions { get; }
        // Решил, что список будет лучше заменить на словарь
        // Ключ - Пользователь, оставивший реакцию; Значение - Htfrwbz
        // Т.к. реакции всего две, а обозначают они согласие/отказ на тренировку, "значение" может обойтись булевым типом. 
    }
}