using System;
using System.Collections;
using System.Collections.Generic;

// Token: 0x02000779 RID: 1913
public class SimpleSmartCard : SmartCard
{
	// Token: 0x060030D3 RID: 12499 RVA: 0x00023E4F File Offset: 0x0002204F
	private void Start()
	{
		this.computerNotice = base.transform.Find("ComputerNotice").gameObject;
		OrderController.Instance.smartCard += base.AutoDiscardCard;
	}

	// Token: 0x060030D4 RID: 12500 RVA: 0x00023E82 File Offset: 0x00022082
	public override IEnumerator DelayDiscardCard(bool isNone)
	{
		return base.DelayDiscardCard(isNone);
	}

	// Token: 0x060030D5 RID: 12501 RVA: 0x0018407C File Offset: 0x0018227C
	public override List<Card> FirstCard()
	{
		List<Card> list = new List<Card>();
		for (int i = 12; i >= 5; i--)
		{
			list = base.FindStraight(base.GetAllCards(null), 0, i, true);
			if (list.Count != 0)
			{
				break;
			}
		}
		if (list.Count == 0)
		{
			for (int j = 0; j < 36; j += 3)
			{
				list = base.FindThreeAndTwo(base.GetAllCards(null), j, true);
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
				list = base.FindThreeAndOne(base.GetAllCards(null), k, true);
				if (list.Count != 0)
				{
					break;
				}
			}
		}
		if (list.Count == 0)
		{
			for (int l = 0; l < 36; l += 3)
			{
				list = base.FindOnlyThree(base.GetAllCards(null), l, true);
				if (list.Count != 0)
				{
					break;
				}
			}
		}
		if (list.Count == 0)
		{
			for (int m = 0; m < 24; m += 2)
			{
				list = base.FindDouble(base.GetAllCards(null), m, true);
				if (list.Count != 0)
				{
					break;
				}
			}
		}
		if (list.Count == 0)
		{
			list = base.FindSingle(base.GetAllCards(null), 0, true);
		}
		return list;
	}
}
