using System;
using UltimateSurvival;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

// Token: 0x020005C5 RID: 1477
public class UI_ShopBtn : ScrollBtn
{
	// Token: 0x06002558 RID: 9560 RVA: 0x0001DF38 File Offset: 0x0001C138
	private void Start()
	{
		base.GetComponent<Button>().onClick.AddListener(new UnityAction(this.OnDeselect));
	}

	// Token: 0x06002559 RID: 9561 RVA: 0x000042DD File Offset: 0x000024DD
	private void Update()
	{
	}

	// Token: 0x0600255A RID: 9562 RVA: 0x0001DF56 File Offset: 0x0001C156
	protected override GameObject getItemUI()
	{
		return UI_HOMESCENE.instense.ItemInspector;
	}

	// Token: 0x0600255B RID: 9563 RVA: 0x000042DD File Offset: 0x000024DD
	protected override void setNameText(Transform window, ItemData itemData)
	{
	}

	// Token: 0x0600255C RID: 9564 RVA: 0x000042DD File Offset: 0x000024DD
	protected override void settipDescText(Transform window, ItemData itemData)
	{
	}

	// Token: 0x0600255D RID: 9565 RVA: 0x00129D64 File Offset: 0x00127F64
	public void OnDeselect()
	{
		ItemData itemData = this.getItemData();
		Transform window = base.findItemWindow();
		this.setImageIcon(window, itemData);
		this.setNameText(window, itemData);
		this.settipDescText(window, itemData);
		this.settooltipAttrText(window, itemData);
	}

	// Token: 0x04001FDE RID: 8158
	public GameObject ItemInspector;
}
