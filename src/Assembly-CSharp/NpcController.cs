using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class NpcController : MonoBehaviour
{
	[SerializeField]
	private PlayerSetRandomFace npcSetRandomFace;

	public LunDaoSayWord sayWord;

	[SerializeField]
	private Text npcName;

	[SerializeField]
	private Text npcStateName;

	[SerializeField]
	private GameObject npcStateTips;

	public int npcStateId;

	public bool isSayWord = true;

	public List<LunDaoCard> cards;

	public void Init()
	{
		npcSetRandomFace.SetNPCFace(LunDaoManager.inst.npcId);
		npcName.text = jsonData.instance.AvatarRandomJsonData[LunDaoManager.inst.npcId.ToString()]["Name"].Str;
		int i = jsonData.instance.AvatarJsonData[LunDaoManager.inst.npcId.ToString()]["Status"]["StatusId"].I;
		i = jsonData.instance.NpcStatusDate[i.ToString()]["LunDao"].I;
		if (LunDaoManager.inst.lunDaoStateNameDictionary.ContainsKey(i))
		{
			npcStateId = i;
		}
		else
		{
			npcStateId = 3;
		}
		npcStateName.text = LunDaoManager.inst.lunDaoStateNameDictionary[npcStateId];
		npcStateTips.GetComponentInChildren<Text>().text = jsonData.instance.LunDaoStateData[npcStateId.ToString()]["MiaoShu"].Str;
	}

	public void NpcSayWord(string content)
	{
		sayWord.Say(content);
	}

	public void NpcStartRound()
	{
		isSayWord = true;
		LunDaoManager.inst.lunDaoPanel.AddNullSlot();
		cards = new List<LunDaoCard>();
		LunDaoManager.inst.lunDaoCardMag.NpcDrawCard(cards);
		if (cards.Count == 0)
		{
			NpcEndRound();
		}
		else
		{
			((MonoBehaviour)this).Invoke("NpcAction", 0.5f);
		}
	}

	private void NpcAction()
	{
		LunDaoCard canCompleteCard = GetCanCompleteCard();
		if (canCompleteCard != null)
		{
			NpcUseCard(canCompleteCard);
		}
		else
		{
			canCompleteCard = GetNpcCanUseCard();
			if (canCompleteCard == null)
			{
				NpcEndRound();
				return;
			}
			NpcUseCard(canCompleteCard);
		}
		if (LunDaoManager.inst.gameState == LunDaoManager.GameState.Npc回合 && LunDaoManager.inst.lunTiMag.GetNullSlot() != -1)
		{
			((MonoBehaviour)this).Invoke("NpcAction", 1f);
		}
		else
		{
			NpcEndRound();
		}
	}

	public void NpcUseCard(LunDaoCard card)
	{
		if (isSayWord)
		{
			isSayWord = false;
			int random = LunDaoManager.inst.lunDaoCardMag.getRandom(1, 5);
			string str = jsonData.instance.LunDaoSayData[card.wudaoId.ToString()]["Desc" + random].Str;
			NpcSayWord(str);
		}
		LunDaoManager.inst.lunDaoAmrMag.AddChuPai(card.wudaoId);
		int nullSlot = LunDaoManager.inst.lunTiMag.GetNullSlot();
		LunDaoManager.inst.lunTiMag.curLunDianList[nullSlot].SetData(card.wudaoId, card.level);
		cards.Remove(card);
		LunDaoManager.inst.ChuPaiCallBack();
	}

	public LunDaoCard GetNpcCanUseCard()
	{
		List<LunDaoCard> shengYuLunDian = LunDaoManager.inst.lunTiMag.GetShengYuLunDian();
		int num = -1;
		LunDaoCard result = null;
		foreach (LunDaoCard card in cards)
		{
			foreach (LunDaoCard item in shengYuLunDian)
			{
				if (card.wudaoId == item.wudaoId && card.level <= item.level && num < card.level)
				{
					num = card.level;
					result = card;
				}
			}
		}
		return result;
	}

	public LunDaoCard GetCanCompleteCard()
	{
		foreach (LunDaoCard card in cards)
		{
			bool flag = false;
			VirtualUseCard(card, delegate(int index)
			{
				if (LunDaoManager.inst.lunTiMag.CheckIsTargetLunTi())
				{
					flag = true;
				}
				LunDaoManager.inst.lunTiMag.curLunDianList[index].SetNull();
			});
			if (flag)
			{
				return card;
			}
		}
		return null;
	}

	public void VirtualUseCard(LunDaoCard card, UnityAction<int> action)
	{
		int nullSlot = LunDaoManager.inst.lunTiMag.GetNullSlot();
		LunDaoManager.inst.lunTiMag.curLunDianList[nullSlot].SetData(card.wudaoId, card.level);
		action.Invoke(nullSlot);
	}

	public void NpcEndRound()
	{
		foreach (LunDaoCard card in cards)
		{
			LunDaoManager.inst.lunDaoCardMag.npcCards.Add(card);
		}
		cards = new List<LunDaoCard>();
		if (LunDaoManager.inst.gameState != LunDaoManager.GameState.论道结束)
		{
			LunDaoManager.inst.gameState = LunDaoManager.GameState.玩家回合;
		}
		LunDaoManager.inst.EndRoundCallBack();
		Debug.Log((object)"Npc回合结束");
	}

	public string GetNpcName()
	{
		return npcName.text;
	}

	public void ShowStateTips()
	{
		npcStateTips.gameObject.SetActive(true);
	}

	public void HideStateTips()
	{
		npcStateTips.gameObject.SetActive(false);
	}
}
