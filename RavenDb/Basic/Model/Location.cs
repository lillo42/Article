namespace Basic.Model
{
    public class Location
    {
        public double Latitude { get; set; }
        public double Longitude { get; set; }

        public override string ToString() 
            => $"Latitude: {Latitude} - Longitude: {Longitude}";
    }
}