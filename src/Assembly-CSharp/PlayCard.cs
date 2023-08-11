using System.Collections.Generic;
using UnityEngine;

public class PlayCard : MonoBehaviour
{
	public bool CheckSelectCards()
	{
		CardSprite[] componentsInChildren = ((Component)this).GetComponentsInChildren<CardSprite>();
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
		CardRules.SortCards(list, ascending: true);
		return CheckPlayCards(list, list2);
	}

	private bool CheckPlayCards(List<Card> selectedCardsList, List<CardSprite> selectedSpriteList)
	{
		GameController component = GameObject.Find("GameController").GetComponent<GameController>();
		Card[] cards = selectedCardsList.ToArray();
		if (CardRules.PopEnable(cards, out var type))
		{
			CardsType rule = DeskCardsCache.Instance.Rule;
			if (OrderController.Instance.Biggest == OrderController.Instance.Type)
			{
				PlayCards(selectedCardsList, selectedSpriteList, type);
				return true;
			}
			if (DeskCardsCache.Instance.Rule == CardsType.None)
			{
				PlayCards(selectedCardsList, selectedSpriteList, type);
				return true;
			}
			if (type == CardsType.Boom && rule != CardsType.Boom)
			{
				component.Multiples = 2;
				PlayCards(selectedCardsList, selectedSpriteList, type);
				return true;
			}
			switch (type)
			{
			case CardsType.JokerBoom:
				component.Multiples = 4;
				PlayCards(selectedCardsList, selectedSpriteList, type);
				return true;
			case CardsType.Boom:
				if (rule == CardsType.Boom && GameController.GetWeight(cards, type) > DeskCardsCache.Instance.TotalWeight)
				{
					component.Multiples = 2;
					PlayCards(selectedCardsList, selectedSpriteList, type);
					return true;
				}
				break;
			}
			if (GameController.GetWeight(cards, type) > DeskCardsCache.Instance.TotalWeight)
			{
				PlayCards(selectedCardsList, selectedSpriteList, type);
				return true;
			}
		}
		return false;
	}

	private void PlayCards(List<Card> selectedCardsList, List<CardSprite> selectedSpriteList, CardsType type)
	{
		HandCards component = GameObject.Find("Player").GetComponent<HandCards>();
		DeskCardsCache.Instance.Clear();
		DeskCardsCache.Instance.Rule = type;
		for (int i = 0; i < selectedSpriteList.Count; i++)
		{
			component.PopCard(selectedSpriteList[i].Poker);
			DeskCardsCache.Instance.AddCard(selectedSpriteList[i].Poker);
			((Component)selectedSpriteList[i]).transform.parent = GameObject.Find("Desk").transform;
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
