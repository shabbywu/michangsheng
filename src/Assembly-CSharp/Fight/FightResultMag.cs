using System;
using UnityEngine;

namespace Fight
{
	// Token: 0x02000A7B RID: 2683
	public class FightResultMag : MonoBehaviour
	{
		// Token: 0x060044F6 RID: 17654 RVA: 0x00031575 File Offset: 0x0002F775
		private void Awake()
		{
			FightResultMag.inst = this;
		}

		// Token: 0x060044F7 RID: 17655 RVA: 0x0003157D File Offset: 0x0002F77D
		public void ShowVictory()
		{
			if (Tools.instance.monstarMag.CanDrowpItem())
			{
				ResManager.inst.LoadPrefab("VictoryPanel").Inst(null);
				return;
			}
			this.VictoryClick();
		}

		// Token: 0x060044F8 RID: 17656 RVA: 0x000315AD File Offset: 0x0002F7AD
		public void VictoryClick()
		{
			PanelMamager.CanOpenOrClose = true;
			Tools.canClickFlag = true;
			Tools.instance.loadMapScenes(Tools.instance.FinalScene, true);
		}

		// Token: 0x04003D1A RID: 15642
		public static FightResultMag inst;
	}
}
