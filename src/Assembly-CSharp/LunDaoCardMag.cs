using System;
using System.Collections.Generic;
using KBEngine;
using UnityEngine;
using UnityEngine.Events;

public class LunDaoCardMag
{
	public List<LunDaoCard> playerCards;

	public List<LunDaoCard> npcCards;

	public Avatar player;

	public Random random;

	public LunDaoCardMag()
	{
		random = new Random();
		player = Tools.instance.getPlayer();
	}

	public void CreatePaiKu(List<int> lunTiList, int npcId)
	{
		JSONObject jSONObject = jsonData.instance.AvatarJsonData[npcId.ToString()]["wuDaoJson"];
		int num = 0;
		int num2 = 0;
		npcCards = new List<LunDaoCard>();
		playerCards = new List<LunDaoCard>();
		foreach (int lunTi in lunTiList)
		{
			num = jSONObject[lunTi.ToString()]["level"].I;
			num2 = player.wuDaoMag.getWuDaoLevelByType(lunTi);
			for (int i = 1; i <= 5; i++)
			{
				for (int j = 1; j <= 3; j++)
				{
					npcCards.Add(new LunDaoCard(lunTi, (num - i >= 0) ? i : 0));
					playerCards.Add(new LunDaoCard(lunTi, (num2 - i >= 0) ? i : 0));
				}
			}
		}
	}

	public void NpcDrawCard(List<LunDaoCard> cards)
	{
		int num = 0;
		if (npcCards.Count >= 5)
		{
			for (int i = 0; i < 5; i++)
			{
				num = ((npcCards.Count != 1) ? getRandom(0, npcCards.Count - 1) : 0);
				cards.Add(new LunDaoCard(npcCards[num].wudaoId, npcCards[num].level));
				npcCards.RemoveAt(num);
			}
		}
	}

	public void PlayerDrawCard(List<LunDaoPlayerCard> cards)
	{
		//IL_00d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00de: Expected O, but got Unknown
		int num = 0;
		for (int i = 0; i < 5; i++)
		{
			num = getRandom(0, playerCards.Count - 1);
			LunDaoCard lunDaoCard = new LunDaoCard(playerCards[num].wudaoId, playerCards[num].level);
			playerCards.RemoveAt(num);
			GameObject obj = Object.Instantiate<GameObject>(LunDaoManager.inst.playerCardTemp, LunDaoManager.inst.playerCardTemp.transform.parent);
			LunDaoPlayerCard component = obj.GetComponent<LunDaoPlayerCard>();
			component.lunDaoCard = lunDaoCard;
			component.cardImage.sprite = LunDaoManager.inst.cardSpriteList[lunDaoCard.wudaoId - 1];
			component.cardLevel.text = lunDaoCard.level.ToString();
			cards.Add(component);
			component.btn.mouseUp.AddListener(new UnityAction(component.SelectCard));
			obj.gameObject.SetActive(true);
		}
	}

	public int getRandom(int min, int max)
	{
		return random.Next(min, max + 1);
	}
}
