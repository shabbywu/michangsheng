using System;
using GUIPackage;
using JSONClass;
using UnityEngine.Events;

namespace Bag;

[Serializable]
public class DanYaoItem : BaseItem
{
	public int CanUse;

	public int DanDu;

	public override void SetItem(int id, int count)
	{
		base.SetItem(id, count);
		CanUse = _ItemJsonData.DataDict[id].CanUse;
		if (Tools.instance.getPlayer().checkHasStudyWuDaoSkillByID(2131))
		{
			CanUse *= 2;
		}
		DanDu = _ItemJsonData.DataDict[id].DanDu;
	}

	public override JiaoBiaoType GetJiaoBiaoType()
	{
		if (Tools.getJsonobject(Tools.instance.getPlayer().NaiYaoXin, string.Concat(Id)) >= CanUse && Type == 5)
		{
			return JiaoBiaoType.耐;
		}
		return base.GetJiaoBiaoType();
	}

	public int GetDanDu()
	{
		int num = _ItemJsonData.DataDict[Id].DanDu;
		if (_ItemJsonData.DataDict[Id].seid.Contains(29) && Tools.instance.getPlayer().level < ItemsSeidJsonData29.DataDict[Id].value1)
		{
			num += ItemsSeidJsonData29.DataDict[Id].value2;
		}
		return num;
	}

	public int GetHasUse()
	{
		return Tools.getJsonobject(Tools.instance.getPlayer().NaiYaoXin, Id.ToString());
	}

	public int GetMaxUseNum()
	{
		return item.GetItemCanUseNum(Id);
	}

	public override void Use()
	{
		//IL_0012: Unknown result type (might be due to invalid IL or missing references)
		//IL_001d: Expected O, but got Unknown
		new item(Id).gongneng((UnityAction)delegate
		{
			Tools.instance.getPlayer().removeItem(Id, 1);
			MessageMag.Instance.Send(MessageName.MSG_PLAYER_USE_ITEM);
		});
	}
}
