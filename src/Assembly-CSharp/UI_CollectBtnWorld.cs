using System;
using UltimateSurvival;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

// Token: 0x02000409 RID: 1033
public class UI_CollectBtnWorld : ScrollBtn
{
	// Token: 0x06002145 RID: 8517 RVA: 0x000E82E6 File Offset: 0x000E64E6
	private void Start()
	{
		base.GetComponent<Button>().onClick.AddListener(new UnityAction(this.OnDeselect));
	}

	// Token: 0x06002146 RID: 8518 RVA: 0x000E8304 File Offset: 0x000E6504
	protected override GameObject getItemUI()
	{
		return GameObject.Find("8-Item Collect");
	}

	// Token: 0x06002147 RID: 8519 RVA: 0x000E8310 File Offset: 0x000E6510
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
