using System;
using UnityEngine;

// Token: 0x020005AB RID: 1451
public class selectNum : MonoBehaviour
{
	// Token: 0x170002CD RID: 717
	// (get) Token: 0x06002492 RID: 9362 RVA: 0x0001D63D File Offset: 0x0001B83D
	public static selectNum instence
	{
		get
		{
			return selectNum._instence;
		}
	}

	// Token: 0x06002493 RID: 9363 RVA: 0x0001D644 File Offset: 0x0001B844
	private void Awake()
	{
		selectNum._instence = this;
	}

	// Token: 0x06002494 RID: 9364 RVA: 0x00128F34 File Offset: 0x00127134
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

	// Token: 0x06002495 RID: 9365 RVA: 0x00017C2D File Offset: 0x00015E2D
	public void close()
	{
		base.gameObject.SetActive(false);
	}

	// Token: 0x06002496 RID: 9366 RVA: 0x0001D64C File Offset: 0x0001B84C
	public void open()
	{
		base.gameObject.SetActive(true);
		base.transform.localPosition = Vector3.up * -2000f;
		base.transform.localScale = Vector3.one;
	}

	// Token: 0x06002497 RID: 9367 RVA: 0x000042DD File Offset: 0x000024DD
	private void Start()
	{
	}

	// Token: 0x04001F6D RID: 8045
	public UIButton cancel;

	// Token: 0x04001F6E RID: 8046
	public UIButton ok;

	// Token: 0x04001F6F RID: 8047
	public UILabel label;

	// Token: 0x04001F70 RID: 8048
	private static selectNum _instence;
}
