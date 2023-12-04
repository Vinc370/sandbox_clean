using dotnet.Data;
using dotnet.Interface;
using dotnet.Models;
using Microsoft.EntityFrameworkCore;

namespace dotnet.Repository
{
    public class UserDataRepository : UserDataInterface
    {
        private readonly ApplicationContext context;

        public UserDataRepository(ApplicationContext context)
        {
            this.context = context;
        }

        public bool Create(UserData userData)
        {
            context.Add(userData);
            return Save();
        }

        public bool Delete(UserData userData)
        {
            context.Remove(userData);
            return Save();
        }

        public async Task<IEnumerable<UserData>> GetAll()
        {
            return await context.datas.ToListAsync();
        }

        public async Task<UserData> GetById(int id)
        {
            return await context.datas.FirstOrDefaultAsync(a => a.Id == id);
        }

        public async Task<IEnumerable<UserData>> GetByName(string name)
        {
            return await context.datas.Where(e => e.Name.Contains(name)).ToListAsync();
        }

        public bool Save()
        {
            var result = context.SaveChanges();
            return result > 0 ? true : false;
        }

        public bool Update(UserData userData)
        {
            context.Update(userData);
            return Save();
        }
    }
}
