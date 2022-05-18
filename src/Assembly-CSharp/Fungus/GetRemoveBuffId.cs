using System;
using UnityEngine;

namespace Fungus
{
	// Token: 0x02001405 RID: 5125
	[CommandInfo("YSNew/Get", "获取当前移除的BuffId", "获取当前移除的Buff", 0)]
	[AddComponentMenu("")]
	public class GetRemoveBuffId : Command
	{
		// Token: 0x06007C6A RID: 31850 RVA: 0x00054724 File Offset: 0x00052924
		public override void OnEnter()
		{
			this.id.Value = RoundManager.instance.curRemoveBuffId;
			this.Continue();
		}

		// Token: 0x06007C6B RID: 31851 RVA: 0x000113CF File Offset: 0x0000F5CF
		public override Color GetButtonColor()
		{
			return new Color32(184, 210, 235, byte.MaxValue);
		}

		// Token: 0x06007C6C RID: 31852 RVA: 0x000042DD File Offset: 0x000024DD
		public override void OnReset()
		{
		}

		// Token: 0x04006A7C RID: 27260
		[Tooltip("buffId")]
		[VariableProperty(new Type[]
		{
			typeof(IntegerVariable)
		})]
		[SerializeField]
		protected IntegerVariable id;
	}
}
