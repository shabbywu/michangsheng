using System;
using UnityEngine;

namespace Fungus
{
	// Token: 0x020013DD RID: 5085
	[CommandInfo("YSNew/Add", "AddCyFuByNpcId", "发送传音符", 0)]
	[AddComponentMenu("")]
	public class AddCyFuByNpcId : Command
	{
		// Token: 0x06007BD0 RID: 31696 RVA: 0x0005442B File Offset: 0x0005262B
		public override void OnEnter()
		{
			NpcJieSuanManager.inst.SendFungusCyByNpcId(this.cyType.Value, this.npcId.Value);
			this.Continue();
		}

		// Token: 0x06007BD1 RID: 31697 RVA: 0x000113CF File Offset: 0x0000F5CF
		public override Color GetButtonColor()
		{
			return new Color32(184, 210, 235, byte.MaxValue);
		}

		// Token: 0x06007BD2 RID: 31698 RVA: 0x000042DD File Offset: 0x000024DD
		public override void OnReset()
		{
		}

		// Token: 0x04006A34 RID: 27188
		[Tooltip("发送的npcId")]
		[VariableProperty(new Type[]
		{
			typeof(IntegerVariable)
		})]
		[SerializeField]
		protected IntegerVariable npcId;

		// Token: 0x04006A35 RID: 27189
		[Tooltip("发送的c传音符类型")]
		[VariableProperty(new Type[]
		{
			typeof(IntegerVariable)
		})]
		[SerializeField]
		protected IntegerVariable cyType;
	}
}
