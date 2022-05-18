using System;
using UnityEngine;

namespace Fungus
{
	// Token: 0x02001424 RID: 5156
	[CommandInfo("YS", "resetWuDao", "重置悟道", 0)]
	[AddComponentMenu("")]
	public class resetWuDao : Command
	{
		// Token: 0x06007CDB RID: 31963 RVA: 0x002C5A38 File Offset: 0x002C3C38
		public override void OnEnter()
		{
			foreach (JSONObject jsonobject in Tools.instance.getPlayer().WuDaoJson.list)
			{
				jsonobject["study"] = new JSONObject(JSONObject.Type.ARRAY);
			}
			this.Continue();
		}

		// Token: 0x06007CDC RID: 31964 RVA: 0x000113CF File Offset: 0x0000F5CF
		public override Color GetButtonColor()
		{
			return new Color32(184, 210, 235, byte.MaxValue);
		}

		// Token: 0x06007CDD RID: 31965 RVA: 0x000042DD File Offset: 0x000024DD
		public override void OnReset()
		{
		}

		// Token: 0x04006AAD RID: 27309
		[Tooltip("描述")]
		[SerializeField]
		protected string desc = "重置所有悟道点数";
	}
}
