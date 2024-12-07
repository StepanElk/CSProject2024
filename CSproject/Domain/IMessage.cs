namespace CSproject.Domain
{
    // Может реализовать это как базовый класс?
    public interface IMessage
    {
        string Sender { get; set; }
        bool Status { get; set; }
        DateTime SendDate { get; set; }
        string Photo { get; set; }
        // URL
        Dictionary<bool, string> Reactions { get; set; }
        // Решил, что список будет лучше заменить на словарь
        // Ключ - Реакция; Значение - Пользователь, оставивший реакцию
        // Т.к. реакции всего две, а обозначают они согласие/отказ на тренировку, "ключ" может обойтись булевым типом. 
    }
}