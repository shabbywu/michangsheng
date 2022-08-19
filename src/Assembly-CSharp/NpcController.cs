using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

// Token: 0x02000317 RID: 791
public class NpcController : MonoBehaviour
{
	// Token: 0x06001B72 RID: 7026 RVA: 0x000C3970 File Offset: 0x000C1B70
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

	// Token: 0x06001B73 RID: 7027 RVA: 0x000C3AA3 File Offset: 0x000C1CA3
	public void NpcSayWord(string content)
	{
		this.sayWord.Say(content);
	}

	// Token: 0x06001B74 RID: 7028 RVA: 0x000C3AB4 File Offset: 0x000C1CB4
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

	// Token: 0x06001B75 RID: 7029 RVA: 0x000C3B1C File Offset: 0x000C1D1C
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

	// Token: 0x06001B76 RID: 7030 RVA: 0x000C3B8C File Offset: 0x000C1D8C
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

	// Token: 0x06001B77 RID: 7031 RVA: 0x000C3C58 File Offset: 0x000C1E58
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

	// Token: 0x06001B78 RID: 7032 RVA: 0x000C3D28 File Offset: 0x000C1F28
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

	// Token: 0x06001B79 RID: 7033 RVA: 0x000C3DA4 File Offset: 0x000C1FA4
	public void VirtualUseCard(LunDaoCard card, UnityAction<int> action)
	{
		int nullSlot = LunDaoManager.inst.lunTiMag.GetNullSlot();
		LunDaoManager.inst.lunTiMag.curLunDianList[nullSlot].SetData(card.wudaoId, card.level);
		action.Invoke(nullSlot);
	}

	// Token: 0x06001B7A RID: 7034 RVA: 0x000C3DF0 File Offset: 0x000C1FF0
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

	// Token: 0x06001B7B RID: 7035 RVA: 0x000C3E88 File Offset: 0x000C2088
	public string GetNpcName()
	{
		return this.npcName.text;
	}

	// Token: 0x06001B7C RID: 7036 RVA: 0x000C3E95 File Offset: 0x000C2095
	public void ShowStateTips()
	{
		this.npcStateTips.gameObject.SetActive(true);
	}

	// Token: 0x06001B7D RID: 7037 RVA: 0x000C3EA8 File Offset: 0x000C20A8
	public void HideStateTips()
	{
		this.npcStateTips.gameObject.SetActive(false);
	}

	// Token: 0x040015F4 RID: 5620
	[SerializeField]
	private PlayerSetRandomFace npcSetRandomFace;

	// Token: 0x040015F5 RID: 5621
	public LunDaoSayWord sayWord;

	// Token: 0x040015F6 RID: 5622
	[SerializeField]
	private Text npcName;

	// Token: 0x040015F7 RID: 5623
	[SerializeField]
	private Text npcStateName;

	// Token: 0x040015F8 RID: 5624
	[SerializeField]
	private GameObject npcStateTips;

	// Token: 0x040015F9 RID: 5625
	public int npcStateId;

	// Token: 0x040015FA RID: 5626
	public bool isSayWord = true;

	// Token: 0x040015FB RID: 5627
	public List<LunDaoCard> cards;
}
