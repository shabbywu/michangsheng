using System.Collections.Generic;
using UnityEngine;

public class UIInventoryLayout : MonoBehaviour
{
	private Vector4 padding = new Vector4(67f, 0f, 19f, 0f);

	private Vector2 cellSize = new Vector2(136f, 136f);

	private Vector2 spacing = new Vector2(7f, 8.5f);

	public int ColCount = 3;

	public List<UIIconShow> IconShowList = new List<UIIconShow>();

	public UIInventory Inventory;

	[HideInInspector]
	public RectTransform RT;

	[HideInInspector]
	public List<UIInventoryGridData> GridDataList = new List<UIInventoryGridData>();

	public float RTY => RT.anchoredPosition.y;

	private void Awake()
	{
		RT = ((Component)this).GetComponent<RectTransform>();
	}

	public void Init()
	{
		GridDataList.Clear();
		foreach (UIIconShow iconShow in IconShowList)
		{
			if (((Component)iconShow).gameObject.activeSelf)
			{
				((Component)iconShow).gameObject.SetActive(false);
			}
		}
	}

	public void RefreshUI()
	{
		int num = (int)(RTY / (spacing.y + cellSize.y)) * ColCount;
		num = Mathf.Max(num, 0);
		int num2 = num + 7 * ColCount - 1;
		num2 = Mathf.Clamp(num2, num, GridDataList.Count - 1);
		foreach (UIIconShow iconShow in IconShowList)
		{
			if (((Component)iconShow).gameObject.activeSelf && (iconShow.InventoryGridData == null || iconShow.InventoryGridData.Index < num || iconShow.InventoryGridData.Index > num2))
			{
				((Component)iconShow).gameObject.SetActive(false);
				iconShow.InventoryGridData = null;
			}
		}
		for (int i = num; i <= num2; i++)
		{
			if (i < 0 || i >= GridDataList.Count)
			{
				Debug.LogError((object)$"{i}超出了GridDataList数量{GridDataList.Count}");
				continue;
			}
			UIInventoryGridData uIInventoryGridData = GridDataList[i];
			if ((Object)(object)uIInventoryGridData.BindShow != (Object)null && ((Component)uIInventoryGridData.BindShow).gameObject.activeInHierarchy && uIInventoryGridData.BindShow.InventoryGridData == uIInventoryGridData)
			{
				continue;
			}
			foreach (UIIconShow iconShow2 in IconShowList)
			{
				if (!((Component)iconShow2).gameObject.activeInHierarchy)
				{
					iconShow2.InventoryGridData = uIInventoryGridData;
					uIInventoryGridData.IconShowInit(iconShow2);
					((Component)iconShow2).gameObject.SetActive(true);
					break;
				}
			}
		}
	}

	public void CalcGridPos()
	{
		//IL_00a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a7: Unknown result type (might be due to invalid IL or missing references)
		foreach (UIInventoryGridData gridData in GridDataList)
		{
			int index = gridData.Index;
			float num = padding.x + cellSize.x / 2f + (float)(index % ColCount) * (spacing.x + cellSize.x);
			float num2 = 0f - (padding.z + cellSize.y / 2f + (float)(index / ColCount) * (spacing.y + cellSize.y));
			gridData.Pos = new Vector2(num, num2);
		}
	}
}
