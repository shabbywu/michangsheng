using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

// Token: 0x0200035D RID: 861
public class UBigCheckBox : MonoBehaviour
{
	// Token: 0x06001D09 RID: 7433 RVA: 0x000CE739 File Offset: 0x000CC939
	private void Awake()
	{
		if (UBigCheckBox.inst != null)
		{
			Object.Destroy(base.gameObject);
		}
		UBigCheckBox.inst = this;
		Object.DontDestroyOnLoad(base.gameObject);
	}

	// Token: 0x06001D0A RID: 7434 RVA: 0x000CE764 File Offset: 0x000CC964
	private void Update()
	{
		if (UBigCheckBox.needShow)
		{
			UBigCheckBox.IsShow = true;
			this.ShowText.text = UBigCheckBox.showText;
			this.OKBtn.onClick.RemoveAllListeners();
			if (UBigCheckBox.OKAction != null)
			{
				this.OKBtn.onClick.AddListener(UBigCheckBox.OKAction);
			}
			this.OKBtn.onClick.AddListener(new UnityAction(this.Close));
			base.transform.GetChild(0).gameObject.SetActive(true);
			this.ContentRT.anchoredPosition = Vector2.zero;
			UBigCheckBox.needShow = false;
		}
	}

	// Token: 0x06001D0B RID: 7435 RVA: 0x000CE806 File Offset: 0x000CCA06
	private void Close()
	{
		UBigCheckBox.IsShow = false;
		base.transform.GetChild(0).gameObject.SetActive(false);
	}

	// Token: 0x06001D0C RID: 7436 RVA: 0x000CE825 File Offset: 0x000CCA25
	public static void Show(string text, UnityAction onOK = null)
	{
		if (UBigCheckBox.inst == null)
		{
			Object.Instantiate<GameObject>(Resources.Load<GameObject>(UBigCheckBox.prefabPath));
		}
		UBigCheckBox.showText = text;
		UBigCheckBox.OKAction = onOK;
		UBigCheckBox.needShow = true;
	}

	// Token: 0x0400178A RID: 6026
	private static UBigCheckBox inst;

	// Token: 0x0400178B RID: 6027
	private static string prefabPath = "UBigCheckBox";

	// Token: 0x0400178C RID: 6028
	private static bool needShow;

	// Token: 0x0400178D RID: 6029
	private static string showText = "";

	// Token: 0x0400178E RID: 6030
	private static UnityAction OKAction;

	// Token: 0x0400178F RID: 6031
	public Text ShowText;

	// Token: 0x04001790 RID: 6032
	public Button OKBtn;

	// Token: 0x04001791 RID: 6033
	public RectTransform ContentRT;

	// Token: 0x04001792 RID: 6034
	public static bool IsShow;
}
