using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x0200038C RID: 908
public class TaskIndexCell : MonoBehaviour
{
	// Token: 0x06001DF4 RID: 7668 RVA: 0x000D3884 File Offset: 0x000D1A84
	public void setContent(string str, bool isFinsh = false)
	{
		if (isFinsh)
		{
			this.content.color = new Color(0.6784314f, 0.52156866f, 0.3882353f);
			this.image.sprite = this.sprite;
		}
		this.content.text = str.STVarReplace().ToCN();
	}

	// Token: 0x04001895 RID: 6293
	[SerializeField]
	private Text content;

	// Token: 0x04001896 RID: 6294
	[SerializeField]
	private Image image;

	// Token: 0x04001897 RID: 6295
	[SerializeField]
	private Sprite sprite;
}
