using System.Collections.Generic;
using Bag;
using KBEngine;
using UnityEngine;
using script.NewLianDan.DanFang;
using script.NewLianDan.LianDan;
using script.NewLianDan.PutDanLu;
using script.NewLianDan.Result;

namespace script.NewLianDan;

public class LianDanUIMag : MonoBehaviour
{
	public static LianDanUIMag Instance;

	public DanFangPanel DanFangPanel;

	public PutDanLuPanel PutDanLuPanel;

	public LianDanPanel LianDanPanel;

	public LianDanResult LianDanResult;

	public DanLuBag DanLuBag;

	public BagItemSelect Select;

	public CaoYaoBag CaoYaoBag;

	public Transform Vector2;

	private void Awake()
	{
		//IL_002e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0042: Unknown result type (might be due to invalid IL or missing references)
		Instance = this;
		((Component)Instance).transform.SetParent(((Component)NewUICanvas.Inst).gameObject.transform);
		((Component)Instance).transform.localScale = Vector3.one;
		((Component)Instance).transform.localPosition = Vector3.zero;
		((Component)Instance).transform.SetAsLastSibling();
		Instance.Init();
		if ((Object)(object)UIHeadPanel.Inst != (Object)null)
		{
			((Component)UIHeadPanel.Inst).transform.SetAsLastSibling();
		}
		if ((Object)(object)UIMiniTaskPanel.Inst != (Object)null)
		{
			((Component)UIMiniTaskPanel.Inst).transform.SetAsLastSibling();
		}
	}

	private void Init()
	{
		LianDanResult = new LianDanResult(((Component)((Component)this).transform.Find("炼丹结果")).gameObject);
		LianDanPanel = new LianDanPanel(((Component)((Component)this).transform.Find("炼丹界面")).gameObject);
		DanFangPanel = new DanFangPanel(((Component)((Component)this).transform.Find("丹方")).gameObject);
		DanFangPanel.UpdateFilter(0);
		PutDanLuPanel = new PutDanLuPanel(((Component)((Component)this).transform.Find("放入丹炉界面")).gameObject);
		DanLuBag.Init(0, isPlayer: true);
		CaoYaoBag.Init(0, isPlayer: true);
	}

	public void Close()
	{
		PanelMamager.inst.closePanel(PanelMamager.PanelType.炼丹);
		Object.Destroy((Object)(object)((Component)this).gameObject);
	}

	public void LianDanCallBack()
	{
		foreach (BigDanFang danFang in DanFangPanel.DanFangList)
		{
			danFang.UpdateState();
		}
		DanLuBag.CreateTempList();
		DanLuBag.UpdateItem();
		LianDanPanel.CheckCanMade();
		LianDanPanel.DanLuUI();
	}

	public bool CheckCanLianZhi(JSONObject child)
	{
		if (child == null)
		{
			return false;
		}
		List<int> list = child["Type"].ToList();
		List<int> list2 = child["Num"].ToList();
		Dictionary<int, int> dictionary = new Dictionary<int, int>();
		for (int i = 0; i < list.Count; i++)
		{
			if (!dictionary.ContainsKey(list[i]))
			{
				dictionary.Add(list[i], list2[i]);
			}
			else
			{
				dictionary[list[i]] += list2[i];
			}
		}
		foreach (int key in dictionary.Keys)
		{
			if (dictionary[key] <= 0)
			{
				continue;
			}
			bool flag = false;
			foreach (ITEM_INFO value in Tools.instance.getPlayer().itemList.values)
			{
				if (key == value.itemId && dictionary[key] <= value.itemCount)
				{
					flag = true;
				}
			}
			if (!flag)
			{
				return false;
			}
		}
		return true;
	}

	private void OnDestroy()
	{
		Instance = null;
		PanelMamager.inst.closePanel(PanelMamager.PanelType.炼丹, 1);
		if ((Object)(object)UIMiniTaskPanel.Inst != (Object)null)
		{
			((Component)UIMiniTaskPanel.Inst).transform.SetAsFirstSibling();
		}
		if ((Object)(object)UIHeadPanel.Inst != (Object)null)
		{
			((Component)UIHeadPanel.Inst).transform.SetAsFirstSibling();
		}
	}
}
