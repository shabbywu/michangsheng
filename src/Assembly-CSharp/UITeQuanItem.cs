using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000507 RID: 1287
public class UITeQuanItem : MonoBehaviour
{
	// Token: 0x06002139 RID: 8505 RVA: 0x0001B5FB File Offset: 0x000197FB
	public void SetText(string text)
	{
		this.LockImage.gameObject.SetActive(false);
		this.TeQuanText.text = text;
		this.TeQuanText.color = UITeQuanItem.c1;
	}

	// Token: 0x0600213A RID: 8506 RVA: 0x0001B62A File Offset: 0x0001982A
	public void SetLockText(string text)
	{
		this.LockImage.gameObject.SetActive(true);
		this.TeQuanText.text = text;
		this.TeQuanText.color = UITeQuanItem.c2;
	}

	// Token: 0x04001CB9 RID: 7353
	public Image LockImage;

	// Token: 0x04001CBA RID: 7354
	public Text TeQuanText;

	// Token: 0x04001CBB RID: 7355
	private static Color c1 = new Color(0.92941177f, 0.75686276f, 0.5176471f);

	// Token: 0x04001CBC RID: 7356
	private static Color c2 = new Color(0.4627451f, 0.34117648f, 0.2627451f);
}
