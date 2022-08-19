using System;
using UnityEngine;

// Token: 0x020000BB RID: 187
public class AnimationSpeed : MonoBehaviour
{
	// Token: 0x06000AA4 RID: 2724 RVA: 0x00040880 File Offset: 0x0003EA80
	private void Start()
	{
		base.GetComponent<Animation>()[this.AniamName].speed = this.speed;
	}

	// Token: 0x040006AA RID: 1706
	public string AniamName;

	// Token: 0x040006AB RID: 1707
	public float speed = 1f;
}
