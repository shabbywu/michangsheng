using System;
using UnityEngine;

namespace Fungus
{
	// Token: 0x0200141B RID: 5147
	[CommandInfo("YSNew/Set", "SetStaticValue", "设置全局变量", 0)]
	[AddComponentMenu("")]
	public class SetStaticValue : Command
	{
		// Token: 0x06007CBC RID: 31932 RVA: 0x00054858 File Offset: 0x00052A58
		public override void OnEnter()
		{
			GlobalValue.Set(this.StaticValueID, this.value, base.GetCommandSourceDesc());
			this.Continue();
		}

		// Token: 0x06007CBD RID: 31933 RVA: 0x000113CF File Offset: 0x0000F5CF
		public override Color GetButtonColor()
		{
			return new Color32(184, 210, 235, byte.MaxValue);
		}

		// Token: 0x04006AA0 RID: 27296
		[Tooltip("全局变量的ID")]
		[SerializeField]
		public int StaticValueID;

		// Token: 0x04006AA1 RID: 27297
		[Tooltip("全局变量的值")]
		[SerializeField]
		public int value;
	}
}
