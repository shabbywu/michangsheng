using System;
using UnityEngine;

namespace Fungus
{
	// Token: 0x02000F27 RID: 3879
	[CommandInfo("YSNew/Add", "AddCyFuByNpcId", "发送传音符", 0)]
	[AddComponentMenu("")]
	public class AddCyFuByNpcId : Command
	{
		// Token: 0x06006DE5 RID: 28133 RVA: 0x002A400A File Offset: 0x002A220A
		public override void OnEnter()
		{
			NpcJieSuanManager.inst.SendFungusCyByNpcId(this.cyType.Value, this.npcId.Value);
			this.Continue();
		}

		// Token: 0x06006DE6 RID: 28134 RVA: 0x0005E228 File Offset: 0x0005C428
		public override Color GetButtonColor()
		{
			return new Color32(184, 210, 235, byte.MaxValue);
		}

		// Token: 0x06006DE7 RID: 28135 RVA: 0x00004095 File Offset: 0x00002295
		public override void OnReset()
		{
		}

		// Token: 0x04005B62 RID: 23394
		[Tooltip("发送的npcId")]
		[VariableProperty(new Type[]
		{
			typeof(IntegerVariable)
		})]
		[SerializeField]
		protected IntegerVariable npcId;

		// Token: 0x04005B63 RID: 23395
		[Tooltip("发送的c传音符类型")]
		[VariableProperty(new Type[]
		{
			typeof(IntegerVariable)
		})]
		[SerializeField]
		protected IntegerVariable cyType;
	}
}
