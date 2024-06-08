using System;
using System.Runtime.CompilerServices;
using static System.Runtime.CompilerServices.MethodImplOptions;

namespace Foundation {
	public sealed partial class NotificationCenter {
		// MARK: - Unexpected Name

		public sealed class UnexpectedName : Exception {
			[MethodImpl(AggressiveInlining)]
			public UnexpectedName()
				: base(MESSAGE_STANDARD) { }

			[MethodImpl(AggressiveInlining)]
			public UnexpectedName(in Notification notification)
				: base(string.Format(MESSAGE_FORMAT_RECEIVED, notification)) { }

			[MethodImpl(AggressiveInlining)]
			public UnexpectedName(in Notification.Name expected)
				: base(string.Format(MESSAGE_FORMAT_EXPECTED, expected)) { }

			[MethodImpl(AggressiveInlining)]
			public UnexpectedName(in Notification notification, in Notification.Name expected)
				: base(string.Format(MESSAGE_FORMAT_RECEIVED_EXPECTED, notification, expected)) { }

			// MARK: Constants

			private const string MESSAGE_STANDARD = "Received a notification with an unexpected name.";
			private const string MESSAGE_FORMAT_RECEIVED = @"
Received a notification with an unexpected name...
Received: {0}
			";
			private const string MESSAGE_FORMAT_EXPECTED = @"
Received a notification with an unexpected name...
Expected: {0}
			";
			private const string MESSAGE_FORMAT_RECEIVED_EXPECTED = @"
Received a notification with an unexpected name...
Expected: {1}
Received: {0}
			";
		}

		// MARK: - Unexpected Sender

		public sealed class UnexpectedSender : Exception {
			[MethodImpl(AggressiveInlining)]
			public UnexpectedSender()
				: base(MESSAGE_STANDARD) { }

			[MethodImpl(AggressiveInlining)]
			public UnexpectedSender(in Notification notification)
				: base(string.Format(MESSAGE_FORMAT_RECEIVED, notification)) { }

			[MethodImpl(AggressiveInlining)]
			public UnexpectedSender(in Type expected)
				: base(string.Format(MESSAGE_FORMAT_EXPECTED, expected)) { }

			[MethodImpl(AggressiveInlining)]
			public UnexpectedSender(in Notification notification, in Type expected)
				: base(string.Format(MESSAGE_FORMAT_RECEIVED_EXPECTED, notification, expected)) { }

			// MARK: Constants

			private const string MESSAGE_STANDARD = "Received a notification from an unexpected sender.";
			private const string MESSAGE_FORMAT_RECEIVED = @"
Received a notification from an unexpected sender...
Received: {0}
			";
			private const string MESSAGE_FORMAT_EXPECTED = @"
Received a notification from an unexpected sender...
Expected: {0}
			";
			private const string MESSAGE_FORMAT_RECEIVED_EXPECTED = @"
Received a notification from an unexpected sender...
Expected: {1}
Received: {0}
			";
		}

		// MARK: - Unexpected Data

		public sealed class UnexpectedData : Exception {
			[MethodImpl(AggressiveInlining)]
			public UnexpectedData()
				: base(MESSAGE_STANDARD) { }

			[MethodImpl(AggressiveInlining)]
			public UnexpectedData(in Notification notification)
				: base(string.Format(MESSAGE_FORMAT_RECEIVED, notification)) { }

			[MethodImpl(AggressiveInlining)]
			public UnexpectedData(in Type expected)
				: base(string.Format(MESSAGE_FORMAT_EXPECTED, expected)) { }

			[MethodImpl(AggressiveInlining)]
			public UnexpectedData(in Notification notification, Type expected)
				: base(string.Format(MESSAGE_FORMAT_RECEIVED_EXPECTED, notification, expected)) { }

			// MARK: Constants

			private const string MESSAGE_STANDARD = "Received a notification with unexpected data.";
			private const string MESSAGE_FORMAT_RECEIVED = @"
Received a notification with unexpected data...
Received: {0}
			";
			private const string MESSAGE_FORMAT_EXPECTED = @"
Received a notification with unexpected data...
Expected: {0}
			";
			private const string MESSAGE_FORMAT_RECEIVED_EXPECTED = @"
Received a notification with unexpected data...
Expected: {1}
Received: {0}
			";
		}
	}
}