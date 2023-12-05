using Dapper;
using dotnet.Data;
using dotnet.Interface;
using dotnet.Models;

namespace dotnet.Repository
{
    public class PersonCommandRepository : GenericCommand<Person>
    {
        private readonly DapperContext context;

        public PersonCommandRepository(DapperContext context)
        {
            this.context = context;
        }

        public async Task Create(Person model)
        {
            var parameter = new DynamicParameters();
            parameter.Add("Id", model.id);
            parameter.Add("Name", model.name);
            parameter.Add("Dob", model.dob);

            using var connection = context.CreateConnection();
            await connection.QueryAsync(@"
                INSERT INTO " +
                    "person(id, name, dob) " +
                    "VALUES(@Id, @Name, @Dob); ",
                parameter);
        }

        public async Task Delete(int id)
        {
            using var connection = context.CreateConnection();
            await connection.QueryMultipleAsync(@"
                DELETE FROM " +
                    "person WHERE id = @id;",
                new { id });
        }

        public async Task Update(Person model)
        {
            var parameter = new DynamicParameters();
            parameter.Add("Id", model.id);
            parameter.Add("Name", model.name);
            parameter.Add("Dob", model.dob);

            using var connection = context.CreateConnection();
            await connection.QueryAsync(@"
                UPDATE " +
                    "person " +
                "SET " +
                    "id = @Id, name = @Name, dob = @Dob " +
                "WHERE " +
                    "id = @Id;",
                parameter);
        }
    }
}
