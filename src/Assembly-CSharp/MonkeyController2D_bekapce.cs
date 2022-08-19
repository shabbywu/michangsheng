using System;
using System.Collections;
using UnityEngine;

// Token: 0x020004C7 RID: 1223
public class MonkeyController2D_bekapce : MonoBehaviour
{
	// Token: 0x06002751 RID: 10065 RVA: 0x00122808 File Offset: 0x00120A08
	private void Awake()
	{
		this.majmun = GameObject.Find("PrinceGorilla").transform;
		this.animator = this.majmun.GetComponent<Animator>();
		this.cameraTarget = base.transform.Find("PlayerFocus2D").gameObject;
		this.cameraTarget_down = base.transform.Find("PlayerFocus2D_down").gameObject;
		this.cameraFollow = Camera.main.transform.parent.GetComponent<CameraFollow2D_new>();
		this.lookAtPos = base.transform.Find("LookAtPos");
		Input.multiTouchEnabled = true;
		this.parentAnim = this.majmun.parent.GetComponent<Animator>();
	}

	// Token: 0x06002752 RID: 10066 RVA: 0x001228C0 File Offset: 0x00120AC0
	private void Start()
	{
		this.startSpeedX = this.maxSpeedX;
		this.startJumpSpeedX = this.jumpSpeedX;
		this.state = MonkeyController2D_bekapce.State.idle;
		Resources.UnloadUnusedAssets();
		this.cameraTarget_down.transform.position = this.cameraTarget.transform.position;
		this.currentBaseState = this.animator.GetCurrentAnimatorStateInfo(0);
		this.animator.speed = 1.5f;
		this.animator.SetLookAtWeight(this.lookWeight);
		this.animator.SetLookAtPosition(this.lookAtPos.position);
		this.tempForce = this.jumpForce;
		this.senka = GameObject.Find("shadowMonkey").transform;
		MonkeyController2D_bekapce.canRespawnThings = true;
		this.groundCheck = base.transform.Find("GroundCheck");
		this.ceilingCheck = base.transform.Find("CeilingCheck");
		this.trail = base.transform.Find("Trail").GetComponent<TrailRenderer>();
	}

	// Token: 0x06002753 RID: 10067 RVA: 0x001229CC File Offset: 0x00120BCC
	private void Update()
	{
		this.hit = Physics2D.Linecast(base.transform.position + new Vector3(0.8f, 0f, 0f), base.transform.position + new Vector3(0.8f, -15.5f, 0f), 1 << LayerMask.NameToLayer("Ground") | 1 << LayerMask.NameToLayer("Platform"));
		if (this.hit)
		{
			this.senka.position = new Vector3(this.senka.position.x, this.hit.point.y - 0.3f, this.senka.position.z);
			this.senka.localScale = Vector3.one;
			this.senka.rotation = Quaternion.Euler(0f, 0f, Mathf.Asin(-this.hit.normal.x) * 180f / 3.1415927f);
		}
		else
		{
			this.senka.localScale = Vector3.zero;
		}
		if (this.startSpustanje)
		{
			if (this.startPenjanje)
			{
				this.pocetniY_spustanje = Mathf.Lerp(this.pocetniY_spustanje, this.cameraTarget.transform.position.y, 0.2f);
				this.cameraTarget_down.transform.position = new Vector3(this.cameraTarget.transform.position.x, this.pocetniY_spustanje, this.cameraTarget_down.transform.position.z);
				if (this.cameraTarget.transform.position.y <= this.cameraTarget_down.transform.position.y)
				{
					this.cameraFollow.cameraTarget = this.cameraTarget;
					this.cameraFollow.transition = false;
				}
			}
			else
			{
				this.pocetniY_spustanje = Mathf.Lerp(this.pocetniY_spustanje, this.cameraTarget_down_y, 0.15f);
				this.cameraTarget_down.transform.position = new Vector3(this.cameraTarget.transform.position.x, this.pocetniY_spustanje, this.cameraTarget_down.transform.position.z);
				if (this.cameraTarget.transform.position.y <= this.cameraTarget_down.transform.position.y)
				{
					this.cameraFollow.cameraTarget = this.cameraTarget;
					this.cameraFollow.transition = false;
				}
			}
		}
		else if (this.currentBaseState.nameHash == this.fall_State)
		{
			if (this.animator.GetBool("Falling"))
			{
				this.animator.SetBool("Falling", false);
			}
		}
		else if (this.currentBaseState.nameHash == this.glide_loop_State && Time.frameCount % 60 == 0 && Random.Range(1, 100) <= 10)
		{
			base.StartCoroutine(this.turnHead(0.1f));
		}
		if (this.proveraGround == 0)
		{
			this.grounded = Physics2D.OverlapCircle(this.groundCheck.position, this.groundedRadius, this.whatIsGround);
		}
		else
		{
			this.proveraGround--;
		}
		this.CheckWallHitNear = Physics2D.Linecast(base.transform.position + new Vector3(0.8f, 2.5f, 0f), base.transform.position + new Vector3(3.5f, 2.5f, 0f), 1 << LayerMask.NameToLayer("WallHit"));
		this.CheckWallHitNear_low = Physics2D.Linecast(base.transform.position + new Vector3(0.8f, 0f, 0f), base.transform.position + new Vector3(3.5f, 0f, 0f), 1 << LayerMask.NameToLayer("WallHit"));
		this.triggerCheckDown = Physics2D.Linecast(base.transform.position + new Vector3(0.8f, 2.5f, 0f), base.transform.position + new Vector3(0.8f, -0.5f, 0f), 1 << LayerMask.NameToLayer("Ground") | 1 << LayerMask.NameToLayer("Platform"));
		this.triggerCheckDownTrigger = Physics2D.Linecast(base.transform.position + new Vector3(0.8f, 2.5f, 0f), base.transform.position + new Vector3(0.8f, -4.5f, 0f), 1 << LayerMask.NameToLayer("Platform"));
		this.triggerCheckDownBehind = Physics2D.Linecast(base.transform.position + new Vector3(-0.8f, 2.5f, 0f), base.transform.position + new Vector3(-0.8f, -4.5f, 0f), 1 << LayerMask.NameToLayer("Platform"));
		this.proveriTerenIspredY = Physics2D.Linecast(base.transform.position + new Vector3(4.4f, 1.2f, 0f), base.transform.position + new Vector3(4.4f, -3.2f, 0f), 1 << LayerMask.NameToLayer("Ground") | 1 << LayerMask.NameToLayer("Platform"));
		this.downHit = Physics2D.Linecast(base.transform.position + new Vector3(0.2f, 0.1f, 0f), base.transform.position + new Vector3(0.2f, -0.65f, 0f), 1 << LayerMask.NameToLayer("Ground") | 1 << LayerMask.NameToLayer("Platform"));
		this.spustanjeRastojanje = Physics2D.Linecast(base.transform.position + new Vector3(2.3f, 1.25f, 0f), base.transform.position + new Vector3(2.3f, -Camera.main.orthographicSize, 0f), 1 << LayerMask.NameToLayer("Ground") | 1 << LayerMask.NameToLayer("Platform"));
		if (this.state == MonkeyController2D_bekapce.State.jumped || this.state == MonkeyController2D_bekapce.State.climbUp)
		{
			if (this.DupliSkok)
			{
				if ((Input.GetMouseButtonDown(0) && Input.mousePosition.x > (float)this.povrsinaZaClick) || Input.GetKeyDown(32))
				{
					if (this.mozeDaSkociOpet && this.hasJumped)
					{
						this.parentAnim.Play("DoubleJumpRotate");
						this.animator.SetBool("DoubleJump", true);
						if (PlaySounds.soundOn)
						{
							PlaySounds.Stop_Run();
							PlaySounds.Play_Jump();
						}
						this.doubleJump = true;
						this.animator.SetBool("Glide", false);
						this.swoosh = false;
						base.GetComponent<Rigidbody2D>().drag = 0f;
					}
				}
				else if (Input.touchCount > 1 && Input.GetTouch(1).phase == null && this.mozeDaSkociOpet && this.hasJumped)
				{
					this.disableGlide = true;
					this.animator.SetBool("DoubleJump", true);
					this.parentAnim.Play("DoubleJumpRotate");
					this.doubleJump = true;
					this.animator.SetBool("Glide", false);
					this.swoosh = false;
					base.GetComponent<Rigidbody2D>().drag = 0f;
				}
			}
			if (this.SlideNaDole || this.Glide)
			{
				if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(32))
				{
					this.duzinaPritiskaZaSkok = Time.time;
					this.startY = Input.mousePosition.y;
					this.canGlide = true;
				}
				else if (Input.GetMouseButtonUp(0) || Input.GetKeyUp(32))
				{
					this.startY = (this.endY = 0f);
					base.GetComponent<Rigidbody2D>().drag = 0f;
					this.animator.SetBool("Glide", false);
					this.disableGlide = false;
					this.canGlide = false;
					if (this.trail.time > 0f)
					{
						base.StartCoroutine(this.nestaniTrail(2f));
					}
				}
				else if (Input.touchCount == 1 && Input.GetTouch(0).phase == 3)
				{
					this.startY = (this.endY = 0f);
					base.GetComponent<Rigidbody2D>().drag = 0f;
					this.animator.SetBool("Glide", false);
					this.canGlide = false;
					if (this.trail.time > 0f)
					{
						base.StartCoroutine(this.nestaniTrail(2f));
					}
				}
			}
			if (Input.GetMouseButton(0))
			{
				this.endY = Input.mousePosition.y;
				if (this.SlideNaDole && this.startY - this.endY > 0.25f * Camera.main.orthographicSize * ((float)Screen.height / (2f * Camera.main.orthographicSize)))
				{
					if (!Physics2D.Linecast(this.groundCheck.position, this.groundCheck.position - Vector3.up * 20f, this.whatIsGround))
					{
						this.powerfullImpact = true;
						this.zutiGlowSwooshVisoki.gameObject.SetActive(true);
					}
					this.swoosh = true;
					this.animator.Play(this.swoosh_State);
				}
				if (this.Glide && this.canGlide && !this.disableGlide)
				{
					if (!this.animator.GetBool("Glide"))
					{
						this.animator.Play(this.glide_start_State);
						this.animator.SetBool("Glide", true);
					}
					base.GetComponent<Rigidbody2D>().velocity = new Vector2(this.maxSpeedX, base.GetComponent<Rigidbody2D>().velocity.y);
					base.GetComponent<Rigidbody2D>().drag = 7.5f;
					this.trail.time = this.trailTime;
				}
			}
			if (this.KontrolisaniSkok)
			{
				if (Input.GetMouseButton(0))
				{
					bool flag = this.jumpControlled;
				}
				if ((Input.GetMouseButtonUp(0) || Time.time - this.duzinaPritiskaZaSkok > 1f) && this.jumpControlled)
				{
					this.jumpControlled = false;
					this.jumpHolding = false;
					this.tempForce = this.jumpForce;
					this.canGlide = false;
					if (this.trail.time > 0f)
					{
						base.StartCoroutine(this.nestaniTrail(2f));
					}
				}
			}
			if (this.trava.isPlaying)
			{
				this.trava.Stop();
			}
			if (this.runParticle.isPlaying)
			{
				this.runParticle.Stop();
			}
		}
		if (this.state == MonkeyController2D_bekapce.State.wallhit)
		{
			if (this.KontrolisaniSkok)
			{
				if (((Input.GetMouseButtonDown(0) && Input.mousePosition.x > (float)this.povrsinaZaClick) || Input.GetKeyDown(32)) && this.RaycastFunction(Input.mousePosition) != "Pause" && this.RaycastFunction(Input.mousePosition) != "HoleButtonsPlay_Pause" && this.RaycastFunction(Input.mousePosition) != "Buy Button" && this.RaycastFunction(Input.mousePosition) != "ButtonMain_NoMoreLives" && this.RaycastFunction(Input.mousePosition) != "HoleButtonsRestart_Pause" && !this.RaycastFunction(Input.mousePosition).Contains("Tutorial"))
				{
					this.duzinaPritiskaZaSkok = Time.time;
					if (!this.inAir)
					{
						this.state = MonkeyController2D_bekapce.State.climbUp;
						if (PlaySounds.soundOn)
						{
							PlaySounds.Stop_Run();
							PlaySounds.Play_Jump();
						}
						this.jumpControlled = true;
						this.animator.Play(this.jump_State);
						this.animator.SetBool("Landing", false);
						this.jumpSafetyCheck = true;
						this.animator.SetBool("WallStop", false);
						this.inAir = true;
						this.tempForce = this.jumpForce;
						this.particleSkok.Emit(20);
					}
				}
				if ((!Input.GetMouseButton(0) || Input.mousePosition.x <= (float)this.povrsinaZaClick) && !Input.GetKey(32) && Input.GetMouseButtonUp(0) && this.usporavanje && this.Zaustavljanje)
				{
					this.usporavanje = false;
					this.state = MonkeyController2D_bekapce.State.running;
					this.maxSpeedX = this.startSpeedX;
					this.animator.Play(this.run_State);
					this.animator.SetBool("WallStop", false);
				}
			}
			else if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(32))
			{
				if (this.RaycastFunction(Input.mousePosition) != "Pause" && this.RaycastFunction(Input.mousePosition) != "HoleButtonsPlay_Pause" && this.RaycastFunction(Input.mousePosition) != "Buy Button" && this.RaycastFunction(Input.mousePosition) != "ButtonMain_NoMoreLives" && this.RaycastFunction(Input.mousePosition) != "HoleButtonsRestart_Pause" && !this.RaycastFunction(Input.mousePosition).Contains("Tutorial") && !this.inAir)
				{
					this.state = MonkeyController2D_bekapce.State.climbUp;
					this.jumpSpeedX = 5f;
					if (PlaySounds.soundOn)
					{
						PlaySounds.Stop_Run();
						PlaySounds.Play_Jump();
					}
					this.jump = true;
					this.jumpSafetyCheck = true;
					this.animator.Play(this.jump_State);
					this.animator.SetBool("Landing", false);
					this.animator.SetBool("WallStop", false);
					this.inAir = true;
					this.particleSkok.Emit(20);
				}
			}
			else if (Input.GetMouseButtonUp(0) && this.usporavanje && this.Zaustavljanje)
			{
				this.usporavanje = false;
				this.state = MonkeyController2D_bekapce.State.running;
				this.maxSpeedX = this.startSpeedX;
				this.animator.Play(this.run_State);
				this.animator.SetBool("WallStop", false);
			}
		}
		if (this.state == MonkeyController2D_bekapce.State.running)
		{
			if (Time.frameCount % 300 == 0 && Random.Range(1, 100) <= 25)
			{
				base.StartCoroutine(this.turnHead(0.1f));
			}
			if (this.KontrolisaniSkok)
			{
				if (((Input.GetMouseButtonDown(0) && Input.mousePosition.x > (float)this.povrsinaZaClick) || Input.GetKeyDown(32)) && this.RaycastFunction(Input.mousePosition) != "Pause" && this.RaycastFunction(Input.mousePosition) != "HoleButtonsPlay_Pause" && this.RaycastFunction(Input.mousePosition) != "Buy Button" && this.RaycastFunction(Input.mousePosition) != "ButtonMain_NoMoreLives" && this.RaycastFunction(Input.mousePosition) != "HoleButtonsRestart_Pause" && !this.RaycastFunction(Input.mousePosition).Contains("Tutorial"))
				{
					this.jumpHolding = true;
					this.grounded = false;
					this.proveraGround = 16;
					this.startVelY = base.GetComponent<Rigidbody2D>().velocity.y;
					this.korakce = 3f;
					base.GetComponent<Rigidbody2D>().velocity = new Vector2(this.maxSpeedX, this.maxSpeedY);
					this.neTrebaDaProdje = false;
					this.duzinaPritiskaZaSkok = Time.time;
					this.state = MonkeyController2D_bekapce.State.jumped;
					if (PlaySounds.soundOn)
					{
						PlaySounds.Stop_Run();
						PlaySounds.Play_Jump();
					}
					this.jumpControlled = true;
					this.animator.Play(this.jump_State);
					this.animator.SetBool("Landing", false);
					this.jumpSafetyCheck = true;
					this.inAir = true;
					this.particleSkok.Emit(20);
				}
				if (!Input.GetMouseButton(0) || Input.mousePosition.x <= (float)this.povrsinaZaClick)
				{
					Input.GetKey(32);
				}
				if ((Input.GetMouseButtonUp(0) || Time.time - this.duzinaPritiskaZaSkok > 2f) && this.jumpControlled)
				{
					this.jumpControlled = false;
					this.tempForce = this.jumpForce;
				}
			}
			else if (((Input.GetMouseButtonDown(0) && Input.mousePosition.x > (float)this.povrsinaZaClick) || Input.GetKeyDown(32)) && this.RaycastFunction(Input.mousePosition) != "Pause" && this.RaycastFunction(Input.mousePosition) != "HoleButtonsPlay_Pause" && this.RaycastFunction(Input.mousePosition) != "Buy Button" && this.RaycastFunction(Input.mousePosition) != "ButtonMain_NoMoreLives" && this.RaycastFunction(Input.mousePosition) != "HoleButtonsRestart_Pause" && !this.RaycastFunction(Input.mousePosition).Contains("Tutorial"))
			{
				this.neTrebaDaProdje = false;
				this.state = MonkeyController2D_bekapce.State.jumped;
				if (PlaySounds.soundOn)
				{
					PlaySounds.Stop_Run();
					PlaySounds.Play_Jump();
				}
				this.jump = true;
				this.animator.Play(this.jump_State);
				this.animator.SetBool("Landing", false);
				this.jumpSafetyCheck = true;
				this.inAir = true;
				this.particleSkok.Emit(20);
			}
			if (this.Zaustavljanje && this.povrsinaZaClick != 0 && Input.GetMouseButton(0))
			{
				this.usporavanje = true;
				this.maxSpeedX = 0f;
				this.state = MonkeyController2D_bekapce.State.wallhit;
				this.animator.SetBool("WallStop", true);
			}
			if (!this.trava.isPlaying)
			{
				this.trava.Play();
			}
			if (!this.runParticle.isPlaying)
			{
				this.runParticle.Play();
			}
		}
		if (this.state == MonkeyController2D_bekapce.State.lijana)
		{
			this.povrsinaZaClick = 0;
			if (Input.GetMouseButtonDown(0) && Input.mousePosition.x > (float)this.povrsinaZaClick && this.heCanJump && this.RaycastFunction(Input.mousePosition) != "ButtonPause" && this.RaycastFunction(Input.mousePosition) != "PauseHolePlay" && this.RaycastFunction(Input.mousePosition) != "PauseHoleShop" && this.RaycastFunction(Input.mousePosition) != "PauseHoleFreeCoins" && this.RaycastFunction(Input.mousePosition) != "PowersCardShield" && this.RaycastFunction(Input.mousePosition) != "PowersCardMagnet" && this.RaycastFunction(Input.mousePosition) != "PowersCardCoinx2" && !this.RaycastFunction(Input.mousePosition).Contains("Tutorial"))
			{
				if (Input.mousePosition.x < (float)(Screen.width / 2))
				{
					this.startY = Input.mousePosition.y;
				}
				else
				{
					this.OtkaciMajmuna();
				}
			}
			if (this.SlideNaDole)
			{
				if (Input.GetMouseButton(0))
				{
					this.endY = Input.mousePosition.y;
					if (this.SlideNaDole && this.startY - this.endY > 0.25f * Camera.main.orthographicSize * ((float)Screen.height / (2f * Camera.main.orthographicSize)))
					{
						this.SpustiMajmunaSaLijaneBrzo();
						this.swoosh = true;
						this.animator.Play(this.swoosh_State);
					}
				}
				if (Input.GetMouseButtonUp(0))
				{
					this.startY = (this.endY = 0f);
				}
			}
		}
		if (this.state == MonkeyController2D_bekapce.State.saZidaNaZid)
		{
			if (Input.GetMouseButtonDown(0) && this.heCanJump && this.RaycastFunction(Input.mousePosition) != "ButtonPause" && this.RaycastFunction(Input.mousePosition) != "PauseHolePlay" && this.RaycastFunction(Input.mousePosition) != "PauseHoleShop" && this.RaycastFunction(Input.mousePosition) != "PauseHoleFreeCoins" && this.RaycastFunction(Input.mousePosition) != "PowersCardShield" && this.RaycastFunction(Input.mousePosition) != "PowersCardMagnet" && this.RaycastFunction(Input.mousePosition) != "PowersCardCoinx2" && !this.RaycastFunction(Input.mousePosition).Contains("Tutorial") && !this.inAir)
			{
				this.neTrebaDaProdje = false;
				if (PlaySounds.soundOn)
				{
					PlaySounds.Stop_Run();
					PlaySounds.Play_Jump();
				}
				this.jump = true;
				this.animator.Play(this.jump_State);
				this.animator.SetBool("Landing", false);
				this.jumpSafetyCheck = true;
				this.inAir = true;
				if (this.klizanje.isPlaying)
				{
					this.klizanje.Stop();
				}
				this.particleSkok.Emit(20);
				this.jumpSpeedX = -this.jumpSpeedX;
				base.GetComponent<Rigidbody2D>().drag = 0f;
				this.moveForce = -this.moveForce;
				this.maxSpeedX = -this.maxSpeedX;
				this.majmun.localScale = new Vector3(this.majmun.localScale.x, this.majmun.localScale.y, -this.majmun.localScale.z);
				return;
			}
		}
		else if (this.state == MonkeyController2D_bekapce.State.preNegoDaSeOdbije && this.saZidaNaZid && (Input.GetMouseButtonDown(0) || Input.GetKeyDown(32)))
		{
			this.jump = true;
			base.GetComponent<Rigidbody2D>().drag = 0f;
			this.state = MonkeyController2D_bekapce.State.saZidaNaZid;
			this.animator.Play(this.jump_State);
			this.animator.SetBool("Landing", false);
			this.animator.SetBool("WallStop", false);
			this.particleSkok.Emit(20);
			this.jumpSafetyCheck = true;
		}
	}

	// Token: 0x06002754 RID: 10068 RVA: 0x0012412C File Offset: 0x0012232C
	private void FixedUpdate()
	{
		this.currentBaseState = this.animator.GetCurrentAnimatorStateInfo(0);
		if (this.state == MonkeyController2D_bekapce.State.saZidaNaZid)
		{
			if (this.jump)
			{
				this.hasJumped = true;
				this.mozeDaSkociOpet = true;
				if (base.GetComponent<Rigidbody2D>().velocity.y < this.maxSpeedY)
				{
					base.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
					base.GetComponent<Rigidbody2D>().AddForce(new Vector2(this.maxSpeedX, 2500f));
				}
				if (Mathf.Abs(base.GetComponent<Rigidbody2D>().velocity.y) > this.maxSpeedY)
				{
					base.GetComponent<Rigidbody2D>().velocity = new Vector2(this.maxSpeedX, this.maxSpeedY);
				}
				this.jump = false;
			}
			base.GetComponent<Rigidbody2D>().velocity = new Vector2(this.jumpSpeedX, base.GetComponent<Rigidbody2D>().velocity.y);
		}
		else if (this.state == MonkeyController2D_bekapce.State.preNegoDaSeOdbije)
		{
			if (this.jump)
			{
				this.hasJumped = true;
				this.mozeDaSkociOpet = true;
				if (base.GetComponent<Rigidbody2D>().velocity.y < this.maxSpeedY)
				{
					base.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
					base.GetComponent<Rigidbody2D>().AddForce(new Vector2(this.maxSpeedX, 2500f));
				}
				if (Mathf.Abs(base.GetComponent<Rigidbody2D>().velocity.y) > this.maxSpeedY)
				{
					base.GetComponent<Rigidbody2D>().velocity = new Vector2(this.maxSpeedX, this.maxSpeedY);
				}
				this.jump = false;
			}
			base.GetComponent<Rigidbody2D>().velocity = new Vector2(this.jumpSpeedX, base.GetComponent<Rigidbody2D>().velocity.y);
			if (base.GetComponent<Rigidbody2D>().velocity.y < -0.01f)
			{
				base.GetComponent<Rigidbody2D>().drag = 5f;
			}
		}
		else if (this.state == MonkeyController2D_bekapce.State.wasted)
		{
			if (base.GetComponent<Rigidbody2D>().velocity.x < this.maxSpeedX && !this.stop)
			{
				base.GetComponent<Rigidbody2D>().AddForce(Vector2.right * this.moveForce);
			}
			if (Mathf.Abs(base.GetComponent<Rigidbody2D>().velocity.x) > this.maxSpeedX && !this.stop)
			{
				base.GetComponent<Rigidbody2D>().velocity = new Vector2(this.maxSpeedX, base.GetComponent<Rigidbody2D>().velocity.y);
			}
		}
		if (this.state == MonkeyController2D_bekapce.State.running)
		{
			if (!PlaySounds.Run.isPlaying && PlaySounds.soundOn)
			{
				PlaySounds.Play_Run();
			}
			if (base.GetComponent<Rigidbody2D>().velocity.x < this.maxSpeedX && !this.stop)
			{
				base.GetComponent<Rigidbody2D>().AddForce(Vector2.right * this.moveForce);
			}
			if (Mathf.Abs(base.GetComponent<Rigidbody2D>().velocity.x) > this.maxSpeedX && !this.stop)
			{
				base.GetComponent<Rigidbody2D>().velocity = new Vector2(this.maxSpeedX, base.GetComponent<Rigidbody2D>().velocity.y);
			}
		}
		else if (this.state == MonkeyController2D_bekapce.State.completed)
		{
			if (base.GetComponent<Rigidbody2D>().velocity.x < this.maxSpeedX && !this.stop)
			{
				base.GetComponent<Rigidbody2D>().AddForce(Vector2.right * this.moveForce);
			}
			if (Mathf.Abs(base.GetComponent<Rigidbody2D>().velocity.x) > this.maxSpeedX && !this.stop)
			{
				base.GetComponent<Rigidbody2D>().velocity = new Vector2(this.maxSpeedX, base.GetComponent<Rigidbody2D>().velocity.y);
			}
			if (this.triggerCheckDown)
			{
				this.animator.Play(this.run_State);
			}
		}
		else if (this.state == MonkeyController2D_bekapce.State.jumped)
		{
			if (this.jumpHolding)
			{
				base.GetComponent<Rigidbody2D>().velocity = new Vector2(this.maxSpeedX, base.GetComponent<Rigidbody2D>().velocity.y + this.korakce);
				if (this.korakce > 0f)
				{
					this.korakce -= 0.085f;
				}
			}
			if (this.swoosh)
			{
				base.GetComponent<Rigidbody2D>().velocity = new Vector2(this.maxSpeedX, 0f);
				base.GetComponent<Rigidbody2D>().AddForce(new Vector2(this.maxSpeedX, -4500f));
				this.swoosh = false;
			}
			if (this.doubleJump)
			{
				this.dupliSkokOblaci.Emit(25);
				this.jumpSpeedX = this.startJumpSpeedX;
				base.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
				base.GetComponent<Rigidbody2D>().AddForce(new Vector2(2000f, this.doubleJumpForce));
				this.doubleJump = false;
				this.hasJumped = false;
			}
			else if (this.jumpControlled)
			{
				this.hasJumped = true;
				this.mozeDaSkociOpet = true;
			}
			if (base.GetComponent<Rigidbody2D>().velocity.y < -0.01f)
			{
				bool flag = this.triggerCheckDown;
			}
			if (!this.triggerCheckDown && this.jumpSafetyCheck)
			{
				this.jumpSafetyCheck = false;
			}
		}
		else if (this.state == MonkeyController2D_bekapce.State.wallhit)
		{
			if (this.trava.isPlaying)
			{
				this.trava.Stop();
			}
			if (this.runParticle.isPlaying)
			{
				this.runParticle.Stop();
			}
		}
		else if (this.state == MonkeyController2D_bekapce.State.climbUp)
		{
			if (this.doubleJump)
			{
				this.dupliSkokOblaci.Emit(25);
				this.jumpSpeedX = this.startJumpSpeedX;
				base.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
				base.GetComponent<Rigidbody2D>().AddForce(new Vector2(2000f, this.doubleJumpForce));
				this.doubleJump = false;
				this.hasJumped = false;
			}
			else if (this.jump)
			{
				base.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
				base.GetComponent<Rigidbody2D>().AddForce(new Vector2(this.maxSpeedX, this.jumpForce));
				this.hasJumped = true;
				this.jump = false;
			}
			else if (this.jumpControlled)
			{
				this.hasJumped = true;
				this.mozeDaSkociOpet = true;
				if (base.GetComponent<Rigidbody2D>().velocity.y < this.maxSpeedY)
				{
					base.GetComponent<Rigidbody2D>().AddForce(new Vector2(this.maxSpeedX, this.tempForce));
				}
				if (Mathf.Abs(base.GetComponent<Rigidbody2D>().velocity.y) > this.maxSpeedY)
				{
					base.GetComponent<Rigidbody2D>().velocity = new Vector2(base.GetComponent<Rigidbody2D>().velocity.x, this.maxSpeedY);
				}
			}
			base.GetComponent<Rigidbody2D>().velocity = new Vector2(this.jumpSpeedX, base.GetComponent<Rigidbody2D>().velocity.y);
			if (base.GetComponent<Rigidbody2D>().velocity.y < -0.01f)
			{
				bool flag2 = this.triggerCheckDown;
			}
		}
		if ((!this.triggerCheckDown || Physics2D.Linecast(base.transform.position + new Vector3(1f, 0f, 0f), base.transform.position + new Vector3(1f, 2f, 0f), 1 << LayerMask.NameToLayer("Ground"))) && this.jumpSafetyCheck)
		{
			this.jumpSafetyCheck = false;
		}
	}

	// Token: 0x06002755 RID: 10069 RVA: 0x001248B8 File Offset: 0x00122AB8
	private void OnTriggerExit2D(Collider2D col)
	{
		if (col.tag == "Footer")
		{
			float y;
			float num;
			if (col.transform.childCount > 0)
			{
				y = col.transform.Find("TriggerPositionUp").position.y;
				num = col.transform.Find("TriggerPositionDown").position.y;
			}
			else
			{
				num = (y = col.transform.position.y);
			}
			if (base.GetComponent<Collider2D>().isTrigger && (this.groundCheck.position.y + 0.2f > y || this.ceilingCheck.position.y < num))
			{
				base.GetComponent<Collider2D>().isTrigger = false;
				this.neTrebaDaProdje = false;
			}
		}
	}

	// Token: 0x06002756 RID: 10070 RVA: 0x00124980 File Offset: 0x00122B80
	private void OnCollisionEnter2D(Collision2D col)
	{
		if (this.state != MonkeyController2D_bekapce.State.completed || this.state != MonkeyController2D_bekapce.State.wasted)
		{
			if (col.gameObject.tag == "ZidZaOdbijanje")
			{
				if (this.state == MonkeyController2D_bekapce.State.running && this.CheckWallHitNear)
				{
					if (this.klizanje.isPlaying)
					{
						this.klizanje.Stop();
					}
					this.animator.SetBool("WallStop", true);
					this.animator.Play(this.wall_stop_State);
					this.state = MonkeyController2D_bekapce.State.preNegoDaSeOdbije;
					if (this.trava.isPlaying)
					{
						this.trava.Stop();
					}
					if (this.runParticle.isPlaying)
					{
						this.runParticle.Stop();
						return;
					}
				}
				else if (this.state != MonkeyController2D_bekapce.State.preNegoDaSeOdbije)
				{
					this.inAir = false;
					this.heCanJump = true;
					base.GetComponent<Rigidbody2D>().drag = 5f;
					this.klizanje.Play();
					this.animator.Play("Klizanje");
					this.state = MonkeyController2D_bekapce.State.saZidaNaZid;
					return;
				}
			}
			else if (col.gameObject.tag == "Footer")
			{
				this.startPenjanje = false;
				this.startSpustanje = false;
				if (this.cameraTarget_down.transform.parent == null)
				{
					this.cameraTarget_down.transform.position = this.cameraTarget.transform.position;
					this.cameraTarget_down.transform.parent = base.transform;
				}
				this.cameraTarget_down.transform.position = this.cameraTarget.transform.position;
				if (this.state == MonkeyController2D_bekapce.State.saZidaNaZid)
				{
					this.moveForce = Mathf.Abs(this.moveForce);
					this.jumpSpeedX = Mathf.Abs(this.jumpSpeedX);
					this.maxSpeedX = Mathf.Abs(this.maxSpeedX);
					this.majmun.localScale = new Vector3(this.majmun.localScale.x, this.majmun.localScale.y, Mathf.Abs(this.majmun.localScale.z));
					if (this.klizanje.isPlaying)
					{
						this.klizanje.Stop();
					}
					if (this.CheckWallHitNear || this.CheckWallHitNear_low)
					{
						this.animator.SetBool("WallStop", true);
						this.animator.Play(this.wall_stop_from_jump_State);
						this.state = MonkeyController2D_bekapce.State.preNegoDaSeOdbije;
						if (this.trava.isPlaying)
						{
							this.trava.Stop();
						}
					}
					else if (!this.CheckWallHitNear && !this.CheckWallHitNear_low)
					{
						this.mozeDaSkociOpet = false;
						this.animator.SetBool("Jump", false);
						this.animator.SetBool("DoubleJump", false);
						this.animator.SetBool("Glide", false);
						this.disableGlide = false;
						this.animator.Play(this.run_State);
						base.GetComponent<Rigidbody2D>().drag = 0f;
						this.state = MonkeyController2D_bekapce.State.running;
						this.canGlide = false;
						if (this.trail.time > 0f)
						{
							base.StartCoroutine(this.nestaniTrail(2f));
						}
						if (PlaySounds.soundOn)
						{
							PlaySounds.Play_Run();
						}
						this.hasJumped = false;
						this.startY = (this.endY = 0f);
						this.inAir = false;
					}
				}
				if (this.state == MonkeyController2D_bekapce.State.jumped)
				{
					if (PlaySounds.soundOn)
					{
						PlaySounds.Play_Landing();
					}
					this.oblak.Play();
					if (this.proveraGround < 12)
					{
						if (this.startSpustanje)
						{
							this.startSpustanje = false;
							this.cameraTarget_down.transform.parent = base.transform;
							this.cameraTarget_down.transform.position = this.cameraTarget.transform.position;
						}
						if (this.powerfullImpact)
						{
							this.powerfullImpact = false;
							this.zutiGlowSwooshVisoki.gameObject.SetActive(false);
							Camera.main.GetComponent<Animator>().Play("CameraShakeTrasGround");
							this.izrazitiPad.Play();
						}
						this.jumpSpeedX = this.startJumpSpeedX;
						this.mozeDaSkociOpet = false;
						this.animator.SetBool("Jump", false);
						this.animator.SetBool("DoubleJump", false);
						this.animator.SetBool("Glide", false);
						this.disableGlide = false;
						this.animator.SetBool("Landing", true);
						base.GetComponent<Rigidbody2D>().drag = 0f;
						this.state = MonkeyController2D_bekapce.State.running;
						this.grounded = true;
						this.canGlide = false;
						if (this.trail.time > 0f)
						{
							base.StartCoroutine(this.nestaniTrail(2f));
						}
						if (PlaySounds.soundOn)
						{
							PlaySounds.Play_Run();
						}
						this.hasJumped = false;
						this.startY = (this.endY = 0f);
						this.inAir = false;
						return;
					}
				}
			}
			else if (col.gameObject.tag == "Enemy")
			{
				if (this.activeShield)
				{
					col.transform.GetComponent<Collider2D>().enabled = false;
					this.activeShield = false;
					GameObject.Find("_GameManager").SendMessage("ApplyPowerUp", -3);
					if (this.state != MonkeyController2D_bekapce.State.running)
					{
						base.GetComponent<Rigidbody2D>().AddForce(new Vector2(this.maxSpeedX, 1100f));
						return;
					}
				}
				else if (!this.killed)
				{
					if (this.state == MonkeyController2D_bekapce.State.running)
					{
						base.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
						this.killed = true;
						this.oblak.Play();
						this.majmunUtepan();
						return;
					}
					this.majmunUtepanULetu();
					if (PlaySounds.BackgroundMusic_Gameplay.isPlaying)
					{
						PlaySounds.Stop_BackgroundMusic_Gameplay();
					}
					if (PlaySounds.soundOn)
					{
						PlaySounds.Play_Level_Failed_Popup();
					}
				}
			}
		}
	}

	// Token: 0x06002757 RID: 10071 RVA: 0x00124F55 File Offset: 0x00123155
	public void majmunUtepanULetu()
	{
		base.StartCoroutine(this.FallDownAfterSpikes());
	}

	// Token: 0x06002758 RID: 10072 RVA: 0x00124F64 File Offset: 0x00123164
	private IEnumerator FallDownAfterSpikes()
	{
		MonkeyController2D_bekapce.canRespawnThings = false;
		GameObject.Find("PrinceGorilla").GetComponent<Animator>().speed = 1.5f;
		if (PlaySounds.BackgroundMusic_Gameplay.isPlaying)
		{
			PlaySounds.Stop_BackgroundMusic_Gameplay();
		}
		if (PlaySounds.soundOn)
		{
			PlaySounds.Play_Level_Failed_Popup();
		}
		base.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
		this.killed = true;
		this.oblak.Play();
		this.animator.Play(this.spikedeath_State);
		this.parentAnim.Play("FallDown");
		this.state = MonkeyController2D_bekapce.State.wasted;
		GameObject.Find("Pause").GetComponent<Collider>().enabled = false;
		if (this.trava.isPlaying)
		{
			this.trava.Stop();
		}
		if (this.runParticle.isPlaying)
		{
			this.runParticle.Stop();
		}
		this.maxSpeedX = 0f;
		base.GetComponent<Rigidbody2D>().isKinematic = true;
		yield return new WaitForSeconds(0.5f);
		GameObject.Find("_GameManager").SendMessage("showFailedScreen");
		this.cameraFollow.stopFollow = true;
		yield break;
	}

	// Token: 0x06002759 RID: 10073 RVA: 0x00124F73 File Offset: 0x00123173
	private IEnumerator ProceduraPenjanja(GameObject obj)
	{
		yield return new WaitForSeconds(0.01f);
		while (this.currentBaseState.nameHash == this.grab_State)
		{
			yield return null;
			if (this.currentBaseState.normalizedTime > 0.82f && !this.helper_disableMoveAfterGrab)
			{
				this.helper_disableMoveAfterGrab = true;
				this.animator.CrossFade(this.run_State, 0.01f);
				yield return new WaitForEndOfFrame();
				base.transform.position = GameObject.Find("GrabLanding").transform.position;
			}
		}
		this.state = MonkeyController2D_bekapce.State.running;
		this.animator.applyRootMotion = false;
		base.GetComponent<Collider2D>().enabled = true;
		base.GetComponent<Rigidbody2D>().isKinematic = false;
		this.helper_disableMoveAfterGrab = false;
		base.GetComponent<Rigidbody2D>().isKinematic = false;
		this.mozeDaSkociOpet = false;
		this.animator.SetBool("Jump", false);
		this.animator.SetBool("DoubleJump", false);
		this.animator.SetBool("Glide", false);
		this.animator.SetBool("Landing", true);
		base.GetComponent<Rigidbody2D>().drag = 0f;
		this.maxSpeedX = this.startSpeedX;
		this.state = MonkeyController2D_bekapce.State.running;
		if (PlaySounds.soundOn)
		{
			PlaySounds.Play_Run();
		}
		this.animator.SetBool("WallStop", false);
		this.inAir = false;
		this.hasJumped = false;
		base.GetComponent<Collider2D>().enabled = true;
		yield break;
	}

	// Token: 0x0600275A RID: 10074 RVA: 0x00124F82 File Offset: 0x00123182
	private IEnumerator snappingProcess()
	{
		float t = 0f;
		float step = 0.25f;
		while (t < 0.02f && Mathf.Abs(base.transform.position.x - this.colliderForClimb.x) > 0.01f && Mathf.Abs(base.transform.position.y - this.colliderForClimb.y) > 0.01f)
		{
			base.transform.position = Vector3.Lerp(base.transform.position, new Vector3(this.colliderForClimb.x, this.colliderForClimb.y, base.transform.position.z), step);
			t += Time.deltaTime * step;
			yield return null;
		}
		this.grab = true;
		this.animator.Play("Grab");
		yield break;
	}

	// Token: 0x0600275B RID: 10075 RVA: 0x00124F94 File Offset: 0x00123194
	private void OnCollisionExit2D(Collision2D col)
	{
		if (col.gameObject.tag == "Footer")
		{
			if (this.state == MonkeyController2D_bekapce.State.running)
			{
				this.neTrebaDaProdje = false;
				if (!this.proveriTerenIspredY && !this.downHit)
				{
					this.state = MonkeyController2D_bekapce.State.jumped;
					this.animator.SetBool("Landing", false);
					this.animator.Play(this.fall_State);
					if (this.runParticle.isPlaying)
					{
						this.runParticle.Stop();
					}
					if (!this.spustanjeRastojanje)
					{
						this.startSpustanje = true;
						this.cameraTarget_down.transform.parent = null;
						this.pocetniY_spustanje = this.cameraTarget.transform.position.y;
						this.cameraTarget_down_y = base.transform.position.y - 7.5f;
						this.cameraFollow.cameraTarget = this.cameraTarget_down;
						return;
					}
				}
			}
		}
		else if (col.gameObject.tag == "ZidZaOdbijanje")
		{
			base.GetComponent<Rigidbody2D>().drag = 0f;
			if (this.trava.isPlaying)
			{
				this.trava.Stop();
			}
			this.animator.Play(this.fall_State);
			this.animator.SetBool("Landing", false);
		}
	}

	// Token: 0x0600275C RID: 10076 RVA: 0x001250F1 File Offset: 0x001232F1
	private void NotifyManagerForFinish()
	{
		GameObject.Find("_GameManager").SendMessage("ShowWinScreen");
	}

	// Token: 0x0600275D RID: 10077 RVA: 0x00125107 File Offset: 0x00123307
	private IEnumerator TutorialPlay(Transform obj, string ime, int next)
	{
		base.StartCoroutine(obj.GetComponent<Animation>().Play(ime, false, delegate(bool what)
		{
			this.helpBool = true;
		}));
		while (!this.helpBool)
		{
			yield return null;
		}
		this.helpBool = false;
		if (next == 1)
		{
			base.StartCoroutine(this.TutorialPlay(obj, "TutorialIdle1_A", -1));
		}
		else if (next == 2)
		{
			base.StartCoroutine(this.TutorialPlay(obj, "TutorialIdle2_A", -1));
		}
		yield break;
	}

	// Token: 0x0600275E RID: 10078 RVA: 0x0012512C File Offset: 0x0012332C
	private void OnTriggerEnter2D(Collider2D col)
	{
		if (this.state != MonkeyController2D_bekapce.State.completed || this.state != MonkeyController2D_bekapce.State.wasted)
		{
			if (col.tag == "Barrel")
			{
				col.transform.GetChild(0).GetComponent<Animator>().Play("BarrelBoom");
			}
			else if (col.name == "Magnet_collect")
			{
				GameObject.Find("_GameManager").SendMessage("ApplyPowerUp", 1);
				col.gameObject.SetActive(false);
				base.StartCoroutine(this.reappearItem(col.gameObject));
			}
			else if (col.name == "BananaCoinX2_collect")
			{
				GameObject.Find("_GameManager").SendMessage("ApplyPowerUp", 2);
				col.gameObject.SetActive(false);
				base.StartCoroutine(this.reappearItem(col.gameObject));
			}
			else if (col.name == "Shield_collect")
			{
				GameObject.Find("_GameManager").SendMessage("ApplyPowerUp", 3);
				col.gameObject.SetActive(false);
				this.activeShield = true;
				base.StartCoroutine(this.reappearItem(col.gameObject));
			}
			else if (col.name == "Banana_collect")
			{
				col.gameObject.SetActive(false);
				Manage.points += 200;
				base.StartCoroutine(this.reappearItem(col.gameObject));
			}
			else if (col.name == "Srce_collect")
			{
				col.gameObject.SetActive(false);
				GameObject.Find("LifeManager").SendMessage("AddLife");
				base.StartCoroutine(this.reappearItem(col.gameObject));
			}
			else if (col.name == "Dijamant_collect")
			{
				col.gameObject.SetActive(false);
				Manage.points += 50;
				base.StartCoroutine(this.reappearItem(col.gameObject));
			}
			if (col.tag == "Finish")
			{
				col.GetComponent<Collider2D>().enabled = false;
				this.cameraFollow.cameraFollowX = false;
				base.Invoke("NotifyManagerForFinish", 1.25f);
				GameObject.Find("Pause").GetComponent<Collider>().enabled = false;
				this.state = MonkeyController2D_bekapce.State.completed;
				return;
			}
			if (col.tag == "Footer")
			{
				float y;
				if (col.transform.childCount > 0)
				{
					y = col.transform.Find("TriggerPositionUp").position.y;
				}
				else
				{
					y = col.transform.position.y;
				}
				if (base.transform.position.y + 0.25f > y && this.triggerCheckDownTrigger && this.triggerCheckDownBehind && base.GetComponent<Collider2D>().isTrigger)
				{
					base.GetComponent<Collider2D>().isTrigger = false;
					return;
				}
			}
			else if (col.tag == "Enemy")
			{
				col.GetComponent<Collider2D>().enabled = false;
				if (this.activeShield)
				{
					col.transform.GetComponent<Collider2D>().enabled = false;
					this.activeShield = false;
					GameObject.Find("_GameManager").SendMessage("ApplyPowerUp", -3);
					if (this.state != MonkeyController2D_bekapce.State.running)
					{
						base.GetComponent<Rigidbody2D>().AddForce(new Vector2(this.maxSpeedX, 1100f));
						return;
					}
				}
				else if (!this.killed)
				{
					if (this.state == MonkeyController2D_bekapce.State.running)
					{
						base.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
						this.killed = true;
						this.oblak.Play();
						this.majmunUtepan();
						return;
					}
					base.StartCoroutine(this.FallDownAfterSpikes());
					return;
				}
			}
			else
			{
				if (col.tag == "0_Plan")
				{
					col.GetComponent<RunWithSpeed>().enabled = true;
					return;
				}
				if (col.tag == "SaZidaNaZid")
				{
					if (this.saZidaNaZid)
					{
						this.saZidaNaZid = false;
						this.state = MonkeyController2D_bekapce.State.jumped;
						return;
					}
					this.saZidaNaZid = true;
					return;
				}
				else
				{
					if (col.tag == "GrabLedge" && !this.downHit)
					{
						col.GetComponent<Collider2D>().enabled = false;
						return;
					}
					if (col.tag == "Lijana")
					{
						this.grabLianaTransform = col.transform.GetChild(0);
						this.lijana = true;
						this.state = MonkeyController2D_bekapce.State.lijana;
						col.enabled = false;
						base.GetComponent<Rigidbody2D>().isKinematic = true;
						this.maxSpeedX = 0f;
						this.jumpSpeedX = 0f;
						col.transform.parent.GetComponent<Animator>().Play("RotateLianaHolder");
						this.animator.Play(this.lijana_State);
						base.StartCoroutine("pratiLijanaTarget", this.grabLianaTransform);
					}
				}
			}
		}
	}

	// Token: 0x0600275F RID: 10079 RVA: 0x00125618 File Offset: 0x00123818
	private void OnTriggerStay2D(Collider2D col)
	{
		if (col.tag == "GrabLedge")
		{
			base.GetComponent<Rigidbody2D>().velocity = new Vector2(0f, Mathf.MoveTowards(base.GetComponent<Rigidbody2D>().velocity.y, 0f, 0.2f));
		}
	}

	// Token: 0x06002760 RID: 10080 RVA: 0x0012566B File Offset: 0x0012386B
	private IEnumerator pratiLijanaTarget(Transform target)
	{
		while (this.lijana)
		{
			yield return null;
			base.transform.position = Vector3.Lerp(base.transform.position, new Vector3(target.position.x, target.position.y, base.transform.position.z), 0.2f);
		}
		yield break;
	}

	// Token: 0x06002761 RID: 10081 RVA: 0x00125684 File Offset: 0x00123884
	public void OtkaciMajmuna()
	{
		this.lijana = false;
		base.StopCoroutine("pratiLijanaTarget");
		this.state = MonkeyController2D_bekapce.State.jumped;
		this.cameraFollow.cameraFollowX = true;
		this.maxSpeedX = this.startSpeedX;
		this.jumpSpeedX = this.startJumpSpeedX;
		base.transform.parent = null;
		base.transform.rotation = Quaternion.identity;
		base.GetComponent<Rigidbody2D>().isKinematic = false;
		base.GetComponent<Rigidbody2D>().AddForce(new Vector2(200f, 2500f));
		this.animator.Play(this.jump_State);
	}

	// Token: 0x06002762 RID: 10082 RVA: 0x00125724 File Offset: 0x00123924
	private void SpustiMajmunaSaLijaneBrzo()
	{
		this.lijana = false;
		base.StopCoroutine("pratiLijanaTarget");
		this.state = MonkeyController2D_bekapce.State.jumped;
		this.cameraFollow.cameraFollowX = true;
		this.maxSpeedX = this.startSpeedX;
		this.jumpSpeedX = this.startJumpSpeedX;
		base.transform.parent = null;
		base.transform.rotation = Quaternion.identity;
		base.GetComponent<Rigidbody2D>().isKinematic = false;
		this.animator.Play(this.jump_State);
	}

	// Token: 0x06002763 RID: 10083 RVA: 0x001257A7 File Offset: 0x001239A7
	private IEnumerator Bounce(float time)
	{
		yield return new WaitForSeconds(time);
		this.stop = false;
		yield break;
	}

	// Token: 0x06002764 RID: 10084 RVA: 0x001257BD File Offset: 0x001239BD
	private void climb()
	{
		base.StartCoroutine(this.MoveUp(0.05f));
	}

	// Token: 0x06002765 RID: 10085 RVA: 0x001257D1 File Offset: 0x001239D1
	private IEnumerator ClimbLedge(Transform target, float time)
	{
		yield return null;
		if (base.transform.position.y > target.position.y)
		{
			base.GetComponent<Rigidbody2D>().velocity = new Vector2(0f, 0f);
			base.GetComponent<Rigidbody2D>().isKinematic = false;
			this.stop = false;
			this.mozeDaSkociOpet = false;
			this.animator.SetBool("Jump", false);
			this.animator.SetBool("DoubleJump", false);
			this.animator.SetBool("Glide", false);
			this.disableGlide = false;
			this.animator.SetBool("Landing", true);
			this.state = MonkeyController2D_bekapce.State.running;
			if (PlaySounds.soundOn)
			{
				PlaySounds.Play_Run();
			}
			this.hasJumped = false;
		}
		yield break;
	}

	// Token: 0x06002766 RID: 10086 RVA: 0x001257E7 File Offset: 0x001239E7
	private IEnumerator MoveUp(float time)
	{
		float target = base.transform.position.y + 1.85f;
		float t = 0f;
		float num = 2f;
		float destX = base.transform.position.x + num;
		float step = 0.03f;
		while (t < 1f)
		{
			base.transform.position = Vector2.MoveTowards(base.transform.position, new Vector3(destX, target, base.transform.position.z), t);
			if (Time.timeScale != 0f)
			{
			}
			t += step;
			yield return null;
		}
		yield break;
	}

	// Token: 0x06002767 RID: 10087 RVA: 0x001257F8 File Offset: 0x001239F8
	public void majmunUtepan()
	{
		MonkeyController2D_bekapce.canRespawnThings = false;
		GameObject.Find("PrinceGorilla").GetComponent<Animator>().speed = 1.5f;
		this.state = MonkeyController2D_bekapce.State.wasted;
		GameObject.Find("Pause").GetComponent<Collider>().enabled = false;
		this.animator.Play("Death1");
		base.StartCoroutine(this.slowDown());
		if (this.trava.isPlaying)
		{
			this.trava.Stop();
		}
		if (this.runParticle.isPlaying)
		{
			this.runParticle.Stop();
		}
		this.maxSpeedX = 0f;
		if (PlaySounds.BackgroundMusic_Gameplay.isPlaying)
		{
			PlaySounds.Stop_BackgroundMusic_Gameplay();
		}
		if (PlaySounds.soundOn)
		{
			PlaySounds.Play_Level_Failed_Popup();
		}
	}

	// Token: 0x06002768 RID: 10088 RVA: 0x001258B5 File Offset: 0x00123AB5
	private IEnumerator slowDown()
	{
		float finish = base.transform.position.x - 5f;
		float t = 0f;
		while (t < 0.5f)
		{
			base.transform.position = Vector3.Lerp(base.transform.position, new Vector3(finish, base.transform.position.y, base.transform.position.z), t);
			t += Time.deltaTime / 2f;
			yield return null;
		}
		GameObject.Find("_GameManager").SendMessage("showFailedScreen");
		this.cameraFollow.stopFollow = true;
		base.GetComponent<Rigidbody2D>().isKinematic = true;
		yield return new WaitForSeconds(1.75f);
		base.transform.Find("Particles/HolderKillStars").gameObject.SetActive(true);
		yield break;
	}

	// Token: 0x06002769 RID: 10089 RVA: 0x00004095 File Offset: 0x00002295
	public void CallShake()
	{
	}

	// Token: 0x0600276A RID: 10090 RVA: 0x001258C4 File Offset: 0x00123AC4
	private IEnumerator shakeCamera()
	{
		yield return null;
		yield break;
	}

	// Token: 0x0600276B RID: 10091 RVA: 0x001258CC File Offset: 0x00123ACC
	private string RaycastFunction(Vector3 vector)
	{
		RaycastHit raycastHit;
		if (Physics.Raycast(Camera.main.ScreenPointToRay(vector), ref raycastHit))
		{
			return raycastHit.collider.name;
		}
		return "";
	}

	// Token: 0x0600276C RID: 10092 RVA: 0x001258FF File Offset: 0x00123AFF
	private IEnumerator turnHead(float step)
	{
		float t = 0f;
		while (t < 0.175f)
		{
			this.animator.SetLookAtWeight(this.lookWeight);
			this.animator.SetLookAtPosition(this.lookAtPos.position);
			this.lookWeight = Mathf.Lerp(this.lookWeight, 1f, t);
			t += step * Time.deltaTime;
			yield return null;
		}
		t = 0f;
		while (t < 0.175f)
		{
			this.animator.SetLookAtWeight(this.lookWeight);
			this.animator.SetLookAtPosition(this.lookAtPos.position);
			this.lookWeight = Mathf.Lerp(this.lookWeight, 0f, t);
			t += step * Time.deltaTime;
			yield return null;
		}
		yield break;
	}

	// Token: 0x0600276D RID: 10093 RVA: 0x00125915 File Offset: 0x00123B15
	private IEnumerator easyStop()
	{
		while (this.maxSpeedX > 0f && this.usporavanje)
		{
			if (this.maxSpeedX < 0.5f)
			{
				this.maxSpeedX = 0f;
			}
			this.maxSpeedX = Mathf.Lerp(this.maxSpeedX, 0f, 10f * Time.deltaTime);
			yield return null;
		}
		yield break;
	}

	// Token: 0x0600276E RID: 10094 RVA: 0x00125924 File Offset: 0x00123B24
	private IEnumerator easyGo()
	{
		while (this.maxSpeedX < this.startSpeedX && !this.usporavanje)
		{
			if (this.maxSpeedX > this.startSpeedX - 0.5f)
			{
				this.maxSpeedX = this.startSpeedX;
			}
			this.maxSpeedX = Mathf.Lerp(this.maxSpeedX, this.startSpeedX, 10f * Time.deltaTime);
			yield return null;
		}
		yield break;
	}

	// Token: 0x0600276F RID: 10095 RVA: 0x00125933 File Offset: 0x00123B33
	private IEnumerator reappearItem(GameObject obj)
	{
		yield return new WaitForSeconds(1.5f);
		obj.SetActive(true);
		yield break;
	}

	// Token: 0x06002770 RID: 10096 RVA: 0x00125942 File Offset: 0x00123B42
	private IEnumerator nestaniTrail(float step)
	{
		float t = 0f;
		while (t < 0.4f)
		{
			this.trail.time = Mathf.Lerp(this.trail.time, 0f, t);
			t += Time.deltaTime * 5f;
			yield return null;
		}
		this.trail.time = 0f;
		yield break;
	}

	// Token: 0x040021A4 RID: 8612
	public float moveForce = 300f;

	// Token: 0x040021A5 RID: 8613
	public float maxSpeedX = 8f;

	// Token: 0x040021A6 RID: 8614
	public float jumpForce = 700f;

	// Token: 0x040021A7 RID: 8615
	public float doubleJumpForce = 100f;

	// Token: 0x040021A8 RID: 8616
	public float gravity = 200f;

	// Token: 0x040021A9 RID: 8617
	public float maxSpeedY = 8f;

	// Token: 0x040021AA RID: 8618
	public float jumpSpeedX = 12f;

	// Token: 0x040021AB RID: 8619
	private bool jump;

	// Token: 0x040021AC RID: 8620
	private bool doubleJump;

	// Token: 0x040021AD RID: 8621
	[HideInInspector]
	public bool inAir;

	// Token: 0x040021AE RID: 8622
	private bool hasJumped;

	// Token: 0x040021AF RID: 8623
	private bool jumpSafetyCheck;

	// Token: 0x040021B0 RID: 8624
	private bool proveriTerenIspredY;

	// Token: 0x040021B1 RID: 8625
	private bool downHit;

	// Token: 0x040021B2 RID: 8626
	[HideInInspector]
	public bool zgaziEnemija;

	// Token: 0x040021B3 RID: 8627
	[HideInInspector]
	public bool killed;

	// Token: 0x040021B4 RID: 8628
	[HideInInspector]
	public bool stop;

	// Token: 0x040021B5 RID: 8629
	private bool triggerCheckDown;

	// Token: 0x040021B6 RID: 8630
	[HideInInspector]
	public bool triggerCheckDownTrigger;

	// Token: 0x040021B7 RID: 8631
	[HideInInspector]
	public bool triggerCheckDownBehind;

	// Token: 0x040021B8 RID: 8632
	private bool CheckWallHitNear;

	// Token: 0x040021B9 RID: 8633
	private bool CheckWallHitNear_low;

	// Token: 0x040021BA RID: 8634
	private bool startSpustanje;

	// Token: 0x040021BB RID: 8635
	private bool startPenjanje;

	// Token: 0x040021BC RID: 8636
	private bool spustanjeRastojanje;

	// Token: 0x040021BD RID: 8637
	private float pocetniY_spustanje;

	// Token: 0x040021BE RID: 8638
	[HideInInspector]
	public float collisionAngle;

	// Token: 0x040021BF RID: 8639
	private float duzinaPritiskaZaSkok;

	// Token: 0x040021C0 RID: 8640
	private bool mozeDaSkociOpet;

	// Token: 0x040021C1 RID: 8641
	private float startY;

	// Token: 0x040021C2 RID: 8642
	private float endX;

	// Token: 0x040021C3 RID: 8643
	private float endY;

	// Token: 0x040021C4 RID: 8644
	private bool swoosh;

	// Token: 0x040021C5 RID: 8645
	private bool grab;

	// Token: 0x040021C6 RID: 8646
	private bool snappingToClimb;

	// Token: 0x040021C7 RID: 8647
	private Vector3 colliderForClimb;

	// Token: 0x040021C8 RID: 8648
	public bool Glide;

	// Token: 0x040021C9 RID: 8649
	public bool DupliSkok;

	// Token: 0x040021CA RID: 8650
	public bool KontrolisaniSkok;

	// Token: 0x040021CB RID: 8651
	public bool SlideNaDole;

	// Token: 0x040021CC RID: 8652
	public bool Zaustavljanje;

	// Token: 0x040021CD RID: 8653
	private Ray2D ray;

	// Token: 0x040021CE RID: 8654
	private RaycastHit2D hit;

	// Token: 0x040021CF RID: 8655
	private Transform ceilingCheck;

	// Token: 0x040021D0 RID: 8656
	private Transform groundCheck;

	// Token: 0x040021D1 RID: 8657
	private Transform majmun;

	// Token: 0x040021D2 RID: 8658
	public ParticleSystem trava;

	// Token: 0x040021D3 RID: 8659
	public ParticleSystem oblak;

	// Token: 0x040021D4 RID: 8660
	public ParticleSystem particleSkok;

	// Token: 0x040021D5 RID: 8661
	public ParticleSystem dupliSkokOblaci;

	// Token: 0x040021D6 RID: 8662
	public ParticleSystem runParticle;

	// Token: 0x040021D7 RID: 8663
	public ParticleSystem klizanje;

	// Token: 0x040021D8 RID: 8664
	public ParticleSystem izrazitiPad;

	// Token: 0x040021D9 RID: 8665
	public Transform zutiGlowSwooshVisoki;

	// Token: 0x040021DA RID: 8666
	private Transform whatToClimb;

	// Token: 0x040021DB RID: 8667
	private float currentSpeed;

	// Token: 0x040021DC RID: 8668
	[HideInInspector]
	public GameObject cameraTarget;

	// Token: 0x040021DD RID: 8669
	[HideInInspector]
	public GameObject cameraTarget_down;

	// Token: 0x040021DE RID: 8670
	private float cameraTarget_down_y;

	// Token: 0x040021DF RID: 8671
	private CameraFollow2D_new cameraFollow;

	// Token: 0x040021E0 RID: 8672
	public MonkeyController2D_bekapce.State state;

	// Token: 0x040021E1 RID: 8673
	[HideInInspector]
	public float startSpeedX;

	// Token: 0x040021E2 RID: 8674
	[HideInInspector]
	public float startJumpSpeedX;

	// Token: 0x040021E3 RID: 8675
	public bool neTrebaDaProdje;

	// Token: 0x040021E4 RID: 8676
	[HideInInspector]
	public Animator animator;

	// Token: 0x040021E5 RID: 8677
	private Animator parentAnim;

	// Token: 0x040021E6 RID: 8678
	[HideInInspector]
	public string lastPlayedAnim;

	// Token: 0x040021E7 RID: 8679
	private bool helpBool;

	// Token: 0x040021E8 RID: 8680
	private AnimatorStateInfo currentBaseState;

	// Token: 0x040021E9 RID: 8681
	[HideInInspector]
	public int run_State = Animator.StringToHash("Base Layer.Running");

	// Token: 0x040021EA RID: 8682
	[HideInInspector]
	public int jump_State = Animator.StringToHash("Base Layer.Jump_Start");

	// Token: 0x040021EB RID: 8683
	[HideInInspector]
	public int fall_State = Animator.StringToHash("Base Layer.Jump_Falling");

	// Token: 0x040021EC RID: 8684
	[HideInInspector]
	public int landing_State = Animator.StringToHash("Base Layer.Landing");

	// Token: 0x040021ED RID: 8685
	[HideInInspector]
	public int doublejump_State = Animator.StringToHash("Base Layer.DoubleJump_Start");

	// Token: 0x040021EE RID: 8686
	[HideInInspector]
	public int glide_start_State = Animator.StringToHash("Base Layer.Glide_Start");

	// Token: 0x040021EF RID: 8687
	[HideInInspector]
	public int glide_loop_State = Animator.StringToHash("Base Layer.Glide_Loop");

	// Token: 0x040021F0 RID: 8688
	[HideInInspector]
	public int grab_State = Animator.StringToHash("Base Layer.Grab");

	// Token: 0x040021F1 RID: 8689
	[HideInInspector]
	public int wall_stop_State = Animator.StringToHash("Base Layer.WallStop");

	// Token: 0x040021F2 RID: 8690
	[HideInInspector]
	public int wall_stop_from_jump_State = Animator.StringToHash("Base Layer.WallStop_From_Jump");

	// Token: 0x040021F3 RID: 8691
	[HideInInspector]
	public int swoosh_State = Animator.StringToHash("Base Layer.Swoosh_Down");

	// Token: 0x040021F4 RID: 8692
	[HideInInspector]
	public int spikedeath_State = Animator.StringToHash("Base Layer.Spike_death");

	// Token: 0x040021F5 RID: 8693
	[HideInInspector]
	public int lijana_State = Animator.StringToHash("Base Layer.Lijana");

	// Token: 0x040021F6 RID: 8694
	private Transform lookAtPos;

	// Token: 0x040021F7 RID: 8695
	private float lookWeight;

	// Token: 0x040021F8 RID: 8696
	private bool disableGlide;

	// Token: 0x040021F9 RID: 8697
	private bool helper_disableMoveAfterGrab;

	// Token: 0x040021FA RID: 8698
	private bool usporavanje;

	// Token: 0x040021FB RID: 8699
	private bool sudarioSeSaZidom;

	// Token: 0x040021FC RID: 8700
	[HideInInspector]
	public bool lijana;

	// Token: 0x040021FD RID: 8701
	private Transform grabLianaTransform;

	// Token: 0x040021FE RID: 8702
	[HideInInspector]
	public bool heCanJump = true;

	// Token: 0x040021FF RID: 8703
	private bool saZidaNaZid;

	// Token: 0x04002200 RID: 8704
	private int povrsinaZaClick;

	// Token: 0x04002201 RID: 8705
	private bool jumpControlled;

	// Token: 0x04002202 RID: 8706
	private float tempForce;

	// Token: 0x04002203 RID: 8707
	[HideInInspector]
	public bool activeShield;

	// Token: 0x04002204 RID: 8708
	private float razmrk;

	// Token: 0x04002205 RID: 8709
	private Vector3 pocScale;

	// Token: 0x04002206 RID: 8710
	private Transform senka;

	// Token: 0x04002207 RID: 8711
	public static bool canRespawnThings = true;

	// Token: 0x04002208 RID: 8712
	[SerializeField]
	private LayerMask whatIsGround;

	// Token: 0x04002209 RID: 8713
	private float groundedRadius = 0.2f;

	// Token: 0x0400220A RID: 8714
	private bool grounded;

	// Token: 0x0400220B RID: 8715
	private float startVelY;

	// Token: 0x0400220C RID: 8716
	private float korakce;

	// Token: 0x0400220D RID: 8717
	[HideInInspector]
	public bool canGlide;

	// Token: 0x0400220E RID: 8718
	private int proveraGround = 16;

	// Token: 0x0400220F RID: 8719
	private bool jumpHolding;

	// Token: 0x04002210 RID: 8720
	private TrailRenderer trail;

	// Token: 0x04002211 RID: 8721
	private bool powerfullImpact;

	// Token: 0x04002212 RID: 8722
	public float trailTime = 0.5f;

	// Token: 0x02001430 RID: 5168
	[HideInInspector]
	public enum State
	{
		// Token: 0x04006AFF RID: 27391
		running,
		// Token: 0x04006B00 RID: 27392
		jumped,
		// Token: 0x04006B01 RID: 27393
		wallhit,
		// Token: 0x04006B02 RID: 27394
		climbUp,
		// Token: 0x04006B03 RID: 27395
		actualClimbing,
		// Token: 0x04006B04 RID: 27396
		wasted,
		// Token: 0x04006B05 RID: 27397
		idle,
		// Token: 0x04006B06 RID: 27398
		completed,
		// Token: 0x04006B07 RID: 27399
		lijana,
		// Token: 0x04006B08 RID: 27400
		saZidaNaZid,
		// Token: 0x04006B09 RID: 27401
		preNegoDaSeOdbije
	}
}
