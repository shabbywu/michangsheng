using System;
using UnityEngine;

namespace Fungus
{
	// Token: 0x020013DC RID: 5084
	[CommandInfo("YSNew/Add", "AddCyFu", "发送传音符", 0)]
	[AddComponentMenu("")]
	public class AddCyFu : Command
	{
		// Token: 0x06007BCC RID: 31692 RVA: 0x0005440E File Offset: 0x0005260E
		public override void OnEnter()
		{
			NpcJieSuanManager.inst.SendFungusCyFu(this.CyType.Value);
			this.Continue();
		}

		// Token: 0x06007BCD RID: 31693 RVA: 0x000113CF File Offset: 0x0000F5CF
		public override Color GetButtonColor()
		{
			return new Color32(184, 210, 235, byte.MaxValue);
		}

		// Token: 0x06007BCE RID: 31694 RVA: 0x000042DD File Offset: 0x000024DD
		public override void OnReset()
		{
		}

		// Token: 0x04006A33 RID: 27187
		[Tooltip("增加npcId")]
		[VariableProperty(new Type[]
		{
			typeof(IntegerVariable)
		})]
		[SerializeField]
		protected IntegerVariable CyType;
	}
}
