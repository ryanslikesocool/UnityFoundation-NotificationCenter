#if !FOUNDATION_NOTIFICATIONCENTER_TYPESAFE
using System;
using System.Runtime.CompilerServices;
using static System.Runtime.CompilerServices.MethodImplOptions;

namespace Foundation {
	public sealed class UnexpectedNotificationNameException : Exception {
		[MethodImpl(AggressiveInlining)]
		public UnexpectedNotificationNameException()
			: base(MESSAGE_STANDARD) { }

		[MethodImpl(AggressiveInlining)]
		public UnexpectedNotificationNameException(in INotification notification)
			: base(string.Format(MESSAGE_FORMAT_RECEIVED, notification)) { }

		[MethodImpl(AggressiveInlining)]
		public UnexpectedNotificationNameException(in NotificationName expected)
			: base(string.Format(MESSAGE_FORMAT_EXPECTED, expected)) { }

		[MethodImpl(AggressiveInlining)]
		public UnexpectedNotificationNameException(in INotification notification, in NotificationName expected)
			: base(string.Format(MESSAGE_FORMAT_RECEIVED_EXPECTED, notification, expected)) { }

		// MARK: - Constants

		private const string MESSAGE_STANDARD = "Received a notification with an unexpected name.";

		private const string MESSAGE_FORMAT_RECEIVED = @"
Received a notification with an unexpected name.
Received: {0}
			";

		private const string MESSAGE_FORMAT_EXPECTED = @"
Received a notification with an unexpected name.
Expected: {0}
			";

		private const string MESSAGE_FORMAT_RECEIVED_EXPECTED = @"
Received a notification with an unexpected name.
Expected: {1}
Received: {0}
			";
	}
}
#endif