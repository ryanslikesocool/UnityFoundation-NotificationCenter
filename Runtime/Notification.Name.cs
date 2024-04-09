using System;

namespace Foundation {
	public partial struct Notification {
		/// <summary>
		/// <c>Notification.Name</c> acts as the primary identifier for a <c>Notification</c>.
		/// </summary>
		public readonly struct Name : IEquatable<Name> {
			public readonly string rawValue;

			/// <summary>
			/// Construct a new notification name.
			/// </summary>
			/// <remarks>
			/// Notification names are used as identifiers when posting and receiving notifications.
			/// Only one notification name should be created per-identifier.
			/// </remarks>
			/// <param name="value">The identifier.</param>
			public Name(in string value) {
				this.rawValue = value;
			}

			// MARK: - IEquatable<Name>

			public bool Equals(Name other) => this.rawValue == other.rawValue;

			// MARK: - Operators

			public static bool operator ==(Name lhs, Name rhs) => lhs.rawValue == rhs.rawValue;
			public static bool operator !=(Name lhs, Name rhs) => lhs.rawValue != rhs.rawValue;

			// MARK: - Override

			public override bool Equals(object obj) => obj switch {
				Name other => this.Equals(other),
				_ => false
			};

			public override int GetHashCode() => rawValue.GetHashCode();

			private const string DESCRIPTION_FORMAT = "Notification.Name({0})";
			public override string ToString()
				=> string.Format(DESCRIPTION_FORMAT, rawValue);
		}
	}
}