using System;
using UnityEngine;

// Token: 0x02000673 RID: 1651
public class CameraFollow2D_new : MonoBehaviour
{
	// Token: 0x0600293C RID: 10556 RVA: 0x00141A7C File Offset: 0x0013FC7C
	private void Awake()
	{
		this.cameraBottomLimit_y = this.cameraBottomLimit.position.y;
		this.thisTransform = base.transform;
		this.limitY = Camera.main.ViewportToWorldPoint(Vector3.one * 0.7f).y;
		this.cameraTarget.transform.position = new Vector3(this.cameraTarget.transform.position.x, Camera.main.transform.position.y, this.cameraTarget.transform.position.z);
		this.playerController = this.player.GetComponent<MonkeyController2D>();
		this.borderY = Camera.main.ScreenToWorldPoint(new Vector3((float)Screen.width, (float)Screen.height, 0f)).y * 0.8f;
	}

	// Token: 0x0600293D RID: 10557 RVA: 0x00141B64 File Offset: 0x0013FD64
	private void Start()
	{
		if (Camera.main.aspect < 1.5f)
		{
			Camera.main.transform.position = new Vector3(Camera.main.transform.position.x, 7.5f, Camera.main.transform.position.z);
			this.cameraTarget.transform.position = new Vector3(this.cameraTarget.transform.position.x, Camera.main.transform.position.y, this.cameraTarget.transform.position.z);
			this.borderY = Camera.main.ScreenToWorldPoint(new Vector3((float)Screen.width, (float)Screen.height, 0f)).y * 0.75f;
			return;
		}
		Camera.main.transform.position = new Vector3(Camera.main.transform.position.x, 8.5f, Camera.main.transform.position.z);
		this.cameraTarget.transform.position = new Vector3(this.cameraTarget.transform.position.x, Camera.main.transform.position.y, this.cameraTarget.transform.position.z);
	}

	// Token: 0x0600293E RID: 10558 RVA: 0x00141CE0 File Offset: 0x0013FEE0
	private void Update()
	{
		if (!this.stopFollow)
		{
			if (this.cameraFollowX)
			{
				this.thisTransform.position = Vector3.Lerp(this.thisTransform.position, new Vector3(this.cameraTarget.transform.position.x, this.thisTransform.position.y, this.thisTransform.position.z), 5f * Time.deltaTime);
			}
			if (this.cameraFollowY)
			{
				this.thisTransform.position = Vector3.SmoothDamp(this.thisTransform.position, new Vector3(this.thisTransform.position.x, this.cameraTarget.transform.position.y, this.thisTransform.position.z), ref this.velocity, this.smoothTime);
			}
			if (!this.cameraFollowY && this.cameraFollowHeight)
			{
				Camera.main.transform.position = new Vector3(Camera.main.transform.position.x, this.cameraHeight, Camera.main.transform.position.z);
			}
			if ((this.playerController.state == MonkeyController2D.State.jumped && this.player.transform.position.y > Camera.main.ScreenToWorldPoint(new Vector3(0f, (float)Screen.height * this.upperLimit, 0f)).y) || (this.playerController.state != MonkeyController2D.State.jumped && this.player.transform.position.y > Camera.main.ScreenToWorldPoint(new Vector3(0f, (float)Screen.height * 0.25f, 0f)).y))
			{
				this.moveUp = true;
			}
			if (this.moveUp && this.playerController.heCanJump)
			{
				this.thisTransform.position = Vector3.SmoothDamp(this.thisTransform.position, new Vector3(this.thisTransform.position.x, this.cameraTarget.transform.position.y, this.thisTransform.position.z), ref this.velocity, this.smoothTime, 2000f * Time.deltaTime, Time.smoothDeltaTime);
			}
			if (this.playerController.state == MonkeyController2D.State.jumped && Mathf.Abs(this.cameraTarget.transform.position.y - Camera.main.transform.position.y) <= 0.1f && this.moveUp)
			{
				this.moveUp = false;
			}
			if (this.cameraTarget.transform.position.y <= Camera.main.transform.position.y)
			{
				this.moveDown = true;
			}
			if (this.moveDown && !this.transition)
			{
				this.thisTransform.position = new Vector3(this.thisTransform.position.x, this.cameraTarget.transform.position.y, this.thisTransform.position.z);
			}
			if (this.cameraTarget.transform.position.y >= Camera.main.transform.position.y)
			{
				this.moveDown = false;
			}
		}
	}

	// Token: 0x04002303 RID: 8963
	public GameObject cameraTarget;

	// Token: 0x04002304 RID: 8964
	public GameObject player;

	// Token: 0x04002305 RID: 8965
	public float smoothTime = 0.01f;

	// Token: 0x04002306 RID: 8966
	public bool cameraFollowX = true;

	// Token: 0x04002307 RID: 8967
	public bool cameraFollowY;

	// Token: 0x04002308 RID: 8968
	public bool cameraFollowHeight;

	// Token: 0x04002309 RID: 8969
	public float cameraHeight = 2.5f;

	// Token: 0x0400230A RID: 8970
	public Vector3 velocity;

	// Token: 0x0400230B RID: 8971
	private Transform thisTransform;

	// Token: 0x0400230C RID: 8972
	public float borderY;

	// Token: 0x0400230D RID: 8973
	public bool moveUp;

	// Token: 0x0400230E RID: 8974
	public bool moveDown;

	// Token: 0x0400230F RID: 8975
	public bool grounded;

	// Token: 0x04002310 RID: 8976
	public float limitY;

	// Token: 0x04002311 RID: 8977
	public Transform cameraBottomLimit;

	// Token: 0x04002312 RID: 8978
	private float cameraBottomLimit_y = 2.1f;

	// Token: 0x04002313 RID: 8979
	[HideInInspector]
	public Transform rotatingPlayer;

	// Token: 0x04002314 RID: 8980
	[HideInInspector]
	public bool stopFollow;

	// Token: 0x04002315 RID: 8981
	private MonkeyController2D playerController;

	// Token: 0x04002316 RID: 8982
	[HideInInspector]
	public bool transition;

	// Token: 0x04002317 RID: 8983
	[HideInInspector]
	public float upperLimit = 0.7f;
}
