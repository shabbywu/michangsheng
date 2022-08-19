using System;
using UnityEngine;

// Token: 0x020003FA RID: 1018
public class selectBox : MonoBehaviour, IESCClose
{
	// Token: 0x17000282 RID: 642
	// (get) Token: 0x060020D4 RID: 8404 RVA: 0x000E6ABC File Offset: 0x000E4CBC
	public static selectBox instence
	{
		get
		{
			return selectBox._instence;
		}
	}

	// Token: 0x060020D5 RID: 8405 RVA: 0x000E6AC3 File Offset: 0x000E4CC3
	private void Awake()
	{
		selectBox._instence = this;
	}

	// Token: 0x060020D6 RID: 8406 RVA: 0x000E6ACC File Offset: 0x000E4CCC
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

	// Token: 0x060020D7 RID: 8407 RVA: 0x000E6B70 File Offset: 0x000E4D70
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

	// Token: 0x060020D8 RID: 8408 RVA: 0x000E6C20 File Offset: 0x000E4E20
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

	// Token: 0x060020D9 RID: 8409 RVA: 0x000E6CD0 File Offset: 0x000E4ED0
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

	// Token: 0x060020DA RID: 8410 RVA: 0x000E6D81 File Offset: 0x000E4F81
	public void setBtnBackSprite(string canceName, string OkName)
	{
		this.cancel.GetComponent<UISprite>().spriteName = canceName;
		this.ok.GetComponent<UISprite>().spriteName = OkName;
		this.cancel.normalSprite = canceName;
		this.ok.normalSprite = OkName;
	}

	// Token: 0x060020DB RID: 8411 RVA: 0x000E6DBD File Offset: 0x000E4FBD
	public void close()
	{
		Tools.canClickFlag = true;
		base.gameObject.SetActive(false);
		ESCCloseManager.Inst.UnRegisterClose(this);
	}

	// Token: 0x060020DC RID: 8412 RVA: 0x000E6DDC File Offset: 0x000E4FDC
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

	// Token: 0x060020DD RID: 8413 RVA: 0x00004095 File Offset: 0x00002295
	private void Start()
	{
	}

	// Token: 0x060020DE RID: 8414 RVA: 0x000E6E74 File Offset: 0x000E5074
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

	// Token: 0x04001AAC RID: 6828
	public UIButton cancel;

	// Token: 0x04001AAD RID: 6829
	public UIButton ok;

	// Token: 0x04001AAE RID: 6830
	public UILabel label;

	// Token: 0x04001AAF RID: 6831
	public UIButton lianQiOK;

	// Token: 0x04001AB0 RID: 6832
	public static selectBox _instence;
}
