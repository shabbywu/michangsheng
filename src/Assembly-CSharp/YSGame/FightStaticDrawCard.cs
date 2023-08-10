using System.Collections.Generic;
using UnityEngine;

namespace YSGame;

public class FightStaticDrawCard : MonoBehaviour
{
	public List<int> cardType = new List<int>();

	private int nowGuildIndex;

	private int allUsedCard;

	public int getCard()
	{
		int result = -1;
		if (allUsedCard < cardType.Count)
		{
			result = cardType[allUsedCard];
		}
		allUsedCard++;
		return result;
	}
}
