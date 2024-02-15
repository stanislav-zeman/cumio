using Cumio.Application.Features.Collections;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Cumio.Application.Common;
using Cumio.Application.Common.Mappings;
using Cumio.Application.Common.Models;
using Cumio.Application.Infrastructure.Persistence;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Cumio.Application.Features.Contents;

public class GetContentWithPaginationController : ApiControllerBase
{
    [HttpGet("/api/contents")]
    public Task<PaginatedList<ContentDto>> GetContentWithPagination([FromQuery] GetContentWithPaginationQuery query)
    {
        return Mediator.Send(query);
    }
}

public class GetContentWithPaginationQuery : IRequest<PaginatedList<ContentDto>>
{
    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 10;
}

public class GetContentWithPaginationQueryValidator : AbstractValidator<GetContentWithPaginationQuery>
{
    public GetContentWithPaginationQueryValidator()
    {
        RuleFor(x => x.PageNumber)
            .GreaterThanOrEqualTo(1).WithMessage("PageNumber at least greater than or equal to 1.");

        RuleFor(x => x.PageSize)
            .GreaterThanOrEqualTo(1).WithMessage("PageSize at least greater than or equal to 1.");
    }
}

internal sealed class GetContentWithPaginationQueryHandler : IRequestHandler<GetContentWithPaginationQuery, PaginatedList<ContentDto>>
{
    private readonly ApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetContentWithPaginationQueryHandler(ApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public Task<PaginatedList<ContentDto>> Handle(GetContentWithPaginationQuery request, CancellationToken cancellationToken)
    {
        return _context.Contents
            .OrderBy(x => x.Title)
            .ProjectTo<ContentDto>(_mapper.ConfigurationProvider)
            .PaginatedListAsync(request.PageNumber, request.PageSize);
    }
}