using System;
using UnityEngine;

// Token: 0x0200038A RID: 906
public class AutoSetParentHeight : MonoBehaviour
{
	// Token: 0x06001DDE RID: 7646 RVA: 0x000D27CB File Offset: 0x000D09CB
	private void Start()
	{
		this.self = base.GetComponent<RectTransform>();
	}

	// Token: 0x06001DDF RID: 7647 RVA: 0x000D27D9 File Offset: 0x000D09D9
	private void Update()
	{
		this.self.sizeDelta = new Vector2(this.width, this.child.sizeDelta.y);
	}

	// Token: 0x04001881 RID: 6273
	[SerializeField]
	private RectTransform child;

	// Token: 0x04001882 RID: 6274
	public float width;

	// Token: 0x04001883 RID: 6275
	private RectTransform self;
}
