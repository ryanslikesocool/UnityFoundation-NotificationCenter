using UnityEngine;

namespace Foundation {
	public sealed partial class NotificationCenter {
		private static NotificationCenter _default = default;

		/// <summary>
		/// The app’s default notification center.
		/// </summary>
		/// <remarks>
		/// In the Unity Editor, the default notification center is recreated each time you enter play mode.
		/// </remarks>
		public static NotificationCenter Default => _default;

		[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
		private static void InitRuntime() {
			_default = new NotificationCenter();
		}
	}
}