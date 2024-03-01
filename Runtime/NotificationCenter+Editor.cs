#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;

namespace Foundation {
	public partial class NotificationCenter {
		private static NotificationCenter _editor = default;

		/// <summary>
		/// The notification center for use with editor events.
		/// </summary>
		/// <remarks>
		/// This is not available in builds.
		/// The editor notification center is persistent throughout the lifetime of the Unity editor.
		/// </remarks>
		public static NotificationCenter Editor => _editor;

		[InitializeOnLoadMethod]
		private static void InitEditor() {
			Debug.Log("init editor");
			_editor = new NotificationCenter();
		}
	}
}
#endif