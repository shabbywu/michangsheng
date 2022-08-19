using System;
using System.Collections;
using UnityEngine;

// Token: 0x020004C6 RID: 1222
public class MonkeyController2D : MonoBehaviour
{
	// Token: 0x170002AF RID: 687
	// (get) Token: 0x06002723 RID: 10019 RVA: 0x0011EAEF File Offset: 0x0011CCEF
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

	// Token: 0x06002724 RID: 10020 RVA: 0x0011EB1C File Offset: 0x0011CD1C
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

	// Token: 0x06002725 RID: 10021 RVA: 0x0011EBEC File Offset: 0x0011CDEC
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

	// Token: 0x06002726 RID: 10022 RVA: 0x0011EE6C File Offset: 0x0011D06C
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

	// Token: 0x06002727 RID: 10023 RVA: 0x0012024C File Offset: 0x0011E44C
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

	// Token: 0x06002728 RID: 10024 RVA: 0x001208CC File Offset: 0x0011EACC
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

	// Token: 0x06002729 RID: 10025 RVA: 0x00120994 File Offset: 0x0011EB94
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

	// Token: 0x0600272A RID: 10026 RVA: 0x001211EB File Offset: 0x0011F3EB
	private void EnableColliderBackOnEnemy()
	{
		this.enemyCollider.enabled = true;
		this.enemyCollider = null;
	}

	// Token: 0x0600272B RID: 10027 RVA: 0x00121200 File Offset: 0x0011F400
	public void majmunUtepanULetu()
	{
		base.StartCoroutine(this.FallDownAfterSpikes());
	}

	// Token: 0x0600272C RID: 10028 RVA: 0x0012120F File Offset: 0x0011F40F
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

	// Token: 0x0600272D RID: 10029 RVA: 0x0012121E File Offset: 0x0011F41E
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

	// Token: 0x0600272E RID: 10030 RVA: 0x00121230 File Offset: 0x0011F430
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

	// Token: 0x0600272F RID: 10031 RVA: 0x00121539 File Offset: 0x0011F739
	private void NotifyManagerForFinish()
	{
		this.manage.SendMessage("ShowWinScreen");
	}

	// Token: 0x06002730 RID: 10032 RVA: 0x0012154B File Offset: 0x0011F74B
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

	// Token: 0x06002731 RID: 10033 RVA: 0x00121570 File Offset: 0x0011F770
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

	// Token: 0x06002732 RID: 10034 RVA: 0x00122024 File Offset: 0x00120224
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

	// Token: 0x06002733 RID: 10035 RVA: 0x00122114 File Offset: 0x00120314
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

	// Token: 0x06002734 RID: 10036 RVA: 0x00122124 File Offset: 0x00120324
	private void RestoreMaxSpeed()
	{
		this.dancing = false;
		base.GetComponent<Rigidbody2D>().mass = 1.25f;
		this.animator.SetTrigger("DancingDone");
		this.maxSpeedX = 16f;
		base.Invoke("NotifyManagerForFinish", 1.25f);
	}

	// Token: 0x06002735 RID: 10037 RVA: 0x00122173 File Offset: 0x00120373
	private void TurnOnKinematic()
	{
		base.GetComponent<Rigidbody2D>().isKinematic = true;
	}

	// Token: 0x06002736 RID: 10038 RVA: 0x00122181 File Offset: 0x00120381
	private IEnumerator pratiLijanaTarget(Transform target)
	{
		for (;;)
		{
			yield return null;
			base.transform.position = Vector3.Lerp(base.transform.position, new Vector3(target.position.x, target.position.y, base.transform.position.z), 0.25f);
		}
		yield break;
	}

	// Token: 0x06002737 RID: 10039 RVA: 0x00122198 File Offset: 0x00120398
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

	// Token: 0x06002738 RID: 10040 RVA: 0x0012224C File Offset: 0x0012044C
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

	// Token: 0x06002739 RID: 10041 RVA: 0x001222C8 File Offset: 0x001204C8
	private IEnumerator Bounce(float time)
	{
		yield return new WaitForSeconds(time);
		this.stop = false;
		yield break;
	}

	// Token: 0x0600273A RID: 10042 RVA: 0x001222E0 File Offset: 0x001204E0
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

	// Token: 0x0600273B RID: 10043 RVA: 0x001223D2 File Offset: 0x001205D2
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

	// Token: 0x0600273C RID: 10044 RVA: 0x001223E1 File Offset: 0x001205E1
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

	// Token: 0x0600273D RID: 10045 RVA: 0x001223F0 File Offset: 0x001205F0
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

	// Token: 0x0600273E RID: 10046 RVA: 0x00122445 File Offset: 0x00120645
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

	// Token: 0x0600273F RID: 10047 RVA: 0x0012245B File Offset: 0x0012065B
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

	// Token: 0x06002740 RID: 10048 RVA: 0x0012246A File Offset: 0x0012066A
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

	// Token: 0x06002741 RID: 10049 RVA: 0x00122479 File Offset: 0x00120679
	private IEnumerator reappearItem(GameObject obj)
	{
		if (MonkeyController2D.canRespawnThings)
		{
			yield return new WaitForSeconds(5.5f);
			obj.SetActive(true);
		}
		yield break;
	}

	// Token: 0x06002742 RID: 10050 RVA: 0x00122488 File Offset: 0x00120688
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

	// Token: 0x06002743 RID: 10051 RVA: 0x00122497 File Offset: 0x00120697
	private void PustiVoiceJump()
	{
		PlaySounds.Play_VoiceJump();
	}

	// Token: 0x06002744 RID: 10052 RVA: 0x001224A0 File Offset: 0x001206A0
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

	// Token: 0x06002745 RID: 10053 RVA: 0x001225DC File Offset: 0x001207DC
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

	// Token: 0x06002746 RID: 10054 RVA: 0x001225EB File Offset: 0x001207EB
	private IEnumerator RemoveFog(float time, Collider2D col)
	{
		yield return new WaitForSeconds(time);
		col.enabled = true;
		col.transform.GetChild(0).GetComponent<Renderer>().enabled = true;
		Camera.main.GetComponent<Animator>().Play("FogOfWar_Remove");
		yield break;
	}

	// Token: 0x06002747 RID: 10055 RVA: 0x00122601 File Offset: 0x00120801
	private void DisableImpact()
	{
		base.transform.Find("Impact").gameObject.SetActive(false);
	}

	// Token: 0x06002748 RID: 10056 RVA: 0x0012261E File Offset: 0x0012081E
	public void cancelPowerfullImpact()
	{
		this.powerfullImpact = false;
		if (PlaySounds.Glide_NEW.isPlaying)
		{
			PlaySounds.Stop_Glide();
		}
		this.zutiGlowSwooshVisoki.gameObject.SetActive(false);
	}

	// Token: 0x06002749 RID: 10057 RVA: 0x00122649 File Offset: 0x00120849
	private void reaktivirajVoiceJump()
	{
		this.pustenVoiceJump = false;
	}

	// Token: 0x0600274A RID: 10058 RVA: 0x00122652 File Offset: 0x00120852
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

	// Token: 0x0600274B RID: 10059 RVA: 0x00122661 File Offset: 0x00120861
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

	// Token: 0x0600274C RID: 10060 RVA: 0x00122670 File Offset: 0x00120870
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

	// Token: 0x0600274D RID: 10061 RVA: 0x0012267F File Offset: 0x0012087F
	private void ReturnFromMushroom()
	{
		base.GetComponent<Rigidbody2D>().isKinematic = false;
		base.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
	}

	// Token: 0x0400212B RID: 8491
	public float moveForce = 300f;

	// Token: 0x0400212C RID: 8492
	public float maxSpeedX = 8f;

	// Token: 0x0400212D RID: 8493
	public float jumpForce = 700f;

	// Token: 0x0400212E RID: 8494
	public float doubleJumpForce = 100f;

	// Token: 0x0400212F RID: 8495
	public float gravity = 200f;

	// Token: 0x04002130 RID: 8496
	public float maxSpeedY = 8f;

	// Token: 0x04002131 RID: 8497
	public float jumpSpeedX = 12f;

	// Token: 0x04002132 RID: 8498
	private bool jump;

	// Token: 0x04002133 RID: 8499
	private bool doubleJump;

	// Token: 0x04002134 RID: 8500
	[HideInInspector]
	public bool inAir;

	// Token: 0x04002135 RID: 8501
	private bool hasJumped;

	// Token: 0x04002136 RID: 8502
	[HideInInspector]
	public bool zgaziEnemija;

	// Token: 0x04002137 RID: 8503
	[HideInInspector]
	public bool killed;

	// Token: 0x04002138 RID: 8504
	[HideInInspector]
	public bool stop;

	// Token: 0x04002139 RID: 8505
	[HideInInspector]
	public bool triggerCheckDownTrigger;

	// Token: 0x0400213A RID: 8506
	[HideInInspector]
	public bool triggerCheckDownBehind;

	// Token: 0x0400213B RID: 8507
	private bool CheckWallHitNear;

	// Token: 0x0400213C RID: 8508
	private bool CheckWallHitNear_low;

	// Token: 0x0400213D RID: 8509
	private bool startSpustanje;

	// Token: 0x0400213E RID: 8510
	private bool startPenjanje;

	// Token: 0x0400213F RID: 8511
	private float pocetniY_spustanje;

	// Token: 0x04002140 RID: 8512
	[HideInInspector]
	public float collisionAngle;

	// Token: 0x04002141 RID: 8513
	private float duzinaPritiskaZaSkok;

	// Token: 0x04002142 RID: 8514
	private bool mozeDaSkociOpet;

	// Token: 0x04002143 RID: 8515
	private float startY;

	// Token: 0x04002144 RID: 8516
	private float endX;

	// Token: 0x04002145 RID: 8517
	private float endY;

	// Token: 0x04002146 RID: 8518
	private bool swoosh;

	// Token: 0x04002147 RID: 8519
	private Vector3 colliderForClimb;

	// Token: 0x04002148 RID: 8520
	public bool Glide;

	// Token: 0x04002149 RID: 8521
	public bool DupliSkok;

	// Token: 0x0400214A RID: 8522
	public bool KontrolisaniSkok;

	// Token: 0x0400214B RID: 8523
	public bool SlideNaDole;

	// Token: 0x0400214C RID: 8524
	public bool Zaustavljanje;

	// Token: 0x0400214D RID: 8525
	private Ray2D ray;

	// Token: 0x0400214E RID: 8526
	private RaycastHit2D hit;

	// Token: 0x0400214F RID: 8527
	private Transform ceilingCheck;

	// Token: 0x04002150 RID: 8528
	private Transform groundCheck;

	// Token: 0x04002151 RID: 8529
	[HideInInspector]
	public Transform majmun;

	// Token: 0x04002152 RID: 8530
	public ParticleSystem trava;

	// Token: 0x04002153 RID: 8531
	public ParticleSystem oblak;

	// Token: 0x04002154 RID: 8532
	public ParticleSystem particleSkok;

	// Token: 0x04002155 RID: 8533
	public ParticleSystem dupliSkokOblaci;

	// Token: 0x04002156 RID: 8534
	public ParticleSystem runParticle;

	// Token: 0x04002157 RID: 8535
	public ParticleSystem klizanje;

	// Token: 0x04002158 RID: 8536
	public ParticleSystem izrazitiPad;

	// Token: 0x04002159 RID: 8537
	public Transform zutiGlowSwooshVisoki;

	// Token: 0x0400215A RID: 8538
	public ParticleSystem collectItem;

	// Token: 0x0400215B RID: 8539
	private Transform whatToClimb;

	// Token: 0x0400215C RID: 8540
	private float currentSpeed;

	// Token: 0x0400215D RID: 8541
	[HideInInspector]
	public GameObject cameraTarget;

	// Token: 0x0400215E RID: 8542
	[HideInInspector]
	public GameObject cameraTarget_down;

	// Token: 0x0400215F RID: 8543
	private float cameraTarget_down_y;

	// Token: 0x04002160 RID: 8544
	private CameraFollow2D_new cameraFollow;

	// Token: 0x04002161 RID: 8545
	public MonkeyController2D.State state;

	// Token: 0x04002162 RID: 8546
	[HideInInspector]
	public float startSpeedX;

	// Token: 0x04002163 RID: 8547
	[HideInInspector]
	public float startJumpSpeedX;

	// Token: 0x04002164 RID: 8548
	public bool neTrebaDaProdje;

	// Token: 0x04002165 RID: 8549
	[HideInInspector]
	public Animator animator;

	// Token: 0x04002166 RID: 8550
	[HideInInspector]
	public string lastPlayedAnim;

	// Token: 0x04002167 RID: 8551
	private bool helpBool;

	// Token: 0x04002168 RID: 8552
	private AnimatorStateInfo currentBaseState;

	// Token: 0x04002169 RID: 8553
	[HideInInspector]
	public int run_State = Animator.StringToHash("Base Layer.Running");

	// Token: 0x0400216A RID: 8554
	[HideInInspector]
	public int jump_State = Animator.StringToHash("Base Layer.Jump_Start");

	// Token: 0x0400216B RID: 8555
	[HideInInspector]
	public int fall_State = Animator.StringToHash("Base Layer.Jump_Falling");

	// Token: 0x0400216C RID: 8556
	[HideInInspector]
	public int landing_State = Animator.StringToHash("Base Layer.Landing");

	// Token: 0x0400216D RID: 8557
	[HideInInspector]
	public int doublejump_State = Animator.StringToHash("Base Layer.DoubleJump_Start");

	// Token: 0x0400216E RID: 8558
	[HideInInspector]
	public int glide_start_State = Animator.StringToHash("Base Layer.Glide_Start");

	// Token: 0x0400216F RID: 8559
	[HideInInspector]
	public int glide_loop_State = Animator.StringToHash("Base Layer.Glide_Loop");

	// Token: 0x04002170 RID: 8560
	[HideInInspector]
	public int grab_State = Animator.StringToHash("Base Layer.Grab");

	// Token: 0x04002171 RID: 8561
	[HideInInspector]
	public int wall_stop_State = Animator.StringToHash("Base Layer.WallStop");

	// Token: 0x04002172 RID: 8562
	[HideInInspector]
	public int wall_stop_from_jump_State = Animator.StringToHash("Base Layer.WallStop_From_Jump");

	// Token: 0x04002173 RID: 8563
	[HideInInspector]
	public int swoosh_State = Animator.StringToHash("Base Layer.Swoosh_Down");

	// Token: 0x04002174 RID: 8564
	[HideInInspector]
	public int spikedeath_State = Animator.StringToHash("Base Layer.Spike_death");

	// Token: 0x04002175 RID: 8565
	[HideInInspector]
	public int lijana_State = Animator.StringToHash("Base Layer.Lijana");

	// Token: 0x04002176 RID: 8566
	private Transform lookAtPos;

	// Token: 0x04002177 RID: 8567
	private float lookWeight;

	// Token: 0x04002178 RID: 8568
	private bool disableGlide;

	// Token: 0x04002179 RID: 8569
	private bool usporavanje;

	// Token: 0x0400217A RID: 8570
	[HideInInspector]
	public bool lijana;

	// Token: 0x0400217B RID: 8571
	private Transform grabLianaTransform;

	// Token: 0x0400217C RID: 8572
	[HideInInspector]
	public bool heCanJump = true;

	// Token: 0x0400217D RID: 8573
	private bool saZidaNaZid;

	// Token: 0x0400217E RID: 8574
	private int povrsinaZaClick;

	// Token: 0x0400217F RID: 8575
	private bool jumpControlled;

	// Token: 0x04002180 RID: 8576
	private float tempForce;

	// Token: 0x04002181 RID: 8577
	[HideInInspector]
	public bool activeShield;

	// Token: 0x04002182 RID: 8578
	private float razmrk;

	// Token: 0x04002183 RID: 8579
	private Vector3 pocScale;

	// Token: 0x04002184 RID: 8580
	private Transform senka;

	// Token: 0x04002185 RID: 8581
	public static bool canRespawnThings = true;

	// Token: 0x04002186 RID: 8582
	[SerializeField]
	private LayerMask whatIsGround;

	// Token: 0x04002187 RID: 8583
	private float groundedRadius = 0.2f;

	// Token: 0x04002188 RID: 8584
	[HideInInspector]
	public bool grounded;

	// Token: 0x04002189 RID: 8585
	private float korakce;

	// Token: 0x0400218A RID: 8586
	[HideInInspector]
	public bool canGlide;

	// Token: 0x0400218B RID: 8587
	private int proveraGround = 16;

	// Token: 0x0400218C RID: 8588
	private bool jumpHolding;

	// Token: 0x0400218D RID: 8589
	private TrailRenderer trail;

	// Token: 0x0400218E RID: 8590
	[HideInInspector]
	public bool powerfullImpact;

	// Token: 0x0400218F RID: 8591
	public float trailTime = 0.5f;

	// Token: 0x04002190 RID: 8592
	public bool invincible;

	// Token: 0x04002191 RID: 8593
	private bool utepanULetu;

	// Token: 0x04002192 RID: 8594
	public bool magnet;

	// Token: 0x04002193 RID: 8595
	public bool doublecoins;

	// Token: 0x04002194 RID: 8596
	private Manage manage;

	// Token: 0x04002195 RID: 8597
	public bool measureDistance;

	// Token: 0x04002196 RID: 8598
	public float distance;

	// Token: 0x04002197 RID: 8599
	private float startPosX;

	// Token: 0x04002198 RID: 8600
	public bool misijaSaDistance;

	// Token: 0x04002199 RID: 8601
	private bool pustenVoiceJump;

	// Token: 0x0400219A RID: 8602
	private Camera guiCamera;

	// Token: 0x0400219B RID: 8603
	[HideInInspector]
	public bool isSliding;

	// Token: 0x0400219C RID: 8604
	private Collider2D enemyCollider;

	// Token: 0x0400219D RID: 8605
	[HideInInspector]
	public float originalCameraTargetPosition;

	// Token: 0x0400219E RID: 8606
	private bool podigniMaloKameru;

	// Token: 0x0400219F RID: 8607
	private bool dancing;

	// Token: 0x040021A0 RID: 8608
	public PhysicsMaterial2D finishDontMove;

	// Token: 0x040021A1 RID: 8609
	[HideInInspector]
	public bool mushroomJumped;

	// Token: 0x040021A2 RID: 8610
	[HideInInspector]
	public bool wallHitGlide;

	// Token: 0x040021A3 RID: 8611
	private static MonkeyController2D instance;

	// Token: 0x0200141D RID: 5149
	[HideInInspector]
	public enum State
	{
		// Token: 0x04006AA4 RID: 27300
		running,
		// Token: 0x04006AA5 RID: 27301
		jumped,
		// Token: 0x04006AA6 RID: 27302
		wallhit,
		// Token: 0x04006AA7 RID: 27303
		climbUp,
		// Token: 0x04006AA8 RID: 27304
		actualClimbing,
		// Token: 0x04006AA9 RID: 27305
		wasted,
		// Token: 0x04006AAA RID: 27306
		idle,
		// Token: 0x04006AAB RID: 27307
		completed,
		// Token: 0x04006AAC RID: 27308
		lijana,
		// Token: 0x04006AAD RID: 27309
		saZidaNaZid,
		// Token: 0x04006AAE RID: 27310
		preNegoDaSeOdbije
	}
}
