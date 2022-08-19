using System;
using UnityEngine;

// Token: 0x020000BA RID: 186
public class ZoomInOut : MonoBehaviour
{
	// Token: 0x06000AA1 RID: 2721 RVA: 0x000407B8 File Offset: 0x0003E9B8
	private void Start()
	{
		this.distance = base.GetComponent<Camera>().fieldOfView;
	}

	// Token: 0x06000AA2 RID: 2722 RVA: 0x000407CC File Offset: 0x0003E9CC
	private void Update()
	{
		this.distance -= Input.GetAxis("Mouse ScrollWheel") * this.sensitivityDistance;
		this.distance = Mathf.Clamp(this.distance, this.minFOV, this.maxFOV);
		base.GetComponent<Camera>().fieldOfView = Mathf.Lerp(base.GetComponent<Camera>().fieldOfView, this.distance, Time.deltaTime * this.damping);
	}

	// Token: 0x040006A5 RID: 1701
	public float distance = 50f;

	// Token: 0x040006A6 RID: 1702
	public float sensitivityDistance = 50f;

	// Token: 0x040006A7 RID: 1703
	public float damping = 50f;

	// Token: 0x040006A8 RID: 1704
	public float minFOV = 5f;

	// Token: 0x040006A9 RID: 1705
	public float maxFOV = 100f;
}
