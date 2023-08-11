using System;
using Bag;
using JiaoYi;
using UnityEngine;
using UnityEngine.Events;

namespace script.ExchangeMeeting.UI.Base;

public class ExchangeFilterTop : JiaoYiFilterTop
{
	public override void CreateQuality()
	{
		//IL_0054: Unknown result type (might be due to invalid IL or missing references)
		//IL_005e: Expected O, but got Unknown
		//IL_00f6: Unknown result type (might be due to invalid IL or missing references)
		//IL_0100: Expected O, but got Unknown
		Array values = Enum.GetValues(typeof(ItemQuality));
		int length = values.Length;
		((Component)ChildUp).gameObject.Inst(Select.transform).GetComponent<BaseFilterTopChild>().Init(BaseBag2.ItemQuality.ToString(), (UnityAction)delegate
		{
			Select.SetActive(false);
			((Component)Unselect).gameObject.SetActive(true);
		});
		for (int i = 0; i < length; i++)
		{
			if (i <= 0 || i >= 5)
			{
				ItemQuality itemQuality = (ItemQuality)values.GetValue(i);
				BaseFilterTopChild baseFilterTopChild = null;
				baseFilterTopChild = ((i != length) ? ((Component)ChildCenter).gameObject.Inst(Select.transform).GetComponent<BaseFilterTopChild>() : ((Component)ChildDown).gameObject.Inst(Select.transform).GetComponent<BaseFilterTopChild>());
				baseFilterTopChild.Init(itemQuality.ToString(), (UnityAction)delegate
				{
					BaseBag2.ItemQuality = itemQuality;
					CurType.SetText((BaseBag2.ItemQuality == ItemQuality.全部) ? "品质" : itemQuality.ToString());
					BaseBag2.UpdateItem();
					Select.SetActive(false);
					((Component)Unselect).gameObject.SetActive(true);
				});
			}
		}
	}
}
