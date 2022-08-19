using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000139 RID: 313
public class InventoryDesign : MonoBehaviour
{
	// Token: 0x06000E87 RID: 3719 RVA: 0x00056DCC File Offset: 0x00054FCC
	public void setVariables()
	{
		this.inventoryTitlePosX = (int)base.transform.GetChild(0).GetComponent<RectTransform>().localPosition.x;
		this.inventoryTitlePosY = (int)base.transform.GetChild(0).GetComponent<RectTransform>().localPosition.y;
		this.panelSizeX = (int)base.GetComponent<RectTransform>().sizeDelta.x;
		this.panelSizeY = (int)base.GetComponent<RectTransform>().sizeDelta.y;
		this.inventoryTitle = base.transform.GetChild(0).GetComponent<Text>().text;
		this.inventoryTitleText = base.transform.GetChild(0).GetComponent<Text>();
		if (base.GetComponent<Hotbar>() == null)
		{
			this.inventoryCrossRectTransform = base.transform.GetChild(2).GetComponent<RectTransform>();
			this.inventoryCrossImage = base.transform.GetChild(2).GetComponent<Image>();
		}
		this.inventoryDesign = base.GetComponent<Image>();
		this.slotDesign = base.transform.GetChild(1).GetChild(0).GetComponent<Image>();
		this.slotDesignTemp = this.slotDesign;
		this.slotDesignTemp.sprite = this.slotDesign.sprite;
		this.slotDesignTemp.color = this.slotDesign.color;
		this.slotDesignTemp.material = this.slotDesign.material;
		this.slotDesignTemp.type = this.slotDesign.type;
		this.slotDesignTemp.fillCenter = this.slotDesign.fillCenter;
	}

	// Token: 0x06000E88 RID: 3720 RVA: 0x00056F60 File Offset: 0x00055160
	public void updateEverything()
	{
		base.transform.GetChild(0).GetComponent<RectTransform>().localPosition = new Vector3((float)this.inventoryTitlePosX, (float)this.inventoryTitlePosY, 0f);
		base.transform.GetChild(0).GetComponent<Text>().text = this.inventoryTitle;
	}

	// Token: 0x06000E89 RID: 3721 RVA: 0x00056FB8 File Offset: 0x000551B8
	public void changeCrossSettings()
	{
		GameObject gameObject = base.transform.GetChild(2).gameObject;
		if (this.showInventoryCross)
		{
			gameObject.SetActive(this.showInventoryCross);
			this.inventoryCrossRectTransform.localPosition = new Vector3((float)this.inventoryCrossPosX, (float)this.inventoryCrossPosY, 0f);
			return;
		}
		gameObject.SetActive(this.showInventoryCross);
	}

	// Token: 0x06000E8A RID: 3722 RVA: 0x0005701C File Offset: 0x0005521C
	public void updateAllSlots()
	{
		for (int i = 0; i < base.transform.GetChild(1).childCount; i++)
		{
			Image component = base.transform.GetChild(1).GetChild(i).GetComponent<Image>();
			component.sprite = this.slotDesignTemp.sprite;
			component.color = this.slotDesignTemp.color;
			component.material = this.slotDesignTemp.material;
			component.type = this.slotDesignTemp.type;
			component.fillCenter = this.slotDesignTemp.fillCenter;
		}
	}

	// Token: 0x04000A7D RID: 2685
	[SerializeField]
	public Image slotDesignTemp;

	// Token: 0x04000A7E RID: 2686
	[SerializeField]
	public Image slotDesign;

	// Token: 0x04000A7F RID: 2687
	[SerializeField]
	public Image inventoryDesign;

	// Token: 0x04000A80 RID: 2688
	[SerializeField]
	public bool showInventoryCross;

	// Token: 0x04000A81 RID: 2689
	[SerializeField]
	public Image inventoryCrossImage;

	// Token: 0x04000A82 RID: 2690
	[SerializeField]
	public RectTransform inventoryCrossRectTransform;

	// Token: 0x04000A83 RID: 2691
	[SerializeField]
	public int inventoryCrossPosX;

	// Token: 0x04000A84 RID: 2692
	[SerializeField]
	public int inventoryCrossPosY;

	// Token: 0x04000A85 RID: 2693
	[SerializeField]
	public string inventoryTitle;

	// Token: 0x04000A86 RID: 2694
	[SerializeField]
	public Text inventoryTitleText;

	// Token: 0x04000A87 RID: 2695
	[SerializeField]
	public int inventoryTitlePosX;

	// Token: 0x04000A88 RID: 2696
	[SerializeField]
	public int inventoryTitlePosY;

	// Token: 0x04000A89 RID: 2697
	[SerializeField]
	public int panelSizeX;

	// Token: 0x04000A8A RID: 2698
	[SerializeField]
	public int panelSizeY;
}
