using System;
namespace DriveNow.Context
{
	public class Promocode
	{
		public string PromocodeName { get; set; }

		public int Sum { get; set; }

		public Guid PromocodeId { get; set; }
	}
}