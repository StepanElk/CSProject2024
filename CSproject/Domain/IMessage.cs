namespace CSproject
{
    // Может реализовать это как базовый класс?
    public interface IMessage
    {
        string Sender { get; set; }
        bool Status { get; set; }
        DateTime SendDate { get; set; }
        string Photo { get; set; }
        // URL
        Dictionary<string, bool> Reactions { get; set; }
        // Решил, что список будет лучше заменить на словарь
        // Ключ - Пользователь, оставивший реакцию; Значение - реакция
        // Т.к. реакции всего две, а обозначают они согласие/отказ на тренировку, "значение" может обойтись булевым типом. 
    }
}