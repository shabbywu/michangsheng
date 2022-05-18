using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

// Token: 0x020004E5 RID: 1253
public class USelectBox : MonoBehaviour, IESCClose
{
	// Token: 0x060020A3 RID: 8355 RVA: 0x0001AD5D File Offset: 0x00018F5D
	private void Awake()
	{
		if (USelectBox.inst != null)
		{
			Object.Destroy(base.gameObject);
		}
		USelectBox.inst = this;
		Object.DontDestroyOnLoad(base.gameObject);
	}

	// Token: 0x060020A4 RID: 8356 RVA: 0x00113A5C File Offset: 0x00111C5C
	private void Update()
	{
		if (USelectBox.needShow)
		{
			USelectBox.IsShow = true;
			this.ShowText.text = USelectBox.showText;
			this.OKBtn.onClick.RemoveAllListeners();
			if (USelectBox.OKAction != null)
			{
				this.OKBtn.onClick.AddListener(USelectBox.OKAction);
			}
			this.OKBtn.onClick.AddListener(new UnityAction(this.Close));
			this.CloseBtn.onClick.RemoveAllListeners();
			if (USelectBox.CloseAction != null)
			{
				this.CloseBtn.onClick.AddListener(USelectBox.CloseAction);
			}
			this.CloseBtn.onClick.AddListener(new UnityAction(this.Close));
			base.transform.GetChild(0).gameObject.SetActive(true);
			USelectBox.needShow = false;
			ESCCloseManager.Inst.RegisterClose(this);
		}
	}

	// Token: 0x060020A5 RID: 8357 RVA: 0x0001AD88 File Offset: 0x00018F88
	private void Close()
	{
		USelectBox.IsShow = false;
		base.transform.GetChild(0).gameObject.SetActive(false);
		ESCCloseManager.Inst.UnRegisterClose(this);
	}

	// Token: 0x060020A6 RID: 8358 RVA: 0x0001ADB2 File Offset: 0x00018FB2
	public static void Show(string text, UnityAction onOK = null, UnityAction onClose = null)
	{
		if (USelectBox.inst == null)
		{
			Object.Instantiate<GameObject>(Resources.Load<GameObject>(USelectBox.prefabPath));
		}
		USelectBox.showText = text;
		USelectBox.OKAction = onOK;
		USelectBox.CloseAction = onClose;
		USelectBox.needShow = true;
	}

	// Token: 0x060020A7 RID: 8359 RVA: 0x0001ADE9 File Offset: 0x00018FE9
	public bool TryEscClose()
	{
		this.CloseBtn.onClick.Invoke();
		return true;
	}

	// Token: 0x04001C21 RID: 7201
	private static USelectBox inst;

	// Token: 0x04001C22 RID: 7202
	private static string prefabPath = "USelectBox";

	// Token: 0x04001C23 RID: 7203
	private static bool needShow;

	// Token: 0x04001C24 RID: 7204
	private static string showText = "";

	// Token: 0x04001C25 RID: 7205
	private static UnityAction OKAction;

	// Token: 0x04001C26 RID: 7206
	private static UnityAction CloseAction;

	// Token: 0x04001C27 RID: 7207
	public Text ShowText;

	// Token: 0x04001C28 RID: 7208
	public Button CloseBtn;

	// Token: 0x04001C29 RID: 7209
	public Button OKBtn;

	// Token: 0x04001C2A RID: 7210
	public static bool IsShow;
}
