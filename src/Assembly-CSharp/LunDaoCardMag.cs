using System;
using System.Collections.Generic;
using KBEngine;
using UnityEngine;
using UnityEngine.Events;

// Token: 0x0200030F RID: 783
public class LunDaoCardMag
{
	// Token: 0x06001B43 RID: 6979 RVA: 0x000C24DB File Offset: 0x000C06DB
	public LunDaoCardMag()
	{
		this.random = new Random();
		this.player = Tools.instance.getPlayer();
	}

	// Token: 0x06001B44 RID: 6980 RVA: 0x000C2500 File Offset: 0x000C0700
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

	// Token: 0x06001B45 RID: 6981 RVA: 0x000C2618 File Offset: 0x000C0818
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

	// Token: 0x06001B46 RID: 6982 RVA: 0x000C26A4 File Offset: 0x000C08A4
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

	// Token: 0x06001B47 RID: 6983 RVA: 0x000C27A5 File Offset: 0x000C09A5
	public int getRandom(int min, int max)
	{
		return this.random.Next(min, max + 1);
	}

	// Token: 0x040015B9 RID: 5561
	public List<LunDaoCard> playerCards;

	// Token: 0x040015BA RID: 5562
	public List<LunDaoCard> npcCards;

	// Token: 0x040015BB RID: 5563
	public Avatar player;

	// Token: 0x040015BC RID: 5564
	public Random random;
}
