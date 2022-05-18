using System;
using UnityEngine;

namespace Fungus
{
	// Token: 0x020013DB RID: 5083
	[CommandInfo("YSNew/Add", "AddCyFriend", "增加传音符好友", 0)]
	[AddComponentMenu("")]
	public class AddCyFriend : Command
	{
		// Token: 0x06007BC8 RID: 31688 RVA: 0x000543EC File Offset: 0x000525EC
		public override void OnEnter()
		{
			Tools.instance.getPlayer().AddFriend(this.npcId.Value);
			this.Continue();
		}

		// Token: 0x06007BC9 RID: 31689 RVA: 0x000113CF File Offset: 0x0000F5CF
		public override Color GetButtonColor()
		{
			return new Color32(184, 210, 235, byte.MaxValue);
		}

		// Token: 0x06007BCA RID: 31690 RVA: 0x000042DD File Offset: 0x000024DD
		public override void OnReset()
		{
		}

		// Token: 0x04006A32 RID: 27186
		[Tooltip("增加npcId")]
		[VariableProperty(new Type[]
		{
			typeof(IntegerVariable)
		})]
		[SerializeField]
		protected IntegerVariable npcId;
	}
}
