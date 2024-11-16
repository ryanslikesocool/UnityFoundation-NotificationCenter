using System;

namespace Foundation {
#if FOUNDATION_NOTIFICATIONCENTER_TYPESAFE
	public partial struct Notification<Sender, Data> {
		/// <summary>
		/// <see cref="Notification.Name"/> acts as the primary identifier for a <see cref="Notification"/>.
		/// </summary>
		public readonly struct Name : INotificationName, IEquatable<Name>, IEquatable<INotificationName> {
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
			public Name(in string rawValue) {
				this.rawValue = rawValue;
				this.senderType = typeof(Sender);
				this.dataType = typeof(Data);
			}

			// MARK: - IEquatable

			public readonly bool Equals(Name other)
				=> this.rawValue == other.rawValue;

			public readonly bool Equals(INotificationName other) {
				if (other is Name typedOther) {
					return this.Equals(typedOther);
				} else {
					return false;
				}
			}

			// MARK: - Operators

			public static bool operator ==(Name lhs, INotificationName rhs)
				=> lhs.Equals(rhs);

			public static bool operator !=(Name lhs, INotificationName rhs)
				=> !lhs.Equals(rhs);

			public static bool operator ==(INotificationName lhs, Name rhs)
				=> rhs == lhs;

			public static bool operator !=(INotificationName lhs, Name rhs)
				=> rhs != lhs;

			public static bool operator ==(Name lhs, Name rhs)
				=> lhs.Equals(rhs);

			public static bool operator !=(Name lhs, Name rhs)
				=> !lhs.Equals(rhs);

			// MARK: - Override

			public readonly override bool Equals(object obj) => obj switch {
				Name other => this.Equals(other),
				INotificationName other => this.Equals(other),
				_ => false
			};

			public readonly override int GetHashCode()
				=> rawValue.GetHashCode();

			public readonly override string ToString()
				=> string.Format(TOSTRING_FORMAT, typeof(Notification<Sender, Data>.Name), rawValue);

			// MARK: - Constants

			private const string TOSTRING_FORMAT = "{0}({1})";
		}
	}
#else
	public partial struct Notification {
		/// <summary>
		/// <see cref="Notification.Name"/> acts as the primary identifier for a <see cref="Notification"/>.
		/// </summary>
		public readonly struct Name : INotificationName, IEquatable<Name> {
			public readonly string rawValue;

			/// <summary>
			/// Construct a new notification name.
			/// </summary>
			/// <remarks>
			/// Notification names are used as identifiers when posting and receiving notifications.
			/// Only one notification name should be created per-identifier.
			/// </remarks>
			/// <param name="rawValue">The identifier.</param>
			public Name(in string rawValue) {
				this.rawValue = rawValue;
			}

			// MARK: - IEquatable<Name>

			public readonly bool Equals(Name other)
				=> this.rawValue == other.rawValue;

			// MARK: - Operators

			public static bool operator ==(Name lhs, Name rhs)
				=> lhs.rawValue == rhs.rawValue;

			public static bool operator !=(Name lhs, Name rhs)
				=> lhs.rawValue != rhs.rawValue;

			// MARK: - Override

			public readonly override bool Equals(object obj) => obj switch {
				Name other => this.Equals(other),
				_ => false
			};

			public readonly override int GetHashCode() => rawValue.GetHashCode();

			public readonly override string ToString()
				=> string.Format(TOSTRING_FORMAT, typeof(Notification.Name), rawValue);

			// MARK: - Constants

			private const string TOSTRING_FORMAT = "{0}({1})";
		}
	}
#endif
}