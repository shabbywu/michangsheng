using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x0200031A RID: 794
public class LunDaoSayWord : MonoBehaviour
{
	// Token: 0x06001B8D RID: 7053 RVA: 0x000C4316 File Offset: 0x000C2516
	public void Say(string msg)
	{
		this.content.text = msg;
		base.Invoke("Show", 0.1f);
	}

	// Token: 0x06001B8E RID: 7054 RVA: 0x000C4334 File Offset: 0x000C2534
	private void Show()
	{
		base.gameObject.SetActive(true);
		base.Invoke("Hide", 2f);
	}

	// Token: 0x06001B8F RID: 7055 RVA: 0x000B5E62 File Offset: 0x000B4062
	public void Hide()
	{
		base.gameObject.SetActive(false);
	}

	// Token: 0x06001B90 RID: 7056 RVA: 0x000C4352 File Offset: 0x000C2552
	private void Awake()
	{
		this.contentRectTransform = this.content.gameObject.GetComponent<RectTransform>();
	}

	// Token: 0x06001B91 RID: 7057 RVA: 0x000C436A File Offset: 0x000C256A
	private void Update()
	{
		this.bg.sizeDelta = new Vector2(334f, this.contentRectTransform.sizeDelta.y + 20f);
	}

	// Token: 0x0400160E RID: 5646
	[SerializeField]
	private RectTransform bg;

	// Token: 0x0400160F RID: 5647
	private RectTransform contentRectTransform;

	// Token: 0x04001610 RID: 5648
	[SerializeField]
	private Text content;
}
