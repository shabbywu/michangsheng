using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000471 RID: 1137
public class LunDaoPlayerCard : MonoBehaviour
{
	// Token: 0x06001E79 RID: 7801 RVA: 0x00107DE8 File Offset: 0x00105FE8
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

	// Token: 0x040019D3 RID: 6611
	public LunDaoCard lunDaoCard;

	// Token: 0x040019D4 RID: 6612
	public Image cardImage;

	// Token: 0x040019D5 RID: 6613
	public Text cardLevel;

	// Token: 0x040019D6 RID: 6614
	public BtnCell btn;

	// Token: 0x040019D7 RID: 6615
	public bool isSelected;
}
