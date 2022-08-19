using System;
using UnityEngine;

namespace Fungus
{
	// Token: 0x02000F26 RID: 3878
	[CommandInfo("YSNew/Add", "发送第二代传音符", "发送第二代传音符", 0)]
	[AddComponentMenu("")]
	public class AddCyFu : Command
	{
		// Token: 0x06006DE1 RID: 28129 RVA: 0x002A3FED File Offset: 0x002A21ED
		public override void OnEnter()
		{
			NpcJieSuanManager.inst.SendFungusCyFu(this.CyType.Value);
			this.Continue();
		}

		// Token: 0x06006DE2 RID: 28130 RVA: 0x0005E228 File Offset: 0x0005C428
		public override Color GetButtonColor()
		{
			return new Color32(184, 210, 235, byte.MaxValue);
		}

		// Token: 0x06006DE3 RID: 28131 RVA: 0x00004095 File Offset: 0x00002295
		public override void OnReset()
		{
		}

		// Token: 0x04005B61 RID: 23393
		[Tooltip("增加npcId")]
		[VariableProperty(new Type[]
		{
			typeof(IntegerVariable)
		})]
		[SerializeField]
		protected IntegerVariable CyType;
	}
}
