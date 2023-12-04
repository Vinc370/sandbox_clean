using Microsoft.AspNetCore.Mvc;

namespace dotnet.Interface
{
    public interface GenericQuery<T>
    {
        Task<IEnumerable<T>> FindAll(int page);
        Task<IEnumerable<T>> FindAllNoPage();
        Task<IEnumerable<T>> Search(String search, int page);
        Task<IEnumerable<T>> SortByAge(bool asc, int page);
        Task<IEnumerable<T>> SortByName(bool asc, int page);
        Task<T> FindById(int id);
    }
}
