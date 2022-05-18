using System;
using UnityEngine;

namespace Fungus
{
	// Token: 0x02001417 RID: 5143
	[CommandInfo("YSNew/Set", "设置NPC境界并更新属性", "设置NPC境界并更新属性", 0)]
	[AddComponentMenu("")]
	public class SetNpcLevel : Command
	{
		// Token: 0x06007CB0 RID: 31920 RVA: 0x000547C8 File Offset: 0x000529C8
		public override void OnEnter()
		{
			FactoryManager.inst.npcFactory.SetNpcLevel(this.NpcId.Value, this.Level.Value);
			this.Continue();
		}

		// Token: 0x04006A9B RID: 27291
		[Tooltip("NpcId")]
		[VariableProperty(new Type[]
		{
			typeof(IntegerVariable)
		})]
		[SerializeField]
		protected IntegerVariable NpcId;

		// Token: 0x04006A9C RID: 27292
		[Tooltip("NpcId")]
		[VariableProperty(new Type[]
		{
			typeof(IntegerVariable)
		})]
		[SerializeField]
		protected IntegerVariable Level;
	}
}
