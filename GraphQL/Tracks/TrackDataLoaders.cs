using ConferencePlanner.GraphQL.Data;
using GreenDonut.Data;
using Microsoft.EntityFrameworkCore;

namespace ConferencePlanner.GraphQL.Tracks;

public static class TrackDataLoaders
{
    [DataLoader]
    public static async Task<IReadOnlyDictionary<int, Track>> TrackByIdAsync(
        IReadOnlyList<int> ids,
        ApplicationDbContext dbContext,
        ISelectorBuilder selector,
        CancellationToken cancellationToken
    )
    {
        return await dbContext
            .Tracks.AsNoTracking()
            .Where(t => ids.Contains(t.Id))
            .Select(t => t.Id, selector)
            .ToDictionaryAsync(t => t.Id, cancellationToken);
    }

    [DataLoader]
    public static async Task<IReadOnlyDictionary<int, Page<Session>>> SessionsByTrackIdAsync(
        IReadOnlyList<int> trackIds,
        ApplicationDbContext dbContext,
        ISelectorBuilder selector,
        PagingArguments pagingArguments,
        CancellationToken cancellationToken
    )
    {
        return await dbContext
            .Sessions.AsNoTracking()
            .Where(t => t.TrackId != null && trackIds.Contains((int)t.TrackId))
            .OrderBy(t => t.Id)
            .Select(t => t.TrackId, selector)
            .ToBatchPageAsync(t => (int)t.TrackId!, pagingArguments, cancellationToken);
    }
}
