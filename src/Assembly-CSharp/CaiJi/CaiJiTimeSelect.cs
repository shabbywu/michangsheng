using System;
using UnityEngine;
using UnityEngine.UI;

namespace CaiJi
{
	// Token: 0x02000A97 RID: 2711
	public class CaiJiTimeSelect : MonoBehaviour
	{
		// Token: 0x0600458E RID: 17806 RVA: 0x00031C3D File Offset: 0x0002FE3D
		private void Awake()
		{
			this.TimeSlider.maxValue = (float)this.MaxValue;
			this.TimeSlider.minValue = (float)this.MinValue;
			this.TimeSlider.wholeNumbers = true;
		}

		// Token: 0x0600458F RID: 17807 RVA: 0x00031C6F File Offset: 0x0002FE6F
		public void Init(bool isLingHeCaiJi = false)
		{
			this.IsLingHeCaiJi = isLingHeCaiJi;
			this.UpdateValue();
			this.TimeSlider.onValueChanged.AddListener(delegate(float obj)
			{
				this.UpdateValue();
			});
		}

		// Token: 0x06004590 RID: 17808 RVA: 0x001DBFDC File Offset: 0x001DA1DC
		public void Add()
		{
			this.TimeSlider.value += 1f;
			if (this.TimeSlider.value > (float)this.MaxValue)
			{
				this.TimeSlider.value = (float)this.MaxValue;
			}
			this.UpdateValue();
		}

		// Token: 0x06004591 RID: 17809 RVA: 0x001DC02C File Offset: 0x001DA22C
		public void Reduce()
		{
			this.TimeSlider.value -= 1f;
			if (this.TimeSlider.value < 0f)
			{
				this.TimeSlider.value = (float)this.MinValue;
			}
			this.UpdateValue();
		}

		// Token: 0x06004592 RID: 17810 RVA: 0x001DC07C File Offset: 0x001DA27C
		private void UpdateValue()
		{
			if (this.IsLingHeCaiJi)
			{
				LingHeCaiJiUIMag.inst.CostTime = (int)this.TimeSlider.value;
				this.Month.text = (LingHeCaiJiUIMag.inst.CostTime % 12).ToString();
				this.Year.text = (LingHeCaiJiUIMag.inst.CostTime / 12).ToString();
				LingHeCaiJiUIMag.inst.UpdateItemShow();
				return;
			}
			CaiJiUIMag.inst.CostTime = (int)this.TimeSlider.value;
			this.Month.text = (CaiJiUIMag.inst.CostTime % 12).ToString();
			this.Year.text = (CaiJiUIMag.inst.CostTime / 12).ToString();
			CaiJiUIMag.inst.UpdateMayGetItem();
		}

		// Token: 0x04003DA9 RID: 15785
		[SerializeField]
		private Slider TimeSlider;

		// Token: 0x04003DAA RID: 15786
		[SerializeField]
		private Text Year;

		// Token: 0x04003DAB RID: 15787
		[SerializeField]
		private Text Month;

		// Token: 0x04003DAC RID: 15788
		[SerializeField]
		private int MinValue;

		// Token: 0x04003DAD RID: 15789
		[SerializeField]
		private int MaxValue;

		// Token: 0x04003DAE RID: 15790
		private bool IsLingHeCaiJi;
	}
}
