using System;
using System.Collections;
using UnityEngine;

// Token: 0x020006F9 RID: 1785
public class MonkeyController2D : MonoBehaviour
{
	// Token: 0x1700039A RID: 922
	// (get) Token: 0x06002D17 RID: 11543 RVA: 0x0002211A File Offset: 0x0002031A
	public static MonkeyController2D Instance
	{
		get
		{
			if (MonkeyController2D.instance == null)
			{
				MonkeyController2D.instance = (Object.FindObjectOfType(typeof(MonkeyController2D)) as MonkeyController2D);
			}
			return MonkeyController2D.instance;
		}
	}

	// Token: 0x06002D18 RID: 11544 RVA: 0x00167CDC File Offset: 0x00165EDC
	private void Awake()
	{
		this.majmun = GameObject.Find("PrinceGorilla").transform;
		this.animator = this.majmun.GetComponent<Animator>();
		this.cameraTarget = base.transform.Find("PlayerFocus2D").gameObject;
		this.cameraTarget_down = base.transform.Find("PlayerFocus2D_down").gameObject;
		this.cameraFollow = Camera.main.transform.parent.GetComponent<CameraFollow2D_new>();
		this.lookAtPos = base.transform.Find("LookAtPos");
		Input.multiTouchEnabled = true;
		this.manage = GameObject.Find("_GameManager").GetComponent<Manage>();
		this.guiCamera = GameObject.Find("GUICamera").GetComponent<Camera>();
		MonkeyController2D.instance = this;
	}

	// Token: 0x06002D19 RID: 11545 RVA: 0x00167DAC File Offset: 0x00165FAC
	private void Start()
	{
		if (!StagesParser.imaKosu)
		{
			this.majmun.Find("Kosa").gameObject.SetActive(false);
		}
		if (!StagesParser.imaUsi)
		{
			this.majmun.Find("Usi").gameObject.SetActive(false);
		}
		if (StagesParser.glava != -1)
		{
			this.majmun.Find("ROOT/Hip/Spine/Chest/Neck/Head/" + StagesParser.glava).GetChild(0).gameObject.SetActive(true);
		}
		if (StagesParser.majica != -1)
		{
			this.majmun.Find("custom_Majica").gameObject.SetActive(true);
			Texture texture = Resources.Load("Majice/Bg" + StagesParser.majica) as Texture;
			this.majmun.Find("custom_Majica").GetComponent<Renderer>().material.SetTexture("_MainTex", texture);
			this.majmun.Find("custom_Majica").GetComponent<Renderer>().material.color = StagesParser.bojaMajice;
		}
		if (StagesParser.ledja != -1)
		{
			this.majmun.Find("ROOT/Hip/Spine/" + StagesParser.ledja).GetChild(0).gameObject.SetActive(true);
		}
		this.startSpeedX = this.maxSpeedX;
		this.startJumpSpeedX = this.jumpSpeedX;
		this.state = MonkeyController2D.State.idle;
		Resources.UnloadUnusedAssets();
		this.cameraTarget_down.transform.position = this.cameraTarget.transform.position;
		this.currentBaseState = this.animator.GetCurrentAnimatorStateInfo(0);
		this.animator.speed = 1.5f;
		this.animator.SetLookAtWeight(this.lookWeight);
		this.animator.SetLookAtPosition(this.lookAtPos.position);
		this.tempForce = this.jumpForce;
		this.senka = GameObject.Find("shadowMonkey").transform;
		MonkeyController2D.canRespawnThings = true;
		this.groundCheck = base.transform.Find("GroundCheck");
		this.ceilingCheck = base.transform.Find("CeilingCheck");
		this.trail = base.transform.Find("Trail").GetComponent<TrailRenderer>();
		this.startPosX = Camera.main.transform.position.x;
		this.originalCameraTargetPosition = this.cameraTarget.transform.localPosition.y;
	}

	// Token: 0x06002D1A RID: 11546 RVA: 0x0016802C File Offset: 0x0016622C
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
		if (this.measureDistance)
		{
			this.distance = (float)((int)(Camera.main.transform.position.x - this.startPosX) / 4);
			MissionManager.Instance.DistanceEvent(this.distance);
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
		else if (this.currentBaseState.nameHash == this.fall_State && this.animator.GetBool("Falling"))
		{
			this.animator.SetBool("Falling", false);
		}
		if (this.proveraGround == 0)
		{
			this.grounded = Physics2D.OverlapCircle(this.groundCheck.position, this.groundedRadius, this.whatIsGround);
		}
		else
		{
			this.proveraGround--;
		}
		this.triggerCheckDownTrigger = Physics2D.Linecast(base.transform.position + new Vector3(0.8f, 2.5f, 0f), base.transform.position + new Vector3(0.8f, -4.5f, 0f), 1 << LayerMask.NameToLayer("Platform"));
		this.triggerCheckDownBehind = Physics2D.Linecast(base.transform.position + new Vector3(-0.8f, 2.5f, 0f), base.transform.position + new Vector3(-0.8f, -4.5f, 0f), 1 << LayerMask.NameToLayer("Platform"));
		if (this.state == MonkeyController2D.State.jumped || this.state == MonkeyController2D.State.climbUp)
		{
			if (this.DupliSkok)
			{
				if ((Input.GetMouseButtonDown(0) && Input.mousePosition.x > (float)this.povrsinaZaClick) || Input.GetKeyDown(32))
				{
					if (this.mozeDaSkociOpet && this.hasJumped)
					{
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
					if (this.mushroomJumped)
					{
						base.GetComponent<Rigidbody2D>().isKinematic = false;
						this.mushroomJumped = false;
					}
				}
				else if (Input.GetMouseButtonUp(0) || Input.GetKeyUp(32))
				{
					this.startY = (this.endY = 0f);
					base.GetComponent<Rigidbody2D>().drag = 0f;
					this.animator.SetBool("Glide", false);
					this.disableGlide = false;
					this.canGlide = false;
					if (PlaySounds.Glide_NEW.isPlaying)
					{
						PlaySounds.Stop_Glide();
					}
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
					if (PlaySounds.Glide_NEW.isPlaying)
					{
						PlaySounds.Stop_Glide();
					}
					if (this.trail.time > 0f)
					{
						base.StartCoroutine(this.nestaniTrail(2f));
					}
				}
			}
			if (Input.GetMouseButton(0))
			{
				this.endY = Input.mousePosition.y;
				if (this.SlideNaDole && this.startY - this.endY > 0.125f * Camera.main.orthographicSize * ((float)Screen.height / (2f * Camera.main.orthographicSize)))
				{
					if (!Physics2D.Linecast(this.groundCheck.position, this.groundCheck.position - Vector3.up * 20f, this.whatIsGround))
					{
						this.powerfullImpact = true;
						this.zutiGlowSwooshVisoki.gameObject.SetActive(true);
					}
					this.swoosh = true;
					this.isSliding = true;
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
					if (!PlaySounds.Glide_NEW.isPlaying && PlaySounds.soundOn)
					{
						PlaySounds.Play_Glide();
					}
				}
			}
			if (this.KontrolisaniSkok)
			{
				if (this.korakce < 1.5f && !this.pustenVoiceJump)
				{
					this.pustenVoiceJump = true;
					if (PlaySounds.soundOn)
					{
						PlaySounds.Play_VoiceJump();
					}
				}
				if ((Input.GetMouseButtonUp(0) || Time.time - this.duzinaPritiskaZaSkok > 1f) && this.jumpControlled)
				{
					this.jumpControlled = false;
					this.jumpHolding = false;
					this.tempForce = this.jumpForce;
					this.canGlide = false;
					if (PlaySounds.Glide_NEW.isPlaying)
					{
						PlaySounds.Stop_Glide();
					}
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
		if (this.state == MonkeyController2D.State.wallhit)
		{
			if (this.KontrolisaniSkok)
			{
				if ((Input.GetMouseButtonDown(0) && Input.mousePosition.x > (float)this.povrsinaZaClick) || Input.GetKeyDown(32))
				{
					if (this.RaycastFunction(Input.mousePosition) != "Pause Button" && this.RaycastFunction(Input.mousePosition) != "Play Button_Pause" && this.RaycastFunction(Input.mousePosition) != "Buy Button" && this.RaycastFunction(Input.mousePosition) != "ButtonMain_NoMoreLives" && this.RaycastFunction(Input.mousePosition) != "Restart Button_Pause" && !this.RaycastFunction(Input.mousePosition).Contains("Tutorial") && !this.RaycastFunction(Input.mousePosition).Contains("Power_") && this.RaycastFunction(Input.mousePosition) != "Menu Button_Pause")
					{
						this.duzinaPritiskaZaSkok = Time.time;
						if (!this.inAir)
						{
							this.state = MonkeyController2D.State.climbUp;
							if (PlaySounds.soundOn)
							{
								PlaySounds.Stop_Run();
								PlaySounds.Play_Jump();
							}
							this.jumpControlled = true;
							this.animator.Play(this.jump_State);
							this.animator.SetBool("Landing", false);
							this.animator.SetBool("WallStop", false);
							this.inAir = true;
							this.tempForce = this.jumpForce;
							this.particleSkok.Emit(20);
						}
					}
				}
				else if (Input.GetMouseButtonUp(0) && this.usporavanje && this.Zaustavljanje)
				{
					this.usporavanje = false;
					this.state = MonkeyController2D.State.running;
					this.maxSpeedX = this.startSpeedX;
					this.animator.Play(this.run_State);
					this.animator.SetBool("WallStop", false);
				}
			}
			else if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(32))
			{
				if (this.RaycastFunction(Input.mousePosition) != "Pause Button" && this.RaycastFunction(Input.mousePosition) != "Play Button_Pause" && this.RaycastFunction(Input.mousePosition) != "Buy Button" && this.RaycastFunction(Input.mousePosition) != "ButtonMain_NoMoreLives" && this.RaycastFunction(Input.mousePosition) != "Restart Button_Pause" && !this.RaycastFunction(Input.mousePosition).Contains("Tutorial") && !this.RaycastFunction(Input.mousePosition).Contains("Power_") && this.RaycastFunction(Input.mousePosition) != "Menu Button_Pause" && !this.inAir)
				{
					this.state = MonkeyController2D.State.climbUp;
					this.jumpSpeedX = 5f;
					if (PlaySounds.soundOn)
					{
						PlaySounds.Stop_Run();
						PlaySounds.Play_Jump();
					}
					this.jump = true;
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
				this.state = MonkeyController2D.State.running;
				this.maxSpeedX = this.startSpeedX;
				this.animator.Play(this.run_State);
				this.animator.SetBool("WallStop", false);
			}
		}
		if (this.state == MonkeyController2D.State.running)
		{
			if (this.KontrolisaniSkok)
			{
				if (((Input.GetMouseButtonDown(0) && Input.mousePosition.x > (float)this.povrsinaZaClick) || Input.GetKeyDown(32)) && this.RaycastFunction(Input.mousePosition) != "Pause Button" && this.RaycastFunction(Input.mousePosition) != "Play Button_Pause" && this.RaycastFunction(Input.mousePosition) != "Buy Button" && this.RaycastFunction(Input.mousePosition) != "ButtonMain_NoMoreLives" && this.RaycastFunction(Input.mousePosition) != "Restart Button_Pause" && !this.RaycastFunction(Input.mousePosition).Contains("Tutorial") && !this.RaycastFunction(Input.mousePosition).Contains("Power_") && this.RaycastFunction(Input.mousePosition) != "Menu Button_Pause")
				{
					this.jumpHolding = true;
					this.grounded = false;
					this.proveraGround = 16;
					this.korakce = 3f;
					base.GetComponent<Rigidbody2D>().velocity = new Vector2(this.maxSpeedX, this.maxSpeedY);
					this.neTrebaDaProdje = false;
					this.duzinaPritiskaZaSkok = Time.time;
					this.state = MonkeyController2D.State.jumped;
					if (PlaySounds.soundOn)
					{
						PlaySounds.Stop_Run();
						PlaySounds.Play_Jump();
						base.Invoke("PustiVoiceJump", 0.35f);
					}
					this.jumpControlled = true;
					this.animator.Play(this.jump_State);
					this.animator.SetBool("Landing", false);
					this.inAir = true;
					this.particleSkok.Emit(20);
				}
				if ((Input.GetMouseButtonUp(0) || Time.time - this.duzinaPritiskaZaSkok > 2f) && this.jumpControlled)
				{
					this.jumpControlled = false;
					this.tempForce = this.jumpForce;
				}
			}
			if (this.Zaustavljanje && this.povrsinaZaClick != 0 && Input.GetMouseButton(0))
			{
				this.usporavanje = true;
				this.maxSpeedX = 0f;
				this.state = MonkeyController2D.State.wallhit;
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
		if (this.state == MonkeyController2D.State.lijana)
		{
			this.povrsinaZaClick = 0;
			if (Input.GetMouseButtonDown(0) && Input.mousePosition.x > (float)this.povrsinaZaClick && this.heCanJump && this.RaycastFunction(Input.mousePosition) != "Pause Button" && this.RaycastFunction(Input.mousePosition) != "Play Button_Pause" && this.RaycastFunction(Input.mousePosition) != "Buy Button" && this.RaycastFunction(Input.mousePosition) != "ButtonMain_NoMoreLives" && this.RaycastFunction(Input.mousePosition) != "Restart Button_Pause" && !this.RaycastFunction(Input.mousePosition).Contains("Tutorial") && !this.RaycastFunction(Input.mousePosition).Contains("Power_") && this.RaycastFunction(Input.mousePosition) != "Menu Button_Pause")
			{
				if (Input.mousePosition.x < (float)(Screen.width / 2))
				{
					this.startY = Input.mousePosition.y;
				}
				else
				{
					this.animator.Play(this.jump_State);
					this.OtkaciMajmuna();
				}
			}
			if (this.SlideNaDole)
			{
				if (Input.GetMouseButton(0))
				{
					this.endY = Input.mousePosition.y;
					if (this.SlideNaDole && this.startY - this.endY > 0.125f * Camera.main.orthographicSize * ((float)Screen.height / (2f * Camera.main.orthographicSize)))
					{
						this.SpustiMajmunaSaLijaneBrzo();
						this.swoosh = true;
						this.isSliding = true;
						this.animator.Play(this.swoosh_State);
					}
				}
				if (Input.GetMouseButtonUp(0))
				{
					this.startY = (this.endY = 0f);
				}
			}
		}
		if (this.state == MonkeyController2D.State.saZidaNaZid)
		{
			if (Input.GetMouseButtonDown(0) && this.heCanJump && this.RaycastFunction(Input.mousePosition) != "Pause Button" && this.RaycastFunction(Input.mousePosition) != "Play Button_Pause" && this.RaycastFunction(Input.mousePosition) != "Buy Button" && this.RaycastFunction(Input.mousePosition) != "ButtonMain_NoMoreLives" && this.RaycastFunction(Input.mousePosition) != "Restart Button_Pause" && !this.RaycastFunction(Input.mousePosition).Contains("Tutorial") && !this.RaycastFunction(Input.mousePosition).Contains("Power_") && this.RaycastFunction(Input.mousePosition) != "Menu Button_Pause" && !this.inAir)
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
		else if (this.state == MonkeyController2D.State.preNegoDaSeOdbije && this.saZidaNaZid && (Input.GetMouseButtonDown(0) || Input.GetKeyDown(32)))
		{
			this.jump = true;
			base.GetComponent<Rigidbody2D>().drag = 0f;
			this.state = MonkeyController2D.State.saZidaNaZid;
			this.animator.Play(this.jump_State);
			this.animator.SetBool("Landing", false);
			this.animator.SetBool("WallStop", false);
			this.particleSkok.Emit(20);
		}
	}

	// Token: 0x06002D1B RID: 11547 RVA: 0x0016940C File Offset: 0x0016760C
	private void FixedUpdate()
	{
		this.currentBaseState = this.animator.GetCurrentAnimatorStateInfo(0);
		if (this.state == MonkeyController2D.State.saZidaNaZid)
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
		else if (this.state == MonkeyController2D.State.preNegoDaSeOdbije)
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
		else if (this.state == MonkeyController2D.State.wasted && this.measureDistance)
		{
			this.measureDistance = false;
		}
		if (this.state == MonkeyController2D.State.running)
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
				return;
			}
		}
		else if (this.state == MonkeyController2D.State.completed)
		{
			if (this.dancing)
			{
				this.proveraGround = 0;
				if (this.grounded)
				{
					base.GetComponent<Rigidbody2D>().velocity = new Vector2(0f, 0f);
					return;
				}
				base.GetComponent<Rigidbody2D>().velocity = new Vector2(0f, -30f);
				return;
			}
			else
			{
				if (base.GetComponent<Rigidbody2D>().velocity.x < this.maxSpeedX && !this.stop)
				{
					base.GetComponent<Rigidbody2D>().AddForce(Vector2.right * this.moveForce);
				}
				if (Mathf.Abs(base.GetComponent<Rigidbody2D>().velocity.x) > this.maxSpeedX && !this.stop)
				{
					base.GetComponent<Rigidbody2D>().velocity = new Vector2(this.maxSpeedX, base.GetComponent<Rigidbody2D>().velocity.y);
					return;
				}
			}
		}
		else if (this.state == MonkeyController2D.State.jumped)
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
			if (this.powerfullImpact)
			{
				if (!PlaySounds.Glide_NEW.isPlaying && PlaySounds.soundOn)
				{
					PlaySounds.Play_Glide();
					return;
				}
			}
			else if (this.jumpControlled)
			{
				this.hasJumped = true;
				this.mozeDaSkociOpet = true;
				return;
			}
		}
		else if (this.state == MonkeyController2D.State.wallhit)
		{
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
		else if (this.state == MonkeyController2D.State.climbUp)
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
		}
	}

	// Token: 0x06002D1C RID: 11548 RVA: 0x00169A8C File Offset: 0x00167C8C
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

	// Token: 0x06002D1D RID: 11549 RVA: 0x00169B54 File Offset: 0x00167D54
	private void OnCollisionEnter2D(Collision2D col)
	{
		if (this.state == MonkeyController2D.State.completed && this.inAir)
		{
			if (this.powerfullImpact)
			{
				base.transform.Find("Impact").gameObject.SetActive(true);
				base.Invoke("DisableImpact", 0.25f);
				this.powerfullImpact = false;
				if (PlaySounds.Glide_NEW.isPlaying)
				{
					PlaySounds.Stop_Glide();
				}
				this.zutiGlowSwooshVisoki.gameObject.SetActive(false);
				Camera.main.GetComponent<Animator>().Play("CameraShakeTrasGround");
				this.izrazitiPad.Play();
				this.animator.Play("Landing");
			}
			else
			{
				this.animator.SetBool("Landing", true);
				this.grounded = true;
				this.oblak.Play();
			}
			this.inAir = false;
			if (this.isSliding)
			{
				this.isSliding = false;
			}
			this.Finish();
			return;
		}
		if (this.state != MonkeyController2D.State.completed || this.state != MonkeyController2D.State.wasted)
		{
			if (col.gameObject.tag == "ZidZaOdbijanje")
			{
				if (this.state == MonkeyController2D.State.running && this.CheckWallHitNear)
				{
					if (this.klizanje.isPlaying)
					{
						this.klizanje.Stop();
					}
					this.animator.SetBool("WallStop", true);
					this.animator.Play(this.wall_stop_State);
					this.state = MonkeyController2D.State.preNegoDaSeOdbije;
					if (this.trava.isPlaying)
					{
						this.trava.Stop();
					}
					if (this.runParticle.isPlaying)
					{
						this.runParticle.Stop();
					}
				}
				else if (this.state != MonkeyController2D.State.preNegoDaSeOdbije)
				{
					this.inAir = false;
					this.heCanJump = true;
					base.GetComponent<Rigidbody2D>().drag = 5f;
					this.klizanje.Play();
					this.animator.Play("Klizanje");
					this.state = MonkeyController2D.State.saZidaNaZid;
				}
				this.wallHitGlide = true;
				return;
			}
			if (col.gameObject.tag == "Footer")
			{
				this.startPenjanje = false;
				this.startSpustanje = false;
				if (this.cameraTarget_down.transform.parent == null)
				{
					this.cameraTarget_down.transform.position = this.cameraTarget.transform.position;
					this.cameraTarget_down.transform.parent = base.transform;
				}
				this.cameraTarget_down.transform.position = this.cameraTarget.transform.position;
				if (this.state == MonkeyController2D.State.saZidaNaZid)
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
						this.state = MonkeyController2D.State.preNegoDaSeOdbije;
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
						this.state = MonkeyController2D.State.running;
						this.canGlide = false;
						if (PlaySounds.Glide_NEW.isPlaying)
						{
							PlaySounds.Stop_Glide();
						}
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
				if (this.state == MonkeyController2D.State.jumped)
				{
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
							base.transform.Find("Impact").gameObject.SetActive(true);
							base.Invoke("DisableImpact", 0.25f);
							this.powerfullImpact = false;
							if (PlaySounds.Glide_NEW.isPlaying)
							{
								PlaySounds.Stop_Glide();
							}
							this.zutiGlowSwooshVisoki.gameObject.SetActive(false);
							Camera.main.GetComponent<Animator>().Play("CameraShakeTrasGround");
							this.izrazitiPad.Play();
							if (PlaySounds.soundOn)
							{
								PlaySounds.Play_Landing_Strong();
							}
						}
						else if (PlaySounds.soundOn)
						{
							PlaySounds.Play_Landing();
						}
						if (this.lijana)
						{
							this.lijana = false;
						}
						this.jumpSpeedX = this.startJumpSpeedX;
						this.mozeDaSkociOpet = false;
						this.animator.SetBool("Jump", false);
						this.animator.SetBool("DoubleJump", false);
						this.animator.SetBool("Glide", false);
						this.disableGlide = false;
						this.animator.SetBool("Landing", true);
						base.GetComponent<Rigidbody2D>().drag = 0f;
						this.state = MonkeyController2D.State.running;
						this.grounded = true;
						this.canGlide = false;
						if (PlaySounds.Glide_NEW.isPlaying)
						{
							PlaySounds.Stop_Glide();
						}
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
						if (this.isSliding)
						{
							this.isSliding = false;
						}
					}
				}
				if (this.state == MonkeyController2D.State.wasted && this.utepanULetu)
				{
					if (this.magnet)
					{
						this.manage.ApplyPowerUp(-1);
					}
					if (this.doublecoins)
					{
						this.manage.ApplyPowerUp(-2);
					}
					base.StartCoroutine(this.AfterFallDown());
					return;
				}
			}
			else if (col.gameObject.tag == "Enemy")
			{
				if (this.activeShield)
				{
					if (PlaySounds.soundOn)
					{
						PlaySounds.Play_LooseShield();
					}
					this.enemyCollider = col.transform.GetComponent<Collider2D>();
					this.enemyCollider.enabled = false;
					base.Invoke("EnableColliderBackOnEnemy", 1f);
					this.activeShield = false;
					base.transform.Find("Particles/ShieldDestroyParticle").GetComponent<ParticleSystem>().Play();
					base.transform.Find("Particles/ShieldDestroyParticle").GetChild(0).GetComponent<ParticleSystem>().Play();
					this.manage.SendMessage("ApplyPowerUp", -3);
					MonkeyController2D.State state = this.state;
					return;
				}
				if (!this.killed && !this.invincible)
				{
					if (col.gameObject.name.Contains("Biljka"))
					{
						if (PlaySounds.soundOn)
						{
							PlaySounds.Play_BiljkaUgriz();
						}
						col.transform.Find("Biljka_mesozder").GetComponent<Animator>().Play("Attack");
					}
					else if (PlaySounds.soundOn)
					{
						PlaySounds.Play_Siljci();
					}
					base.transform.Find("Particles/OblakKill").GetComponent<ParticleSystem>().Play();
					if (this.state == MonkeyController2D.State.running)
					{
						base.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
						this.killed = true;
						this.oblak.Play();
						this.majmunUtepan();
						return;
					}
					this.majmunUtepanULetu();
					return;
				}
			}
			else if (col.gameObject.tag.Equals("WallHit"))
			{
				this.wallHitGlide = true;
			}
		}
	}

	// Token: 0x06002D1E RID: 11550 RVA: 0x00022147 File Offset: 0x00020347
	private void EnableColliderBackOnEnemy()
	{
		this.enemyCollider.enabled = true;
		this.enemyCollider = null;
	}

	// Token: 0x06002D1F RID: 11551 RVA: 0x0002215C File Offset: 0x0002035C
	public void majmunUtepanULetu()
	{
		base.StartCoroutine(this.FallDownAfterSpikes());
	}

	// Token: 0x06002D20 RID: 11552 RVA: 0x0002216B File Offset: 0x0002036B
	private IEnumerator FallDownAfterSpikes()
	{
		if (base.GetComponent<Rigidbody2D>().drag != 0f)
		{
			base.GetComponent<Rigidbody2D>().drag = 0f;
			this.canGlide = false;
			if (PlaySounds.Glide_NEW.isPlaying)
			{
				PlaySounds.Stop_Glide();
			}
			if (this.trail.time != 0f)
			{
				base.StartCoroutine(this.nestaniTrail(2f));
			}
		}
		if (this.powerfullImpact)
		{
			this.powerfullImpact = false;
			if (PlaySounds.Glide_NEW.isPlaying)
			{
				PlaySounds.Stop_Glide();
			}
			this.zutiGlowSwooshVisoki.gameObject.SetActive(false);
		}
		this.majmun.GetComponent<Animator>().speed = 1.5f;
		base.gameObject.layer = LayerMask.NameToLayer("MonkeyShadow");
		this.utepanULetu = true;
		MonkeyController2D.canRespawnThings = false;
		this.killed = true;
		this.oblak.Play();
		this.animator.Play(this.spikedeath_State);
		this.state = MonkeyController2D.State.wasted;
		this.manage.transform.Find("Gameplay Scena Interface/_TopRight/Pause Button").GetComponent<Collider>().enabled = false;
		if (this.trava.isPlaying)
		{
			this.trava.Stop();
		}
		if (this.runParticle.isPlaying)
		{
			this.runParticle.Stop();
		}
		base.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
		base.GetComponent<Rigidbody2D>().velocity = new Vector2(-8f, 30f);
		yield return new WaitForSeconds(1f);
		yield break;
	}

	// Token: 0x06002D21 RID: 11553 RVA: 0x0002217A File Offset: 0x0002037A
	private IEnumerator AfterFallDown()
	{
		this.utepanULetu = false;
		this.animator.SetTrigger("ToLand");
		base.GetComponent<Rigidbody2D>().velocity = new Vector2(-4f, 22f);
		yield return new WaitForSeconds(0.65f);
		while (!this.grounded)
		{
			yield return null;
		}
		base.GetComponent<Rigidbody2D>().isKinematic = true;
		yield return new WaitForSeconds(0.35f);
		base.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
		if (this.manage.keepPlayingCount >= 1)
		{
			this.manage.SendMessage("showFailedScreen");
		}
		else
		{
			this.manage.SendMessage("ShowKeepPlayingScreen");
		}
		this.cameraFollow.stopFollow = true;
		if (!this.invincible)
		{
			base.transform.Find("Particles/HolderKillStars").gameObject.SetActive(true);
		}
		yield break;
	}

	// Token: 0x06002D22 RID: 11554 RVA: 0x0016A3AC File Offset: 0x001685AC
	private void OnCollisionExit2D(Collision2D col)
	{
		if (col.gameObject.tag == "Footer")
		{
			if (this.state == MonkeyController2D.State.running)
			{
				this.neTrebaDaProdje = false;
				if (!Physics2D.Linecast(base.transform.position + new Vector3(4.4f, 1.2f, 0f), base.transform.position + new Vector3(4.4f, -3.2f, 0f), 1 << LayerMask.NameToLayer("Ground") | 1 << LayerMask.NameToLayer("Platform")) && !Physics2D.Linecast(base.transform.position + new Vector3(0.2f, 0.1f, 0f), base.transform.position + new Vector3(0.2f, -0.65f, 0f), 1 << LayerMask.NameToLayer("Ground") | 1 << LayerMask.NameToLayer("Platform")))
				{
					this.state = MonkeyController2D.State.jumped;
					this.animator.SetBool("Landing", false);
					this.animator.Play(this.fall_State);
					if (this.runParticle.isPlaying)
					{
						this.runParticle.Stop();
					}
					if (!Physics2D.Linecast(base.transform.position + new Vector3(2.3f, 1.25f, 0f), base.transform.position + new Vector3(2.3f, -Camera.main.orthographicSize, 0f), 1 << LayerMask.NameToLayer("Ground") | 1 << LayerMask.NameToLayer("Platform")))
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
		else
		{
			if (col.gameObject.tag == "ZidZaOdbijanje")
			{
				if (this.klizanje.isPlaying)
				{
					this.klizanje.Stop();
				}
				base.GetComponent<Rigidbody2D>().drag = 0f;
				if (this.trava.isPlaying)
				{
					this.trava.Stop();
				}
				if (this.state != MonkeyController2D.State.wasted)
				{
					this.animator.Play(this.fall_State);
				}
				this.animator.SetBool("Landing", false);
				this.wallHitGlide = false;
				return;
			}
			if (col.gameObject.tag.Equals("WallHit"))
			{
				this.wallHitGlide = false;
			}
		}
	}

	// Token: 0x06002D23 RID: 11555 RVA: 0x00022189 File Offset: 0x00020389
	private void NotifyManagerForFinish()
	{
		this.manage.SendMessage("ShowWinScreen");
	}

	// Token: 0x06002D24 RID: 11556 RVA: 0x0002219B File Offset: 0x0002039B
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

	// Token: 0x06002D25 RID: 11557 RVA: 0x0016A6B8 File Offset: 0x001688B8
	private void OnTriggerEnter2D(Collider2D col)
	{
		if (this.state != MonkeyController2D.State.completed || this.state != MonkeyController2D.State.wasted)
		{
			if (col.name == "Magnet_collect")
			{
				if (PlaySounds.soundOn)
				{
					PlaySounds.Play_CollectPowerUp();
				}
				LevelFactory.instance.magnetCollected = true;
				this.magnet = true;
				this.manage.SendMessage("ApplyPowerUp", 1);
				col.gameObject.SetActive(false);
				this.collectItem.Play();
				base.StartCoroutine(this.reappearItem(col.gameObject));
			}
			else if (col.name == "BananaCoinX2_collect")
			{
				if (PlaySounds.soundOn)
				{
					PlaySounds.Play_CollectPowerUp();
				}
				this.doublecoins = true;
				this.manage.SendMessage("ApplyPowerUp", 2);
				col.gameObject.SetActive(false);
				this.collectItem.Play();
				base.StartCoroutine(this.reappearItem(col.gameObject));
			}
			else if (col.name == "Shield_collect")
			{
				if (PlaySounds.soundOn)
				{
					PlaySounds.Play_CollectPowerUp();
				}
				LevelFactory.instance.shieldCollected = true;
				this.manage.SendMessage("ApplyPowerUp", 3);
				col.gameObject.SetActive(false);
				this.activeShield = true;
				this.collectItem.Play();
				base.StartCoroutine(this.reappearItem(col.gameObject));
			}
			else if (col.name == "Banana_collect")
			{
				if (PlaySounds.soundOn)
				{
					PlaySounds.Play_CollectBanana();
				}
				col.gameObject.SetActive(false);
				Manage.points += 200;
				Manage.pointsText.text = Manage.points.ToString();
				Manage.pointsEffects.RefreshTextOutline(false, true, true);
				this.collectItem.Play();
				base.StartCoroutine(this.reappearItem(col.gameObject));
				StagesParser.currentBananas++;
				PlayerPrefs.SetInt("TotalBananas", StagesParser.currentBananas);
				PlayerPrefs.Save();
			}
			else if (col.name == "Srce_collect")
			{
				col.gameObject.SetActive(false);
				GameObject.Find("LifeManager").SendMessage("AddLife");
				base.StartCoroutine(this.reappearItem(col.gameObject));
			}
			else if (col.name.Contains("Diamond_collect"))
			{
				if (PlaySounds.soundOn)
				{
					PlaySounds.Play_CollectDiamond();
				}
				col.gameObject.SetActive(false);
				Manage.points += 50;
				Manage.pointsText.text = Manage.points.ToString();
				Manage.pointsEffects.RefreshTextOutline(false, true, true);
				this.collectItem.Play();
				base.StartCoroutine(this.reappearItem(col.gameObject));
				if (col.name.Contains("Red"))
				{
					Manage.redDiamonds++;
					MissionManager.Instance.RedDiamondEvent(Manage.redDiamonds);
				}
				else if (col.name.Contains("Blue"))
				{
					Manage.blueDiamonds++;
					MissionManager.Instance.BlueDiamondEvent(Manage.blueDiamonds);
				}
				else if (col.name.Contains("Green"))
				{
					Manage.greenDiamonds++;
					MissionManager.Instance.GreenDiamondEvent(Manage.greenDiamonds);
				}
				MissionManager.Instance.DiamondEvent(Manage.redDiamonds + Manage.greenDiamonds + Manage.blueDiamonds);
			}
			else if (col.name == "BananaFog")
			{
				col.transform.GetChild(0).GetComponent<Renderer>().enabled = false;
				Camera.main.transform.Find("FogOfWar").GetComponent<Renderer>().enabled = true;
				base.StartCoroutine(this.pojaviMaglu(col));
			}
			else if (col.name.Contains("CoinsBagBig"))
			{
				col.transform.Find("+3CoinsHolder/+3Coins").GetComponent<TextMesh>().text = (col.transform.transform.Find("+3CoinsHolder/+3Coins/+3CoinsShadow").GetComponent<TextMesh>().text = "+500");
				col.transform.Find("+3CoinsHolder").GetComponent<Animator>().Play("FadeOutCoins");
				col.transform.Find("AnimationHolder").GetComponent<Animation>().Play("CoinsBagCollect");
				Manage.coinsCollected += 500;
				Manage.Instance.coinsCollectedText.text = Manage.coinsCollected.ToString();
				Manage.Instance.coinsCollectedText.GetComponent<TextMeshEffects>().RefreshTextOutline(false, true, true);
				this.collectItem.Play();
			}
			else if (col.name.Contains("CoinsBagMedium"))
			{
				col.transform.Find("+3CoinsHolder/+3Coins").GetComponent<TextMesh>().text = (col.transform.transform.Find("+3CoinsHolder/+3Coins/+3CoinsShadow").GetComponent<TextMesh>().text = "+250");
				col.transform.Find("+3CoinsHolder").GetComponent<Animator>().Play("FadeOutCoins");
				col.transform.Find("AnimationHolder").GetComponent<Animation>().Play("CoinsBagCollect");
				Manage.coinsCollected += 250;
				Manage.Instance.coinsCollectedText.text = Manage.coinsCollected.ToString();
				Manage.Instance.coinsCollectedText.GetComponent<TextMeshEffects>().RefreshTextOutline(false, true, true);
				this.collectItem.Play();
			}
			else if (col.name.Contains("CoinsBagSmall"))
			{
				col.transform.Find("+3CoinsHolder/+3Coins").GetComponent<TextMesh>().text = (col.transform.transform.Find("+3CoinsHolder/+3Coins/+3CoinsShadow").GetComponent<TextMesh>().text = "+100");
				col.transform.Find("+3CoinsHolder").GetComponent<Animator>().Play("FadeOutCoins");
				col.transform.Find("AnimationHolder").GetComponent<Animation>().Play("CoinsBagCollect");
				Manage.coinsCollected += 100;
				Manage.Instance.coinsCollectedText.text = Manage.coinsCollected.ToString();
				Manage.Instance.coinsCollectedText.GetComponent<TextMeshEffects>().RefreshTextOutline(false, true, true);
				this.collectItem.Play();
			}
			if (col.tag == "Finish")
			{
				col.GetComponent<Collider2D>().enabled = false;
				this.Finish();
				return;
			}
			if (col.tag == "Footer" && col.gameObject.layer == LayerMask.NameToLayer("Platform"))
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
				if (this.activeShield)
				{
					if (PlaySounds.soundOn)
					{
						PlaySounds.Play_LooseShield();
					}
					this.enemyCollider = col.transform.GetComponent<Collider2D>();
					this.enemyCollider.enabled = false;
					base.Invoke("EnableColliderBackOnEnemy", 1f);
					this.activeShield = false;
					base.transform.Find("Particles/ShieldDestroyParticle").GetComponent<ParticleSystem>().Play();
					base.transform.Find("Particles/ShieldDestroyParticle").GetChild(0).GetComponent<ParticleSystem>().Play();
					this.manage.SendMessage("ApplyPowerUp", -3);
					if (col.name.Equals("Koplje"))
					{
						col.GetComponent<DestroySpearGorilla>().DestroyGorilla();
						return;
					}
				}
				else if (!this.killed && !this.invincible)
				{
					if (col.gameObject.name.Contains("Biljka"))
					{
						if (PlaySounds.soundOn)
						{
							PlaySounds.Play_BiljkaUgriz();
						}
						col.transform.Find("Biljka_mesozder").GetComponent<Animator>().Play("Attack");
					}
					else if (PlaySounds.soundOn)
					{
						PlaySounds.Play_Siljci();
					}
					base.transform.Find("Particles/OblakKill").GetComponent<ParticleSystem>().Play();
					if (this.state == MonkeyController2D.State.running)
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
						this.state = MonkeyController2D.State.jumped;
						return;
					}
					this.saZidaNaZid = true;
					return;
				}
				else if (col.tag == "Lijana")
				{
					if (base.GetComponent<Rigidbody2D>().drag != 0f)
					{
						base.GetComponent<Rigidbody2D>().drag = 0f;
						this.canGlide = false;
						if (PlaySounds.Glide_NEW.isPlaying)
						{
							PlaySounds.Stop_Glide();
						}
						if (this.trail.time != 0f)
						{
							base.StartCoroutine(this.nestaniTrail(2f));
						}
					}
					if (this.powerfullImpact)
					{
						this.powerfullImpact = false;
						if (PlaySounds.Glide_NEW.isPlaying)
						{
							PlaySounds.Stop_Glide();
						}
						this.zutiGlowSwooshVisoki.gameObject.SetActive(false);
					}
					this.animator.SetBool("LianaLetGo", false);
					this.grabLianaTransform = col.GetComponent<LianaAnimationEvent>().lijanaTarget;
					if (!this.lijana)
					{
						this.lijana = true;
						this.animator.Play(this.lijana_State);
					}
					else
					{
						this.animator.Play("Lijana_mirror");
						this.lijana = false;
					}
					this.state = MonkeyController2D.State.lijana;
					col.enabled = false;
					base.GetComponent<Rigidbody2D>().isKinematic = true;
					this.maxSpeedX = 0f;
					this.jumpSpeedX = 0f;
					col.transform.GetChild(0).GetComponent<Animator>().Play("Glide_liana");
					base.Invoke("OtkaciMajmuna", 0.6f);
					base.StartCoroutine("pratiLijanaTarget", this.grabLianaTransform);
				}
			}
		}
	}

	// Token: 0x06002D26 RID: 11558 RVA: 0x0016B16C File Offset: 0x0016936C
	public void Finish()
	{
		MonkeyController2D.canRespawnThings = false;
		this.state = MonkeyController2D.State.completed;
		if (!this.inAir)
		{
			if (this.trail.time != 0f)
			{
				base.StartCoroutine(this.nestaniTrail(2f));
			}
			this.cameraFollow.cameraFollowX = false;
			base.StartCoroutine(this.ReduceMaxSpeedGradually());
			this.manage.transform.Find("Gameplay Scena Interface/_TopRight/Pause Button").GetComponent<Collider>().enabled = false;
			base.gameObject.layer = LayerMask.NameToLayer("MonkeyShadow");
			base.Invoke("TurnOnKinematic", 8f);
			return;
		}
		if (this.canGlide)
		{
			base.GetComponent<Rigidbody2D>().drag = 0f;
			this.animator.SetBool("Glide", false);
			this.disableGlide = false;
			this.canGlide = false;
			if (PlaySounds.Glide_NEW.isPlaying)
			{
				PlaySounds.Stop_Glide();
			}
		}
	}

	// Token: 0x06002D27 RID: 11559 RVA: 0x000221BF File Offset: 0x000203BF
	private IEnumerator ReduceMaxSpeedGradually()
	{
		while (this.maxSpeedX > 0.1f)
		{
			yield return null;
			this.maxSpeedX = Mathf.Lerp(this.maxSpeedX, 0f, 0.2f);
		}
		this.maxSpeedX = 0f;
		this.dancing = true;
		base.GetComponent<Collider2D>().sharedMaterial = this.finishDontMove;
		if (this.currentBaseState.nameHash == this.run_State)
		{
			this.runParticle.Stop();
			this.trava.Stop();
			this.animator.SetTrigger("Finished");
		}
		else
		{
			this.animator.Play("Dancing");
		}
		base.Invoke("RestoreMaxSpeed", 2.15f);
		yield break;
	}

	// Token: 0x06002D28 RID: 11560 RVA: 0x0016B25C File Offset: 0x0016945C
	private void RestoreMaxSpeed()
	{
		this.dancing = false;
		base.GetComponent<Rigidbody2D>().mass = 1.25f;
		this.animator.SetTrigger("DancingDone");
		this.maxSpeedX = 16f;
		base.Invoke("NotifyManagerForFinish", 1.25f);
	}

	// Token: 0x06002D29 RID: 11561 RVA: 0x000221CE File Offset: 0x000203CE
	private void TurnOnKinematic()
	{
		base.GetComponent<Rigidbody2D>().isKinematic = true;
	}

	// Token: 0x06002D2A RID: 11562 RVA: 0x000221DC File Offset: 0x000203DC
	private IEnumerator pratiLijanaTarget(Transform target)
	{
		for (;;)
		{
			yield return null;
			base.transform.position = Vector3.Lerp(base.transform.position, new Vector3(target.position.x, target.position.y, base.transform.position.z), 0.25f);
		}
		yield break;
	}

	// Token: 0x06002D2B RID: 11563 RVA: 0x0016B2AC File Offset: 0x001694AC
	public void OtkaciMajmuna()
	{
		if (this.state == MonkeyController2D.State.lijana)
		{
			base.StopCoroutine("pratiLijanaTarget");
			this.state = MonkeyController2D.State.jumped;
			this.cameraFollow.cameraFollowX = true;
			this.maxSpeedX = this.startSpeedX;
			this.jumpSpeedX = this.startJumpSpeedX;
			base.transform.parent = null;
			base.transform.rotation = Quaternion.identity;
			base.GetComponent<Rigidbody2D>().isKinematic = false;
			base.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
			base.GetComponent<Rigidbody2D>().AddForce(new Vector2(1000f, 2500f));
			this.animator.SetBool("LianaLetGo", true);
		}
	}

	// Token: 0x06002D2C RID: 11564 RVA: 0x0016B360 File Offset: 0x00169560
	private void SpustiMajmunaSaLijaneBrzo()
	{
		base.StopCoroutine("pratiLijanaTarget");
		this.state = MonkeyController2D.State.jumped;
		this.cameraFollow.cameraFollowX = true;
		this.maxSpeedX = this.startSpeedX;
		this.jumpSpeedX = this.startJumpSpeedX;
		base.transform.parent = null;
		base.transform.rotation = Quaternion.identity;
		base.GetComponent<Rigidbody2D>().isKinematic = false;
		this.animator.Play(this.jump_State);
	}

	// Token: 0x06002D2D RID: 11565 RVA: 0x000221F2 File Offset: 0x000203F2
	private IEnumerator Bounce(float time)
	{
		yield return new WaitForSeconds(time);
		this.stop = false;
		yield break;
	}

	// Token: 0x06002D2E RID: 11566 RVA: 0x0016B3DC File Offset: 0x001695DC
	public void majmunUtepan()
	{
		if (this.magnet)
		{
			this.manage.ApplyPowerUp(-1);
		}
		if (this.doublecoins)
		{
			this.manage.ApplyPowerUp(-2);
		}
		MonkeyController2D.canRespawnThings = false;
		this.majmun.GetComponent<Animator>().speed = 1.5f;
		base.gameObject.layer = LayerMask.NameToLayer("MonkeyShadow");
		this.state = MonkeyController2D.State.wasted;
		this.manage.transform.Find("Gameplay Scena Interface/_TopRight/Pause Button").GetComponent<Collider>().enabled = false;
		this.animator.Play("DeathStart");
		base.StartCoroutine(this.slowDown());
		base.StartCoroutine(this.checkGrounded());
		if (this.trava.isPlaying)
		{
			this.trava.Stop();
		}
		if (this.runParticle.isPlaying)
		{
			this.runParticle.Stop();
		}
		this.maxSpeedX = 0f;
	}

	// Token: 0x06002D2F RID: 11567 RVA: 0x00022208 File Offset: 0x00020408
	private IEnumerator checkGrounded()
	{
		yield return new WaitForSeconds(0.5f);
		while (!this.grounded)
		{
			yield return null;
		}
		this.animator.SetTrigger("Grounded");
		if (this.manage.keepPlayingCount >= 1)
		{
			this.manage.SendMessage("showFailedScreen");
		}
		else
		{
			this.manage.SendMessage("ShowKeepPlayingScreen");
		}
		this.cameraFollow.stopFollow = true;
		base.GetComponent<Rigidbody2D>().isKinematic = true;
		yield return new WaitForSeconds(1.75f);
		if (!this.invincible)
		{
			base.transform.Find("Particles/HolderKillStars").gameObject.SetActive(true);
		}
		yield break;
	}

	// Token: 0x06002D30 RID: 11568 RVA: 0x00022217 File Offset: 0x00020417
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
		yield break;
	}

	// Token: 0x06002D31 RID: 11569 RVA: 0x0016B4D0 File Offset: 0x001696D0
	private string RaycastFunction(Vector3 vector)
	{
		RaycastHit raycastHit;
		if (Physics.Raycast(this.guiCamera.ScreenPointToRay(vector), ref raycastHit))
		{
			return raycastHit.collider.name;
		}
		if (Physics.Raycast(Camera.main.ScreenPointToRay(vector), ref raycastHit))
		{
			return raycastHit.collider.name;
		}
		return "";
	}

	// Token: 0x06002D32 RID: 11570 RVA: 0x00022226 File Offset: 0x00020426
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

	// Token: 0x06002D33 RID: 11571 RVA: 0x0002223C File Offset: 0x0002043C
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

	// Token: 0x06002D34 RID: 11572 RVA: 0x0002224B File Offset: 0x0002044B
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

	// Token: 0x06002D35 RID: 11573 RVA: 0x0002225A File Offset: 0x0002045A
	private IEnumerator reappearItem(GameObject obj)
	{
		if (MonkeyController2D.canRespawnThings)
		{
			yield return new WaitForSeconds(5.5f);
			obj.SetActive(true);
		}
		yield break;
	}

	// Token: 0x06002D36 RID: 11574 RVA: 0x00022269 File Offset: 0x00020469
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

	// Token: 0x06002D37 RID: 11575 RVA: 0x00022278 File Offset: 0x00020478
	private void PustiVoiceJump()
	{
		PlaySounds.Play_VoiceJump();
	}

	// Token: 0x06002D38 RID: 11576 RVA: 0x0016B528 File Offset: 0x00169728
	public void SetInvincible()
	{
		this.jumpSpeedX = Mathf.Abs(this.jumpSpeedX);
		this.moveForce = Mathf.Abs(this.moveForce);
		this.maxSpeedX = Mathf.Abs(this.maxSpeedX);
		this.majmun.localScale = new Vector3(this.majmun.localScale.x, this.majmun.localScale.y, Mathf.Abs(this.majmun.localScale.z));
		if (this.misijaSaDistance)
		{
			this.measureDistance = true;
		}
		this.invincible = true;
		base.StartCoroutine(this.blink());
		base.StopCoroutine(this.slowDown());
		this.killed = false;
		base.transform.Find("Particles/HolderKillStars").gameObject.SetActive(false);
		this.animator.Play(this.jump_State);
		this.grounded = false;
		this.proveraGround = 16;
		this.maxSpeedX = this.startSpeedX;
		this.state = MonkeyController2D.State.jumped;
		base.GetComponent<Rigidbody2D>().isKinematic = false;
		base.GetComponent<Rigidbody2D>().velocity = new Vector2(this.maxSpeedX, 50f);
		this.cameraFollow.stopFollow = false;
	}

	// Token: 0x06002D39 RID: 11577 RVA: 0x0002227F File Offset: 0x0002047F
	private IEnumerator blink()
	{
		float t = 0f;
		int i = 0;
		bool radi = false;
		Renderer meshrenderer = this.majmun.Find("PorinceGorilla_LP").GetComponent<Renderer>();
		Renderer usi = this.majmun.Find("Usi").GetComponent<Renderer>();
		Renderer kosa = this.majmun.Find("Kosa").GetComponent<Renderer>();
		Renderer glava = null;
		Renderer majica = null;
		Renderer ledja = null;
		base.gameObject.layer = LayerMask.NameToLayer("Monkey3D");
		if (StagesParser.glava != -1)
		{
			glava = this.majmun.Find("ROOT/Hip/Spine/Chest/Neck/Head/" + StagesParser.glava).GetChild(0).GetComponent<Renderer>();
		}
		if (StagesParser.majica != -1)
		{
			majica = this.majmun.Find("custom_Majica").GetComponent<Renderer>();
		}
		if (StagesParser.ledja != -1)
		{
			ledja = this.majmun.Find("ROOT/Hip/Spine/" + StagesParser.ledja).GetChild(0).GetComponent<Renderer>();
		}
		while (t < 1f)
		{
			if (StagesParser.imaUsi)
			{
				usi.enabled = radi;
			}
			if (StagesParser.imaKosu)
			{
				kosa.enabled = radi;
			}
			if (StagesParser.glava != -1)
			{
				glava.enabled = radi;
			}
			if (StagesParser.majica != -1)
			{
				majica.enabled = radi;
			}
			if (StagesParser.ledja != -1)
			{
				ledja.enabled = radi;
			}
			meshrenderer.enabled = radi;
			if (i == 3)
			{
				radi = !radi;
				i = 0;
			}
			int num = i;
			i = num + 1;
			t += Time.deltaTime / 3f;
			yield return null;
		}
		MonkeyController2D.canRespawnThings = true;
		meshrenderer.enabled = true;
		usi.enabled = true;
		kosa.enabled = true;
		if (glava != null)
		{
			glava.enabled = true;
		}
		if (majica != null)
		{
			majica.enabled = true;
		}
		if (ledja != null)
		{
			ledja.enabled = true;
		}
		this.invincible = false;
		yield break;
	}

	// Token: 0x06002D3A RID: 11578 RVA: 0x0002228E File Offset: 0x0002048E
	private IEnumerator RemoveFog(float time, Collider2D col)
	{
		yield return new WaitForSeconds(time);
		col.enabled = true;
		col.transform.GetChild(0).GetComponent<Renderer>().enabled = true;
		Camera.main.GetComponent<Animator>().Play("FogOfWar_Remove");
		yield break;
	}

	// Token: 0x06002D3B RID: 11579 RVA: 0x000222A4 File Offset: 0x000204A4
	private void DisableImpact()
	{
		base.transform.Find("Impact").gameObject.SetActive(false);
	}

	// Token: 0x06002D3C RID: 11580 RVA: 0x000222C1 File Offset: 0x000204C1
	public void cancelPowerfullImpact()
	{
		this.powerfullImpact = false;
		if (PlaySounds.Glide_NEW.isPlaying)
		{
			PlaySounds.Stop_Glide();
		}
		this.zutiGlowSwooshVisoki.gameObject.SetActive(false);
	}

	// Token: 0x06002D3D RID: 11581 RVA: 0x000222EC File Offset: 0x000204EC
	private void reaktivirajVoiceJump()
	{
		this.pustenVoiceJump = false;
	}

	// Token: 0x06002D3E RID: 11582 RVA: 0x000222F5 File Offset: 0x000204F5
	private IEnumerator pojaviMaglu(Collider2D col)
	{
		Transform fogOfWar = Camera.main.transform.Find("FogOfWar");
		float value = fogOfWar.localScale.x;
		float target = 12f;
		float t = 0f;
		while (t < 1f)
		{
			fogOfWar.localScale = new Vector3(Mathf.Lerp(fogOfWar.localScale.x, target, t), fogOfWar.localScale.y, fogOfWar.localScale.z);
			t += Time.deltaTime / 1.2f;
			yield return null;
		}
		col.enabled = true;
		yield return new WaitForSeconds(5f);
		t = 0f;
		while (t < 1f)
		{
			fogOfWar.localScale = new Vector3(Mathf.Lerp(fogOfWar.localScale.x, value, t), fogOfWar.localScale.y, fogOfWar.localScale.z);
			t += Time.deltaTime / 1.2f;
			yield return null;
		}
		col.transform.GetChild(0).GetComponent<Renderer>().enabled = true;
		fogOfWar.GetComponent<Renderer>().enabled = false;
		yield break;
	}

	// Token: 0x06002D3F RID: 11583 RVA: 0x00022304 File Offset: 0x00020504
	private IEnumerator Spusti2DFollow()
	{
		Vector3 target = this.cameraTarget.transform.localPosition - new Vector3(0f, 4.5f, 0f);
		while (this.cameraTarget.transform.localPosition.y >= target.y + 0.1f)
		{
			yield return null;
			this.cameraTarget.transform.localPosition = new Vector3(this.cameraTarget.transform.localPosition.x, Mathf.MoveTowards(this.cameraTarget.transform.localPosition.y, target.y, 0.2f), this.cameraTarget.transform.localPosition.z);
		}
		yield break;
	}

	// Token: 0x06002D40 RID: 11584 RVA: 0x00022313 File Offset: 0x00020513
	private IEnumerator Podigni2DFollow()
	{
		while (this.cameraTarget.transform.localPosition.y <= this.originalCameraTargetPosition - 0.1f)
		{
			yield return null;
			this.cameraTarget.transform.localPosition = new Vector3(this.cameraTarget.transform.localPosition.x, Mathf.MoveTowards(this.cameraTarget.transform.localPosition.y, this.originalCameraTargetPosition, 0.1f), this.cameraTarget.transform.localPosition.z);
		}
		this.cameraTarget.transform.localPosition = new Vector3(this.cameraTarget.transform.localPosition.x, this.originalCameraTargetPosition, this.cameraTarget.transform.localPosition.z);
		yield break;
	}

	// Token: 0x06002D41 RID: 11585 RVA: 0x00022322 File Offset: 0x00020522
	private void ReturnFromMushroom()
	{
		base.GetComponent<Rigidbody2D>().isKinematic = false;
		base.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
	}

	// Token: 0x0400278C RID: 10124
	public float moveForce = 300f;

	// Token: 0x0400278D RID: 10125
	public float maxSpeedX = 8f;

	// Token: 0x0400278E RID: 10126
	public float jumpForce = 700f;

	// Token: 0x0400278F RID: 10127
	public float doubleJumpForce = 100f;

	// Token: 0x04002790 RID: 10128
	public float gravity = 200f;

	// Token: 0x04002791 RID: 10129
	public float maxSpeedY = 8f;

	// Token: 0x04002792 RID: 10130
	public float jumpSpeedX = 12f;

	// Token: 0x04002793 RID: 10131
	private bool jump;

	// Token: 0x04002794 RID: 10132
	private bool doubleJump;

	// Token: 0x04002795 RID: 10133
	[HideInInspector]
	public bool inAir;

	// Token: 0x04002796 RID: 10134
	private bool hasJumped;

	// Token: 0x04002797 RID: 10135
	[HideInInspector]
	public bool zgaziEnemija;

	// Token: 0x04002798 RID: 10136
	[HideInInspector]
	public bool killed;

	// Token: 0x04002799 RID: 10137
	[HideInInspector]
	public bool stop;

	// Token: 0x0400279A RID: 10138
	[HideInInspector]
	public bool triggerCheckDownTrigger;

	// Token: 0x0400279B RID: 10139
	[HideInInspector]
	public bool triggerCheckDownBehind;

	// Token: 0x0400279C RID: 10140
	private bool CheckWallHitNear;

	// Token: 0x0400279D RID: 10141
	private bool CheckWallHitNear_low;

	// Token: 0x0400279E RID: 10142
	private bool startSpustanje;

	// Token: 0x0400279F RID: 10143
	private bool startPenjanje;

	// Token: 0x040027A0 RID: 10144
	private float pocetniY_spustanje;

	// Token: 0x040027A1 RID: 10145
	[HideInInspector]
	public float collisionAngle;

	// Token: 0x040027A2 RID: 10146
	private float duzinaPritiskaZaSkok;

	// Token: 0x040027A3 RID: 10147
	private bool mozeDaSkociOpet;

	// Token: 0x040027A4 RID: 10148
	private float startY;

	// Token: 0x040027A5 RID: 10149
	private float endX;

	// Token: 0x040027A6 RID: 10150
	private float endY;

	// Token: 0x040027A7 RID: 10151
	private bool swoosh;

	// Token: 0x040027A8 RID: 10152
	private Vector3 colliderForClimb;

	// Token: 0x040027A9 RID: 10153
	public bool Glide;

	// Token: 0x040027AA RID: 10154
	public bool DupliSkok;

	// Token: 0x040027AB RID: 10155
	public bool KontrolisaniSkok;

	// Token: 0x040027AC RID: 10156
	public bool SlideNaDole;

	// Token: 0x040027AD RID: 10157
	public bool Zaustavljanje;

	// Token: 0x040027AE RID: 10158
	private Ray2D ray;

	// Token: 0x040027AF RID: 10159
	private RaycastHit2D hit;

	// Token: 0x040027B0 RID: 10160
	private Transform ceilingCheck;

	// Token: 0x040027B1 RID: 10161
	private Transform groundCheck;

	// Token: 0x040027B2 RID: 10162
	[HideInInspector]
	public Transform majmun;

	// Token: 0x040027B3 RID: 10163
	public ParticleSystem trava;

	// Token: 0x040027B4 RID: 10164
	public ParticleSystem oblak;

	// Token: 0x040027B5 RID: 10165
	public ParticleSystem particleSkok;

	// Token: 0x040027B6 RID: 10166
	public ParticleSystem dupliSkokOblaci;

	// Token: 0x040027B7 RID: 10167
	public ParticleSystem runParticle;

	// Token: 0x040027B8 RID: 10168
	public ParticleSystem klizanje;

	// Token: 0x040027B9 RID: 10169
	public ParticleSystem izrazitiPad;

	// Token: 0x040027BA RID: 10170
	public Transform zutiGlowSwooshVisoki;

	// Token: 0x040027BB RID: 10171
	public ParticleSystem collectItem;

	// Token: 0x040027BC RID: 10172
	private Transform whatToClimb;

	// Token: 0x040027BD RID: 10173
	private float currentSpeed;

	// Token: 0x040027BE RID: 10174
	[HideInInspector]
	public GameObject cameraTarget;

	// Token: 0x040027BF RID: 10175
	[HideInInspector]
	public GameObject cameraTarget_down;

	// Token: 0x040027C0 RID: 10176
	private float cameraTarget_down_y;

	// Token: 0x040027C1 RID: 10177
	private CameraFollow2D_new cameraFollow;

	// Token: 0x040027C2 RID: 10178
	public MonkeyController2D.State state;

	// Token: 0x040027C3 RID: 10179
	[HideInInspector]
	public float startSpeedX;

	// Token: 0x040027C4 RID: 10180
	[HideInInspector]
	public float startJumpSpeedX;

	// Token: 0x040027C5 RID: 10181
	public bool neTrebaDaProdje;

	// Token: 0x040027C6 RID: 10182
	[HideInInspector]
	public Animator animator;

	// Token: 0x040027C7 RID: 10183
	[HideInInspector]
	public string lastPlayedAnim;

	// Token: 0x040027C8 RID: 10184
	private bool helpBool;

	// Token: 0x040027C9 RID: 10185
	private AnimatorStateInfo currentBaseState;

	// Token: 0x040027CA RID: 10186
	[HideInInspector]
	public int run_State = Animator.StringToHash("Base Layer.Running");

	// Token: 0x040027CB RID: 10187
	[HideInInspector]
	public int jump_State = Animator.StringToHash("Base Layer.Jump_Start");

	// Token: 0x040027CC RID: 10188
	[HideInInspector]
	public int fall_State = Animator.StringToHash("Base Layer.Jump_Falling");

	// Token: 0x040027CD RID: 10189
	[HideInInspector]
	public int landing_State = Animator.StringToHash("Base Layer.Landing");

	// Token: 0x040027CE RID: 10190
	[HideInInspector]
	public int doublejump_State = Animator.StringToHash("Base Layer.DoubleJump_Start");

	// Token: 0x040027CF RID: 10191
	[HideInInspector]
	public int glide_start_State = Animator.StringToHash("Base Layer.Glide_Start");

	// Token: 0x040027D0 RID: 10192
	[HideInInspector]
	public int glide_loop_State = Animator.StringToHash("Base Layer.Glide_Loop");

	// Token: 0x040027D1 RID: 10193
	[HideInInspector]
	public int grab_State = Animator.StringToHash("Base Layer.Grab");

	// Token: 0x040027D2 RID: 10194
	[HideInInspector]
	public int wall_stop_State = Animator.StringToHash("Base Layer.WallStop");

	// Token: 0x040027D3 RID: 10195
	[HideInInspector]
	public int wall_stop_from_jump_State = Animator.StringToHash("Base Layer.WallStop_From_Jump");

	// Token: 0x040027D4 RID: 10196
	[HideInInspector]
	public int swoosh_State = Animator.StringToHash("Base Layer.Swoosh_Down");

	// Token: 0x040027D5 RID: 10197
	[HideInInspector]
	public int spikedeath_State = Animator.StringToHash("Base Layer.Spike_death");

	// Token: 0x040027D6 RID: 10198
	[HideInInspector]
	public int lijana_State = Animator.StringToHash("Base Layer.Lijana");

	// Token: 0x040027D7 RID: 10199
	private Transform lookAtPos;

	// Token: 0x040027D8 RID: 10200
	private float lookWeight;

	// Token: 0x040027D9 RID: 10201
	private bool disableGlide;

	// Token: 0x040027DA RID: 10202
	private bool usporavanje;

	// Token: 0x040027DB RID: 10203
	[HideInInspector]
	public bool lijana;

	// Token: 0x040027DC RID: 10204
	private Transform grabLianaTransform;

	// Token: 0x040027DD RID: 10205
	[HideInInspector]
	public bool heCanJump = true;

	// Token: 0x040027DE RID: 10206
	private bool saZidaNaZid;

	// Token: 0x040027DF RID: 10207
	private int povrsinaZaClick;

	// Token: 0x040027E0 RID: 10208
	private bool jumpControlled;

	// Token: 0x040027E1 RID: 10209
	private float tempForce;

	// Token: 0x040027E2 RID: 10210
	[HideInInspector]
	public bool activeShield;

	// Token: 0x040027E3 RID: 10211
	private float razmrk;

	// Token: 0x040027E4 RID: 10212
	private Vector3 pocScale;

	// Token: 0x040027E5 RID: 10213
	private Transform senka;

	// Token: 0x040027E6 RID: 10214
	public static bool canRespawnThings = true;

	// Token: 0x040027E7 RID: 10215
	[SerializeField]
	private LayerMask whatIsGround;

	// Token: 0x040027E8 RID: 10216
	private float groundedRadius = 0.2f;

	// Token: 0x040027E9 RID: 10217
	[HideInInspector]
	public bool grounded;

	// Token: 0x040027EA RID: 10218
	private float korakce;

	// Token: 0x040027EB RID: 10219
	[HideInInspector]
	public bool canGlide;

	// Token: 0x040027EC RID: 10220
	private int proveraGround = 16;

	// Token: 0x040027ED RID: 10221
	private bool jumpHolding;

	// Token: 0x040027EE RID: 10222
	private TrailRenderer trail;

	// Token: 0x040027EF RID: 10223
	[HideInInspector]
	public bool powerfullImpact;

	// Token: 0x040027F0 RID: 10224
	public float trailTime = 0.5f;

	// Token: 0x040027F1 RID: 10225
	public bool invincible;

	// Token: 0x040027F2 RID: 10226
	private bool utepanULetu;

	// Token: 0x040027F3 RID: 10227
	public bool magnet;

	// Token: 0x040027F4 RID: 10228
	public bool doublecoins;

	// Token: 0x040027F5 RID: 10229
	private Manage manage;

	// Token: 0x040027F6 RID: 10230
	public bool measureDistance;

	// Token: 0x040027F7 RID: 10231
	public float distance;

	// Token: 0x040027F8 RID: 10232
	private float startPosX;

	// Token: 0x040027F9 RID: 10233
	public bool misijaSaDistance;

	// Token: 0x040027FA RID: 10234
	private bool pustenVoiceJump;

	// Token: 0x040027FB RID: 10235
	private Camera guiCamera;

	// Token: 0x040027FC RID: 10236
	[HideInInspector]
	public bool isSliding;

	// Token: 0x040027FD RID: 10237
	private Collider2D enemyCollider;

	// Token: 0x040027FE RID: 10238
	[HideInInspector]
	public float originalCameraTargetPosition;

	// Token: 0x040027FF RID: 10239
	private bool podigniMaloKameru;

	// Token: 0x04002800 RID: 10240
	private bool dancing;

	// Token: 0x04002801 RID: 10241
	public PhysicsMaterial2D finishDontMove;

	// Token: 0x04002802 RID: 10242
	[HideInInspector]
	public bool mushroomJumped;

	// Token: 0x04002803 RID: 10243
	[HideInInspector]
	public bool wallHitGlide;

	// Token: 0x04002804 RID: 10244
	private static MonkeyController2D instance;

	// Token: 0x020006FA RID: 1786
	[HideInInspector]
	public enum State
	{
		// Token: 0x04002806 RID: 10246
		running,
		// Token: 0x04002807 RID: 10247
		jumped,
		// Token: 0x04002808 RID: 10248
		wallhit,
		// Token: 0x04002809 RID: 10249
		climbUp,
		// Token: 0x0400280A RID: 10250
		actualClimbing,
		// Token: 0x0400280B RID: 10251
		wasted,
		// Token: 0x0400280C RID: 10252
		idle,
		// Token: 0x0400280D RID: 10253
		completed,
		// Token: 0x0400280E RID: 10254
		lijana,
		// Token: 0x0400280F RID: 10255
		saZidaNaZid,
		// Token: 0x04002810 RID: 10256
		preNegoDaSeOdbije
	}
}
