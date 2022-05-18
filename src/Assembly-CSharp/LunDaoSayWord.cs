using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x0200047D RID: 1149
public class LunDaoSayWord : MonoBehaviour
{
	// Token: 0x06001EC0 RID: 7872 RVA: 0x000197FB File Offset: 0x000179FB
	public void Say(string msg)
	{
		this.content.text = msg;
		base.Invoke("Show", 0.1f);
	}

	// Token: 0x06001EC1 RID: 7873 RVA: 0x00019819 File Offset: 0x00017A19
	private void Show()
	{
		base.gameObject.SetActive(true);
		base.Invoke("Hide", 2f);
	}

	// Token: 0x06001EC2 RID: 7874 RVA: 0x00017C2D File Offset: 0x00015E2D
	public void Hide()
	{
		base.gameObject.SetActive(false);
	}

	// Token: 0x06001EC3 RID: 7875 RVA: 0x00019837 File Offset: 0x00017A37
	private void Awake()
	{
		this.contentRectTransform = this.content.gameObject.GetComponent<RectTransform>();
	}

	// Token: 0x06001EC4 RID: 7876 RVA: 0x0001984F File Offset: 0x00017A4F
	private void Update()
	{
		this.bg.sizeDelta = new Vector2(334f, this.contentRectTransform.sizeDelta.y + 20f);
	}

	// Token: 0x04001A29 RID: 6697
	[SerializeField]
	private RectTransform bg;

	// Token: 0x04001A2A RID: 6698
	private RectTransform contentRectTransform;

	// Token: 0x04001A2B RID: 6699
	[SerializeField]
	private Text content;
}
