using System;
using UltimateSurvival;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

// Token: 0x02000408 RID: 1032
public class UI_CollectBtn : ScrollBtn
{
	// Token: 0x06002141 RID: 8513 RVA: 0x000E823E File Offset: 0x000E643E
	private void Start()
	{
		base.GetComponent<Button>().onClick.AddListener(new UnityAction(this.OnDeselect));
	}

	// Token: 0x06002142 RID: 8514 RVA: 0x000E825C File Offset: 0x000E645C
	protected override GameObject getItemUI()
	{
		return UI_HOMESCENE.instense.ItemCollect;
	}

	// Token: 0x06002143 RID: 8515 RVA: 0x000E8268 File Offset: 0x000E6468
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
