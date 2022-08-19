using System;
using UnityEngine;

namespace Fungus
{
	// Token: 0x02000F96 RID: 3990
	[CommandInfo("YSTools", "打开炼器提交界面", "打开炼器提交界面", 0)]
	[AddComponentMenu("")]
	public class OpenLianQiTiJiao : Command
	{
		// Token: 0x06006F8A RID: 28554 RVA: 0x0005E3AF File Offset: 0x0005C5AF
		public override void OnEnter()
		{
			this.Continue();
		}

		// Token: 0x04005C14 RID: 23572
		[Tooltip("TaskId")]
		[VariableProperty(new Type[]
		{
			typeof(IntegerVariable)
		})]
		[SerializeField]
		protected IntegerVariable TaskId;
	}
}
