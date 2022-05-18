using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

// Token: 0x020004DE RID: 1246
public class UInputBox : MonoBehaviour
{
	// Token: 0x06002087 RID: 8327 RVA: 0x0001ABC6 File Offset: 0x00018DC6
	private void Awake()
	{
		if (UInputBox.inst != null)
		{
			Object.Destroy(base.gameObject);
		}
		UInputBox.inst = this;
		Object.DontDestroyOnLoad(base.gameObject);
	}

	// Token: 0x06002088 RID: 8328 RVA: 0x00113528 File Offset: 0x00111728
	private void Update()
	{
		if (UInputBox.needShow)
		{
			UInputBox.needShow = false;
			this.ShowText.text = UInputBox.showText;
			this.TextInputField.text = "";
			this.OKBtn.onClick.RemoveAllListeners();
			if (UInputBox.OKAction != null)
			{
				this.OKBtn.onClick.AddListener(delegate()
				{
					UInputBox.OKAction.Invoke(this.TextInputField.text);
				});
			}
			this.OKBtn.onClick.AddListener(new UnityAction(this.Close));
			base.transform.GetChild(0).gameObject.SetActive(true);
			UInputBox.IsShow = true;
		}
	}

	// Token: 0x06002089 RID: 8329 RVA: 0x0001ABF1 File Offset: 0x00018DF1
	private void Close()
	{
		UInputBox.IsShow = false;
		base.transform.GetChild(0).gameObject.SetActive(false);
	}

	// Token: 0x0600208A RID: 8330 RVA: 0x0001AC10 File Offset: 0x00018E10
	public static void Show(string text, UnityAction<string> onOK = null)
	{
		if (UInputBox.inst == null)
		{
			Object.Instantiate<GameObject>(Resources.Load<GameObject>(UInputBox.prefabPath));
		}
		UInputBox.showText = text;
		UInputBox.OKAction = onOK;
		UInputBox.needShow = true;
	}

	// Token: 0x04001BF5 RID: 7157
	private static UInputBox inst;

	// Token: 0x04001BF6 RID: 7158
	public static bool IsShow;

	// Token: 0x04001BF7 RID: 7159
	private static string prefabPath = "UInputBox";

	// Token: 0x04001BF8 RID: 7160
	private static bool needShow;

	// Token: 0x04001BF9 RID: 7161
	private static string showText = "";

	// Token: 0x04001BFA RID: 7162
	private static UnityAction<string> OKAction;

	// Token: 0x04001BFB RID: 7163
	public Text ShowText;

	// Token: 0x04001BFC RID: 7164
	public InputField TextInputField;

	// Token: 0x04001BFD RID: 7165
	public Button CloseBtn;

	// Token: 0x04001BFE RID: 7166
	public Button OKBtn;
}
