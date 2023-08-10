using JSONClass;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UILingTianCell : MonoBehaviour
{
	public int Slot;

	public UIIconShow IconShow;

	public GameObject Bar;

	public Slider ProcessSlider;

	public Text ShengYuTimeText;

	private void Awake()
	{
	}

	public void RefreshUI()
	{
		int iD = UIDongFu.Inst.DongFu.LingTian[Slot].ID;
		int lingLi = UIDongFu.Inst.DongFu.LingTian[Slot].LingLi;
		if (iD > 1)
		{
			IconShow.SetItem(iD);
			Bar.SetActive(true);
			_ItemJsonData itemJsonData = _ItemJsonData.DataDict[iD];
			int num = lingLi / itemJsonData.price;
			int num2 = lingLi % itemJsonData.price;
			IconShow.Count = num + 1;
			ProcessSlider.value = (float)num2 / (float)itemJsonData.price;
			int num3 = itemJsonData.price - num2;
			for (int i = 1; i < int.MaxValue; i++)
			{
				int num4 = Mathf.Min(i, UILingTianPanel.Inst.CuiShengTime);
				float num5 = UILingTianPanel.Inst.BaseSpeedPer * (float)i + (float)num4 * UILingTianPanel.Inst.CuiShengSpeedPer;
				if (!((float)num3 > num5))
				{
					ShengYuTimeText.text = "剩" + i.MonthToDesc();
					break;
				}
			}
			IconShow.OnClick = delegate
			{
				//IL_0018: Unknown result type (might be due to invalid IL or missing references)
				//IL_0023: Expected O, but got Unknown
				if (UILingTianPanel.Inst.IsShouGe)
				{
					USelectBox.Show("药材正在生长，你确认要收割吗？", (UnityAction)delegate
					{
						DongFuManager.ShouHuo(DongFuManager.NowDongFuID, Slot);
					});
				}
			};
		}
		else
		{
			IconShow.SetNull();
			Bar.SetActive(false);
			IconShow.OnClick = null;
		}
	}
}
