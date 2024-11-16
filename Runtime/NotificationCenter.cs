#if FOUNDATION_NOTIFICATIONCENTER_TYPESAFE
using System;
using System.Linq;
#endif
using System.Collections.Generic;
using UnityEditor;

namespace Foundation {
	/// <summary>
	/// A notification dispatch mechanism that enables the broadcast of information to registered observers.
	/// </summary>
	public sealed partial class NotificationCenter {
#if FOUNDATION_NOTIFICATIONCENTER_TYPESAFE
		private readonly Dictionary<NotificationIdentifier, INotificationEvent> events;
#else
		private readonly Dictionary<Notification.Name, Notification.Event> events;
#endif

		// MARK: - Lifecycle

		public NotificationCenter() {
#if FOUNDATION_NOTIFICATIONCENTER_TYPESAFE
			events = new Dictionary<NotificationIdentifier, INotificationEvent>();
#else
			events = new Dictionary<Notification.Name, Notification.Event>();
#endif
		}

		// MARK: - Internal

#if FOUNDATION_NOTIFICATIONCENTER_TYPESAFE
		/// <summary>
		/// Validate that the given notification name has the proper stores created.  If not, create them.
		/// </summary>
		private Notification<Sender, Data>.Event ValidateNotification<Sender, Data>(in Notification<Sender, Data>.Name name) where Sender : class {
			NotificationIdentifier identifer = new NotificationIdentifier(name, typeof(Sender), typeof(Data));
			Notification<Sender, Data>.Event result;

			if (events.TryGetValue(identifer, out INotificationEvent untypedEvent)) {
				if (untypedEvent is Notification<Sender, Data>.Event typedEvent) {
					result = typedEvent;
				} else {
					//throw new NotificationTypeMismatch();
					throw new NotImplementedException();
				}
			} else {
				result = new Notification<Sender, Data>.Event();
				events.Add(identifer, result);
			}

			return result;
		}
#else
		/// <summary>
		/// Validate that the given notification name has the proper stores created.  If not, create them.
		/// </summary>
		private Notification.Event ValidateNotification(in Notification.Name name) {
			Notification.Event result;

			if (events.TryGetValue(name, out Notification.Event value)) {
				result = value;
			} else {
				result = new Notification.Event();
				events.Add(name, result);
			}

			return result;
		}
#endif

		// MARK: - Post

#if FOUNDATION_NOTIFICATIONCENTER_TYPESAFE
		public void Post<Sender, Data>(in Notification<Sender, Data> notification) where Sender : class {
			Notification<Sender, Data>.Event notificationEvent = ValidateNotification(name: notification.name);
			notificationEvent.Post(notification: notification);
		}

		public void Post<Sender, Data>(in Notification<Sender, Data>.Name name, in Sender sender, in Data data) where Sender : class {
			Notification<Sender, Data> notification = new Notification<Sender, Data>(name: name, sender: sender, data: data);
			Post(notification);
		}

		public void Post<Sender>(in Notification<Sender, byte>.Name name, in Sender sender) where Sender : class
			=> Post(name: name, sender: sender, data: byte.MinValue);

		public void Post(in Notification<object, byte>.Name name)
			=> Post(name: name, sender: (object)null, data: byte.MinValue);
#else
		public void Post(in Notification notification) {
			Notification.Event notificationEvent = ValidateNotification(notification.name);
			notificationEvent.Post(notification);
		}

		public void Post(in Notification.Name name, in object sender, in object data) {
			Notification notification = new Notification(name: name, sender: sender, data: data);
			Post(notification);
		}

		public void Post(in Notification.Name name, in object sender)
			=> Post(name: name, sender: sender, data: null);

		public void Post(in Notification.Name name)
			=> Post(name: name, sender: null, data: null);
#endif

		// MARK: - Add Observer

#if FOUNDATION_NOTIFICATIONCENTER_TYPESAFE
		public void AddObserver<Sender, Data>(in Notification<Sender, Data>.Name name, in Notification<Sender, Data>.Callback callback) where Sender : class {
			Notification<Sender, Data>.Event notificationEvent = ValidateNotification<Sender, Data>(name);
			notificationEvent.received += callback;
		}
#else
		public void AddObserver(in Notification.Name name, in Notification.Callback callback) {
			Notification.Event notificationEvent = ValidateNotification(name);
			notificationEvent.received += callback;
		}
#endif

		// MARK: - Remove Observer

#if FOUNDATION_NOTIFICATIONCENTER_TYPESAFE
		public void RemoveObserver<Sender, Data>(in Notification<Sender, Data>.Name name, in Notification<Sender, Data>.Callback callback) where Sender : class {
			Notification<Sender, Data>.Event notificationEvent = ValidateNotification<Sender, Data>(name);
			notificationEvent.received -= callback;
		}
#else
		public void RemoveObserver(in Notification.Name name, in Notification.Callback callback) {
			Notification.Event notificationEvent = ValidateNotification(name);
			notificationEvent.received -= callback;
		}
#endif

		// MARK: - Utility

#if FOUNDATION_NOTIFICATIONCENTER_TYPESAFE
		public void RemoveAllObservers<Sender, Data>(Notification<Sender, Data>.Name name) where Sender : class {
			NotificationIdentifier[] matchingIdentifiers = events.Keys.Where(key => key.name == name).ToArray();
			foreach (NotificationIdentifier identifier in matchingIdentifiers) {
				events.Remove(identifier);
			}
		}
#else
		public void RemoveAllObservers(Notification.Name name) {
			events.Remove(name);
		}
#endif

		/// <summary>
		/// Remove all events and obserevers from this notification center.
		/// </summary>
		public void Clear() {
			events.Clear();
		}
	}
}