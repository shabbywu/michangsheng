using System;
using KBEngine;
using UnityEngine;

namespace Fungus
{
	// Token: 0x020013F1 RID: 5105
	[CommandInfo("YSNew/Add", "SendChuanYingFu", "发送传音符", 0)]
	[AddComponentMenu("")]
	public class SendChuanYingFu : Command
	{
		// Token: 0x06007C19 RID: 31769 RVA: 0x002C46D0 File Offset: 0x002C28D0
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

		// Token: 0x06007C1A RID: 31770 RVA: 0x000113CF File Offset: 0x0000F5CF
		public override Color GetButtonColor()
		{
			return new Color32(184, 210, 235, byte.MaxValue);
		}

		// Token: 0x06007C1B RID: 31771 RVA: 0x000042DD File Offset: 0x000024DD
		public override void OnReset()
		{
		}

		// Token: 0x04006A59 RID: 27225
		[Tooltip("传音符的ID")]
		[SerializeField]
		protected int ID;

		// Token: 0x04006A5A RID: 27226
		[Tooltip("传音符的ID")]
		[VariableProperty(new Type[]
		{
			typeof(IntegerVariable)
		})]
		[SerializeField]
		protected IntegerVariable ChuanYingID;
	}
}
