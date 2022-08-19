using System;
using UnityEngine;

namespace Fungus
{
	// Token: 0x02000E52 RID: 3666
	[CommandInfo("Camera", "Stop Swipe", "Deactivates swipe panning mode.", 0)]
	[AddComponentMenu("")]
	public class StopSwipe : Command
	{
		// Token: 0x06006707 RID: 26375 RVA: 0x002887AF File Offset: 0x002869AF
		public override void OnEnter()
		{
			FungusManager.Instance.CameraManager.StopSwipePan();
			this.Continue();
		}

		// Token: 0x06006708 RID: 26376 RVA: 0x0027EC04 File Offset: 0x0027CE04
		public override Color GetButtonColor()
		{
			return new Color32(216, 228, 170, byte.MaxValue);
		}
	}
}
