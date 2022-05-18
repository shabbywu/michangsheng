using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000511 RID: 1297
public class TaskIndexCell : MonoBehaviour
{
	// Token: 0x0600216F RID: 8559 RVA: 0x001174E0 File Offset: 0x001156E0
	public void setContent(string str, bool isFinsh = false)
	{
		if (isFinsh)
		{
			this.content.color = new Color(0.6784314f, 0.52156866f, 0.3882353f);
			this.image.sprite = this.sprite;
		}
		this.content.text = str.STVarReplace().ToCN();
	}

	// Token: 0x04001CF5 RID: 7413
	[SerializeField]
	private Text content;

	// Token: 0x04001CF6 RID: 7414
	[SerializeField]
	private Image image;

	// Token: 0x04001CF7 RID: 7415
	[SerializeField]
	private Sprite sprite;
}
