using System.Collections.Generic;

namespace KBEngine;

public class CardMag
{
	public Avatar entity;

	public List<card> _cardlist = new List<card>();

	public int Count => 6;

	public int this[int cardType]
	{
		get
		{
			int num = 0;
			foreach (card item in _cardlist)
			{
				if (item.cardType == cardType)
				{
					num++;
				}
			}
			return num;
		}
	}

	public void Clear()
	{
		_cardlist.Clear();
	}

	public card addCard(int cardType, int CardNum)
	{
		card card2 = new card(cardType);
		for (int i = 0; i < CardNum; i++)
		{
			_cardlist.Add(card2);
		}
		MessageMag.Instance.Send("Fight_CardChange");
		entity.fightTemp.SetAddLingQi(cardType, CardNum);
		return card2;
	}

	public void removeCard(int cardType, int CardNum)
	{
		if (CardNum == 0)
		{
			return;
		}
		int num = 0;
		for (int num2 = _cardlist.Count - 1; num2 >= 0; num2--)
		{
			if (_cardlist[num2].cardType == cardType)
			{
				_cardlist.RemoveAt(num2);
				num++;
				if (num == CardNum)
				{
					break;
				}
			}
		}
		MessageMag.Instance.Send("Fight_CardChange");
	}

	public void removeCard(card _card)
	{
		MessageMag.Instance.Send("Fight_CardChange");
		_cardlist.Remove(_card);
	}

	public card ChengCard(int oldType, int NewType)
	{
		foreach (card item in _cardlist)
		{
			if (item.cardType == oldType)
			{
				item.cardType = NewType;
				return item;
			}
		}
		return null;
	}

	public card getRandomCard()
	{
		List<card> list = _cardlist.FindAll((card aa) => aa.cardType < 5);
		if (list.Count > 0)
		{
			return list[jsonData.GetRandom() % list.Count];
		}
		return null;
	}

	public CardMag(Entity avater)
	{
		entity = (Avatar)avater;
	}

	public int getCardNum()
	{
		return _cardlist.Count;
	}

	public void ListRemoveCard(ref List<int> list32, int _cardType, int Num)
	{
		if (list32[_cardType] >= Num)
		{
			list32[_cardType] -= Num;
			return;
		}
		int num = Num - list32[_cardType];
		if (!entity.buffmag.HasBuffSeid(59))
		{
			return;
		}
		foreach (List<int> item in entity.buffmag.getBuffBySeid(59))
		{
			JSONObject jSONObject = jsonData.instance.BuffSeidJsonData[59][item[2]];
			int cardType = (int)jSONObject["value1"].n;
			if ((int)jSONObject["value2"].n == _cardType)
			{
				ListRemoveCard(ref list32, cardType, num);
			}
		}
	}

	public int getCardTypeNum(int cardType)
	{
		return this[cardType];
	}

	public bool HasNoEnoughNum(int lingQiType, int num)
	{
		if (getCardTypeNum(lingQiType) < num)
		{
			return true;
		}
		return false;
	}

	public bool HasEnoughNum(int lingQiType, int num)
	{
		if (getCardTypeNum(lingQiType) >= num)
		{
			return true;
		}
		return false;
	}

	public List<int> ToListInt32()
	{
		List<int> crystal = new List<int>();
		for (int i = 0; i < 6; i++)
		{
			crystal.Add(0);
		}
		_cardlist.ForEach(delegate(card a)
		{
			crystal[a.cardType]++;
		});
		return crystal;
	}
}
