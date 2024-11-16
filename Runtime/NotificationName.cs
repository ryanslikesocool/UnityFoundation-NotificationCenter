using System;

namespace Foundation {
#if FOUNDATION_NOTIFICATIONCENTER_TYPESAFE
	/// <summary>
	/// <see cref="NotificationName"/> acts as the primary identifier for a <see cref="Notification"/>.
	/// </summary>
	public readonly struct NotificationName<Sender, Data> : INotificationName, IEquatable<NotificationName<Sender, Data>>, IEquatable<INotificationName> where Sender : class {
		public readonly string rawValue;
		public readonly Type senderType;
		public readonly Type dataType;

		/// <summary>
		/// Construct a new notification name.
		/// </summary>
		/// <remarks>
		/// Notification names are used as identifiers when posting and receiving notifications.
		/// Only one notification name should be created per-identifier.
		/// </remarks>
		/// <param name="rawValue">The identifier.</param>
		public NotificationName(in string rawValue) {
			this.rawValue = rawValue;
			this.senderType = typeof(Sender);
			this.dataType = typeof(Data);
		}

		// MARK: - IEquatable

		public readonly bool Equals(NotificationName<Sender, Data> other)
			=> this.rawValue == other.rawValue;

		public readonly bool Equals(INotificationName other) {
			if (other is NotificationName<Sender, Data> typedOther) {
				return this.Equals(other);
			} else {
				return false;
			}
		}

		// MARK: - Operators

		public static bool operator ==(NotificationName<Sender, Data> lhs, INotificationName rhs)
			=> lhs.Equals(rhs);

		public static bool operator !=(NotificationName<Sender, Data> lhs, INotificationName rhs)
			=> !lhs.Equals(rhs);

		public static bool operator ==(INotificationName lhs, NotificationName<Sender, Data> rhs)
			=> rhs == lhs;

		public static bool operator !=(INotificationName lhs, NotificationName<Sender, Data> rhs)
			=> rhs != lhs;

		public static bool operator ==(NotificationName<Sender, Data> lhs, NotificationName<Sender, Data> rhs)
			=> lhs.Equals(rhs);

		public static bool operator !=(NotificationName<Sender, Data> lhs, NotificationName<Sender, Data> rhs)
			=> !lhs.Equals(rhs);

		// MARK: - Override

		public readonly override bool Equals(object obj) => obj switch {
			NotificationName<Sender, Data> other => this.Equals(other),
			INotificationName other => this.Equals(other),
			_ => false
		};

		public readonly override int GetHashCode()
			=> rawValue.GetHashCode();

		public readonly override string ToString()
			=> string.Format(TOSTRING_FORMAT, senderType, dataType, rawValue);

		// MARK: - Constants

		private const string TOSTRING_FORMAT = "Notification.Name<{0}, {1}>({2})";
	}
#else
	/// <summary>
	/// <see cref="NotificationName"/> acts as the primary identifier for a <see cref="Notification"/>.
	/// </summary>
	public readonly struct NotificationName : INotificationName, IEquatable<NotificationName> {
		public readonly string rawValue;

		/// <summary>
		/// Construct a new notification name.
		/// </summary>
		/// <remarks>
		/// Notification names are used as identifiers when posting and receiving notifications.
		/// Only one notification name should be created per-identifier.
		/// </remarks>
		/// <param name="rawValue">The identifier.</param>
		public NotificationName(in string rawValue) {
			this.rawValue = rawValue;
		}

		// MARK: - IEquatable<Name>

		public readonly bool Equals(NotificationName other)
			=> this.rawValue == other.rawValue;

		// MARK: - Operators

		public static bool operator ==(NotificationName lhs, NotificationName rhs)
			=> lhs.rawValue == rhs.rawValue;

		public static bool operator !=(NotificationName lhs, NotificationName rhs)
			=> lhs.rawValue != rhs.rawValue;

		// MARK: - Override

		public readonly override bool Equals(object obj) => obj switch {
			NotificationName other => this.Equals(other),
			_ => false
		};

		public readonly override int GetHashCode() => rawValue.GetHashCode();

		public readonly override string ToString()
			=> string.Format(TOSTRING_FORMAT, rawValue);

		// MARK: - Constants

		private const string TOSTRING_FORMAT = "Notification.Name({0})";
	}
#endif
}