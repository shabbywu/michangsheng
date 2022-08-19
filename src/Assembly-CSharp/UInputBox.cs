using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

// Token: 0x02000361 RID: 865
public class UInputBox : MonoBehaviour
{
	// Token: 0x06001D1D RID: 7453 RVA: 0x000CEABE File Offset: 0x000CCCBE
	private void Awake()
	{
		if (UInputBox.Inst != null)
		{
			Object.Destroy(base.gameObject);
		}
		UInputBox.Inst = this;
		Object.DontDestroyOnLoad(base.gameObject);
	}

	// Token: 0x06001D1E RID: 7454 RVA: 0x000CEAEC File Offset: 0x000CCCEC
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

	// Token: 0x06001D1F RID: 7455 RVA: 0x000CEB95 File Offset: 0x000CCD95
	private void Close()
	{
		UInputBox.IsShow = false;
		base.transform.GetChild(0).gameObject.SetActive(false);
	}

	// Token: 0x06001D20 RID: 7456 RVA: 0x000CEBB4 File Offset: 0x000CCDB4
	public static void Show(string text, UnityAction<string> onOK = null)
	{
		if (UInputBox.Inst == null)
		{
			Object.Instantiate<GameObject>(Resources.Load<GameObject>(UInputBox.prefabPath));
		}
		UInputBox.showText = text;
		UInputBox.OKAction = onOK;
		UInputBox.needShow = true;
	}

	// Token: 0x040017A3 RID: 6051
	public static UInputBox Inst;

	// Token: 0x040017A4 RID: 6052
	public static bool IsShow;

	// Token: 0x040017A5 RID: 6053
	private static string prefabPath = "UInputBox";

	// Token: 0x040017A6 RID: 6054
	private static bool needShow;

	// Token: 0x040017A7 RID: 6055
	private static string showText = "";

	// Token: 0x040017A8 RID: 6056
	private static UnityAction<string> OKAction;

	// Token: 0x040017A9 RID: 6057
	public Text ShowText;

	// Token: 0x040017AA RID: 6058
	public InputField TextInputField;

	// Token: 0x040017AB RID: 6059
	public Button CloseBtn;

	// Token: 0x040017AC RID: 6060
	public Button OKBtn;
}
