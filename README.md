# NotificationCenter
An event system for Unity + C# based on Apple's [NotificationCenter](https://developer.apple.com/documentation/foundation/notificationcenter).

## Installation
**Recommended Installation** (Unity Package Manager)
- "Add package from git URL..."
- `https://github.com/ryanslikesocool/NotificationCenter.git`

**Alternate Installation** (not recommended)
- Get the latest [release](https://github.com/ryanslikesocool/NotificationCenter/releases)
- Import into your project's Plugins folder

## Usage
Add the NotificationCenter component to an object in your scene.  Your scene and the NotificationCenter's lifecycles are tied together.\

Notifications can take in any object and send it to observers.  Just cast it to the desired type on the recieving end.\
Notification objects are optional.  If you don't need to attach an object, just pass in `null`.

```cs
//// to post an event

// the notification name can be stored somewhere so a new string isn't always created
Notification.Name notificationID = new Notification.Name("some identifying string");
int anyObject = 42;

NotificationCenter.Post(notificationID, anyObject);


//// and to recieve

// add an observer with a Notification.Name and a handler
NotificationCenter.AddObserver(notificationID, MyNotificationHandler);

// remove the observer when it doesn't need to recieve any more events
NotificationCenter.RemoveObserver(notificationID, MyNotificationHandler);

public void MyNotificationHandler(Notification notification) {
	// get the notification name, in case you need to check for the sender
	Notification.Name notificationID = notification.name;
	
	// get the object, if desired
	int notificationObject = (int)notification.data; // 42!
	
	// etc...
}
```
