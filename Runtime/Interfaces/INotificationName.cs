using System;

namespace Foundation {
	/// <summary>
	/// The base interface for a notification name.
	/// </summary>
	/// <remarks>
	/// Do not implement this interface.  Instead, use the existing concrete representation <see cref="Notification.Name"/>.
	/// </remarks>
	public interface INotificationName : IEquatable<INotificationName> { }
}