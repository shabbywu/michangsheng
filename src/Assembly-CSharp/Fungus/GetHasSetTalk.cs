using System;
using UnityEngine;

namespace Fungus
{
	// Token: 0x02000F42 RID: 3906
	[CommandInfo("YSNew/Get", "GetHasSetTalk", "根据流派和境界获取NpcId", 0)]
	[AddComponentMenu("")]
	public class GetHasSetTalk : Command
	{
		// Token: 0x06006E4A RID: 28234 RVA: 0x002A49AB File Offset: 0x002A2BAB
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

		// Token: 0x06006E4B RID: 28235 RVA: 0x0005E228 File Offset: 0x0005C428
		public override Color GetButtonColor()
		{
			return new Color32(184, 210, 235, byte.MaxValue);
		}

		// Token: 0x04005B93 RID: 23443
		[Tooltip("是否有setTalk")]
		[VariableProperty(new Type[]
		{
			typeof(BooleanVariable)
		})]
		[SerializeField]
		protected BooleanVariable flag;
	}
}
