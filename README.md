# Unity Foundation - Notification Center
A simple event system for Unity + C#.

## NOTICE
UnityFoundation-NotificationCenter is not considered production-ready.  Breaking changes are common and support is limited.  Use at your own risk.

## Installation
**Recommended Installation** (Unity Package Manager)
- "Add package from git URL..."
- `https://github.com/ryanslikesocool/UnityFoundation-NotificationCenter.git`

**Alternate Installation** (not recommended)
- Get the latest [release](https://github.com/ryanslikesocool/UnityFoundation-NotificationCenter/releases)
- Import into your project's Plugins folder

## Dependencies
UnityFoundation-NotificationCenter has one dependency:
- [Foundation](https://github.com/ryanslikesocool/UnityFoundation)

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
    // notifications will be pushed to `MyObserver` after this is called.
    NotificationCenter.Default.AddObserver(myNotificationName, MyObserver);
}

void RemoveMyObserver() {
    // notifications will no longer be pushed to `MyObserver` after this is called.
    NotificationCenter.Default.RemoveObserver(myNotificationName, MyObserver);
}
```

### Posting Notifications
When posting a notification, the notification name is required.  Any observers that listen for `myNotificationName` will recieve the following notification.  The `sender` and `data` are not required.
```cs
void PostMyNotification() {
    // The notification sender can be of any type.
    object mySender = this;

    // The notification data can be of any type.
    object myData = (int)42;

    NotificationCenter.Default.Post(myNotificationName, mySender, myData);
}
```

### Observing
```cs
// this function will be called every time it recieves a notification with the name `myNotificationName`
// note the `in` keyword here
void MyObserver(in Notification notification) {
    Notification.Name notificationName = notification.name;

    // return early if the notification identifiers are incorrect
    if (notificationName != myNotificationName || notification.sender != mySender) {
        return;
    }

    // get the provided datas
    if (notification.data is int integer) {
        Debug.Log(integer); // -> 42!
    }
}
```
