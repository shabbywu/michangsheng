using System;
using JSONClass;
using UnityEngine;

namespace Bag;

[Serializable]
public class CaiLiaoItem : BaseItem
{
	public int WuWeiType;

	public int ShuXingType;

	public override void SetItem(int id, int count)
	{
		base.SetItem(id, count);
		WuWeiType = _ItemJsonData.DataDict[id].WuWeiType;
		ShuXingType = _ItemJsonData.DataDict[id].ShuXingType;
	}

	public string GetZhongLei()
	{
		return LianQiWuWeiBiao.DataDict[WuWeiType].desc;
	}

	public string GetShuXing()
	{
		return LianQiShuXinLeiBie.DataDict[ShuXingType].desc;
	}

	public LianQiCaiLiaoYinYang GetYinYang()
	{
		if (ShuXingType % 10 == 1)
		{
			return LianQiCaiLiaoYinYang.阳;
		}
		return LianQiCaiLiaoYinYang.阴;
	}

	public LianQiCaiLiaoType GetLianQiCaiLiaoType()
	{
		switch (ShuXingType / 10)
		{
		case 0:
			return LianQiCaiLiaoType.金;
		case 1:
			return LianQiCaiLiaoType.木;
		case 2:
			return LianQiCaiLiaoType.水;
		case 3:
			return LianQiCaiLiaoType.火;
		case 4:
			return LianQiCaiLiaoType.土;
		case 5:
			return LianQiCaiLiaoType.混元;
		case 6:
			return LianQiCaiLiaoType.神念;
		case 7:
			return LianQiCaiLiaoType.剑;
		default:
			Debug.LogError((object)$"不存在的材料属性{ShuXingType},itemId{Id}");
			return LianQiCaiLiaoType.全部;
		}
	}
}
