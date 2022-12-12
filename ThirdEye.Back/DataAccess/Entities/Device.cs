namespace ThirdEye.Back.DataAccess.Entities
{
    public class Device
    {
        public int Id { get; set; }
        public string SerialNumber { get; set; }
        public DeviceState LastState { get; set; }
        public Room InstalationRoom { get; set; }
    }
}
