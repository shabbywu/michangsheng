using System;
using UnityEngine;

// Token: 0x020005AA RID: 1450
public class selectBox : MonoBehaviour, IESCClose
{
	// Token: 0x170002CC RID: 716
	// (get) Token: 0x06002486 RID: 9350 RVA: 0x0001D5D3 File Offset: 0x0001B7D3
	public static selectBox instence
	{
		get
		{
			return selectBox._instence;
		}
	}

	// Token: 0x06002487 RID: 9351 RVA: 0x0001D5DA File Offset: 0x0001B7DA
	private void Awake()
	{
		selectBox._instence = this;
	}

	// Token: 0x06002488 RID: 9352 RVA: 0x00128B7C File Offset: 0x00126D7C
	public void setChoice(string text, EventDelegate OK, EventDelegate Cancel)
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

	// Token: 0x06002489 RID: 9353 RVA: 0x00128C20 File Offset: 0x00126E20
	public void LianDanChoice(string text, EventDelegate OK, EventDelegate Cancel, Vector3 scale)
	{
		this.open();
		this.label.text = text;
		this.cancel.onClick.Clear();
		this.ok.onClick.Clear();
		this.cancel.onClick.Add(Cancel);
		this.ok.onClick.Add(OK);
		this.cancel.onClick.Add(new EventDelegate(new EventDelegate.Callback(this.close)));
		this.ok.onClick.Add(new EventDelegate(new EventDelegate.Callback(this.close)));
		base.transform.localScale = scale;
	}

	// Token: 0x0600248A RID: 9354 RVA: 0x00128CD0 File Offset: 0x00126ED0
	public void LianQiChoice(string text, EventDelegate OK, EventDelegate Cancel, Vector3 scale)
	{
		this.open();
		this.label.text = text;
		this.cancel.onClick.Clear();
		this.ok.onClick.Clear();
		this.cancel.onClick.Add(Cancel);
		this.ok.onClick.Add(new EventDelegate(new EventDelegate.Callback(this.close)));
		this.ok.onClick.Add(OK);
		this.cancel.onClick.Add(new EventDelegate(new EventDelegate.Callback(this.close)));
		base.transform.localScale = scale;
	}

	// Token: 0x0600248B RID: 9355 RVA: 0x00128D80 File Offset: 0x00126F80
	public void LianQiResult(string text, EventDelegate OK, EventDelegate Cancel, Vector3 scale)
	{
		this.open();
		this.label.text = text;
		this.lianQiOK.onClick.Clear();
		this.lianQiOK.onClick.Add(OK);
		this.lianQiOK.onClick.Add(new EventDelegate(new EventDelegate.Callback(this.close)));
		this.cancel.onClick.Clear();
		this.ok.gameObject.SetActive(false);
		this.cancel.gameObject.SetActive(false);
		this.lianQiOK.gameObject.SetActive(true);
		base.transform.localScale = scale;
	}

	// Token: 0x0600248C RID: 9356 RVA: 0x0001D5E2 File Offset: 0x0001B7E2
	public void setBtnBackSprite(string canceName, string OkName)
	{
		this.cancel.GetComponent<UISprite>().spriteName = canceName;
		this.ok.GetComponent<UISprite>().spriteName = OkName;
		this.cancel.normalSprite = canceName;
		this.ok.normalSprite = OkName;
	}

	// Token: 0x0600248D RID: 9357 RVA: 0x0001D61E File Offset: 0x0001B81E
	public void close()
	{
		Tools.canClickFlag = true;
		base.gameObject.SetActive(false);
		ESCCloseManager.Inst.UnRegisterClose(this);
	}

	// Token: 0x0600248E RID: 9358 RVA: 0x00128E34 File Offset: 0x00127034
	public void open()
	{
		Tools.canClickFlag = false;
		base.gameObject.SetActive(true);
		base.transform.localPosition = Vector3.up * -2000f;
		base.transform.localScale = Vector3.one;
		this.cancel.gameObject.SetActive(true);
		this.ok.gameObject.SetActive(true);
		if (this.lianQiOK != null)
		{
			this.lianQiOK.gameObject.SetActive(false);
		}
		ESCCloseManager.Inst.RegisterClose(this);
	}

	// Token: 0x0600248F RID: 9359 RVA: 0x000042DD File Offset: 0x000024DD
	private void Start()
	{
	}

	// Token: 0x06002490 RID: 9360 RVA: 0x00128ECC File Offset: 0x001270CC
	public bool TryEscClose()
	{
		if (this.cancel.gameObject.activeInHierarchy)
		{
			int count = this.cancel.onClick.Count;
			for (int i = 0; i < count; i++)
			{
				if (this.cancel.onClick[i] != null)
				{
					this.cancel.onClick[i].Execute();
				}
			}
		}
		return true;
	}

	// Token: 0x04001F68 RID: 8040
	public UIButton cancel;

	// Token: 0x04001F69 RID: 8041
	public UIButton ok;

	// Token: 0x04001F6A RID: 8042
	public UILabel label;

	// Token: 0x04001F6B RID: 8043
	public UIButton lianQiOK;

	// Token: 0x04001F6C RID: 8044
	public static selectBox _instence;
}
