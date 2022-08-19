using System;
using System.Collections;
using UnityEngine;

// Token: 0x020004D8 RID: 1240
public class RunWithSpeed : MonoBehaviour
{
	// Token: 0x0600282F RID: 10287 RVA: 0x00130334 File Offset: 0x0012E534
	private void Awake()
	{
		this.player = GameObject.FindGameObjectWithTag("Monkey");
		this.playerController = this.player.GetComponent<MonkeyController2D>();
		this.bgCamera = GameObject.FindGameObjectWithTag("bgCamera").GetComponent<Camera>();
		this.bgCameraX = GameObject.FindGameObjectWithTag("bgCamera").transform.position.x;
	}

	// Token: 0x06002830 RID: 10288 RVA: 0x00130396 File Offset: 0x0012E596
	private void Start()
	{
		this.offset = base.transform.position.y - Camera.main.transform.position.y;
		this.startSpeed = this.speed;
	}

	// Token: 0x06002831 RID: 10289 RVA: 0x001303D0 File Offset: 0x0012E5D0
	private void Update()
	{
		if (this.desnaGranica != null && this.desnaGranica.position.x < this.bgCameraX - this.bgCamera.orthographicSize * this.bgCamera.aspect)
		{
			base.transform.position = new Vector3(this.desnaGranica.position.x + this.desnaGranica.localPosition.x, base.transform.position.y, base.transform.position.z);
		}
		if (((this.playerController.state == MonkeyController2D.State.running || this.playerController.state == MonkeyController2D.State.jumped) && this.playerController.GetComponent<Rigidbody2D>().velocity.x > 0.05f && !this.playerController.wallHitGlide) || this.continueMoving)
		{
			if (this.smooth)
			{
				this.smoothMove = true;
			}
			if (this.speed != this.startSpeed)
			{
				this.speed = this.startSpeed;
			}
			base.StopCoroutine("SmoothMovePlan");
			base.transform.Translate(Vector3.right * this.speed * Time.deltaTime, 0);
		}
		if (this.smoothMove && (this.playerController.state == MonkeyController2D.State.wallhit || this.playerController.state == MonkeyController2D.State.climbUp))
		{
			this.smoothMove = false;
			base.StartCoroutine("SmoothMovePlan");
		}
		if (this.FollowCameraHeight)
		{
			base.transform.position = new Vector3(base.transform.position.x, Camera.main.transform.position.y + this.offset, base.transform.position.z);
		}
		if (this.IskljuciKadIzadjeIzKadra && base.transform.position.x + 25f < Camera.main.ViewportToWorldPoint(Vector3.zero).x)
		{
			base.gameObject.SetActive(false);
		}
	}

	// Token: 0x06002832 RID: 10290 RVA: 0x001305DF File Offset: 0x0012E7DF
	private IEnumerator SmoothMovePlan()
	{
		float targetPos = base.transform.position.x - 5f;
		for (float t = 0f; t < 1f; t += Time.deltaTime / 10f)
		{
			yield return null;
			base.transform.position = Vector3.Lerp(base.transform.position, new Vector3(targetPos, base.transform.position.y, base.transform.position.z), t);
		}
		yield break;
	}

	// Token: 0x04002332 RID: 9010
	public float speed = 5f;

	// Token: 0x04002333 RID: 9011
	public bool continueMoving;

	// Token: 0x04002334 RID: 9012
	private MonkeyController2D playerController;

	// Token: 0x04002335 RID: 9013
	private GameObject player;

	// Token: 0x04002336 RID: 9014
	private float offset;

	// Token: 0x04002337 RID: 9015
	public bool FollowCameraHeight;

	// Token: 0x04002338 RID: 9016
	public bool IskljuciKadIzadjeIzKadra;

	// Token: 0x04002339 RID: 9017
	public bool smooth;

	// Token: 0x0400233A RID: 9018
	private bool smoothMove;

	// Token: 0x0400233B RID: 9019
	private float startSpeed;

	// Token: 0x0400233C RID: 9020
	public Transform desnaGranica;

	// Token: 0x0400233D RID: 9021
	private Camera bgCamera;

	// Token: 0x0400233E RID: 9022
	private float bgCameraX;
}
