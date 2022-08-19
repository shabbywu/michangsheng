using System;
using System.Collections.Generic;
using UnityEngine;

namespace Fungus
{
	// Token: 0x02000F7F RID: 3967
	[CommandInfo("YSTools", "CheckXunLuo", "检查是否巡逻到玩家", 0)]
	[AddComponentMenu("")]
	public class CheckXunLuo : Command
	{
		// Token: 0x06006F32 RID: 28466 RVA: 0x002A68E4 File Offset: 0x002A4AE4
		public override void OnEnter()
		{
			if (NpcJieSuanManager.inst.isCanJieSuan)
			{
				List<int> xunLuoNpcList = NpcJieSuanManager.inst.GetXunLuoNpcList(Tools.getScreenName(), Tools.instance.getPlayer().fubenContorl[Tools.getScreenName()].NowIndex);
				if (xunLuoNpcList.Count > 0)
				{
					this.npcId.Value = xunLuoNpcList[NpcJieSuanManager.inst.getRandomInt(0, xunLuoNpcList.Count - 1)];
				}
				else
				{
					this.npcId.Value = 0;
				}
			}
			else
			{
				this.npcId.Value = 0;
			}
			this.Continue();
		}

		// Token: 0x06006F33 RID: 28467 RVA: 0x0005E228 File Offset: 0x0005C428
		public override Color GetButtonColor()
		{
			return new Color32(184, 210, 235, byte.MaxValue);
		}

		// Token: 0x06006F34 RID: 28468 RVA: 0x00004095 File Offset: 0x00002295
		public override void OnReset()
		{
		}

		// Token: 0x04005BF6 RID: 23542
		[Tooltip("npcId")]
		[VariableProperty(new Type[]
		{
			typeof(IntegerVariable)
		})]
		[SerializeField]
		protected IntegerVariable npcId;
	}
}
