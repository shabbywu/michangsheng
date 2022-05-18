using System;
using UnityEngine;

namespace Fungus
{
	// Token: 0x0200144D RID: 5197
	[CommandInfo("YSTools", "OpenZuLi", "打开租赁界面", 0)]
	[AddComponentMenu("")]
	public class OpenZuLi : Command
	{
		// Token: 0x06007D7F RID: 32127 RVA: 0x002C6850 File Offset: 0x002C4A50
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

		// Token: 0x06007D80 RID: 32128 RVA: 0x000113CF File Offset: 0x0000F5CF
		public override Color GetButtonColor()
		{
			return new Color32(184, 210, 235, byte.MaxValue);
		}

		// Token: 0x04006AEB RID: 27371
		[Tooltip("租赁每个月的时间所消耗的灵石")]
		[SerializeField]
		protected int Price = 1;

		// Token: 0x04006AEC RID: 27372
		[Tooltip("关联的房间名称")]
		[SerializeField]
		protected string ScreenName = "";
	}
}
