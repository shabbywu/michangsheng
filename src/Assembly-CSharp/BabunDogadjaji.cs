using System;
using System.Collections;
using UnityEngine;

// Token: 0x02000665 RID: 1637
public class BabunDogadjaji : MonoBehaviour
{
	// Token: 0x060028DA RID: 10458 RVA: 0x0013F38C File Offset: 0x0013D58C
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

	// Token: 0x060028DB RID: 10459 RVA: 0x0001FDAC File Offset: 0x0001DFAC
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

	// Token: 0x060028DC RID: 10460 RVA: 0x0013F400 File Offset: 0x0013D600
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

	// Token: 0x060028DD RID: 10461 RVA: 0x0013F620 File Offset: 0x0013D820
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

	// Token: 0x060028DE RID: 10462 RVA: 0x0001FDEB File Offset: 0x0001DFEB
	private void SobaliJump()
	{
		this.canJump = false;
	}

	// Token: 0x060028DF RID: 10463 RVA: 0x0001FDF4 File Offset: 0x0001DFF4
	private void OdvaliJump()
	{
		this.fireEvent = true;
	}

	// Token: 0x060028E0 RID: 10464 RVA: 0x0001FDFD File Offset: 0x0001DFFD
	private IEnumerator Patrol()
	{
		yield return null;
		yield break;
	}

	// Token: 0x060028E1 RID: 10465 RVA: 0x0001FE05 File Offset: 0x0001E005
	private void OnTriggerEnter2D(Collider2D col)
	{
		if (col.gameObject.tag == "Monkey" && this.player.state != MonkeyController2D.State.wasted)
		{
			this.Interakcija();
		}
	}

	// Token: 0x060028E2 RID: 10466 RVA: 0x0013F69C File Offset: 0x0013D89C
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

	// Token: 0x060028E3 RID: 10467 RVA: 0x0001FE32 File Offset: 0x0001E032
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

	// Token: 0x060028E4 RID: 10468 RVA: 0x0013F748 File Offset: 0x0013D948
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

	// Token: 0x060028E5 RID: 10469 RVA: 0x0013F850 File Offset: 0x0013DA50
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

	// Token: 0x0400227E RID: 8830
	public bool IdleUdaranje;

	// Token: 0x0400227F RID: 8831
	private Animator anim;

	// Token: 0x04002280 RID: 8832
	private Animator parentAnim;

	// Token: 0x04002281 RID: 8833
	public GameObject bureRaketa;

	// Token: 0x04002282 RID: 8834
	public bool patrol;

	// Token: 0x04002283 RID: 8835
	public bool jump;

	// Token: 0x04002284 RID: 8836
	public bool fly;

	// Token: 0x04002285 RID: 8837
	public bool shooting;

	// Token: 0x04002286 RID: 8838
	public bool run;

	// Token: 0x04002287 RID: 8839
	public bool runAndJump;

	// Token: 0x04002288 RID: 8840
	public bool animateInstantly;

	// Token: 0x04002289 RID: 8841
	private BoxCollider2D[] colliders;

	// Token: 0x0400228A RID: 8842
	private bool fireEvent = true;

	// Token: 0x0400228B RID: 8843
	private bool canJump;

	// Token: 0x0400228C RID: 8844
	private bool canShoot;

	// Token: 0x0400228D RID: 8845
	private bool proveraJednom = true;

	// Token: 0x0400228E RID: 8846
	private bool jumpNow;

	// Token: 0x0400228F RID: 8847
	private bool skocioOdmah;

	// Token: 0x04002290 RID: 8848
	public ParticleSystem oblak;

	// Token: 0x04002291 RID: 8849
	private MonkeyController2D player;

	// Token: 0x04002292 RID: 8850
	private Transform babun;

	// Token: 0x04002293 RID: 8851
	private Transform reqHeight;

	// Token: 0x04002294 RID: 8852
	private int idle_state = Animator.StringToHash("Base Layer.Idle");

	// Token: 0x04002295 RID: 8853
	private int idle_udaranje_state = Animator.StringToHash("Base Layer.Idle_Udaranje");

	// Token: 0x04002296 RID: 8854
	private int death_state = Animator.StringToHash("Base Layer.Death");

	// Token: 0x04002297 RID: 8855
	private int deathJump_state = Animator.StringToHash("Base Layer.DeathJump");

	// Token: 0x04002298 RID: 8856
	private int strike_state = Animator.StringToHash("Base Layer.Strike_1");

	// Token: 0x04002299 RID: 8857
	private float maxSpeedX = 15f;

	// Token: 0x0400229A RID: 8858
	public float distance;

	// Token: 0x0400229B RID: 8859
	private Vector3 baboonLocPos;
}
