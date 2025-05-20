using BusinessLayer.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BusinessLayer.Abstract
{
    public interface IGenericService<T> where T : class
    {
        Task<Result<T>> TAdd(T t);

        Task TDelete(T t);

        Task<Result<T>> TUpdate(T t);

        Task<List<T>> Getlist();

        Task<Result<T>> TGetById(int id);

    }
}
