namespace WebAPI.Data
{
    using System.Threading.Tasks;
    using WebAPI.Data.Repo;
    using WebAPI.Interfaces;
    public class UnitOfWork : IUnitOfWork
    {
        private readonly StoreDataContext dc;
        public UnitOfWork(StoreDataContext dc)
        {
            this.dc = dc;

        }


        public ICategoryRepository categoryRepository => new CategoryRepository(dc);

        public IUserRepository userRepository => new UserRepository(dc);

        public async Task<bool> SaveAsync()
        {
            return await dc.SaveChangesAsync() > 0;
        }
    }
}