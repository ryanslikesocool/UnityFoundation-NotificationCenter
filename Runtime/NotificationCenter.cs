using System.Collections.Generic;
using UnityEngine;

namespace Foundation {
	/// <summary>
	/// A notification dispatch mechanism that enables the broadcast of information to registered observers.
	/// </summary>
	public sealed partial class NotificationCenter {
		private static NotificationCenter @default = default;

		/// <summary>
		/// The app’s default notification center.
		/// </summary>
		public static NotificationCenter Default
			=> @default ??= new NotificationCenter();

		public delegate void Callback(in Notification notification);

		private Dictionary<int, NotificationEvent> events;

		public NotificationEvent this[in Notification.Name name]
			=> events[ValidateNotification(name)];

		// MARK: - Initialization

		public NotificationCenter() {
			events = new Dictionary<int, NotificationEvent>();
		}

		// MARK: - Internal

		[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
		private static void Init() {
			@default?.Clear();
			@default = null;
		}

		/// <summary>
		/// Validate that the given notification name has the proper stores created.  If not, create them.
		/// </summary>
		private int ValidateNotification(in Notification.Name name) {
			int nameHash = name.identifier;
			if (!events.ContainsKey(nameHash)) {
				events.Add(nameHash, new NotificationEvent(name));
			}
			return nameHash;
		}

		/// <summary>
		/// Remove all events and obserevers from this notification center.
		/// </summary>
		private void Clear() {
			foreach (int n in events.Keys) {
				events[n].Clear();
			}
			events.Clear();
		}
	}
}