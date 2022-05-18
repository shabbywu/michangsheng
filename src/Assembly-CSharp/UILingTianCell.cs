using System;
using JSONClass;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

// Token: 0x02000325 RID: 805
public class UILingTianCell : MonoBehaviour
{
	// Token: 0x060017B9 RID: 6073 RVA: 0x000042DD File Offset: 0x000024DD
	private void Awake()
	{
	}

	// Token: 0x060017BA RID: 6074 RVA: 0x000CEFB4 File Offset: 0x000CD1B4
	public void RefreshUI()
	{
		int id = UIDongFu.Inst.DongFu.LingTian[this.Slot].ID;
		int lingLi = UIDongFu.Inst.DongFu.LingTian[this.Slot].LingLi;
		if (id > 1)
		{
			this.IconShow.SetItem(id);
			this.Bar.SetActive(true);
			_ItemJsonData itemJsonData = _ItemJsonData.DataDict[id];
			int num = lingLi / itemJsonData.price;
			int num2 = lingLi % itemJsonData.price;
			this.IconShow.Count = num + 1;
			this.ProcessSlider.value = (float)num2 / (float)itemJsonData.price;
			int num3 = itemJsonData.price - num2;
			for (int i = 1; i < 2147483647; i++)
			{
				int num4 = Mathf.Min(i, UILingTianPanel.Inst.CuiShengTime);
				float num5 = UILingTianPanel.Inst.BaseSpeedPer * (float)i + (float)num4 * UILingTianPanel.Inst.CuiShengSpeedPer;
				if ((float)num3 <= num5)
				{
					this.ShengYuTimeText.text = "剩" + i.MonthToDesc();
					break;
				}
			}
			this.IconShow.OnClick = delegate(PointerEventData p)
			{
				if (UILingTianPanel.Inst.IsShouGe)
				{
					USelectBox.Show("药材正在生长，你确认要收割吗？", delegate
					{
						DongFuManager.ShouHuo(DongFuManager.NowDongFuID, this.Slot);
					}, null);
				}
			};
			return;
		}
		this.IconShow.SetNull();
		this.Bar.SetActive(false);
		this.IconShow.OnClick = null;
	}

	// Token: 0x04001319 RID: 4889
	public int Slot;

	// Token: 0x0400131A RID: 4890
	public UIIconShow IconShow;

	// Token: 0x0400131B RID: 4891
	public GameObject Bar;

	// Token: 0x0400131C RID: 4892
	public Slider ProcessSlider;

	// Token: 0x0400131D RID: 4893
	public Text ShengYuTimeText;
}
