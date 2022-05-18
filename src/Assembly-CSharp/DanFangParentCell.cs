using System;
using System.Collections.Generic;
using KBEngine;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

// Token: 0x02000442 RID: 1090
public class DanFangParentCell : MonoBehaviour
{
	// Token: 0x06001D00 RID: 7424 RVA: 0x000FFC8C File Offset: 0x000FDE8C
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

	// Token: 0x06001D01 RID: 7425 RVA: 0x000FFDC8 File Offset: 0x000FDFC8
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

	// Token: 0x06001D02 RID: 7426 RVA: 0x000FFE90 File Offset: 0x000FE090
	public void updateSelfHeight()
	{
		LayoutRebuilder.ForceRebuildLayoutImmediate(this.content);
		base.transform.GetComponent<RectTransform>().sizeDelta = new Vector2(this.startSizeDelta.x, this.startSizeDelta.y + this.content.sizeDelta.y);
		LayoutRebuilder.ForceRebuildLayoutImmediate(base.transform.GetComponent<RectTransform>());
	}

	// Token: 0x06001D03 RID: 7427 RVA: 0x000FFEF4 File Offset: 0x000FE0F4
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

	// Token: 0x06001D04 RID: 7428 RVA: 0x00100028 File Offset: 0x000FE228
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

	// Token: 0x06001D05 RID: 7429 RVA: 0x00100128 File Offset: 0x000FE328
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

	// Token: 0x04001900 RID: 6400
	public bool isShow;

	// Token: 0x04001901 RID: 6401
	[HideInInspector]
	public int DanFangID = -1;

	// Token: 0x04001902 RID: 6402
	[HideInInspector]
	public List<JSONObject> childs = new List<JSONObject>();

	// Token: 0x04001903 RID: 6403
	[SerializeField]
	private Text danFangNameText;

	// Token: 0x04001904 RID: 6404
	[SerializeField]
	private GameObject DanFangChildCell;

	// Token: 0x04001905 RID: 6405
	public List<DanFangChildCell> childDanFangChildCellList;

	// Token: 0x04001906 RID: 6406
	[SerializeField]
	private Image bgImage;

	// Token: 0x04001907 RID: 6407
	[SerializeField]
	private RectTransform content;

	// Token: 0x04001908 RID: 6408
	private Vector2 startSizeDelta;

	// Token: 0x04001909 RID: 6409
	[SerializeField]
	private GameObject CanLianZhiImage;

	// Token: 0x0400190A RID: 6410
	[SerializeField]
	private Image CanLianZhiBgImage;

	// Token: 0x0400190B RID: 6411
	[SerializeField]
	private Button btnDanFang;

	// Token: 0x0400190C RID: 6412
	public int finallyIndex = -1;
}
