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

        public async void  Add(User person)
        {
            if (person == null)
            {
                throw new ArgumentNullException(nameof(person));
            }

            await _db.Users.AddAsync(person);
            await _db.SaveChangesAsync();
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

        public CheckUserAnswers CheckUser(string login , string pasword)
        {
            User? user = _db.Users.FirstOrDefault(x => x.Login == login);

            if (user is not  null)
            {
                return user.Password == pasword 
                    ? CheckUserAnswers.Ok 
                    : CheckUserAnswers.WrongPassword;
            }

            return CheckUserAnswers.WrongLogin;
        }
    }
}
