using System.Xml;

namespace CSproject.Domain
{
    public abstract class BaseMessage : IMessage
    {
        public Person Sender { get; private set; }
        public bool Status { get; private set; }
        public DateTime SendDate { get; private set; }
        public string Photo { get; private set; }
        // URL
        public Dictionary<string, bool> Reactions { get; private set; }
        // Решил, что список будет лучше заменить на словарь
        // Ключ - Пользователь, оставивший реакцию; Значение - Htfrwbz
        // Т.к. реакции всего две, а обозначают они согласие/отказ на тренировку, "значение" может обойтись булевым типом.
        protected BaseMessage(Person sender, DateTime sendDate /*...*/)
        {
            Sender = sender;
            SendDate = sendDate;
            /*...*/
        }
    }
}