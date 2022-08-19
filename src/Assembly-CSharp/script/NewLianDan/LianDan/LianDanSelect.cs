using System;
using UnityEngine.Events;

namespace script.NewLianDan.LianDan
{
	// Token: 0x020009FC RID: 2556
	public class LianDanSelect : BagItemSelect
	{
		// Token: 0x060046D8 RID: 18136 RVA: 0x001DFE78 File Offset: 0x001DE078
		public override void Init(string itemName, int maxNum, UnityAction Ok = null, UnityAction Cancel = null)
		{
			base.Clear();
			this.CurNum = 1;
			this.MinNum = 1;
			this.MaxNum = maxNum;
			this.Slider.maxValue = (float)maxNum;
			this.Slider.minValue = 1f;
			this.ItemName = itemName;
			this.Slider.value = (float)this.CurNum;
			this.UpdateUI(0f);
			base.gameObject.SetActive(true);
			this.OkBtn.mouseUpEvent.AddListener(delegate()
			{
				if (Ok != null)
				{
					Ok.Invoke();
				}
				this.Close();
			});
			this.CancelBtn.mouseUpEvent.AddListener(delegate()
			{
				if (Cancel != null)
				{
					Cancel.Invoke();
				}
				this.Close();
			});
		}

		// Token: 0x060046D9 RID: 18137 RVA: 0x001DFF44 File Offset: 0x001DE144
		public override void UpdateUI(float call)
		{
			this.CurNum = (int)this.Slider.value;
			this.Content.SetText(Tools.Code64(string.Format("炼制{0}x{1}\n预计花费", this.ItemName, this.CurNum) + LianDanUIMag.Instance.LianDanPanel.GetCostTime(this.CurNum)));
		}
	}
}
