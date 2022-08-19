using System;
using UnityEngine;

// Token: 0x020004AF RID: 1199
public class FreezeRotation : MonoBehaviour
{
	// Token: 0x060025F2 RID: 9714 RVA: 0x00106C86 File Offset: 0x00104E86
	private void Start()
	{
		this.shieldPosition = base.transform.position;
	}

	// Token: 0x060025F3 RID: 9715 RVA: 0x00106C9C File Offset: 0x00104E9C
	private void Update()
	{
		base.transform.position = new Vector3(this.tr.position.x, this.tr.position.y, base.transform.position.z);
	}

	// Token: 0x04001EBD RID: 7869
	public Transform tr;

	// Token: 0x04001EBE RID: 7870
	private Transform myTransform;

	// Token: 0x04001EBF RID: 7871
	private Vector3 shieldPosition;

	// Token: 0x04001EC0 RID: 7872
	private float x;

	// Token: 0x04001EC1 RID: 7873
	private float y;
}
