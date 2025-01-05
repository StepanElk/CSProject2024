using CSproject.Domain;
using Microsoft.EntityFrameworkCore;
using System;

namespace CSproject.Infrastructure
{
    //Работа с изменением списка пользователей в базе
    //Сам функционал изменения вынесен в DB и здесь не описывается
    public class UserRepository
    {
        private readonly EFContext _db;

        public UserRepository(EFContext db)
        {
            _db = db;
        }

        public void  SignUp(User person)
        {
            if (person == null)
            {
                throw new ArgumentNullException(nameof(person));
            }

             _db.Users.Add(person);
             _db.SaveChanges();
        }

        public async Task<List<User>> GetList()
        {
            return await _db.Users.ToListAsync();
        }

        public async void DeleteUser(User user)
        {
            _db.Users.Remove(user);
            await _db.SaveChangesAsync();
        }

        public TryLoginAnswers CheckUser(string login , string pasword)
        {
            User? user = _db.Users.FirstOrDefault(x => x.Login == login);

            if (user is not  null)
            {
                return user.Password == pasword 
                    ? TryLoginAnswers.Ok 
                    : TryLoginAnswers.WrongPassword;
            }

            return TryLoginAnswers.WrongLogin;
        }

        public bool IsUserExist(string login)
        {
            return _db.Users.FirstOrDefault(x => x.Login == login) is not null;
        }
    }
}
