using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000745 RID: 1861
public class PlayCard : MonoBehaviour
{
	// Token: 0x06002F42 RID: 12098 RVA: 0x0017BFE0 File Offset: 0x0017A1E0
	public bool CheckSelectCards()
	{
		CardSprite[] componentsInChildren = base.GetComponentsInChildren<CardSprite>();
		List<Card> list = new List<Card>();
		List<CardSprite> list2 = new List<CardSprite>();
		for (int i = 0; i < componentsInChildren.Length; i++)
		{
			if (componentsInChildren[i].Select)
			{
				list2.Add(componentsInChildren[i]);
				list.Add(componentsInChildren[i].Poker);
			}
		}
		CardRules.SortCards(list, true);
		return this.CheckPlayCards(list, list2);
	}

	// Token: 0x06002F43 RID: 12099 RVA: 0x0017C040 File Offset: 0x0017A240
	private bool CheckPlayCards(List<Card> selectedCardsList, List<CardSprite> selectedSpriteList)
	{
		GameController component = GameObject.Find("GameController").GetComponent<GameController>();
		Card[] cards = selectedCardsList.ToArray();
		CardsType cardsType;
		if (CardRules.PopEnable(cards, out cardsType))
		{
			CardsType rule = DeskCardsCache.Instance.Rule;
			if (OrderController.Instance.Biggest == OrderController.Instance.Type)
			{
				this.PlayCards(selectedCardsList, selectedSpriteList, cardsType);
				return true;
			}
			if (DeskCardsCache.Instance.Rule == CardsType.None)
			{
				this.PlayCards(selectedCardsList, selectedSpriteList, cardsType);
				return true;
			}
			if (cardsType == CardsType.Boom && rule != CardsType.Boom)
			{
				component.Multiples = 2;
				this.PlayCards(selectedCardsList, selectedSpriteList, cardsType);
				return true;
			}
			if (cardsType == CardsType.JokerBoom)
			{
				component.Multiples = 4;
				this.PlayCards(selectedCardsList, selectedSpriteList, cardsType);
				return true;
			}
			if (cardsType == CardsType.Boom && rule == CardsType.Boom && GameController.GetWeight(cards, cardsType) > DeskCardsCache.Instance.TotalWeight)
			{
				component.Multiples = 2;
				this.PlayCards(selectedCardsList, selectedSpriteList, cardsType);
				return true;
			}
			if (GameController.GetWeight(cards, cardsType) > DeskCardsCache.Instance.TotalWeight)
			{
				this.PlayCards(selectedCardsList, selectedSpriteList, cardsType);
				return true;
			}
		}
		return false;
	}

	// Token: 0x06002F44 RID: 12100 RVA: 0x0017C130 File Offset: 0x0017A330
	private void PlayCards(List<Card> selectedCardsList, List<CardSprite> selectedSpriteList, CardsType type)
	{
		HandCards component = GameObject.Find("Player").GetComponent<HandCards>();
		DeskCardsCache.Instance.Clear();
		DeskCardsCache.Instance.Rule = type;
		for (int i = 0; i < selectedSpriteList.Count; i++)
		{
			component.PopCard(selectedSpriteList[i].Poker);
			DeskCardsCache.Instance.AddCard(selectedSpriteList[i].Poker);
			selectedSpriteList[i].transform.parent = GameObject.Find("Desk").transform;
		}
		DeskCardsCache.Instance.Sort();
		GameController.AdjustCardSpritsPosition(CharacterType.Desk);
		GameController.AdjustCardSpritsPosition(CharacterType.Player);
		GameController.UpdateLeftCardsCount(CharacterType.Player, component.CardsCount);
		if (component.CardsCount == 0)
		{
			GameObject.Find("GameController").GetComponent<GameController>().GameOver();
			return;
		}
		OrderController.Instance.Biggest = CharacterType.Player;
		OrderController.Instance.Turn();
	}
}
