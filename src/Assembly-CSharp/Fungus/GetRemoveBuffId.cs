using System;
using UnityEngine;

namespace Fungus
{
	// Token: 0x02000F4F RID: 3919
	[CommandInfo("YSNew/Get", "获取当前移除的BuffId", "获取当前移除的Buff", 0)]
	[AddComponentMenu("")]
	public class GetRemoveBuffId : Command
	{
		// Token: 0x06006E7F RID: 28287 RVA: 0x002A5018 File Offset: 0x002A3218
		public override void OnEnter()
		{
			this.id.Value = RoundManager.instance.curRemoveBuffId;
			this.Continue();
		}

		// Token: 0x06006E80 RID: 28288 RVA: 0x0005E228 File Offset: 0x0005C428
		public override Color GetButtonColor()
		{
			return new Color32(184, 210, 235, byte.MaxValue);
		}

		// Token: 0x06006E81 RID: 28289 RVA: 0x00004095 File Offset: 0x00002295
		public override void OnReset()
		{
		}

		// Token: 0x04005BAB RID: 23467
		[Tooltip("buffId")]
		[VariableProperty(new Type[]
		{
			typeof(IntegerVariable)
		})]
		[SerializeField]
		protected IntegerVariable id;
	}
}
