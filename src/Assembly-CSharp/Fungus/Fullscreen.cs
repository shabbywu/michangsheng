using System;
using UnityEngine;

namespace Fungus
{
	// Token: 0x0200120F RID: 4623
	[CommandInfo("Camera", "Fullscreen", "Sets the application to fullscreen, windowed or toggles the current state.", 0)]
	[AddComponentMenu("")]
	public class Fullscreen : Command
	{
		// Token: 0x06007113 RID: 28947 RVA: 0x002A442C File Offset: 0x002A262C
		public override void OnEnter()
		{
			switch (this.fullscreenMode)
			{
			case FullscreenMode.Toggle:
				Screen.fullScreen = !Screen.fullScreen;
				break;
			case FullscreenMode.Fullscreen:
				Screen.fullScreen = true;
				break;
			case FullscreenMode.Windowed:
				Screen.fullScreen = false;
				break;
			}
			this.Continue();
		}

		// Token: 0x06007114 RID: 28948 RVA: 0x0004CD18 File Offset: 0x0004AF18
		public override string GetSummary()
		{
			return this.fullscreenMode.ToString();
		}

		// Token: 0x06007115 RID: 28949 RVA: 0x0004CAB8 File Offset: 0x0004ACB8
		public override Color GetButtonColor()
		{
			return new Color32(216, 228, 170, byte.MaxValue);
		}

		// Token: 0x0400636C RID: 25452
		[SerializeField]
		protected FullscreenMode fullscreenMode;
	}
}
