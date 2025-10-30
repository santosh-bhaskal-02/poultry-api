namespace myProject.Models
{
    public class FeedInventory
    {

        public int Id { get; set; }

        public DateTime Date { get; set; }

        public string FeedName { get; set; }

        public int NumberOfBagsArrived {  get; set; }

        public string DriverName { get; set; }

        public int DriverPhoneNumber { get; set; }
      
    }
}
