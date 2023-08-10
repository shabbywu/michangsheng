using System.Collections.Generic;
using JSONClass;
using script.ItemSource.Interface;

namespace script.ItemSource;

public class ItemSourceMag : ABItemSourceMag
{
	public ItemSourceMag()
	{
		Init();
	}

	private void Init()
	{
		Update = new ItemSourceUpdate();
		IO = new ItemSourceIO();
		Dictionary<int, ABItemSourceData> itemSourceDataDic = ABItemSource.Get().ItemSourceDataDic;
		foreach (_ItemJsonData data in _ItemJsonData.DataList)
		{
			if (data.id >= jsonData.QingJiaoItemIDSegment)
			{
				break;
			}
			if (data.ShuaXin >= 1)
			{
				if (itemSourceDataDic.ContainsKey(data.id))
				{
					itemSourceDataDic[data.id].UpdateTime = data.ShuaXin;
					continue;
				}
				itemSourceDataDic.Add(data.id, new ItemSourceData
				{
					Id = data.id,
					UpdateTime = data.ShuaXin,
					HasCostTime = 0
				});
			}
		}
	}
}
