using System;
using UnityEngine;

namespace Fungus
{
	// Token: 0x02001419 RID: 5145
	[CommandInfo("YSNew/Set", "SetRandomTalkNotClick", "设置指定随机事件不再触发", 0)]
	[AddComponentMenu("")]
	public class SetRandomTalkClick : Command
	{
		// Token: 0x06007CB5 RID: 31925 RVA: 0x0005481B File Offset: 0x00052A1B
		public override void OnEnter()
		{
			AllMapManage.instance.RandomFlag[this.TaskID.Value] = 1;
			this.Continue();
		}

		// Token: 0x06007CB6 RID: 31926 RVA: 0x000113CF File Offset: 0x0000F5CF
		public override Color GetButtonColor()
		{
			return new Color32(184, 210, 235, byte.MaxValue);
		}

		// Token: 0x06007CB7 RID: 31927 RVA: 0x000042DD File Offset: 0x000024DD
		public override void OnReset()
		{
		}

		// Token: 0x04006A9E RID: 27294
		[Tooltip("事件ID")]
		[VariableProperty(new Type[]
		{
			typeof(IntegerVariable)
		})]
		[SerializeField]
		protected IntegerVariable TaskID;
	}
}
