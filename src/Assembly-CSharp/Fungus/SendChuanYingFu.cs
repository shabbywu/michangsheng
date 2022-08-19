using System;
using KBEngine;
using UnityEngine;

namespace Fungus
{
	// Token: 0x02000F3B RID: 3899
	[CommandInfo("YSNew/Add", "发送第一代传音符", "发送第一代传音符", 0)]
	[AddComponentMenu("")]
	public class SendChuanYingFu : Command
	{
		// Token: 0x06006E2E RID: 28206 RVA: 0x002A468C File Offset: 0x002A288C
		public override void OnEnter()
		{
			Avatar player = Tools.instance.getPlayer();
			if (this.ID != 0)
			{
				player.chuanYingManager.addChuanYingFu(this.ID);
			}
			else
			{
				player.chuanYingManager.addChuanYingFu(this.ChuanYingID.Value);
			}
			player.updateChuanYingFu();
			this.Continue();
		}

		// Token: 0x06006E2F RID: 28207 RVA: 0x0005E228 File Offset: 0x0005C428
		public override Color GetButtonColor()
		{
			return new Color32(184, 210, 235, byte.MaxValue);
		}

		// Token: 0x06006E30 RID: 28208 RVA: 0x00004095 File Offset: 0x00002295
		public override void OnReset()
		{
		}

		// Token: 0x04005B87 RID: 23431
		[Tooltip("传音符的ID")]
		[SerializeField]
		protected int ID;

		// Token: 0x04005B88 RID: 23432
		[Tooltip("传音符的ID")]
		[VariableProperty(new Type[]
		{
			typeof(IntegerVariable)
		})]
		[SerializeField]
		protected IntegerVariable ChuanYingID;
	}
}
