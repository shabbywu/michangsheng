using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

// Token: 0x0200042B RID: 1067
public class BagItemSelect : MonoBehaviour, ISelectNum
{
	// Token: 0x06001C75 RID: 7285 RVA: 0x00017C0E File Offset: 0x00015E0E
	private void Awake()
	{
		this.Slider.onValueChanged.AddListener(new UnityAction<float>(this.UpdateUI));
	}

	// Token: 0x06001C76 RID: 7286 RVA: 0x000FBD78 File Offset: 0x000F9F78
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

	// Token: 0x06001C77 RID: 7287 RVA: 0x000FBE68 File Offset: 0x000FA068
	public void Clear()
	{
		this.CurNum = 0;
		this.MinNum = 0;
		this.MinNum = 0;
		this.Slider.value = 0f;
		this.OkBtn.mouseUpEvent.RemoveAllListeners();
		this.CancelBtn.mouseUpEvent.RemoveAllListeners();
	}

	// Token: 0x06001C78 RID: 7288 RVA: 0x00017C2D File Offset: 0x00015E2D
	public void Close()
	{
		base.gameObject.SetActive(false);
	}

	// Token: 0x06001C79 RID: 7289 RVA: 0x00017C3B File Offset: 0x00015E3B
	public virtual void UpdateUI(float call)
	{
		this.CurNum = (int)this.Slider.value;
		this.Content.SetText(string.Format("{0}x{1}", this.ItemName, this.CurNum));
	}

	// Token: 0x06001C7A RID: 7290 RVA: 0x00017C75 File Offset: 0x00015E75
	public void AddNum()
	{
		this.CurNum++;
		if (this.CurNum > this.MaxNum)
		{
			this.CurNum = this.MaxNum;
		}
		this.Slider.value = (float)this.CurNum;
	}

	// Token: 0x06001C7B RID: 7291 RVA: 0x00017CB1 File Offset: 0x00015EB1
	public void ReduceNum()
	{
		this.CurNum--;
		if (this.CurNum < this.MinNum)
		{
			this.CurNum = this.MinNum;
		}
		this.Slider.value = (float)this.CurNum;
	}

	// Token: 0x0400186C RID: 6252
	public Slider Slider;

	// Token: 0x0400186D RID: 6253
	public string ItemName;

	// Token: 0x0400186E RID: 6254
	public int CurNum;

	// Token: 0x0400186F RID: 6255
	public int MaxNum;

	// Token: 0x04001870 RID: 6256
	public int MinNum;

	// Token: 0x04001871 RID: 6257
	public Text Content;

	// Token: 0x04001872 RID: 6258
	public FpBtn OkBtn;

	// Token: 0x04001873 RID: 6259
	public FpBtn CancelBtn;
}
