using System;
using UnityEngine;

// Token: 0x02000455 RID: 1109
public class csLightIntancityControl : MonoBehaviour
{
	// Token: 0x060022E7 RID: 8935 RVA: 0x000EE738 File Offset: 0x000EC938
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

	// Token: 0x04001C17 RID: 7191
	public Light _light;

	// Token: 0x04001C18 RID: 7192
	private float _time;

	// Token: 0x04001C19 RID: 7193
	public float Delay = 0.5f;

	// Token: 0x04001C1A RID: 7194
	public float Down = 1f;
}
