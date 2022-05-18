using System;
using UnityEngine;

namespace Fungus
{
	// Token: 0x0200143A RID: 5178
	[CommandInfo("YSTools", "IsCyFriend", "检查是否是传音符好友", 0)]
	[AddComponentMenu("")]
	public class IsCyFriend : Command
	{
		// Token: 0x06007D31 RID: 32049 RVA: 0x002C6518 File Offset: 0x002C4718
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

		// Token: 0x06007D32 RID: 32050 RVA: 0x000113CF File Offset: 0x0000F5CF
		public override Color GetButtonColor()
		{
			return new Color32(184, 210, 235, byte.MaxValue);
		}

		// Token: 0x06007D33 RID: 32051 RVA: 0x000042DD File Offset: 0x000024DD
		public override void OnReset()
		{
		}

		// Token: 0x04006ACF RID: 27343
		[Tooltip("npcId")]
		[VariableProperty(new Type[]
		{
			typeof(IntegerVariable)
		})]
		[SerializeField]
		protected IntegerVariable npcId;

		// Token: 0x04006AD0 RID: 27344
		[Tooltip("结果")]
		[VariableProperty(new Type[]
		{
			typeof(BooleanVariable)
		})]
		[SerializeField]
		protected BooleanVariable result;
	}
}
