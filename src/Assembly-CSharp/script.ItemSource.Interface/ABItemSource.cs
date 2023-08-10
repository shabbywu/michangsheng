using System;
using System.Collections.Generic;

namespace script.ItemSource.Interface;

[Serializable]
public abstract class ABItemSource
{
	public Dictionary<int, ABItemSourceData> ItemSourceDataDic;

	public static ABItemSource Get()
	{
		return PlayerEx.Player.StreamData.ABItemSource;
	}

	public virtual void Init()
	{
		if (ItemSourceDataDic == null)
		{
			ItemSourceDataDic = new Dictionary<int, ABItemSourceData>();
		}
	}
}
