
namespace EM.InsurePlus.Data
{
    using System.Threading.Tasks;
    using EM.InsurePlus.Data.Contract;
    using EM.InsurePlus.Data.Models.Identity;
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;   
    public class StorageContext : IdentityDbContext<User, Role, int>, IStorageContext
    {
        private readonly IModelRegistrar modelRegistrar;
        public StorageContext(DbContextOptions<StorageContext> options, IModelRegistrar modelRegistrar)
            : base(options)
        {
           
            this.modelRegistrar = modelRegistrar;
        }

        public async override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            return await this.SaveChangesAsync(true, cancellationToken);
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            modelRegistrar.RegisterModels(builder);
        }
    }
}
