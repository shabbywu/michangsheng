using System;
using UnityEngine;

namespace Fungus
{
	// Token: 0x02000F6C RID: 3948
	[CommandInfo("YS", "resetWuDao", "重置悟道", 0)]
	[AddComponentMenu("")]
	public class resetWuDao : Command
	{
		// Token: 0x06006EE9 RID: 28393 RVA: 0x002A5CE0 File Offset: 0x002A3EE0
		public override void OnEnter()
		{
			foreach (JSONObject jsonobject in Tools.instance.getPlayer().WuDaoJson.list)
			{
				jsonobject["study"] = new JSONObject(JSONObject.Type.ARRAY);
			}
			this.Continue();
		}

		// Token: 0x06006EEA RID: 28394 RVA: 0x0005E228 File Offset: 0x0005C428
		public override Color GetButtonColor()
		{
			return new Color32(184, 210, 235, byte.MaxValue);
		}

		// Token: 0x06006EEB RID: 28395 RVA: 0x00004095 File Offset: 0x00002295
		public override void OnReset()
		{
		}

		// Token: 0x04005BD7 RID: 23511
		[Tooltip("描述")]
		[SerializeField]
		protected string desc = "重置所有悟道点数";
	}
}
