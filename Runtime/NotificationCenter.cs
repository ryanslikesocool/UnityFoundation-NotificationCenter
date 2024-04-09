using System.Collections.Generic;

namespace Foundation {
	/// <summary>
	/// A notification dispatch mechanism that enables the broadcast of information to registered observers.
	/// </summary>
	public sealed partial class NotificationCenter {
		public delegate void Callback(in Notification notification);

		private readonly Dictionary<Notification.Name, NotificationEvent> events;

		public NotificationEvent this[in Notification.Name name]
			=> events[ValidateNotification(name)];

		// MARK: - Initialization

		public NotificationCenter() {
			events = new Dictionary<Notification.Name, NotificationEvent>();
		}

		// MARK: - Internal

		/// <summary>
		/// Validate that the given notification name has the proper stores created.  If not, create them.
		/// </summary>
		private Notification.Name ValidateNotification(in Notification.Name name) {
			if (!events.ContainsKey(name)) {
				events.Add(name, new NotificationEvent(name));
			}
			return name;
		}

		/// <summary>
		/// Remove all events and obserevers from this notification center.
		/// </summary>
		public void Clear() {
			foreach (Notification.Name name in events.Keys) {
				events[name].Clear();
			}
			events.Clear();
		}
	}
}