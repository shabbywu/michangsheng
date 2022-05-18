using System;
using UnityEngine;

// Token: 0x0200012A RID: 298
public class ZoomInOut : MonoBehaviour
{
	// Token: 0x06000B84 RID: 2948 RVA: 0x0000D8C6 File Offset: 0x0000BAC6
	private void Start()
	{
		this.distance = base.GetComponent<Camera>().fieldOfView;
	}

	// Token: 0x06000B85 RID: 2949 RVA: 0x00092C68 File Offset: 0x00090E68
	private void Update()
	{
		this.distance -= Input.GetAxis("Mouse ScrollWheel") * this.sensitivityDistance;
		this.distance = Mathf.Clamp(this.distance, this.minFOV, this.maxFOV);
		base.GetComponent<Camera>().fieldOfView = Mathf.Lerp(base.GetComponent<Camera>().fieldOfView, this.distance, Time.deltaTime * this.damping);
	}

	// Token: 0x0400084C RID: 2124
	public float distance = 50f;

	// Token: 0x0400084D RID: 2125
	public float sensitivityDistance = 50f;

	// Token: 0x0400084E RID: 2126
	public float damping = 50f;

	// Token: 0x0400084F RID: 2127
	public float minFOV = 5f;

	// Token: 0x04000850 RID: 2128
	public float maxFOV = 100f;
}
