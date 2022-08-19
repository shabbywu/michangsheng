using System;
using UnityEngine;

namespace Fungus
{
	// Token: 0x02000F3E RID: 3902
	[CommandInfo("YSNew/Get", "GetHaiShangNpcId", "根据静态变量Id获取NPCId", 0)]
	[AddComponentMenu("")]
	public class GetHaiShangNpcId : Command
	{
		// Token: 0x06006E3A RID: 28218 RVA: 0x002A48B5 File Offset: 0x002A2AB5
		public override void OnEnter()
		{
			this.NpcId.Value = NPCEx.GetSeaNPCID(this.staticId.Value);
			this.Continue();
		}

		// Token: 0x06006E3B RID: 28219 RVA: 0x0005E228 File Offset: 0x0005C428
		public override Color GetButtonColor()
		{
			return new Color32(184, 210, 235, byte.MaxValue);
		}

		// Token: 0x04005B8C RID: 23436
		[Tooltip("静态变量Id")]
		[VariableProperty(new Type[]
		{
			typeof(IntegerVariable)
		})]
		[SerializeField]
		protected IntegerVariable staticId;

		// Token: 0x04005B8D RID: 23437
		[Tooltip("NpcId存放位置")]
		[VariableProperty(new Type[]
		{
			typeof(IntegerVariable)
		})]
		[SerializeField]
		protected IntegerVariable NpcId;
	}
}
