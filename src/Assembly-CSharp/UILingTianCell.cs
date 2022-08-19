using System;
using JSONClass;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

// Token: 0x0200020D RID: 525
public class UILingTianCell : MonoBehaviour
{
	// Token: 0x06001508 RID: 5384 RVA: 0x00004095 File Offset: 0x00002295
	private void Awake()
	{
	}

	// Token: 0x06001509 RID: 5385 RVA: 0x00086814 File Offset: 0x00084A14
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

	// Token: 0x04000FC9 RID: 4041
	public int Slot;

	// Token: 0x04000FCA RID: 4042
	public UIIconShow IconShow;

	// Token: 0x04000FCB RID: 4043
	public GameObject Bar;

	// Token: 0x04000FCC RID: 4044
	public Slider ProcessSlider;

	// Token: 0x04000FCD RID: 4045
	public Text ShengYuTimeText;
}
