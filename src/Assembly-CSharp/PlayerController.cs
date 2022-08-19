using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

// Token: 0x02000319 RID: 793
public class PlayerController : MonoBehaviour
{
	// Token: 0x06001B83 RID: 7043 RVA: 0x000C3F70 File Offset: 0x000C2170
	public void Init()
	{
		this.playerSetRandomFace.SetNPCFace(1);
		this.playerName.text = LunDaoManager.inst.player.name;
		this.playerStateId = LunDaoManager.inst.player.LunDaoState;
		this.playerStateName.text = LunDaoManager.inst.lunDaoStateNameDictionary[this.playerStateId];
		this.playerStateTips.GetComponentInChildren<Text>().text = jsonData.instance.LunDaoStateData[this.playerStateId.ToString()]["MiaoShu"].Str;
		this.lunDaoHuiHe.Init();
		this.BtnEnd.mouseUp.AddListener(new UnityAction(this.PlayerEndRound));
		this.BtnChuPai.mouseUp.AddListener(new UnityAction(this.PlayerUseCard));
	}

	// Token: 0x06001B84 RID: 7044 RVA: 0x000C4054 File Offset: 0x000C2254
	public void ShowChuPaiBtn()
	{
		this.BtnChuPai.gameObject.SetActive(true);
	}

	// Token: 0x06001B85 RID: 7045 RVA: 0x000C4067 File Offset: 0x000C2267
	public void HideChuPaiBtn()
	{
		this.BtnChuPai.gameObject.SetActive(false);
	}

	// Token: 0x06001B86 RID: 7046 RVA: 0x000C407C File Offset: 0x000C227C
	public void PlayerUseCard()
	{
		int nullSlot = LunDaoManager.inst.lunTiMag.GetNullSlot();
		if (nullSlot == -1)
		{
			UIPopTip.Inst.Pop("没有空的", PopTipIconType.叹号);
		}
		if (this.isSayWord)
		{
			this.isSayWord = false;
			int random = LunDaoManager.inst.lunDaoCardMag.getRandom(1, 5);
			string str = jsonData.instance.LunDaoSayData[this.selectCard.lunDaoCard.wudaoId.ToString()]["Desc" + random].Str;
			this.PlayerSayWord(str);
		}
		this.BtnChuPai.gameObject.SetActive(false);
		LunDaoManager.inst.lunTiMag.curLunDianList[nullSlot].SetData(this.selectCard.lunDaoCard.wudaoId, this.selectCard.lunDaoCard.level);
		this.cards.Remove(this.selectCard);
		Object.Destroy(this.selectCard.gameObject);
		this.selectCard = null;
		LunDaoManager.inst.ChuPaiCallBack();
	}

	// Token: 0x06001B87 RID: 7047 RVA: 0x000C4192 File Offset: 0x000C2392
	public void PlayerSayWord(string content)
	{
		this.sayWord.Say(content);
	}

	// Token: 0x06001B88 RID: 7048 RVA: 0x000C41A0 File Offset: 0x000C23A0
	public void PlayerStartRound()
	{
		this.isSayWord = true;
		LunDaoManager.inst.lunDaoPanel.AddNullSlot();
		this.BtnEnd.gameObject.SetActive(true);
		LunDaoManager.inst.lunDaoCardMag.PlayerDrawCard(this.cards);
	}

	// Token: 0x06001B89 RID: 7049 RVA: 0x000C41E0 File Offset: 0x000C23E0
	public void PlayerEndRound()
	{
		this.BtnEnd.gameObject.SetActive(false);
		this.BtnChuPai.gameObject.SetActive(false);
		this.tips.SetActive(false);
		foreach (LunDaoPlayerCard lunDaoPlayerCard in this.cards)
		{
			LunDaoManager.inst.lunDaoCardMag.playerCards.Add(new LunDaoCard(lunDaoPlayerCard.lunDaoCard.wudaoId, lunDaoPlayerCard.lunDaoCard.level));
		}
		for (int i = this.cards.Count - 1; i >= 0; i--)
		{
			Object.Destroy(this.cards[i].gameObject);
		}
		this.cards = new List<LunDaoPlayerCard>();
		if (LunDaoManager.inst.gameState != LunDaoManager.GameState.论道结束)
		{
			LunDaoManager.inst.gameState = LunDaoManager.GameState.Npc回合;
			this.lunDaoHuiHe.ReduceHuiHe();
		}
		LunDaoManager.inst.EndRoundCallBack();
	}

	// Token: 0x06001B8A RID: 7050 RVA: 0x000C42F0 File Offset: 0x000C24F0
	public void ShowStateTips()
	{
		this.playerStateTips.gameObject.SetActive(true);
	}

	// Token: 0x06001B8B RID: 7051 RVA: 0x000C4303 File Offset: 0x000C2503
	public void HideStateTips()
	{
		this.playerStateTips.gameObject.SetActive(false);
	}

	// Token: 0x04001601 RID: 5633
	[SerializeField]
	private PlayerSetRandomFace playerSetRandomFace;

	// Token: 0x04001602 RID: 5634
	[SerializeField]
	private BtnCell BtnChuPai;

	// Token: 0x04001603 RID: 5635
	[SerializeField]
	private BtnCell BtnEnd;

	// Token: 0x04001604 RID: 5636
	[SerializeField]
	private Text playerName;

	// Token: 0x04001605 RID: 5637
	public LunDaoSayWord sayWord;

	// Token: 0x04001606 RID: 5638
	[SerializeField]
	private Text playerStateName;

	// Token: 0x04001607 RID: 5639
	public int playerStateId;

	// Token: 0x04001608 RID: 5640
	public List<LunDaoPlayerCard> cards;

	// Token: 0x04001609 RID: 5641
	public LunDaoHuiHe lunDaoHuiHe;

	// Token: 0x0400160A RID: 5642
	public LunDaoPlayerCard selectCard;

	// Token: 0x0400160B RID: 5643
	public GameObject tips;

	// Token: 0x0400160C RID: 5644
	[SerializeField]
	private GameObject playerStateTips;

	// Token: 0x0400160D RID: 5645
	private bool isSayWord;
}
