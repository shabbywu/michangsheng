using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020004C9 RID: 1225
[RequireComponent(typeof(Text))]
public class AutoSetTextHeight : MonoBehaviour
{
	// Token: 0x0600202E RID: 8238 RVA: 0x0001A647 File Offset: 0x00018847
	private void Start()
	{
		this.self = base.GetComponent<RectTransform>();
		this.myText = base.GetComponent<Text>();
	}

	// Token: 0x0600202F RID: 8239 RVA: 0x0001A661 File Offset: 0x00018861
	private void Update()
	{
		this.self.sizeDelta = new Vector2(this.Width, this.myText.preferredHeight + this.ExHeight);
	}

	// Token: 0x04001BA8 RID: 7080
	[Header("宽度")]
	public float Width;

	// Token: 0x04001BA9 RID: 7081
	[Header("额外高度")]
	public float ExHeight;

	// Token: 0x04001BAA RID: 7082
	private RectTransform self;

	// Token: 0x04001BAB RID: 7083
	private Text myText;
}
