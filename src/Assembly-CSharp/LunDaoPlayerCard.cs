using UnityEngine;
using UnityEngine.UI;

public class LunDaoPlayerCard : MonoBehaviour
{
	public LunDaoCard lunDaoCard;

	public Image cardImage;

	public Text cardLevel;

	public BtnCell btn;

	public bool isSelected;

	public void SelectCard()
	{
		//IL_00ec: Unknown result type (might be due to invalid IL or missing references)
		//IL_005e: Unknown result type (might be due to invalid IL or missing references)
		//IL_009d: Unknown result type (might be due to invalid IL or missing references)
		if (LunDaoManager.inst.gameState != LunDaoManager.GameState.玩家回合 || LunDaoManager.inst.playerController.tips.activeSelf)
		{
			return;
		}
		isSelected = !isSelected;
		if (isSelected)
		{
			((Component)cardImage).gameObject.transform.localPosition = new Vector3(0f, 30.79f, 0f);
			if ((Object)(object)LunDaoManager.inst.playerController.selectCard != (Object)null)
			{
				((Component)LunDaoManager.inst.playerController.selectCard.cardImage).gameObject.transform.localPosition = Vector3.zero;
				LunDaoManager.inst.playerController.selectCard.isSelected = false;
			}
			LunDaoManager.inst.playerController.selectCard = this;
			LunDaoManager.inst.playerController.ShowChuPaiBtn();
		}
		else
		{
			((Component)cardImage).gameObject.transform.localPosition = Vector3.zero;
			LunDaoManager.inst.playerController.selectCard = null;
			LunDaoManager.inst.playerController.HideChuPaiBtn();
		}
	}
}
