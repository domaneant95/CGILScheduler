using Application.Filemanager;
using Persistence;

namespace API.Services
{
    public class DbFileProviderService
    {
        public DbFileProvider DbFileProvider { get; private set; }
        public DbFileProviderService()
        {
        }

        public void InitDbFileProvider(DataContext dataContext)
        {
            DbFileProvider = new DbFileProvider(dataContext);
        }
    }
}
