using System.Text.RegularExpressions;
using UltimateSurvival;
using UnityEngine;
using UnityEngine.UI;

public abstract class ScrollBtn : MonoBehaviour
{
	public int ItemID;

	public ulong ietmUUID;

	public GameObject ItemUI;

	private void Start()
	{
	}

	protected abstract GameObject getItemUI();

	protected Transform findItemWindow()
	{
		GameObject itemUI = getItemUI();
		itemUI.SetActive(true);
		Tooltip component = itemUI.GetComponent<Tooltip>();
		component.item.itemID = ItemID;
		component.item.itemUUID = ietmUUID;
		return itemUI.transform.Find("Window");
	}

	protected virtual ItemData getItemData()
	{
		UI_HOMESCENE.instense.database.FindItemById(ItemID, out var itemData);
		return itemData;
	}

	protected virtual void setImageIcon(Transform window, ItemData itemData)
	{
		((Component)window.Find("Icon")).GetComponent<Image>().sprite = itemData.Icon;
	}

	protected virtual void setNameText(Transform window, ItemData itemData)
	{
		((Component)window.Find("Name")).GetComponent<Text>().text = itemData.Name;
	}

	protected virtual void settipDescText(Transform window, ItemData itemData)
	{
		Text component = ((Component)window.Find("Main Description")).GetComponent<Text>();
		string text = Regex.Unescape(itemData.Descriptions[0]);
		component.text = text;
	}

	protected virtual void settooltipAttrText(Transform window, ItemData itemData)
	{
		((Component)window.Find("Secondary Description")).GetComponent<Text>().text = "";
	}
}
