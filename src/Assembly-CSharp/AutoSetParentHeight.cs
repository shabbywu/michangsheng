using System;
using UnityEngine;

// Token: 0x0200050E RID: 1294
public class AutoSetParentHeight : MonoBehaviour
{
	// Token: 0x06002157 RID: 8535 RVA: 0x0001B7BF File Offset: 0x000199BF
	private void Start()
	{
		this.self = base.GetComponent<RectTransform>();
	}

	// Token: 0x06002158 RID: 8536 RVA: 0x0001B7CD File Offset: 0x000199CD
	private void Update()
	{
		this.self.sizeDelta = new Vector2(this.width, this.child.sizeDelta.y);
	}

	// Token: 0x04001CDF RID: 7391
	[SerializeField]
	private RectTransform child;

	// Token: 0x04001CE0 RID: 7392
	public float width;

	// Token: 0x04001CE1 RID: 7393
	private RectTransform self;
}
