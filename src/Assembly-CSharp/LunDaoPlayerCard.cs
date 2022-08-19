using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000310 RID: 784
public class LunDaoPlayerCard : MonoBehaviour
{
	// Token: 0x06001B48 RID: 6984 RVA: 0x000C27B8 File Offset: 0x000C09B8
	public void SelectCard()
	{
		if (LunDaoManager.inst.gameState != LunDaoManager.GameState.玩家回合)
		{
			return;
		}
		if (LunDaoManager.inst.playerController.tips.activeSelf)
		{
			return;
		}
		this.isSelected = !this.isSelected;
		if (this.isSelected)
		{
			this.cardImage.gameObject.transform.localPosition = new Vector3(0f, 30.79f, 0f);
			if (LunDaoManager.inst.playerController.selectCard != null)
			{
				LunDaoManager.inst.playerController.selectCard.cardImage.gameObject.transform.localPosition = Vector3.zero;
				LunDaoManager.inst.playerController.selectCard.isSelected = false;
			}
			LunDaoManager.inst.playerController.selectCard = this;
			LunDaoManager.inst.playerController.ShowChuPaiBtn();
			return;
		}
		this.cardImage.gameObject.transform.localPosition = Vector3.zero;
		LunDaoManager.inst.playerController.selectCard = null;
		LunDaoManager.inst.playerController.HideChuPaiBtn();
	}

	// Token: 0x040015BD RID: 5565
	public LunDaoCard lunDaoCard;

	// Token: 0x040015BE RID: 5566
	public Image cardImage;

	// Token: 0x040015BF RID: 5567
	public Text cardLevel;

	// Token: 0x040015C0 RID: 5568
	public BtnCell btn;

	// Token: 0x040015C1 RID: 5569
	public bool isSelected;
}
