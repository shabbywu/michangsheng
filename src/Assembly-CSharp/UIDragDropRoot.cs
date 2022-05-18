using System;
using UnityEngine;

// Token: 0x0200007E RID: 126
[AddComponentMenu("NGUI/Interaction/Drag and Drop Root")]
public class UIDragDropRoot : MonoBehaviour
{
	// Token: 0x06000537 RID: 1335 RVA: 0x00008AB6 File Offset: 0x00006CB6
	private void OnEnable()
	{
		UIDragDropRoot.root = base.transform;
	}

	// Token: 0x06000538 RID: 1336 RVA: 0x00008AC3 File Offset: 0x00006CC3
	private void OnDisable()
	{
		if (UIDragDropRoot.root == base.transform)
		{
			UIDragDropRoot.root = null;
		}
	}

	// Token: 0x04000385 RID: 901
	public static Transform root;
}
