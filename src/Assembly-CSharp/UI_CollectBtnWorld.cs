using System;
using UltimateSurvival;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

// Token: 0x020005B9 RID: 1465
public class UI_CollectBtnWorld : ScrollBtn
{
	// Token: 0x060024F7 RID: 9463 RVA: 0x0001DAFA File Offset: 0x0001BCFA
	private void Start()
	{
		base.GetComponent<Button>().onClick.AddListener(new UnityAction(this.OnDeselect));
	}

	// Token: 0x060024F8 RID: 9464 RVA: 0x0001DB18 File Offset: 0x0001BD18
	protected override GameObject getItemUI()
	{
		return GameObject.Find("8-Item Collect");
	}

	// Token: 0x060024F9 RID: 9465 RVA: 0x00129D64 File Offset: 0x00127F64
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
