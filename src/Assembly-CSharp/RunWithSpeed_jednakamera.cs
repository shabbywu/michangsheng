using System;
using System.Collections;
using UnityEngine;

// Token: 0x020004D9 RID: 1241
public class RunWithSpeed_jednakamera : MonoBehaviour
{
	// Token: 0x06002834 RID: 10292 RVA: 0x00130604 File Offset: 0x0012E804
	private void Awake()
	{
		this.player = GameObject.FindGameObjectWithTag("Monkey");
		this.playerController = this.player.GetComponent<MonkeyController2D>();
		this.bgCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
		this.bgCameraX = GameObject.FindGameObjectWithTag("MainCamera").transform.position.x;
		this.desnaGranica = base.transform.Find("DesnaGranica");
		this.offset = base.transform.position.y - Camera.main.transform.position.y;
		this.startSpeed = this.speed;
	}

	// Token: 0x06002835 RID: 10293 RVA: 0x00004095 File Offset: 0x00002295
	private void Start()
	{
	}

	// Token: 0x06002836 RID: 10294 RVA: 0x001306B4 File Offset: 0x0012E8B4
	private void Update()
	{
		if (this.desnaGranica != null)
		{
			this.bgCameraX = this.bgCamera.transform.position.x;
			if (this.desnaGranica.position.x < this.bgCameraX - this.bgCamera.orthographicSize * this.bgCamera.aspect)
			{
				base.transform.position = new Vector3(this.bgCameraX + this.bgCamera.orthographicSize * this.bgCamera.aspect, base.transform.position.y, base.transform.position.z);
			}
		}
		if (((this.playerController.state == MonkeyController2D.State.running || this.playerController.state == MonkeyController2D.State.jumped) && this.playerController.GetComponent<Rigidbody2D>().velocity.x > 0.05f) || this.continueMoving)
		{
			if (this.smooth)
			{
				this.smoothMove = true;
			}
			if (this.speed != this.startSpeed)
			{
				this.speed = this.startSpeed;
			}
			if (!this.dovoljno)
			{
				base.Invoke("startSpeedDaj", 0.15f);
			}
			base.StopCoroutine("SmoothMovePlan");
			base.transform.position = Vector3.Lerp(base.transform.position, new Vector3(base.transform.position.x - this.speed, base.transform.position.y, base.transform.position.z), 5f * Time.deltaTime);
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

	// Token: 0x06002837 RID: 10295 RVA: 0x00130925 File Offset: 0x0012EB25
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

	// Token: 0x06002838 RID: 10296 RVA: 0x00130934 File Offset: 0x0012EB34
	private void izracunajOffset()
	{
		this.offset = base.transform.position.y - Camera.main.transform.position.y;
		this.startSpeed = this.speed;
	}

	// Token: 0x06002839 RID: 10297 RVA: 0x0013096D File Offset: 0x0012EB6D
	private void startSpeedDaj()
	{
		this.dovoljno = true;
	}

	// Token: 0x0400233F RID: 9023
	public float speed = 5f;

	// Token: 0x04002340 RID: 9024
	public bool continueMoving;

	// Token: 0x04002341 RID: 9025
	private MonkeyController2D playerController;

	// Token: 0x04002342 RID: 9026
	private GameObject player;

	// Token: 0x04002343 RID: 9027
	private float offset;

	// Token: 0x04002344 RID: 9028
	public bool FollowCameraHeight;

	// Token: 0x04002345 RID: 9029
	public bool IskljuciKadIzadjeIzKadra;

	// Token: 0x04002346 RID: 9030
	public bool smooth;

	// Token: 0x04002347 RID: 9031
	private bool smoothMove;

	// Token: 0x04002348 RID: 9032
	private float startSpeed;

	// Token: 0x04002349 RID: 9033
	public Transform desnaGranica;

	// Token: 0x0400234A RID: 9034
	private Camera bgCamera;

	// Token: 0x0400234B RID: 9035
	private float bgCameraX;

	// Token: 0x0400234C RID: 9036
	private bool dovoljno;
}
