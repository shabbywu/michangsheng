using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

// Token: 0x020004DA RID: 1242
public class UCheckBox : MonoBehaviour
{
	// Token: 0x06002073 RID: 8307 RVA: 0x0001AACB File Offset: 0x00018CCB
	private void Awake()
	{
		if (UCheckBox.inst != null)
		{
			Object.Destroy(base.gameObject);
		}
		UCheckBox.inst = this;
		Object.DontDestroyOnLoad(base.gameObject);
	}

	// Token: 0x06002074 RID: 8308 RVA: 0x0011333C File Offset: 0x0011153C
	private void Update()
	{
		if (UCheckBox.needShow)
		{
			UCheckBox.IsShow = true;
			this.ShowText.text = UCheckBox.showText;
			this.OKBtn.onClick.RemoveAllListeners();
			if (UCheckBox.OKAction != null)
			{
				this.OKBtn.onClick.AddListener(UCheckBox.OKAction);
			}
			this.OKBtn.onClick.AddListener(new UnityAction(this.Close));
			base.transform.GetChild(0).gameObject.SetActive(true);
			UCheckBox.needShow = false;
		}
	}

	// Token: 0x06002075 RID: 8309 RVA: 0x0001AAF6 File Offset: 0x00018CF6
	private void Close()
	{
		UCheckBox.IsShow = false;
		base.transform.GetChild(0).gameObject.SetActive(false);
	}

	// Token: 0x06002076 RID: 8310 RVA: 0x0001AB15 File Offset: 0x00018D15
	public static void Show(string text, UnityAction onOK = null)
	{
		if (UCheckBox.inst == null)
		{
			Object.Instantiate<GameObject>(Resources.Load<GameObject>(UCheckBox.prefabPath));
		}
		UCheckBox.showText = text;
		UCheckBox.OKAction = onOK;
		UCheckBox.needShow = true;
	}

	// Token: 0x04001BE2 RID: 7138
	private static UCheckBox inst;

	// Token: 0x04001BE3 RID: 7139
	private static string prefabPath = "UCheckBox";

	// Token: 0x04001BE4 RID: 7140
	private static bool needShow;

	// Token: 0x04001BE5 RID: 7141
	private static string showText = "";

	// Token: 0x04001BE6 RID: 7142
	private static UnityAction OKAction;

	// Token: 0x04001BE7 RID: 7143
	public Text ShowText;

	// Token: 0x04001BE8 RID: 7144
	public Button OKBtn;

	// Token: 0x04001BE9 RID: 7145
	public static bool IsShow;
}
