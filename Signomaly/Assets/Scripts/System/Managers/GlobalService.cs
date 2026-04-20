using CSTGames.SharedResources;
using UnityEngine;
using UnityEngine.InputSystem;

namespace AforgeStudios.Signomaly
{
	public sealed class GlobalService : PersistentSingleton<GlobalService>
	{	
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

		public void ToggleLockCursor(bool isLocked)
		{
			if (isLocked)
			{
				Cursor.lockState = CursorLockMode.Locked;
				Cursor.visible = false;
			}
			else
			{
				Cursor.lockState = CursorLockMode.None;
				Cursor.visible = true;
			}
		}
	}
}