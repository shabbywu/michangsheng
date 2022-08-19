using System;
using UnityEngine;

namespace Fungus
{
	// Token: 0x02000F60 RID: 3936
	[CommandInfo("YSNew/Set", "设置NPC境界并更新属性", "设置NPC境界并更新属性", 0)]
	[AddComponentMenu("")]
	public class SetNpcLevel : Command
	{
		// Token: 0x06006EC0 RID: 28352 RVA: 0x002A56FC File Offset: 0x002A38FC
		public override void OnEnter()
		{
			FactoryManager.inst.npcFactory.SetNpcLevel(this.NpcId.Value, this.Level.Value);
			this.Continue();
		}

		// Token: 0x04005BC6 RID: 23494
		[Tooltip("NpcId")]
		[VariableProperty(new Type[]
		{
			typeof(IntegerVariable)
		})]
		[SerializeField]
		protected IntegerVariable NpcId;

		// Token: 0x04005BC7 RID: 23495
		[Tooltip("NpcId")]
		[VariableProperty(new Type[]
		{
			typeof(IntegerVariable)
		})]
		[SerializeField]
		protected IntegerVariable Level;
	}
}
