using System;
using UltimateSurvival;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

// Token: 0x02000405 RID: 1029
public class UI_CheckinBtn : ScrollBtn
{
	// Token: 0x06002133 RID: 8499 RVA: 0x000E80E9 File Offset: 0x000E62E9
	private void Start()
	{
		base.GetComponent<Button>().onClick.AddListener(new UnityAction(this.OnDeselect));
	}

	// Token: 0x06002134 RID: 8500 RVA: 0x000E8107 File Offset: 0x000E6307
	protected override GameObject getItemUI()
	{
		return UI_HOMESCENE.instense.ItemCheckIn;
	}

	// Token: 0x06002135 RID: 8501 RVA: 0x000E8114 File Offset: 0x000E6314
	public void OnDeselect()
	{
		ItemData itemData = this.getItemData();
		Transform window = base.findItemWindow();
		this.setImageIcon(window, itemData);
		this.setNameText(window, itemData);
		this.settipDescText(window, itemData);
		this.settooltipAttrText(window, itemData);
	}
}
