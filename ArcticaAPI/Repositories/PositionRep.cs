using ArcticaAPI.IRepositories;

namespace ArcticaAPI.Repositories
{
    public class PositionRep : IPositionRep
    {
        private readonly MyDbContext _db;

        public PositionRep(MyDbContext myDbContext)
        {
            _db = myDbContext;
        }

        public bool PositionExists(int id)
        {
            return _db.Positions.Any(h => h.Id == id);
        }
    }
}
