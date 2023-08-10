using System.Collections.Generic;
using KBEngine;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class DanFangParentCell : MonoBehaviour
{
	public bool isShow;

	[HideInInspector]
	public int DanFangID = -1;

	[HideInInspector]
	public List<JSONObject> childs = new List<JSONObject>();

	[SerializeField]
	private Text danFangNameText;

	[SerializeField]
	private GameObject DanFangChildCell;

	public List<DanFangChildCell> childDanFangChildCellList;

	[SerializeField]
	private Image bgImage;

	[SerializeField]
	private RectTransform content;

	private Vector2 startSizeDelta;

	[SerializeField]
	private GameObject CanLianZhiImage;

	[SerializeField]
	private Image CanLianZhiBgImage;

	[SerializeField]
	private Button btnDanFang;

	public int finallyIndex = -1;

	public void init()
	{
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		//IL_0125: Unknown result type (might be due to invalid IL or missing references)
		//IL_012f: Expected O, but got Unknown
		startSizeDelta = ((Component)((Component)this).transform).GetComponent<RectTransform>().sizeDelta;
		if (childs.Count > 0 && DanFangID != -1)
		{
			danFangNameText.text = Tools.Code64(jsonData.instance.ItemJsonData[DanFangID.ToString()]["name"].str);
			childDanFangChildCellList = new List<DanFangChildCell>();
			finallyIndex = -1;
			Tools.ClearObj(DanFangChildCell.transform);
			for (int i = 0; i < childs.Count; i++)
			{
				DanFangChildCell component = Tools.InstantiateGameObject(DanFangChildCell, DanFangChildCell.transform.parent).GetComponent<DanFangChildCell>();
				component.danFang = childs[i];
				component.init();
				childDanFangChildCellList.Add(component);
				finallyIndex++;
			}
			childDanFangChildCellList[finallyIndex].hideLine();
			updateState();
			((UnityEvent)btnDanFang.onClick).AddListener(new UnityAction(clickDanFang));
		}
	}

	public void addChild(JSONObject obj)
	{
		DanFangChildCell component = Tools.InstantiateGameObject(DanFangChildCell, DanFangChildCell.transform.parent).GetComponent<DanFangChildCell>();
		childs.Add(obj);
		component.danFang = childs[childs.Count - 1];
		component.init();
		childDanFangChildCellList.Add(component);
		childDanFangChildCellList[finallyIndex].showLine();
		finallyIndex++;
		childDanFangChildCellList[finallyIndex].hideLine();
		updateState();
		if (isShow)
		{
			updateSelfHeight();
			LianDanSystemManager.inst.DanFangPageManager.clickCallBack();
		}
	}

	public void updateSelfHeight()
	{
		//IL_0032: Unknown result type (might be due to invalid IL or missing references)
		//IL_003d: Unknown result type (might be due to invalid IL or missing references)
		LayoutRebuilder.ForceRebuildLayoutImmediate(content);
		((Component)((Component)this).transform).GetComponent<RectTransform>().sizeDelta = new Vector2(startSizeDelta.x, startSizeDelta.y + content.sizeDelta.y);
		LayoutRebuilder.ForceRebuildLayoutImmediate(((Component)((Component)this).transform).GetComponent<RectTransform>());
	}

	public void clickDanFang()
	{
		//IL_00af: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ee: Unknown result type (might be due to invalid IL or missing references)
		//IL_0063: Unknown result type (might be due to invalid IL or missing references)
		isShow = !isShow;
		if (isShow)
		{
			((Component)content).gameObject.SetActive(true);
			updateSelfHeight();
			bgImage.sprite = LianDanSystemManager.inst.DanFangPageManager.sprites[1];
			((Graphic)danFangNameText).color = new Color(0.94509804f, 73f / 85f, 0.6627451f);
			CanLianZhiBgImage.sprite = LianDanSystemManager.inst.DanFangPageManager.sprites[3];
		}
		else
		{
			((Component)content).gameObject.SetActive(false);
			((Component)((Component)this).transform).GetComponent<RectTransform>().sizeDelta = startSizeDelta;
			bgImage.sprite = LianDanSystemManager.inst.DanFangPageManager.sprites[0];
			((Graphic)danFangNameText).color = new Color(0.70980394f, 0.94509804f, 78f / 85f);
			CanLianZhiBgImage.sprite = LianDanSystemManager.inst.DanFangPageManager.sprites[2];
		}
		LianDanSystemManager.inst.DanFangPageManager.clickCallBack();
	}

	private bool checkCanLianZhi(List<JSONObject> childs)
	{
		if (childs.Count == 0)
		{
			return false;
		}
		for (int i = 0; i < childs.Count; i++)
		{
			JSONObject jSONObject = childs[i]["Type"];
			JSONObject jSONObject2 = childs[i]["Num"];
			int num = 0;
			while (num < jSONObject.Count)
			{
				if (jSONObject[num].I > 0)
				{
					bool flag = false;
					foreach (ITEM_INFO value in Tools.instance.getPlayer().itemList.values)
					{
						if (jSONObject[num].I == value.itemId && jSONObject2[num].I <= value.itemCount)
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

	public void updateState()
	{
		if (LianDanSystemManager.inst.DanFangPageManager.checkCanLianZhi(childs))
		{
			CanLianZhiImage.SetActive(true);
		}
		else
		{
			CanLianZhiImage.SetActive(false);
		}
		for (int i = 0; i < childDanFangChildCellList.Count; i++)
		{
			childDanFangChildCellList[i].updateState();
		}
	}
}
