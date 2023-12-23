using System.Text;

namespace Foundation {
	/// <summary>
	/// A <c>Notification</c> contains the data passed around through the <c>NotificationCenter</c> API.
	/// </summary>
	public readonly partial struct Notification {
		public readonly Name name;
		public readonly object sender;
		public readonly object data;

		// MARK: - Initializers

		/// <summary>
		/// Crate new notification data with a name.
		/// </summary>
		public Notification(in Name name) : this(name, null, null) { }

		/// <summary>
		/// Crate new notification data with a name and sender.
		/// </summary>
		public Notification(in Name name, in object sender) : this(name, sender, null) { }

		/// <summary>
		/// Crate new notification data with a name, sender, and additional data.
		/// </summary>
		public Notification(in Name name, in object sender, in object data) {
			this.name = name;
			this.sender = sender;
			this.data = data;
		}

		// MARK: - Sender

		/// <summary>
		/// Forcefully unwrap the notification sender as the given type.
		/// </summary>
		/// <typeparam name="T">The expected type of the notification sender.</typeparam>
		/// <exception cref="NotificationCenter.UnexpectedSender">When the notification sender is not of the given type.</exception>
		public T ReadSender<T>() {
			if (sender is not T obj) {
				throw new NotificationCenter.UnexpectedSender(this, typeof(T));
			}
			return obj;
		}

		/// <summary>
		/// Attempt to unwrap the notification sender as the given type.
		/// </summary>
		/// <typeparam name="T">The expected type of the notification sender.</typeparam>
		/// <param name="result">The result of unwrapping the notification sender; <see langword="default"/> if the sender was not of the expected type.</param>
		/// <returns><see langword="true"/> if the notification sender was unwrapped; <see langword="false"/> otherwise.</returns>
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
		/// Forcefully unwrap the notification data as the given type.
		/// </summary>
		/// <typeparam name="T">The expected type of the notification data.</typeparam>
		/// <exception cref="NotificationCenter.UnexpectedData">When the notification data is not of the given type.</exception>
		public T ReadData<T>() {
			if (data is not T obj) {
				throw new NotificationCenter.UnexpectedData(this, typeof(T));
			}
			return obj;
		}

		/// <summary>
		/// Attempt to unwrap the notification data as the given type.
		/// </summary>
		/// <typeparam name="T">The expected type of the notification data.</typeparam>
		/// <param name="result">The result of unwrapping the notification data; <see langword="default"/> if the data was not of the expected type.</param>
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

		// MARK: - Override

		public override string ToString() {
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
}