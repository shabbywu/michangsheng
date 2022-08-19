using System;
using UnityEngine;

namespace Fungus
{
	// Token: 0x02000F24 RID: 3876
	[CommandInfo("YSNew/Add", "第三代传音符发送", "第三代传音符发送", 0)]
	[AddComponentMenu("")]
	public class AddCyF3 : Command
	{
		// Token: 0x06006DD9 RID: 28121 RVA: 0x002A3FAE File Offset: 0x002A21AE
		public override void OnEnter()
		{
			NpcJieSuanManager.inst.SendFungusCy(this.CyId.Value);
			this.Continue();
		}

		// Token: 0x06006DDA RID: 28122 RVA: 0x0005E228 File Offset: 0x0005C428
		public override Color GetButtonColor()
		{
			return new Color32(184, 210, 235, byte.MaxValue);
		}

		// Token: 0x06006DDB RID: 28123 RVA: 0x00004095 File Offset: 0x00002295
		public override void OnReset()
		{
		}

		// Token: 0x04005B5F RID: 23391
		[Tooltip("发送传音符IdnpcId")]
		[VariableProperty(new Type[]
		{
			typeof(IntegerVariable)
		})]
		[SerializeField]
		protected IntegerVariable CyId;
	}
}
