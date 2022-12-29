using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using ThirdEye.Back.Controllers.Abstratctions;
using ThirdEye.Back.DataAccess.Contexts;
using ThirdEye.Back.DataAccess.Entities;
using ThirdEye.Back.Extensions;
using ThirdEye.Back.Requests.Room;
using ThirdEye.Back.Responses.Room;
using static ThirdEye.Back.Constants.Wording.RoomWording;
using static ThirdEye.Back.Constants.ControllerConstants;

namespace ThirdEye.Back.Controllers
{
    public class RoomController : AuthorizedControllerBase
    {
        private readonly IStringLocalizer<RoomController> _localizer;
        private readonly ApplicationContext _context;

        public RoomController(IStringLocalizer<RoomController> localizer, 
                              ApplicationContext context,
                              UserManager<User> userManager)
            : base(userManager)
        {
            _localizer = localizer;
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> CreateRoomAsync([FromForm] CreateRequest model)
        {
            var business = await _context.Businesses
                .Include(x => x.Workers)
                .ThenInclude(x => x.WorkerAccount)
                .Include(x => x.Rooms)
                .FirstOrDefaultAsync(x => x.Id == model.BusinessId);

            if (business is default(Business))
            {
                return BusinessNotFoundMessage.ToNotFoundUsing(_localizer);
            }

            if (!IsManagerOf(business))
            {
                return UserIsNotManagerMessage.ToBadRequestUsing(_localizer); 
            }

            var room = new Room()
            {
                BusinessLocated = business,
                Name = model.Name,
                CurrentState = RoomState.Empty,
                LastDeviceResponceTime = DateTime.UtcNow,
            };

            try
            {
                _context.Rooms.Add(room);
                var result = await _context.SaveChangesAsync();
                if (result == 0)
                {
                    return UnableToCreateMessage.ToBadRequestUsing(_localizer);
                }
            }
            catch(Exception ex)
            {
                return UnexpectedErrorMessage.ToBadRequestUsing(_localizer);
            }

            return CreatedAtAction("CreateRoom", room);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteRoomAsync([FromForm] DeleteRequest model)
        {
            var room = await _context.Rooms
                .Include(x => x.BusinessLocated)
                .FirstOrDefaultAsync(x => x.Id == model.Id);
            if(room is default(Room))
            {
                return RoomNotFoundMessage.ToNotFoundUsing(_localizer);
            }

            var business = await _context.Businesses
                .Include(x => x.Workers)
                .ThenInclude(x => x.WorkerAccount)
                .FirstOrDefaultAsync(x => x.Id == room.BusinessLocated.Id);
            if (!IsManagerOf(business))
            {
                return UserIsNotManagerMessage.ToBadRequestUsing(_localizer);
            }

            try
            {
                _context.Rooms.Remove(room);
                var result = await _context.SaveChangesAsync() > 0;
                if (!result)
                {
                    return UnableToDeleteMessage.ToBadRequestUsing(_localizer);
                }
            }
            catch (Exception ex)
            {
                return UnexpectedErrorMessage.ToBadRequestUsing(_localizer);
            }

            return Ok();
        }

        [HttpPatch]
        public async Task<IActionResult> UpdateRoomAsync([FromForm] UpdateRequest model)
        {
            var room = _context.Rooms
                .Include(x => x.BusinessLocated)
                .FirstOrDefault(x => x.Id == model.Id);
            if(room is default(Room))
            {
                return RoomNotFoundMessage.ToBadRequestUsing(_localizer);
            }

            var business = await _context.Businesses
                .Include(x => x.Workers)
                .ThenInclude(x => x.WorkerAccount)
                .FirstOrDefaultAsync(x => x.Id == room.BusinessLocated.Id);
            if (!IsManagerOf(business))
            {
                return UserIsNotManagerMessage.ToBadRequestUsing(_localizer);
            }

            try
            {
                room.Name = model.Name;
                var result = await _context.SaveChangesAsync() > 0;
                if (!result)
                {
                    return UnableToUpdateMessage.ToBadRequestUsing(_localizer);
                }
            }
            catch (Exception ex)
            {
                return UnexpectedErrorMessage.ToBadRequestUsing(_localizer);
            }

            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> GetAllRoomsAsync(int businessId)
        {
            var business = await _context.Businesses
                .Include(x => x.Rooms)
                .ThenInclude(x => x.StateRecords)
                .Include(x => x.Workers)
                .ThenInclude(x => x.WorkerAccount)
                .FirstOrDefaultAsync(x => x.Id == businessId);
            if (business is default(Business))
            {
                return BusinessNotFoundMessage.ToNotFoundUsing(_localizer);
            }
            if (!IsWorkerOf(business))
            {
                return UserIsNotWorkerMessage.ToBadRequestUsing(_localizer);
            }

            return Ok(business.Rooms.Select(x =>
            {
                return new RoomViewModel(
                    x.Id,
                    x.Name,
                    x.CurrentState,
                    100 * (GetCurrentStateSecondsIfUnempty(x) + x.StateRecords
                        .Where(state => (DateTime.UtcNow - state.StartTime) < Day)
                        .Sum(state => state.StateTimeSeconds / (double) Day.TotalSeconds)
                    ));
            }));
        }

        private double GetCurrentStateSecondsIfUnempty(Room room)
        {
            DateTime operationTime = DateTime.UtcNow;
            if (room.CurrentState == RoomState.Unempty)
            {
                return (operationTime - room.LastDeviceResponceTime < StateLiveTime)
                       ? (DateTime.UtcNow - room.LastDeviceResponceTime).TotalSeconds / (double) Day.TotalSeconds
                       : StateLiveTime.TotalSeconds / (double) Day.TotalSeconds;
            }
            return 0.0;
        }
    }
}
