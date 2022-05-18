using System;
using UnityEngine;

namespace Fungus
{
	// Token: 0x020012A3 RID: 4771
	[CommandInfo("Camera", "Stop Swipe", "Deactivates swipe panning mode.", 0)]
	[AddComponentMenu("")]
	public class StopSwipe : Command
	{
		// Token: 0x06007395 RID: 29589 RVA: 0x0004EDA5 File Offset: 0x0004CFA5
		public override void OnEnter()
		{
			FungusManager.Instance.CameraManager.StopSwipePan();
			this.Continue();
		}

		// Token: 0x06007396 RID: 29590 RVA: 0x0004CAB8 File Offset: 0x0004ACB8
		public override Color GetButtonColor()
		{
			return new Color32(216, 228, 170, byte.MaxValue);
		}
	}
}
