using System;
using UnityEngine;

namespace Fungus
{
	// Token: 0x02000F77 RID: 3959
	[CommandInfo("YSTools", "增加Npc下一次突破概率", "增加Npc下一次突破概率", 0)]
	[AddComponentMenu("")]
	public class AddNpcNextToPoLv : Command
	{
		// Token: 0x06006F13 RID: 28435 RVA: 0x002A6437 File Offset: 0x002A4637
		public override void OnEnter()
		{
			NPCEx.AddNpcNextToPoLv(this.NpcId.Value, this.AddLV.Value);
			this.Continue();
		}

		// Token: 0x04005BE8 RID: 23528
		[Tooltip("NpcId")]
		[VariableProperty(new Type[]
		{
			typeof(IntegerVariable)
		})]
		[SerializeField]
		protected IntegerVariable NpcId;

		// Token: 0x04005BE9 RID: 23529
		[Tooltip("增加概率")]
		[VariableProperty(new Type[]
		{
			typeof(IntegerVariable)
		})]
		[SerializeField]
		protected IntegerVariable AddLV;
	}
}
