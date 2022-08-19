using System;
using KBEngine;
using UnityEngine;

namespace Fungus
{
	// Token: 0x02000F81 RID: 3969
	[CommandInfo("YSTools", "FaFengLu", "发放俸禄", 0)]
	[AddComponentMenu("")]
	public class FaFengLu : Command
	{
		// Token: 0x06006F39 RID: 28473 RVA: 0x002A69A4 File Offset: 0x002A4BA4
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

		// Token: 0x06006F3A RID: 28474 RVA: 0x0005E228 File Offset: 0x0005C428
		public override Color GetButtonColor()
		{
			return new Color32(184, 210, 235, byte.MaxValue);
		}

		// Token: 0x06006F3B RID: 28475 RVA: 0x00004095 File Offset: 0x00002295
		public override void OnReset()
		{
		}

		// Token: 0x04005BF8 RID: 23544
		[Tooltip("说明")]
		[SerializeField]
		protected string init = "发放俸禄，发放后的总钱数保存到Money的值当中";

		// Token: 0x04005BF9 RID: 23545
		[Tooltip("存放总俸禄钱数")]
		[VariableProperty(new Type[]
		{
			typeof(IntegerVariable)
		})]
		[SerializeField]
		protected IntegerVariable Money;
	}
}
