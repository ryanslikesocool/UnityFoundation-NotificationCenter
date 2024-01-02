# Unity Foundation - Notification Center
A simple event system for Unity + C#.

## NOTICE
This package is not considered production-ready.  Breaking changes are common and support is limited.  Use at your own risk.

## Installation
**Recommended Installation** (Unity Package Manager)
- "Add package from git URL..."
- `https://github.com/ryanslikesocool/UnityFoundation-NotificationCenter.git`

## Usage
`NotificationCenter` creates a default shared instance the first time it is accessed in C#.

### Notifications
A `Notification.Name` is the primary identifier for notifications.
It is required to send and receive notifications, and should be initialized once and cached.
```cs
static readonly Notification.Name myNotificationName = new Notification.Name("my super special notification");
```

Events are passed around with associated data, stored in a `Notification`.
Each `Notification` has the following properties:
| Property | Description |
| - | - |
| `Notification.Name name` | The notification's primary identifier. |
| `object sender` | The object that this notification was sent from. (Optional) |
| `object data` | Associated data that the sender provides. (Optional) |

### Adding and Removing Observers
Add and remove notification observers for recieving events with `NotificationCenter.Default.AddObserver` and `NotificationCenter.Default.RemoveObserver`.  In most cases, this would be in an object's initialization and deinitialization stages.
```cs
void AddMyObserver() {
    // notifications will be posted to `MyObserver` after this is called.
    NotificationCenter.Default[myNotificationName].received += MyObserver;
}

void RemoveMyObserver() {
    // notifications will no longer be posted to `MyObserver` after this is called.
    NotificationCenter.Default[myNotificationName].received -= MyObserver;
}
```

### Posting Notifications
When posting a notification, the notification name is required.  Any observers that listen for `myNotificationName` will recieve the following notification.  The `sender` and `data` are not required.
```cs
void PostMyNotification() {
    // the notification sender can be of any type, but it should be a class.
    object mySender = this;

    // the notification data can be of any type.
    object myData = 42; // int

    NotificationCenter.Default[myNotificationName].Post(mySender, myData);
}
```

### Observing
```cs
// this function will be called every time it recieves a notification with the name `myNotificationName`
// note the `in` keyword here
void MyObserver(in Notification notification) {
	// safely unwrap the provided data...
	if(notification.TryReadData(out int integer)) {
        Debug.Log(integer); // 42
	}

    // ...forcefully unwrap it...
	int integer = notification.ReadData(); // 42
	MonoBehaviour mb = notification.ReadData(); // error!

	// ...or access it directly
	object data = notification.data;
}
```

### Safety
```cs
void MyObserver(in Notification notification) {
	if (notification.name != myNotificationName) {
		throw new NotificationCenter.UnexpectedName(notification, expected: myNotificationName);
	}
	if (notification.sender != mySender) {
		throw new NotificationCenter.UnexpectedSender(notification, expected: mySender);
	}
	if (!notification.TryReadData(out int integer)) {
		throw new NotificationCenter.UnexpectedData(notification, expected: typeof(int));
	}

	// notification name, sender, and data are all valid
	Debug.Log(integer); // 42
}
```