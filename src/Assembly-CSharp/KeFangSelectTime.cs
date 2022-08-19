using System;
using KBEngine;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// Token: 0x020002E1 RID: 737
public class KeFangSelectTime : MonoBehaviour, IESCClose
{
	// Token: 0x06001997 RID: 6551 RVA: 0x000B6B3C File Offset: 0x000B4D3C
	private void Awake()
	{
		base.transform.SetParent(NewUICanvas.Inst.gameObject.transform);
		base.transform.localScale = Vector3.one;
		base.transform.SetAsLastSibling();
		base.transform.GetComponent<RectTransform>().offsetMin = new Vector2(0f, 0f);
		base.transform.GetComponent<RectTransform>().offsetMax = new Vector2(0f, 0f);
		KeFangSelectTime.inst = this;
		this.player = Tools.instance.getPlayer();
		this.curMonth = 1;
	}

	// Token: 0x06001998 RID: 6552 RVA: 0x000B6BDC File Offset: 0x000B4DDC
	public void Init()
	{
		if ((int)this.player.money / this.price < this.maxMonth)
		{
			this.maxMonth = (int)this.player.money / this.price;
		}
		this.slider.value = 1f / (float)this.maxMonth;
		this.updateData();
		this.slider.onValueChanged.AddListener(new UnityAction<float>(this.OnDragSlider));
		ESCCloseManager.Inst.RegisterClose(KeFangSelectTime.inst);
	}

	// Token: 0x06001999 RID: 6553 RVA: 0x000B6C66 File Offset: 0x000B4E66
	public void OnDragSlider(float data)
	{
		this.curMonth = (int)(data * (float)this.maxMonth);
		if (this.curMonth < 1)
		{
			this.curMonth = 1;
		}
		this.updateData();
	}

	// Token: 0x0600199A RID: 6554 RVA: 0x000B6C90 File Offset: 0x000B4E90
	private void updateData()
	{
		this.year.text = (this.curMonth / 12).ToString();
		this.month.text = (this.curMonth % 12).ToString();
		this.needLingShi.text = (this.curMonth * this.price).ToString();
	}

	// Token: 0x0600199B RID: 6555 RVA: 0x000B6CF8 File Offset: 0x000B4EF8
	public void AddMonth()
	{
		this.curMonth++;
		if (this.curMonth > this.maxMonth)
		{
			this.curMonth = this.maxMonth;
		}
		this.slider.value = (float)this.curMonth / (float)this.maxMonth;
		this.updateData();
	}

	// Token: 0x0600199C RID: 6556 RVA: 0x000B6D4D File Offset: 0x000B4F4D
	public void ReduceMonth()
	{
		this.curMonth--;
		if (this.curMonth < 1)
		{
			this.curMonth = 1;
		}
		this.slider.value = (float)this.curMonth / (float)this.maxMonth;
		this.updateData();
	}

	// Token: 0x0600199D RID: 6557 RVA: 0x000B6D8D File Offset: 0x000B4F8D
	public void Close()
	{
		KeFangSelectTime.inst = null;
		ESCCloseManager.Inst.UnRegisterClose(this);
		Object.Destroy(base.gameObject);
	}

	// Token: 0x0600199E RID: 6558 RVA: 0x000B6DAC File Offset: 0x000B4FAC
	public void QueDing()
	{
		this.player.zulinContorl.KZAddTime(this.screenName, 0, this.curMonth, 0);
		this.player.money -= (ulong)((long)(this.curMonth * this.price));
		if (SceneManager.GetActiveScene().name != this.screenName)
		{
			Tools.instance.loadMapScenes(this.screenName, true);
		}
		this.Close();
	}

	// Token: 0x0600199F RID: 6559 RVA: 0x000B6E28 File Offset: 0x000B5028
	public bool TryEscClose()
	{
		this.Close();
		return true;
	}

	// Token: 0x040014BD RID: 5309
	public static KeFangSelectTime inst;

	// Token: 0x040014BE RID: 5310
	[SerializeField]
	private Text needLingShi;

	// Token: 0x040014BF RID: 5311
	[SerializeField]
	private Text year;

	// Token: 0x040014C0 RID: 5312
	[SerializeField]
	private Text month;

	// Token: 0x040014C1 RID: 5313
	[SerializeField]
	private Slider slider;

	// Token: 0x040014C2 RID: 5314
	private int curMonth;

	// Token: 0x040014C3 RID: 5315
	private int maxMonth = 360;

	// Token: 0x040014C4 RID: 5316
	public int price = 10;

	// Token: 0x040014C5 RID: 5317
	public string screenName = "";

	// Token: 0x040014C6 RID: 5318
	public Avatar player;
}
