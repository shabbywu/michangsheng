using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

// Token: 0x0200035E RID: 862
public class UCheckBox : MonoBehaviour
{
	// Token: 0x06001D0F RID: 7439 RVA: 0x000CE86C File Offset: 0x000CCA6C
	private void Awake()
	{
		if (UCheckBox.inst != null)
		{
			Object.Destroy(base.gameObject);
		}
		UCheckBox.inst = this;
		Object.DontDestroyOnLoad(base.gameObject);
	}

	// Token: 0x06001D10 RID: 7440 RVA: 0x000CE898 File Offset: 0x000CCA98
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

	// Token: 0x06001D11 RID: 7441 RVA: 0x000CE927 File Offset: 0x000CCB27
	private void Close()
	{
		UCheckBox.IsShow = false;
		base.transform.GetChild(0).gameObject.SetActive(false);
	}

	// Token: 0x06001D12 RID: 7442 RVA: 0x000CE946 File Offset: 0x000CCB46
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

	// Token: 0x04001793 RID: 6035
	private static UCheckBox inst;

	// Token: 0x04001794 RID: 6036
	private static string prefabPath = "UCheckBox";

	// Token: 0x04001795 RID: 6037
	private static bool needShow;

	// Token: 0x04001796 RID: 6038
	private static string showText = "";

	// Token: 0x04001797 RID: 6039
	private static UnityAction OKAction;

	// Token: 0x04001798 RID: 6040
	public Text ShowText;

	// Token: 0x04001799 RID: 6041
	public Button OKBtn;

	// Token: 0x0400179A RID: 6042
	public static bool IsShow;
}
