using System;
using UnityEngine;

namespace Fungus
{
	// Token: 0x02001449 RID: 5193
	[CommandInfo("YSTools", "打开炼器提交界面", "打开炼器提交界面", 0)]
	[AddComponentMenu("")]
	public class OpenLianQiTiJiao : Command
	{
		// Token: 0x06007D71 RID: 32113 RVA: 0x00011424 File Offset: 0x0000F624
		public override void OnEnter()
		{
			this.Continue();
		}

		// Token: 0x04006AE3 RID: 27363
		[Tooltip("TaskId")]
		[VariableProperty(new Type[]
		{
			typeof(IntegerVariable)
		})]
		[SerializeField]
		protected IntegerVariable TaskId;
	}
}
