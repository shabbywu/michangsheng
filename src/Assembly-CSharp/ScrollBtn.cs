using System;
using System.Text.RegularExpressions;
using UltimateSurvival;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020003F8 RID: 1016
public abstract class ScrollBtn : MonoBehaviour
{
	// Token: 0x060020C3 RID: 8387 RVA: 0x00004095 File Offset: 0x00002295
	private void Start()
	{
	}

	// Token: 0x060020C4 RID: 8388
	protected abstract GameObject getItemUI();

	// Token: 0x060020C5 RID: 8389 RVA: 0x000E67D0 File Offset: 0x000E49D0
	protected Transform findItemWindow()
	{
		GameObject itemUI = this.getItemUI();
		itemUI.SetActive(true);
		Tooltip component = itemUI.GetComponent<Tooltip>();
		component.item.itemID = this.ItemID;
		component.item.itemUUID = this.ietmUUID;
		return itemUI.transform.Find("Window");
	}

	// Token: 0x060020C6 RID: 8390 RVA: 0x000E6820 File Offset: 0x000E4A20
	protected virtual ItemData getItemData()
	{
		ItemData result;
		UI_HOMESCENE.instense.database.FindItemById(this.ItemID, out result);
		return result;
	}

	// Token: 0x060020C7 RID: 8391 RVA: 0x000E6846 File Offset: 0x000E4A46
	protected virtual void setImageIcon(Transform window, ItemData itemData)
	{
		window.Find("Icon").GetComponent<Image>().sprite = itemData.Icon;
	}

	// Token: 0x060020C8 RID: 8392 RVA: 0x000E6863 File Offset: 0x000E4A63
	protected virtual void setNameText(Transform window, ItemData itemData)
	{
		window.Find("Name").GetComponent<Text>().text = itemData.Name;
	}

	// Token: 0x060020C9 RID: 8393 RVA: 0x000E6880 File Offset: 0x000E4A80
	protected virtual void settipDescText(Transform window, ItemData itemData)
	{
		Text component = window.Find("Main Description").GetComponent<Text>();
		string text = Regex.Unescape(itemData.Descriptions[0]);
		component.text = text;
	}

	// Token: 0x060020CA RID: 8394 RVA: 0x000E68B1 File Offset: 0x000E4AB1
	protected virtual void settooltipAttrText(Transform window, ItemData itemData)
	{
		window.Find("Secondary Description").GetComponent<Text>().text = "";
	}

	// Token: 0x04001AA6 RID: 6822
	public int ItemID;

	// Token: 0x04001AA7 RID: 6823
	public ulong ietmUUID;

	// Token: 0x04001AA8 RID: 6824
	public GameObject ItemUI;
}
