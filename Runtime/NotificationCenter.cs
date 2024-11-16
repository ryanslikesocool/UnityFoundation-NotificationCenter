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
		private readonly Dictionary<NotificationName, NotificationEvent> events;
#endif

		// MARK: - Lifecycle

		public NotificationCenter() {
#if FOUNDATION_NOTIFICATIONCENTER_TYPESAFE
			events = new Dictionary<NotificationIdentifier, INotificationEvent>();
#else
			events = new Dictionary<NotificationName, NotificationEvent>();
#endif
		}

		// MARK: - Internal

#if FOUNDATION_NOTIFICATIONCENTER_TYPESAFE
		/// <summary>
		/// Validate that the given notification name has the proper stores created.  If not, create them.
		/// </summary>
		private NotificationEvent<Sender, Data> ValidateNotification<Sender, Data>(in NotificationName<Sender, Data> name) where Sender : class {
			NotificationIdentifier identifer = new NotificationIdentifier(name, typeof(Sender), typeof(Data));
			NotificationEvent<Sender, Data> result;

			if (events.TryGetValue(identifer, out INotificationEvent untypedEvent)) {
				if (untypedEvent is NotificationEvent<Sender, Data> typedEvent) {
					result = typedEvent;
				} else {
					//throw new NotificationTypeMismatch();
					throw new NotImplementedException();
				}
			} else {
				result = new NotificationEvent<Sender, Data>();
				events.Add(identifer, result);
			}

			return result;
		}
#else
		/// <summary>
		/// Validate that the given notification name has the proper stores created.  If not, create them.
		/// </summary>
		private NotificationEvent ValidateNotification(in NotificationName name) {
			NotificationEvent result;

			if (events.TryGetValue(name, out NotificationEvent value)) {
				result = value;
			} else {
				result = new NotificationEvent();
				events.Add(name, result);
			}

			return result;
		}
#endif

		// MARK: - Post

#if FOUNDATION_NOTIFICATIONCENTER_TYPESAFE
		public void Post<Sender, Data>(in Notification<Sender, Data> notification) where Sender : class {
			NotificationEvent<Sender, Data> notificationEvent = ValidateNotification<Sender, Data>(name: notification.name);
			notificationEvent.Post(notification: notification);
		}

		public void Post<Sender, Data>(in NotificationName<Sender, Data> name, in Sender sender, in Data data) where Sender : class {
			Notification<Sender, Data> notification = new Notification<Sender, Data>(name: name, sender: sender, data: data);
			Post(notification);
		}

		public void Post<Sender>(in NotificationName<Sender, byte> name, in Sender sender) where Sender : class
			=> Post(name: name, sender: sender, data: byte.MinValue);

		public void Post(in NotificationName<object, byte> name)
			=> Post(name: name, sender: (object)null, data: byte.MinValue);
#else
		public void Post(in Notification notification) {
			NotificationEvent notificationEvent = ValidateNotification(notification.name);
			notificationEvent.Post(notification);
		}

		public void Post(in NotificationName name, in object sender, in object data) {
			Notification notification = new Notification(name: name, sender: sender, data: data);
			Post(notification);
		}

		public void Post(in NotificationName name, in object sender)
			=> Post(name: name, sender: sender, data: null);

		public void Post(in NotificationName name)
			=> Post(name: name, sender: null, data: null);
#endif

		// MARK: - Add Observer

#if FOUNDATION_NOTIFICATIONCENTER_TYPESAFE
		public void AddObserver<Sender, Data>(in NotificationName<Sender, Data> name, in Notification<Sender, Data>.Callback callback) where Sender : class {
			NotificationEvent<Sender, Data> notificationEvent = ValidateNotification<Sender, Data>(name);
			notificationEvent.received += callback;
		}
#else
		public void AddObserver(in NotificationName name, in Notification.Callback callback) {
			NotificationEvent notificationEvent = ValidateNotification(name);
			notificationEvent.received += callback;
		}
#endif

		// MARK: - Remove Observer

#if FOUNDATION_NOTIFICATIONCENTER_TYPESAFE
		public void RemoveObserver<Sender, Data>(in NotificationName<Sender, Data> name, in Notification<Sender, Data>.Callback callback) where Sender : class {
			NotificationEvent<Sender, Data> notificationEvent = ValidateNotification<Sender, Data>(name);
			notificationEvent.received -= callback;
		}
#else
		public void RemoveObserver(in NotificationName name, in Notification.Callback callback) {
			NotificationEvent notificationEvent = ValidateNotification(name);
			notificationEvent.received -= callback;
		}
#endif

		// MARK: - Utility

#if FOUNDATION_NOTIFICATIONCENTER_TYPESAFE
		public void RemoveAllObservers<Sender, Data>(NotificationName<Sender, Data> name) where Sender : class {
			NotificationIdentifier[] matchingIdentifiers = events.Keys.Where(key => key.name == name).ToArray();
			foreach (NotificationIdentifier identifier in matchingIdentifiers) {
				events.Remove(identifier);
			}
		}

		public void RemoveAllObservers(in INotificationName name, in Type senderType, in Type dataType) {
			NotificationIdentifier identifier = new NotificationIdentifier(name: name, senderType: senderType, dataType: dataType);
			events.Remove(identifier);
		}

		public void RemoveAllObservers<Sender, Data>(in NotificationName<Sender, Data> name) where Sender : class
			=> RemoveAllObservers(name: name, senderType: typeof(Sender), dataType: typeof(Data));
#else
		public void RemoveAllObservers(NotificationName name) {
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