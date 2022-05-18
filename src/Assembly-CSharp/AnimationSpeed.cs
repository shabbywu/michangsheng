using System;
using UnityEngine;

// Token: 0x0200012B RID: 299
public class AnimationSpeed : MonoBehaviour
{
	// Token: 0x06000B87 RID: 2951 RVA: 0x0000D918 File Offset: 0x0000BB18
	private void Start()
	{
		base.GetComponent<Animation>()[this.AniamName].speed = this.speed;
	}

	// Token: 0x04000851 RID: 2129
	public string AniamName;

	// Token: 0x04000852 RID: 2130
	public float speed = 1f;
}
