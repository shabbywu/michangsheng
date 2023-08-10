using System.Collections.Generic;
using script.ItemSource.Interface;

namespace script.ItemSource;

public class ItemSourceUpdate : ABItemSourceUpdate
{
	public override void Update(int times)
	{
		Dictionary<int, ABItemSourceData> itemSourceDataDic = ABItemSource.Get().ItemSourceDataDic;
		foreach (int key in itemSourceDataDic.Keys)
		{
			itemSourceDataDic[key].HasCostTime += times;
			if (itemSourceDataDic[key].HasCostTime >= itemSourceDataDic[key].UpdateTime)
			{
				itemSourceDataDic[key].HasCostTime = 0;
				itemSourceDataDic[key].Count = 1;
			}
		}
	}
}
