using System;
using UnityEngine;

namespace Fungus
{
	// Token: 0x02000F5F RID: 3935
	[CommandInfo("YSNew/Set", "SetNewNpcDeath", "设置npc死亡", 0)]
	[AddComponentMenu("")]
	public class SetNewNpcDeath : Command
	{
		// Token: 0x06006EBC RID: 28348 RVA: 0x002A5684 File Offset: 0x002A3884
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

		// Token: 0x06006EBD RID: 28349 RVA: 0x0005E228 File Offset: 0x0005C428
		public override Color GetButtonColor()
		{
			return new Color32(184, 210, 235, byte.MaxValue);
		}

		// Token: 0x06006EBE RID: 28350 RVA: 0x00004095 File Offset: 0x00002295
		public override void OnReset()
		{
		}

		// Token: 0x04005BC5 RID: 23493
		[Tooltip("指定npcId")]
		[VariableProperty(new Type[]
		{
			typeof(IntegerVariable)
		})]
		[SerializeField]
		protected IntegerVariable npcId;
	}
}
