using System;
using System.Collections.Generic;
using KBEngine;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

// Token: 0x020002E2 RID: 738
public class DanFangPageManager : MonoBehaviour
{
	// Token: 0x060019A1 RID: 6561 RVA: 0x000B6E57 File Offset: 0x000B5057
	public void init()
	{
		this.updateDanFang();
		this.deleteButton.onClick.AddListener(new UnityAction(this.deleteDanFang));
		this.topButton.onClick.AddListener(new UnityAction(this.topDanFang));
	}

	// Token: 0x060019A2 RID: 6562 RVA: 0x000B6E98 File Offset: 0x000B5098
	public void updateDanFang()
	{
		Tools.ClearObj(this.parentDanFang.transform);
		this.danFangParentCells = new List<DanFangParentCell>();
		Dictionary<int, List<JSONObject>> noSameDanFangList = this.getNoSameDanFangList();
		if (noSameDanFangList.Keys.Count > 0)
		{
			foreach (int num in noSameDanFangList.Keys)
			{
				DanFangParentCell component = Tools.InstantiateGameObject(this.parentDanFang, this.parentDanFang.transform.parent).GetComponent<DanFangParentCell>();
				component.DanFangID = num;
				component.childs = noSameDanFangList[num];
				component.init();
				this.danFangParentCells.Add(component);
			}
		}
	}

	// Token: 0x060019A3 RID: 6563 RVA: 0x000B6F5C File Offset: 0x000B515C
	private Dictionary<int, List<JSONObject>> getNoSameDanFangList()
	{
		Dictionary<int, List<JSONObject>> dictionary = new Dictionary<int, List<JSONObject>>();
		List<JSONObject> list = Tools.instance.getPlayer().DanFang.list;
		for (int i = 0; i < list.Count; i++)
		{
			if (this.danFangPingJie == DanFangPageManager.DanFangPingJie.所有 || jsonData.instance.ItemJsonData[list[i]["ID"].I.ToString()]["quality"].I == (int)this.danFangPingJie)
			{
				if (dictionary.ContainsKey(list[i]["ID"].I))
				{
					dictionary[list[i]["ID"].I].Add(list[i]);
				}
				else
				{
					List<JSONObject> list2 = new List<JSONObject>();
					list2.Add(list[i]);
					dictionary.Add(list[i]["ID"].I, list2);
				}
			}
		}
		return dictionary;
	}

	// Token: 0x060019A4 RID: 6564 RVA: 0x000B7063 File Offset: 0x000B5263
	public void clickCallBack()
	{
		LayoutRebuilder.ForceRebuildLayoutImmediate(this.rectTransform);
	}

	// Token: 0x060019A5 RID: 6565 RVA: 0x000B7070 File Offset: 0x000B5270
	public bool checkCanLianZhi(List<JSONObject> childs)
	{
		if (childs.Count == 0)
		{
			return false;
		}
		for (int i = 0; i < childs.Count; i++)
		{
			bool flag = this.checkCanLianZhi(childs[i]);
			if (flag)
			{
				return flag;
			}
		}
		return false;
	}

	// Token: 0x060019A6 RID: 6566 RVA: 0x000B70B0 File Offset: 0x000B52B0
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
				Dictionary<int, int> dictionary2 = dictionary;
				int key = list[i];
				dictionary2[key] += list2[i];
			}
		}
		foreach (int num in dictionary.Keys)
		{
			if (dictionary[num] > 0)
			{
				bool flag = false;
				foreach (ITEM_INFO item_INFO in Tools.instance.getPlayer().itemList.values)
				{
					if (num == item_INFO.itemId && (long)dictionary[num] <= (long)((ulong)item_INFO.itemCount))
					{
						flag = true;
					}
				}
				if (!flag)
				{
					return false;
				}
			}
		}
		return true;
	}

	// Token: 0x060019A7 RID: 6567 RVA: 0x000B7214 File Offset: 0x000B5414
	public void updateState()
	{
		for (int i = 0; i < this.danFangParentCells.Count; i++)
		{
			this.danFangParentCells[i].updateState();
		}
	}

	// Token: 0x060019A8 RID: 6568 RVA: 0x000B7248 File Offset: 0x000B5448
	public void IsSame(JSONObject obj, ref int index)
	{
		List<JSONObject> list = Tools.instance.getPlayer().DanFang.list;
		JSONObject jsonobject = obj["Type"];
		JSONObject jsonobject2 = obj["Num"];
		for (int i = 0; i < list.Count; i++)
		{
			if (list[i]["ID"].I == obj["ID"].I)
			{
				int num = 0;
				for (int j = 0; j < jsonobject.Count; j++)
				{
					if (list[i]["Type"][j].I == jsonobject[j].I && list[i]["Num"][j].I == jsonobject2[j].I)
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
		}
		index = -1;
	}

	// Token: 0x060019A9 RID: 6569 RVA: 0x000B7344 File Offset: 0x000B5544
	public void addDanFang(JSONObject obj)
	{
		int num = -1;
		this.IsSame(obj, ref num);
		if (num == -1)
		{
			Tools.instance.getPlayer().DanFang.list.Add(obj);
			int i = obj["ID"].I;
			bool flag = false;
			for (int j = 0; j < this.danFangParentCells.Count; j++)
			{
				if (this.danFangParentCells[j].DanFangID == i)
				{
					this.danFangParentCells[j].addChild(obj);
					return;
				}
			}
			if (!flag)
			{
				DanFangParentCell component = Tools.InstantiateGameObject(this.parentDanFang, this.parentDanFang.transform.parent).GetComponent<DanFangParentCell>();
				component.DanFangID = i;
				component.childs = new List<JSONObject>
				{
					obj
				};
				component.init();
				this.danFangParentCells.Add(component);
			}
		}
	}

	// Token: 0x060019AA RID: 6570 RVA: 0x000B7428 File Offset: 0x000B5628
	private void deleteDanFang()
	{
		if (this.curSelectDanFanParent == null || this.curSelectDanFanParent.DanFangID < 1)
		{
			UIPopTip.Inst.Pop("请选择丹方", PopTipIconType.叹号);
			return;
		}
		LianDanSystemManager.inst.openMask();
		selectBox.instence.LianDanChoice("确定要删除" + Tools.Code64(jsonData.instance.ItemJsonData[this.curSelectDanFanParent.DanFangID.ToString()]["name"].str) + "丹方吗", new EventDelegate(delegate()
		{
			int num = -1;
			if (this.curSelectJSONObject != null)
			{
				this.IsSame(this.curSelectJSONObject, ref num);
				if (num != -1)
				{
					Tools.instance.getPlayer().DanFang.list.RemoveAt(num);
					DanFangParentCell danFangParentCell = this.curSelectDanFanParent;
					int count = danFangParentCell.childs.Count;
					danFangParentCell.childs.Remove(this.curSelectJSONObject);
					danFangParentCell.childDanFangChildCellList.Remove(this.curSelectDanFang.GetComponent<DanFangChildCell>());
					if (danFangParentCell.childs.Count <= 0)
					{
						this.danFangParentCells.Remove(danFangParentCell);
						Object.Destroy(danFangParentCell.gameObject);
						this.curSelectDanFanParent = null;
					}
					else
					{
						danFangParentCell.finallyIndex--;
						danFangParentCell.childDanFangChildCellList[danFangParentCell.finallyIndex].hideLine();
						Object.Destroy(this.curSelectDanFang);
						if (danFangParentCell.isShow)
						{
							danFangParentCell.updateSelfHeight();
							LianDanSystemManager.inst.DanFangPageManager.clickCallBack();
						}
						danFangParentCell.updateState();
					}
					this.curSelectDanFang = null;
					this.curSelectDanFangBg = null;
					this.curSelectJSONObject = null;
				}
			}
			else
			{
				UIPopTip.Inst.Pop("数据异常,删除失败", PopTipIconType.叹号);
			}
			LianDanSystemManager.inst.closeMask();
		}), new EventDelegate(new EventDelegate.Callback(LianDanSystemManager.inst.closeMask)), new Vector3(0.8f, 0.8f, 0.8f));
	}

	// Token: 0x060019AB RID: 6571 RVA: 0x000B74F2 File Offset: 0x000B56F2
	private void topDanFang()
	{
		this.curSelectDanFang.transform.SetSiblingIndex(0);
	}

	// Token: 0x060019AC RID: 6572 RVA: 0x000B7505 File Offset: 0x000B5705
	public void setPingJie(DanFangPageManager.DanFangPingJie pingJie)
	{
		this.danFangPingJie = pingJie;
		this.updateDanFang();
	}

	// Token: 0x040014C7 RID: 5319
	[SerializeField]
	private GameObject parentDanFang;

	// Token: 0x040014C8 RID: 5320
	private DanFangPageManager.DanFangPingJie danFangPingJie;

	// Token: 0x040014C9 RID: 5321
	[SerializeField]
	public List<Sprite> sprites = new List<Sprite>();

	// Token: 0x040014CA RID: 5322
	[HideInInspector]
	public List<DanFangParentCell> danFangParentCells;

	// Token: 0x040014CB RID: 5323
	[SerializeField]
	private RectTransform rectTransform;

	// Token: 0x040014CC RID: 5324
	[HideInInspector]
	public GameObject curSelectDanFang;

	// Token: 0x040014CD RID: 5325
	[HideInInspector]
	public GameObject curSelectDanFangBg;

	// Token: 0x040014CE RID: 5326
	[HideInInspector]
	public JSONObject curSelectJSONObject;

	// Token: 0x040014CF RID: 5327
	[HideInInspector]
	public DanFangParentCell curSelectDanFanParent;

	// Token: 0x040014D0 RID: 5328
	[SerializeField]
	private Button deleteButton;

	// Token: 0x040014D1 RID: 5329
	[SerializeField]
	private Button topButton;

	// Token: 0x040014D2 RID: 5330
	[SerializeField]
	public List<Sprite> pingJieSprites = new List<Sprite>();

	// Token: 0x02001329 RID: 4905
	public enum DanFangPingJie
	{
		// Token: 0x040067A6 RID: 26534
		所有,
		// Token: 0x040067A7 RID: 26535
		一品,
		// Token: 0x040067A8 RID: 26536
		二品,
		// Token: 0x040067A9 RID: 26537
		三品,
		// Token: 0x040067AA RID: 26538
		四品,
		// Token: 0x040067AB RID: 26539
		五品,
		// Token: 0x040067AC RID: 26540
		六品
	}
}
