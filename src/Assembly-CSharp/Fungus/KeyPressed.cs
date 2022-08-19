using System;
using UnityEngine;

namespace Fungus
{
	// Token: 0x02000E9F RID: 3743
	[EventHandlerInfo("Input", "Key Pressed", "The block will execute when a key press event occurs.")]
	[AddComponentMenu("")]
	public class KeyPressed : EventHandler
	{
		// Token: 0x06006A0A RID: 27146 RVA: 0x002928B0 File Offset: 0x00290AB0
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

		// Token: 0x06006A0B RID: 27147 RVA: 0x00292915 File Offset: 0x00290B15
		public override string GetSummary()
		{
			return this.keyCode.ToString();
		}

		// Token: 0x040059D6 RID: 22998
		[Tooltip("The type of keypress to activate on")]
		[SerializeField]
		protected KeyPressType keyPressType;

		// Token: 0x040059D7 RID: 22999
		[Tooltip("Keycode of the key to activate on")]
		[SerializeField]
		protected KeyCode keyCode;
	}
}
