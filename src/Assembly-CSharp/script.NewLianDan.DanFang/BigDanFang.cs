using System;
using System.Collections.Generic;
using Bag;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace script.NewLianDan.DanFang;

public class BigDanFang : UIBase
{
	private bool _isSelect;

	public DanFangData Data;

	public BaseItem BaseItem;

	private GameObject _unSelect;

	private GameObject _canMade1;

	private GameObject _select;

	private GameObject _canMade2;

	private Text _zhuYao1;

	private Text _fuYao1;

	public List<SmallDanFang> ChildList = new List<SmallDanFang>();

	public bool IsSelect => _isSelect;

	public BigDanFang(GameObject go, DanFangData data, BaseItem baseItem)
	{
		//IL_003d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0047: Expected O, but got Unknown
		//IL_005e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0068: Expected O, but got Unknown
		//IL_007f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0089: Expected O, but got Unknown
		//IL_00da: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e4: Expected O, but got Unknown
		//IL_00fb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0105: Expected O, but got Unknown
		//IL_011c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0126: Expected O, but got Unknown
		_go = go;
		Data = data;
		BaseItem = baseItem;
		Get<FpBtn>("未选中").mouseUpEvent.AddListener(new UnityAction(Select));
		Get<FpBtn>("未选中").mouseEnterEvent.AddListener(new UnityAction(Enter));
		Get<FpBtn>("未选中").mouseOutEvent.AddListener(new UnityAction(Out));
		_unSelect = Get("未选中");
		_canMade1 = Get("未选中/小丹炉/可炼制标记");
		Get<Text>("未选中/名称").SetText(data.Name);
		Get<FpBtn>("已选中").mouseUpEvent.AddListener(new UnityAction(UnSelect));
		Get<FpBtn>("已选中").mouseEnterEvent.AddListener(new UnityAction(Enter));
		Get<FpBtn>("已选中").mouseOutEvent.AddListener(new UnityAction(Out));
		_select = Get("已选中");
		Get<Text>("已选中/名称").SetText(data.Name);
		_canMade2 = Get("已选中/小丹炉/可炼制标记");
		_zhuYao1 = Get<Text>("已选中/药性/主药");
		_fuYao1 = Get<Text>("已选中/药性/辅药");
		_isSelect = false;
		_go.SetActive(true);
		CreateChild();
		UpdateState();
	}

	public void CreateChild()
	{
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		//IL_002a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0068: Unknown result type (might be due to invalid IL or missing references)
		//IL_006d: Unknown result type (might be due to invalid IL or missing references)
		Clear();
		GameObject val = Get("已选中/Temp");
		float x = val.transform.localPosition.x;
		float num = val.transform.localPosition.y;
		for (int i = 0; i < Data.DanFangBases.Count; i++)
		{
			DanFangBase data = Data.DanFangBases[i];
			GameObject val2 = val.Inst(val.transform.parent);
			val2.transform.localPosition = Vector2.op_Implicit(new Vector2(x, num));
			ChildList.Add(new SmallDanFang(val2, data, this));
			num -= 112f;
		}
		if (ChildList.Count > 0)
		{
			ChildList[ChildList.Count - 1].HideLine();
		}
	}

	public float GetHeight()
	{
		//IL_001d: Unknown result type (might be due to invalid IL or missing references)
		return Math.Abs(ChildList[ChildList.Count - 1].GetTransform().localPosition.y) + 107f;
	}

	private void Select()
	{
		_isSelect = true;
		_unSelect.SetActive(false);
		_select.SetActive(true);
		CreateChild();
		foreach (SmallDanFang child in ChildList)
		{
			if (LianDanUIMag.Instance.CheckCanLianZhi(child.DanFangData.Json))
			{
				child.CanMade = true;
				child.SmallDanLu.SetActive(true);
			}
			else
			{
				child.CanMade = false;
				child.SmallDanLu.SetActive(false);
			}
		}
		LianDanUIMag.Instance.DanFangPanel.SetBigDanFangCallBack(this);
	}

	public void Enter()
	{
		//IL_0041: Unknown result type (might be due to invalid IL or missing references)
		//IL_0055: Unknown result type (might be due to invalid IL or missing references)
		if ((Object)(object)ToolTipsMag.Inst == (Object)null)
		{
			ResManager.inst.LoadPrefab("ToolTips").Inst(((Component)NewUICanvas.Inst).transform);
		}
		ToolTipsMag.Inst.Show(BaseItem, new Vector2(LianDanUIMag.Instance.Vector2.position.x, ToolTipsMag.Inst.GetMouseY()));
	}

	public void Out()
	{
		ToolTipsMag.Inst.Close();
	}

	private void UnSelect()
	{
		_isSelect = false;
		_unSelect.SetActive(true);
		_select.SetActive(false);
		LianDanUIMag.Instance.DanFangPanel.UpdatePosition();
	}

	public void UpdateState()
	{
		DanFangBase danFangData = ChildList[0].DanFangData;
		_zhuYao1.SetText("");
		if (danFangData.ZhuYaoYaoXin1 > 0)
		{
			_zhuYao1.SetText(Tools.getLiDanLeiXinStr(danFangData.ZhuYaoYaoXin1));
		}
		if (danFangData.ZhuYaoYaoXin2 > 0)
		{
			string liDanLeiXinStr = Tools.getLiDanLeiXinStr(danFangData.ZhuYaoYaoXin2);
			if (!_zhuYao1.text.Contains(liDanLeiXinStr))
			{
				if (_zhuYao1.text.Length > 0)
				{
					_zhuYao1.AddText(",");
				}
				_zhuYao1.AddText(Tools.getLiDanLeiXinStr(danFangData.ZhuYaoYaoXin2));
			}
		}
		_fuYao1.SetText("");
		if (danFangData.FuYaoYaoXin1 > 0)
		{
			_fuYao1.SetText(Tools.getLiDanLeiXinStr(danFangData.FuYaoYaoXin1));
		}
		if (danFangData.FuYaoYaoXin2 > 0)
		{
			string liDanLeiXinStr2 = Tools.getLiDanLeiXinStr(danFangData.FuYaoYaoXin2);
			if (!_fuYao1.text.Contains(liDanLeiXinStr2))
			{
				if (_fuYao1.text.Length > 0)
				{
					_fuYao1.AddText(",");
				}
				_fuYao1.AddText(Tools.getLiDanLeiXinStr(danFangData.FuYaoYaoXin2));
			}
		}
		if (CheckCanMade())
		{
			_canMade1.SetActive(true);
			_canMade2.SetActive(true);
		}
		else
		{
			_canMade1.SetActive(false);
			_canMade2.SetActive(false);
		}
	}

	public bool CheckCanMade()
	{
		if (ChildList.Count == 0)
		{
			return false;
		}
		bool result = false;
		foreach (SmallDanFang child in ChildList)
		{
			if (LianDanUIMag.Instance.CheckCanLianZhi(child.DanFangData.Json))
			{
				child.CanMade = true;
				result = true;
				child.SmallDanLu.SetActive(true);
			}
			else
			{
				child.CanMade = false;
				child.SmallDanLu.SetActive(false);
			}
		}
		return result;
	}

	private void Clear()
	{
		if (ChildList.Count > 0)
		{
			foreach (SmallDanFang child in ChildList)
			{
				child.Destroy();
			}
		}
		ChildList = new List<SmallDanFang>();
	}

	public void RemoveChild(SmallDanFang smallDanFang)
	{
		ChildList.Remove(smallDanFang);
		Data.DanFangBases.Remove(smallDanFang.DanFangData);
		smallDanFang.Destroy();
	}

	public void Destroy()
	{
		Object.Destroy((Object)(object)_go);
	}
}
