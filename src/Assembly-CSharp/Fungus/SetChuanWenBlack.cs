using System;
using UnityEngine;

namespace Fungus
{
	// Token: 0x02001411 RID: 5137
	[CommandInfo("YSNew/Set", "将传闻置灰", "将传闻置灰", 0)]
	[AddComponentMenu("")]
	public class SetChuanWenBlack : Command
	{
		// Token: 0x06007C9F RID: 31903 RVA: 0x000547A1 File Offset: 0x000529A1
		public override void OnEnter()
		{
			Tools.instance.getPlayer().taskMag.SetChuanWenBlack(this.TaskID.Value);
			this.Continue();
		}

		// Token: 0x04006A90 RID: 27280
		[Tooltip("需要置灰的任务的ID")]
		[VariableProperty(new Type[]
		{
			typeof(IntegerVariable)
		})]
		[SerializeField]
		protected IntegerVariable TaskID;
	}
}
