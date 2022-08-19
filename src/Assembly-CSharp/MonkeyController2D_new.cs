using System;
using System.Collections;
using UnityEngine;

// Token: 0x020004C8 RID: 1224
public class MonkeyController2D_new : MonoBehaviour
{
	// Token: 0x06002774 RID: 10100 RVA: 0x00125ABC File Offset: 0x00123CBC
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

	// Token: 0x06002775 RID: 10101 RVA: 0x00125B74 File Offset: 0x00123D74
	private void Start()
	{
		this.startSpeedX = this.maxSpeedX;
		this.startJumpSpeedX = this.jumpSpeedX;
		this.state = MonkeyController2D_new.State.idle;
		Resources.UnloadUnusedAssets();
		this.cameraTarget_down.transform.position = this.cameraTarget.transform.position;
		this.currentBaseState = this.animator.GetCurrentAnimatorStateInfo(0);
		this.animator.speed = 1.5f;
		this.animator.SetLookAtWeight(this.lookWeight);
		this.animator.SetLookAtPosition(this.lookAtPos.position);
		this.tempForce = this.jumpForce;
		this.senka = GameObject.Find("shadowMonkey").transform;
		MonkeyController2D_new.canRespawnThings = true;
		this.groundCheck = base.transform.Find("GroundCheck");
	}

	// Token: 0x06002776 RID: 10102 RVA: 0x00125C4C File Offset: 0x00123E4C
	private void Update()
	{
		this.hit = Physics2D.Linecast(base.transform.position + new Vector3(0.8f, 2.5f, 0f), base.transform.position + new Vector3(0.8f, -15.5f, 0f), 1 << LayerMask.NameToLayer("Ground") | 1 << LayerMask.NameToLayer("Platform"));
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
		if (this.snappingToClimb)
		{
			base.StartCoroutine(this.snappingProcess());
			this.snappingToClimb = false;
		}
		if (this.grab)
		{
			base.StartCoroutine(this.ProceduraPenjanja(null));
			this.grab = false;
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
		if (this.currentBaseState.nameHash == this.doublejump_State)
		{
			if (this.animator.GetBool("DoubleJump"))
			{
				this.animator.SetBool("DoubleJump", false);
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
		this.grounded = Physics2D.OverlapCircle(this.groundCheck.position, this.groundedRadius, this.whatIsGround);
		this.CheckWallHitNear = Physics2D.Linecast(base.transform.position + new Vector3(0.8f, 2.5f, 0f), base.transform.position + new Vector3(3.5f, 2.5f, 0f), 1 << LayerMask.NameToLayer("WallHit"));
		this.CheckWallHitNear_low = Physics2D.Linecast(base.transform.position + new Vector3(0.8f, 0f, 0f), base.transform.position + new Vector3(3.5f, 0f, 0f), 1 << LayerMask.NameToLayer("WallHit"));
		this.triggerCheckDown = Physics2D.Linecast(base.transform.position + new Vector3(0.8f, 2.5f, 0f), base.transform.position + new Vector3(0.8f, -0.5f, 0f), 1 << LayerMask.NameToLayer("Ground") | 1 << LayerMask.NameToLayer("Platform"));
		this.triggerCheckDownTrigger = Physics2D.Linecast(base.transform.position + new Vector3(0.8f, 2.5f, 0f), base.transform.position + new Vector3(0.8f, -4.5f, 0f), 1 << LayerMask.NameToLayer("Ground") | 1 << LayerMask.NameToLayer("Platform"));
		this.triggerCheckDownBehind = Physics2D.Linecast(base.transform.position + new Vector3(-0.8f, 2.5f, 0f), base.transform.position + new Vector3(-0.8f, -4.5f, 0f), 1 << LayerMask.NameToLayer("Platform"));
		this.proveriTerenIspredY = Physics2D.Linecast(base.transform.position + new Vector3(4.4f, 1.2f, 0f), base.transform.position + new Vector3(4.4f, -3.2f, 0f), 1 << LayerMask.NameToLayer("Ground") | 1 << LayerMask.NameToLayer("Platform"));
		this.downHit = Physics2D.Linecast(base.transform.position + new Vector3(0.2f, 0.1f, 0f), base.transform.position + new Vector3(0.2f, -0.65f, 0f), 1 << LayerMask.NameToLayer("Ground") | 1 << LayerMask.NameToLayer("Platform"));
		this.spustanjeRastojanje = Physics2D.Linecast(base.transform.position + new Vector3(2.3f, 1.25f, 0f), base.transform.position + new Vector3(2.3f, -Camera.main.orthographicSize, 0f), 1 << LayerMask.NameToLayer("Ground") | 1 << LayerMask.NameToLayer("Platform"));
		if (this.state == MonkeyController2D_new.State.jumped || this.state == MonkeyController2D_new.State.climbUp)
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
				}
				else if (Input.touchCount == 1 && Input.GetTouch(0).phase == 3)
				{
					this.startY = (this.endY = 0f);
					base.GetComponent<Rigidbody2D>().drag = 0f;
					this.animator.SetBool("Glide", false);
					this.canGlide = false;
				}
			}
			if (Input.GetMouseButton(0))
			{
				this.endY = Input.mousePosition.y;
				if (this.SlideNaDole && this.startY - this.endY > 0.25f * Camera.main.orthographicSize * ((float)Screen.height / (2f * Camera.main.orthographicSize)))
				{
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
					base.GetComponent<Rigidbody2D>().drag = 7.5f;
				}
			}
			if (this.KontrolisaniSkok)
			{
				if (Input.GetMouseButton(0) && this.jumpControlled)
				{
					base.GetComponent<Rigidbody2D>().velocity = new Vector2(base.GetComponent<Rigidbody2D>().velocity.x, base.GetComponent<Rigidbody2D>().velocity.y + this.korakce);
					if (this.korakce > 0f)
					{
						this.korakce -= 0.085f;
					}
				}
				if ((Input.GetMouseButtonUp(0) || Time.time - this.duzinaPritiskaZaSkok > 1f) && this.jumpControlled)
				{
					this.jumpControlled = false;
					this.tempForce = this.jumpForce;
					this.canGlide = false;
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
		if (this.state == MonkeyController2D_new.State.wallhit)
		{
			if (this.KontrolisaniSkok)
			{
				if (((Input.GetMouseButtonDown(0) && Input.mousePosition.x > (float)this.povrsinaZaClick) || Input.GetKeyDown(32)) && this.RaycastFunction(Input.mousePosition) != "Pause" && this.RaycastFunction(Input.mousePosition) != "HoleButtonsPlay_Pause" && this.RaycastFunction(Input.mousePosition) != "Buy Button" && this.RaycastFunction(Input.mousePosition) != "ButtonMain_NoMoreLives" && this.RaycastFunction(Input.mousePosition) != "HoleButtonsRestart_Pause" && !this.RaycastFunction(Input.mousePosition).Contains("Tutorial"))
				{
					this.duzinaPritiskaZaSkok = Time.time;
					if (!this.inAir)
					{
						this.state = MonkeyController2D_new.State.climbUp;
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
					this.state = MonkeyController2D_new.State.running;
					this.maxSpeedX = this.startSpeedX;
					this.animator.Play(this.run_State);
					this.animator.SetBool("WallStop", false);
				}
			}
			else if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(32))
			{
				if (this.RaycastFunction(Input.mousePosition) != "Pause" && this.RaycastFunction(Input.mousePosition) != "HoleButtonsPlay_Pause" && this.RaycastFunction(Input.mousePosition) != "Buy Button" && this.RaycastFunction(Input.mousePosition) != "ButtonMain_NoMoreLives" && this.RaycastFunction(Input.mousePosition) != "HoleButtonsRestart_Pause" && !this.RaycastFunction(Input.mousePosition).Contains("Tutorial") && !this.inAir)
				{
					this.state = MonkeyController2D_new.State.climbUp;
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
				this.state = MonkeyController2D_new.State.running;
				this.maxSpeedX = this.startSpeedX;
				this.animator.Play(this.run_State);
				this.animator.SetBool("WallStop", false);
			}
		}
		if (this.state == MonkeyController2D_new.State.running)
		{
			if (Time.frameCount % 300 == 0 && Random.Range(1, 100) <= 25)
			{
				base.StartCoroutine(this.turnHead(0.1f));
			}
			if (this.KontrolisaniSkok)
			{
				if (((Input.GetMouseButtonDown(0) && Input.mousePosition.x > (float)this.povrsinaZaClick) || Input.GetKeyDown(32)) && this.RaycastFunction(Input.mousePosition) != "Pause" && this.RaycastFunction(Input.mousePosition) != "HoleButtonsPlay_Pause" && this.RaycastFunction(Input.mousePosition) != "Buy Button" && this.RaycastFunction(Input.mousePosition) != "ButtonMain_NoMoreLives" && this.RaycastFunction(Input.mousePosition) != "HoleButtonsRestart_Pause" && !this.RaycastFunction(Input.mousePosition).Contains("Tutorial"))
				{
					this.startVelY = base.GetComponent<Rigidbody2D>().velocity.y;
					this.korakce = 3f;
					base.GetComponent<Rigidbody2D>().velocity = new Vector2(base.GetComponent<Rigidbody2D>().velocity.x, this.startVelY + this.maxSpeedY);
					this.neTrebaDaProdje = false;
					this.duzinaPritiskaZaSkok = Time.time;
					this.state = MonkeyController2D_new.State.jumped;
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
				this.state = MonkeyController2D_new.State.jumped;
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
				this.state = MonkeyController2D_new.State.wallhit;
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
		if (this.state == MonkeyController2D_new.State.lijana)
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
		if (this.state == MonkeyController2D_new.State.saZidaNaZid)
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
		else if (this.state == MonkeyController2D_new.State.preNegoDaSeOdbije && this.saZidaNaZid && (Input.GetMouseButtonDown(0) || Input.GetKeyDown(32)))
		{
			this.jump = true;
			base.GetComponent<Rigidbody2D>().drag = 0f;
			this.state = MonkeyController2D_new.State.saZidaNaZid;
			this.animator.Play(this.jump_State);
			this.animator.SetBool("Landing", false);
			this.animator.SetBool("WallStop", false);
			this.particleSkok.Emit(20);
			this.jumpSafetyCheck = true;
		}
	}

	// Token: 0x06002777 RID: 10103 RVA: 0x00127350 File Offset: 0x00125550
	private void FixedUpdate()
	{
		this.currentBaseState = this.animator.GetCurrentAnimatorStateInfo(0);
		if (this.state == MonkeyController2D_new.State.saZidaNaZid)
		{
			if (this.jump)
			{
				this.hasJumped = true;
				this.mozeDaSkociOpet = true;
				if (base.GetComponent<Rigidbody2D>().velocity.y < this.maxSpeedY)
				{
					base.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
					base.GetComponent<Rigidbody2D>().AddForce(new Vector2(base.GetComponent<Rigidbody2D>().velocity.x, 2500f));
				}
				if (Mathf.Abs(base.GetComponent<Rigidbody2D>().velocity.y) > this.maxSpeedY)
				{
					base.GetComponent<Rigidbody2D>().velocity = new Vector2(base.GetComponent<Rigidbody2D>().velocity.x, this.maxSpeedY);
				}
				this.jump = false;
			}
			base.GetComponent<Rigidbody2D>().velocity = new Vector2(this.jumpSpeedX, base.GetComponent<Rigidbody2D>().velocity.y);
		}
		else if (this.state == MonkeyController2D_new.State.preNegoDaSeOdbije)
		{
			if (this.jump)
			{
				this.hasJumped = true;
				this.mozeDaSkociOpet = true;
				if (base.GetComponent<Rigidbody2D>().velocity.y < this.maxSpeedY)
				{
					base.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
					base.GetComponent<Rigidbody2D>().AddForce(new Vector2(base.GetComponent<Rigidbody2D>().velocity.x, 2500f));
				}
				if (Mathf.Abs(base.GetComponent<Rigidbody2D>().velocity.y) > this.maxSpeedY)
				{
					base.GetComponent<Rigidbody2D>().velocity = new Vector2(base.GetComponent<Rigidbody2D>().velocity.x, this.maxSpeedY);
				}
				this.jump = false;
			}
			base.GetComponent<Rigidbody2D>().velocity = new Vector2(this.jumpSpeedX, base.GetComponent<Rigidbody2D>().velocity.y);
			if (base.GetComponent<Rigidbody2D>().velocity.y < -0.01f)
			{
				base.GetComponent<Rigidbody2D>().drag = 5f;
			}
		}
		else if (this.state == MonkeyController2D_new.State.wasted)
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
		if (this.state == MonkeyController2D_new.State.running)
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
		else if (this.state == MonkeyController2D_new.State.completed)
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
		else if (this.state == MonkeyController2D_new.State.jumped)
		{
			if (this.swoosh)
			{
				base.GetComponent<Rigidbody2D>().AddForce(new Vector2(base.GetComponent<Rigidbody2D>().velocity.x, -3000f));
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
		else if (this.state == MonkeyController2D_new.State.wallhit)
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
		else if (this.state == MonkeyController2D_new.State.climbUp)
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
				base.GetComponent<Rigidbody2D>().AddForce(new Vector2(base.GetComponent<Rigidbody2D>().velocity.x, this.jumpForce));
				this.hasJumped = true;
				this.jump = false;
			}
			else if (this.jumpControlled)
			{
				this.hasJumped = true;
				this.mozeDaSkociOpet = true;
				if (base.GetComponent<Rigidbody2D>().velocity.y < this.maxSpeedY)
				{
					base.GetComponent<Rigidbody2D>().AddForce(new Vector2(base.GetComponent<Rigidbody2D>().velocity.x, this.tempForce));
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

	// Token: 0x06002778 RID: 10104 RVA: 0x00127AB4 File Offset: 0x00125CB4
	private void OnTriggerExit2D(Collider2D col)
	{
		if (col.tag == "Footer" && base.GetComponent<Collider2D>().isTrigger && base.transform.position.y > col.transform.position.y)
		{
			base.GetComponent<Collider2D>().isTrigger = false;
			this.neTrebaDaProdje = false;
		}
	}

	// Token: 0x06002779 RID: 10105 RVA: 0x00127B18 File Offset: 0x00125D18
	private void OnCollisionEnter2D(Collision2D col)
	{
		if (this.state != MonkeyController2D_new.State.completed || this.state != MonkeyController2D_new.State.wasted)
		{
			if (col.gameObject.tag == "ZidZaOdbijanje")
			{
				if (this.state == MonkeyController2D_new.State.running && this.CheckWallHitNear)
				{
					if (this.klizanje.isPlaying)
					{
						this.klizanje.Stop();
					}
					this.animator.SetBool("WallStop", true);
					this.animator.Play(this.wall_stop_State);
					this.state = MonkeyController2D_new.State.preNegoDaSeOdbije;
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
				else if (this.state != MonkeyController2D_new.State.preNegoDaSeOdbije)
				{
					this.inAir = false;
					this.heCanJump = true;
					base.GetComponent<Rigidbody2D>().drag = 5f;
					this.klizanje.Play();
					this.animator.Play("Klizanje");
					this.state = MonkeyController2D_new.State.saZidaNaZid;
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
				if (this.state == MonkeyController2D_new.State.saZidaNaZid)
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
						this.state = MonkeyController2D_new.State.preNegoDaSeOdbije;
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
						this.state = MonkeyController2D_new.State.running;
						this.canGlide = false;
						if (PlaySounds.soundOn)
						{
							PlaySounds.Play_Run();
						}
						this.hasJumped = false;
						this.startY = (this.endY = 0f);
						this.inAir = false;
					}
				}
				if (this.state == MonkeyController2D_new.State.jumped)
				{
					if (PlaySounds.soundOn)
					{
						PlaySounds.Play_Landing();
					}
					this.oblak.Play();
					if (!base.GetComponent<Collider2D>().isTrigger && this.triggerCheckDownTrigger)
					{
						if (this.startSpustanje)
						{
							this.startSpustanje = false;
							this.cameraTarget_down.transform.parent = base.transform;
							this.cameraTarget_down.transform.position = this.cameraTarget.transform.position;
						}
						this.jumpSpeedX = this.startJumpSpeedX;
						this.mozeDaSkociOpet = false;
						this.animator.SetBool("Jump", false);
						this.animator.SetBool("DoubleJump", false);
						this.animator.SetBool("Glide", false);
						this.disableGlide = false;
						this.animator.SetBool("Landing", true);
						base.GetComponent<Rigidbody2D>().drag = 0f;
						this.state = MonkeyController2D_new.State.running;
						this.canGlide = false;
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
					if (this.state != MonkeyController2D_new.State.running)
					{
						base.GetComponent<Rigidbody2D>().AddForce(new Vector2(base.GetComponent<Rigidbody2D>().velocity.x, 1100f));
						return;
					}
				}
				else if (!this.killed)
				{
					if (this.state == MonkeyController2D_new.State.running)
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

	// Token: 0x0600277A RID: 10106 RVA: 0x0012807A File Offset: 0x0012627A
	public void majmunUtepanULetu()
	{
		base.StartCoroutine(this.FallDownAfterSpikes());
	}

	// Token: 0x0600277B RID: 10107 RVA: 0x00128089 File Offset: 0x00126289
	private IEnumerator FallDownAfterSpikes()
	{
		MonkeyController2D_new.canRespawnThings = false;
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
		this.state = MonkeyController2D_new.State.wasted;
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

	// Token: 0x0600277C RID: 10108 RVA: 0x00128098 File Offset: 0x00126298
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
		this.state = MonkeyController2D_new.State.running;
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
		this.state = MonkeyController2D_new.State.running;
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

	// Token: 0x0600277D RID: 10109 RVA: 0x001280A7 File Offset: 0x001262A7
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

	// Token: 0x0600277E RID: 10110 RVA: 0x001280B8 File Offset: 0x001262B8
	private void OnCollisionExit2D(Collision2D col)
	{
		if (col.gameObject.tag == "Footer")
		{
			if (this.state == MonkeyController2D_new.State.running)
			{
				this.neTrebaDaProdje = false;
				if (!this.proveriTerenIspredY && !this.downHit)
				{
					this.state = MonkeyController2D_new.State.jumped;
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

	// Token: 0x0600277F RID: 10111 RVA: 0x001250F1 File Offset: 0x001232F1
	private void NotifyManagerForFinish()
	{
		GameObject.Find("_GameManager").SendMessage("ShowWinScreen");
	}

	// Token: 0x06002780 RID: 10112 RVA: 0x00128215 File Offset: 0x00126415
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

	// Token: 0x06002781 RID: 10113 RVA: 0x0012823C File Offset: 0x0012643C
	private void OnTriggerEnter2D(Collider2D col)
	{
		if (this.state != MonkeyController2D_new.State.completed || this.state != MonkeyController2D_new.State.wasted)
		{
			if (col.tag == "Barrel")
			{
				col.transform.GetChild(0).GetComponent<Animator>().Play("BarrelBoom");
			}
			else if (col.name == "Magnet")
			{
				GameObject.Find("_GameManager").SendMessage("ApplyPowerUp", 1);
				col.gameObject.SetActive(false);
			}
			else if (col.name == "Banana_Coin_X2")
			{
				GameObject.Find("_GameManager").SendMessage("ApplyPowerUp", 2);
				col.gameObject.SetActive(false);
			}
			else if (col.name == "Banana_Shield")
			{
				GameObject.Find("_GameManager").SendMessage("ApplyPowerUp", 3);
				col.gameObject.SetActive(false);
				this.activeShield = true;
			}
			if (col.tag == "Finish")
			{
				col.GetComponent<Collider2D>().enabled = false;
				this.cameraFollow.cameraFollowX = false;
				base.Invoke("NotifyManagerForFinish", 1.25f);
				GameObject.Find("Pause").GetComponent<Collider>().enabled = false;
				this.state = MonkeyController2D_new.State.completed;
				return;
			}
			if (col.tag == "Footer")
			{
				if (base.transform.position.y + 0.5f > col.transform.position.y && this.triggerCheckDownTrigger && this.triggerCheckDownBehind && base.GetComponent<Collider2D>().isTrigger)
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
					if (this.state != MonkeyController2D_new.State.running)
					{
						base.GetComponent<Rigidbody2D>().AddForce(new Vector2(base.GetComponent<Rigidbody2D>().velocity.x, 1100f));
						return;
					}
				}
				else if (!this.killed)
				{
					if (this.state == MonkeyController2D_new.State.running)
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
						this.state = MonkeyController2D_new.State.jumped;
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
						this.state = MonkeyController2D_new.State.lijana;
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

	// Token: 0x06002782 RID: 10114 RVA: 0x001285F4 File Offset: 0x001267F4
	private void OnTriggerStay2D(Collider2D col)
	{
		if (col.tag == "GrabLedge")
		{
			base.GetComponent<Rigidbody2D>().velocity = new Vector2(0f, Mathf.MoveTowards(base.GetComponent<Rigidbody2D>().velocity.y, 0f, 0.2f));
		}
	}

	// Token: 0x06002783 RID: 10115 RVA: 0x00128647 File Offset: 0x00126847
	private IEnumerator pratiLijanaTarget(Transform target)
	{
		while (this.lijana)
		{
			yield return null;
			base.transform.position = Vector3.Lerp(base.transform.position, new Vector3(target.position.x, target.position.y, base.transform.position.z), 0.2f);
		}
		yield break;
	}

	// Token: 0x06002784 RID: 10116 RVA: 0x00128660 File Offset: 0x00126860
	public void OtkaciMajmuna()
	{
		this.lijana = false;
		base.StopCoroutine("pratiLijanaTarget");
		this.state = MonkeyController2D_new.State.jumped;
		this.cameraFollow.cameraFollowX = true;
		this.maxSpeedX = this.startSpeedX;
		this.jumpSpeedX = this.startJumpSpeedX;
		base.transform.parent = null;
		base.transform.rotation = Quaternion.identity;
		base.GetComponent<Rigidbody2D>().isKinematic = false;
		base.GetComponent<Rigidbody2D>().AddForce(new Vector2(200f, 2500f));
		this.animator.Play(this.jump_State);
	}

	// Token: 0x06002785 RID: 10117 RVA: 0x00128700 File Offset: 0x00126900
	private void SpustiMajmunaSaLijaneBrzo()
	{
		this.lijana = false;
		base.StopCoroutine("pratiLijanaTarget");
		this.state = MonkeyController2D_new.State.jumped;
		this.cameraFollow.cameraFollowX = true;
		this.maxSpeedX = this.startSpeedX;
		this.jumpSpeedX = this.startJumpSpeedX;
		base.transform.parent = null;
		base.transform.rotation = Quaternion.identity;
		base.GetComponent<Rigidbody2D>().isKinematic = false;
		this.animator.Play(this.jump_State);
	}

	// Token: 0x06002786 RID: 10118 RVA: 0x00128783 File Offset: 0x00126983
	private IEnumerator Bounce(float time)
	{
		yield return new WaitForSeconds(time);
		this.stop = false;
		yield break;
	}

	// Token: 0x06002787 RID: 10119 RVA: 0x00128799 File Offset: 0x00126999
	private void climb()
	{
		base.StartCoroutine(this.MoveUp(0.05f));
	}

	// Token: 0x06002788 RID: 10120 RVA: 0x001287AD File Offset: 0x001269AD
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
			this.state = MonkeyController2D_new.State.running;
			if (PlaySounds.soundOn)
			{
				PlaySounds.Play_Run();
			}
			this.hasJumped = false;
		}
		yield break;
	}

	// Token: 0x06002789 RID: 10121 RVA: 0x001287C3 File Offset: 0x001269C3
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

	// Token: 0x0600278A RID: 10122 RVA: 0x001287D4 File Offset: 0x001269D4
	public void majmunUtepan()
	{
		MonkeyController2D_new.canRespawnThings = false;
		GameObject.Find("PrinceGorilla").GetComponent<Animator>().speed = 1.5f;
		this.state = MonkeyController2D_new.State.wasted;
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

	// Token: 0x0600278B RID: 10123 RVA: 0x00128891 File Offset: 0x00126A91
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
		base.transform.Find("HolderKillStars").gameObject.SetActive(true);
		yield break;
	}

	// Token: 0x0600278C RID: 10124 RVA: 0x00004095 File Offset: 0x00002295
	public void CallShake()
	{
	}

	// Token: 0x0600278D RID: 10125 RVA: 0x001288A0 File Offset: 0x00126AA0
	private IEnumerator shakeCamera()
	{
		yield return null;
		yield break;
	}

	// Token: 0x0600278E RID: 10126 RVA: 0x001288A8 File Offset: 0x00126AA8
	private string RaycastFunction(Vector3 vector)
	{
		RaycastHit raycastHit;
		if (Physics.Raycast(Camera.main.ScreenPointToRay(vector), ref raycastHit))
		{
			return raycastHit.collider.name;
		}
		return "";
	}

	// Token: 0x0600278F RID: 10127 RVA: 0x001288DB File Offset: 0x00126ADB
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

	// Token: 0x06002790 RID: 10128 RVA: 0x001288F1 File Offset: 0x00126AF1
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

	// Token: 0x06002791 RID: 10129 RVA: 0x00128900 File Offset: 0x00126B00
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

	// Token: 0x04002213 RID: 8723
	public float moveForce = 300f;

	// Token: 0x04002214 RID: 8724
	public float maxSpeedX = 8f;

	// Token: 0x04002215 RID: 8725
	public float jumpForce = 700f;

	// Token: 0x04002216 RID: 8726
	public float doubleJumpForce = 100f;

	// Token: 0x04002217 RID: 8727
	public float gravity = 200f;

	// Token: 0x04002218 RID: 8728
	public float maxSpeedY = 8f;

	// Token: 0x04002219 RID: 8729
	public float jumpSpeedX = 12f;

	// Token: 0x0400221A RID: 8730
	private bool jump;

	// Token: 0x0400221B RID: 8731
	private bool doubleJump;

	// Token: 0x0400221C RID: 8732
	[HideInInspector]
	public bool inAir;

	// Token: 0x0400221D RID: 8733
	private bool hasJumped;

	// Token: 0x0400221E RID: 8734
	private bool jumpSafetyCheck;

	// Token: 0x0400221F RID: 8735
	private bool proveriTerenIspredY;

	// Token: 0x04002220 RID: 8736
	private bool downHit;

	// Token: 0x04002221 RID: 8737
	[HideInInspector]
	public bool zgaziEnemija;

	// Token: 0x04002222 RID: 8738
	[HideInInspector]
	public bool killed;

	// Token: 0x04002223 RID: 8739
	[HideInInspector]
	public bool stop;

	// Token: 0x04002224 RID: 8740
	private bool triggerCheckDown;

	// Token: 0x04002225 RID: 8741
	[HideInInspector]
	public bool triggerCheckDownTrigger;

	// Token: 0x04002226 RID: 8742
	[HideInInspector]
	public bool triggerCheckDownBehind;

	// Token: 0x04002227 RID: 8743
	private bool CheckWallHitNear;

	// Token: 0x04002228 RID: 8744
	private bool CheckWallHitNear_low;

	// Token: 0x04002229 RID: 8745
	private bool startSpustanje;

	// Token: 0x0400222A RID: 8746
	private bool startPenjanje;

	// Token: 0x0400222B RID: 8747
	private bool spustanjeRastojanje;

	// Token: 0x0400222C RID: 8748
	private float pocetniY_spustanje;

	// Token: 0x0400222D RID: 8749
	[HideInInspector]
	public float collisionAngle;

	// Token: 0x0400222E RID: 8750
	private float duzinaPritiskaZaSkok;

	// Token: 0x0400222F RID: 8751
	private bool mozeDaSkociOpet;

	// Token: 0x04002230 RID: 8752
	private float startY;

	// Token: 0x04002231 RID: 8753
	private float endX;

	// Token: 0x04002232 RID: 8754
	private float endY;

	// Token: 0x04002233 RID: 8755
	private bool swoosh;

	// Token: 0x04002234 RID: 8756
	private bool grab;

	// Token: 0x04002235 RID: 8757
	private bool snappingToClimb;

	// Token: 0x04002236 RID: 8758
	private Vector3 colliderForClimb;

	// Token: 0x04002237 RID: 8759
	public bool Glide;

	// Token: 0x04002238 RID: 8760
	public bool DupliSkok;

	// Token: 0x04002239 RID: 8761
	public bool KontrolisaniSkok;

	// Token: 0x0400223A RID: 8762
	public bool SlideNaDole;

	// Token: 0x0400223B RID: 8763
	public bool Zaustavljanje;

	// Token: 0x0400223C RID: 8764
	private Ray2D ray;

	// Token: 0x0400223D RID: 8765
	private RaycastHit2D hit;

	// Token: 0x0400223E RID: 8766
	private Transform groundCheck;

	// Token: 0x0400223F RID: 8767
	private Transform majmun;

	// Token: 0x04002240 RID: 8768
	public ParticleSystem trava;

	// Token: 0x04002241 RID: 8769
	public ParticleSystem oblak;

	// Token: 0x04002242 RID: 8770
	public ParticleSystem particleSkok;

	// Token: 0x04002243 RID: 8771
	public ParticleSystem dupliSkokOblaci;

	// Token: 0x04002244 RID: 8772
	public ParticleSystem runParticle;

	// Token: 0x04002245 RID: 8773
	public ParticleSystem klizanje;

	// Token: 0x04002246 RID: 8774
	private Transform whatToClimb;

	// Token: 0x04002247 RID: 8775
	private float currentSpeed;

	// Token: 0x04002248 RID: 8776
	[HideInInspector]
	public GameObject cameraTarget;

	// Token: 0x04002249 RID: 8777
	[HideInInspector]
	public GameObject cameraTarget_down;

	// Token: 0x0400224A RID: 8778
	private float cameraTarget_down_y;

	// Token: 0x0400224B RID: 8779
	private CameraFollow2D_new cameraFollow;

	// Token: 0x0400224C RID: 8780
	public MonkeyController2D_new.State state;

	// Token: 0x0400224D RID: 8781
	[HideInInspector]
	public float startSpeedX;

	// Token: 0x0400224E RID: 8782
	[HideInInspector]
	public float startJumpSpeedX;

	// Token: 0x0400224F RID: 8783
	public bool neTrebaDaProdje;

	// Token: 0x04002250 RID: 8784
	[HideInInspector]
	public Animator animator;

	// Token: 0x04002251 RID: 8785
	private Animator parentAnim;

	// Token: 0x04002252 RID: 8786
	[HideInInspector]
	public string lastPlayedAnim;

	// Token: 0x04002253 RID: 8787
	private bool helpBool;

	// Token: 0x04002254 RID: 8788
	private AnimatorStateInfo currentBaseState;

	// Token: 0x04002255 RID: 8789
	private int run_State = Animator.StringToHash("Base Layer.Running");

	// Token: 0x04002256 RID: 8790
	private int jump_State = Animator.StringToHash("Base Layer.Jump_Start");

	// Token: 0x04002257 RID: 8791
	private int fall_State = Animator.StringToHash("Base Layer.Jump_Falling");

	// Token: 0x04002258 RID: 8792
	private int landing_State = Animator.StringToHash("Base Layer.Landing");

	// Token: 0x04002259 RID: 8793
	private int doublejump_State = Animator.StringToHash("Base Layer.DoubleJump_Start");

	// Token: 0x0400225A RID: 8794
	private int glide_start_State = Animator.StringToHash("Base Layer.Glide_Start");

	// Token: 0x0400225B RID: 8795
	private int glide_loop_State = Animator.StringToHash("Base Layer.Glide_Loop");

	// Token: 0x0400225C RID: 8796
	private int grab_State = Animator.StringToHash("Base Layer.Grab");

	// Token: 0x0400225D RID: 8797
	private int wall_stop_State = Animator.StringToHash("Base Layer.WallStop");

	// Token: 0x0400225E RID: 8798
	private int wall_stop_from_jump_State = Animator.StringToHash("Base Layer.WallStop_From_Jump");

	// Token: 0x0400225F RID: 8799
	private int swoosh_State = Animator.StringToHash("Base Layer.Swoosh_Down");

	// Token: 0x04002260 RID: 8800
	private int spikedeath_State = Animator.StringToHash("Base Layer.Spike_death");

	// Token: 0x04002261 RID: 8801
	private int lijana_State = Animator.StringToHash("Base Layer.Lijana");

	// Token: 0x04002262 RID: 8802
	private Transform lookAtPos;

	// Token: 0x04002263 RID: 8803
	private float lookWeight;

	// Token: 0x04002264 RID: 8804
	private bool disableGlide;

	// Token: 0x04002265 RID: 8805
	private bool helper_disableMoveAfterGrab;

	// Token: 0x04002266 RID: 8806
	private bool usporavanje;

	// Token: 0x04002267 RID: 8807
	private bool sudarioSeSaZidom;

	// Token: 0x04002268 RID: 8808
	[HideInInspector]
	public bool lijana;

	// Token: 0x04002269 RID: 8809
	private Transform grabLianaTransform;

	// Token: 0x0400226A RID: 8810
	[HideInInspector]
	public bool heCanJump = true;

	// Token: 0x0400226B RID: 8811
	private bool saZidaNaZid;

	// Token: 0x0400226C RID: 8812
	private int povrsinaZaClick;

	// Token: 0x0400226D RID: 8813
	private bool jumpControlled;

	// Token: 0x0400226E RID: 8814
	private float tempForce;

	// Token: 0x0400226F RID: 8815
	[HideInInspector]
	public bool activeShield;

	// Token: 0x04002270 RID: 8816
	private float razmrk;

	// Token: 0x04002271 RID: 8817
	private Vector3 pocScale;

	// Token: 0x04002272 RID: 8818
	private Transform senka;

	// Token: 0x04002273 RID: 8819
	public static bool canRespawnThings = true;

	// Token: 0x04002274 RID: 8820
	[SerializeField]
	private LayerMask whatIsGround;

	// Token: 0x04002275 RID: 8821
	private float groundedRadius = 0.2f;

	// Token: 0x04002276 RID: 8822
	private bool grounded;

	// Token: 0x04002277 RID: 8823
	private float startVelY;

	// Token: 0x04002278 RID: 8824
	private float korakce;

	// Token: 0x04002279 RID: 8825
	private bool canGlide;

	// Token: 0x02001440 RID: 5184
	[HideInInspector]
	public enum State
	{
		// Token: 0x04006B48 RID: 27464
		running,
		// Token: 0x04006B49 RID: 27465
		jumped,
		// Token: 0x04006B4A RID: 27466
		wallhit,
		// Token: 0x04006B4B RID: 27467
		climbUp,
		// Token: 0x04006B4C RID: 27468
		actualClimbing,
		// Token: 0x04006B4D RID: 27469
		wasted,
		// Token: 0x04006B4E RID: 27470
		idle,
		// Token: 0x04006B4F RID: 27471
		completed,
		// Token: 0x04006B50 RID: 27472
		lijana,
		// Token: 0x04006B51 RID: 27473
		saZidaNaZid,
		// Token: 0x04006B52 RID: 27474
		preNegoDaSeOdbije
	}
}
