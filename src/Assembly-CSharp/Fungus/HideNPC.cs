using System;
using UnityEngine;

namespace Fungus
{
	// Token: 0x02000F83 RID: 3971
	[CommandInfo("YSTools", "HideNPC", "隐藏NPC界面", 0)]
	[AddComponentMenu("")]
	public class HideNPC : Command
	{
		// Token: 0x06006F3F RID: 28479 RVA: 0x002A6A2C File Offset: 0x002A4C2C
		public override void OnEnter()
		{
			GameObject gameObject = GameObject.Find("Canvas/Scroll View/Viewport/Content");
			if (gameObject != null)
			{
				gameObject.transform.localScale = Vector3.zero;
			}
			this.Continue();
		}

		// Token: 0x06006F40 RID: 28480 RVA: 0x0005E228 File Offset: 0x0005C428
		public override Color GetButtonColor()
		{
			return new Color32(184, 210, 235, byte.MaxValue);
		}

		// Token: 0x06006F41 RID: 28481 RVA: 0x00004095 File Offset: 0x00002295
		public override void OnReset()
		{
		}

		// Token: 0x04005BFB RID: 23547
		[Tooltip("说明")]
		[SerializeField]
		protected string init = "隐藏NPC界面";
	}
}
