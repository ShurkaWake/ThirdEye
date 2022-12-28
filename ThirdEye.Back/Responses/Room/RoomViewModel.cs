using ThirdEye.Back.DataAccess.Entities;

namespace ThirdEye.Back.Responses.Room
{
    public sealed record RoomViewModel(
        int id,
        string name,
        RoomState currentState,
        double engagedPercent
        );
}
