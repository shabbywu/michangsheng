using System.Collections.Generic;
using KBEngine;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class DanFangPageManager : MonoBehaviour
{
	public enum DanFangPingJie
	{
		所有,
		一品,
		二品,
		三品,
		四品,
		五品,
		六品
	}

	[SerializeField]
	private GameObject parentDanFang;

	private DanFangPingJie danFangPingJie;

	[SerializeField]
	public List<Sprite> sprites = new List<Sprite>();

	[HideInInspector]
	public List<DanFangParentCell> danFangParentCells;

	[SerializeField]
	private RectTransform rectTransform;

	[HideInInspector]
	public GameObject curSelectDanFang;

	[HideInInspector]
	public GameObject curSelectDanFangBg;

	[HideInInspector]
	public JSONObject curSelectJSONObject;

	[HideInInspector]
	public DanFangParentCell curSelectDanFanParent;

	[SerializeField]
	private Button deleteButton;

	[SerializeField]
	private Button topButton;

	[SerializeField]
	public List<Sprite> pingJieSprites = new List<Sprite>();

	public void init()
	{
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		//IL_0022: Expected O, but got Unknown
		//IL_0034: Unknown result type (might be due to invalid IL or missing references)
		//IL_003e: Expected O, but got Unknown
		updateDanFang();
		((UnityEvent)deleteButton.onClick).AddListener(new UnityAction(deleteDanFang));
		((UnityEvent)topButton.onClick).AddListener(new UnityAction(topDanFang));
	}

	public void updateDanFang()
	{
		Tools.ClearObj(parentDanFang.transform);
		danFangParentCells = new List<DanFangParentCell>();
		Dictionary<int, List<JSONObject>> noSameDanFangList = getNoSameDanFangList();
		if (noSameDanFangList.Keys.Count <= 0)
		{
			return;
		}
		foreach (int key in noSameDanFangList.Keys)
		{
			DanFangParentCell component = Tools.InstantiateGameObject(parentDanFang, parentDanFang.transform.parent).GetComponent<DanFangParentCell>();
			component.DanFangID = key;
			component.childs = noSameDanFangList[key];
			component.init();
			danFangParentCells.Add(component);
		}
	}

	private Dictionary<int, List<JSONObject>> getNoSameDanFangList()
	{
		Dictionary<int, List<JSONObject>> dictionary = new Dictionary<int, List<JSONObject>>();
		List<JSONObject> list = Tools.instance.getPlayer().DanFang.list;
		for (int i = 0; i < list.Count; i++)
		{
			if (danFangPingJie == DanFangPingJie.所有 || jsonData.instance.ItemJsonData[list[i]["ID"].I.ToString()]["quality"].I == (int)danFangPingJie)
			{
				if (dictionary.ContainsKey(list[i]["ID"].I))
				{
					dictionary[list[i]["ID"].I].Add(list[i]);
					continue;
				}
				List<JSONObject> list2 = new List<JSONObject>();
				list2.Add(list[i]);
				dictionary.Add(list[i]["ID"].I, list2);
			}
		}
		return dictionary;
	}

	public void clickCallBack()
	{
		LayoutRebuilder.ForceRebuildLayoutImmediate(rectTransform);
	}

	public bool checkCanLianZhi(List<JSONObject> childs)
	{
		if (childs.Count == 0)
		{
			return false;
		}
		bool flag = false;
		for (int i = 0; i < childs.Count; i++)
		{
			flag = checkCanLianZhi(childs[i]);
			if (flag)
			{
				return flag;
			}
		}
		return false;
	}

	public bool checkCanLianZhi(JSONObject child)
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

	public void updateState()
	{
		for (int i = 0; i < danFangParentCells.Count; i++)
		{
			danFangParentCells[i].updateState();
		}
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

	public void addDanFang(JSONObject obj)
	{
		int index = -1;
		IsSame(obj, ref index);
		if (index != -1)
		{
			return;
		}
		Tools.instance.getPlayer().DanFang.list.Add(obj);
		int i = obj["ID"].I;
		bool flag = false;
		for (int j = 0; j < danFangParentCells.Count; j++)
		{
			if (danFangParentCells[j].DanFangID == i)
			{
				flag = true;
				danFangParentCells[j].addChild(obj);
				return;
			}
		}
		if (!flag)
		{
			DanFangParentCell component = Tools.InstantiateGameObject(parentDanFang, parentDanFang.transform.parent).GetComponent<DanFangParentCell>();
			component.DanFangID = i;
			List<JSONObject> list = new List<JSONObject>();
			list.Add(obj);
			component.childs = list;
			component.init();
			danFangParentCells.Add(component);
		}
	}

	private void deleteDanFang()
	{
		//IL_00b3: Unknown result type (might be due to invalid IL or missing references)
		if ((Object)(object)curSelectDanFanParent == (Object)null || curSelectDanFanParent.DanFangID < 1)
		{
			UIPopTip.Inst.Pop("请选择丹方");
			return;
		}
		LianDanSystemManager.inst.openMask();
		selectBox.instence.LianDanChoice("确定要删除" + Tools.Code64(jsonData.instance.ItemJsonData[curSelectDanFanParent.DanFangID.ToString()]["name"].str) + "丹方吗", new EventDelegate(delegate
		{
			int index = -1;
			if (curSelectJSONObject != null)
			{
				IsSame(curSelectJSONObject, ref index);
				if (index != -1)
				{
					Tools.instance.getPlayer().DanFang.list.RemoveAt(index);
					DanFangParentCell danFangParentCell = curSelectDanFanParent;
					_ = danFangParentCell.childs.Count;
					danFangParentCell.childs.Remove(curSelectJSONObject);
					danFangParentCell.childDanFangChildCellList.Remove(curSelectDanFang.GetComponent<DanFangChildCell>());
					if (danFangParentCell.childs.Count <= 0)
					{
						danFangParentCells.Remove(danFangParentCell);
						Object.Destroy((Object)(object)((Component)danFangParentCell).gameObject);
						curSelectDanFanParent = null;
					}
					else
					{
						danFangParentCell.finallyIndex--;
						danFangParentCell.childDanFangChildCellList[danFangParentCell.finallyIndex].hideLine();
						Object.Destroy((Object)(object)curSelectDanFang);
						if (danFangParentCell.isShow)
						{
							danFangParentCell.updateSelfHeight();
							LianDanSystemManager.inst.DanFangPageManager.clickCallBack();
						}
						danFangParentCell.updateState();
					}
					curSelectDanFang = null;
					curSelectDanFangBg = null;
					curSelectJSONObject = null;
				}
			}
			else
			{
				UIPopTip.Inst.Pop("数据异常,删除失败");
			}
			LianDanSystemManager.inst.closeMask();
		}), new EventDelegate(LianDanSystemManager.inst.closeMask), new Vector3(0.8f, 0.8f, 0.8f));
	}

	private void topDanFang()
	{
		curSelectDanFang.transform.SetSiblingIndex(0);
	}

	public void setPingJie(DanFangPingJie pingJie)
	{
		danFangPingJie = pingJie;
		updateDanFang();
	}
}
