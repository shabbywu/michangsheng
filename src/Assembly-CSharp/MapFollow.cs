using System;
using UnityEngine;

// Token: 0x020001EC RID: 492
public class MapFollow : MonoBehaviour
{
	// Token: 0x06001464 RID: 5220 RVA: 0x00004095 File Offset: 0x00002295
	private void Start()
	{
	}

	// Token: 0x06001465 RID: 5221 RVA: 0x0008336C File Offset: 0x0008156C
	private void Update()
	{
		if (this.target != null)
		{
			base.transform.position = new Vector3(this.target.position.x, base.transform.position.y, this.target.position.z);
		}
	}

	// Token: 0x04000F24 RID: 3876
	public Transform target;
}
