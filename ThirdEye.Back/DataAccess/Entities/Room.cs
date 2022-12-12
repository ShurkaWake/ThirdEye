namespace ThirdEye.Back.DataAccess.Entities
{
    public class Room
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Institution InstitutionLocated { get; set; }
        public IEnumerable<RoomStateChange> StateChanges { get; set; }
        public IEnumerable<Device> Devices { get; set; }
    }
}
