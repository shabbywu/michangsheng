using System;
using KBEngine;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// Token: 0x02000434 RID: 1076
public class KeFangSelectTime : MonoBehaviour, IESCClose
{
	// Token: 0x06001CB0 RID: 7344 RVA: 0x000FC880 File Offset: 0x000FAA80
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

	// Token: 0x06001CB1 RID: 7345 RVA: 0x000FC920 File Offset: 0x000FAB20
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

	// Token: 0x06001CB2 RID: 7346 RVA: 0x00018041 File Offset: 0x00016241
	public void OnDragSlider(float data)
	{
		this.curMonth = (int)(data * (float)this.maxMonth);
		if (this.curMonth < 1)
		{
			this.curMonth = 1;
		}
		this.updateData();
	}

	// Token: 0x06001CB3 RID: 7347 RVA: 0x000FC9AC File Offset: 0x000FABAC
	private void updateData()
	{
		this.year.text = (this.curMonth / 12).ToString();
		this.month.text = (this.curMonth % 12).ToString();
		this.needLingShi.text = (this.curMonth * this.price).ToString();
	}

	// Token: 0x06001CB4 RID: 7348 RVA: 0x000FCA14 File Offset: 0x000FAC14
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

	// Token: 0x06001CB5 RID: 7349 RVA: 0x00018069 File Offset: 0x00016269
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

	// Token: 0x06001CB6 RID: 7350 RVA: 0x000180A9 File Offset: 0x000162A9
	public void Close()
	{
		KeFangSelectTime.inst = null;
		ESCCloseManager.Inst.UnRegisterClose(this);
		Object.Destroy(base.gameObject);
	}

	// Token: 0x06001CB7 RID: 7351 RVA: 0x000FCA6C File Offset: 0x000FAC6C
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

	// Token: 0x06001CB8 RID: 7352 RVA: 0x000180C7 File Offset: 0x000162C7
	public bool TryEscClose()
	{
		this.Close();
		return true;
	}

	// Token: 0x040018A0 RID: 6304
	public static KeFangSelectTime inst;

	// Token: 0x040018A1 RID: 6305
	[SerializeField]
	private Text needLingShi;

	// Token: 0x040018A2 RID: 6306
	[SerializeField]
	private Text year;

	// Token: 0x040018A3 RID: 6307
	[SerializeField]
	private Text month;

	// Token: 0x040018A4 RID: 6308
	[SerializeField]
	private Slider slider;

	// Token: 0x040018A5 RID: 6309
	private int curMonth;

	// Token: 0x040018A6 RID: 6310
	private int maxMonth = 360;

	// Token: 0x040018A7 RID: 6311
	public int price = 10;

	// Token: 0x040018A8 RID: 6312
	public string screenName = "";

	// Token: 0x040018A9 RID: 6313
	public Avatar player;
}
