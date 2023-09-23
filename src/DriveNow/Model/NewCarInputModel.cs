using System;
namespace DriveNow.Model
{
	public class NewCarInputModel
	{
		public string NameCar { get; set; }

        public int Power { get; set; }

        public int FromOneToHundred { get; set; }

        public int MaxSpeed { get; set; }

        public int Passengers { get; set; }

        public string Expenditure { get; set; }

        public int Price { get; set; }

        public int Discount { get; set; }

        public string Year { get; set; }

        public string About { get; set; }
        
        public double Latitude { get; set; }
		
        public double Longitude { get; set; }
        
        public string PowerReserve { get; set; }

        public string Category { get; set; }
        public IFormFile FileUrl { get; set; }
    }
}