using Microsoft.EntityFrameworkCore;

namespace Domain.Context;

public class BaseDbContext : DbContext {
    public BaseDbContext(DbContextOptions options) : base(options) {
        
    }
}