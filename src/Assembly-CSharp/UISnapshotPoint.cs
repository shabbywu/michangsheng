using System;
using UnityEngine;

// Token: 0x02000091 RID: 145
[ExecuteInEditMode]
[AddComponentMenu("NGUI/Internal/Snapshot Point")]
public class UISnapshotPoint : MonoBehaviour
{
	// Token: 0x060007DB RID: 2011 RVA: 0x0002FE21 File Offset: 0x0002E021
	private void Start()
	{
		if (base.tag != "EditorOnly")
		{
			base.tag = "EditorOnly";
		}
	}

	// Token: 0x040004E5 RID: 1253
	public bool isOrthographic = true;

	// Token: 0x040004E6 RID: 1254
	public float nearClip = -100f;

	// Token: 0x040004E7 RID: 1255
	public float farClip = 100f;

	// Token: 0x040004E8 RID: 1256
	[Range(10f, 80f)]
	public int fieldOfView = 35;

	// Token: 0x040004E9 RID: 1257
	public float orthoSize = 30f;
}
