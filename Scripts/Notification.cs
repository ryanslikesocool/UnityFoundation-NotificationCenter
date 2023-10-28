using System;
using System.Collections.Generic;
using System.Text;

namespace Foundation {
	public readonly struct Notification : ICustomStringConvertible {
		public readonly Name name;
		public readonly object sender;
		public readonly object data;

		public string description {
			get {
				StringBuilder stringBuilder = new StringBuilder($"name = {name.value}");
				if (sender != null) {
					stringBuilder.AppendFormat(", sender = {0}", sender);
				}
				if (data != null) {
					stringBuilder.AppendFormat(", data = {0}", data);
				}
				return stringBuilder.ToString();
			}
		}

		/// <summary>
		/// Forcefully unwrap the notification data as the given type.
		/// </summary>
		public T ReadData<T>() => (T)data;

		/// <summary>
		/// Attempt to unwrap the notification data as the given type.
		/// </summary>
		/// <typeparam name="T">The type to read the notification data as.</typeparam>
		/// <param name="result"></param>
		/// <returns><see langword="true"/> if the notification data was unwrapped; <see langword="false"/> otherwise.</returns>
		public bool TryReadData<T>(out T result) {
			if (data is T _result) {
				result = _result;
				return true;
			} else {
				result = default;
				return false;
			}
		}

		public Notification(in Name name) {
			this.name = name;
			this.sender = null;
			this.data = null;
		}

		public Notification(in Name name, in object sender) {
			this.name = name;
			this.sender = sender;
			this.data = null;
		}

		public Notification(in Name name, in object sender, in object data) {
			this.name = name;
			this.sender = sender;
			this.data = data;
		}

		public override string ToString() => description;

		public readonly struct Name {
			public readonly string value;

			/// <summary>
			/// Shorthand for adding and removing a notification observer.  The default notification center will be used.
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

			public override bool Equals(object obj) {
				if (obj is Notification.Name other) {
					return value == other.value;
				}
				return false;
			}

			private const string DESCRIPTION_FORMAT = "Notification.Name({0})";
			public override string ToString()
				=> string.Format(DESCRIPTION_FORMAT, value);

			public override int GetHashCode() => value.GetHashCode();

			public static bool operator ==(Name lhs, Name rhs) => lhs.value == rhs.value;
			public static bool operator !=(Name lhs, Name rhs) => lhs.value != rhs.value;
		}
	}
}