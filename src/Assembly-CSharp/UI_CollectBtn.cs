using System;
using UltimateSurvival;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

// Token: 0x020005B8 RID: 1464
public class UI_CollectBtn : ScrollBtn
{
	// Token: 0x060024F3 RID: 9459 RVA: 0x0001DAD0 File Offset: 0x0001BCD0
	private void Start()
	{
		base.GetComponent<Button>().onClick.AddListener(new UnityAction(this.OnDeselect));
	}

	// Token: 0x060024F4 RID: 9460 RVA: 0x0001DAEE File Offset: 0x0001BCEE
	protected override GameObject getItemUI()
	{
		return UI_HOMESCENE.instense.ItemCollect;
	}

	// Token: 0x060024F5 RID: 9461 RVA: 0x00129DFC File Offset: 0x00127FFC
	public void OnDeselect()
	{
		ItemData itemData = this.getItemData();
		Transform transform = base.findItemWindow();
		this.setImageIcon(transform, itemData);
		this.setNameText(transform, itemData);
		this.settipDescText(transform, itemData);
		this.settooltipAttrText(transform, itemData);
		Transform transform2 = transform.Find("Actions");
		transform2.Find("use");
		if (itemData.Category == "Consumable")
		{
			transform2.gameObject.SetActive(true);
			return;
		}
		transform2.gameObject.SetActive(false);
	}
}
