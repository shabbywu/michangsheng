using System;
using KBEngine;
using UnityEngine;

namespace Fungus
{
	// Token: 0x02001412 RID: 5138
	[CommandInfo("YSNew/Set", "SetLinGen", "设置后续对话", 0)]
	[AddComponentMenu("")]
	public class SetLinGen : Command
	{
		// Token: 0x06007CA1 RID: 31905 RVA: 0x002C52EC File Offset: 0x002C34EC
		public override void OnEnter()
		{
			Avatar player = Tools.instance.getPlayer();
			player.taskMag.setTaskIndex(this.TaskID, this.TaskIndex.Value);
			player.LingGeng[this.TaskID] = this.TaskIndex.Value;
			this.Continue();
		}

		// Token: 0x06007CA2 RID: 31906 RVA: 0x000113CF File Offset: 0x0000F5CF
		public override Color GetButtonColor()
		{
			return new Color32(184, 210, 235, byte.MaxValue);
		}

		// Token: 0x06007CA3 RID: 31907 RVA: 0x000042DD File Offset: 0x000024DD
		public override void OnReset()
		{
		}

		// Token: 0x04006A91 RID: 27281
		[Tooltip("灵根类型")]
		[SerializeField]
		protected int TaskID;

		// Token: 0x04006A92 RID: 27282
		[Tooltip("灵根值")]
		[VariableProperty(new Type[]
		{
			typeof(IntegerVariable)
		})]
		[SerializeField]
		protected IntegerVariable TaskIndex;
	}
}
