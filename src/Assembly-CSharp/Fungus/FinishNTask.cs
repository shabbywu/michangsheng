using System;
using KBEngine;
using UnityEngine;

namespace Fungus
{
	// Token: 0x02001425 RID: 5157
	[CommandInfo("YSTask", "FinishNTask", "完成一个杂闻任务", 0)]
	[AddComponentMenu("")]
	public class FinishNTask : Command
	{
		// Token: 0x06007CDF RID: 31967 RVA: 0x002C5AA8 File Offset: 0x002C3CA8
		public override void OnEnter()
		{
			Avatar player = Tools.instance.getPlayer();
			if (player.nomelTaskMag.IsNTaskCanFinish(this.NTaskID.Value))
			{
				player.nomelTaskMag.EndNTask(this.NTaskID.Value);
				Debug.Log(string.Format("完成了任务{0}", this.NTaskID.Value));
			}
			this.Continue();
		}

		// Token: 0x06007CE0 RID: 31968 RVA: 0x000113CF File Offset: 0x0000F5CF
		public override Color GetButtonColor()
		{
			return new Color32(184, 210, 235, byte.MaxValue);
		}

		// Token: 0x04006AAE RID: 27310
		[Tooltip("需要完成的任务ID")]
		[VariableProperty(new Type[]
		{
			typeof(IntegerVariable)
		})]
		[SerializeField]
		protected IntegerVariable NTaskID;
	}
}
