using BISA.Server.Data.DbContexts;
using BISA.Shared.Entities;

namespace BISA.Server.Services.EventService
{
    public class EventService : IEventService
    {
        private readonly BisaDbContext _context;
        private ServiceResponseDTO<EventDTO> responseDTO = new();

        public EventService(BisaDbContext context)
        {
            _context = context;
        }

        public async Task<ServiceResponseDTO<EventDTO>> CreateEvent(EventDTO eventToCreate)
        {
            //Get all events
            var allEvents = await GetEvents();

            //See if event to be created has exact same property data as another event except Id.
            var foundDuplicate = allEvents.Data
                .Any(e => e.Subject.ToLower() == eventToCreate.Subject.ToLower()
                && e.Date.Equals(eventToCreate.Date)
                && e.Location.ToLower() == eventToCreate.Location.ToLower()
                && e.Organizer.ToLower() == eventToCreate.Organizer.ToLower()
                && e.EventTypeId == eventToCreate.EventTypeId);

            if (foundDuplicate)
            {
                responseDTO.Success = false;
                responseDTO.Message = "Event already exists.";
                return responseDTO;
            }

            var eventEntity = new EventEntity()
            {
                Id = eventToCreate.Id,
                Date = eventToCreate.Date,
                Organizer = eventToCreate.Organizer,
                Subject = eventToCreate.Subject,
                Location = eventToCreate.Location,
                EventTypeId = eventToCreate.EventTypeId
            };

            _context.Events.Add(eventEntity);
            await _context.SaveChangesAsync();

            responseDTO.Success = true;
            responseDTO.Message = "Event created";
            return responseDTO;
        }

        public async Task<ServiceResponseDTO<EventDTO>> DeleteEvent(int id)
        {
            var eventToDelete = await _context.Events.Where(e => e.Id == id).FirstOrDefaultAsync();

            if (eventToDelete == null)
            {
                responseDTO.Success = false;
                responseDTO.Message = "Event requested for deletion not found.";
                return responseDTO;
            }

            var removed = _context.Events.Remove(eventToDelete);
            if (removed != null)
            {
                await _context.SaveChangesAsync();
                responseDTO.Success = true;
            }

            return responseDTO;
        }

        public async Task<ServiceResponseDTO<EventDTO>> GetEvent(int id)
        {
            var response = await _context.Events.Where(e => e.Id == id).FirstOrDefaultAsync();

            if (response == null)
            {
                responseDTO.Success = false;
                responseDTO.Message = "Event not found";
                return responseDTO;
            }

            responseDTO.Success = true;
            responseDTO.Data = new EventDTO
            {
                Id = response.Id,
                Date = response.Date,
                Organizer = response.Organizer,
                Subject = response.Subject,
                Location = response.Location,
                EventTypeId = response.EventTypeId
            };
            return responseDTO;

        }

        public async Task<ServiceResponseDTO<List<EventDTO>>> GetEvents()
        {
            var response = await _context.Events.ToListAsync();

            List<EventDTO> Events = new();
            ServiceResponseDTO<List<EventDTO>> responseDTO = new();

            if (response == null)
            {
                //Is an empty list also a result?
                responseDTO.Success = false;
                responseDTO.Message = "No events found";
                responseDTO.Data = null;
                return responseDTO;
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
                    EventTypeId = item.EventTypeId
                });
            }

            responseDTO.Success = true;
            responseDTO.Data = Events;

            return responseDTO;
        }

        public async Task<ServiceResponseDTO<List<EventTypeDTO>>> GetEventTypes()
        {
            var response = await _context.EventType.ToListAsync();

            List<EventTypeDTO> EventTypes = new();
            ServiceResponseDTO<List<EventTypeDTO>> responseDTO = new();

            if (response == null)
            {
                //Is an empty list also a result?
                responseDTO.Success = false;
                responseDTO.Message = "No event types found";
                responseDTO.Data = null;
                return responseDTO;
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

            responseDTO.Success = true;
            responseDTO.Data = EventTypes;

            return responseDTO;
        }

        public async Task<ServiceResponseDTO<EventDTO>> UpdateEvent(EventDTO eventToUpdate)
        {
            var eventEntity = await _context.Events.FirstOrDefaultAsync(i => i.Id == eventToUpdate.Id);

            if (eventEntity == null)
            {
                responseDTO.Success = false;
                responseDTO.Message = "Event requested for update not found.";
                return responseDTO;
            }

            EventEntity updatedEvent = new()
            {
                Id = eventToUpdate.Id,
                Date = eventToUpdate.Date,
                Organizer = eventToUpdate.Organizer,
                Subject = eventToUpdate.Subject,
                Location = eventToUpdate.Location,
                EventTypeId = eventToUpdate.EventTypeId,
            };


            //Overridear värdena som finns i eventEntity med dem nya.
            _context.Entry(eventEntity).CurrentValues.SetValues(updatedEvent);
            await _context.SaveChangesAsync();
            responseDTO.Success = true;
            responseDTO.Data = eventToUpdate;

            return responseDTO;
        }
    }
}
