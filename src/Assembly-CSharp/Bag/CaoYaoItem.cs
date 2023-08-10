using System;
using GUIPackage;
using JSONClass;
using UnityEngine.Events;

namespace Bag;

[Serializable]
public class CaoYaoItem : BaseItem
{
	public int YaoYin;

	public int ZhuYao;

	public int FuYao;

	public override void SetItem(int id, int count)
	{
		base.SetItem(id, count);
		YaoYin = _ItemJsonData.DataDict[id].yaoZhi1;
		ZhuYao = _ItemJsonData.DataDict[id].yaoZhi2;
		FuYao = _ItemJsonData.DataDict[id].yaoZhi3;
	}

	public string GetZhuYao()
	{
		if (Tools.instance.getPlayer().GetHasZhuYaoShuXin(Id, Quality))
		{
			return Tools.getLiDanLeiXinStr(ZhuYao);
		}
		return "未知";
	}

	public string GetFuYao()
	{
		if (Tools.instance.getPlayer().GetHasFuYaoShuXin(Id, Quality))
		{
			return Tools.getLiDanLeiXinStr(FuYao);
		}
		return "未知";
	}

	public string GetYaoYin()
	{
		if (Tools.instance.getPlayer().GetHasYaoYinShuXin(Id, Quality))
		{
			return Tools.getLiDanLeiXinStr(YaoYin);
		}
		return "未知";
	}

	public int GetZhuYaoId()
	{
		if (Tools.instance.getPlayer().GetHasZhuYaoShuXin(Id, Quality))
		{
			return ZhuYao;
		}
		return -1;
	}

	public int GetFuYaoId()
	{
		if (Tools.instance.getPlayer().GetHasFuYaoShuXin(Id, Quality))
		{
			return FuYao;
		}
		return -1;
	}

	public int GetYaoYinId()
	{
		if (Tools.instance.getPlayer().GetHasYaoYinShuXin(Id, Quality))
		{
			return YaoYin;
		}
		return -1;
	}

	public override void Use()
	{
		//IL_002a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0035: Expected O, but got Unknown
		if (_ItemJsonData.DataDict[Id].vagueType == 1)
		{
			new item(Id).gongneng((UnityAction)delegate
			{
				Tools.instance.getPlayer().removeItem(Id, 1);
				MessageMag.Instance.Send(MessageName.MSG_PLAYER_USE_ITEM);
			});
		}
	}
}
