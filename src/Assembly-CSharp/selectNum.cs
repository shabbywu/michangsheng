using System;
using UnityEngine;

// Token: 0x020003FB RID: 1019
public class selectNum : MonoBehaviour
{
	// Token: 0x17000283 RID: 643
	// (get) Token: 0x060020E0 RID: 8416 RVA: 0x000E6EDB File Offset: 0x000E50DB
	public static selectNum instence
	{
		get
		{
			return selectNum._instence;
		}
	}

	// Token: 0x060020E1 RID: 8417 RVA: 0x000E6EE2 File Offset: 0x000E50E2
	private void Awake()
	{
		selectNum._instence = this;
	}

	// Token: 0x060020E2 RID: 8418 RVA: 0x000E6EEC File Offset: 0x000E50EC
	public void setChoice(EventDelegate OK, EventDelegate Cancel, string text = "选择数量")
	{
		this.open();
		this.label.text = text;
		this.cancel.onClick.Clear();
		this.ok.onClick.Clear();
		this.cancel.onClick.Add(Cancel);
		this.ok.onClick.Add(OK);
		this.cancel.onClick.Add(new EventDelegate(new EventDelegate.Callback(this.close)));
		this.ok.onClick.Add(new EventDelegate(new EventDelegate.Callback(this.close)));
	}

	// Token: 0x060020E3 RID: 8419 RVA: 0x000B5E62 File Offset: 0x000B4062
	public void close()
	{
		base.gameObject.SetActive(false);
	}

	// Token: 0x060020E4 RID: 8420 RVA: 0x000E6F8F File Offset: 0x000E518F
	public void open()
	{
		base.gameObject.SetActive(true);
		base.transform.localPosition = Vector3.up * -2000f;
		base.transform.localScale = Vector3.one;
	}

	// Token: 0x060020E5 RID: 8421 RVA: 0x00004095 File Offset: 0x00002295
	private void Start()
	{
	}

	// Token: 0x04001AB1 RID: 6833
	public UIButton cancel;

	// Token: 0x04001AB2 RID: 6834
	public UIButton ok;

	// Token: 0x04001AB3 RID: 6835
	public UILabel label;

	// Token: 0x04001AB4 RID: 6836
	private static selectNum _instence;
}
