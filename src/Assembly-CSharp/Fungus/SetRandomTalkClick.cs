using System;
using UnityEngine;

namespace Fungus
{
	// Token: 0x02000F62 RID: 3938
	[CommandInfo("YSNew/Set", "SetRandomTalkNotClick", "设置指定随机事件不再触发", 0)]
	[AddComponentMenu("")]
	public class SetRandomTalkClick : Command
	{
		// Token: 0x06006EC5 RID: 28357 RVA: 0x002A574F File Offset: 0x002A394F
		public override void OnEnter()
		{
			AllMapManage.instance.RandomFlag[this.TaskID.Value] = 1;
			this.Continue();
		}

		// Token: 0x06006EC6 RID: 28358 RVA: 0x0005E228 File Offset: 0x0005C428
		public override Color GetButtonColor()
		{
			return new Color32(184, 210, 235, byte.MaxValue);
		}

		// Token: 0x06006EC7 RID: 28359 RVA: 0x00004095 File Offset: 0x00002295
		public override void OnReset()
		{
		}

		// Token: 0x04005BC9 RID: 23497
		[Tooltip("事件ID")]
		[VariableProperty(new Type[]
		{
			typeof(IntegerVariable)
		})]
		[SerializeField]
		protected IntegerVariable TaskID;
	}
}
