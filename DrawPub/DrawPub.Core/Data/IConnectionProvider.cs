using System.Data.Common;
using System.Threading.Tasks;


namespace DrawPub.Repository
{
    public interface IConnectionProvider
    {
        Task<DbConnection> GetAsync();
    }
}
