using System;
using UnityEngine;

// Token: 0x02000498 RID: 1176
public class CameraFollow2D_new : MonoBehaviour
{
	// Token: 0x0600251A RID: 9498 RVA: 0x00101DB8 File Offset: 0x000FFFB8
	private void Awake()
	{
		this.cameraBottomLimit_y = this.cameraBottomLimit.position.y;
		this.thisTransform = base.transform;
		this.limitY = Camera.main.ViewportToWorldPoint(Vector3.one * 0.7f).y;
		this.cameraTarget.transform.position = new Vector3(this.cameraTarget.transform.position.x, Camera.main.transform.position.y, this.cameraTarget.transform.position.z);
		this.playerController = this.player.GetComponent<MonkeyController2D>();
		this.borderY = Camera.main.ScreenToWorldPoint(new Vector3((float)Screen.width, (float)Screen.height, 0f)).y * 0.8f;
	}

	// Token: 0x0600251B RID: 9499 RVA: 0x00101EA0 File Offset: 0x001000A0
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

	// Token: 0x0600251C RID: 9500 RVA: 0x0010201C File Offset: 0x0010021C
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

	// Token: 0x04001DED RID: 7661
	public GameObject cameraTarget;

	// Token: 0x04001DEE RID: 7662
	public GameObject player;

	// Token: 0x04001DEF RID: 7663
	public float smoothTime = 0.01f;

	// Token: 0x04001DF0 RID: 7664
	public bool cameraFollowX = true;

	// Token: 0x04001DF1 RID: 7665
	public bool cameraFollowY;

	// Token: 0x04001DF2 RID: 7666
	public bool cameraFollowHeight;

	// Token: 0x04001DF3 RID: 7667
	public float cameraHeight = 2.5f;

	// Token: 0x04001DF4 RID: 7668
	public Vector3 velocity;

	// Token: 0x04001DF5 RID: 7669
	private Transform thisTransform;

	// Token: 0x04001DF6 RID: 7670
	public float borderY;

	// Token: 0x04001DF7 RID: 7671
	public bool moveUp;

	// Token: 0x04001DF8 RID: 7672
	public bool moveDown;

	// Token: 0x04001DF9 RID: 7673
	public bool grounded;

	// Token: 0x04001DFA RID: 7674
	public float limitY;

	// Token: 0x04001DFB RID: 7675
	public Transform cameraBottomLimit;

	// Token: 0x04001DFC RID: 7676
	private float cameraBottomLimit_y = 2.1f;

	// Token: 0x04001DFD RID: 7677
	[HideInInspector]
	public Transform rotatingPlayer;

	// Token: 0x04001DFE RID: 7678
	[HideInInspector]
	public bool stopFollow;

	// Token: 0x04001DFF RID: 7679
	private MonkeyController2D playerController;

	// Token: 0x04001E00 RID: 7680
	[HideInInspector]
	public bool transition;

	// Token: 0x04001E01 RID: 7681
	[HideInInspector]
	public float upperLimit = 0.7f;
}
