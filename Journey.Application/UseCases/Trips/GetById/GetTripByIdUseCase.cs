using Journey.Communication.Responses;
using Journey.Exception;
using Journey.Exception.ExceptionsBase;
using Journey.Infrastructure;
using Journey.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;

namespace Journey.Application.UseCases.Trips.GetById
{
    public class GetTripByIdUseCase
    {
        public ResponseTripJson Execute(Guid id) 
        {
            JourneyDbContext dbContext = new JourneyDbContext();

            Trip trip = dbContext
                .Trips
                .Include(t => t.Activities)
                .FirstOrDefault(t => t.Id == id);

            if (trip is null)
            {
                throw new NotFoundException(ResourceErrorMessages.TRIP_NOT_FOUND);
            }

            return new ResponseTripJson
            {
                Id = trip.Id,
                Name = trip.Name,
                StartDate = trip.StartDate,
                EndDate = trip.EndDate,
                Activities = trip.Activities.Select(a => new ResponseActivityJson 
                { 
                    Id = a.Id,
                    Name = a.Name,
                    Date = a.Date,
                    Status = (Communication.Enums.ActivityStatus)a.Status
                }).ToList()
            };
        }
    }
}
