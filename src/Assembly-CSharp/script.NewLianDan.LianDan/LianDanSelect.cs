using UnityEngine;
using UnityEngine.Events;

namespace script.NewLianDan.LianDan;

public class LianDanSelect : BagItemSelect
{
	public override void Init(string itemName, int maxNum, UnityAction Ok = null, UnityAction Cancel = null)
	{
		//IL_0096: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a0: Expected O, but got Unknown
		//IL_00b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bc: Expected O, but got Unknown
		Clear();
		CurNum = 1;
		MinNum = 1;
		MaxNum = maxNum;
		Slider.maxValue = maxNum;
		Slider.minValue = 1f;
		ItemName = itemName;
		Slider.value = CurNum;
		UpdateUI(0f);
		((Component)this).gameObject.SetActive(true);
		OkBtn.mouseUpEvent.AddListener((UnityAction)delegate
		{
			if (Ok != null)
			{
				Ok.Invoke();
			}
			Close();
		});
		CancelBtn.mouseUpEvent.AddListener((UnityAction)delegate
		{
			if (Cancel != null)
			{
				Cancel.Invoke();
			}
			Close();
		});
	}

	public override void UpdateUI(float call)
	{
		CurNum = (int)Slider.value;
		Content.SetText(Tools.Code64($"炼制{ItemName}x{CurNum}\n预计花费" + LianDanUIMag.Instance.LianDanPanel.GetCostTime(CurNum)));
	}
}
