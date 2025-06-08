using ConferencePlanner.GraphQL.Data;
using GreenDonut.Data;
using Microsoft.EntityFrameworkCore;

namespace ConferencePlanner.GraphQL.Attendees;

public static class AttendeeDataLoaders
{
    [DataLoader]
    public static async Task<IReadOnlyDictionary<int, Attendee>> AttendeeByIdAsync(
        IReadOnlyList<int> ids,
        ApplicationDbContext dbContext,
        ISelectorBuilder selector,
        CancellationToken cancellationToken
    )
    {
        return await dbContext
            .Attendees.AsNoTracking()
            .Where(a => ids.Contains(a.Id))
            .Select(s => s.Id, selector)
            .ToDictionaryAsync(a => a.Id, cancellationToken);
    }

    [DataLoader]
    public static async Task<IReadOnlyDictionary<int, Session[]>> SessionsByAttendeeIdAsync(
        IReadOnlyList<int> attendeeIds,
        ApplicationDbContext dbContext,
        ISelectorBuilder selector,
        CancellationToken cancellationToken
    )
    {
        return await dbContext
            .Attendees.AsNoTracking()
            .Where(a => attendeeIds.Contains(a.Id))
            .Select(a => a.Id, a => a.SessionsAttendees.Select(aa => aa.Session), selector)
            .ToDictionaryAsync(a => a.Key, a => a.Value.ToArray(), cancellationToken);
    }
}
