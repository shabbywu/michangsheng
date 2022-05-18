using System;
using UnityEngine.Events;

namespace script.NewLianDan.LianDan
{
	// Token: 0x02000ACA RID: 2762
	public class LianDanSelect : BagItemSelect
	{
		// Token: 0x06004685 RID: 18053 RVA: 0x001E258C File Offset: 0x001E078C
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

		// Token: 0x06004686 RID: 18054 RVA: 0x001E2658 File Offset: 0x001E0858
		public override void UpdateUI(float call)
		{
			this.CurNum = (int)this.Slider.value;
			this.Content.SetText(Tools.Code64(string.Format("炼制{0}x{1}\n预计花费", this.ItemName, this.CurNum) + LianDanUIMag.Instance.LianDanPanel.GetCostTime(this.CurNum)));
		}
	}
}
