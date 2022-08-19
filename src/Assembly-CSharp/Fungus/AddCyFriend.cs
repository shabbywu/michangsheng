using System;
using UnityEngine;

namespace Fungus
{
	// Token: 0x02000F25 RID: 3877
	[CommandInfo("YSNew/Add", "AddCyFriend", "增加传音符好友", 0)]
	[AddComponentMenu("")]
	public class AddCyFriend : Command
	{
		// Token: 0x06006DDD RID: 28125 RVA: 0x002A3FCB File Offset: 0x002A21CB
		public override void OnEnter()
		{
			Tools.instance.getPlayer().AddFriend(this.npcId.Value);
			this.Continue();
		}

		// Token: 0x06006DDE RID: 28126 RVA: 0x0005E228 File Offset: 0x0005C428
		public override Color GetButtonColor()
		{
			return new Color32(184, 210, 235, byte.MaxValue);
		}

		// Token: 0x06006DDF RID: 28127 RVA: 0x00004095 File Offset: 0x00002295
		public override void OnReset()
		{
		}

		// Token: 0x04005B60 RID: 23392
		[Tooltip("增加npcId")]
		[VariableProperty(new Type[]
		{
			typeof(IntegerVariable)
		})]
		[SerializeField]
		protected IntegerVariable npcId;
	}
}
