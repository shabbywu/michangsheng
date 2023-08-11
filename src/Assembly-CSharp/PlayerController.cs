using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
	[SerializeField]
	private PlayerSetRandomFace playerSetRandomFace;

	[SerializeField]
	private BtnCell BtnChuPai;

	[SerializeField]
	private BtnCell BtnEnd;

	[SerializeField]
	private Text playerName;

	public LunDaoSayWord sayWord;

	[SerializeField]
	private Text playerStateName;

	public int playerStateId;

	public List<LunDaoPlayerCard> cards;

	public LunDaoHuiHe lunDaoHuiHe;

	public LunDaoPlayerCard selectCard;

	public GameObject tips;

	[SerializeField]
	private GameObject playerStateTips;

	private bool isSayWord;

	public void Init()
	{
		//IL_00b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bb: Expected O, but got Unknown
		//IL_00cd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d7: Expected O, but got Unknown
		playerSetRandomFace.SetNPCFace(1);
		playerName.text = LunDaoManager.inst.player.name;
		playerStateId = LunDaoManager.inst.player.LunDaoState;
		playerStateName.text = LunDaoManager.inst.lunDaoStateNameDictionary[playerStateId];
		playerStateTips.GetComponentInChildren<Text>().text = jsonData.instance.LunDaoStateData[playerStateId.ToString()]["MiaoShu"].Str;
		lunDaoHuiHe.Init();
		BtnEnd.mouseUp.AddListener(new UnityAction(PlayerEndRound));
		BtnChuPai.mouseUp.AddListener(new UnityAction(PlayerUseCard));
	}

	public void ShowChuPaiBtn()
	{
		((Component)BtnChuPai).gameObject.SetActive(true);
	}

	public void HideChuPaiBtn()
	{
		((Component)BtnChuPai).gameObject.SetActive(false);
	}

	public void PlayerUseCard()
	{
		int nullSlot = LunDaoManager.inst.lunTiMag.GetNullSlot();
		if (nullSlot == -1)
		{
			UIPopTip.Inst.Pop("没有空的");
		}
		if (isSayWord)
		{
			isSayWord = false;
			int random = LunDaoManager.inst.lunDaoCardMag.getRandom(1, 5);
			string str = jsonData.instance.LunDaoSayData[selectCard.lunDaoCard.wudaoId.ToString()]["Desc" + random].Str;
			PlayerSayWord(str);
		}
		((Component)BtnChuPai).gameObject.SetActive(false);
		LunDaoManager.inst.lunTiMag.curLunDianList[nullSlot].SetData(selectCard.lunDaoCard.wudaoId, selectCard.lunDaoCard.level);
		cards.Remove(selectCard);
		Object.Destroy((Object)(object)((Component)selectCard).gameObject);
		selectCard = null;
		LunDaoManager.inst.ChuPaiCallBack();
	}

	public void PlayerSayWord(string content)
	{
		sayWord.Say(content);
	}

	public void PlayerStartRound()
	{
		isSayWord = true;
		LunDaoManager.inst.lunDaoPanel.AddNullSlot();
		((Component)BtnEnd).gameObject.SetActive(true);
		LunDaoManager.inst.lunDaoCardMag.PlayerDrawCard(cards);
	}

	public void PlayerEndRound()
	{
		((Component)BtnEnd).gameObject.SetActive(false);
		((Component)BtnChuPai).gameObject.SetActive(false);
		tips.SetActive(false);
		foreach (LunDaoPlayerCard card in cards)
		{
			LunDaoManager.inst.lunDaoCardMag.playerCards.Add(new LunDaoCard(card.lunDaoCard.wudaoId, card.lunDaoCard.level));
		}
		for (int num = cards.Count - 1; num >= 0; num--)
		{
			Object.Destroy((Object)(object)((Component)cards[num]).gameObject);
		}
		cards = new List<LunDaoPlayerCard>();
		if (LunDaoManager.inst.gameState != LunDaoManager.GameState.论道结束)
		{
			LunDaoManager.inst.gameState = LunDaoManager.GameState.Npc回合;
			lunDaoHuiHe.ReduceHuiHe();
		}
		LunDaoManager.inst.EndRoundCallBack();
	}

	public void ShowStateTips()
	{
		playerStateTips.gameObject.SetActive(true);
	}

	public void HideStateTips()
	{
		playerStateTips.gameObject.SetActive(false);
	}
}
