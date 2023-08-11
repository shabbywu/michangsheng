using UnityEngine;

namespace Fungus;

[EventHandlerInfo("Input", "Key Pressed", "The block will execute when a key press event occurs.")]
[AddComponentMenu("")]
public class KeyPressed : EventHandler
{
	[Tooltip("The type of keypress to activate on")]
	[SerializeField]
	protected KeyPressType keyPressType;

	[Tooltip("Keycode of the key to activate on")]
	[SerializeField]
	protected KeyCode keyCode;

	protected virtual void Update()
	{
		//IL_001b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0030: Unknown result type (might be due to invalid IL or missing references)
		//IL_0045: Unknown result type (might be due to invalid IL or missing references)
		switch (keyPressType)
		{
		case KeyPressType.KeyDown:
			if (Input.GetKeyDown(keyCode))
			{
				ExecuteBlock();
			}
			break;
		case KeyPressType.KeyUp:
			if (Input.GetKeyUp(keyCode))
			{
				ExecuteBlock();
			}
			break;
		case KeyPressType.KeyRepeat:
			if (Input.GetKey(keyCode))
			{
				ExecuteBlock();
			}
			break;
		}
	}

	public override string GetSummary()
	{
		return ((object)(KeyCode)(ref keyCode)).ToString();
	}
}
