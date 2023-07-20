using System;
namespace DriveNow.Context
{
	public class Catogory
	{
		public Guid CategoryId { get; set; }

		public string CategoryName { get; set; }

		public ICollection<Car> Cars { get; set; }

	}
}

