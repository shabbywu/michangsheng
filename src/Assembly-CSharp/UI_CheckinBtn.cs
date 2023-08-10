using UltimateSurvival;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UI_CheckinBtn : ScrollBtn
{
	private void Start()
	{
		//IL_0012: Unknown result type (might be due to invalid IL or missing references)
		//IL_001c: Expected O, but got Unknown
		((UnityEvent)((Component)this).GetComponent<Button>().onClick).AddListener(new UnityAction(OnDeselect));
	}

	protected override GameObject getItemUI()
	{
		return UI_HOMESCENE.instense.ItemCheckIn;
	}

	public void OnDeselect()
	{
		ItemData itemData = getItemData();
		Transform window = findItemWindow();
		setImageIcon(window, itemData);
		setNameText(window, itemData);
		settipDescText(window, itemData);
		settooltipAttrText(window, itemData);
	}
}
