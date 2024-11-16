#if !FOUNDATION_NOTIFICATIONCENTER_TYPESAFE
using System;
using System.Runtime.CompilerServices;
using static System.Runtime.CompilerServices.MethodImplOptions;

namespace Foundation {
	public sealed class UnexpectedNotificationSenderException : Exception {
		[MethodImpl(AggressiveInlining)]
		public UnexpectedNotificationSenderException()
			: base(MESSAGE_STANDARD) { }

		[MethodImpl(AggressiveInlining)]
		public UnexpectedNotificationSenderException(in Notification notification)
			: base(string.Format(MESSAGE_FORMAT_RECEIVED, notification)) { }

		[MethodImpl(AggressiveInlining)]
		public UnexpectedNotificationSenderException(in Type expected)
			: base(string.Format(MESSAGE_FORMAT_EXPECTED, expected)) { }

		[MethodImpl(AggressiveInlining)]
		public UnexpectedNotificationSenderException(in Notification notification, in Type expected)
			: base(string.Format(MESSAGE_FORMAT_RECEIVED_EXPECTED, notification, expected)) { }

		// MARK: - Constants

		private const string MESSAGE_STANDARD = "Received a notification from an unexpected sender.";

		private const string MESSAGE_FORMAT_RECEIVED = @"
Received a notification from an unexpected sender.
Received: {0}
			";

		private const string MESSAGE_FORMAT_EXPECTED = @"
Received a notification from an unexpected sender.
Expected: {0}
			";

		private const string MESSAGE_FORMAT_RECEIVED_EXPECTED = @"
Received a notification from an unexpected sender.
Expected: {1}
Received: {0}
			";
	}
}
#endif