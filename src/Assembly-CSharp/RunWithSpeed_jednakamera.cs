using System;
using System.Collections;
using UnityEngine;

// Token: 0x0200074D RID: 1869
public class RunWithSpeed_jednakamera : MonoBehaviour
{
	// Token: 0x06002F91 RID: 12177 RVA: 0x0017CE20 File Offset: 0x0017B020
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

	// Token: 0x06002F92 RID: 12178 RVA: 0x000042DD File Offset: 0x000024DD
	private void Start()
	{
	}

	// Token: 0x06002F93 RID: 12179 RVA: 0x0017CED0 File Offset: 0x0017B0D0
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

	// Token: 0x06002F94 RID: 12180 RVA: 0x00023439 File Offset: 0x00021639
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

	// Token: 0x06002F95 RID: 12181 RVA: 0x00023448 File Offset: 0x00021648
	private void izracunajOffset()
	{
		this.offset = base.transform.position.y - Camera.main.transform.position.y;
		this.startSpeed = this.speed;
	}

	// Token: 0x06002F96 RID: 12182 RVA: 0x00023481 File Offset: 0x00021681
	private void startSpeedDaj()
	{
		this.dovoljno = true;
	}

	// Token: 0x04002AC4 RID: 10948
	public float speed = 5f;

	// Token: 0x04002AC5 RID: 10949
	public bool continueMoving;

	// Token: 0x04002AC6 RID: 10950
	private MonkeyController2D playerController;

	// Token: 0x04002AC7 RID: 10951
	private GameObject player;

	// Token: 0x04002AC8 RID: 10952
	private float offset;

	// Token: 0x04002AC9 RID: 10953
	public bool FollowCameraHeight;

	// Token: 0x04002ACA RID: 10954
	public bool IskljuciKadIzadjeIzKadra;

	// Token: 0x04002ACB RID: 10955
	public bool smooth;

	// Token: 0x04002ACC RID: 10956
	private bool smoothMove;

	// Token: 0x04002ACD RID: 10957
	private float startSpeed;

	// Token: 0x04002ACE RID: 10958
	public Transform desnaGranica;

	// Token: 0x04002ACF RID: 10959
	private Camera bgCamera;

	// Token: 0x04002AD0 RID: 10960
	private float bgCameraX;

	// Token: 0x04002AD1 RID: 10961
	private bool dovoljno;
}
