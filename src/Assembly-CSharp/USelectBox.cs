using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

// Token: 0x02000368 RID: 872
public class USelectBox : MonoBehaviour, IESCClose
{
	// Token: 0x06001D39 RID: 7481 RVA: 0x000CF186 File Offset: 0x000CD386
	private void Awake()
	{
		if (USelectBox.inst != null)
		{
			Object.Destroy(base.gameObject);
		}
		USelectBox.inst = this;
		Object.DontDestroyOnLoad(base.gameObject);
	}

	// Token: 0x06001D3A RID: 7482 RVA: 0x000CF1B4 File Offset: 0x000CD3B4
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

	// Token: 0x06001D3B RID: 7483 RVA: 0x000CF299 File Offset: 0x000CD499
	private void Close()
	{
		USelectBox.IsShow = false;
		base.transform.GetChild(0).gameObject.SetActive(false);
		ESCCloseManager.Inst.UnRegisterClose(this);
	}

	// Token: 0x06001D3C RID: 7484 RVA: 0x000CF2C3 File Offset: 0x000CD4C3
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

	// Token: 0x06001D3D RID: 7485 RVA: 0x000CF2FA File Offset: 0x000CD4FA
	public bool TryEscClose()
	{
		this.CloseBtn.onClick.Invoke();
		return true;
	}

	// Token: 0x040017CF RID: 6095
	private static USelectBox inst;

	// Token: 0x040017D0 RID: 6096
	private static string prefabPath = "USelectBox";

	// Token: 0x040017D1 RID: 6097
	private static bool needShow;

	// Token: 0x040017D2 RID: 6098
	private static string showText = "";

	// Token: 0x040017D3 RID: 6099
	private static UnityAction OKAction;

	// Token: 0x040017D4 RID: 6100
	private static UnityAction CloseAction;

	// Token: 0x040017D5 RID: 6101
	public Text ShowText;

	// Token: 0x040017D6 RID: 6102
	public Button CloseBtn;

	// Token: 0x040017D7 RID: 6103
	public Button OKBtn;

	// Token: 0x040017D8 RID: 6104
	public static bool IsShow;
}
