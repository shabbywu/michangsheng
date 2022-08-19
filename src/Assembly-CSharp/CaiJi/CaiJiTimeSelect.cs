using System;
using UnityEngine;
using UnityEngine.UI;

namespace CaiJi
{
	// Token: 0x02000735 RID: 1845
	public class CaiJiTimeSelect : MonoBehaviour
	{
		// Token: 0x06003AD0 RID: 15056 RVA: 0x001945AA File Offset: 0x001927AA
		private void Awake()
		{
			this.TimeSlider.maxValue = (float)this.MaxValue;
			this.TimeSlider.minValue = (float)this.MinValue;
			this.TimeSlider.wholeNumbers = true;
		}

		// Token: 0x06003AD1 RID: 15057 RVA: 0x001945DC File Offset: 0x001927DC
		public void Init(bool isLingHeCaiJi = false)
		{
			this.IsLingHeCaiJi = isLingHeCaiJi;
			this.UpdateValue();
			this.TimeSlider.onValueChanged.AddListener(delegate(float obj)
			{
				this.UpdateValue();
			});
		}

		// Token: 0x06003AD2 RID: 15058 RVA: 0x00194608 File Offset: 0x00192808
		public void Add()
		{
			this.TimeSlider.value += 1f;
			if (this.TimeSlider.value > (float)this.MaxValue)
			{
				this.TimeSlider.value = (float)this.MaxValue;
			}
			this.UpdateValue();
		}

		// Token: 0x06003AD3 RID: 15059 RVA: 0x00194658 File Offset: 0x00192858
		public void Reduce()
		{
			this.TimeSlider.value -= 1f;
			if (this.TimeSlider.value < 0f)
			{
				this.TimeSlider.value = (float)this.MinValue;
			}
			this.UpdateValue();
		}

		// Token: 0x06003AD4 RID: 15060 RVA: 0x001946A8 File Offset: 0x001928A8
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

		// Token: 0x040032F0 RID: 13040
		[SerializeField]
		private Slider TimeSlider;

		// Token: 0x040032F1 RID: 13041
		[SerializeField]
		private Text Year;

		// Token: 0x040032F2 RID: 13042
		[SerializeField]
		private Text Month;

		// Token: 0x040032F3 RID: 13043
		[SerializeField]
		private int MinValue;

		// Token: 0x040032F4 RID: 13044
		[SerializeField]
		private int MaxValue;

		// Token: 0x040032F5 RID: 13045
		private bool IsLingHeCaiJi;
	}
}
