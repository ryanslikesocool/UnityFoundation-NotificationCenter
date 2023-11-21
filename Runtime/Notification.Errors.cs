using System;

namespace Foundation {
	public sealed partial class NotificationCenter {
		public sealed class UnexpectedName : Exception {
			public UnexpectedName() { }

			public UnexpectedName(in Notification notification) : base($@"
Received a notification with an unexpected notification name...
Received: {notification}") { }

			public UnexpectedName(in Notification notification, in Notification.Name expected) : base($@"
Received a notification with an unexpected notification name...
Expected: {expected}
Received: {notification}
") { }
		}

		public sealed class UnexpectedSender : Exception {
			public UnexpectedSender() { }

			public UnexpectedSender(in Notification notification) : base($@"
Received a notification from an unexpected sender...
Received: {notification}
") { }

			public UnexpectedSender(in Notification notification, in Type expected) : base($@"
Received a notification from an unexpected sender...
Expected: {expected}
Received: {notification}
") { }
		}

		public sealed class UnexpectedData : Exception {
			public UnexpectedData() { }

			public UnexpectedData(in Notification notification) : base($@"
Received a notification with unexpected data...
Received: {notification}
") { }

			public UnexpectedData(in Notification notification, Type expected) : base($@"
Received a notification with unexpected data...
Expected: {expected}
Received: {notification}
") { }
		}
	}
}