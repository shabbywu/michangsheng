using System;
using UnityEngine;

namespace Fungus
{
	// Token: 0x02000DD4 RID: 3540
	[CommandInfo("Camera", "Fullscreen", "Sets the application to fullscreen, windowed or toggles the current state.", 0)]
	[AddComponentMenu("")]
	public class Fullscreen : Command
	{
		// Token: 0x06006491 RID: 25745 RVA: 0x0027F710 File Offset: 0x0027D910
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

		// Token: 0x06006492 RID: 25746 RVA: 0x0027F75B File Offset: 0x0027D95B
		public override string GetSummary()
		{
			return this.fullscreenMode.ToString();
		}

		// Token: 0x06006493 RID: 25747 RVA: 0x0027EC04 File Offset: 0x0027CE04
		public override Color GetButtonColor()
		{
			return new Color32(216, 228, 170, byte.MaxValue);
		}

		// Token: 0x0400566C RID: 22124
		[SerializeField]
		protected FullscreenMode fullscreenMode;
	}
}
