using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleSmartCard : SmartCard
{
	private void Start()
	{
		computerNotice = ((Component)((Component)this).transform.Find("ComputerNotice")).gameObject;
		OrderController.Instance.smartCard += base.AutoDiscardCard;
	}

	public override IEnumerator DelayDiscardCard(bool isNone)
	{
		return base.DelayDiscardCard(isNone);
	}

	public override List<Card> FirstCard()
	{
		List<Card> list = new List<Card>();
		for (int num = 12; num >= 5; num--)
		{
			list = FindStraight(GetAllCards(), 0, num, equal: true);
			if (list.Count != 0)
			{
				break;
			}
		}
		if (list.Count == 0)
		{
			for (int i = 0; i < 36; i += 3)
			{
				list = FindThreeAndTwo(GetAllCards(), i, equal: true);
				if (list.Count != 0)
				{
					break;
				}
			}
		}
		if (list.Count == 0)
		{
			for (int j = 0; j < 36; j += 3)
			{
				list = FindThreeAndOne(GetAllCards(), j, equal: true);
				if (list.Count != 0)
				{
					break;
				}
			}
		}
		if (list.Count == 0)
		{
			for (int k = 0; k < 36; k += 3)
			{
				list = FindOnlyThree(GetAllCards(), k, equal: true);
				if (list.Count != 0)
				{
					break;
				}
			}
		}
		if (list.Count == 0)
		{
			for (int l = 0; l < 24; l += 2)
			{
				list = FindDouble(GetAllCards(), l, equal: true);
				if (list.Count != 0)
				{
					break;
				}
			}
		}
		if (list.Count == 0)
		{
			list = FindSingle(GetAllCards(), 0, equal: true);
		}
		return list;
	}
}
