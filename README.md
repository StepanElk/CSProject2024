# CSProject2024
Проект по предмету Проектирование на языке C#

## Название проекта
Приложение-органайзер для спорт клуба

## Список студентов-участников: ФИО и группа.
Елькин Степан Андреевич – РИ-320945
Бутаков Никита Сергеевич – РИ-320932
Бобина Анастасия Алексеевна – РИ-320933
Зайцев Дмитрий Алексеевич - РИ-320932

## Проблема, которую решает проект. Основные сценарии использования.
### Проблема
Часто участники спортивных клубов испытывают трудности с поддержанием мотивации, регулярным планированием тренировок и социальным взаимодействием с единомышленниками. Наш проект решает эту проблему, представляя собой веб-приложение, в котором участники спортивного клуба смогут запланировать совместную тренировку/встречу и поделиться результатами тренировки. Продукт помогает поддерживать философию клуба, что заключается во взаимной поддержке участников, чтобы каждый мог поделиться своими результатами, в удобной форме собрать друзей на тренировку или встречу, заряжаться мотивацией и товарищеской конкуренцией.
### Основные сценарии использования:
1)	[Пользователь-клубный участник] хочет организовать совместную тренировку с друзьями, для этого в приложении создаёт событие, указывает дату, время и место, а также отправляет приглашение участникам клуба.
2)	[Пользователь-клубный участник] хочет принять участие в тренировке, организованной другими, для этого в приложении просматривает список предстоящих событий и отмечает свою реакцию (будет присутствовать или нет).
3)	[Пользователь-клубный участник] хочет поделиться своим прогрессом в тренировках, для этого в приложении создаёт запись о своей тренировке, указывая тип тренировки, описание, продолжительность и потраченные калории.
4)	[Пользователь-клубный участник] хочет зарядиться мотивацией и почувствовать поддержку от других участников клуба, для этого в приложении просматривает события, записи тренировок других пользователей и оставляет реакции/комментарии.

## Краткое описание основных компонент системы, из которой будет состоять ваш проект.
### Компоненты всей системы:
- Сервер, к которому подключается приложение
- База данных, хранящая все сообщения и иную информацию из приложения
- Приложение, выполняющее функцию авторизации и чата

### Компоненты приложения:
- Application 
  - Chat
[Конфигурация signalR хаба. Фронтенд вынесен отдельно, потому здесь, на беке, вызываются функции, отвечающие за какое-то действие в ответ на работу пользователя в приложении. Есть интерфейс IChatClient, в котором прописываются методы, через которые осуществляется отправка данных клиентам]
- Domain
  - Connection
[]
  - Message
[Базовый класс-сущность сообщения. Является основой для сообщений-событий и сообщений-тренировок]
  - EventMessage
[Сообщение-событие. Сущность, основанная на базовом сообщении, предлагающая информирование о событии с описанием характеристик оного]
  - TrainingMessage
[Сообщение-тренировка. Сущность, основанная на базовом сообщении, предлагающая информирование о тренировке с описанием характеристик оной]
  - User
[Сущность пользователь]
- Infrastructure
  - ConnectionsRepository
[Работа с активными подключениями. Здесь устанавливается связь между подключенным клиентом и его профилем User в системе]
  - DB
[Конфигурация базы данных на основе Entity Framework. В этом файле все основные настройки запуска, подключения к базе]
  - MessagesRepository
[Регистрация отправленных сообщений, подгрузка прошлых сообщений при входе в приложение]
  - UserRepository
[На этом классе основана работа входа и регистрации. Вся работа с пользователями]
  - TryLoginAnswers[Статусы авторизации в приложении]
  - TrySignUpAnswers[Статусы входа в комнату]

##	Краткое описание точек расширения: то есть заложенные в архитектуру особенности, которые облегчают добавления новой планируемой функциональности по аналогии с уже существующей.
### Наиболее простые для реализации точки расширения:
- Новые виды сообщений, порожденные от уже существующего базового класса сообщений.
- Функция ведения личной переписки между пользователями.
- Функция ведения личной статистики, основанная на отправленных сообщениях-событиях и сообщениях-тренировках.

### Теоретически возможные точки расширения: 
- Возможность взаимодействия с приложениями календаря, для созданий напоминаний о тренировках и событиях.

## Ссылки
- Figma с работой по дизайну:  
https://www.figma.com/design/igkXM77Lc2lDQo3ole9vMl/CS-sport-club?node-id=0-1&p=f&t=scOwcyt8hCv0DON4-0
- Git с работой по фронтенду:  
https://github.com/StepanElk/CS-frontend
