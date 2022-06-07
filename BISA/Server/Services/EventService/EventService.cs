using BISA.Server.Data.DbContexts;
using BISA.Shared.Entities;

namespace BISA.Server.Services.EventService
{
    public class EventService : IEventService
    {
        private readonly BisaDbContext _context;

        public EventService(BisaDbContext context)
        {
            _context = context;
        }

        public async Task<EventDTO> CreateEvent(EventCreateDTO eventToCreate)
        {
            //Get all events
            var allEvents = await GetEvents();

            //See if event to be created has exact same property data as another event except Id.
            var foundDuplicate = allEvents
                .Any(e => e.Subject.ToLower() == eventToCreate.Subject.ToLower()
                && e.Date.Equals(eventToCreate.Date)
                && e.Location.ToLower() == eventToCreate.Location.ToLower()
                && e.Organizer.ToLower() == eventToCreate.Organizer.ToLower()
                && e.Description.ToLower() == eventToCreate.Description.ToLower()
                && e.Type.Id == eventToCreate.Type.Id);

            if (foundDuplicate)
            {
                throw new ArgumentException("Event already exists");
            }

            var eventEntity = new EventEntity()
            {
                Date = eventToCreate.Date,
                Organizer = eventToCreate.Organizer,
                Subject = eventToCreate.Subject,
                Location = eventToCreate.Location,
                Description = eventToCreate.Description,
                EventTypeId = eventToCreate.Type.Id
            };

            var savedEntity = _context.Events.Add(eventEntity);
            var savedResult = await _context.SaveChangesAsync();

            if (savedResult < 1)
            {
                throw new DbUpdateException("Unable to save event to database");
            }

            var savedEvent = new EventDTO
            {
                Id = savedEntity.Entity.Id,
                Date = savedEntity.Entity.Date,
                Organizer = savedEntity.Entity.Organizer,
                Subject = savedEntity.Entity.Subject,
                Location = savedEntity.Entity.Location,
                Description = savedEntity.Entity.Description,
                Type = new EventTypeDTO
                {
                    Id = savedEntity.Entity.EventType.Id,
                    Capacity = savedEntity.Entity.EventType.Capacity,
                    Description = savedEntity.Entity.EventType.Description,
                    Image = savedEntity.Entity.EventType.Image,
                    Type = savedEntity.Entity.EventType.Type
                }
            };

            return savedEvent;
        }

        public async Task<bool> DeleteEvent(int eventId)
        {
            var eventToDelete = await _context.Events.Where(e => e.Id == eventId).FirstOrDefaultAsync();

            if (eventToDelete == null)
            {
                throw new NotFoundException("Event requested for deletion not found.");
            }

            var removed = _context.Events.Remove(eventToDelete);
            if (removed != null)
            {
                await _context.SaveChangesAsync();
            }

            return true;
        }

        public async Task<EventDTO> GetEvent(int eventId)
        {
            var response = await _context.Events
                .Where(e => e.Id == eventId)
                .Include(e => e.EventType)
                .FirstOrDefaultAsync();

            if (response == null)
            {
                throw new NotFoundException("Event not found");
            }


            EventDTO eventDto = new()
            {
                Id = response.Id,
                Date = response.Date,
                Organizer = response.Organizer,
                Subject = response.Subject,
                Location = response.Location,
                Description = response.Description,
                Type = new EventTypeDTO
                {
                    Id = response.EventType.Id,
                    Type = response.EventType.Type,
                    Capacity = response.EventType.Capacity,
                    Description = response.EventType.Description,
                    Image = response.EventType.Image
                }
            };
            return eventDto;

        }

        public async Task<List<EventDTO>> GetEvents()
        {
            var response = await _context.Events.Include(e => e.EventType).ToListAsync();

            List<EventDTO> Events = new();
            ServiceResponseDTO<List<EventDTO>> responseDTO = new();

            if (!response.Any())
            {
                throw new NotFoundException("No events found");
            }

            foreach (var item in response)
            {
                Events.Add(new EventDTO
                {
                    Id = item.Id,
                    Date = item.Date,
                    Organizer = item.Organizer,
                    Subject = item.Subject,
                    Location = item.Location,
                    Description = item.Description,
                    Type = new EventTypeDTO
                    {
                        Id = item.EventType.Id,
                        Type = item.EventType.Type,
                        Capacity = item.EventType.Capacity,
                        Description = item.EventType.Description,
                        Image = item.EventType.Image
                    }
                });
            }

            return Events;
        }

        public async Task<List<EventTypeDTO>> GetEventTypes()
        {
            var response = await _context.EventType.ToListAsync();

            List<EventTypeDTO> EventTypes = new();

            if (!response.Any())
            {
                throw new NotFoundException("No event types found");
            }

            foreach (var item in response)
            {
                EventTypes.Add(new EventTypeDTO
                {
                    Id = item.Id,
                    Type = item.Type,
                    Capacity = item.Capacity,
                    Description = item.Description,
                    Image = item.Image,
                });
            }

            return EventTypes;
        }

        public async Task<EventDTO> UpdateEvent(int eventId, EventDTO eventToUpdate)
        {
            var eventEntity = await _context.Events.FirstOrDefaultAsync(i => i.Id == eventId);

            if (eventEntity == null)
            {
                throw new NotFoundException("Event requested for update not found.");
            }

            EventEntity updatedEvent = new()
            {
                Id = eventId,
                Date = eventToUpdate.Date,
                Organizer = eventToUpdate.Organizer,
                Subject = eventToUpdate.Subject,
                Location = eventToUpdate.Location,
                Description = eventToUpdate.Description,
                EventTypeId = eventToUpdate.Type.Id,
            };


            //Overridear värdena som finns i eventEntity med dem nya.
            _context.Entry(eventEntity).CurrentValues.SetValues(updatedEvent);
            await _context.SaveChangesAsync();

            return eventToUpdate;
        }
    }
}
