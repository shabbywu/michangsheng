using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000330 RID: 816
public class MainUITooltip : MonoBehaviour
{
	// Token: 0x06001C25 RID: 7205 RVA: 0x000C97C8 File Offset: 0x000C79C8
	public void Show(string content, Vector3 vector3)
	{
		base.gameObject.SetActive(true);
		this.desc.text = content;
		base.transform.position = vector3;
		this.childSizeFitter.SetLayoutHorizontal();
		this.sizeFitter.SetLayoutHorizontal();
		this.sizeFitter.SetLayoutVertical();
		LayoutRebuilder.ForceRebuildLayoutImmediate(this.rectTransform);
		base.transform.localPosition = new Vector3(base.transform.localPosition.x - this.rectTransform.rect.width / 2f, base.transform.localPosition.y, base.transform.localPosition.z);
	}

	// Token: 0x06001C26 RID: 7206 RVA: 0x000B5E62 File Offset: 0x000B4062
	public void Hide()
	{
		base.gameObject.SetActive(false);
	}

	// Token: 0x040016B0 RID: 5808
	[SerializeField]
	private Text desc;

	// Token: 0x040016B1 RID: 5809
	[SerializeField]
	private RectTransform rectTransform;

	// Token: 0x040016B2 RID: 5810
	[SerializeField]
	private ContentSizeFitter childSizeFitter;

	// Token: 0x040016B3 RID: 5811
	[SerializeField]
	private ContentSizeFitter sizeFitter;
}
