namespace myProject.Models
{
    public class BirdInventory
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public int NumberOfBox  { get; set; }
        public int NumberOfBirds { get; set; }

        public int Total { get; set; }
        public int NumberOfBirdsArrived { get; set; }

        public int NumberOfBoxMortality { get; set; }
        public int NumberOfRuns { get; set; }

        public int NumberOfWeaks {  get; set; }

        public int NumberOfExcess { get; set; }

        public int NumberOfBirdsHoused { get; set; }

    }
}
