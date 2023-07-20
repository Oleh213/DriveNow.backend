using System;
namespace DriveNow.Context
{
	public class Car
	{ 
		public Guid CarId { get; set; }

		public string NameCar { get; set; }

		public int Power { get; set; }

		public int FromOneToHundred { get; set; }

		public int MaxSpeed { get; set; }

		public int Passengers { get; set; }

		public string Expenditure { get; set; }

		public int Price { get; set; }

		public int? Discount { get; set; }

		public string Year { get; set; }

		public string About { get; set; }

        public string AccualFileUrl { get; set; }

		public Guid CategoryForId { get; set; }

		public Catogory Catogories { get; set; }
    }
}