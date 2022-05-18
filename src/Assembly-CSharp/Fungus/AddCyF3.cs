using System;
using UnityEngine;

namespace Fungus
{
	// Token: 0x020013DA RID: 5082
	[CommandInfo("YSNew/Add", "第三代传音符发送", "发送传音符", 0)]
	[AddComponentMenu("")]
	public class AddCyF3 : Command
	{
		// Token: 0x06007BC4 RID: 31684 RVA: 0x000543CF File Offset: 0x000525CF
		public override void OnEnter()
		{
			NpcJieSuanManager.inst.SendFungusCy(this.CyId.Value);
			this.Continue();
		}

		// Token: 0x06007BC5 RID: 31685 RVA: 0x000113CF File Offset: 0x0000F5CF
		public override Color GetButtonColor()
		{
			return new Color32(184, 210, 235, byte.MaxValue);
		}

		// Token: 0x06007BC6 RID: 31686 RVA: 0x000042DD File Offset: 0x000024DD
		public override void OnReset()
		{
		}

		// Token: 0x04006A31 RID: 27185
		[Tooltip("发送传音符IdnpcId")]
		[VariableProperty(new Type[]
		{
			typeof(IntegerVariable)
		})]
		[SerializeField]
		protected IntegerVariable CyId;
	}
}
