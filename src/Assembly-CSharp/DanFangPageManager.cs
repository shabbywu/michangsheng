using System;
using System.Collections.Generic;
using KBEngine;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

// Token: 0x02000435 RID: 1077
public class DanFangPageManager : MonoBehaviour
{
	// Token: 0x06001CBA RID: 7354 RVA: 0x000180F6 File Offset: 0x000162F6
	public void init()
	{
		this.updateDanFang();
		this.deleteButton.onClick.AddListener(new UnityAction(this.deleteDanFang));
		this.topButton.onClick.AddListener(new UnityAction(this.topDanFang));
	}

	// Token: 0x06001CBB RID: 7355 RVA: 0x000FCAE8 File Offset: 0x000FACE8
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

	// Token: 0x06001CBC RID: 7356 RVA: 0x000FCBAC File Offset: 0x000FADAC
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

	// Token: 0x06001CBD RID: 7357 RVA: 0x00018136 File Offset: 0x00016336
	public void clickCallBack()
	{
		LayoutRebuilder.ForceRebuildLayoutImmediate(this.rectTransform);
	}

	// Token: 0x06001CBE RID: 7358 RVA: 0x000FCCB4 File Offset: 0x000FAEB4
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

	// Token: 0x06001CBF RID: 7359 RVA: 0x000FCCF4 File Offset: 0x000FAEF4
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

	// Token: 0x06001CC0 RID: 7360 RVA: 0x000FCE58 File Offset: 0x000FB058
	public void updateState()
	{
		for (int i = 0; i < this.danFangParentCells.Count; i++)
		{
			this.danFangParentCells[i].updateState();
		}
	}

	// Token: 0x06001CC1 RID: 7361 RVA: 0x000FCE8C File Offset: 0x000FB08C
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

	// Token: 0x06001CC2 RID: 7362 RVA: 0x000FCF88 File Offset: 0x000FB188
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

	// Token: 0x06001CC3 RID: 7363 RVA: 0x000FD06C File Offset: 0x000FB26C
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

	// Token: 0x06001CC4 RID: 7364 RVA: 0x00018143 File Offset: 0x00016343
	private void topDanFang()
	{
		this.curSelectDanFang.transform.SetSiblingIndex(0);
	}

	// Token: 0x06001CC5 RID: 7365 RVA: 0x00018156 File Offset: 0x00016356
	public void setPingJie(DanFangPageManager.DanFangPingJie pingJie)
	{
		this.danFangPingJie = pingJie;
		this.updateDanFang();
	}

	// Token: 0x040018AA RID: 6314
	[SerializeField]
	private GameObject parentDanFang;

	// Token: 0x040018AB RID: 6315
	private DanFangPageManager.DanFangPingJie danFangPingJie;

	// Token: 0x040018AC RID: 6316
	[SerializeField]
	public List<Sprite> sprites = new List<Sprite>();

	// Token: 0x040018AD RID: 6317
	[HideInInspector]
	public List<DanFangParentCell> danFangParentCells;

	// Token: 0x040018AE RID: 6318
	[SerializeField]
	private RectTransform rectTransform;

	// Token: 0x040018AF RID: 6319
	[HideInInspector]
	public GameObject curSelectDanFang;

	// Token: 0x040018B0 RID: 6320
	[HideInInspector]
	public GameObject curSelectDanFangBg;

	// Token: 0x040018B1 RID: 6321
	[HideInInspector]
	public JSONObject curSelectJSONObject;

	// Token: 0x040018B2 RID: 6322
	[HideInInspector]
	public DanFangParentCell curSelectDanFanParent;

	// Token: 0x040018B3 RID: 6323
	[SerializeField]
	private Button deleteButton;

	// Token: 0x040018B4 RID: 6324
	[SerializeField]
	private Button topButton;

	// Token: 0x040018B5 RID: 6325
	[SerializeField]
	public List<Sprite> pingJieSprites = new List<Sprite>();

	// Token: 0x02000436 RID: 1078
	public enum DanFangPingJie
	{
		// Token: 0x040018B7 RID: 6327
		所有,
		// Token: 0x040018B8 RID: 6328
		一品,
		// Token: 0x040018B9 RID: 6329
		二品,
		// Token: 0x040018BA RID: 6330
		三品,
		// Token: 0x040018BB RID: 6331
		四品,
		// Token: 0x040018BC RID: 6332
		五品,
		// Token: 0x040018BD RID: 6333
		六品
	}
}
