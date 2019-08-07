using FestiDB.Persistence;

namespace FestiAPI.Persistence
{
    public class ApiContext : FestiContext
    {
        public ApiContext(string getConnectionString) : base(getConnectionString)
        {
        }
    }


}
