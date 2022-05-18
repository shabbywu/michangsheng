using System;
using UnityEngine;

namespace Fungus
{
	// Token: 0x0200131E RID: 4894
	[EventHandlerInfo("Input", "Key Pressed", "The block will execute when a key press event occurs.")]
	[AddComponentMenu("")]
	public class KeyPressed : EventHandler
	{
		// Token: 0x06007740 RID: 30528 RVA: 0x002B510C File Offset: 0x002B330C
		protected virtual void Update()
		{
			switch (this.keyPressType)
			{
			case KeyPressType.KeyDown:
				if (Input.GetKeyDown(this.keyCode))
				{
					this.ExecuteBlock();
					return;
				}
				break;
			case KeyPressType.KeyUp:
				if (Input.GetKeyUp(this.keyCode))
				{
					this.ExecuteBlock();
					return;
				}
				break;
			case KeyPressType.KeyRepeat:
				if (Input.GetKey(this.keyCode))
				{
					this.ExecuteBlock();
				}
				break;
			default:
				return;
			}
		}

		// Token: 0x06007741 RID: 30529 RVA: 0x000513ED File Offset: 0x0004F5ED
		public override string GetSummary()
		{
			return this.keyCode.ToString();
		}

		// Token: 0x040067F0 RID: 26608
		[Tooltip("The type of keypress to activate on")]
		[SerializeField]
		protected KeyPressType keyPressType;

		// Token: 0x040067F1 RID: 26609
		[Tooltip("Keycode of the key to activate on")]
		[SerializeField]
		protected KeyCode keyCode;
	}
}
