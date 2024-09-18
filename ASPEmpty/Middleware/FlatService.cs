using ASPEmpty.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;

namespace ASPEmpty.Middleware
{
    public class FlatService
    {
        private RealestaterentalContext db;
        private IMemoryCache cache;
        private readonly Int32 cacheDuration = 252;
        private String FlatChacheKey = "Flats";

        public FlatService(RealestaterentalContext db, IMemoryCache cache)
        {
            this.db = db;
            this.cache = cache;
        }

        public async Task<IEnumerable<Flat>> GetFlats()
        {
            IEnumerable<Flat> flats = null;
            if (!cache.TryGetValue(FlatChacheKey, out flats))
            {
                flats = await db.Flats.ToListAsync();
                if (flats != null)
                {
                    cache.Set(FlatChacheKey, flats, new MemoryCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromSeconds(cacheDuration)));
                }
            }
            return flats;
        }

        public async Task<Flat> GetFlat(Int32 id)
        {
            Flat flat = null;
            if (!cache.TryGetValue(id, out flat))
            {
                flat = await db.Flats.FirstOrDefaultAsync(l => l.Fid == id);
                if (flat != null)
                {
                    cache.Set(flat.Fid, flat,
                        new MemoryCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromSeconds(cacheDuration)));
                }
            }

            return flat;
        }

        public async Task AddFlat(Flat flat)
        {
            db.Flats.Add(flat);
            Int32 n = await db.SaveChangesAsync();
            if (n > 0)
            {
                cache.Set(flat.Fid, flat, new MemoryCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(cacheDuration)
                });
            }
        }
    }
}
