using Microsoft.EntityFrameworkCore;

namespace ConferencePlanner.GraphQL.Data;

public sealed class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options)
{
    public DbSet<Speaker> Speakers { get; init; }
    public DbSet<Session> Sessions { get; init; }
    public DbSet<Attendee> Attendees { get; init; }
    public DbSet<Track> Tracks { get; init; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .Entity<Attendee>()
            .HasIndex(a => a.Username)
            .IsUnique();

        modelBuilder
            .Entity<SessionAttendee>()
            .HasKey(sa => new { sa.SessionId, sa.AttendeeId });

        modelBuilder
            .Entity<SessionSpeaker>()
            .HasKey(sa => new { sa.SessionId, sa.SpeakerId });
    }
}
