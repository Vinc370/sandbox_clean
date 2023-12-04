namespace dotnet.Interface
{
    public interface GenericCommand<T>
    {
        Task Create(T model);
        Task Update(T model);
        Task Delete(int id);
    }
}
