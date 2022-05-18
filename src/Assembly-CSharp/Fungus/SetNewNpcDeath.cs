using System;
using UnityEngine;

namespace Fungus
{
	// Token: 0x02001416 RID: 5142
	[CommandInfo("YSNew/Set", "SetNewNpcDeath", "设置npc死亡", 0)]
	[AddComponentMenu("")]
	public class SetNewNpcDeath : Command
	{
		// Token: 0x06007CAC RID: 31916 RVA: 0x002C550C File Offset: 0x002C370C
		public override void OnEnter()
		{
			int key = this.npcId.Value;
			if (NpcJieSuanManager.inst.ImportantNpcBangDingDictionary.ContainsKey(key))
			{
				key = NpcJieSuanManager.inst.ImportantNpcBangDingDictionary[key];
			}
			if (NpcJieSuanManager.inst.isCanJieSuan)
			{
				NpcJieSuanManager.inst.npcDeath.SetNpcDeath(10, key, 0, false);
			}
			else
			{
				NpcJieSuanManager.inst.npcDeath.SetNpcDeath(10, key, 0, true);
			}
			this.Continue();
		}

		// Token: 0x06007CAD RID: 31917 RVA: 0x000113CF File Offset: 0x0000F5CF
		public override Color GetButtonColor()
		{
			return new Color32(184, 210, 235, byte.MaxValue);
		}

		// Token: 0x06007CAE RID: 31918 RVA: 0x000042DD File Offset: 0x000024DD
		public override void OnReset()
		{
		}

		// Token: 0x04006A9A RID: 27290
		[Tooltip("指定npcId")]
		[VariableProperty(new Type[]
		{
			typeof(IntegerVariable)
		})]
		[SerializeField]
		protected IntegerVariable npcId;
	}
}
