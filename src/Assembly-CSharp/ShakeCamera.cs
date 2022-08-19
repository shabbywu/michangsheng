using System;
using UnityEngine;

// Token: 0x020000D8 RID: 216
[RequireComponent(typeof(OrbitGameObject))]
public class ShakeCamera : MonoBehaviour
{
	// Token: 0x06000B36 RID: 2870 RVA: 0x0004466E File Offset: 0x0004286E
	public static ShakeCamera Shake(float magnitude, float duration)
	{
		ShakeCamera shakeCamera = Camera.main.gameObject.AddComponent<ShakeCamera>();
		shakeCamera.Magnitude = magnitude;
		shakeCamera.Duration = duration;
		return shakeCamera;
	}

	// Token: 0x06000B37 RID: 2871 RVA: 0x00044690 File Offset: 0x00042890
	private void Update()
	{
		this.Duration -= Time.deltaTime;
		if (this.Duration < 0f)
		{
			Object.Destroy(this);
		}
		base.gameObject.GetComponent<OrbitGameObject>().ArmOffset.y = Mathf.Sin(1000f * Time.time) * this.Duration * this.Magnitude;
	}

	// Token: 0x0400075B RID: 1883
	public float Magnitude = 1f;

	// Token: 0x0400075C RID: 1884
	public float Duration = 1f;
}
