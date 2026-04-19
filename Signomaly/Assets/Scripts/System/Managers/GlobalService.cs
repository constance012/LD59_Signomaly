using CSTGames.SharedResources;
using UnityEngine.InputSystem;

namespace AforgeStudios.Signomaly
{
	public sealed class GlobalService : PersistentSingleton<GlobalService>
	{
		private void Start()
		{
			TogglePlayerInput(true);
		}
		
		public void TogglePlayerInput(bool isEnabled)
		{
			if (isEnabled)
			{
				NewInputManager.Instance.GetInputAction(KeybindingAction.Movement).Enable();
				NewInputManager.Instance.GetInputAction(KeybindingAction.Interact).Enable();
			}
			else
			{
				NewInputManager.Instance.GetInputAction(KeybindingAction.Movement).Disable();
				NewInputManager.Instance.GetInputAction(KeybindingAction.Interact).Disable();
			}
		}
	}
}