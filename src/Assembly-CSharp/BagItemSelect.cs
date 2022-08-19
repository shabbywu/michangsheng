using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

// Token: 0x020002DA RID: 730
public class BagItemSelect : MonoBehaviour, ISelectNum
{
	// Token: 0x06001962 RID: 6498 RVA: 0x000B5D01 File Offset: 0x000B3F01
	private void Awake()
	{
		this.Slider.onValueChanged.AddListener(new UnityAction<float>(this.UpdateUI));
	}

	// Token: 0x06001963 RID: 6499 RVA: 0x000B5D20 File Offset: 0x000B3F20
	public virtual void Init(string itemName, int maxNum, UnityAction Ok = null, UnityAction Cancel = null)
	{
		if (maxNum == 1)
		{
			this.CurNum = 1;
			if (Ok != null)
			{
				Ok.Invoke();
			}
			this.Close();
			return;
		}
		this.Clear();
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

	// Token: 0x06001964 RID: 6500 RVA: 0x000B5E10 File Offset: 0x000B4010
	public void Clear()
	{
		this.CurNum = 0;
		this.MinNum = 0;
		this.MinNum = 0;
		this.Slider.value = 0f;
		this.OkBtn.mouseUpEvent.RemoveAllListeners();
		this.CancelBtn.mouseUpEvent.RemoveAllListeners();
	}

	// Token: 0x06001965 RID: 6501 RVA: 0x000B5E62 File Offset: 0x000B4062
	public void Close()
	{
		base.gameObject.SetActive(false);
	}

	// Token: 0x06001966 RID: 6502 RVA: 0x000B5E70 File Offset: 0x000B4070
	public virtual void UpdateUI(float call)
	{
		this.CurNum = (int)this.Slider.value;
		this.Content.SetText(string.Format("{0}x{1}", this.ItemName, this.CurNum));
	}

	// Token: 0x06001967 RID: 6503 RVA: 0x000B5EAA File Offset: 0x000B40AA
	public void AddNum()
	{
		this.CurNum++;
		if (this.CurNum > this.MaxNum)
		{
			this.CurNum = this.MaxNum;
		}
		this.Slider.value = (float)this.CurNum;
	}

	// Token: 0x06001968 RID: 6504 RVA: 0x000B5EE6 File Offset: 0x000B40E6
	public void ReduceNum()
	{
		this.CurNum--;
		if (this.CurNum < this.MinNum)
		{
			this.CurNum = this.MinNum;
		}
		this.Slider.value = (float)this.CurNum;
	}

	// Token: 0x0400148F RID: 5263
	public Slider Slider;

	// Token: 0x04001490 RID: 5264
	public string ItemName;

	// Token: 0x04001491 RID: 5265
	public int CurNum;

	// Token: 0x04001492 RID: 5266
	public int MaxNum;

	// Token: 0x04001493 RID: 5267
	public int MinNum;

	// Token: 0x04001494 RID: 5268
	public Text Content;

	// Token: 0x04001495 RID: 5269
	public FpBtn OkBtn;

	// Token: 0x04001496 RID: 5270
	public FpBtn CancelBtn;
}
