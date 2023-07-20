using System;
namespace DriveNow.Context
{
	public class OrderItem
	{
		public Guid OrderId { get; set; }

		public Guid RoomId { get; set; }

		public Guid UserId { get; set; }

		public Guid OrderItemId { get; set; }

		public int Count { get; set; }

		public int Price { get; set; }

		public Order Order { get; set; }
	}
}

