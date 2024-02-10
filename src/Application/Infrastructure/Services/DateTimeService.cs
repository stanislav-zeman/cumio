using Cumio.Application.Common.Interfaces;

namespace Cumio.Application.Infrastructure.Services;

public class DateTimeService : IDateTime
{
    public DateTime Now => DateTime.Now;
}