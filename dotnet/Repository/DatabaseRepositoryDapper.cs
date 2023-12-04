using Dapper;
using dotnet.Data;
using dotnet.Interface;
using dotnet.Interface.Generic;
using dotnet.Models;
using Microsoft.Data.SqlClient;

namespace dotnet.Repository
{
    public class DatabaseRepositoryDapper : DapperInterface
    {
        private readonly DapperContext context;

        public DatabaseRepositoryDapper(DapperContext context)
        {
            this.context = context;
        }

        public async Task Create(Database model)
        {
            var parameter = new DynamicParameters();
            parameter.Add("Id", model.Id);
            parameter.Add("Email", model.Email);
            parameter.Add("Password", model.Password);
            parameter.Add("DataID", model.Data.Id);
            parameter.Add("Name", model.Data.Name);
            parameter.Add("Phone", model.Data.Phone);
            parameter.Add("Picture", model.Data.Picture);
            parameter.Add("Bio", model.Data.Bio);

            using var connection = context.CreateConnection();
            await connection.QueryMultipleAsync(@"
                INSERT INTO " +
                    "users(Id, Email, Password, DataID) " +
                    "VALUES(@Id, @Email, @Password, @Id);" +
                "INSERT INTO " +
                    "datas(Id, Name, Phone, Picture, Bio) " +
                    "VALUES( @Id, @Name, @Phone, @Picture, @Bio);",
                parameter);
        }

        public async Task Delete(int id)
        {
            using var connection = context.CreateConnection();
            await connection.QueryMultipleAsync(@"
                DELETE FROM " +
                    "users WHERE Id = @Id;" +
                "DELETE FROM " +
                    "datas WHERE Id = @Id;",
                new { id });
        }

        public async Task<IEnumerable<Database>> GetAll()
        {
            using var connection = context.CreateConnection();
            var indexD = await connection.QueryAsync<Database, UserData, Database>("SELECT users.*, datas.* FROM users INNER JOIN datas ON users.DataID = datas.Id", (databases, users) =>
            {
                databases.Data = users;
                return databases;
            });

            return indexD.ToList();
        }

        public async Task<Database> GetById(int id)
        {
            using var connection = context.CreateConnection();
            var indexD = await connection.QueryAsync<Database, UserData, Database>("SELECT users.*, datas.* FROM users INNER JOIN datas ON users.DataID = datas.Id WHERE users.Id = @Id", (databases, users) =>
            {
                databases.Data = users;
                return databases;
            }, new { id });

            return indexD.FirstOrDefault();
        }

        public async Task<IEnumerable<Database>> GetByName(string name)
        {
            throw new NotImplementedException();
        }

        public async Task Update(Database model)
        {
            var parameter = new DynamicParameters();
            parameter.Add("Id", model.Id);
            parameter.Add("Email", model.Email);
            parameter.Add("Password", model.Password);
            parameter.Add("DataID", model.DataID);
            parameter.Add("Name", model.Data.Name);
            parameter.Add("Phone", model.Data.Phone);
            parameter.Add("Picture", model.Data.Picture);
            parameter.Add("Bio", model.Data.Bio);

            using var connection = context.CreateConnection();
            await connection.QueryMultipleAsync(@"
                UPDATE " +
                    "users " +
                "SET " +
                    "Id = @Id, Email = @Email, Password = @Password, DataID = @Id " + 
                "WHERE " +
                    "Id = @Id; " +
                "UPDATE " +
                    "datas " +
                "SET " +
                    "Id = @Id, Name = @Name, Phone = @Phone, Picture = @Picture, Bio = @Bio " + 
                "WHERE " +
                    "Id = @Id; ",
                parameter);
        }

        bool GenericInterface<Database>.Create(Database model)
        {
            throw new NotImplementedException();
        }

        bool GenericInterface<Database>.Delete(Database model)
        {
            throw new NotImplementedException();
        }

        bool GenericInterface<Database>.Update(Database model)
        {
            throw new NotImplementedException();
        }

        public bool Save()
        {
            throw new NotImplementedException();
        }
    }
}
