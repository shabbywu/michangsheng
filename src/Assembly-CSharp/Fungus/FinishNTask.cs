using System;
using KBEngine;
using UnityEngine;

namespace Fungus
{
	// Token: 0x02000F6D RID: 3949
	[CommandInfo("YSTask", "FinishNTask", "完成一个杂闻任务", 0)]
	[AddComponentMenu("")]
	public class FinishNTask : Command
	{
		// Token: 0x06006EED RID: 28397 RVA: 0x002A5D64 File Offset: 0x002A3F64
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

		// Token: 0x06006EEE RID: 28398 RVA: 0x0005E228 File Offset: 0x0005C428
		public override Color GetButtonColor()
		{
			return new Color32(184, 210, 235, byte.MaxValue);
		}

		// Token: 0x04005BD8 RID: 23512
		[Tooltip("需要完成的任务ID")]
		[VariableProperty(new Type[]
		{
			typeof(IntegerVariable)
		})]
		[SerializeField]
		protected IntegerVariable NTaskID;
	}
}
