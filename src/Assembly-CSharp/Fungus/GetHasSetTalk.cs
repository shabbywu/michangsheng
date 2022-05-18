using System;
using UnityEngine;

namespace Fungus
{
	// Token: 0x020013F8 RID: 5112
	[CommandInfo("YSNew/Get", "GetHasSetTalk", "根据流派和境界获取NpcId", 0)]
	[AddComponentMenu("")]
	public class GetHasSetTalk : Command
	{
		// Token: 0x06007C35 RID: 31797 RVA: 0x000546C3 File Offset: 0x000528C3
		public override void OnEnter()
		{
			if (GlobalValue.Get(0, base.GetCommandSourceDesc()) > 0)
			{
				this.flag.Value = true;
			}
			else
			{
				this.flag.Value = false;
			}
			this.Continue();
		}

		// Token: 0x06007C36 RID: 31798 RVA: 0x000113CF File Offset: 0x0000F5CF
		public override Color GetButtonColor()
		{
			return new Color32(184, 210, 235, byte.MaxValue);
		}

		// Token: 0x04006A65 RID: 27237
		[Tooltip("是否有setTalk")]
		[VariableProperty(new Type[]
		{
			typeof(BooleanVariable)
		})]
		[SerializeField]
		protected BooleanVariable flag;
	}
}
