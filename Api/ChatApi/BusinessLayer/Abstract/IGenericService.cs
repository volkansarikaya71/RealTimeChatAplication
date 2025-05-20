namespace ChatApi.BusinessLayer.Abstract
{
    public interface IGenericService<T>
    {
        void TAdd(T t);

        void TDelete(T t);

        void TUpdate(T t);

        List<T> TGetlist();

        T TGetById(int id);
    }
}
