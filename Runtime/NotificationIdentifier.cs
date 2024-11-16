#if FOUNDATION_NOTIFICATIONCENTER_TYPESAFE
using System;

namespace Foundation {
	internal readonly struct NotificationIdentifier {
		public readonly INotificationName name;
		public readonly Type senderType;
		public readonly Type dataType;

		// MARK: - Lifecycle

		public NotificationIdentifier(in INotificationName name, in Type senderType, in Type dataType) {
			this.name = name;
			this.senderType = senderType;
			this.dataType = dataType;
		}
	}
}
#endif
