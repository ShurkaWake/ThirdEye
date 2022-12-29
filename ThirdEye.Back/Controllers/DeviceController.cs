using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using ThirdEye.Back.Controllers.Abstratctions;
using ThirdEye.Back.DataAccess.Contexts;
using ThirdEye.Back.DataAccess.Entities;
using ThirdEye.Back.Requests.Device;
using static ThirdEye.Back.Constants.Wording.DeviceWording;
using static ThirdEye.Back.Constants.ControllerConstants;
using ThirdEye.Back.Extensions;
using Microsoft.AspNetCore.Authorization;

namespace ThirdEye.Back.Controllers
{
    public class DeviceController : AuthorizedControllerBase
    {
        private readonly IStringLocalizer<DeviceController> _localizer;
        private readonly ApplicationContext _context;

        public DeviceController(IStringLocalizer<DeviceController> localizer,
                              ApplicationContext context,
                              UserManager<User> userManager)
            : base(userManager)
        {
            _localizer = localizer;
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> CreateDeviceAsync([FromForm] CreateRequest model)
        {
            var room = await _context.Rooms
                .Include(x => x.BusinessLocated)
                .ThenInclude(x => x.Workers)
                .ThenInclude(x => x.WorkerAccount)
                .Include(x => x.Devices)
                .FirstOrDefaultAsync(x => x.Id == model.RoomId);
            if(room is default(Room))
            {
                return RoomNotFoundMessage.ToNotFoundUsing(_localizer);
            }
            if (!IsManagerOf(room.BusinessLocated))
            {
                return UserIsNotManagerMessage.ToBadRequestUsing(_localizer);
            }

            var device = new Device()
            {
                InstalationRoom = room,
                SerialNumber = model.SerialNumber,
            };

            try
            {
                _context.Devices.Add(device);
                var result = await _context.SaveChangesAsync() > 0;
                if (!result)
                {
                    return UnableToCreateMessage.ToBadRequestUsing(_localizer);
                }
            } 
            catch (Exception ex)
            {
                return UnexpectedErrorMessage.ToBadRequestUsing(_localizer);
            }

            return CreatedAtAction("CreateDevice", device);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteDeviceAsync([FromForm] DeleteRequest model)
        {
            var device = await _context.Devices
                .Include(x => x.InstalationRoom)
                .ThenInclude(x => x.BusinessLocated)
                .ThenInclude(x => x.Workers)
                .ThenInclude(x => x.WorkerAccount)
                .FirstOrDefaultAsync(x => x.SerialNumber == model.SerialNumber);
            if (device is default(Device))
            {
                return DeviceNotFoundMessage.ToNotFoundUsing(_localizer);
            }
            if (!IsManagerOf(device.InstalationRoom.BusinessLocated))
            {
                return UserIsNotManagerMessage.ToBadRequestUsing(_localizer);
            }

            try
            {
                _context.Devices.Remove(device);
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
    }
}
