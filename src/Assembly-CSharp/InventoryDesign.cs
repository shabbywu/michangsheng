using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x0200020C RID: 524
public class InventoryDesign : MonoBehaviour
{
	// Token: 0x0600109D RID: 4253 RVA: 0x000A7144 File Offset: 0x000A5344
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

	// Token: 0x0600109E RID: 4254 RVA: 0x000A72D8 File Offset: 0x000A54D8
	public void updateEverything()
	{
		base.transform.GetChild(0).GetComponent<RectTransform>().localPosition = new Vector3((float)this.inventoryTitlePosX, (float)this.inventoryTitlePosY, 0f);
		base.transform.GetChild(0).GetComponent<Text>().text = this.inventoryTitle;
	}

	// Token: 0x0600109F RID: 4255 RVA: 0x000A7330 File Offset: 0x000A5530
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

	// Token: 0x060010A0 RID: 4256 RVA: 0x000A7394 File Offset: 0x000A5594
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

	// Token: 0x04000D15 RID: 3349
	[SerializeField]
	public Image slotDesignTemp;

	// Token: 0x04000D16 RID: 3350
	[SerializeField]
	public Image slotDesign;

	// Token: 0x04000D17 RID: 3351
	[SerializeField]
	public Image inventoryDesign;

	// Token: 0x04000D18 RID: 3352
	[SerializeField]
	public bool showInventoryCross;

	// Token: 0x04000D19 RID: 3353
	[SerializeField]
	public Image inventoryCrossImage;

	// Token: 0x04000D1A RID: 3354
	[SerializeField]
	public RectTransform inventoryCrossRectTransform;

	// Token: 0x04000D1B RID: 3355
	[SerializeField]
	public int inventoryCrossPosX;

	// Token: 0x04000D1C RID: 3356
	[SerializeField]
	public int inventoryCrossPosY;

	// Token: 0x04000D1D RID: 3357
	[SerializeField]
	public string inventoryTitle;

	// Token: 0x04000D1E RID: 3358
	[SerializeField]
	public Text inventoryTitleText;

	// Token: 0x04000D1F RID: 3359
	[SerializeField]
	public int inventoryTitlePosX;

	// Token: 0x04000D20 RID: 3360
	[SerializeField]
	public int inventoryTitlePosY;

	// Token: 0x04000D21 RID: 3361
	[SerializeField]
	public int panelSizeX;

	// Token: 0x04000D22 RID: 3362
	[SerializeField]
	public int panelSizeY;
}
