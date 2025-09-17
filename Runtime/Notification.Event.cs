using UnityEngine;

namespace Foundation {
#if FOUNDATION_NOTIFICATIONCENTER_TYPESAFE
	public partial struct Notification<Sender, Data> {
		internal sealed class Event : INotificationEvent {
			public event Callback received;

			// MARK: - Lifecycle

			public Event() { }

			// MARK: -

			[HideInCallstack]
			public void Post(in Notification<Sender, Data> notification)
				=> received?.Invoke(notification);
		}
	}
#else
	public partial struct Notification {
		internal sealed class Event : INotificationEvent {
			public event Callback received;

			// MARK: - Lifecycle

			public Event() { }

			// MARK: -

			[HideInCallstack]
			public void Post(in Notification notification)
				=> received?.Invoke(notification);
		}
	}
#endif
}