using System;
using UltimateSurvival;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

// Token: 0x020005B5 RID: 1461
public class UI_CheckinBtn : ScrollBtn
{
	// Token: 0x060024E5 RID: 9445 RVA: 0x0001DA13 File Offset: 0x0001BC13
	private void Start()
	{
		base.GetComponent<Button>().onClick.AddListener(new UnityAction(this.OnDeselect));
	}

	// Token: 0x060024E6 RID: 9446 RVA: 0x0001DA31 File Offset: 0x0001BC31
	protected override GameObject getItemUI()
	{
		return UI_HOMESCENE.instense.ItemCheckIn;
	}

	// Token: 0x060024E7 RID: 9447 RVA: 0x00129D64 File Offset: 0x00127F64
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
