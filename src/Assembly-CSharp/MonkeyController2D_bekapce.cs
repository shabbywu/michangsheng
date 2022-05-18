using System;
using System.Collections;
using UnityEngine;

// Token: 0x0200070D RID: 1805
public class MonkeyController2D_bekapce : MonoBehaviour
{
	// Token: 0x06002DB1 RID: 11697 RVA: 0x0016CA68 File Offset: 0x0016AC68
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

	// Token: 0x06002DB2 RID: 11698 RVA: 0x0016CB20 File Offset: 0x0016AD20
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

	// Token: 0x06002DB3 RID: 11699 RVA: 0x0016CC2C File Offset: 0x0016AE2C
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

	// Token: 0x06002DB4 RID: 11700 RVA: 0x0016E38C File Offset: 0x0016C58C
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

	// Token: 0x06002DB5 RID: 11701 RVA: 0x0016EB18 File Offset: 0x0016CD18
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

	// Token: 0x06002DB6 RID: 11702 RVA: 0x0016EBE0 File Offset: 0x0016CDE0
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

	// Token: 0x06002DB7 RID: 11703 RVA: 0x000224EF File Offset: 0x000206EF
	public void majmunUtepanULetu()
	{
		base.StartCoroutine(this.FallDownAfterSpikes());
	}

	// Token: 0x06002DB8 RID: 11704 RVA: 0x000224FE File Offset: 0x000206FE
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

	// Token: 0x06002DB9 RID: 11705 RVA: 0x0002250D File Offset: 0x0002070D
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

	// Token: 0x06002DBA RID: 11706 RVA: 0x0002251C File Offset: 0x0002071C
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

	// Token: 0x06002DBB RID: 11707 RVA: 0x0016F1B8 File Offset: 0x0016D3B8
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

	// Token: 0x06002DBC RID: 11708 RVA: 0x0002252B File Offset: 0x0002072B
	private void NotifyManagerForFinish()
	{
		GameObject.Find("_GameManager").SendMessage("ShowWinScreen");
	}

	// Token: 0x06002DBD RID: 11709 RVA: 0x00022541 File Offset: 0x00020741
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

	// Token: 0x06002DBE RID: 11710 RVA: 0x0016F318 File Offset: 0x0016D518
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

	// Token: 0x06002DBF RID: 11711 RVA: 0x0016F804 File Offset: 0x0016DA04
	private void OnTriggerStay2D(Collider2D col)
	{
		if (col.tag == "GrabLedge")
		{
			base.GetComponent<Rigidbody2D>().velocity = new Vector2(0f, Mathf.MoveTowards(base.GetComponent<Rigidbody2D>().velocity.y, 0f, 0.2f));
		}
	}

	// Token: 0x06002DC0 RID: 11712 RVA: 0x00022565 File Offset: 0x00020765
	private IEnumerator pratiLijanaTarget(Transform target)
	{
		while (this.lijana)
		{
			yield return null;
			base.transform.position = Vector3.Lerp(base.transform.position, new Vector3(target.position.x, target.position.y, base.transform.position.z), 0.2f);
		}
		yield break;
	}

	// Token: 0x06002DC1 RID: 11713 RVA: 0x0016F858 File Offset: 0x0016DA58
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

	// Token: 0x06002DC2 RID: 11714 RVA: 0x0016F8F8 File Offset: 0x0016DAF8
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

	// Token: 0x06002DC3 RID: 11715 RVA: 0x0002257B File Offset: 0x0002077B
	private IEnumerator Bounce(float time)
	{
		yield return new WaitForSeconds(time);
		this.stop = false;
		yield break;
	}

	// Token: 0x06002DC4 RID: 11716 RVA: 0x00022591 File Offset: 0x00020791
	private void climb()
	{
		base.StartCoroutine(this.MoveUp(0.05f));
	}

	// Token: 0x06002DC5 RID: 11717 RVA: 0x000225A5 File Offset: 0x000207A5
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

	// Token: 0x06002DC6 RID: 11718 RVA: 0x000225BB File Offset: 0x000207BB
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

	// Token: 0x06002DC7 RID: 11719 RVA: 0x0016F97C File Offset: 0x0016DB7C
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

	// Token: 0x06002DC8 RID: 11720 RVA: 0x000225CA File Offset: 0x000207CA
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

	// Token: 0x06002DC9 RID: 11721 RVA: 0x000042DD File Offset: 0x000024DD
	public void CallShake()
	{
	}

	// Token: 0x06002DCA RID: 11722 RVA: 0x000225D9 File Offset: 0x000207D9
	private IEnumerator shakeCamera()
	{
		yield return null;
		yield break;
	}

	// Token: 0x06002DCB RID: 11723 RVA: 0x00149A14 File Offset: 0x00147C14
	private string RaycastFunction(Vector3 vector)
	{
		RaycastHit raycastHit;
		if (Physics.Raycast(Camera.main.ScreenPointToRay(vector), ref raycastHit))
		{
			return raycastHit.collider.name;
		}
		return "";
	}

	// Token: 0x06002DCC RID: 11724 RVA: 0x000225E1 File Offset: 0x000207E1
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

	// Token: 0x06002DCD RID: 11725 RVA: 0x000225F7 File Offset: 0x000207F7
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

	// Token: 0x06002DCE RID: 11726 RVA: 0x00022606 File Offset: 0x00020806
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

	// Token: 0x06002DCF RID: 11727 RVA: 0x00022615 File Offset: 0x00020815
	private IEnumerator reappearItem(GameObject obj)
	{
		yield return new WaitForSeconds(1.5f);
		obj.SetActive(true);
		yield break;
	}

	// Token: 0x06002DD0 RID: 11728 RVA: 0x00022624 File Offset: 0x00020824
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

	// Token: 0x04002860 RID: 10336
	public float moveForce = 300f;

	// Token: 0x04002861 RID: 10337
	public float maxSpeedX = 8f;

	// Token: 0x04002862 RID: 10338
	public float jumpForce = 700f;

	// Token: 0x04002863 RID: 10339
	public float doubleJumpForce = 100f;

	// Token: 0x04002864 RID: 10340
	public float gravity = 200f;

	// Token: 0x04002865 RID: 10341
	public float maxSpeedY = 8f;

	// Token: 0x04002866 RID: 10342
	public float jumpSpeedX = 12f;

	// Token: 0x04002867 RID: 10343
	private bool jump;

	// Token: 0x04002868 RID: 10344
	private bool doubleJump;

	// Token: 0x04002869 RID: 10345
	[HideInInspector]
	public bool inAir;

	// Token: 0x0400286A RID: 10346
	private bool hasJumped;

	// Token: 0x0400286B RID: 10347
	private bool jumpSafetyCheck;

	// Token: 0x0400286C RID: 10348
	private bool proveriTerenIspredY;

	// Token: 0x0400286D RID: 10349
	private bool downHit;

	// Token: 0x0400286E RID: 10350
	[HideInInspector]
	public bool zgaziEnemija;

	// Token: 0x0400286F RID: 10351
	[HideInInspector]
	public bool killed;

	// Token: 0x04002870 RID: 10352
	[HideInInspector]
	public bool stop;

	// Token: 0x04002871 RID: 10353
	private bool triggerCheckDown;

	// Token: 0x04002872 RID: 10354
	[HideInInspector]
	public bool triggerCheckDownTrigger;

	// Token: 0x04002873 RID: 10355
	[HideInInspector]
	public bool triggerCheckDownBehind;

	// Token: 0x04002874 RID: 10356
	private bool CheckWallHitNear;

	// Token: 0x04002875 RID: 10357
	private bool CheckWallHitNear_low;

	// Token: 0x04002876 RID: 10358
	private bool startSpustanje;

	// Token: 0x04002877 RID: 10359
	private bool startPenjanje;

	// Token: 0x04002878 RID: 10360
	private bool spustanjeRastojanje;

	// Token: 0x04002879 RID: 10361
	private float pocetniY_spustanje;

	// Token: 0x0400287A RID: 10362
	[HideInInspector]
	public float collisionAngle;

	// Token: 0x0400287B RID: 10363
	private float duzinaPritiskaZaSkok;

	// Token: 0x0400287C RID: 10364
	private bool mozeDaSkociOpet;

	// Token: 0x0400287D RID: 10365
	private float startY;

	// Token: 0x0400287E RID: 10366
	private float endX;

	// Token: 0x0400287F RID: 10367
	private float endY;

	// Token: 0x04002880 RID: 10368
	private bool swoosh;

	// Token: 0x04002881 RID: 10369
	private bool grab;

	// Token: 0x04002882 RID: 10370
	private bool snappingToClimb;

	// Token: 0x04002883 RID: 10371
	private Vector3 colliderForClimb;

	// Token: 0x04002884 RID: 10372
	public bool Glide;

	// Token: 0x04002885 RID: 10373
	public bool DupliSkok;

	// Token: 0x04002886 RID: 10374
	public bool KontrolisaniSkok;

	// Token: 0x04002887 RID: 10375
	public bool SlideNaDole;

	// Token: 0x04002888 RID: 10376
	public bool Zaustavljanje;

	// Token: 0x04002889 RID: 10377
	private Ray2D ray;

	// Token: 0x0400288A RID: 10378
	private RaycastHit2D hit;

	// Token: 0x0400288B RID: 10379
	private Transform ceilingCheck;

	// Token: 0x0400288C RID: 10380
	private Transform groundCheck;

	// Token: 0x0400288D RID: 10381
	private Transform majmun;

	// Token: 0x0400288E RID: 10382
	public ParticleSystem trava;

	// Token: 0x0400288F RID: 10383
	public ParticleSystem oblak;

	// Token: 0x04002890 RID: 10384
	public ParticleSystem particleSkok;

	// Token: 0x04002891 RID: 10385
	public ParticleSystem dupliSkokOblaci;

	// Token: 0x04002892 RID: 10386
	public ParticleSystem runParticle;

	// Token: 0x04002893 RID: 10387
	public ParticleSystem klizanje;

	// Token: 0x04002894 RID: 10388
	public ParticleSystem izrazitiPad;

	// Token: 0x04002895 RID: 10389
	public Transform zutiGlowSwooshVisoki;

	// Token: 0x04002896 RID: 10390
	private Transform whatToClimb;

	// Token: 0x04002897 RID: 10391
	private float currentSpeed;

	// Token: 0x04002898 RID: 10392
	[HideInInspector]
	public GameObject cameraTarget;

	// Token: 0x04002899 RID: 10393
	[HideInInspector]
	public GameObject cameraTarget_down;

	// Token: 0x0400289A RID: 10394
	private float cameraTarget_down_y;

	// Token: 0x0400289B RID: 10395
	private CameraFollow2D_new cameraFollow;

	// Token: 0x0400289C RID: 10396
	public MonkeyController2D_bekapce.State state;

	// Token: 0x0400289D RID: 10397
	[HideInInspector]
	public float startSpeedX;

	// Token: 0x0400289E RID: 10398
	[HideInInspector]
	public float startJumpSpeedX;

	// Token: 0x0400289F RID: 10399
	public bool neTrebaDaProdje;

	// Token: 0x040028A0 RID: 10400
	[HideInInspector]
	public Animator animator;

	// Token: 0x040028A1 RID: 10401
	private Animator parentAnim;

	// Token: 0x040028A2 RID: 10402
	[HideInInspector]
	public string lastPlayedAnim;

	// Token: 0x040028A3 RID: 10403
	private bool helpBool;

	// Token: 0x040028A4 RID: 10404
	private AnimatorStateInfo currentBaseState;

	// Token: 0x040028A5 RID: 10405
	[HideInInspector]
	public int run_State = Animator.StringToHash("Base Layer.Running");

	// Token: 0x040028A6 RID: 10406
	[HideInInspector]
	public int jump_State = Animator.StringToHash("Base Layer.Jump_Start");

	// Token: 0x040028A7 RID: 10407
	[HideInInspector]
	public int fall_State = Animator.StringToHash("Base Layer.Jump_Falling");

	// Token: 0x040028A8 RID: 10408
	[HideInInspector]
	public int landing_State = Animator.StringToHash("Base Layer.Landing");

	// Token: 0x040028A9 RID: 10409
	[HideInInspector]
	public int doublejump_State = Animator.StringToHash("Base Layer.DoubleJump_Start");

	// Token: 0x040028AA RID: 10410
	[HideInInspector]
	public int glide_start_State = Animator.StringToHash("Base Layer.Glide_Start");

	// Token: 0x040028AB RID: 10411
	[HideInInspector]
	public int glide_loop_State = Animator.StringToHash("Base Layer.Glide_Loop");

	// Token: 0x040028AC RID: 10412
	[HideInInspector]
	public int grab_State = Animator.StringToHash("Base Layer.Grab");

	// Token: 0x040028AD RID: 10413
	[HideInInspector]
	public int wall_stop_State = Animator.StringToHash("Base Layer.WallStop");

	// Token: 0x040028AE RID: 10414
	[HideInInspector]
	public int wall_stop_from_jump_State = Animator.StringToHash("Base Layer.WallStop_From_Jump");

	// Token: 0x040028AF RID: 10415
	[HideInInspector]
	public int swoosh_State = Animator.StringToHash("Base Layer.Swoosh_Down");

	// Token: 0x040028B0 RID: 10416
	[HideInInspector]
	public int spikedeath_State = Animator.StringToHash("Base Layer.Spike_death");

	// Token: 0x040028B1 RID: 10417
	[HideInInspector]
	public int lijana_State = Animator.StringToHash("Base Layer.Lijana");

	// Token: 0x040028B2 RID: 10418
	private Transform lookAtPos;

	// Token: 0x040028B3 RID: 10419
	private float lookWeight;

	// Token: 0x040028B4 RID: 10420
	private bool disableGlide;

	// Token: 0x040028B5 RID: 10421
	private bool helper_disableMoveAfterGrab;

	// Token: 0x040028B6 RID: 10422
	private bool usporavanje;

	// Token: 0x040028B7 RID: 10423
	private bool sudarioSeSaZidom;

	// Token: 0x040028B8 RID: 10424
	[HideInInspector]
	public bool lijana;

	// Token: 0x040028B9 RID: 10425
	private Transform grabLianaTransform;

	// Token: 0x040028BA RID: 10426
	[HideInInspector]
	public bool heCanJump = true;

	// Token: 0x040028BB RID: 10427
	private bool saZidaNaZid;

	// Token: 0x040028BC RID: 10428
	private int povrsinaZaClick;

	// Token: 0x040028BD RID: 10429
	private bool jumpControlled;

	// Token: 0x040028BE RID: 10430
	private float tempForce;

	// Token: 0x040028BF RID: 10431
	[HideInInspector]
	public bool activeShield;

	// Token: 0x040028C0 RID: 10432
	private float razmrk;

	// Token: 0x040028C1 RID: 10433
	private Vector3 pocScale;

	// Token: 0x040028C2 RID: 10434
	private Transform senka;

	// Token: 0x040028C3 RID: 10435
	public static bool canRespawnThings = true;

	// Token: 0x040028C4 RID: 10436
	[SerializeField]
	private LayerMask whatIsGround;

	// Token: 0x040028C5 RID: 10437
	private float groundedRadius = 0.2f;

	// Token: 0x040028C6 RID: 10438
	private bool grounded;

	// Token: 0x040028C7 RID: 10439
	private float startVelY;

	// Token: 0x040028C8 RID: 10440
	private float korakce;

	// Token: 0x040028C9 RID: 10441
	[HideInInspector]
	public bool canGlide;

	// Token: 0x040028CA RID: 10442
	private int proveraGround = 16;

	// Token: 0x040028CB RID: 10443
	private bool jumpHolding;

	// Token: 0x040028CC RID: 10444
	private TrailRenderer trail;

	// Token: 0x040028CD RID: 10445
	private bool powerfullImpact;

	// Token: 0x040028CE RID: 10446
	public float trailTime = 0.5f;

	// Token: 0x0200070E RID: 1806
	[HideInInspector]
	public enum State
	{
		// Token: 0x040028D0 RID: 10448
		running,
		// Token: 0x040028D1 RID: 10449
		jumped,
		// Token: 0x040028D2 RID: 10450
		wallhit,
		// Token: 0x040028D3 RID: 10451
		climbUp,
		// Token: 0x040028D4 RID: 10452
		actualClimbing,
		// Token: 0x040028D5 RID: 10453
		wasted,
		// Token: 0x040028D6 RID: 10454
		idle,
		// Token: 0x040028D7 RID: 10455
		completed,
		// Token: 0x040028D8 RID: 10456
		lijana,
		// Token: 0x040028D9 RID: 10457
		saZidaNaZid,
		// Token: 0x040028DA RID: 10458
		preNegoDaSeOdbije
	}
}
