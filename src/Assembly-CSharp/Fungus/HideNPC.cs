using System;
using UnityEngine;

namespace Fungus
{
	// Token: 0x02001439 RID: 5177
	[CommandInfo("YSTools", "HideNPC", "隐藏NPC界面", 0)]
	[AddComponentMenu("")]
	public class HideNPC : Command
	{
		// Token: 0x06007D2D RID: 32045 RVA: 0x002C64E0 File Offset: 0x002C46E0
		public override void OnEnter()
		{
			GameObject gameObject = GameObject.Find("Canvas/Scroll View/Viewport/Content");
			if (gameObject != null)
			{
				gameObject.transform.localScale = Vector3.zero;
			}
			this.Continue();
		}

		// Token: 0x06007D2E RID: 32046 RVA: 0x000113CF File Offset: 0x0000F5CF
		public override Color GetButtonColor()
		{
			return new Color32(184, 210, 235, byte.MaxValue);
		}

		// Token: 0x06007D2F RID: 32047 RVA: 0x000042DD File Offset: 0x000024DD
		public override void OnReset()
		{
		}

		// Token: 0x04006ACE RID: 27342
		[Tooltip("说明")]
		[SerializeField]
		protected string init = "隐藏NPC界面";
	}
}
