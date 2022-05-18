using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000414 RID: 1044
public class UIInventoryLayout : MonoBehaviour
{
	// Token: 0x17000291 RID: 657
	// (get) Token: 0x06001C21 RID: 7201 RVA: 0x00017877 File Offset: 0x00015A77
	public float RTY
	{
		get
		{
			return this.RT.anchoredPosition.y;
		}
	}

	// Token: 0x06001C22 RID: 7202 RVA: 0x00017889 File Offset: 0x00015A89
	private void Awake()
	{
		this.RT = base.GetComponent<RectTransform>();
	}

	// Token: 0x06001C23 RID: 7203 RVA: 0x000FA904 File Offset: 0x000F8B04
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

	// Token: 0x06001C24 RID: 7204 RVA: 0x000FA974 File Offset: 0x000F8B74
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

	// Token: 0x06001C25 RID: 7205 RVA: 0x000FAB58 File Offset: 0x000F8D58
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

	// Token: 0x0400181C RID: 6172
	private Vector4 padding = new Vector4(67f, 0f, 19f, 0f);

	// Token: 0x0400181D RID: 6173
	private Vector2 cellSize = new Vector2(136f, 136f);

	// Token: 0x0400181E RID: 6174
	private Vector2 spacing = new Vector2(7f, 8.5f);

	// Token: 0x0400181F RID: 6175
	public int ColCount = 3;

	// Token: 0x04001820 RID: 6176
	public List<UIIconShow> IconShowList = new List<UIIconShow>();

	// Token: 0x04001821 RID: 6177
	public UIInventory Inventory;

	// Token: 0x04001822 RID: 6178
	[HideInInspector]
	public RectTransform RT;

	// Token: 0x04001823 RID: 6179
	[HideInInspector]
	public List<UIInventoryGridData> GridDataList = new List<UIInventoryGridData>();
}
