using System;
using System.Collections.Generic;
using KBEngine;
using UnityEngine;
using UnityEngine.Events;

// Token: 0x02000470 RID: 1136
public class LunDaoCardMag
{
	// Token: 0x06001E74 RID: 7796 RVA: 0x00019452 File Offset: 0x00017652
	public LunDaoCardMag()
	{
		this.random = new Random();
		this.player = Tools.instance.getPlayer();
	}

	// Token: 0x06001E75 RID: 7797 RVA: 0x00107B40 File Offset: 0x00105D40
	public void CreatePaiKu(List<int> lunTiList, int npcId)
	{
		JSONObject jsonobject = jsonData.instance.AvatarJsonData[npcId.ToString()]["wuDaoJson"];
		this.npcCards = new List<LunDaoCard>();
		this.playerCards = new List<LunDaoCard>();
		foreach (int num in lunTiList)
		{
			int i = jsonobject[num.ToString()]["level"].I;
			int wuDaoLevelByType = this.player.wuDaoMag.getWuDaoLevelByType(num);
			for (int j = 1; j <= 5; j++)
			{
				for (int k = 1; k <= 3; k++)
				{
					this.npcCards.Add(new LunDaoCard(num, (i - j >= 0) ? j : 0));
					this.playerCards.Add(new LunDaoCard(num, (wuDaoLevelByType - j >= 0) ? j : 0));
				}
			}
		}
	}

	// Token: 0x06001E76 RID: 7798 RVA: 0x00107C58 File Offset: 0x00105E58
	public void NpcDrawCard(List<LunDaoCard> cards)
	{
		if (this.npcCards.Count < 5)
		{
			return;
		}
		for (int i = 0; i < 5; i++)
		{
			int index;
			if (this.npcCards.Count == 1)
			{
				index = 0;
			}
			else
			{
				index = this.getRandom(0, this.npcCards.Count - 1);
			}
			cards.Add(new LunDaoCard(this.npcCards[index].wudaoId, this.npcCards[index].level));
			this.npcCards.RemoveAt(index);
		}
	}

	// Token: 0x06001E77 RID: 7799 RVA: 0x00107CE4 File Offset: 0x00105EE4
	public void PlayerDrawCard(List<LunDaoPlayerCard> cards)
	{
		for (int i = 0; i < 5; i++)
		{
			int index = this.getRandom(0, this.playerCards.Count - 1);
			LunDaoCard lunDaoCard = new LunDaoCard(this.playerCards[index].wudaoId, this.playerCards[index].level);
			this.playerCards.RemoveAt(index);
			GameObject gameObject = Object.Instantiate<GameObject>(LunDaoManager.inst.playerCardTemp, LunDaoManager.inst.playerCardTemp.transform.parent);
			LunDaoPlayerCard component = gameObject.GetComponent<LunDaoPlayerCard>();
			component.lunDaoCard = lunDaoCard;
			component.cardImage.sprite = LunDaoManager.inst.cardSpriteList[lunDaoCard.wudaoId - 1];
			component.cardLevel.text = lunDaoCard.level.ToString();
			cards.Add(component);
			component.btn.mouseUp.AddListener(new UnityAction(component.SelectCard));
			gameObject.gameObject.SetActive(true);
		}
	}

	// Token: 0x06001E78 RID: 7800 RVA: 0x00019475 File Offset: 0x00017675
	public int getRandom(int min, int max)
	{
		return this.random.Next(min, max - 1);
	}

	// Token: 0x040019CF RID: 6607
	public List<LunDaoCard> playerCards;

	// Token: 0x040019D0 RID: 6608
	public List<LunDaoCard> npcCards;

	// Token: 0x040019D1 RID: 6609
	public Avatar player;

	// Token: 0x040019D2 RID: 6610
	public Random random;
}
