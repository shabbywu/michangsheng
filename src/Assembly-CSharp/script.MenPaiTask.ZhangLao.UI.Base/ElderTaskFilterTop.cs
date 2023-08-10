using System;
using Bag;
using JiaoYi;
using UnityEngine;
using UnityEngine.Events;

namespace script.MenPaiTask.ZhangLao.UI.Base;

public class ElderTaskFilterTop : JiaoYiFilterTop
{
	public override void CreateQuality()
	{
		//IL_0057: Unknown result type (might be due to invalid IL or missing references)
		//IL_0061: Expected O, but got Unknown
		//IL_00f1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fb: Expected O, but got Unknown
		Array values = Enum.GetValues(typeof(ItemQuality));
		int num = values.Length - 3;
		int num2 = 0;
		((Component)ChildUp).gameObject.Inst(Select.transform).GetComponent<BaseFilterTopChild>().Init(BaseBag2.ItemQuality.ToString(), (UnityAction)delegate
		{
			Select.SetActive(false);
			((Component)Unselect).gameObject.SetActive(true);
		});
		foreach (ItemQuality itemQuality in values)
		{
			BaseFilterTopChild baseFilterTopChild = null;
			baseFilterTopChild = ((num2 != num) ? ((Component)ChildCenter).gameObject.Inst(Select.transform).GetComponent<BaseFilterTopChild>() : ((Component)ChildDown).gameObject.Inst(Select.transform).GetComponent<BaseFilterTopChild>());
			baseFilterTopChild.Init(itemQuality.ToString(), (UnityAction)delegate
			{
				BaseBag2.ItemQuality = itemQuality;
				CurType.SetText((BaseBag2.ItemQuality == ItemQuality.全部) ? "品质" : itemQuality.ToString());
				BaseBag2.UpdateItem();
				Select.SetActive(false);
				((Component)Unselect).gameObject.SetActive(true);
			});
			if (num2 == num)
			{
				break;
			}
			num2++;
		}
	}
}
