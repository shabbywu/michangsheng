using System;
using UnityEngine;

namespace Fungus
{
	// Token: 0x02000F84 RID: 3972
	[CommandInfo("YSTools", "IsCyFriend", "检查是否是传音符好友", 0)]
	[AddComponentMenu("")]
	public class IsCyFriend : Command
	{
		// Token: 0x06006F43 RID: 28483 RVA: 0x002A6A78 File Offset: 0x002A4C78
		public override void OnEnter()
		{
			if (Tools.instance.getPlayer().emailDateMag.IsFriend(this.npcId.Value))
			{
				this.result.Value = true;
			}
			else
			{
				this.result.Value = false;
			}
			this.Continue();
		}

		// Token: 0x06006F44 RID: 28484 RVA: 0x0005E228 File Offset: 0x0005C428
		public override Color GetButtonColor()
		{
			return new Color32(184, 210, 235, byte.MaxValue);
		}

		// Token: 0x06006F45 RID: 28485 RVA: 0x00004095 File Offset: 0x00002295
		public override void OnReset()
		{
		}

		// Token: 0x04005BFC RID: 23548
		[Tooltip("npcId")]
		[VariableProperty(new Type[]
		{
			typeof(IntegerVariable)
		})]
		[SerializeField]
		protected IntegerVariable npcId;

		// Token: 0x04005BFD RID: 23549
		[Tooltip("结果")]
		[VariableProperty(new Type[]
		{
			typeof(BooleanVariable)
		})]
		[SerializeField]
		protected BooleanVariable result;
	}
}
