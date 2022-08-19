using System;
using UltimateSurvival;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

// Token: 0x02000413 RID: 1043
public class UI_ShopBtn : ScrollBtn
{
	// Token: 0x060021A0 RID: 8608 RVA: 0x000E9607 File Offset: 0x000E7807
	private void Start()
	{
		base.GetComponent<Button>().onClick.AddListener(new UnityAction(this.OnDeselect));
	}

	// Token: 0x060021A1 RID: 8609 RVA: 0x00004095 File Offset: 0x00002295
	private void Update()
	{
	}

	// Token: 0x060021A2 RID: 8610 RVA: 0x000E9625 File Offset: 0x000E7825
	protected override GameObject getItemUI()
	{
		return UI_HOMESCENE.instense.ItemInspector;
	}

	// Token: 0x060021A3 RID: 8611 RVA: 0x00004095 File Offset: 0x00002295
	protected override void setNameText(Transform window, ItemData itemData)
	{
	}

	// Token: 0x060021A4 RID: 8612 RVA: 0x00004095 File Offset: 0x00002295
	protected override void settipDescText(Transform window, ItemData itemData)
	{
	}

	// Token: 0x060021A5 RID: 8613 RVA: 0x000E9634 File Offset: 0x000E7834
	public void OnDeselect()
	{
		ItemData itemData = this.getItemData();
		Transform window = base.findItemWindow();
		this.setImageIcon(window, itemData);
		this.setNameText(window, itemData);
		this.settipDescText(window, itemData);
		this.settooltipAttrText(window, itemData);
	}

	// Token: 0x04001B1F RID: 6943
	public GameObject ItemInspector;
}
