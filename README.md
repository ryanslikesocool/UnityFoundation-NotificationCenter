# Unity Foundation - Notification Center
A simple event system for Unity + C# inspired by Swift's [NotificationCenter](https://developer.apple.com/documentation/foundation/notificationcenter).\
Notification Center falls under the [Unity Foundation](https://github.com/ryanslikesocool/UnityFoundation/) umbrella.

## NOTICE
**This package is under development and not considered production-ready.**\
Breaking changes are common, documentation is incomplete, and support is limited.  Use at your own risk.

## Installation (Unity Package Manager)
- Select "Add package from git URL..." from the plus (+) menu in the package manager window.
- Paste the package's git url.
```
https://github.com/ryanslikesocool/UnityFoundation-NotificationCenter.git
```

## Usage
```cs
using Foundation;
// NotificationCenter is now available to use!
```

### Notification Centers
More than one notification center may be present in an application, each with its own notifications and observers.

A default shared `NotificationCenter` instance is persistent throughout the lifecycle of a built application.
In the editor, this instance is recreated each time Play mode is entered.
```cs
NotificationCenter defaultNotificationCenter = NotificationCenter.Default;
```

When in the Unity editor, a shared editor instance becomes available.  The editor notification center is persistent thoughout the lifecycle of the editor application.
```cs
NotificationCenter editorNotificationCenter = NotificationCenter.Editor;
```

### Notifications
A `Notification.Name` is the primary identifier for notifications.  Each notification should have a unique name to avoid potential collisions.
It is required to send and receive notifications, and should be initialized once and cached.
```cs
static readonly Notification.Name myNotificationName = new Notification.Name("my super special notification");
```

Events are posted with associated data, stored in a `Notification`.
Each `Notification` has the following properties:
| Type | Name | Description | Optional |
| - | - | - | - |
| `Notification.Name` | `name` | The notification's primary identifier. | No |
| `object` | `sender` | The object that this notification was sent from. | Yes |
| `object` | `data` | Associated data that the sender provides. | Yes |

### Posting Notifications
When posting a notification, the notification name is required.  Any observers that listen for `myNotificationName` will receive the following notification.  The `sender` and `data` parameters are optional.
```cs
void PostMyNotification() {
    // The notification sender can be of any type, but it should ideally be a class.
    object mySender = this;

    // The notification data can be of any type.
    object myData = 42; // int

    NotificationCenter.Default[myNotificationName].Post(mySender, myData);
}
```

### Observing
Processing notification data in observers is as simple as defining a method.
```cs
// `MyObserver` will be called every time it receives a notification with the name `myNotificationName`
// Note the `in` keyword here
void MyObserver(in Notification notification) {
	// Safely unwrap the provided data...
	if(notification.TryReadData(out int integer)) {
        Debug.Log(integer); // 42
	}

    // ... forcefully unwrap it...
	int integer = notification.ReadData(); // 42
	MonoBehaviour monoBehaviour = notification.ReadData(); // error!

	// ... or access it directly.
	object data = notification.data;
}
```

### Adding and Removing Observers
Add and remove notification observers for receiving events with `NotificationCenter.Default[notificationName].receive += ObserverMethod` and `NotificationCenter.Default[notificationName].received -= ObserverMethod`.  In many cases, this is in an object's initialization and deinitialization stages.
```cs
void AddMyObserver() {
    NotificationCenter.Default[myNotificationName].received += MyObserver;
    // `MyObserver` will now receive notifications from `myNotificationName`.
}

void RemoveMyObserver() {
    NotificationCenter.Default[myNotificationName].received -= MyObserver;
    // `MyObserver` will no longer receive notifications from `myNotificationName`.
}
```
A single observer may receive multiple notifications with different names.

### Safety
Multiple exception types are provided to ensure safety.
```cs
void MyObserver(in Notification notification) {
	// Throw an error for an unexpected notification name...
	if (notification.name != myNotificationName) {
		throw new NotificationCenter.UnexpectedName(notification, expected: myNotificationName);
	}

	// ... an unexpected sender...
	if (notification.sender != mySender) {
		throw new NotificationCenter.UnexpectedSender(notification, expected: mySender);
	}

	// ... or unexpected data.
	if (!notification.TryReadData(out int integer)) {
		throw new NotificationCenter.UnexpectedData(notification, expected: typeof(int));
	}

	// Notification name, sender, and data are all valid.
	Debug.Log(integer); // 42
}
```