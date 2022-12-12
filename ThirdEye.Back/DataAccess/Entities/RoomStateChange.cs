namespace ThirdEye.Back.DataAccess.Entities
{
    public class RoomStateChange
    {
        public int Id { get; set; }
        public DateTime ChangeTime { get; set; }
        public RoomState State { get; set; }
        public Room RoomChanged { get; set; }
    }
}
