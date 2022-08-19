using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020002C9 RID: 713
public class UIInventoryLayout : MonoBehaviour
{
	// Token: 0x17000247 RID: 583
	// (get) Token: 0x06001919 RID: 6425 RVA: 0x000B47FE File Offset: 0x000B29FE
	public float RTY
	{
		get
		{
			return this.RT.anchoredPosition.y;
		}
	}

	// Token: 0x0600191A RID: 6426 RVA: 0x000B4810 File Offset: 0x000B2A10
	private void Awake()
	{
		this.RT = base.GetComponent<RectTransform>();
	}

	// Token: 0x0600191B RID: 6427 RVA: 0x000B4820 File Offset: 0x000B2A20
	public void Init()
	{
		this.GridDataList.Clear();
		foreach (UIIconShow uiiconShow in this.IconShowList)
		{
			if (uiiconShow.gameObject.activeSelf)
			{
				uiiconShow.gameObject.SetActive(false);
			}
		}
	}

	// Token: 0x0600191C RID: 6428 RVA: 0x000B4890 File Offset: 0x000B2A90
	public void RefreshUI()
	{
		int num = (int)(this.RTY / (this.spacing.y + this.cellSize.y)) * this.ColCount;
		num = Mathf.Max(num, 0);
		int num2 = num + 7 * this.ColCount - 1;
		num2 = Mathf.Clamp(num2, num, this.GridDataList.Count - 1);
		foreach (UIIconShow uiiconShow in this.IconShowList)
		{
			if (uiiconShow.gameObject.activeSelf && (uiiconShow.InventoryGridData == null || uiiconShow.InventoryGridData.Index < num || uiiconShow.InventoryGridData.Index > num2))
			{
				uiiconShow.gameObject.SetActive(false);
				uiiconShow.InventoryGridData = null;
			}
		}
		for (int i = num; i <= num2; i++)
		{
			if (i < 0 || i >= this.GridDataList.Count)
			{
				Debug.LogError(string.Format("{0}超出了GridDataList数量{1}", i, this.GridDataList.Count));
			}
			else
			{
				UIInventoryGridData uiinventoryGridData = this.GridDataList[i];
				if (!(uiinventoryGridData.BindShow != null) || !uiinventoryGridData.BindShow.gameObject.activeInHierarchy || uiinventoryGridData.BindShow.InventoryGridData != uiinventoryGridData)
				{
					foreach (UIIconShow uiiconShow2 in this.IconShowList)
					{
						if (!uiiconShow2.gameObject.activeInHierarchy)
						{
							uiiconShow2.InventoryGridData = uiinventoryGridData;
							uiinventoryGridData.IconShowInit(uiiconShow2);
							uiiconShow2.gameObject.SetActive(true);
							break;
						}
					}
				}
			}
		}
	}

	// Token: 0x0600191D RID: 6429 RVA: 0x000B4A74 File Offset: 0x000B2C74
	public void CalcGridPos()
	{
		foreach (UIInventoryGridData uiinventoryGridData in this.GridDataList)
		{
			int index = uiinventoryGridData.Index;
			float num = this.padding.x + this.cellSize.x / 2f + (float)(index % this.ColCount) * (this.spacing.x + this.cellSize.x);
			float num2 = -(this.padding.z + this.cellSize.y / 2f + (float)(index / this.ColCount) * (this.spacing.y + this.cellSize.y));
			uiinventoryGridData.Pos = new Vector2(num, num2);
		}
	}

	// Token: 0x04001452 RID: 5202
	private Vector4 padding = new Vector4(67f, 0f, 19f, 0f);

	// Token: 0x04001453 RID: 5203
	private Vector2 cellSize = new Vector2(136f, 136f);

	// Token: 0x04001454 RID: 5204
	private Vector2 spacing = new Vector2(7f, 8.5f);

	// Token: 0x04001455 RID: 5205
	public int ColCount = 3;

	// Token: 0x04001456 RID: 5206
	public List<UIIconShow> IconShowList = new List<UIIconShow>();

	// Token: 0x04001457 RID: 5207
	public UIInventory Inventory;

	// Token: 0x04001458 RID: 5208
	[HideInInspector]
	public RectTransform RT;

	// Token: 0x04001459 RID: 5209
	[HideInInspector]
	public List<UIInventoryGridData> GridDataList = new List<UIInventoryGridData>();
}
