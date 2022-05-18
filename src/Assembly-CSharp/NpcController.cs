using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

// Token: 0x02000479 RID: 1145
public class NpcController : MonoBehaviour
{
	// Token: 0x06001EA3 RID: 7843 RVA: 0x00108DEC File Offset: 0x00106FEC
	public void Init()
	{
		this.npcSetRandomFace.SetNPCFace(LunDaoManager.inst.npcId);
		this.npcName.text = jsonData.instance.AvatarRandomJsonData[LunDaoManager.inst.npcId.ToString()]["Name"].Str;
		int i = jsonData.instance.AvatarJsonData[LunDaoManager.inst.npcId.ToString()]["Status"]["StatusId"].I;
		i = jsonData.instance.NpcStatusDate[i.ToString()]["LunDao"].I;
		if (LunDaoManager.inst.lunDaoStateNameDictionary.ContainsKey(i))
		{
			this.npcStateId = i;
		}
		else
		{
			this.npcStateId = 3;
		}
		this.npcStateName.text = LunDaoManager.inst.lunDaoStateNameDictionary[this.npcStateId];
		this.npcStateTips.GetComponentInChildren<Text>().text = jsonData.instance.LunDaoStateData[this.npcStateId.ToString()]["MiaoShu"].Str;
	}

	// Token: 0x06001EA4 RID: 7844 RVA: 0x0001963B File Offset: 0x0001783B
	public void NpcSayWord(string content)
	{
		this.sayWord.Say(content);
	}

	// Token: 0x06001EA5 RID: 7845 RVA: 0x00108F20 File Offset: 0x00107120
	public void NpcStartRound()
	{
		this.isSayWord = true;
		LunDaoManager.inst.lunDaoPanel.AddNullSlot();
		this.cards = new List<LunDaoCard>();
		LunDaoManager.inst.lunDaoCardMag.NpcDrawCard(this.cards);
		if (this.cards.Count == 0)
		{
			this.NpcEndRound();
			return;
		}
		base.Invoke("NpcAction", 0.5f);
	}

	// Token: 0x06001EA6 RID: 7846 RVA: 0x00108F88 File Offset: 0x00107188
	private void NpcAction()
	{
		LunDaoCard lunDaoCard = this.GetCanCompleteCard();
		if (lunDaoCard != null)
		{
			this.NpcUseCard(lunDaoCard);
		}
		else
		{
			lunDaoCard = this.GetNpcCanUseCard();
			if (lunDaoCard == null)
			{
				this.NpcEndRound();
				return;
			}
			this.NpcUseCard(lunDaoCard);
		}
		if (LunDaoManager.inst.gameState == LunDaoManager.GameState.Npc回合 && LunDaoManager.inst.lunTiMag.GetNullSlot() != -1)
		{
			base.Invoke("NpcAction", 1f);
			return;
		}
		this.NpcEndRound();
	}

	// Token: 0x06001EA7 RID: 7847 RVA: 0x00108FF8 File Offset: 0x001071F8
	public void NpcUseCard(LunDaoCard card)
	{
		if (this.isSayWord)
		{
			this.isSayWord = false;
			int random = LunDaoManager.inst.lunDaoCardMag.getRandom(1, 5);
			string str = jsonData.instance.LunDaoSayData[card.wudaoId.ToString()]["Desc" + random].Str;
			this.NpcSayWord(str);
		}
		LunDaoManager.inst.lunDaoAmrMag.AddChuPai(card.wudaoId);
		int nullSlot = LunDaoManager.inst.lunTiMag.GetNullSlot();
		LunDaoManager.inst.lunTiMag.curLunDianList[nullSlot].SetData(card.wudaoId, card.level);
		this.cards.Remove(card);
		LunDaoManager.inst.ChuPaiCallBack();
	}

	// Token: 0x06001EA8 RID: 7848 RVA: 0x001090C4 File Offset: 0x001072C4
	public LunDaoCard GetNpcCanUseCard()
	{
		List<LunDaoCard> shengYuLunDian = LunDaoManager.inst.lunTiMag.GetShengYuLunDian();
		int num = -1;
		LunDaoCard result = null;
		foreach (LunDaoCard lunDaoCard in this.cards)
		{
			foreach (LunDaoCard lunDaoCard2 in shengYuLunDian)
			{
				if (lunDaoCard.wudaoId == lunDaoCard2.wudaoId && lunDaoCard.level <= lunDaoCard2.level && num < lunDaoCard.level)
				{
					num = lunDaoCard.level;
					result = lunDaoCard;
				}
			}
		}
		return result;
	}

	// Token: 0x06001EA9 RID: 7849 RVA: 0x00109194 File Offset: 0x00107394
	public LunDaoCard GetCanCompleteCard()
	{
		foreach (LunDaoCard lunDaoCard in this.cards)
		{
			bool flag = false;
			this.VirtualUseCard(lunDaoCard, delegate(int index)
			{
				if (LunDaoManager.inst.lunTiMag.CheckIsTargetLunTi())
				{
					flag = true;
				}
				LunDaoManager.inst.lunTiMag.curLunDianList[index].SetNull();
			});
			if (flag)
			{
				return lunDaoCard;
			}
		}
		return null;
	}

	// Token: 0x06001EAA RID: 7850 RVA: 0x00109210 File Offset: 0x00107410
	public void VirtualUseCard(LunDaoCard card, UnityAction<int> action)
	{
		int nullSlot = LunDaoManager.inst.lunTiMag.GetNullSlot();
		LunDaoManager.inst.lunTiMag.curLunDianList[nullSlot].SetData(card.wudaoId, card.level);
		action.Invoke(nullSlot);
	}

	// Token: 0x06001EAB RID: 7851 RVA: 0x0010925C File Offset: 0x0010745C
	public void NpcEndRound()
	{
		foreach (LunDaoCard item in this.cards)
		{
			LunDaoManager.inst.lunDaoCardMag.npcCards.Add(item);
		}
		this.cards = new List<LunDaoCard>();
		if (LunDaoManager.inst.gameState != LunDaoManager.GameState.论道结束)
		{
			LunDaoManager.inst.gameState = LunDaoManager.GameState.玩家回合;
		}
		LunDaoManager.inst.EndRoundCallBack();
		Debug.Log("Npc回合结束");
	}

	// Token: 0x06001EAC RID: 7852 RVA: 0x00019649 File Offset: 0x00017849
	public string GetNpcName()
	{
		return this.npcName.text;
	}

	// Token: 0x06001EAD RID: 7853 RVA: 0x00019656 File Offset: 0x00017856
	public void ShowStateTips()
	{
		this.npcStateTips.gameObject.SetActive(true);
	}

	// Token: 0x06001EAE RID: 7854 RVA: 0x00019669 File Offset: 0x00017869
	public void HideStateTips()
	{
		this.npcStateTips.gameObject.SetActive(false);
	}

	// Token: 0x04001A0E RID: 6670
	[SerializeField]
	private PlayerSetRandomFace npcSetRandomFace;

	// Token: 0x04001A0F RID: 6671
	public LunDaoSayWord sayWord;

	// Token: 0x04001A10 RID: 6672
	[SerializeField]
	private Text npcName;

	// Token: 0x04001A11 RID: 6673
	[SerializeField]
	private Text npcStateName;

	// Token: 0x04001A12 RID: 6674
	[SerializeField]
	private GameObject npcStateTips;

	// Token: 0x04001A13 RID: 6675
	public int npcStateId;

	// Token: 0x04001A14 RID: 6676
	public bool isSayWord = true;

	// Token: 0x04001A15 RID: 6677
	public List<LunDaoCard> cards;
}
