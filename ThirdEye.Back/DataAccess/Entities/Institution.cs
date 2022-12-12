namespace ThirdEye.Back.DataAccess.Entities
{
    public class Institution
    {
        public int Id { get; set; } 
        public string Name { get; set; }
        public IEnumerable<InsitutionWorker> Workers { get; set; }
        public IEnumerable<Room> Rooms { get; set; }
    }
}
