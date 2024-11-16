namespace Foundation {
	//	public partial struct Notification {
	//		internal sealed class NotificationEvent<Sender, Data> : INotificationEvent where Sender : class {
	//			public event Callback received;
	//
	//			// MARK: - Lifecycle
	//
	//			public NotificationEvent() { }
	//
	//			// MARK: -
	//
	//			public void Post(in Notification notification)
	//				=> received?.Invoke(notification);
	//		}
	//	}

#if FOUNDATION_NOTIFICATIONCENTER_TYPESAFE
	internal sealed class NotificationEvent<Sender, Data> : INotificationEvent where Sender : class {
		public event Notification<Sender, Data>.Callback received;

		// MARK: - Lifecycle

		public NotificationEvent() { }

		// MARK: -

		public void Post(in Notification<Sender, Data> notification)
			=> received?.Invoke(notification);
	}
#else
	internal sealed class NotificationEvent : INotificationEvent {
		public event Notification.Callback received;

		// MARK: - Lifecycle

		public NotificationEvent() { }

		// MARK: -

		public void Post(in Notification notification)
			=> received?.Invoke(notification);
	}
#endif
}