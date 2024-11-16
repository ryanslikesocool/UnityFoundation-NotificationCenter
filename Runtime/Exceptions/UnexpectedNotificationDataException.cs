using System;
using System.Runtime.CompilerServices;
using static System.Runtime.CompilerServices.MethodImplOptions;

namespace Foundation {
	public sealed class UnexpectedNotificationDataException : Exception {
		[MethodImpl(AggressiveInlining)]
		public UnexpectedNotificationDataException()
			: base(MESSAGE_STANDARD) { }

		[MethodImpl(AggressiveInlining)]
		public UnexpectedNotificationDataException(in INotification notification)
			: base(string.Format(MESSAGE_FORMAT_RECEIVED, notification)) { }

		[MethodImpl(AggressiveInlining)]
		public UnexpectedNotificationDataException(in Type expected)
			: base(string.Format(MESSAGE_FORMAT_EXPECTED, expected)) { }

		[MethodImpl(AggressiveInlining)]
		public UnexpectedNotificationDataException(in INotification notification, Type expected)
			: base(string.Format(MESSAGE_FORMAT_RECEIVED_EXPECTED, notification, expected)) { }

		// MARK: - Constants

		private const string MESSAGE_STANDARD = "Received a notification with unexpected data.";

		private const string MESSAGE_FORMAT_RECEIVED = @"
Received a notification with unexpected data.
Received: {0}
			";

		private const string MESSAGE_FORMAT_EXPECTED = @"
Received a notification with unexpected data.
Expected: {0}
			";

		private const string MESSAGE_FORMAT_RECEIVED_EXPECTED = @"
Received a notification with unexpected data.
Expected: {1}
Received: {0}
			";
	}
}