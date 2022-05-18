using System;
using UnityEngine;

// Token: 0x0200060F RID: 1551
public class csPrefebMake : MonoBehaviour
{
	// Token: 0x060026A0 RID: 9888 RVA: 0x0001ECA6 File Offset: 0x0001CEA6
	private void Start()
	{
		this._MakePrefeb = Object.Instantiate<Transform>(this.MakePrefeb, base.transform.position, Quaternion.identity);
		this._MakePrefeb.transform.parent = base.transform;
	}

	// Token: 0x060026A1 RID: 9889 RVA: 0x0012EFAC File Offset: 0x0012D1AC
	private void Update()
	{
		if (this.DeadTime <= 0f)
		{
			return;
		}
		if (this._MakePrefeb)
		{
			Object.Destroy(this._MakePrefeb.gameObject, this.DeadTime);
			return;
		}
		if (!this._MakePrefeb)
		{
			this._MakePrefeb = Object.Instantiate<Transform>(this.MakePrefeb, base.transform.position, Quaternion.identity);
			this._MakePrefeb.transform.parent = base.transform;
		}
	}

	// Token: 0x040020EB RID: 8427
	public Transform MakePrefeb;

	// Token: 0x040020EC RID: 8428
	private Transform _MakePrefeb;

	// Token: 0x040020ED RID: 8429
	public float DeadTime;
}
