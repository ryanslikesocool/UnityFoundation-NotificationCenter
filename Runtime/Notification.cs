using System.Text;

namespace Foundation {
#if FOUNDATION_NOTIFICATIONCENTER_TYPESAFE
	/// <summary>
	/// A <see cref="Notification"/> contains the data passed around through the <see cref="NotificationCenter"/> API.
	/// </summary>
	/// <typeparam name="Sender">The type of the notification's <see cref="sender"/>.</typeparam>
	/// <typeparam name="Data">The type of the notification's <see cref="data"/>.</typeparam>
	public readonly struct Notification<Sender, Data> : INotification where Sender : class {
		public delegate void Callback(in Notification<Sender, Data> notification);

		/// <summary>
		/// The name of the notification.
		/// </summary>
		public readonly NotificationName<Sender, Data> name;

		/// <summary>
		/// The object that sent the notification.
		/// </summary>
		public readonly Sender sender;

		/// <summary>
		/// Additional data associated with the notification.
		/// </summary>
		public readonly Data data;

		// MARK: - Lifecycle

		/// <summary>
		/// Create new notification data with a name, sender, and data.
		/// </summary>
		/// <param name="name">The name of the notification.</param>
		/// <param name="sender">The object that sent the notification.</param>
		/// <param name="data">Additional data associated with the notification.</param>
		public Notification(in NotificationName<Sender, Data> name, in Sender sender, in Data data) {
			this.name = name;
			this.sender = sender;
			this.data = data;
		}

		/// <summary>
		/// Create new notification data with a name and sender.
		/// </summary>
		/// <param name="name">The name of the notification.</param>
		/// <param name="sender">The object that sent the notification.</param>
		public Notification(in NotificationName<Sender, Data> name, in Sender sender) : this(name: name, sender: sender, data: default) { }

		/// <summary>
		/// Create new notification data with a name.
		/// </summary>
		/// <param name="name">The name of the notification.</param>
		public Notification(in NotificationName<Sender, Data> name) : this(name: name, sender: default, data: default) { }

		// MARK: - Override

		public override string ToString() {
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.AppendFormat(TOSTRING_FORMAT_NAME, name);
			if (sender != null) {
				stringBuilder.AppendFormat(TOSTRING_FORMAT_SENDER, sender);
			}
			if (data != null) {
				stringBuilder.AppendFormat(TOSTRING_FORMAT_DATA, data);
			}
			return stringBuilder.ToString();
		}

		// MARK: - Constants

		private const string TOSTRING_FORMAT_NAME = "name = {0}";
		private const string TOSTRING_FORMAT_SENDER = ", sender = {0}";
		private const string TOSTRING_FORMAT_DATA = ", data = {0}";
	}
#else
	/// <summary>
	/// A <see cref="Notification"/> contains the data passed around through the <see cref="NotificationCenter"/> API.
	/// </summary>
	public readonly partial struct Notification : INotification {
		public delegate void Callback(in Notification notification);

		/// <summary>
		/// The name of the notification.
		/// </summary>
		public readonly NotificationName name;

		/// <summary>
		/// The object that sent the notification.
		/// </summary>
		public readonly object sender;

		/// <summary>
		/// Additional data associated with the notification.
		/// </summary>
		public readonly object data;

		// MARK: - Initializers

		/// <summary>
		/// Create new notification data with a name, sender, and data.
		/// </summary>
		/// <param name="name">The name of the notification.</param>
		/// <param name="sender">The object that sent the notification.</param>
		/// <param name="data">Additional data associated with the notification.</param>
		public Notification(in NotificationName name, in object sender, in object data) {
			this.name = name;
			this.sender = sender;
			this.data = data;
		}

		/// <summary>
		/// Create new notification data with a name and sender.
		/// </summary>
		/// <param name="name">The name of the notification.</param>
		/// <param name="sender">The object that sent the notification.</param>
		public Notification(in NotificationName name, in object sender) : this(name: name, sender: sender, data: default) { }

		/// <summary>
		/// Create new notification data with a name.
		/// </summary>
		/// <param name="name">The name of the notification.</param>
		public Notification(in NotificationName name) : this(name: name, sender: default, data: default) { }

		// MARK: - Sender

		/// <summary>
		/// Forcefully unwrap the notification's <see cref="sender"/> as the given type.
		/// </summary>
		/// <typeparam name="T">The expected type of the notification's <see cref="sender"/>.</typeparam>
		/// <exception cref="UnexpectedNotificationSenderException">When the notification's <see cref="sender"/> is not of the given type.</exception>
		public T ReadSender<T>() {
			if (sender is not T obj) {
				throw new UnexpectedNotificationSenderException(this, typeof(T));
			}
			return obj;
		}

		/// <summary>
		/// Attempt to unwrap the notification's <see cref="sender"/> as the given type.
		/// </summary>
		/// <typeparam name="T">The expected type of the notification's <see cref="sender"/>.</typeparam>
		/// <param name="result">The result of unwrapping the notification's <see cref="sender"/>; <see langword="default"/> if the sender was not of the expected type.</param>
		/// <returns><see langword="true"/> if the notification's <see cref="sender"/> was unwrapped; <see langword="false"/> otherwise.</returns>
		public bool TryReadSender<T>(out T result) {
			if (sender is T _result) {
				result = _result;
				return true;
			} else {
				result = default;
				return false;
			}
		}

		// MARK: - Data

		/// <summary>
		/// Forcefully unwrap the notification's <see cref="data"/> as the given type.
		/// </summary>
		/// <typeparam name="T">The expected type of the notification's <see cref="data"/>.</typeparam>
		/// <exception cref="UnexpectedNotificationDataException">When the notification's <see cref="data"/> is not of the given type.</exception>
		public T ReadData<T>() {
			if (data is not T obj) {
				throw new UnexpectedNotificationDataException(this, typeof(T));
			}
			return obj;
		}

		/// <summary>
		/// Attempt to unwrap the notification's <see cref="data"/> as the given type.
		/// </summary>
		/// <typeparam name="T">The expected type of the notification's <see cref="data"/>.</typeparam>
		/// <param name="result">The result of unwrapping the notification's <see cref="data"/>; <see langword="default"/> if the data was not of the expected type.</param>
		/// <returns><see langword="true"/> if the notification's <see cref="data"/> was unwrapped; <see langword="false"/> otherwise.</returns>
		public bool TryReadData<T>(out T result) {
			if (data is T _result) {
				result = _result;
				return true;
			} else {
				result = default;
				return false;
			}
		}

		// MARK: - Override

		public override string ToString() {
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.AppendFormat(TOSTRING_FORMAT_NAME, name);
			if (sender != null) {
				stringBuilder.AppendFormat(TOSTRING_FORMAT_SENDER, sender);
			}
			if (data != null) {
				stringBuilder.AppendFormat(TOSTRING_FORMAT_DATA, data);
			}
			return stringBuilder.ToString();
		}

		// MARK: - Constants

		private const string TOSTRING_FORMAT_NAME = "name = {0}";
		private const string TOSTRING_FORMAT_SENDER = ", sender = {0}";
		private const string TOSTRING_FORMAT_DATA = ", data = {0}";
	}
#endif
}