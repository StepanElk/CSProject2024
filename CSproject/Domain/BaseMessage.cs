using System.Xml;

namespace CSproject.Domain
{
    //Entity ?
    public abstract class BaseMessage
    {
        //список реакций 
        public User Sender { get; private set; }
        public bool Status { get; private set; }
        public DateTime SendDate { get; private set; }
        public string Photo { get; private set; }
        // URL
        public Dictionary<string, bool> Reactions { get; private set; }
        //Степан : Нужен класс реакции тк у нее тоже есть отправитель,
        //реакции привязаны к сообщению, логически нужно для подсчета и отображения списков

        // Решил, что список будет лучше заменить на словарь
        // Ключ - Пользователь, оставивший реакцию; Значение - Htfrwbz
        // Т.к. реакции всего две, а обозначают они согласие/отказ на тренировку, "значение" может обойтись булевым типом.
        protected BaseMessage(User sender, DateTime sendDate /*...*/)
        {
            Sender = sender;
            SendDate = sendDate;
            /*...*/
        }
    }
}