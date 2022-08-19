using System;
using KBEngine;
using UnityEngine;

namespace Fungus
{
	// Token: 0x02000F5C RID: 3932
	[CommandInfo("YSNew/Set", "SetLinGen", "设置后续对话", 0)]
	[AddComponentMenu("")]
	public class SetLinGen : Command
	{
		// Token: 0x06006EB1 RID: 28337 RVA: 0x002A5464 File Offset: 0x002A3664
		public override void OnEnter()
		{
			Avatar player = Tools.instance.getPlayer();
			player.taskMag.setTaskIndex(this.TaskID, this.TaskIndex.Value);
			player.LingGeng[this.TaskID] = this.TaskIndex.Value;
			this.Continue();
		}

		// Token: 0x06006EB2 RID: 28338 RVA: 0x0005E228 File Offset: 0x0005C428
		public override Color GetButtonColor()
		{
			return new Color32(184, 210, 235, byte.MaxValue);
		}

		// Token: 0x06006EB3 RID: 28339 RVA: 0x00004095 File Offset: 0x00002295
		public override void OnReset()
		{
		}

		// Token: 0x04005BBF RID: 23487
		[Tooltip("灵根类型")]
		[SerializeField]
		protected int TaskID;

		// Token: 0x04005BC0 RID: 23488
		[Tooltip("灵根值")]
		[VariableProperty(new Type[]
		{
			typeof(IntegerVariable)
		})]
		[SerializeField]
		protected IntegerVariable TaskIndex;
	}
}
