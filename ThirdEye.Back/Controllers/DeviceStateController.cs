using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using ThirdEye.Back.DataAccess.Contexts;
using ThirdEye.Back.DataAccess.Entities;
using ThirdEye.Back.Requests.DeviceState;
using static ThirdEye.Back.Constants.ControllerConstants;

namespace ThirdEye.Back.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DeviceStateController : ControllerBase
    {
        private readonly ApplicationContext _context;

        public DeviceStateController(ApplicationContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> PostStateAsync(PostStateRequest model)
        {
            var device = await _context.Devices
                .Include(x => x.InstalationRoom)
                .ThenInclude(x => x.StateRecords)
                .FirstOrDefaultAsync(x => x.SerialNumber == model.SerialNumber);
            if (device is default(Device))
            {
                return BadRequest();
            }

            var operationTime = DateTime.UtcNow;
            var stateTime = operationTime - device.InstalationRoom.LastDeviceResponceTime;

            if (model.State != device.InstalationRoom.CurrentState
                && device.InstalationRoom.CurrentState == RoomState.Unempty
                && stateTime < StateLiveTime)
            {
                return Ok();
            }

            try
            {
                var roomState = new RoomStateRecord()
                {
                    Room = device.InstalationRoom,
                    StartTime = device.InstalationRoom.LastDeviceResponceTime,
                    State = model.State,
                    StateTimeSeconds = stateTime < StateLiveTime
                                       ? (int)stateTime.TotalSeconds
                                       : (int)StateLiveTime.TotalSeconds,
                };
                _context.RoomsStateHistories.Add(roomState);
                device.InstalationRoom.CurrentState = model.State;
                device.InstalationRoom.LastDeviceResponceTime = operationTime;

                var result = await _context.SaveChangesAsync() > 0;
                if (!result)
                {
                    return BadRequest();
                }
            }
            catch (Exception ex)
            {
                return BadRequest();
            }

            return Ok();
        }
    }
}
