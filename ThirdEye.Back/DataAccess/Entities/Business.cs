namespace ThirdEye.Back.DataAccess.Entities
{
    public class Business
    {
        public int Id { get; set; } 
        public string Name { get; set; }
        public IEnumerable<BusinessWorker> Workers { get; set; }
        public IEnumerable<Room> Rooms { get; set; }
    }
}
