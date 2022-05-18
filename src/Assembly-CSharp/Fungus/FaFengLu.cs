using System;
using KBEngine;
using UnityEngine;

namespace Fungus
{
	// Token: 0x02001438 RID: 5176
	[CommandInfo("YSTools", "FaFengLu", "发放俸禄", 0)]
	[AddComponentMenu("")]
	public class FaFengLu : Command
	{
		// Token: 0x06007D29 RID: 32041 RVA: 0x002C6498 File Offset: 0x002C4698
		public override void OnEnter()
		{
			Avatar player = Tools.instance.getPlayer();
			int allFengLuMoney = player.chenghaomag.GetAllFengLuMoney();
			if (allFengLuMoney > 0)
			{
				player.chenghaomag.GiveMoney();
			}
			this.Money.Value = allFengLuMoney;
			this.Continue();
		}

		// Token: 0x06007D2A RID: 32042 RVA: 0x000113CF File Offset: 0x0000F5CF
		public override Color GetButtonColor()
		{
			return new Color32(184, 210, 235, byte.MaxValue);
		}

		// Token: 0x06007D2B RID: 32043 RVA: 0x000042DD File Offset: 0x000024DD
		public override void OnReset()
		{
		}

		// Token: 0x04006ACC RID: 27340
		[Tooltip("说明")]
		[SerializeField]
		protected string init = "发放俸禄，发放后的总钱数保存到Money的值当中";

		// Token: 0x04006ACD RID: 27341
		[Tooltip("存放总俸禄钱数")]
		[VariableProperty(new Type[]
		{
			typeof(IntegerVariable)
		})]
		[SerializeField]
		protected IntegerVariable Money;
	}
}
