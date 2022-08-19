using System;
using KBEngine;
using UnityEngine;

namespace Fight
{
	// Token: 0x02000724 RID: 1828
	public class FightResultMag : MonoBehaviour
	{
		// Token: 0x06003A53 RID: 14931 RVA: 0x00190975 File Offset: 0x0018EB75
		private void Awake()
		{
			FightResultMag.inst = this;
		}

		// Token: 0x06003A54 RID: 14932 RVA: 0x0019097D File Offset: 0x0018EB7D
		public void ShowVictory()
		{
			base.Invoke("LaterShowVictory", 1f);
		}

		// Token: 0x06003A55 RID: 14933 RVA: 0x0019098F File Offset: 0x0018EB8F
		private void LaterShowVictory()
		{
			if (Tools.instance.monstarMag.CanDrowpItem())
			{
				ResManager.inst.LoadPrefab("VictoryPanel").Inst(null);
				return;
			}
			this.VictoryClick();
		}

		// Token: 0x06003A56 RID: 14934 RVA: 0x001909BF File Offset: 0x0018EBBF
		public void VictoryClick()
		{
			Avatar otherAvatar = Tools.instance.getPlayer().OtherAvatar;
			if (otherAvatar != null)
			{
				otherAvatar.die();
			}
			PanelMamager.CanOpenOrClose = true;
			Tools.canClickFlag = true;
			Tools.instance.loadMapScenes(Tools.instance.FinalScene, true);
		}

		// Token: 0x0400327F RID: 12927
		public static FightResultMag inst;
	}
}
