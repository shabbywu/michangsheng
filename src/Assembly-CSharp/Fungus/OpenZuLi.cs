using System;
using UnityEngine;

namespace Fungus
{
	// Token: 0x02000F9D RID: 3997
	[CommandInfo("YSTools", "OpenZuLi", "打开租赁界面", 0)]
	[AddComponentMenu("")]
	public class OpenZuLi : Command
	{
		// Token: 0x06006F9F RID: 28575 RVA: 0x002A724C File Offset: 0x002A544C
		public override void OnEnter()
		{
			if ((int)Tools.instance.getPlayer().money < this.Price)
			{
				UIPopTip.Inst.Pop("灵石不足", PopTipIconType.叹号);
			}
			else
			{
				Object.Instantiate<GameObject>(ResManager.inst.LoadPrefab("KFTimeSelect"));
				KeFangSelectTime.inst.price = this.Price;
				KeFangSelectTime.inst.screenName = this.ScreenName;
				KeFangSelectTime.inst.Init();
			}
			this.Continue();
		}

		// Token: 0x06006FA0 RID: 28576 RVA: 0x0005E228 File Offset: 0x0005C428
		public override Color GetButtonColor()
		{
			return new Color32(184, 210, 235, byte.MaxValue);
		}

		// Token: 0x04005C1C RID: 23580
		[Tooltip("租赁每个月的时间所消耗的灵石")]
		[SerializeField]
		protected int Price = 1;

		// Token: 0x04005C1D RID: 23581
		[Tooltip("关联的房间名称")]
		[SerializeField]
		protected string ScreenName = "";
	}
}
