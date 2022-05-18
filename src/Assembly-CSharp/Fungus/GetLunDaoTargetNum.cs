using System;
using UnityEngine;

namespace Fungus
{
	// Token: 0x020013FB RID: 5115
	[CommandInfo("YSNew/Get", "GetLunDaoTargetNum", "获取完成论道数目", 0)]
	[AddComponentMenu("")]
	public class GetLunDaoTargetNum : Command
	{
		// Token: 0x06007C40 RID: 31808 RVA: 0x000546F4 File Offset: 0x000528F4
		public override void OnEnter()
		{
			this.num.Value = Tools.instance.TargetLunTiNum;
			this.Continue();
		}

		// Token: 0x06007C41 RID: 31809 RVA: 0x000113CF File Offset: 0x0000F5CF
		public override Color GetButtonColor()
		{
			return new Color32(184, 210, 235, byte.MaxValue);
		}

		// Token: 0x06007C42 RID: 31810 RVA: 0x000042DD File Offset: 0x000024DD
		public override void OnReset()
		{
		}

		// Token: 0x04006A69 RID: 27241
		[Tooltip("完成论题数目")]
		[VariableProperty(new Type[]
		{
			typeof(IntegerVariable)
		})]
		[SerializeField]
		protected IntegerVariable num;
	}
}
