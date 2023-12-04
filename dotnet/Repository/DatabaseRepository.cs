using dotnet.Data;
using dotnet.Interface;
using dotnet.Models;
using Microsoft.EntityFrameworkCore;

namespace dotnet.Repository
{
    public class DatabaseRepository : DatabaseInterface
    {
        private readonly ApplicationContext context;
        public DatabaseRepository(ApplicationContext context)
        {
            this.context = context;
        }
        public bool Create(Database database)
        {
            context.Add(database);
            return Save();
        }

        public bool Delete(Database database)
        {

            context.datas.Remove(database.Data);
            context.Remove(database);
            return Save();
        }

        public async Task<IEnumerable<Database>> GetAll()
        {
            return await context.users.Include(x => x.Data).ToListAsync();
        }

        public async Task<Database> GetById(int id)
        {
            return await context.users.Include(a => a.Data).FirstOrDefaultAsync(e => e.Id == id);
        }

        public async Task<IEnumerable<Database>> GetByName(string name)
        {
            return await context.users.Include(c => c.Data).Where(i => i.Data.Name.Contains(name)).ToListAsync();
        }

        public bool Save()
        {
            var result = context.SaveChanges();
            return result > 0 ? true : false;
        }

        public bool Update(Database database)
        {
            //Doesn't work
            //context.Update(database);

            var dat = context.datas.Find(database.DataID);
            var user = context.users.Find(database.Id);
            if (dat != null && user != null)
            {
                user.Id = database.Id;
                user.Email = database.Email;
                user.Password = database.Password;
                user.DataID = database.DataID;
                dat.Id = database.DataID;
                dat.Name = database.Data.Name;
                dat.Phone = database.Data.Phone;
                dat.Picture = database.Data.Picture;
                dat.Bio = database.Data.Bio;
                context.SaveChanges();
            }
            return Save();
        }
    }
}
