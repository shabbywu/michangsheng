using System;
using System.Collections;
using UnityEngine;

// Token: 0x02000671 RID: 1649
public class CameraFollow : MonoBehaviour
{
	// Token: 0x06002932 RID: 10546 RVA: 0x00020078 File Offset: 0x0001E278
	private void Start()
	{
		this.thisTransform = base.transform;
		this.monkeyControll = GameObject.Find("Player").GetComponent<MonkeyController2D>();
	}

	// Token: 0x06002933 RID: 10547 RVA: 0x00141810 File Offset: 0x0013FA10
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

	// Token: 0x06002934 RID: 10548 RVA: 0x0002009B File Offset: 0x0001E29B
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

	// Token: 0x040022F4 RID: 8948
	public GameObject cameraTarget;

	// Token: 0x040022F5 RID: 8949
	public GameObject player;

	// Token: 0x040022F6 RID: 8950
	public float smoothTime = 0.1f;

	// Token: 0x040022F7 RID: 8951
	public bool cameraFollowX = true;

	// Token: 0x040022F8 RID: 8952
	public bool cameraFollowY = true;

	// Token: 0x040022F9 RID: 8953
	public bool cameraFollowHeight;

	// Token: 0x040022FA RID: 8954
	public float cameraHeight = 2.5f;

	// Token: 0x040022FB RID: 8955
	public Vector2 velocity;

	// Token: 0x040022FC RID: 8956
	private Transform thisTransform;

	// Token: 0x040022FD RID: 8957
	public bool changeHeight;

	// Token: 0x040022FE RID: 8958
	private MonkeyController2D monkeyControll;
}
