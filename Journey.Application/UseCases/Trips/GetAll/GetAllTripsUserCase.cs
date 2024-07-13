using Journey.Communication.Responses;
using Journey.Infrastructure;
using Journey.Infrastructure.Entities;

namespace Journey.Application.UseCases.Trips.GetAll
{
    public class GetAllTripsUserCase
    {
        public ResponseTripsJson Execute()
        {
            JourneyDbContext dbContext = new JourneyDbContext();

            List<Trip> trips = dbContext.Trips.ToList();

            return new ResponseTripsJson()
            {
                Trips = trips.Select(t => new ResponseShortTripJson { 
                    Id = t.Id,
                    EndDate = t.EndDate,
                    Name = t.Name,
                    StartDate = t.StartDate
                }).ToList()
            };
        }
    }
}
