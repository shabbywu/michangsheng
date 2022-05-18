using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

// Token: 0x0200047C RID: 1148
public class PlayerController : MonoBehaviour
{
	// Token: 0x06001EB6 RID: 7862 RVA: 0x001092F4 File Offset: 0x001074F4
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

	// Token: 0x06001EB7 RID: 7863 RVA: 0x00019763 File Offset: 0x00017963
	public void ShowChuPaiBtn()
	{
		this.BtnChuPai.gameObject.SetActive(true);
	}

	// Token: 0x06001EB8 RID: 7864 RVA: 0x00019776 File Offset: 0x00017976
	public void HideChuPaiBtn()
	{
		this.BtnChuPai.gameObject.SetActive(false);
	}

	// Token: 0x06001EB9 RID: 7865 RVA: 0x001093D8 File Offset: 0x001075D8
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

	// Token: 0x06001EBA RID: 7866 RVA: 0x00019789 File Offset: 0x00017989
	public void PlayerSayWord(string content)
	{
		this.sayWord.Say(content);
	}

	// Token: 0x06001EBB RID: 7867 RVA: 0x00019797 File Offset: 0x00017997
	public void PlayerStartRound()
	{
		this.isSayWord = true;
		LunDaoManager.inst.lunDaoPanel.AddNullSlot();
		this.BtnEnd.gameObject.SetActive(true);
		LunDaoManager.inst.lunDaoCardMag.PlayerDrawCard(this.cards);
	}

	// Token: 0x06001EBC RID: 7868 RVA: 0x001094F0 File Offset: 0x001076F0
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

	// Token: 0x06001EBD RID: 7869 RVA: 0x000197D5 File Offset: 0x000179D5
	public void ShowStateTips()
	{
		this.playerStateTips.gameObject.SetActive(true);
	}

	// Token: 0x06001EBE RID: 7870 RVA: 0x000197E8 File Offset: 0x000179E8
	public void HideStateTips()
	{
		this.playerStateTips.gameObject.SetActive(false);
	}

	// Token: 0x04001A1C RID: 6684
	[SerializeField]
	private PlayerSetRandomFace playerSetRandomFace;

	// Token: 0x04001A1D RID: 6685
	[SerializeField]
	private BtnCell BtnChuPai;

	// Token: 0x04001A1E RID: 6686
	[SerializeField]
	private BtnCell BtnEnd;

	// Token: 0x04001A1F RID: 6687
	[SerializeField]
	private Text playerName;

	// Token: 0x04001A20 RID: 6688
	public LunDaoSayWord sayWord;

	// Token: 0x04001A21 RID: 6689
	[SerializeField]
	private Text playerStateName;

	// Token: 0x04001A22 RID: 6690
	public int playerStateId;

	// Token: 0x04001A23 RID: 6691
	public List<LunDaoPlayerCard> cards;

	// Token: 0x04001A24 RID: 6692
	public LunDaoHuiHe lunDaoHuiHe;

	// Token: 0x04001A25 RID: 6693
	public LunDaoPlayerCard selectCard;

	// Token: 0x04001A26 RID: 6694
	public GameObject tips;

	// Token: 0x04001A27 RID: 6695
	[SerializeField]
	private GameObject playerStateTips;

	// Token: 0x04001A28 RID: 6696
	private bool isSayWord;
}
