using System;
using System.Collections.Generic;
using KBEngine;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

// Token: 0x020002E8 RID: 744
public class DanFangParentCell : MonoBehaviour
{
	// Token: 0x060019DE RID: 6622 RVA: 0x000B93C4 File Offset: 0x000B75C4
	public void init()
	{
		this.startSizeDelta = base.transform.GetComponent<RectTransform>().sizeDelta;
		if (this.childs.Count > 0 && this.DanFangID != -1)
		{
			this.danFangNameText.text = Tools.Code64(jsonData.instance.ItemJsonData[this.DanFangID.ToString()]["name"].str);
			this.childDanFangChildCellList = new List<DanFangChildCell>();
			this.finallyIndex = -1;
			Tools.ClearObj(this.DanFangChildCell.transform);
			for (int i = 0; i < this.childs.Count; i++)
			{
				DanFangChildCell component = Tools.InstantiateGameObject(this.DanFangChildCell, this.DanFangChildCell.transform.parent).GetComponent<DanFangChildCell>();
				component.danFang = this.childs[i];
				component.init();
				this.childDanFangChildCellList.Add(component);
				this.finallyIndex++;
			}
			this.childDanFangChildCellList[this.finallyIndex].hideLine();
			this.updateState();
			this.btnDanFang.onClick.AddListener(new UnityAction(this.clickDanFang));
		}
	}

	// Token: 0x060019DF RID: 6623 RVA: 0x000B9500 File Offset: 0x000B7700
	public void addChild(JSONObject obj)
	{
		DanFangChildCell component = Tools.InstantiateGameObject(this.DanFangChildCell, this.DanFangChildCell.transform.parent).GetComponent<DanFangChildCell>();
		this.childs.Add(obj);
		component.danFang = this.childs[this.childs.Count - 1];
		component.init();
		this.childDanFangChildCellList.Add(component);
		this.childDanFangChildCellList[this.finallyIndex].showLine();
		this.finallyIndex++;
		this.childDanFangChildCellList[this.finallyIndex].hideLine();
		this.updateState();
		if (this.isShow)
		{
			this.updateSelfHeight();
			LianDanSystemManager.inst.DanFangPageManager.clickCallBack();
		}
	}

	// Token: 0x060019E0 RID: 6624 RVA: 0x000B95C8 File Offset: 0x000B77C8
	public void updateSelfHeight()
	{
		LayoutRebuilder.ForceRebuildLayoutImmediate(this.content);
		base.transform.GetComponent<RectTransform>().sizeDelta = new Vector2(this.startSizeDelta.x, this.startSizeDelta.y + this.content.sizeDelta.y);
		LayoutRebuilder.ForceRebuildLayoutImmediate(base.transform.GetComponent<RectTransform>());
	}

	// Token: 0x060019E1 RID: 6625 RVA: 0x000B962C File Offset: 0x000B782C
	public void clickDanFang()
	{
		this.isShow = !this.isShow;
		if (this.isShow)
		{
			this.content.gameObject.SetActive(true);
			this.updateSelfHeight();
			this.bgImage.sprite = LianDanSystemManager.inst.DanFangPageManager.sprites[1];
			this.danFangNameText.color = new Color(0.94509804f, 0.85882354f, 0.6627451f);
			this.CanLianZhiBgImage.sprite = LianDanSystemManager.inst.DanFangPageManager.sprites[3];
		}
		else
		{
			this.content.gameObject.SetActive(false);
			base.transform.GetComponent<RectTransform>().sizeDelta = this.startSizeDelta;
			this.bgImage.sprite = LianDanSystemManager.inst.DanFangPageManager.sprites[0];
			this.danFangNameText.color = new Color(0.70980394f, 0.94509804f, 0.91764706f);
			this.CanLianZhiBgImage.sprite = LianDanSystemManager.inst.DanFangPageManager.sprites[2];
		}
		LianDanSystemManager.inst.DanFangPageManager.clickCallBack();
	}

	// Token: 0x060019E2 RID: 6626 RVA: 0x000B9760 File Offset: 0x000B7960
	private bool checkCanLianZhi(List<JSONObject> childs)
	{
		if (childs.Count == 0)
		{
			return false;
		}
		for (int i = 0; i < childs.Count; i++)
		{
			JSONObject jsonobject = childs[i]["Type"];
			JSONObject jsonobject2 = childs[i]["Num"];
			int j = 0;
			while (j < jsonobject.Count)
			{
				if (jsonobject[j].I > 0)
				{
					bool flag = false;
					foreach (ITEM_INFO item_INFO in Tools.instance.getPlayer().itemList.values)
					{
						if (jsonobject[j].I == item_INFO.itemId && (long)jsonobject2[j].I <= (long)((ulong)item_INFO.itemCount))
						{
							flag = true;
						}
					}
					if (!flag)
					{
						return false;
					}
				}
				i++;
			}
		}
		return true;
	}

	// Token: 0x060019E3 RID: 6627 RVA: 0x000B9860 File Offset: 0x000B7A60
	public void updateState()
	{
		if (LianDanSystemManager.inst.DanFangPageManager.checkCanLianZhi(this.childs))
		{
			this.CanLianZhiImage.SetActive(true);
		}
		else
		{
			this.CanLianZhiImage.SetActive(false);
		}
		for (int i = 0; i < this.childDanFangChildCellList.Count; i++)
		{
			this.childDanFangChildCellList[i].updateState();
		}
	}

	// Token: 0x040014FC RID: 5372
	public bool isShow;

	// Token: 0x040014FD RID: 5373
	[HideInInspector]
	public int DanFangID = -1;

	// Token: 0x040014FE RID: 5374
	[HideInInspector]
	public List<JSONObject> childs = new List<JSONObject>();

	// Token: 0x040014FF RID: 5375
	[SerializeField]
	private Text danFangNameText;

	// Token: 0x04001500 RID: 5376
	[SerializeField]
	private GameObject DanFangChildCell;

	// Token: 0x04001501 RID: 5377
	public List<DanFangChildCell> childDanFangChildCellList;

	// Token: 0x04001502 RID: 5378
	[SerializeField]
	private Image bgImage;

	// Token: 0x04001503 RID: 5379
	[SerializeField]
	private RectTransform content;

	// Token: 0x04001504 RID: 5380
	private Vector2 startSizeDelta;

	// Token: 0x04001505 RID: 5381
	[SerializeField]
	private GameObject CanLianZhiImage;

	// Token: 0x04001506 RID: 5382
	[SerializeField]
	private Image CanLianZhiBgImage;

	// Token: 0x04001507 RID: 5383
	[SerializeField]
	private Button btnDanFang;

	// Token: 0x04001508 RID: 5384
	public int finallyIndex = -1;
}
