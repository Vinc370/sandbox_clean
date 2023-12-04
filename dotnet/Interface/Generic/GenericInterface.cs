namespace dotnet.Interface.Generic
{
    public interface GenericInterface<T>
    {
        Task<IEnumerable<T>> GetAll();
        Task<IEnumerable<T>> GetByName(string name);
        Task<T> GetById(int id);
        bool Create(T model);
        bool Update(T model);
        bool Delete(T model);
        bool Save();
    }
}
