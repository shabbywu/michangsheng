using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x0200035F RID: 863
[ExecuteAlways]
public class UIAutoSize : MonoBehaviour
{
	// Token: 0x06001D15 RID: 7445 RVA: 0x000CE98D File Offset: 0x000CCB8D
	private void Awake()
	{
		this.layoutElement = base.GetComponent<ILayoutElement>();
		this.rectTransform = base.GetComponent<RectTransform>();
	}

	// Token: 0x06001D16 RID: 7446 RVA: 0x000CE9A8 File Offset: 0x000CCBA8
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

	// Token: 0x0400179B RID: 6043
	public bool AutoWidth;

	// Token: 0x0400179C RID: 6044
	public bool AutoHeight;

	// Token: 0x0400179D RID: 6045
	private ILayoutElement layoutElement;

	// Token: 0x0400179E RID: 6046
	private RectTransform rectTransform;
}
