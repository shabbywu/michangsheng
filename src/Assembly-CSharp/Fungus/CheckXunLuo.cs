using System;
using System.Collections.Generic;
using UnityEngine;

namespace Fungus
{
	// Token: 0x02001436 RID: 5174
	[CommandInfo("YSTools", "CheckXunLuo", "检查是否巡逻到玩家", 0)]
	[AddComponentMenu("")]
	public class CheckXunLuo : Command
	{
		// Token: 0x06007D22 RID: 32034 RVA: 0x002C6400 File Offset: 0x002C4600
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

		// Token: 0x06007D23 RID: 32035 RVA: 0x000113CF File Offset: 0x0000F5CF
		public override Color GetButtonColor()
		{
			return new Color32(184, 210, 235, byte.MaxValue);
		}

		// Token: 0x06007D24 RID: 32036 RVA: 0x000042DD File Offset: 0x000024DD
		public override void OnReset()
		{
		}

		// Token: 0x04006ACA RID: 27338
		[Tooltip("npcId")]
		[VariableProperty(new Type[]
		{
			typeof(IntegerVariable)
		})]
		[SerializeField]
		protected IntegerVariable npcId;
	}
}
