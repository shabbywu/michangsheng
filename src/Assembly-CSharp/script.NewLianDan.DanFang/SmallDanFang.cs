using System.Collections.Generic;
using Bag;
using JSONClass;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace script.NewLianDan.DanFang;

public class SmallDanFang : UIBase
{
	public DanFangBase DanFangData;

	public GameObject SmallDanLu;

	private GameObject _line;

	private GameObject _selectImg;

	public BigDanFang Parent;

	public bool CanMade;

	public bool IsSelect;

	public bool IsDestroy;

	public SmallDanFang(GameObject go, DanFangBase data, BigDanFang bigDanFang)
	{
		//IL_0070: Unknown result type (might be due to invalid IL or missing references)
		//IL_007a: Expected O, but got Unknown
		Parent = bigDanFang;
		_go = go;
		CanMade = false;
		_go.SetActive(true);
		IsSelect = false;
		DanFangData = data;
		SmallDanLu = Get("小丹炉/可炼制标记");
		_selectImg = Get("选中");
		_go.GetComponent<FpBtn>().mouseUpEvent.AddListener(new UnityAction(Click));
		_line = Get("Line");
		Text val = Get<Text>("主药/草药");
		val.SetText("");
		foreach (int key in data.ZhuYao1.Keys)
		{
			val.AddText($"{_ItemJsonData.DataDict[key].name}x{data.ZhuYao1[key]}");
		}
		foreach (int key2 in data.ZhuYao2.Keys)
		{
			if (val.text.Length > 0)
			{
				val.AddText(",");
			}
			val.AddText($"{_ItemJsonData.DataDict[key2].name}x{data.ZhuYao2[key2]}");
		}
		Text val2 = Get<Text>("辅药/草药");
		val2.SetText("");
		foreach (int key3 in data.FuYao1.Keys)
		{
			val2.AddText($"{_ItemJsonData.DataDict[key3].name}x{data.FuYao1[key3]}");
		}
		foreach (int key4 in data.FuYao2.Keys)
		{
			if (val2.text.Length > 0)
			{
				val2.AddText(",");
			}
			val2.AddText($"{_ItemJsonData.DataDict[key4].name}x{data.FuYao2[key4]}");
		}
		if (val2.text.Length < 1)
		{
			val2.SetText("无");
		}
		Text val3 = Get<Text>("药引/草药");
		val3.SetText("");
		foreach (int key5 in data.YaoYin.Keys)
		{
			val3.AddText($"{_ItemJsonData.DataDict[key5].name}x{data.YaoYin[key5]}");
		}
		if (val3.text.Length < 1)
		{
			val3.SetText("无");
		}
	}

	public void Click()
	{
		if (!IsSelect)
		{
			IsSelect = true;
			_selectImg.SetActive(true);
		}
		LianDanUIMag.Instance.DanFangPanel.SetSmallDanFangCallBack(this);
		if (CanMade)
		{
			if (LianDanUIMag.Instance.LianDanPanel.DanLu.IsNull())
			{
				UIPopTip.Inst.Pop("请先放入丹炉");
			}
			else if (!CanPut())
			{
				UIPopTip.Inst.Pop("丹炉品阶不足");
			}
			else
			{
				LianDanUIMag.Instance.DanFangPanel.PutCaoYaoByDanFang();
			}
		}
	}

	public bool CanPut()
	{
		List<LianDanSlot> caoYaoList = LianDanUIMag.Instance.LianDanPanel.CaoYaoList;
		int num = 0;
		if (DanFangData.ZhuYao1.Keys.Count > 0)
		{
			num++;
		}
		if (DanFangData.ZhuYao2.Keys.Count > 0)
		{
			num++;
		}
		int num2 = 0;
		if (!caoYaoList[1].IsLock)
		{
			num2++;
		}
		if (!caoYaoList[2].IsLock)
		{
			num2++;
		}
		if (num2 < num)
		{
			return false;
		}
		int num3 = 0;
		if (DanFangData.FuYao1.Keys.Count > 0)
		{
			num3++;
		}
		if (DanFangData.FuYao2.Keys.Count > 0)
		{
			num3++;
		}
		int num4 = 0;
		if (!caoYaoList[3].IsLock)
		{
			num4++;
		}
		if (!caoYaoList[4].IsLock)
		{
			num4++;
		}
		if (num4 < num3)
		{
			return false;
		}
		return true;
	}

	public void CancelSelect()
	{
		IsSelect = false;
		_selectImg.SetActive(false);
	}

	public void HideLine()
	{
		_line.SetActive(false);
	}

	public void Destroy()
	{
		IsDestroy = true;
		Object.Destroy((Object)(object)_go);
	}
}
