using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x0200049E RID: 1182
public class MainUITooltip : MonoBehaviour
{
	// Token: 0x06001F77 RID: 8055 RVA: 0x0010EB4C File Offset: 0x0010CD4C
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

	// Token: 0x06001F78 RID: 8056 RVA: 0x00017C2D File Offset: 0x00015E2D
	public void Hide()
	{
		base.gameObject.SetActive(false);
	}

	// Token: 0x04001AE6 RID: 6886
	[SerializeField]
	private Text desc;

	// Token: 0x04001AE7 RID: 6887
	[SerializeField]
	private RectTransform rectTransform;

	// Token: 0x04001AE8 RID: 6888
	[SerializeField]
	private ContentSizeFitter childSizeFitter;

	// Token: 0x04001AE9 RID: 6889
	[SerializeField]
	private ContentSizeFitter sizeFitter;
}
