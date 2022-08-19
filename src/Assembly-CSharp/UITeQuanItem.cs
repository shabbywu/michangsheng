using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000384 RID: 900
public class UITeQuanItem : MonoBehaviour
{
	// Token: 0x06001DC0 RID: 7616 RVA: 0x000D1F10 File Offset: 0x000D0110
	public void SetText(string text)
	{
		this.LockImage.gameObject.SetActive(false);
		this.TeQuanText.text = text;
		this.TeQuanText.color = UITeQuanItem.c1;
	}

	// Token: 0x06001DC1 RID: 7617 RVA: 0x000D1F3F File Offset: 0x000D013F
	public void SetLockText(string text)
	{
		this.LockImage.gameObject.SetActive(true);
		this.TeQuanText.text = text;
		this.TeQuanText.color = UITeQuanItem.c2;
	}

	// Token: 0x0400185E RID: 6238
	public Image LockImage;

	// Token: 0x0400185F RID: 6239
	public Text TeQuanText;

	// Token: 0x04001860 RID: 6240
	private static Color c1 = new Color(0.92941177f, 0.75686276f, 0.5176471f);

	// Token: 0x04001861 RID: 6241
	private static Color c2 = new Color(0.4627451f, 0.34117648f, 0.2627451f);
}
