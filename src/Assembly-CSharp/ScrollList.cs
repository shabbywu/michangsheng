using System;
using System.Collections.Generic;
using KBEngine;
using UltimateSurvival;
using UnityEngine;
using UnityEngine.UI;

public class ScrollList : MonoBehaviour
{
	public GameObject ItemTemple;

	public ItemDatabase database;

	public GameObject UIList;

	private void Start()
	{
	}

	public GameObject createBtn<Templet>(ITEM_INFO info) where Templet : ScrollBtn
	{
		GameObject obj = Object.Instantiate<GameObject>(ItemTemple.gameObject);
		Templet component = obj.GetComponent<Templet>();
		obj.SetActive(true);
		component.ItemID = info.itemId;
		component.ietmUUID = info.UUID;
		return obj;
	}

	public virtual void setItemImage(GameObject _Button, ITEM_INFO info)
	{
		//IL_004e: Unknown result type (might be due to invalid IL or missing references)
		database.FindItemById(info.itemId, out var itemData);
		Image component = ((Component)_Button.transform.Find("Image")).GetComponent<Image>();
		component.overrideSprite = itemData.Icon;
		((Component)component).gameObject.transform.localScale = new Vector3(0.8f, 0.8f, 0.8f);
	}

	public virtual void setItemCount(GameObject _Button, ITEM_INFO info)
	{
		int num = Convert.ToInt32(info.itemCount);
		Text component = ((Component)_Button.transform.Find("Text")).GetComponent<Text>();
		if (num > 1)
		{
			component.text = "x" + num;
		}
		else
		{
			component.text = "";
		}
	}

	public void setList<T>(ITEM_INFO_LIST infos) where T : ScrollBtn
	{
		clenrNowBtn();
		List<ITEM_INFO> values = infos.values;
		for (int i = 0; i < values.Count; i++)
		{
			ITEM_INFO info = values[i];
			GameObject val = createBtn<T>(info);
			setItemImage(val, info);
			setItemCount(val, info);
			setItemButton(val, i);
			val.transform.parent = UIList.transform;
		}
	}

	public virtual void setItemButton(GameObject button, int itemIndex)
	{
	}

	public virtual void clenrNowBtn()
	{
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		//IL_001f: Expected O, but got Unknown
		foreach (Transform item in ((Component)this).gameObject.transform)
		{
			Transform val = item;
			if (((Component)val).gameObject.active)
			{
				Object.Destroy((Object)(object)((Component)val).gameObject);
			}
		}
	}
}
