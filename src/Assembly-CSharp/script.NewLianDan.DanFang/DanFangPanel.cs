using System.Collections.Generic;
using Bag;
using JSONClass;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using script.NewLianDan.DanFang.Filter;

namespace script.NewLianDan.DanFang;

public class DanFangPanel : UIBase
{
	public DanFangFilter DanFangFilter;

	public Dictionary<int, string> FilterDataDict;

	public List<BigDanFang> DanFangList;

	public GameObject DanFangTemp;

	public int CurQuality;

	public SmallDanFang CurSmallDanFang;

	public BigDanFang CurBigDanFang;

	private Text _curQuality;

	public DanFangPanel(GameObject go)
	{
		//IL_0070: Unknown result type (might be due to invalid IL or missing references)
		//IL_007a: Expected O, but got Unknown
		//IL_0091: Unknown result type (might be due to invalid IL or missing references)
		//IL_009b: Expected O, but got Unknown
		//IL_00b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bc: Expected O, but got Unknown
		_go = go;
		FilterDataDict = GetFilterData();
		DanFangFilter = new DanFangFilter(Get("品阶选择界面"));
		_curQuality = Get<Text>("品阶筛选按钮/Value");
		DanFangTemp = Get("丹方列表/Mask/Content/Temp");
		UnityEvent mouseUpEvent = Get<FpBtn>("品阶筛选按钮").mouseUpEvent;
		DanFangFilter danFangFilter = DanFangFilter;
		mouseUpEvent.AddListener(new UnityAction(danFangFilter.Show));
		Get<FpBtn>("置顶丹方").mouseUpEvent.AddListener(new UnityAction(SetTop));
		Get<FpBtn>("删除丹方").mouseUpEvent.AddListener(new UnityAction(Delete));
	}

	public void UpdateFilter(int quality)
	{
		DanFangFilter.Hide();
		CurQuality = quality;
		_curQuality.SetText(FilterDataDict[quality]);
		UpdateDanFangList();
	}

	public void UpdateDanFangList()
	{
		Clear();
		CurBigDanFang = null;
		CurSmallDanFang = null;
		Dictionary<int, DanFangData> dictionary = new Dictionary<int, DanFangData>();
		DanFangList = new List<BigDanFang>();
		foreach (JSONObject item in Tools.instance.getPlayer().DanFang.list)
		{
			int i = item["ID"].I;
			if (CurQuality == 0 || _ItemJsonData.DataDict[i].quality == CurQuality)
			{
				DanFangBase danFangBase = new DanFangBase();
				if (item["Type"][0].I > 0)
				{
					danFangBase.YaoYin.Add(item["Type"][0].I, item["Num"][0].I);
				}
				if (item["Type"][1].I > 0)
				{
					danFangBase.ZhuYao1.Add(item["Type"][1].I, item["Num"][1].I);
					danFangBase.ZhuYaoYaoXin1 = _ItemJsonData.DataDict[item["Type"][1].I].yaoZhi2;
				}
				if (item["Type"][2].I > 0)
				{
					danFangBase.ZhuYao2.Add(item["Type"][2].I, item["Num"][2].I);
					danFangBase.ZhuYaoYaoXin2 = _ItemJsonData.DataDict[item["Type"][2].I].yaoZhi2;
				}
				if (item["Type"][3].I > 0)
				{
					danFangBase.FuYao1.Add(item["Type"][3].I, item["Num"][3].I);
					danFangBase.FuYaoYaoXin1 = _ItemJsonData.DataDict[item["Type"][3].I].yaoZhi3;
				}
				if (item["Type"][4].I > 0)
				{
					danFangBase.FuYao2.Add(item["Type"][4].I, item["Num"][4].I);
					danFangBase.FuYaoYaoXin2 = _ItemJsonData.DataDict[item["Type"][4].I].yaoZhi3;
				}
				danFangBase.Json = item.Copy();
				if (!dictionary.ContainsKey(i))
				{
					DanFangData danFangData = new DanFangData();
					danFangData.Id = i;
					danFangData.Name = _ItemJsonData.DataDict[i].name;
					danFangData.DanFangBases = new List<DanFangBase>();
					dictionary.Add(i, danFangData);
				}
				dictionary[i].DanFangBases.Add(danFangBase);
			}
		}
		foreach (int key in dictionary.Keys)
		{
			DanFangList.Add(new BigDanFang(DanFangTemp.Inst(DanFangTemp.transform.parent), dictionary[key], BaseItem.Create(key, 1, Tools.getUUID(), Tools.CreateItemSeid(key))));
		}
		UpdatePosition();
	}

	public void UpdatePosition()
	{
		//IL_0022: Unknown result type (might be due to invalid IL or missing references)
		//IL_0038: Unknown result type (might be due to invalid IL or missing references)
		//IL_0061: Unknown result type (might be due to invalid IL or missing references)
		//IL_0066: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c0: Unknown result type (might be due to invalid IL or missing references)
		if (DanFangList == null || DanFangList.Count < 1)
		{
			return;
		}
		float x = DanFangTemp.transform.localPosition.x;
		float num = DanFangTemp.transform.localPosition.y;
		foreach (BigDanFang danFang in DanFangList)
		{
			danFang.GetTransform().localPosition = Vector2.op_Implicit(new Vector2(x, num));
			num = ((!danFang.IsSelect) ? (num - 61f) : (num - danFang.GetHeight()));
		}
		((Component)DanFangTemp.transform.parent).GetComponent<RectTransform>().sizeDelta = new Vector2(464f, 0f - num);
	}

	public void Clear()
	{
		if (DanFangList == null)
		{
			return;
		}
		foreach (BigDanFang danFang in DanFangList)
		{
			danFang.Destroy();
		}
	}

	private Dictionary<int, string> GetFilterData()
	{
		return new Dictionary<int, string>
		{
			{ 0, "所有品阶" },
			{ 1, "一品" },
			{ 2, "二品" },
			{ 3, "三品" },
			{ 4, "四品" },
			{ 5, "五品" },
			{ 6, "六品" }
		};
	}

	public void SetBigDanFangCallBack(BigDanFang bigDanFang)
	{
		CurBigDanFang = bigDanFang;
		if (CurSmallDanFang != null && !CurSmallDanFang.IsNull())
		{
			CurSmallDanFang.CancelSelect();
		}
		UpdatePosition();
	}

	public void SetSmallDanFangCallBack(SmallDanFang smallDanFang)
	{
		if (CurSmallDanFang != null && smallDanFang != CurSmallDanFang)
		{
			if (CurSmallDanFang.IsDestroy)
			{
				CurSmallDanFang = null;
			}
			else
			{
				CurSmallDanFang.CancelSelect();
			}
		}
		CurSmallDanFang = smallDanFang;
	}

	public void PutCaoYaoByDanFang()
	{
		LianDanUIMag.Instance.LianDanPanel.BackAllCaoYao();
		DanFangBase danFangData = CurSmallDanFang.DanFangData;
		foreach (int key in danFangData.YaoYin.Keys)
		{
			LianDanUIMag.Instance.LianDanPanel.PutCaoYao(0, key, danFangData.YaoYin[key]);
		}
		foreach (int key2 in danFangData.ZhuYao1.Keys)
		{
			LianDanUIMag.Instance.LianDanPanel.PutCaoYao(1, key2, danFangData.ZhuYao1[key2]);
		}
		foreach (int key3 in danFangData.ZhuYao2.Keys)
		{
			LianDanUIMag.Instance.LianDanPanel.PutCaoYao(2, key3, danFangData.ZhuYao2[key3]);
		}
		foreach (int key4 in danFangData.FuYao1.Keys)
		{
			LianDanUIMag.Instance.LianDanPanel.PutCaoYao(3, key4, danFangData.FuYao1[key4]);
		}
		foreach (int key5 in danFangData.FuYao2.Keys)
		{
			LianDanUIMag.Instance.LianDanPanel.PutCaoYao(4, key5, danFangData.FuYao2[key5]);
		}
		LianDanUIMag.Instance.LianDanPanel.CheckCanMade();
	}

	public void SetTop()
	{
		if (CurBigDanFang == null || CurBigDanFang.IsNull() || DanFangList == null || DanFangList.Count < 2)
		{
			UIPopTip.Inst.Pop("请选择要置顶的丹方");
			return;
		}
		BigDanFang value = DanFangList[0];
		int index = DanFangList.IndexOf(CurBigDanFang);
		DanFangList[index] = value;
		DanFangList[0] = CurBigDanFang;
		UpdatePosition();
	}

	public void Delete()
	{
		//IL_0070: Unknown result type (might be due to invalid IL or missing references)
		//IL_007b: Expected O, but got Unknown
		if (CurSmallDanFang == null || CurSmallDanFang.IsNull() || CurSmallDanFang.Parent == null || CurSmallDanFang.Parent.IsNull())
		{
			UIPopTip.Inst.Pop("请选择要删除的丹方");
			return;
		}
		USelectBox.Show("确定要删除" + CurSmallDanFang.Parent.Data.Name + "吗", (UnityAction)delegate
		{
			int index = -1;
			IsSame(CurSmallDanFang.DanFangData.Json, ref index);
			if (index > -1)
			{
				BigDanFang parent = CurSmallDanFang.Parent;
				Tools.instance.getPlayer().DanFang.list.RemoveAt(index);
				CurSmallDanFang.Parent.RemoveChild(CurSmallDanFang);
				CurSmallDanFang = null;
				if (parent.ChildList == null || parent.ChildList.Count < 1)
				{
					DanFangList.Remove(parent);
					parent.Destroy();
					CurBigDanFang = null;
				}
				else
				{
					parent.CreateChild();
				}
				UpdatePosition();
				UIPopTip.Inst.Pop("删除成功");
			}
			else
			{
				UIPopTip.Inst.Pop("数据异常,删除失败");
			}
		});
	}

	public void IsSame(JSONObject obj, ref int index)
	{
		List<JSONObject> list = Tools.instance.getPlayer().DanFang.list;
		JSONObject jSONObject = obj["Type"];
		JSONObject jSONObject2 = obj["Num"];
		for (int i = 0; i < list.Count; i++)
		{
			if (list[i]["ID"].I != obj["ID"].I)
			{
				continue;
			}
			int num = 0;
			for (int j = 0; j < jSONObject.Count; j++)
			{
				if (list[i]["Type"][j].I == jSONObject[j].I && list[i]["Num"][j].I == jSONObject2[j].I)
				{
					num++;
				}
			}
			if (num == 5)
			{
				index = i;
				return;
			}
		}
		index = -1;
	}

	public void AddDanFang(JSONObject json)
	{
		int index = -1;
		IsSame(json, ref index);
		if (index != -1)
		{
			return;
		}
		Dictionary<int, DanFangData> dictionary = new Dictionary<int, DanFangData>();
		Tools.instance.getPlayer().DanFang.list.Add(json);
		bool flag = false;
		int i = json["ID"].I;
		if (CurQuality != 0 && _ItemJsonData.DataDict[i].quality != CurQuality)
		{
			return;
		}
		DanFangBase danFangBase = new DanFangBase();
		if (json["Type"][0].I > 0)
		{
			danFangBase.YaoYin.Add(json["Type"][0].I, json["Num"][0].I);
		}
		if (json["Type"][1].I > 0)
		{
			danFangBase.ZhuYao1.Add(json["Type"][1].I, json["Num"][1].I);
			danFangBase.ZhuYaoYaoXin1 = _ItemJsonData.DataDict[json["Type"][1].I].yaoZhi2;
		}
		if (json["Type"][2].I > 0)
		{
			danFangBase.ZhuYao2.Add(json["Type"][2].I, json["Num"][2].I);
			danFangBase.ZhuYaoYaoXin2 = _ItemJsonData.DataDict[json["Type"][2].I].yaoZhi2;
		}
		if (json["Type"][3].I > 0)
		{
			danFangBase.FuYao1.Add(json["Type"][3].I, json["Num"][3].I);
			danFangBase.FuYaoYaoXin1 = _ItemJsonData.DataDict[json["Type"][3].I].yaoZhi3;
		}
		if (json["Type"][4].I > 0)
		{
			danFangBase.FuYao2.Add(json["Type"][4].I, json["Num"][4].I);
			danFangBase.FuYaoYaoXin2 = _ItemJsonData.DataDict[json["Type"][4].I].yaoZhi3;
		}
		danFangBase.Json = json.Copy();
		if (CurSmallDanFang != null && !CurSmallDanFang.IsDestroy)
		{
			CurSmallDanFang.CancelSelect();
		}
		foreach (BigDanFang danFang in DanFangList)
		{
			if (i == danFang.Data.Id)
			{
				flag = true;
				danFang.Data.DanFangBases.Add(danFangBase);
				CurSmallDanFang = null;
				danFang.CreateChild();
				danFang.UpdateState();
				break;
			}
		}
		if (!flag)
		{
			DanFangData danFangData = new DanFangData();
			danFangData.Id = i;
			danFangData.Name = _ItemJsonData.DataDict[i].name;
			danFangData.DanFangBases = new List<DanFangBase>();
			dictionary.Add(i, danFangData);
			dictionary[i].DanFangBases.Add(danFangBase);
			DanFangList.Add(new BigDanFang(DanFangTemp.Inst(DanFangTemp.transform.parent), dictionary[i], BaseItem.Create(i, 1, Tools.getUUID(), Tools.CreateItemSeid(i))));
		}
		UpdatePosition();
	}
}
