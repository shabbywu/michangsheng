using System;
using UnityEngine;

// Token: 0x02000061 RID: 97
[AddComponentMenu("NGUI/Interaction/Drag and Drop Root")]
public class UIDragDropRoot : MonoBehaviour
{
	// Token: 0x060004E5 RID: 1253 RVA: 0x0001AD28 File Offset: 0x00018F28
	private void OnEnable()
	{
		UIDragDropRoot.root = base.transform;
	}

	// Token: 0x060004E6 RID: 1254 RVA: 0x0001AD35 File Offset: 0x00018F35
	private void OnDisable()
	{
		if (UIDragDropRoot.root == base.transform)
		{
			UIDragDropRoot.root = null;
		}
	}

	// Token: 0x04000301 RID: 769
	public static Transform root;
}
