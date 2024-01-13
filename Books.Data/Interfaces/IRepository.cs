namespace Books.Data.Interfaces
{
    public interface IRepository<T>
    {
        IQueryable<T> Get();
        T? GetById(int id);
        T Update(T entity);
        T Delete(T entity);
        T Insert(T entity);
        void SaveChanges();
    }
}
