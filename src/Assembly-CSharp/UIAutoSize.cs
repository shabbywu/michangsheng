using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020004DB RID: 1243
[ExecuteAlways]
public class UIAutoSize : MonoBehaviour
{
	// Token: 0x06002079 RID: 8313 RVA: 0x0001AB5C File Offset: 0x00018D5C
	private void Awake()
	{
		this.layoutElement = base.GetComponent<ILayoutElement>();
		this.rectTransform = base.GetComponent<RectTransform>();
	}

	// Token: 0x0600207A RID: 8314 RVA: 0x001133CC File Offset: 0x001115CC
	private void Update()
	{
		if (this.layoutElement != null)
		{
			if (this.AutoWidth && this.rectTransform.sizeDelta.x != this.layoutElement.preferredWidth)
			{
				this.rectTransform.SetSizeWithCurrentAnchors(0, this.layoutElement.preferredWidth);
			}
			if (this.AutoHeight && this.rectTransform.sizeDelta.y != this.layoutElement.preferredHeight)
			{
				this.rectTransform.SetSizeWithCurrentAnchors(1, this.layoutElement.preferredHeight);
			}
		}
	}

	// Token: 0x04001BEA RID: 7146
	public bool AutoWidth;

	// Token: 0x04001BEB RID: 7147
	public bool AutoHeight;

	// Token: 0x04001BEC RID: 7148
	private ILayoutElement layoutElement;

	// Token: 0x04001BED RID: 7149
	private RectTransform rectTransform;
}
