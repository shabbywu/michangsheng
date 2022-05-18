using System;
using System.Text.RegularExpressions;
using UltimateSurvival;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020005A8 RID: 1448
public abstract class ScrollBtn : MonoBehaviour
{
	// Token: 0x06002475 RID: 9333 RVA: 0x000042DD File Offset: 0x000024DD
	private void Start()
	{
	}

	// Token: 0x06002476 RID: 9334
	protected abstract GameObject getItemUI();

	// Token: 0x06002477 RID: 9335 RVA: 0x001288E4 File Offset: 0x00126AE4
	protected Transform findItemWindow()
	{
		GameObject itemUI = this.getItemUI();
		itemUI.SetActive(true);
		Tooltip component = itemUI.GetComponent<Tooltip>();
		component.item.itemID = this.ItemID;
		component.item.itemUUID = this.ietmUUID;
		return itemUI.transform.Find("Window");
	}

	// Token: 0x06002478 RID: 9336 RVA: 0x00128934 File Offset: 0x00126B34
	protected virtual ItemData getItemData()
	{
		ItemData result;
		UI_HOMESCENE.instense.database.FindItemById(this.ItemID, out result);
		return result;
	}

	// Token: 0x06002479 RID: 9337 RVA: 0x0001D57D File Offset: 0x0001B77D
	protected virtual void setImageIcon(Transform window, ItemData itemData)
	{
		window.Find("Icon").GetComponent<Image>().sprite = itemData.Icon;
	}

	// Token: 0x0600247A RID: 9338 RVA: 0x0001D59A File Offset: 0x0001B79A
	protected virtual void setNameText(Transform window, ItemData itemData)
	{
		window.Find("Name").GetComponent<Text>().text = itemData.Name;
	}

	// Token: 0x0600247B RID: 9339 RVA: 0x0012895C File Offset: 0x00126B5C
	protected virtual void settipDescText(Transform window, ItemData itemData)
	{
		Text component = window.Find("Main Description").GetComponent<Text>();
		string text = Regex.Unescape(itemData.Descriptions[0]);
		component.text = text;
	}

	// Token: 0x0600247C RID: 9340 RVA: 0x0001D5B7 File Offset: 0x0001B7B7
	protected virtual void settooltipAttrText(Transform window, ItemData itemData)
	{
		window.Find("Secondary Description").GetComponent<Text>().text = "";
	}

	// Token: 0x04001F62 RID: 8034
	public int ItemID;

	// Token: 0x04001F63 RID: 8035
	public ulong ietmUUID;

	// Token: 0x04001F64 RID: 8036
	public GameObject ItemUI;
}
