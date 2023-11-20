using System;

namespace Foundation {
	public partial struct Notification {
		/// <summary>
		/// <c>Notification.Name</c> acts as the primary identifier for a <c>Notification</c>.
		/// </summary>
		public readonly struct Name : IEquatable<Name> {
			public readonly string value;

			/// <summary>
			/// Shorthand for adding and removing a notification observer.  <c>NotificationCenter.Default</c> will be used.
			/// </summary>
			public event NotificationCenter.Callback received {
				add => NotificationCenter.Default.AddObserver(this, value);
				remove => NotificationCenter.Default.RemoveObserver(this, value);
			}

			/// <summary>
			/// Construct a new notification name.
			/// </summary>
			/// <remarks>
			/// Notification names are used as identifiers when posting and receiving notifications.
			/// Only one notification name should be created per-identifier.
			/// </remarks>
			/// <param name="value">The identifier.</param>
			public Name(in string value) {
				this.value = value;
			}

			// MARK: - IEquatable<Name>

			public bool Equals(Name other) => this.value == other.value;

			// MARK: - Operators

			public static bool operator ==(Name lhs, Name rhs) => lhs.value == rhs.value;
			public static bool operator !=(Name lhs, Name rhs) => lhs.value != rhs.value;

			// MARK: - Override

			public override bool Equals(object obj) => obj switch {
				Name other => this.Equals(other),
				string other => this.value == other,
				_ => false
			};

			public override int GetHashCode() => value.GetHashCode();

			private const string DESCRIPTION_FORMAT = "Notification.Name({0})";
			public override string ToString()
				=> string.Format(DESCRIPTION_FORMAT, value);
		}
	}
}