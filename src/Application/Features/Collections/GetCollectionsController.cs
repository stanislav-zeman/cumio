using Cumio.Application.Domain.ValueObjects;

namespace Cumio.Application.Features.Collections;

using AutoMapper;
using AutoMapper.QueryableExtensions;
using Common;
using Common.Mappings;
using Domain.Entities;
using Infrastructure.Persistence;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

public class GetCollectionsController : ApiControllerBase
{
    [HttpGet("/api/collections")]
    public async Task<ActionResult<CollectionsVm>> Get()
    {
        return await Mediator.Send(new GetCollectionsQuery());
    }
}

public class GetCollectionsQuery : IRequest<CollectionsVm>
{
}

public class CollectionsVm
{
    public IList<CollectionDto> Collections { get; set; } = new List<CollectionDto>();
}


public class CollectionDto : IMapFrom<Collection>
{
    public CollectionDto()
    {
        Items = new List<ContentDto>();
    }

    public int Id { get; set; }

    public string? Title { get; set; }

    public string? Description { get; set; }

    public string? Author { get; set; }

    public IList<ContentDto> Items { get; set; }
}

public class ContentDto : IMapFrom<Content>
{
    public int Id { get; set; }

    public string? Title { get; set; }

    public string? Author { get; set; }

    public ObjectStorageLocation? DataLocation { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<Content, ContentDto>();
    }
}

internal sealed class GetCollectionsQueryHandler : IRequestHandler<GetCollectionsQuery, CollectionsVm>
{
    private readonly ApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetCollectionsQueryHandler(ApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<CollectionsVm> Handle(GetCollectionsQuery request, CancellationToken cancellationToken)
    {
        return new CollectionsVm
        {
            Collections = await _context.Collections
                .AsNoTracking()
                .ProjectTo<CollectionDto>(_mapper.ConfigurationProvider)
                .OrderBy(t => t.Title)
                .ToListAsync(cancellationToken)
        };
    }
}