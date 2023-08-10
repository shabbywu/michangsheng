using UnityEngine;

namespace script.ItemSource.Interface;

public abstract class ABItemSourceIO
{
	public virtual bool NeedCheckCount(int id)
	{
		return ABItemSource.Get().ItemSourceDataDic.ContainsKey(id);
	}

	public virtual bool Get(int id)
	{
		if (ABItemSource.Get().ItemSourceDataDic.ContainsKey(id))
		{
			if (ABItemSource.Get().ItemSourceDataDic[id].Count == 1)
			{
				ABItemSource.Get().ItemSourceDataDic[id].Count = 0;
				return true;
			}
			return false;
		}
		Debug.LogError((object)("物品Id：" + id + "不在自动生成表中"));
		return false;
	}
}
