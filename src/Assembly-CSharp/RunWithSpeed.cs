using System;
using System.Collections;
using UnityEngine;

// Token: 0x0200074B RID: 1867
public class RunWithSpeed : MonoBehaviour
{
	// Token: 0x06002F86 RID: 12166 RVA: 0x0017CACC File Offset: 0x0017ACCC
	private void Awake()
	{
		this.player = GameObject.FindGameObjectWithTag("Monkey");
		this.playerController = this.player.GetComponent<MonkeyController2D>();
		this.bgCamera = GameObject.FindGameObjectWithTag("bgCamera").GetComponent<Camera>();
		this.bgCameraX = GameObject.FindGameObjectWithTag("bgCamera").transform.position.x;
	}

	// Token: 0x06002F87 RID: 12167 RVA: 0x000233C7 File Offset: 0x000215C7
	private void Start()
	{
		this.offset = base.transform.position.y - Camera.main.transform.position.y;
		this.startSpeed = this.speed;
	}

	// Token: 0x06002F88 RID: 12168 RVA: 0x0017CB30 File Offset: 0x0017AD30
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

	// Token: 0x06002F89 RID: 12169 RVA: 0x00023400 File Offset: 0x00021600
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

	// Token: 0x04002AB2 RID: 10930
	public float speed = 5f;

	// Token: 0x04002AB3 RID: 10931
	public bool continueMoving;

	// Token: 0x04002AB4 RID: 10932
	private MonkeyController2D playerController;

	// Token: 0x04002AB5 RID: 10933
	private GameObject player;

	// Token: 0x04002AB6 RID: 10934
	private float offset;

	// Token: 0x04002AB7 RID: 10935
	public bool FollowCameraHeight;

	// Token: 0x04002AB8 RID: 10936
	public bool IskljuciKadIzadjeIzKadra;

	// Token: 0x04002AB9 RID: 10937
	public bool smooth;

	// Token: 0x04002ABA RID: 10938
	private bool smoothMove;

	// Token: 0x04002ABB RID: 10939
	private float startSpeed;

	// Token: 0x04002ABC RID: 10940
	public Transform desnaGranica;

	// Token: 0x04002ABD RID: 10941
	private Camera bgCamera;

	// Token: 0x04002ABE RID: 10942
	private float bgCameraX;
}
