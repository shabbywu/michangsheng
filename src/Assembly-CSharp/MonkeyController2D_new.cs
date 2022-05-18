using System;
using System.Collections;
using UnityEngine;

// Token: 0x0200071E RID: 1822
public class MonkeyController2D_new : MonoBehaviour
{
	// Token: 0x06002E2E RID: 11822 RVA: 0x0017090C File Offset: 0x0016EB0C
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

	// Token: 0x06002E2F RID: 11823 RVA: 0x001709C4 File Offset: 0x0016EBC4
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

	// Token: 0x06002E30 RID: 11824 RVA: 0x00170A9C File Offset: 0x0016EC9C
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

	// Token: 0x06002E31 RID: 11825 RVA: 0x001721A0 File Offset: 0x001703A0
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

	// Token: 0x06002E32 RID: 11826 RVA: 0x00172904 File Offset: 0x00170B04
	private void OnTriggerExit2D(Collider2D col)
	{
		if (col.tag == "Footer" && base.GetComponent<Collider2D>().isTrigger && base.transform.position.y > col.transform.position.y)
		{
			base.GetComponent<Collider2D>().isTrigger = false;
			this.neTrebaDaProdje = false;
		}
	}

	// Token: 0x06002E33 RID: 11827 RVA: 0x00172968 File Offset: 0x00170B68
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

	// Token: 0x06002E34 RID: 11828 RVA: 0x0002279D File Offset: 0x0002099D
	public void majmunUtepanULetu()
	{
		base.StartCoroutine(this.FallDownAfterSpikes());
	}

	// Token: 0x06002E35 RID: 11829 RVA: 0x000227AC File Offset: 0x000209AC
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

	// Token: 0x06002E36 RID: 11830 RVA: 0x000227BB File Offset: 0x000209BB
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

	// Token: 0x06002E37 RID: 11831 RVA: 0x000227CA File Offset: 0x000209CA
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

	// Token: 0x06002E38 RID: 11832 RVA: 0x00172ECC File Offset: 0x001710CC
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

	// Token: 0x06002E39 RID: 11833 RVA: 0x0002252B File Offset: 0x0002072B
	private void NotifyManagerForFinish()
	{
		GameObject.Find("_GameManager").SendMessage("ShowWinScreen");
	}

	// Token: 0x06002E3A RID: 11834 RVA: 0x000227D9 File Offset: 0x000209D9
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

	// Token: 0x06002E3B RID: 11835 RVA: 0x0017302C File Offset: 0x0017122C
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

	// Token: 0x06002E3C RID: 11836 RVA: 0x0016F804 File Offset: 0x0016DA04
	private void OnTriggerStay2D(Collider2D col)
	{
		if (col.tag == "GrabLedge")
		{
			base.GetComponent<Rigidbody2D>().velocity = new Vector2(0f, Mathf.MoveTowards(base.GetComponent<Rigidbody2D>().velocity.y, 0f, 0.2f));
		}
	}

	// Token: 0x06002E3D RID: 11837 RVA: 0x000227FD File Offset: 0x000209FD
	private IEnumerator pratiLijanaTarget(Transform target)
	{
		while (this.lijana)
		{
			yield return null;
			base.transform.position = Vector3.Lerp(base.transform.position, new Vector3(target.position.x, target.position.y, base.transform.position.z), 0.2f);
		}
		yield break;
	}

	// Token: 0x06002E3E RID: 11838 RVA: 0x001733E4 File Offset: 0x001715E4
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

	// Token: 0x06002E3F RID: 11839 RVA: 0x00173484 File Offset: 0x00171684
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

	// Token: 0x06002E40 RID: 11840 RVA: 0x00022813 File Offset: 0x00020A13
	private IEnumerator Bounce(float time)
	{
		yield return new WaitForSeconds(time);
		this.stop = false;
		yield break;
	}

	// Token: 0x06002E41 RID: 11841 RVA: 0x00022829 File Offset: 0x00020A29
	private void climb()
	{
		base.StartCoroutine(this.MoveUp(0.05f));
	}

	// Token: 0x06002E42 RID: 11842 RVA: 0x0002283D File Offset: 0x00020A3D
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

	// Token: 0x06002E43 RID: 11843 RVA: 0x00022853 File Offset: 0x00020A53
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

	// Token: 0x06002E44 RID: 11844 RVA: 0x00173508 File Offset: 0x00171708
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

	// Token: 0x06002E45 RID: 11845 RVA: 0x00022862 File Offset: 0x00020A62
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

	// Token: 0x06002E46 RID: 11846 RVA: 0x000042DD File Offset: 0x000024DD
	public void CallShake()
	{
	}

	// Token: 0x06002E47 RID: 11847 RVA: 0x00022871 File Offset: 0x00020A71
	private IEnumerator shakeCamera()
	{
		yield return null;
		yield break;
	}

	// Token: 0x06002E48 RID: 11848 RVA: 0x00149A14 File Offset: 0x00147C14
	private string RaycastFunction(Vector3 vector)
	{
		RaycastHit raycastHit;
		if (Physics.Raycast(Camera.main.ScreenPointToRay(vector), ref raycastHit))
		{
			return raycastHit.collider.name;
		}
		return "";
	}

	// Token: 0x06002E49 RID: 11849 RVA: 0x00022879 File Offset: 0x00020A79
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

	// Token: 0x06002E4A RID: 11850 RVA: 0x0002288F File Offset: 0x00020A8F
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

	// Token: 0x06002E4B RID: 11851 RVA: 0x0002289E File Offset: 0x00020A9E
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

	// Token: 0x04002918 RID: 10520
	public float moveForce = 300f;

	// Token: 0x04002919 RID: 10521
	public float maxSpeedX = 8f;

	// Token: 0x0400291A RID: 10522
	public float jumpForce = 700f;

	// Token: 0x0400291B RID: 10523
	public float doubleJumpForce = 100f;

	// Token: 0x0400291C RID: 10524
	public float gravity = 200f;

	// Token: 0x0400291D RID: 10525
	public float maxSpeedY = 8f;

	// Token: 0x0400291E RID: 10526
	public float jumpSpeedX = 12f;

	// Token: 0x0400291F RID: 10527
	private bool jump;

	// Token: 0x04002920 RID: 10528
	private bool doubleJump;

	// Token: 0x04002921 RID: 10529
	[HideInInspector]
	public bool inAir;

	// Token: 0x04002922 RID: 10530
	private bool hasJumped;

	// Token: 0x04002923 RID: 10531
	private bool jumpSafetyCheck;

	// Token: 0x04002924 RID: 10532
	private bool proveriTerenIspredY;

	// Token: 0x04002925 RID: 10533
	private bool downHit;

	// Token: 0x04002926 RID: 10534
	[HideInInspector]
	public bool zgaziEnemija;

	// Token: 0x04002927 RID: 10535
	[HideInInspector]
	public bool killed;

	// Token: 0x04002928 RID: 10536
	[HideInInspector]
	public bool stop;

	// Token: 0x04002929 RID: 10537
	private bool triggerCheckDown;

	// Token: 0x0400292A RID: 10538
	[HideInInspector]
	public bool triggerCheckDownTrigger;

	// Token: 0x0400292B RID: 10539
	[HideInInspector]
	public bool triggerCheckDownBehind;

	// Token: 0x0400292C RID: 10540
	private bool CheckWallHitNear;

	// Token: 0x0400292D RID: 10541
	private bool CheckWallHitNear_low;

	// Token: 0x0400292E RID: 10542
	private bool startSpustanje;

	// Token: 0x0400292F RID: 10543
	private bool startPenjanje;

	// Token: 0x04002930 RID: 10544
	private bool spustanjeRastojanje;

	// Token: 0x04002931 RID: 10545
	private float pocetniY_spustanje;

	// Token: 0x04002932 RID: 10546
	[HideInInspector]
	public float collisionAngle;

	// Token: 0x04002933 RID: 10547
	private float duzinaPritiskaZaSkok;

	// Token: 0x04002934 RID: 10548
	private bool mozeDaSkociOpet;

	// Token: 0x04002935 RID: 10549
	private float startY;

	// Token: 0x04002936 RID: 10550
	private float endX;

	// Token: 0x04002937 RID: 10551
	private float endY;

	// Token: 0x04002938 RID: 10552
	private bool swoosh;

	// Token: 0x04002939 RID: 10553
	private bool grab;

	// Token: 0x0400293A RID: 10554
	private bool snappingToClimb;

	// Token: 0x0400293B RID: 10555
	private Vector3 colliderForClimb;

	// Token: 0x0400293C RID: 10556
	public bool Glide;

	// Token: 0x0400293D RID: 10557
	public bool DupliSkok;

	// Token: 0x0400293E RID: 10558
	public bool KontrolisaniSkok;

	// Token: 0x0400293F RID: 10559
	public bool SlideNaDole;

	// Token: 0x04002940 RID: 10560
	public bool Zaustavljanje;

	// Token: 0x04002941 RID: 10561
	private Ray2D ray;

	// Token: 0x04002942 RID: 10562
	private RaycastHit2D hit;

	// Token: 0x04002943 RID: 10563
	private Transform groundCheck;

	// Token: 0x04002944 RID: 10564
	private Transform majmun;

	// Token: 0x04002945 RID: 10565
	public ParticleSystem trava;

	// Token: 0x04002946 RID: 10566
	public ParticleSystem oblak;

	// Token: 0x04002947 RID: 10567
	public ParticleSystem particleSkok;

	// Token: 0x04002948 RID: 10568
	public ParticleSystem dupliSkokOblaci;

	// Token: 0x04002949 RID: 10569
	public ParticleSystem runParticle;

	// Token: 0x0400294A RID: 10570
	public ParticleSystem klizanje;

	// Token: 0x0400294B RID: 10571
	private Transform whatToClimb;

	// Token: 0x0400294C RID: 10572
	private float currentSpeed;

	// Token: 0x0400294D RID: 10573
	[HideInInspector]
	public GameObject cameraTarget;

	// Token: 0x0400294E RID: 10574
	[HideInInspector]
	public GameObject cameraTarget_down;

	// Token: 0x0400294F RID: 10575
	private float cameraTarget_down_y;

	// Token: 0x04002950 RID: 10576
	private CameraFollow2D_new cameraFollow;

	// Token: 0x04002951 RID: 10577
	public MonkeyController2D_new.State state;

	// Token: 0x04002952 RID: 10578
	[HideInInspector]
	public float startSpeedX;

	// Token: 0x04002953 RID: 10579
	[HideInInspector]
	public float startJumpSpeedX;

	// Token: 0x04002954 RID: 10580
	public bool neTrebaDaProdje;

	// Token: 0x04002955 RID: 10581
	[HideInInspector]
	public Animator animator;

	// Token: 0x04002956 RID: 10582
	private Animator parentAnim;

	// Token: 0x04002957 RID: 10583
	[HideInInspector]
	public string lastPlayedAnim;

	// Token: 0x04002958 RID: 10584
	private bool helpBool;

	// Token: 0x04002959 RID: 10585
	private AnimatorStateInfo currentBaseState;

	// Token: 0x0400295A RID: 10586
	private int run_State = Animator.StringToHash("Base Layer.Running");

	// Token: 0x0400295B RID: 10587
	private int jump_State = Animator.StringToHash("Base Layer.Jump_Start");

	// Token: 0x0400295C RID: 10588
	private int fall_State = Animator.StringToHash("Base Layer.Jump_Falling");

	// Token: 0x0400295D RID: 10589
	private int landing_State = Animator.StringToHash("Base Layer.Landing");

	// Token: 0x0400295E RID: 10590
	private int doublejump_State = Animator.StringToHash("Base Layer.DoubleJump_Start");

	// Token: 0x0400295F RID: 10591
	private int glide_start_State = Animator.StringToHash("Base Layer.Glide_Start");

	// Token: 0x04002960 RID: 10592
	private int glide_loop_State = Animator.StringToHash("Base Layer.Glide_Loop");

	// Token: 0x04002961 RID: 10593
	private int grab_State = Animator.StringToHash("Base Layer.Grab");

	// Token: 0x04002962 RID: 10594
	private int wall_stop_State = Animator.StringToHash("Base Layer.WallStop");

	// Token: 0x04002963 RID: 10595
	private int wall_stop_from_jump_State = Animator.StringToHash("Base Layer.WallStop_From_Jump");

	// Token: 0x04002964 RID: 10596
	private int swoosh_State = Animator.StringToHash("Base Layer.Swoosh_Down");

	// Token: 0x04002965 RID: 10597
	private int spikedeath_State = Animator.StringToHash("Base Layer.Spike_death");

	// Token: 0x04002966 RID: 10598
	private int lijana_State = Animator.StringToHash("Base Layer.Lijana");

	// Token: 0x04002967 RID: 10599
	private Transform lookAtPos;

	// Token: 0x04002968 RID: 10600
	private float lookWeight;

	// Token: 0x04002969 RID: 10601
	private bool disableGlide;

	// Token: 0x0400296A RID: 10602
	private bool helper_disableMoveAfterGrab;

	// Token: 0x0400296B RID: 10603
	private bool usporavanje;

	// Token: 0x0400296C RID: 10604
	private bool sudarioSeSaZidom;

	// Token: 0x0400296D RID: 10605
	[HideInInspector]
	public bool lijana;

	// Token: 0x0400296E RID: 10606
	private Transform grabLianaTransform;

	// Token: 0x0400296F RID: 10607
	[HideInInspector]
	public bool heCanJump = true;

	// Token: 0x04002970 RID: 10608
	private bool saZidaNaZid;

	// Token: 0x04002971 RID: 10609
	private int povrsinaZaClick;

	// Token: 0x04002972 RID: 10610
	private bool jumpControlled;

	// Token: 0x04002973 RID: 10611
	private float tempForce;

	// Token: 0x04002974 RID: 10612
	[HideInInspector]
	public bool activeShield;

	// Token: 0x04002975 RID: 10613
	private float razmrk;

	// Token: 0x04002976 RID: 10614
	private Vector3 pocScale;

	// Token: 0x04002977 RID: 10615
	private Transform senka;

	// Token: 0x04002978 RID: 10616
	public static bool canRespawnThings = true;

	// Token: 0x04002979 RID: 10617
	[SerializeField]
	private LayerMask whatIsGround;

	// Token: 0x0400297A RID: 10618
	private float groundedRadius = 0.2f;

	// Token: 0x0400297B RID: 10619
	private bool grounded;

	// Token: 0x0400297C RID: 10620
	private float startVelY;

	// Token: 0x0400297D RID: 10621
	private float korakce;

	// Token: 0x0400297E RID: 10622
	private bool canGlide;

	// Token: 0x0200071F RID: 1823
	[HideInInspector]
	public enum State
	{
		// Token: 0x04002980 RID: 10624
		running,
		// Token: 0x04002981 RID: 10625
		jumped,
		// Token: 0x04002982 RID: 10626
		wallhit,
		// Token: 0x04002983 RID: 10627
		climbUp,
		// Token: 0x04002984 RID: 10628
		actualClimbing,
		// Token: 0x04002985 RID: 10629
		wasted,
		// Token: 0x04002986 RID: 10630
		idle,
		// Token: 0x04002987 RID: 10631
		completed,
		// Token: 0x04002988 RID: 10632
		lijana,
		// Token: 0x04002989 RID: 10633
		saZidaNaZid,
		// Token: 0x0400298A RID: 10634
		preNegoDaSeOdbije
	}
}
