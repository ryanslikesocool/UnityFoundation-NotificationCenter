using System.Collections.Generic;

namespace Foundation {
	public sealed partial class NotificationCenter {
		public sealed class NotificationEvent {
			private readonly Notification.Name notificationName;

			private List<Callback> observers;

			internal event Callback eventDelegate;

			public event Callback received {
				add => AddObserver(value);
				remove => RemoveObserver(value);
			}

			internal NotificationEvent(in Notification.Name notificationName) {
				this.notificationName = notificationName;
				observers = new List<Callback>();
			}

			internal void Clear()
				=> observers.Clear();

			// MARK: - Post


			/// <summary>
			/// Posts a given notification to the notification center.
			/// </summary>
			/// <param name="notification">The notification to post.</param>
			public void Post(in Notification notification)
				=> eventDelegate?.Invoke(notification);

			/// <summary>
			/// Creates a notification with a given sender and information and posts it to the notification center.
			/// </summary>
			/// <param name="sender">The sender posting the notification.</param>
			/// <param name="data">A optional information about the notification.</param>
			public void Post(in object sender, in object data)
				=> Post(new Notification(this.notificationName, sender, data));

			/// <summary>
			/// Creates a notification with a given sender and posts it to the notification center.
			/// </summary>
			/// <param name="sender">The sender posting the notification.</param>
			public void Post(in object sender)
				=> Post(sender, null);

			/// <summary>
			/// Creates a notification and posts it to the notification center.
			/// </summary>
			public void Post()
				=> Post(null);

			// MARK: - Add Observer

			/// <summary>
			/// Adds an entry to the notification center to receive notifications that passed to the provided block.
			/// </summary>
			/// <param name="block">
			/// The block that executes when receiving a notification.
			///
			/// The notification center copies the block.The notification center strongly holds the copied block until you remove the observer registration.
			///
			/// The block takes one argument: the notification.
			/// </param>
			public void AddObserver(Callback block) {
				observers.Add(block);
				eventDelegate += block;
			}

			// MARK: - Remove Observer

			/// <summary>
			/// Removes matching entries from the notification center's dispatch table.
			/// </summary>
			/// <param name="observer">The block to remove from the dispatch table. Specify a notification observer to remove only entries with this observer.</param>
			public void RemoveObserver(Callback block) {
				observers.Remove(block);
				eventDelegate -= block;
			}

			/// <summary>
			/// Removes all matching entries from the dispatch table.
			/// </summary>
			public void RemoveAllObservers() {
				foreach (Callback observer in observers) {
					eventDelegate -= observer;
				}
			}
		}
	}
}