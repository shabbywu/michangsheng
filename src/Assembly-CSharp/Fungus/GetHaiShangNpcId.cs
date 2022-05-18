using System;
using UnityEngine;

namespace Fungus
{
	// Token: 0x020013F4 RID: 5108
	[CommandInfo("YSNew/Get", "GetHaiShangNpcId", "根据静态变量Id获取NPCId", 0)]
	[AddComponentMenu("")]
	public class GetHaiShangNpcId : Command
	{
		// Token: 0x06007C25 RID: 31781 RVA: 0x00054659 File Offset: 0x00052859
		public override void OnEnter()
		{
			this.NpcId.Value = NPCEx.GetSeaNPCID(this.staticId.Value);
			this.Continue();
		}

		// Token: 0x06007C26 RID: 31782 RVA: 0x000113CF File Offset: 0x0000F5CF
		public override Color GetButtonColor()
		{
			return new Color32(184, 210, 235, byte.MaxValue);
		}

		// Token: 0x04006A5E RID: 27230
		[Tooltip("静态变量Id")]
		[VariableProperty(new Type[]
		{
			typeof(IntegerVariable)
		})]
		[SerializeField]
		protected IntegerVariable staticId;

		// Token: 0x04006A5F RID: 27231
		[Tooltip("NpcId存放位置")]
		[VariableProperty(new Type[]
		{
			typeof(IntegerVariable)
		})]
		[SerializeField]
		protected IntegerVariable NpcId;
	}
}
