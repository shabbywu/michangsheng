using System;
using UnityEngine;

// Token: 0x020004D1 RID: 1233
public class KameraZoom : MonoBehaviour
{
	// Token: 0x060027E8 RID: 10216 RVA: 0x00004095 File Offset: 0x00002295
	private void Start()
	{
	}

	// Token: 0x060027E9 RID: 10217 RVA: 0x0012F00C File Offset: 0x0012D20C
	private void Update()
	{
		if (Input.touchCount == 2 && Input.GetTouch(0).phase == 1 && Input.GetTouch(1).phase == 1)
		{
			this.curDist = Input.GetTouch(0).position - Input.GetTouch(1).position;
			this.prevDist = Input.GetTouch(0).position - Input.GetTouch(0).deltaPosition - (Input.GetTouch(1).position - Input.GetTouch(1).deltaPosition);
			this.touchDelta = this.curDist.magnitude - this.prevDist.magnitude;
			this.speedTouch0 = Input.GetTouch(0).deltaPosition.magnitude / Input.GetTouch(0).deltaTime;
			this.speedTouch1 = Input.GetTouch(1).deltaPosition.magnitude / Input.GetTouch(1).deltaTime;
			if (this.touchDelta + this.varianceInDistances <= 1f && this.speedTouch0 > this.minPinchSpeed && this.speedTouch1 > this.minPinchSpeed)
			{
				this.selectedCamera.orthographicSize = Mathf.Clamp(this.selectedCamera.orthographicSize + (float)this.speed, 5f, 10f);
			}
			if (this.touchDelta + this.varianceInDistances > 1f && this.speedTouch0 > this.minPinchSpeed && this.speedTouch1 > this.minPinchSpeed)
			{
				this.selectedCamera.orthographicSize = Mathf.Clamp(this.selectedCamera.fieldOfView - (float)this.speed, 5f, 10f);
			}
		}
	}

	// Token: 0x040022EC RID: 8940
	public int speed = 4;

	// Token: 0x040022ED RID: 8941
	public Camera selectedCamera;

	// Token: 0x040022EE RID: 8942
	public float MINSCALE = 2f;

	// Token: 0x040022EF RID: 8943
	public float MAXSCALE = 5f;

	// Token: 0x040022F0 RID: 8944
	public float minPinchSpeed = 5f;

	// Token: 0x040022F1 RID: 8945
	public float varianceInDistances = 5f;

	// Token: 0x040022F2 RID: 8946
	private float touchDelta;

	// Token: 0x040022F3 RID: 8947
	private Vector2 prevDist = new Vector2(0f, 0f);

	// Token: 0x040022F4 RID: 8948
	private Vector2 curDist = new Vector2(0f, 0f);

	// Token: 0x040022F5 RID: 8949
	private float speedTouch0;

	// Token: 0x040022F6 RID: 8950
	private float speedTouch1;
}
