using System;
using UnityEngine;

// Token: 0x020000DC RID: 220
[ExecuteInEditMode]
[AddComponentMenu("NGUI/Internal/Snapshot Point")]
public class UISnapshotPoint : MonoBehaviour
{
	// Token: 0x06000883 RID: 2179 RVA: 0x0000B003 File Offset: 0x00009203
	private void Start()
	{
		if (base.tag != "EditorOnly")
		{
			base.tag = "EditorOnly";
		}
	}

	// Token: 0x040005FA RID: 1530
	public bool isOrthographic = true;

	// Token: 0x040005FB RID: 1531
	public float nearClip = -100f;

	// Token: 0x040005FC RID: 1532
	public float farClip = 100f;

	// Token: 0x040005FD RID: 1533
	[Range(10f, 80f)]
	public int fieldOfView = 35;

	// Token: 0x040005FE RID: 1534
	public float orthoSize = 30f;
}
