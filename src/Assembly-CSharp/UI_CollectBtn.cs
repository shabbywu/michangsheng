using UltimateSurvival;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UI_CollectBtn : ScrollBtn
{
	private void Start()
	{
		//IL_0012: Unknown result type (might be due to invalid IL or missing references)
		//IL_001c: Expected O, but got Unknown
		((UnityEvent)((Component)this).GetComponent<Button>().onClick).AddListener(new UnityAction(OnDeselect));
	}

	protected override GameObject getItemUI()
	{
		return UI_HOMESCENE.instense.ItemCollect;
	}

	public void OnDeselect()
	{
		ItemData itemData = getItemData();
		Transform val = findItemWindow();
		setImageIcon(val, itemData);
		setNameText(val, itemData);
		settipDescText(val, itemData);
		settooltipAttrText(val, itemData);
		Transform val2 = val.Find("Actions");
		val2.Find("use");
		if (itemData.Category == "Consumable")
		{
			((Component)val2).gameObject.SetActive(true);
		}
		else
		{
			((Component)val2).gameObject.SetActive(false);
		}
	}
}
