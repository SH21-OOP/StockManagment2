namespace DAL.Abstract
{
    public interface IMapper<TEntity, TEntityDTO, Repo>
        where TEntity : class, IEntity
        where TEntityDTO : class
        where Repo : IRepository<TEntity>
    {
        TEntityDTO Map(TEntity entity);

        TEntity DeMap(TEntityDTO dto);
    }
}
