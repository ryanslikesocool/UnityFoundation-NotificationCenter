using System;

namespace Foundation {
	public partial struct Notification {
		public sealed class UnexpectedData : Exception {
			public UnexpectedData() { }

			public UnexpectedData(in Notification notification) : base($"Got unexpected data from a notification: {notification}") { }

			public UnexpectedData(in Notification notification, Type expected) : base($"Got unexpected data from a notification (expected {expected}) : {notification}") { }
		}
	}
}