using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x0200034D RID: 845
[RequireComponent(typeof(Text))]
public class AutoSetTextHeight : MonoBehaviour
{
	// Token: 0x06001CC7 RID: 7367 RVA: 0x000CDCC1 File Offset: 0x000CBEC1
	private void Start()
	{
		this.self = base.GetComponent<RectTransform>();
		this.myText = base.GetComponent<Text>();
	}

	// Token: 0x06001CC8 RID: 7368 RVA: 0x000CDCDB File Offset: 0x000CBEDB
	private void Update()
	{
		this.self.sizeDelta = new Vector2(this.Width, this.myText.preferredHeight + this.ExHeight);
	}

	// Token: 0x04001752 RID: 5970
	[Header("宽度")]
	public float Width;

	// Token: 0x04001753 RID: 5971
	[Header("额外高度")]
	public float ExHeight;

	// Token: 0x04001754 RID: 5972
	private RectTransform self;

	// Token: 0x04001755 RID: 5973
	private Text myText;
}
