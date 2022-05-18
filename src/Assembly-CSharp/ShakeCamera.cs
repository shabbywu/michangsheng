using System;
using UnityEngine;

// Token: 0x0200014F RID: 335
[RequireComponent(typeof(OrbitGameObject))]
public class ShakeCamera : MonoBehaviour
{
	// Token: 0x06000C25 RID: 3109 RVA: 0x0000E2AC File Offset: 0x0000C4AC
	public static ShakeCamera Shake(float magnitude, float duration)
	{
		ShakeCamera shakeCamera = Camera.main.gameObject.AddComponent<ShakeCamera>();
		shakeCamera.Magnitude = magnitude;
		shakeCamera.Duration = duration;
		return shakeCamera;
	}

	// Token: 0x06000C26 RID: 3110 RVA: 0x00096458 File Offset: 0x00094658
	private void Update()
	{
		this.Duration -= Time.deltaTime;
		if (this.Duration < 0f)
		{
			Object.Destroy(this);
		}
		base.gameObject.GetComponent<OrbitGameObject>().ArmOffset.y = Mathf.Sin(1000f * Time.time) * this.Duration * this.Magnitude;
	}

	// Token: 0x04000936 RID: 2358
	public float Magnitude = 1f;

	// Token: 0x04000937 RID: 2359
	public float Duration = 1f;
}
