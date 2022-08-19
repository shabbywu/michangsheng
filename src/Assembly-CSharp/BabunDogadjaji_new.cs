using System;
using System.Collections;
using UnityEngine;

// Token: 0x02000492 RID: 1170
public class BabunDogadjaji_new : MonoBehaviour
{
	// Token: 0x060024EF RID: 9455 RVA: 0x001004B0 File Offset: 0x000FE6B0
	private void Awake()
	{
		this.player = GameObject.FindGameObjectWithTag("Monkey").GetComponent<MonkeyController2D>();
		this.coinsCollectedText = GameObject.Find("CoinsGamePlayText").GetComponent<TextMesh>();
		this.babun = base.transform;
		this.reqHeight = base.transform.parent.Find("_BabunNadrlja");
		this.anim = this.babun.parent.GetComponent<Animator>();
		this.colliders = base.GetComponents<CircleCollider2D>();
		this.fireEvent = false;
		this.parentRigidbody2D = base.transform.parent.GetComponent<Rigidbody2D>();
		this.pravac = -Vector2.right;
		this.pravacFly = Vector2.up;
		if (this.runAndJump)
		{
			this.baboonShadow = base.transform.parent.Find("shadow");
		}
		this.baboonRealOrgPos = base.transform.parent.localPosition;
		if (base.transform.parent.parent.name.Contains("Gorilla"))
		{
			this.isGorilla = true;
		}
		this.PogasiBabuna();
	}

	// Token: 0x060024F0 RID: 9456 RVA: 0x00004095 File Offset: 0x00002295
	private void Start()
	{
	}

	// Token: 0x060024F1 RID: 9457 RVA: 0x001005D0 File Offset: 0x000FE7D0
	private void Update()
	{
		this.hit = Physics2D.Linecast(base.transform.position + new Vector3(0.8f, 1f, 0f), base.transform.position + new Vector3(0.8f, -35.5f, 0f), 1 << LayerMask.NameToLayer("Ground") | 1 << LayerMask.NameToLayer("Platform"));
		if (this.hit)
		{
			this.senka.position = new Vector3(this.senka.position.x, this.hit.point.y - 0f, this.senka.position.z);
		}
		if (base.transform.position.x + 5f < Camera.main.ViewportToWorldPoint(Vector3.zero).x && !this.ugasen)
		{
			this.ugasen = true;
			this.PogasiBabuna();
		}
		if (this.runAndJump && base.transform.position.x < Camera.main.ViewportToWorldPoint(Vector3.one).x - 30f && !this.skocioOdmah)
		{
			this.anim.Play("Jump");
			this.parentRigidbody2D.velocity = new Vector2(-this.maxSpeedX * 0.7f, 0f);
			this.parentRigidbody2D.velocity = new Vector2(-this.maxSpeedX * 0.7f, 43f);
			this.jumpNow = true;
			this.skocioOdmah = true;
			this.anim.SetBool("Land", false);
		}
		if ((base.transform.position.x < Camera.main.ViewportToWorldPoint(Vector3.one).x + this.distance || this.animateInstantly) && this.proveraJednom)
		{
			this.proveraJednom = false;
			this.anim.enabled = true;
			this.fireEvent = true;
			this.ugasen = false;
		}
		base.transform.parent.name.Equals("BaboonRealll");
		if (this.patrol)
		{
			if (this.fireEvent)
			{
				this.fireEvent = false;
				this.parentRigidbody2D.isKinematic = false;
				this.anim.Play("Walking_left");
				this.patrolinjo = true;
				base.InvokeRepeating("ObrniSe", 2.65f, 2.65f);
				return;
			}
		}
		else if (this.run)
		{
			if (this.fireEvent)
			{
				this.fireEvent = false;
				this.anim.Play("Running");
				this.parentRigidbody2D.isKinematic = false;
				this.runinjo = true;
				return;
			}
		}
		else if (this.jump)
		{
			if (this.fireEvent)
			{
				this.fireEvent = false;
				this.anim.Play("Jump2");
				this.anim.SetBool("Land", false);
				this.anim.applyRootMotion = true;
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
				this.anim.Play("Fly");
				this.flyinjo = true;
				base.InvokeRepeating("ObrniSeVertikalno", 1.5f, 1.5f);
				return;
			}
		}
		else if (this.runAndJump)
		{
			if (this.fireEvent)
			{
				this.fireEvent = false;
				this.anim.Play("Running");
				this.parentRigidbody2D.isKinematic = false;
				this.runinjo = true;
				return;
			}
		}
		else if (this.IdleUdaranje)
		{
			if (this.fireEvent)
			{
				this.fireEvent = false;
				if (Random.Range(1, 3) > 1)
				{
					this.anim.Play(this.idle_udaranje_state);
					return;
				}
				this.anim.Play(this.idle_state);
				return;
			}
		}
		else if (this.udaranjePoGrudi)
		{
			if (this.fireEvent)
			{
				this.fireEvent = false;
				this.anim.Play("Chest_drum");
				return;
			}
		}
		else if (this.koplje)
		{
			if (this.fireEvent)
			{
				this.fireEvent = false;
				this.anim.Play("koplje");
				return;
			}
		}
		else if (this.boomerang && this.fireEvent)
		{
			this.fireEvent = false;
			this.anim.Play("Boomerang");
		}
	}

	// Token: 0x060024F2 RID: 9458 RVA: 0x00100A54 File Offset: 0x000FEC54
	private void FixedUpdate()
	{
		if (this.patrolinjo)
		{
			this.parentRigidbody2D.velocity = new Vector2(this.pravac.x * 5f, this.parentRigidbody2D.velocity.y);
			if (this.parentRigidbody2D.velocity.y > 2f)
			{
				this.parentRigidbody2D.velocity = new Vector2(this.pravac.x * 6.25f, this.parentRigidbody2D.velocity.y);
				return;
			}
			if (this.parentRigidbody2D.velocity.y < -2f)
			{
				this.parentRigidbody2D.velocity = new Vector2(this.pravac.x * 3.75f, this.parentRigidbody2D.velocity.y);
				return;
			}
		}
		else
		{
			if (this.flyinjo)
			{
				base.transform.parent.Translate(this.pravacFly * Time.deltaTime * 3f);
				return;
			}
			if ((this.run || this.runAndJump) && this.runinjo)
			{
				if (this.jumpNow)
				{
					this.parentRigidbody2D.velocity = new Vector2(-this.maxSpeedX * 0.45f, this.parentRigidbody2D.velocity.y);
				}
				else
				{
					this.parentRigidbody2D.velocity = new Vector2(-this.maxSpeedX, this.parentRigidbody2D.velocity.y);
				}
				if (this.runAndJump)
				{
					this.hit = Physics2D.Linecast(base.transform.position + new Vector3(0.8f, 1f, 0f), base.transform.position + new Vector3(0.8f, -35.5f, 0f), 1 << LayerMask.NameToLayer("Ground") | 1 << LayerMask.NameToLayer("Platform"));
					if (this.hit)
					{
						this.baboonShadow.position = new Vector3(this.baboonShadow.position.x, this.hit.point.y + 0.3f, this.baboonShadow.position.z);
					}
				}
			}
		}
	}

	// Token: 0x060024F3 RID: 9459 RVA: 0x00100CC0 File Offset: 0x000FEEC0
	private void ObrniSe()
	{
		this.pravac = -this.pravac;
		this.anim.SetBool("changeSide", !this.anim.GetBool("changeSide"));
	}

	// Token: 0x060024F4 RID: 9460 RVA: 0x00100CF6 File Offset: 0x000FEEF6
	private void ObrniSeVertikalno()
	{
		this.pravacFly = -this.pravacFly;
	}

	// Token: 0x060024F5 RID: 9461 RVA: 0x00100D09 File Offset: 0x000FEF09
	private void SobaliJump()
	{
		this.canJump = false;
	}

	// Token: 0x060024F6 RID: 9462 RVA: 0x00100D12 File Offset: 0x000FEF12
	private void OdvaliJump()
	{
		this.fireEvent = true;
	}

	// Token: 0x060024F7 RID: 9463 RVA: 0x00100D1B File Offset: 0x000FEF1B
	private IEnumerator Patrol()
	{
		yield return null;
		yield break;
	}

	// Token: 0x060024F8 RID: 9464 RVA: 0x00100D24 File Offset: 0x000FEF24
	private void OnTriggerEnter2D(Collider2D col)
	{
		if (col.gameObject.tag == "Monkey" && this.player.state != MonkeyController2D.State.wasted)
		{
			this.Interakcija();
			return;
		}
		if (col.name == "Impact")
		{
			this.impact = true;
			this.reqHeight.GetComponent<KillTheBaboon>().turnOffColliders();
			this.killBaboonStuff();
		}
	}

	// Token: 0x060024F9 RID: 9465 RVA: 0x00100D8C File Offset: 0x000FEF8C
	private void OnCollisionEnter2D(Collision2D col)
	{
		if (this.runAndJump && col.gameObject.tag == "Footer" && this.jumpNow)
		{
			this.anim.SetBool("Land", true);
			this.jumpNow = false;
			this.anim.Play("Running");
		}
		if (col.gameObject.tag == "Monkey" && this.player.state != MonkeyController2D.State.wasted)
		{
			this.Interakcija();
		}
	}

	// Token: 0x060024FA RID: 9466 RVA: 0x00100E13 File Offset: 0x000FF013
	private IEnumerator destroyBabun()
	{
		if (this.isGorilla)
		{
			Manage.gorillasKilled++;
			MissionManager.Instance.GorillaEvent(Manage.gorillasKilled);
			this.kolicinaPoena = 40;
			if (this.fly)
			{
				Manage.fly_GorillasKilled++;
				MissionManager.Instance.Fly_GorillaEvent(Manage.fly_GorillasKilled);
				this.kolicinaPoena = 60;
			}
			else if (this.koplje)
			{
				Manage.koplje_GorillasKilled++;
				MissionManager.Instance.Koplje_GorillaEvent(Manage.koplje_GorillasKilled);
				this.kolicinaPoena = 70;
				this.Koplje.GetComponent<Collider2D>().enabled = false;
			}
			else if (this.jump)
			{
				this.kolicinaPoena = 50;
			}
			else if (this.patrol)
			{
				this.kolicinaPoena = 40;
			}
			else if (this.run)
			{
				this.kolicinaPoena = 70;
			}
			else if (this.runAndJump)
			{
				this.kolicinaPoena = 80;
			}
		}
		else
		{
			Manage.baboonsKilled++;
			MissionManager.Instance.BaboonEvent(Manage.baboonsKilled);
			this.kolicinaPoena = 40;
			if (this.fly)
			{
				Manage.fly_BaboonsKilled++;
				MissionManager.Instance.Fly_BaboonEvent(Manage.fly_BaboonsKilled);
				this.kolicinaPoena = 60;
			}
			else if (this.boomerang)
			{
				Manage.boomerang_BaboonsKilled++;
				MissionManager.Instance.Boomerang_BaboonEvent(Manage.boomerang_BaboonsKilled);
				this.kolicinaPoena = 70;
				this.Boomerang.SetActive(false);
			}
			else if (this.jump)
			{
				this.kolicinaPoena = 50;
			}
			else if (this.patrol)
			{
				this.kolicinaPoena = 40;
			}
			else if (this.run)
			{
				this.kolicinaPoena = 70;
			}
			else if (this.runAndJump)
			{
				this.kolicinaPoena = 80;
			}
		}
		if (this.fly || this.run)
		{
			this.senka.GetComponent<Renderer>().enabled = false;
		}
		if (PlaySounds.soundOn)
		{
			PlaySounds.Play_SmashBaboon();
		}
		int num = 3;
		if (this.jump)
		{
			num = 4;
		}
		else if (this.fly)
		{
			num = 4;
		}
		else if (this.patrol)
		{
			num = 3;
		}
		else if (this.run)
		{
			num = 4;
		}
		else if (this.runAndJump)
		{
			num = 5;
		}
		else if (this.boomerang || this.koplje)
		{
			num = 6;
		}
		Transform transform = base.transform.parent.Find("+3CoinsHolder");
		transform.Find("+3Coins").GetComponent<TextMesh>().text = (transform.Find("+3Coins/+3CoinsShadow").GetComponent<TextMesh>().text = "+" + num);
		transform.parent = base.transform.parent.parent;
		transform.GetComponent<Animator>().Play("FadeOutCoins");
		Manage.coinsCollected += num;
		MissionManager.Instance.CoinEvent(Manage.coinsCollected);
		this.coinsCollectedText.text = Manage.coinsCollected.ToString();
		this.coinsCollectedText.GetComponent<TextMeshEffects>().RefreshTextOutline(false, true, true);
		Manage.points += this.kolicinaPoena;
		Manage.pointsText.text = Manage.points.ToString();
		Manage.pointsEffects.RefreshTextOutline(false, true, true);
		yield return new WaitForSeconds(1.2f);
		base.transform.parent.parent.Find("+3CoinsHolder").parent = base.transform.parent;
		yield break;
	}

	// Token: 0x060024FB RID: 9467 RVA: 0x00100E24 File Offset: 0x000FF024
	public void killBaboonStuff()
	{
		if (this.player.powerfullImpact)
		{
			this.player.cancelPowerfullImpact();
		}
		if (this.patrolinjo)
		{
			this.patrolinjo = false;
		}
		else if (this.flyinjo)
		{
			this.flyinjo = false;
		}
		else if (this.runinjo)
		{
			this.runinjo = false;
		}
		if (this.parentRigidbody2D != null)
		{
			this.parentRigidbody2D.isKinematic = true;
		}
		this.anim.applyRootMotion = true;
		if (this.anim.GetBool("Land"))
		{
			if (!this.runAndJump)
			{
				this.anim.Play(this.death_state);
			}
			else
			{
				this.anim.Play(this.deathJump_state);
				base.Invoke("UgasiBabunaPoslePada", 1f);
			}
		}
		else
		{
			this.anim.Play(this.deathJump_state);
			base.Invoke("UgasiBabunaPoslePada", 1f);
		}
		this.oblak.Play();
		this.colliders[0].enabled = false;
		this.colliders[1].enabled = false;
		if (!this.impact)
		{
			base.StartCoroutine(this.bounceOffEnemy());
		}
		else
		{
			this.impact = false;
		}
		this.player.GetComponent<Rigidbody2D>().drag = 0f;
		this.player.canGlide = false;
		base.StartCoroutine(this.destroyBabun());
	}

	// Token: 0x060024FC RID: 9468 RVA: 0x00100F84 File Offset: 0x000FF184
	private IEnumerator bounceOffEnemy()
	{
		yield return new WaitForFixedUpdate();
		if (!this.player.isSliding)
		{
			this.player.GetComponent<Rigidbody2D>().velocity = new Vector2(this.player.maxSpeedX, 0f);
			this.player.GetComponent<Rigidbody2D>().AddForce(new Vector2(0f, 1500f));
			this.player.animator.Play(this.player.jump_State);
		}
		else if (this.player.state != MonkeyController2D.State.running)
		{
			this.player.GetComponent<Rigidbody2D>().velocity = new Vector2(this.player.maxSpeedX, 0f);
			this.player.GetComponent<Rigidbody2D>().AddForce(new Vector2(0f, -2500f));
		}
		yield break;
	}

	// Token: 0x060024FD RID: 9469 RVA: 0x00100F94 File Offset: 0x000FF194
	private void Interakcija()
	{
		if (this.patrolinjo)
		{
			this.patrolinjo = false;
		}
		else if (this.flyinjo)
		{
			this.flyinjo = false;
		}
		if (this.player.activeShield || this.player.invincible || this.player.powerfullImpact)
		{
			if (this.runinjo)
			{
				this.runinjo = false;
			}
			this.reqHeight.GetComponent<KillTheBaboon>().turnOffColliders();
			if (this.parentRigidbody2D != null)
			{
				this.parentRigidbody2D.isKinematic = true;
			}
			this.anim.applyRootMotion = true;
			if (this.anim.GetBool("Land"))
			{
				this.anim.Play(this.death_state);
			}
			else
			{
				this.anim.Play(this.deathJump_state);
				base.Invoke("UgasiBabunaPoslePada", 1f);
			}
			this.oblak.Play();
			this.colliders[0].enabled = false;
			this.colliders[1].enabled = false;
			this.player.GetComponent<Rigidbody2D>().velocity = new Vector2(this.player.maxSpeedX, 0f);
			if (this.player.activeShield)
			{
				if (PlaySounds.soundOn)
				{
					PlaySounds.Play_LooseShield();
				}
				this.player.activeShield = false;
				this.player.transform.Find("Particles/ShieldDestroyParticle").GetComponent<ParticleSystem>().Play();
				this.player.transform.Find("Particles/ShieldDestroyParticle").GetChild(0).GetComponent<ParticleSystem>().Play();
				GameObject.Find("_GameManager").SendMessage("ApplyPowerUp", -3);
				if (this.player.state != MonkeyController2D.State.running)
				{
					this.player.GetComponent<Rigidbody2D>().drag = 0f;
					this.player.canGlide = false;
					this.player.animator.Play(this.player.jump_State);
				}
			}
			else if (!this.player.isSliding && this.player.state != MonkeyController2D.State.running)
			{
				this.player.GetComponent<Rigidbody2D>().AddForce(new Vector2(0f, 1500f));
			}
			base.StartCoroutine(this.destroyBabun());
			return;
		}
		if (!this.player.killed)
		{
			if (PlaySounds.soundOn)
			{
				PlaySounds.Play_SmashBaboon();
			}
			this.player.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
			this.player.killed = true;
			if (this.run)
			{
				this.run = false;
				this.runTurnedOff = true;
			}
			this.reqHeight.GetComponent<KillTheBaboon>().turnOffColliders();
			this.colliders[0].enabled = false;
			this.colliders[1].enabled = false;
			if (this.anim.GetBool("Land"))
			{
				if (!this.run)
				{
					this.anim.Play(this.strike_state);
					if (this.parentRigidbody2D != null)
					{
						this.parentRigidbody2D.isKinematic = true;
					}
					this.maxSpeedX = 0f;
				}
			}
			else
			{
				base.Invoke("UkljuciCollidereOpet", 0.35f);
			}
			this.oblak.Play();
			if (this.player.state == MonkeyController2D.State.running)
			{
				this.player.majmunUtepan();
			}
			else
			{
				this.player.majmunUtepanULetu();
			}
			this.kontrolaZaBrzinuY = true;
		}
	}

	// Token: 0x060024FE RID: 9470 RVA: 0x001012F3 File Offset: 0x000FF4F3
	private void UkljuciCollidereOpet()
	{
		this.colliders[0].enabled = true;
		this.colliders[1].enabled = true;
	}

	// Token: 0x060024FF RID: 9471 RVA: 0x00101311 File Offset: 0x000FF511
	private void resetujKontroluZaBrzinu()
	{
		this.kontrolaZaBrzinuY = false;
	}

	// Token: 0x06002500 RID: 9472 RVA: 0x0010131C File Offset: 0x000FF51C
	private void PogasiBabuna()
	{
		if (this.fly)
		{
			if (this.bureRaketa.activeSelf)
			{
				this.bureRaketa.SetActive(false);
			}
			base.CancelInvoke("ObrniSeVertikalno");
		}
		else if (this.patrol)
		{
			base.CancelInvoke("ObrniSe");
			this.anim.Play("New State");
			this.anim.SetBool("changeSide", false);
			this.pravac = -Vector2.right;
		}
		else if (this.jump)
		{
			this.anim.Play("New State");
		}
		if (this.parentRigidbody2D != null)
		{
			this.parentRigidbody2D.isKinematic = true;
		}
		this.colliders[0].enabled = false;
		this.colliders[1].enabled = false;
		base.transform.parent.Find("Baboon").GetComponent<Renderer>().enabled = false;
		this.anim.enabled = false;
		this.patrolinjo = false;
		this.flyinjo = false;
		this.runinjo = false;
		this.anim.applyRootMotion = false;
		base.enabled = false;
	}

	// Token: 0x06002501 RID: 9473 RVA: 0x00101440 File Offset: 0x000FF640
	private void UgasiBabunaPoslePada()
	{
		this.oblak.Play();
		base.transform.parent.Find("Baboon").GetComponent<Renderer>().enabled = false;
		if (this.fly)
		{
			this.bureRaketa.SetActive(false);
		}
	}

	// Token: 0x06002502 RID: 9474 RVA: 0x0010148C File Offset: 0x000FF68C
	public void ResetujBabuna()
	{
		base.transform.parent.localPosition = this.baboonRealOrgPos;
		this.colliders[0].enabled = true;
		this.colliders[1].enabled = true;
		this.reqHeight.GetComponent<KillTheBaboon>().turnOnColliders();
		base.transform.parent.Find("Baboon").GetComponent<Renderer>().enabled = true;
		this.anim.enabled = true;
		base.enabled = true;
		this.ugasen = false;
		this.proveraJednom = true;
		this.skocioOdmah = false;
		if (this.fly)
		{
			this.bureRaketa.SetActive(true);
		}
		if (this.boomerang)
		{
			this.Boomerang.SetActive(true);
		}
		if (this.koplje)
		{
			this.Koplje.GetComponent<Collider2D>().enabled = true;
		}
		if (this.runTurnedOff)
		{
			this.runTurnedOff = false;
			this.run = true;
		}
		if (this.fly || this.run)
		{
			this.senka.GetComponent<Renderer>().enabled = true;
		}
		this.maxSpeedX = 17f;
	}

	// Token: 0x04001D9B RID: 7579
	public bool IdleUdaranje;

	// Token: 0x04001D9C RID: 7580
	private Animator anim;

	// Token: 0x04001D9D RID: 7581
	private Animator parentAnim;

	// Token: 0x04001D9E RID: 7582
	public GameObject bureRaketa;

	// Token: 0x04001D9F RID: 7583
	public Transform senka;

	// Token: 0x04001DA0 RID: 7584
	public bool patrol;

	// Token: 0x04001DA1 RID: 7585
	public bool jump;

	// Token: 0x04001DA2 RID: 7586
	public bool fly;

	// Token: 0x04001DA3 RID: 7587
	public bool shooting;

	// Token: 0x04001DA4 RID: 7588
	public bool run;

	// Token: 0x04001DA5 RID: 7589
	public bool runAndJump;

	// Token: 0x04001DA6 RID: 7590
	public bool animateInstantly;

	// Token: 0x04001DA7 RID: 7591
	private CircleCollider2D[] colliders;

	// Token: 0x04001DA8 RID: 7592
	private bool fireEvent = true;

	// Token: 0x04001DA9 RID: 7593
	private bool canJump;

	// Token: 0x04001DAA RID: 7594
	private bool canShoot;

	// Token: 0x04001DAB RID: 7595
	private bool proveraJednom = true;

	// Token: 0x04001DAC RID: 7596
	private bool jumpNow;

	// Token: 0x04001DAD RID: 7597
	private bool skocioOdmah;

	// Token: 0x04001DAE RID: 7598
	public ParticleSystem oblak;

	// Token: 0x04001DAF RID: 7599
	private MonkeyController2D player;

	// Token: 0x04001DB0 RID: 7600
	private Transform babun;

	// Token: 0x04001DB1 RID: 7601
	private Transform reqHeight;

	// Token: 0x04001DB2 RID: 7602
	private int idle_state = Animator.StringToHash("Base Layer.Idle");

	// Token: 0x04001DB3 RID: 7603
	private int idle_udaranje_state = Animator.StringToHash("Base Layer.Idle_Udaranje");

	// Token: 0x04001DB4 RID: 7604
	private int death_state = Animator.StringToHash("Base Layer.Death");

	// Token: 0x04001DB5 RID: 7605
	private int deathJump_state = Animator.StringToHash("Base Layer.DeathJump");

	// Token: 0x04001DB6 RID: 7606
	private int strike_state = Animator.StringToHash("Base Layer.Strike_1");

	// Token: 0x04001DB7 RID: 7607
	public float maxSpeedX = 17f;

	// Token: 0x04001DB8 RID: 7608
	public float distance;

	// Token: 0x04001DB9 RID: 7609
	private Vector3 baboonLocPos;

	// Token: 0x04001DBA RID: 7610
	private Vector2 pravac;

	// Token: 0x04001DBB RID: 7611
	private Vector2 pravacFly;

	// Token: 0x04001DBC RID: 7612
	private bool patrolinjo;

	// Token: 0x04001DBD RID: 7613
	private bool flyinjo;

	// Token: 0x04001DBE RID: 7614
	private bool runinjo;

	// Token: 0x04001DBF RID: 7615
	private Rigidbody2D parentRigidbody2D;

	// Token: 0x04001DC0 RID: 7616
	private Transform baboonShadow;

	// Token: 0x04001DC1 RID: 7617
	private RaycastHit2D hit;

	// Token: 0x04001DC2 RID: 7618
	public bool canDo;

	// Token: 0x04001DC3 RID: 7619
	public Vector3 baboonRealOrgPos;

	// Token: 0x04001DC4 RID: 7620
	private bool kontrolaZaBrzinuY;

	// Token: 0x04001DC5 RID: 7621
	private float smanjivac;

	// Token: 0x04001DC6 RID: 7622
	private bool ugasen = true;

	// Token: 0x04001DC7 RID: 7623
	private bool impact;

	// Token: 0x04001DC8 RID: 7624
	private TextMesh coinsCollectedText;

	// Token: 0x04001DC9 RID: 7625
	public GameObject Koplje;

	// Token: 0x04001DCA RID: 7626
	public GameObject Boomerang;

	// Token: 0x04001DCB RID: 7627
	public bool koplje;

	// Token: 0x04001DCC RID: 7628
	public bool udaranjePoGrudi;

	// Token: 0x04001DCD RID: 7629
	public bool boomerang;

	// Token: 0x04001DCE RID: 7630
	private bool isGorilla;

	// Token: 0x04001DCF RID: 7631
	private int kolicinaPoena;

	// Token: 0x04001DD0 RID: 7632
	private bool runTurnedOff;
}
