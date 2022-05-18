using System;
using System.Collections;
using UnityEngine;

// Token: 0x02000668 RID: 1640
public class BabunDogadjaji_new : MonoBehaviour
{
	// Token: 0x060028F3 RID: 10483 RVA: 0x0013FC84 File Offset: 0x0013DE84
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

	// Token: 0x060028F4 RID: 10484 RVA: 0x000042DD File Offset: 0x000024DD
	private void Start()
	{
	}

	// Token: 0x060028F5 RID: 10485 RVA: 0x0013FDA4 File Offset: 0x0013DFA4
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

	// Token: 0x060028F6 RID: 10486 RVA: 0x00140228 File Offset: 0x0013E428
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

	// Token: 0x060028F7 RID: 10487 RVA: 0x0001FE6F File Offset: 0x0001E06F
	private void ObrniSe()
	{
		this.pravac = -this.pravac;
		this.anim.SetBool("changeSide", !this.anim.GetBool("changeSide"));
	}

	// Token: 0x060028F8 RID: 10488 RVA: 0x0001FEA5 File Offset: 0x0001E0A5
	private void ObrniSeVertikalno()
	{
		this.pravacFly = -this.pravacFly;
	}

	// Token: 0x060028F9 RID: 10489 RVA: 0x0001FEB8 File Offset: 0x0001E0B8
	private void SobaliJump()
	{
		this.canJump = false;
	}

	// Token: 0x060028FA RID: 10490 RVA: 0x0001FEC1 File Offset: 0x0001E0C1
	private void OdvaliJump()
	{
		this.fireEvent = true;
	}

	// Token: 0x060028FB RID: 10491 RVA: 0x0001FECA File Offset: 0x0001E0CA
	private IEnumerator Patrol()
	{
		yield return null;
		yield break;
	}

	// Token: 0x060028FC RID: 10492 RVA: 0x00140494 File Offset: 0x0013E694
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

	// Token: 0x060028FD RID: 10493 RVA: 0x001404FC File Offset: 0x0013E6FC
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

	// Token: 0x060028FE RID: 10494 RVA: 0x0001FED2 File Offset: 0x0001E0D2
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

	// Token: 0x060028FF RID: 10495 RVA: 0x00140584 File Offset: 0x0013E784
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

	// Token: 0x06002900 RID: 10496 RVA: 0x0001FEE1 File Offset: 0x0001E0E1
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

	// Token: 0x06002901 RID: 10497 RVA: 0x001406E4 File Offset: 0x0013E8E4
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

	// Token: 0x06002902 RID: 10498 RVA: 0x0001FEF0 File Offset: 0x0001E0F0
	private void UkljuciCollidereOpet()
	{
		this.colliders[0].enabled = true;
		this.colliders[1].enabled = true;
	}

	// Token: 0x06002903 RID: 10499 RVA: 0x0001FF0E File Offset: 0x0001E10E
	private void resetujKontroluZaBrzinu()
	{
		this.kontrolaZaBrzinuY = false;
	}

	// Token: 0x06002904 RID: 10500 RVA: 0x00140A44 File Offset: 0x0013EC44
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

	// Token: 0x06002905 RID: 10501 RVA: 0x00140B68 File Offset: 0x0013ED68
	private void UgasiBabunaPoslePada()
	{
		this.oblak.Play();
		base.transform.parent.Find("Baboon").GetComponent<Renderer>().enabled = false;
		if (this.fly)
		{
			this.bureRaketa.SetActive(false);
		}
	}

	// Token: 0x06002906 RID: 10502 RVA: 0x00140BB4 File Offset: 0x0013EDB4
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

	// Token: 0x040022A1 RID: 8865
	public bool IdleUdaranje;

	// Token: 0x040022A2 RID: 8866
	private Animator anim;

	// Token: 0x040022A3 RID: 8867
	private Animator parentAnim;

	// Token: 0x040022A4 RID: 8868
	public GameObject bureRaketa;

	// Token: 0x040022A5 RID: 8869
	public Transform senka;

	// Token: 0x040022A6 RID: 8870
	public bool patrol;

	// Token: 0x040022A7 RID: 8871
	public bool jump;

	// Token: 0x040022A8 RID: 8872
	public bool fly;

	// Token: 0x040022A9 RID: 8873
	public bool shooting;

	// Token: 0x040022AA RID: 8874
	public bool run;

	// Token: 0x040022AB RID: 8875
	public bool runAndJump;

	// Token: 0x040022AC RID: 8876
	public bool animateInstantly;

	// Token: 0x040022AD RID: 8877
	private CircleCollider2D[] colliders;

	// Token: 0x040022AE RID: 8878
	private bool fireEvent = true;

	// Token: 0x040022AF RID: 8879
	private bool canJump;

	// Token: 0x040022B0 RID: 8880
	private bool canShoot;

	// Token: 0x040022B1 RID: 8881
	private bool proveraJednom = true;

	// Token: 0x040022B2 RID: 8882
	private bool jumpNow;

	// Token: 0x040022B3 RID: 8883
	private bool skocioOdmah;

	// Token: 0x040022B4 RID: 8884
	public ParticleSystem oblak;

	// Token: 0x040022B5 RID: 8885
	private MonkeyController2D player;

	// Token: 0x040022B6 RID: 8886
	private Transform babun;

	// Token: 0x040022B7 RID: 8887
	private Transform reqHeight;

	// Token: 0x040022B8 RID: 8888
	private int idle_state = Animator.StringToHash("Base Layer.Idle");

	// Token: 0x040022B9 RID: 8889
	private int idle_udaranje_state = Animator.StringToHash("Base Layer.Idle_Udaranje");

	// Token: 0x040022BA RID: 8890
	private int death_state = Animator.StringToHash("Base Layer.Death");

	// Token: 0x040022BB RID: 8891
	private int deathJump_state = Animator.StringToHash("Base Layer.DeathJump");

	// Token: 0x040022BC RID: 8892
	private int strike_state = Animator.StringToHash("Base Layer.Strike_1");

	// Token: 0x040022BD RID: 8893
	public float maxSpeedX = 17f;

	// Token: 0x040022BE RID: 8894
	public float distance;

	// Token: 0x040022BF RID: 8895
	private Vector3 baboonLocPos;

	// Token: 0x040022C0 RID: 8896
	private Vector2 pravac;

	// Token: 0x040022C1 RID: 8897
	private Vector2 pravacFly;

	// Token: 0x040022C2 RID: 8898
	private bool patrolinjo;

	// Token: 0x040022C3 RID: 8899
	private bool flyinjo;

	// Token: 0x040022C4 RID: 8900
	private bool runinjo;

	// Token: 0x040022C5 RID: 8901
	private Rigidbody2D parentRigidbody2D;

	// Token: 0x040022C6 RID: 8902
	private Transform baboonShadow;

	// Token: 0x040022C7 RID: 8903
	private RaycastHit2D hit;

	// Token: 0x040022C8 RID: 8904
	public bool canDo;

	// Token: 0x040022C9 RID: 8905
	public Vector3 baboonRealOrgPos;

	// Token: 0x040022CA RID: 8906
	private bool kontrolaZaBrzinuY;

	// Token: 0x040022CB RID: 8907
	private float smanjivac;

	// Token: 0x040022CC RID: 8908
	private bool ugasen = true;

	// Token: 0x040022CD RID: 8909
	private bool impact;

	// Token: 0x040022CE RID: 8910
	private TextMesh coinsCollectedText;

	// Token: 0x040022CF RID: 8911
	public GameObject Koplje;

	// Token: 0x040022D0 RID: 8912
	public GameObject Boomerang;

	// Token: 0x040022D1 RID: 8913
	public bool koplje;

	// Token: 0x040022D2 RID: 8914
	public bool udaranjePoGrudi;

	// Token: 0x040022D3 RID: 8915
	public bool boomerang;

	// Token: 0x040022D4 RID: 8916
	private bool isGorilla;

	// Token: 0x040022D5 RID: 8917
	private int kolicinaPoena;

	// Token: 0x040022D6 RID: 8918
	private bool runTurnedOff;
}
