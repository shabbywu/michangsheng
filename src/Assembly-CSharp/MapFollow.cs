using System;
using UnityEngine;

// Token: 0x02000300 RID: 768
public class MapFollow : MonoBehaviour
{
	// Token: 0x0600170E RID: 5902 RVA: 0x000042DD File Offset: 0x000024DD
	private void Start()
	{
	}

	// Token: 0x0600170F RID: 5903 RVA: 0x000CC304 File Offset: 0x000CA504
	private void Update()
	{
		if (this.target != null)
		{
			base.transform.position = new Vector3(this.target.position.x, base.transform.position.y, this.target.position.z);
		}
	}

	// Token: 0x04001267 RID: 4711
	public Transform target;
}
