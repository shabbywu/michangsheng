using System;
using System.Collections;
using UnityEngine;

// Token: 0x02000497 RID: 1175
public class CameraFollow : MonoBehaviour
{
	// Token: 0x06002516 RID: 9494 RVA: 0x00101BF2 File Offset: 0x000FFDF2
	private void Start()
	{
		this.thisTransform = base.transform;
		this.monkeyControll = GameObject.Find("Player").GetComponent<MonkeyController2D>();
	}

	// Token: 0x06002517 RID: 9495 RVA: 0x00101C18 File Offset: 0x000FFE18
	private void Update()
	{
		if (this.cameraFollowX)
		{
			float num = Mathf.SmoothDamp(this.thisTransform.position.x, this.cameraTarget.transform.position.x, ref this.velocity.x, this.smoothTime);
			this.thisTransform.position = new Vector3(num, this.thisTransform.position.y, this.thisTransform.position.z);
		}
		if (this.cameraFollowY)
		{
			float num2 = Mathf.SmoothDamp(this.thisTransform.position.y, this.cameraTarget.transform.position.y, ref this.velocity.y, this.smoothTime);
			this.thisTransform.position = new Vector3(this.thisTransform.position.x, num2, this.thisTransform.position.z);
		}
		if (!this.cameraFollowY && this.cameraFollowHeight)
		{
			Camera.main.transform.position = new Vector3(Camera.main.transform.position.x, this.cameraHeight, Camera.main.transform.position.z);
		}
		if (this.changeHeight)
		{
			base.StartCoroutine(this.catchCameraY());
		}
		bool flag = this.changeHeight;
	}

	// Token: 0x06002518 RID: 9496 RVA: 0x00101D7D File Offset: 0x000FFF7D
	private IEnumerator catchCameraY()
	{
		for (float i = 0f; i < 1f; i += 0.001f)
		{
			Debug.Log("Usao u korutinu: " + i);
			yield return null;
			if (!this.changeHeight)
			{
				break;
			}
			this.thisTransform.position = new Vector3(this.thisTransform.position.x, Mathf.MoveTowards(this.thisTransform.position.y, this.cameraTarget.transform.position.y, i), this.thisTransform.position.z);
		}
		this.changeHeight = false;
		yield break;
	}

	// Token: 0x04001DE2 RID: 7650
	public GameObject cameraTarget;

	// Token: 0x04001DE3 RID: 7651
	public GameObject player;

	// Token: 0x04001DE4 RID: 7652
	public float smoothTime = 0.1f;

	// Token: 0x04001DE5 RID: 7653
	public bool cameraFollowX = true;

	// Token: 0x04001DE6 RID: 7654
	public bool cameraFollowY = true;

	// Token: 0x04001DE7 RID: 7655
	public bool cameraFollowHeight;

	// Token: 0x04001DE8 RID: 7656
	public float cameraHeight = 2.5f;

	// Token: 0x04001DE9 RID: 7657
	public Vector2 velocity;

	// Token: 0x04001DEA RID: 7658
	private Transform thisTransform;

	// Token: 0x04001DEB RID: 7659
	public bool changeHeight;

	// Token: 0x04001DEC RID: 7660
	private MonkeyController2D monkeyControll;
}
