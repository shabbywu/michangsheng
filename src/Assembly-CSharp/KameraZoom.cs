using System;
using UnityEngine;

// Token: 0x02000744 RID: 1860
public class KameraZoom : MonoBehaviour
{
	// Token: 0x06002F3F RID: 12095 RVA: 0x000042DD File Offset: 0x000024DD
	private void Start()
	{
	}

	// Token: 0x06002F40 RID: 12096 RVA: 0x0017BD8C File Offset: 0x00179F8C
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

	// Token: 0x04002A6C RID: 10860
	public int speed = 4;

	// Token: 0x04002A6D RID: 10861
	public Camera selectedCamera;

	// Token: 0x04002A6E RID: 10862
	public float MINSCALE = 2f;

	// Token: 0x04002A6F RID: 10863
	public float MAXSCALE = 5f;

	// Token: 0x04002A70 RID: 10864
	public float minPinchSpeed = 5f;

	// Token: 0x04002A71 RID: 10865
	public float varianceInDistances = 5f;

	// Token: 0x04002A72 RID: 10866
	private float touchDelta;

	// Token: 0x04002A73 RID: 10867
	private Vector2 prevDist = new Vector2(0f, 0f);

	// Token: 0x04002A74 RID: 10868
	private Vector2 curDist = new Vector2(0f, 0f);

	// Token: 0x04002A75 RID: 10869
	private float speedTouch0;

	// Token: 0x04002A76 RID: 10870
	private float speedTouch1;
}
