using System;
using UnityEngine;

// Token: 0x02000456 RID: 1110
public class csPrefebMake : MonoBehaviour
{
	// Token: 0x060022E9 RID: 8937 RVA: 0x000EE7D5 File Offset: 0x000EC9D5
	private void Start()
	{
		this._MakePrefeb = Object.Instantiate<Transform>(this.MakePrefeb, base.transform.position, Quaternion.identity);
		this._MakePrefeb.transform.parent = base.transform;
	}

	// Token: 0x060022EA RID: 8938 RVA: 0x000EE810 File Offset: 0x000ECA10
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

	// Token: 0x04001C1B RID: 7195
	public Transform MakePrefeb;

	// Token: 0x04001C1C RID: 7196
	private Transform _MakePrefeb;

	// Token: 0x04001C1D RID: 7197
	public float DeadTime;
}
