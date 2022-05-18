using System;
using UnityEngine;

// Token: 0x0200060E RID: 1550
public class csLightIntancityControl : MonoBehaviour
{
	// Token: 0x0600269E RID: 9886 RVA: 0x0012EF2C File Offset: 0x0012D12C
	private void Update()
	{
		this._time += Time.deltaTime;
		if (this._time > this.Delay)
		{
			if (this._light.intensity > 0f)
			{
				this._light.intensity -= Time.deltaTime * this.Down;
			}
			if (this._light.intensity <= 0f)
			{
				this._light.intensity = 0f;
			}
		}
	}

	// Token: 0x040020E7 RID: 8423
	public Light _light;

	// Token: 0x040020E8 RID: 8424
	private float _time;

	// Token: 0x040020E9 RID: 8425
	public float Delay = 0.5f;

	// Token: 0x040020EA RID: 8426
	public float Down = 1f;
}
