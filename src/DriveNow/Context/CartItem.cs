using System;
namespace DriveNow.Context
{
	public class CartItem
	{
		public Guid CartItemId { get; set; }

		public Guid UserId { get; set; }

		public Guid RoomId { get; set; }

		public int Price { get; set; }

		public int Count { get; set; }

		public User User { get; set; }
	}
}

