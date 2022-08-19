using System;
using System.Collections;
using UnityEngine;

// Token: 0x02000491 RID: 1169
public class BabunDogadjaji : MonoBehaviour
{
	// Token: 0x060024E2 RID: 9442 RVA: 0x000FFC6C File Offset: 0x000FDE6C
	private void Awake()
	{
		this.player = GameObject.FindGameObjectWithTag("Monkey").GetComponent<MonkeyController2D>();
		this.babun = base.transform;
		this.reqHeight = base.transform.Find("BabunHeight");
		this.anim = this.babun.GetComponent<Animator>();
		this.colliders = base.GetComponents<BoxCollider2D>();
		this.anim.enabled = false;
		this.fireEvent = false;
	}

	// Token: 0x060024E3 RID: 9443 RVA: 0x000FFCE0 File Offset: 0x000FDEE0
	private void Start()
	{
		if (!this.IdleUdaranje)
		{
			this.anim.Play(this.idle_state);
		}
		else
		{
			this.anim.Play(this.idle_udaranje_state);
		}
		this.baboonLocPos = base.transform.localPosition;
	}

	// Token: 0x060024E4 RID: 9444 RVA: 0x000FFD20 File Offset: 0x000FDF20
	private void Update()
	{
		if (this.runAndJump && base.transform.position.x < Camera.main.ViewportToWorldPoint(Vector3.one).x - 7.5f && !this.skocioOdmah)
		{
			this.anim.Play("Jump");
			this.parentAnim.Play("BaboonJumpOnce");
			this.jumpNow = true;
			this.skocioOdmah = true;
			this.anim.SetBool("Land", false);
		}
		if ((base.transform.position.x < Camera.main.ViewportToWorldPoint(Vector3.one).x + this.distance || this.animateInstantly) && this.proveraJednom)
		{
			this.proveraJednom = false;
			this.anim.enabled = true;
			this.fireEvent = true;
		}
		if (this.patrol)
		{
			if (this.fireEvent)
			{
				this.fireEvent = false;
				this.anim.Play("Walking_left");
				return;
			}
		}
		else if (this.run)
		{
			if (this.fireEvent)
			{
				this.fireEvent = false;
				this.anim.Play("Running");
				base.GetComponent<Rigidbody2D>().isKinematic = false;
				return;
			}
		}
		else if (this.jump)
		{
			if (this.fireEvent)
			{
				base.GetComponent<Rigidbody2D>().isKinematic = true;
				this.fireEvent = false;
				this.anim.Play("Jump");
				this.parentAnim.Play("BaboonJump");
				return;
			}
		}
		else if (this.fly)
		{
			if (this.fireEvent)
			{
				this.bureRaketa.SetActive(true);
				this.anim.SetBool("Land", false);
				this.fireEvent = false;
				base.GetComponent<Rigidbody2D>().isKinematic = true;
				this.parentAnim.Play("BaboonFly");
				this.anim.Play("Fly");
				return;
			}
		}
		else if (this.runAndJump && this.fireEvent)
		{
			this.fireEvent = false;
			this.anim.Play("Running");
			base.GetComponent<Rigidbody2D>().isKinematic = false;
		}
	}

	// Token: 0x060024E5 RID: 9445 RVA: 0x000FFF40 File Offset: 0x000FE140
	private void FixedUpdate()
	{
		if (this.run || this.runAndJump)
		{
			base.GetComponent<Rigidbody2D>().AddForce(new Vector2(-3500f, 0f));
			if (Mathf.Abs(base.GetComponent<Rigidbody2D>().velocity.x) > this.maxSpeedX)
			{
				base.GetComponent<Rigidbody2D>().velocity = new Vector2(-this.maxSpeedX, base.GetComponent<Rigidbody2D>().velocity.y);
			}
		}
	}

	// Token: 0x060024E6 RID: 9446 RVA: 0x000FFFBB File Offset: 0x000FE1BB
	private void SobaliJump()
	{
		this.canJump = false;
	}

	// Token: 0x060024E7 RID: 9447 RVA: 0x000FFFC4 File Offset: 0x000FE1C4
	private void OdvaliJump()
	{
		this.fireEvent = true;
	}

	// Token: 0x060024E8 RID: 9448 RVA: 0x000FFFCD File Offset: 0x000FE1CD
	private IEnumerator Patrol()
	{
		yield return null;
		yield break;
	}

	// Token: 0x060024E9 RID: 9449 RVA: 0x000FFFD5 File Offset: 0x000FE1D5
	private void OnTriggerEnter2D(Collider2D col)
	{
		if (col.gameObject.tag == "Monkey" && this.player.state != MonkeyController2D.State.wasted)
		{
			this.Interakcija();
		}
	}

	// Token: 0x060024EA RID: 9450 RVA: 0x00100004 File Offset: 0x000FE204
	private void OnCollisionEnter2D(Collision2D col)
	{
		if (this.runAndJump && col.gameObject.tag == "Footer" && this.jumpNow)
		{
			this.anim.SetBool("Land", true);
			this.jumpNow = false;
			base.transform.parent = base.transform.parent.parent;
			this.anim.Play("Running");
		}
		if (col.gameObject.tag == "Monkey" && this.player.state != MonkeyController2D.State.wasted)
		{
			Debug.Log("OVDEJUSO");
			this.Interakcija();
		}
	}

	// Token: 0x060024EB RID: 9451 RVA: 0x001000B0 File Offset: 0x000FE2B0
	private IEnumerator destroyBabun()
	{
		base.transform.parent.parent.Find("+3CoinsHolder").GetComponent<Animator>().Play("FadeOutCoins");
		Manage.coinsCollected += 3;
		GameObject.Find("CoinsGamePlayText").GetComponent<TextMesh>().text = Manage.coinsCollected.ToString();
		yield return new WaitForSeconds(1.2f);
		if (MonkeyController2D.canRespawnThings)
		{
			if (!this.IdleUdaranje)
			{
				this.anim.Play(this.idle_state);
			}
			else
			{
				this.anim.Play(this.idle_udaranje_state);
			}
			this.colliders[0].enabled = true;
			this.colliders[1].enabled = true;
			this.reqHeight.GetComponent<KillTheBaboon>().turnOnColliders();
			base.transform.localPosition = this.baboonLocPos;
		}
		yield break;
	}

	// Token: 0x060024EC RID: 9452 RVA: 0x001000C0 File Offset: 0x000FE2C0
	public void killBaboonStuff()
	{
		base.GetComponent<Rigidbody2D>().isKinematic = true;
		if (PlaySounds.soundOn)
		{
			PlaySounds.Play_SmashBaboon();
		}
		this.anim.applyRootMotion = true;
		if (this.anim.GetBool("Land"))
		{
			this.anim.Play(this.death_state);
		}
		else
		{
			this.jump = false;
			this.anim.Play(this.death_state);
		}
		this.oblak.Play();
		this.player.GetComponent<Rigidbody2D>().velocity = new Vector2(this.player.maxSpeedX, 0f);
		this.player.GetComponent<Rigidbody2D>().AddForce(new Vector2(0f, 1500f));
		this.player.GetComponent<Rigidbody2D>().drag = 0f;
		this.player.canGlide = false;
		this.player.animator.Play(this.player.jump_State);
		base.StartCoroutine(this.destroyBabun());
	}

	// Token: 0x060024ED RID: 9453 RVA: 0x001001C8 File Offset: 0x000FE3C8
	private void Interakcija()
	{
		if (this.player.activeShield)
		{
			Debug.Log("Stitulj");
			this.reqHeight.GetComponent<KillTheBaboon>().turnOffColliders();
			base.GetComponent<Rigidbody2D>().isKinematic = true;
			if (PlaySounds.soundOn)
			{
				PlaySounds.Play_SmashBaboon();
			}
			this.anim.applyRootMotion = true;
			if (this.anim.GetBool("Land"))
			{
				this.anim.Play(this.death_state);
			}
			else
			{
				this.jump = false;
				this.anim.Play(this.death_state);
			}
			this.oblak.Play();
			this.colliders[0].enabled = false;
			this.colliders[1].enabled = false;
			this.player.GetComponent<Rigidbody2D>().velocity = new Vector2(this.player.maxSpeedX, 0f);
			if (this.player.activeShield)
			{
				this.player.activeShield = false;
				GameObject.Find("_GameManager").SendMessage("ApplyPowerUp", -3);
				if (this.player.state != MonkeyController2D.State.running)
				{
					this.player.GetComponent<Rigidbody2D>().AddForce(new Vector2(0f, 1500f));
					this.player.GetComponent<Rigidbody2D>().drag = 0f;
					this.player.canGlide = false;
					this.player.animator.Play(this.player.jump_State);
				}
			}
			else
			{
				this.player.GetComponent<Rigidbody2D>().AddForce(new Vector2(0f, 1500f));
			}
			base.StartCoroutine(this.destroyBabun());
			return;
		}
		if (!this.player.killed)
		{
			Debug.Log("nema stitulj");
			if (PlaySounds.soundOn)
			{
				PlaySounds.Play_SmashBaboon();
			}
			this.player.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
			this.player.killed = true;
			this.maxSpeedX = 0f;
			this.run = false;
			base.GetComponent<Rigidbody2D>().isKinematic = true;
			if (this.anim.GetBool("Land"))
			{
				this.anim.Play(this.strike_state);
			}
			this.oblak.Play();
			if (this.player.state == MonkeyController2D.State.running)
			{
				this.player.majmunUtepan();
				return;
			}
			this.player.majmunUtepanULetu();
		}
	}

	// Token: 0x04001D7D RID: 7549
	public bool IdleUdaranje;

	// Token: 0x04001D7E RID: 7550
	private Animator anim;

	// Token: 0x04001D7F RID: 7551
	private Animator parentAnim;

	// Token: 0x04001D80 RID: 7552
	public GameObject bureRaketa;

	// Token: 0x04001D81 RID: 7553
	public bool patrol;

	// Token: 0x04001D82 RID: 7554
	public bool jump;

	// Token: 0x04001D83 RID: 7555
	public bool fly;

	// Token: 0x04001D84 RID: 7556
	public bool shooting;

	// Token: 0x04001D85 RID: 7557
	public bool run;

	// Token: 0x04001D86 RID: 7558
	public bool runAndJump;

	// Token: 0x04001D87 RID: 7559
	public bool animateInstantly;

	// Token: 0x04001D88 RID: 7560
	private BoxCollider2D[] colliders;

	// Token: 0x04001D89 RID: 7561
	private bool fireEvent = true;

	// Token: 0x04001D8A RID: 7562
	private bool canJump;

	// Token: 0x04001D8B RID: 7563
	private bool canShoot;

	// Token: 0x04001D8C RID: 7564
	private bool proveraJednom = true;

	// Token: 0x04001D8D RID: 7565
	private bool jumpNow;

	// Token: 0x04001D8E RID: 7566
	private bool skocioOdmah;

	// Token: 0x04001D8F RID: 7567
	public ParticleSystem oblak;

	// Token: 0x04001D90 RID: 7568
	private MonkeyController2D player;

	// Token: 0x04001D91 RID: 7569
	private Transform babun;

	// Token: 0x04001D92 RID: 7570
	private Transform reqHeight;

	// Token: 0x04001D93 RID: 7571
	private int idle_state = Animator.StringToHash("Base Layer.Idle");

	// Token: 0x04001D94 RID: 7572
	private int idle_udaranje_state = Animator.StringToHash("Base Layer.Idle_Udaranje");

	// Token: 0x04001D95 RID: 7573
	private int death_state = Animator.StringToHash("Base Layer.Death");

	// Token: 0x04001D96 RID: 7574
	private int deathJump_state = Animator.StringToHash("Base Layer.DeathJump");

	// Token: 0x04001D97 RID: 7575
	private int strike_state = Animator.StringToHash("Base Layer.Strike_1");

	// Token: 0x04001D98 RID: 7576
	private float maxSpeedX = 15f;

	// Token: 0x04001D99 RID: 7577
	public float distance;

	// Token: 0x04001D9A RID: 7578
	private Vector3 baboonLocPos;
}
