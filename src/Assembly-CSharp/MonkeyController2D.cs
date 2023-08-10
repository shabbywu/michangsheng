using System;
using System.Collections;
using UnityEngine;

public class MonkeyController2D : MonoBehaviour
{
	[HideInInspector]
	public enum State
	{
		running,
		jumped,
		wallhit,
		climbUp,
		actualClimbing,
		wasted,
		idle,
		completed,
		lijana,
		saZidaNaZid,
		preNegoDaSeOdbije
	}

	public float moveForce = 300f;

	public float maxSpeedX = 8f;

	public float jumpForce = 700f;

	public float doubleJumpForce = 100f;

	public float gravity = 200f;

	public float maxSpeedY = 8f;

	public float jumpSpeedX = 12f;

	private bool jump;

	private bool doubleJump;

	[HideInInspector]
	public bool inAir;

	private bool hasJumped;

	[HideInInspector]
	public bool zgaziEnemija;

	[HideInInspector]
	public bool killed;

	[HideInInspector]
	public bool stop;

	[HideInInspector]
	public bool triggerCheckDownTrigger;

	[HideInInspector]
	public bool triggerCheckDownBehind;

	private bool CheckWallHitNear;

	private bool CheckWallHitNear_low;

	private bool startSpustanje;

	private bool startPenjanje;

	private float pocetniY_spustanje;

	[HideInInspector]
	public float collisionAngle;

	private float duzinaPritiskaZaSkok;

	private bool mozeDaSkociOpet;

	private float startY;

	private float endX;

	private float endY;

	private bool swoosh;

	private Vector3 colliderForClimb;

	public bool Glide;

	public bool DupliSkok;

	public bool KontrolisaniSkok;

	public bool SlideNaDole;

	public bool Zaustavljanje;

	private Ray2D ray;

	private RaycastHit2D hit;

	private Transform ceilingCheck;

	private Transform groundCheck;

	[HideInInspector]
	public Transform majmun;

	public ParticleSystem trava;

	public ParticleSystem oblak;

	public ParticleSystem particleSkok;

	public ParticleSystem dupliSkokOblaci;

	public ParticleSystem runParticle;

	public ParticleSystem klizanje;

	public ParticleSystem izrazitiPad;

	public Transform zutiGlowSwooshVisoki;

	public ParticleSystem collectItem;

	private Transform whatToClimb;

	private float currentSpeed;

	[HideInInspector]
	public GameObject cameraTarget;

	[HideInInspector]
	public GameObject cameraTarget_down;

	private float cameraTarget_down_y;

	private CameraFollow2D_new cameraFollow;

	public State state;

	[HideInInspector]
	public float startSpeedX;

	[HideInInspector]
	public float startJumpSpeedX;

	public bool neTrebaDaProdje;

	[HideInInspector]
	public Animator animator;

	[HideInInspector]
	public string lastPlayedAnim;

	private bool helpBool;

	private AnimatorStateInfo currentBaseState;

	[HideInInspector]
	public int run_State = Animator.StringToHash("Base Layer.Running");

	[HideInInspector]
	public int jump_State = Animator.StringToHash("Base Layer.Jump_Start");

	[HideInInspector]
	public int fall_State = Animator.StringToHash("Base Layer.Jump_Falling");

	[HideInInspector]
	public int landing_State = Animator.StringToHash("Base Layer.Landing");

	[HideInInspector]
	public int doublejump_State = Animator.StringToHash("Base Layer.DoubleJump_Start");

	[HideInInspector]
	public int glide_start_State = Animator.StringToHash("Base Layer.Glide_Start");

	[HideInInspector]
	public int glide_loop_State = Animator.StringToHash("Base Layer.Glide_Loop");

	[HideInInspector]
	public int grab_State = Animator.StringToHash("Base Layer.Grab");

	[HideInInspector]
	public int wall_stop_State = Animator.StringToHash("Base Layer.WallStop");

	[HideInInspector]
	public int wall_stop_from_jump_State = Animator.StringToHash("Base Layer.WallStop_From_Jump");

	[HideInInspector]
	public int swoosh_State = Animator.StringToHash("Base Layer.Swoosh_Down");

	[HideInInspector]
	public int spikedeath_State = Animator.StringToHash("Base Layer.Spike_death");

	[HideInInspector]
	public int lijana_State = Animator.StringToHash("Base Layer.Lijana");

	private Transform lookAtPos;

	private float lookWeight;

	private bool disableGlide;

	private bool usporavanje;

	[HideInInspector]
	public bool lijana;

	private Transform grabLianaTransform;

	[HideInInspector]
	public bool heCanJump = true;

	private bool saZidaNaZid;

	private int povrsinaZaClick;

	private bool jumpControlled;

	private float tempForce;

	[HideInInspector]
	public bool activeShield;

	private float razmrk;

	private Vector3 pocScale;

	private Transform senka;

	public static bool canRespawnThings = true;

	[SerializeField]
	private LayerMask whatIsGround;

	private float groundedRadius = 0.2f;

	[HideInInspector]
	public bool grounded;

	private float korakce;

	[HideInInspector]
	public bool canGlide;

	private int proveraGround = 16;

	private bool jumpHolding;

	private TrailRenderer trail;

	[HideInInspector]
	public bool powerfullImpact;

	public float trailTime = 0.5f;

	public bool invincible;

	private bool utepanULetu;

	public bool magnet;

	public bool doublecoins;

	private Manage manage;

	public bool measureDistance;

	public float distance;

	private float startPosX;

	public bool misijaSaDistance;

	private bool pustenVoiceJump;

	private Camera guiCamera;

	[HideInInspector]
	public bool isSliding;

	private Collider2D enemyCollider;

	[HideInInspector]
	public float originalCameraTargetPosition;

	private bool podigniMaloKameru;

	private bool dancing;

	public PhysicsMaterial2D finishDontMove;

	[HideInInspector]
	public bool mushroomJumped;

	[HideInInspector]
	public bool wallHitGlide;

	private static MonkeyController2D instance;

	public static MonkeyController2D Instance
	{
		get
		{
			if ((Object)(object)instance == (Object)null)
			{
				instance = Object.FindObjectOfType(typeof(MonkeyController2D)) as MonkeyController2D;
			}
			return instance;
		}
	}

	private void Awake()
	{
		majmun = GameObject.Find("PrinceGorilla").transform;
		animator = ((Component)majmun).GetComponent<Animator>();
		cameraTarget = ((Component)((Component)this).transform.Find("PlayerFocus2D")).gameObject;
		cameraTarget_down = ((Component)((Component)this).transform.Find("PlayerFocus2D_down")).gameObject;
		cameraFollow = ((Component)((Component)Camera.main).transform.parent).GetComponent<CameraFollow2D_new>();
		lookAtPos = ((Component)this).transform.Find("LookAtPos");
		Input.multiTouchEnabled = true;
		manage = GameObject.Find("_GameManager").GetComponent<Manage>();
		guiCamera = GameObject.Find("GUICamera").GetComponent<Camera>();
		instance = this;
	}

	private void Start()
	{
		//IL_0100: Unknown result type (might be due to invalid IL or missing references)
		//IL_017d: Unknown result type (might be due to invalid IL or missing references)
		//IL_018f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0194: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_0249: Unknown result type (might be due to invalid IL or missing references)
		//IL_0264: Unknown result type (might be due to invalid IL or missing references)
		if (!StagesParser.imaKosu)
		{
			((Component)majmun.Find("Kosa")).gameObject.SetActive(false);
		}
		if (!StagesParser.imaUsi)
		{
			((Component)majmun.Find("Usi")).gameObject.SetActive(false);
		}
		if (StagesParser.glava != -1)
		{
			((Component)majmun.Find("ROOT/Hip/Spine/Chest/Neck/Head/" + StagesParser.glava).GetChild(0)).gameObject.SetActive(true);
		}
		if (StagesParser.majica != -1)
		{
			((Component)majmun.Find("custom_Majica")).gameObject.SetActive(true);
			Object obj = Resources.Load("Majice/Bg" + StagesParser.majica);
			Texture val = (Texture)(object)((obj is Texture) ? obj : null);
			((Component)majmun.Find("custom_Majica")).GetComponent<Renderer>().material.SetTexture("_MainTex", val);
			((Component)majmun.Find("custom_Majica")).GetComponent<Renderer>().material.color = StagesParser.bojaMajice;
		}
		if (StagesParser.ledja != -1)
		{
			((Component)majmun.Find("ROOT/Hip/Spine/" + StagesParser.ledja).GetChild(0)).gameObject.SetActive(true);
		}
		startSpeedX = maxSpeedX;
		startJumpSpeedX = jumpSpeedX;
		state = State.idle;
		Resources.UnloadUnusedAssets();
		cameraTarget_down.transform.position = cameraTarget.transform.position;
		currentBaseState = animator.GetCurrentAnimatorStateInfo(0);
		animator.speed = 1.5f;
		animator.SetLookAtWeight(lookWeight);
		animator.SetLookAtPosition(lookAtPos.position);
		tempForce = jumpForce;
		senka = GameObject.Find("shadowMonkey").transform;
		canRespawnThings = true;
		groundCheck = ((Component)this).transform.Find("GroundCheck");
		ceilingCheck = ((Component)this).transform.Find("CeilingCheck");
		trail = ((Component)((Component)this).transform.Find("Trail")).GetComponent<TrailRenderer>();
		startPosX = ((Component)Camera.main).transform.position.x;
		originalCameraTargetPosition = cameraTarget.transform.localPosition.y;
	}

	private void Update()
	{
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_001b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0020: Unknown result type (might be due to invalid IL or missing references)
		//IL_0025: Unknown result type (might be due to invalid IL or missing references)
		//IL_0030: Unknown result type (might be due to invalid IL or missing references)
		//IL_0044: Unknown result type (might be due to invalid IL or missing references)
		//IL_0049: Unknown result type (might be due to invalid IL or missing references)
		//IL_004e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0072: Unknown result type (might be due to invalid IL or missing references)
		//IL_0077: Unknown result type (might be due to invalid IL or missing references)
		//IL_007d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0126: Unknown result type (might be due to invalid IL or missing references)
		//IL_0098: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00be: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f8: Unknown result type (might be due to invalid IL or missing references)
		//IL_0114: Unknown result type (might be due to invalid IL or missing references)
		//IL_0143: Unknown result type (might be due to invalid IL or missing references)
		//IL_0276: Unknown result type (might be due to invalid IL or missing references)
		//IL_0291: Unknown result type (might be due to invalid IL or missing references)
		//IL_029b: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_0195: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_01df: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e9: Unknown result type (might be due to invalid IL or missing references)
		//IL_01fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_0213: Unknown result type (might be due to invalid IL or missing references)
		//IL_0335: Unknown result type (might be due to invalid IL or missing references)
		//IL_033a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0346: Unknown result type (might be due to invalid IL or missing references)
		//IL_0376: Unknown result type (might be due to invalid IL or missing references)
		//IL_038a: Unknown result type (might be due to invalid IL or missing references)
		//IL_038f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0394: Unknown result type (might be due to invalid IL or missing references)
		//IL_039f: Unknown result type (might be due to invalid IL or missing references)
		//IL_03b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_03b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_03bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_03d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_03e7: Unknown result type (might be due to invalid IL or missing references)
		//IL_03fb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0400: Unknown result type (might be due to invalid IL or missing references)
		//IL_0405: Unknown result type (might be due to invalid IL or missing references)
		//IL_0410: Unknown result type (might be due to invalid IL or missing references)
		//IL_0424: Unknown result type (might be due to invalid IL or missing references)
		//IL_0429: Unknown result type (might be due to invalid IL or missing references)
		//IL_042e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0442: Unknown result type (might be due to invalid IL or missing references)
		//IL_0479: Unknown result type (might be due to invalid IL or missing references)
		//IL_059d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0703: Unknown result type (might be due to invalid IL or missing references)
		//IL_0507: Unknown result type (might be due to invalid IL or missing references)
		//IL_050c: Unknown result type (might be due to invalid IL or missing references)
		//IL_050f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b7b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0988: Unknown result type (might be due to invalid IL or missing references)
		//IL_0fa3: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d3a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b95: Unknown result type (might be due to invalid IL or missing references)
		//IL_09a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_075d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0762: Unknown result type (might be due to invalid IL or missing references)
		//IL_076d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0772: Unknown result type (might be due to invalid IL or missing references)
		//IL_077c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0781: Unknown result type (might be due to invalid IL or missing references)
		//IL_0786: Unknown result type (might be due to invalid IL or missing references)
		//IL_078c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0796: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d5a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0baf: Unknown result type (might be due to invalid IL or missing references)
		//IL_09c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_0676: Unknown result type (might be due to invalid IL or missing references)
		//IL_067b: Unknown result type (might be due to invalid IL or missing references)
		//IL_067e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0684: Invalid comparison between Unknown and I4
		//IL_118f: Unknown result type (might be due to invalid IL or missing references)
		//IL_10dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0fc5: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d74: Unknown result type (might be due to invalid IL or missing references)
		//IL_0bc9: Unknown result type (might be due to invalid IL or missing references)
		//IL_09dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_11a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0fdf: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d8e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0be3: Unknown result type (might be due to invalid IL or missing references)
		//IL_09f6: Unknown result type (might be due to invalid IL or missing references)
		//IL_0840: Unknown result type (might be due to invalid IL or missing references)
		//IL_084a: Unknown result type (might be due to invalid IL or missing references)
		//IL_11c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ff9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0da8: Unknown result type (might be due to invalid IL or missing references)
		//IL_0bfd: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a10: Unknown result type (might be due to invalid IL or missing references)
		//IL_11dd: Unknown result type (might be due to invalid IL or missing references)
		//IL_1013: Unknown result type (might be due to invalid IL or missing references)
		//IL_0dc2: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c17: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a2a: Unknown result type (might be due to invalid IL or missing references)
		//IL_11f7: Unknown result type (might be due to invalid IL or missing references)
		//IL_102d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ddc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c31: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a44: Unknown result type (might be due to invalid IL or missing references)
		//IL_1211: Unknown result type (might be due to invalid IL or missing references)
		//IL_1047: Unknown result type (might be due to invalid IL or missing references)
		//IL_0df6: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a5e: Unknown result type (might be due to invalid IL or missing references)
		//IL_122b: Unknown result type (might be due to invalid IL or missing references)
		//IL_105e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e10: Unknown result type (might be due to invalid IL or missing references)
		//IL_1245: Unknown result type (might be due to invalid IL or missing references)
		//IL_1075: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e5c: Unknown result type (might be due to invalid IL or missing references)
		//IL_108b: Unknown result type (might be due to invalid IL or missing references)
		//IL_10a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_1319: Unknown result type (might be due to invalid IL or missing references)
		//IL_1329: Unknown result type (might be due to invalid IL or missing references)
		//IL_1339: Unknown result type (might be due to invalid IL or missing references)
		//IL_1344: Unknown result type (might be due to invalid IL or missing references)
		hit = Physics2D.Linecast(Vector2.op_Implicit(((Component)this).transform.position + new Vector3(0.8f, 0f, 0f)), Vector2.op_Implicit(((Component)this).transform.position + new Vector3(0.8f, -15.5f, 0f)), (1 << LayerMask.NameToLayer("Ground")) | (1 << LayerMask.NameToLayer("Platform")));
		if (RaycastHit2D.op_Implicit(hit))
		{
			senka.position = new Vector3(senka.position.x, ((RaycastHit2D)(ref hit)).point.y - 0.3f, senka.position.z);
			senka.localScale = Vector3.one;
			senka.rotation = Quaternion.Euler(0f, 0f, Mathf.Asin(0f - ((RaycastHit2D)(ref hit)).normal.x) * 180f / (float)Math.PI);
		}
		else
		{
			senka.localScale = Vector3.zero;
		}
		if (measureDistance)
		{
			distance = (int)(((Component)Camera.main).transform.position.x - startPosX) / 4;
			MissionManager.Instance.DistanceEvent(distance);
		}
		if (startSpustanje)
		{
			if (startPenjanje)
			{
				pocetniY_spustanje = Mathf.Lerp(pocetniY_spustanje, cameraTarget.transform.position.y, 0.2f);
				cameraTarget_down.transform.position = new Vector3(cameraTarget.transform.position.x, pocetniY_spustanje, cameraTarget_down.transform.position.z);
				if (cameraTarget.transform.position.y <= cameraTarget_down.transform.position.y)
				{
					cameraFollow.cameraTarget = cameraTarget;
					cameraFollow.transition = false;
				}
			}
			else
			{
				pocetniY_spustanje = Mathf.Lerp(pocetniY_spustanje, cameraTarget_down_y, 0.15f);
				cameraTarget_down.transform.position = new Vector3(cameraTarget.transform.position.x, pocetniY_spustanje, cameraTarget_down.transform.position.z);
				if (cameraTarget.transform.position.y <= cameraTarget_down.transform.position.y)
				{
					cameraFollow.cameraTarget = cameraTarget;
					cameraFollow.transition = false;
				}
			}
		}
		else if (((AnimatorStateInfo)(ref currentBaseState)).nameHash == fall_State && animator.GetBool("Falling"))
		{
			animator.SetBool("Falling", false);
		}
		if (proveraGround == 0)
		{
			grounded = Object.op_Implicit((Object)(object)Physics2D.OverlapCircle(Vector2.op_Implicit(groundCheck.position), groundedRadius, LayerMask.op_Implicit(whatIsGround)));
		}
		else
		{
			proveraGround--;
		}
		triggerCheckDownTrigger = RaycastHit2D.op_Implicit(Physics2D.Linecast(Vector2.op_Implicit(((Component)this).transform.position + new Vector3(0.8f, 2.5f, 0f)), Vector2.op_Implicit(((Component)this).transform.position + new Vector3(0.8f, -4.5f, 0f)), 1 << LayerMask.NameToLayer("Platform")));
		triggerCheckDownBehind = RaycastHit2D.op_Implicit(Physics2D.Linecast(Vector2.op_Implicit(((Component)this).transform.position + new Vector3(-0.8f, 2.5f, 0f)), Vector2.op_Implicit(((Component)this).transform.position + new Vector3(-0.8f, -4.5f, 0f)), 1 << LayerMask.NameToLayer("Platform")));
		if (state == State.jumped || state == State.climbUp)
		{
			Touch touch;
			if (DupliSkok)
			{
				if ((Input.GetMouseButtonDown(0) && Input.mousePosition.x > (float)povrsinaZaClick) || Input.GetKeyDown((KeyCode)32))
				{
					if (mozeDaSkociOpet && hasJumped)
					{
						animator.SetBool("DoubleJump", true);
						if (PlaySounds.soundOn)
						{
							PlaySounds.Stop_Run();
							PlaySounds.Play_Jump();
						}
						doubleJump = true;
						animator.SetBool("Glide", false);
						swoosh = false;
						((Component)this).GetComponent<Rigidbody2D>().drag = 0f;
					}
				}
				else if (Input.touchCount > 1)
				{
					touch = Input.GetTouch(1);
					if ((int)((Touch)(ref touch)).phase == 0 && mozeDaSkociOpet && hasJumped)
					{
						disableGlide = true;
						animator.SetBool("DoubleJump", true);
						doubleJump = true;
						animator.SetBool("Glide", false);
						swoosh = false;
						((Component)this).GetComponent<Rigidbody2D>().drag = 0f;
					}
				}
			}
			if (SlideNaDole || Glide)
			{
				if (Input.GetMouseButtonDown(0) || Input.GetKeyDown((KeyCode)32))
				{
					duzinaPritiskaZaSkok = Time.time;
					startY = Input.mousePosition.y;
					canGlide = true;
					if (mushroomJumped)
					{
						((Component)this).GetComponent<Rigidbody2D>().isKinematic = false;
						mushroomJumped = false;
					}
				}
				else if (Input.GetMouseButtonUp(0) || Input.GetKeyUp((KeyCode)32))
				{
					startY = (endY = 0f);
					((Component)this).GetComponent<Rigidbody2D>().drag = 0f;
					animator.SetBool("Glide", false);
					disableGlide = false;
					canGlide = false;
					if (PlaySounds.Glide_NEW.isPlaying)
					{
						PlaySounds.Stop_Glide();
					}
					if (trail.time > 0f)
					{
						((MonoBehaviour)this).StartCoroutine(nestaniTrail(2f));
					}
				}
				else if (Input.touchCount == 1)
				{
					touch = Input.GetTouch(0);
					if ((int)((Touch)(ref touch)).phase == 3)
					{
						startY = (endY = 0f);
						((Component)this).GetComponent<Rigidbody2D>().drag = 0f;
						animator.SetBool("Glide", false);
						canGlide = false;
						if (PlaySounds.Glide_NEW.isPlaying)
						{
							PlaySounds.Stop_Glide();
						}
						if (trail.time > 0f)
						{
							((MonoBehaviour)this).StartCoroutine(nestaniTrail(2f));
						}
					}
				}
			}
			if (Input.GetMouseButton(0))
			{
				endY = Input.mousePosition.y;
				if (SlideNaDole && startY - endY > 0.125f * Camera.main.orthographicSize * ((float)Screen.height / (2f * Camera.main.orthographicSize)))
				{
					if (!RaycastHit2D.op_Implicit(Physics2D.Linecast(Vector2.op_Implicit(groundCheck.position), Vector2.op_Implicit(groundCheck.position - Vector3.up * 20f), LayerMask.op_Implicit(whatIsGround))))
					{
						powerfullImpact = true;
						((Component)zutiGlowSwooshVisoki).gameObject.SetActive(true);
					}
					swoosh = true;
					isSliding = true;
					animator.Play(swoosh_State);
				}
				if (Glide && canGlide && !disableGlide)
				{
					if (!animator.GetBool("Glide"))
					{
						animator.Play(glide_start_State);
						animator.SetBool("Glide", true);
					}
					((Component)this).GetComponent<Rigidbody2D>().velocity = new Vector2(maxSpeedX, ((Component)this).GetComponent<Rigidbody2D>().velocity.y);
					((Component)this).GetComponent<Rigidbody2D>().drag = 7.5f;
					trail.time = trailTime;
					if (!PlaySounds.Glide_NEW.isPlaying && PlaySounds.soundOn)
					{
						PlaySounds.Play_Glide();
					}
				}
			}
			if (KontrolisaniSkok)
			{
				if (korakce < 1.5f && !pustenVoiceJump)
				{
					pustenVoiceJump = true;
					if (PlaySounds.soundOn)
					{
						PlaySounds.Play_VoiceJump();
					}
				}
				if ((Input.GetMouseButtonUp(0) || Time.time - duzinaPritiskaZaSkok > 1f) && jumpControlled)
				{
					jumpControlled = false;
					jumpHolding = false;
					tempForce = jumpForce;
					canGlide = false;
					if (PlaySounds.Glide_NEW.isPlaying)
					{
						PlaySounds.Stop_Glide();
					}
					if (trail.time > 0f)
					{
						((MonoBehaviour)this).StartCoroutine(nestaniTrail(2f));
					}
				}
			}
			if (trava.isPlaying)
			{
				trava.Stop();
			}
			if (runParticle.isPlaying)
			{
				runParticle.Stop();
			}
		}
		if (state == State.wallhit)
		{
			if (KontrolisaniSkok)
			{
				if ((Input.GetMouseButtonDown(0) && Input.mousePosition.x > (float)povrsinaZaClick) || Input.GetKeyDown((KeyCode)32))
				{
					if (RaycastFunction(Input.mousePosition) != "Pause Button" && RaycastFunction(Input.mousePosition) != "Play Button_Pause" && RaycastFunction(Input.mousePosition) != "Buy Button" && RaycastFunction(Input.mousePosition) != "ButtonMain_NoMoreLives" && RaycastFunction(Input.mousePosition) != "Restart Button_Pause" && !RaycastFunction(Input.mousePosition).Contains("Tutorial") && !RaycastFunction(Input.mousePosition).Contains("Power_") && RaycastFunction(Input.mousePosition) != "Menu Button_Pause")
					{
						duzinaPritiskaZaSkok = Time.time;
						if (!inAir)
						{
							state = State.climbUp;
							if (PlaySounds.soundOn)
							{
								PlaySounds.Stop_Run();
								PlaySounds.Play_Jump();
							}
							jumpControlled = true;
							animator.Play(jump_State);
							animator.SetBool("Landing", false);
							animator.SetBool("WallStop", false);
							inAir = true;
							tempForce = jumpForce;
							particleSkok.Emit(20);
						}
					}
				}
				else if (Input.GetMouseButtonUp(0) && usporavanje && Zaustavljanje)
				{
					usporavanje = false;
					state = State.running;
					maxSpeedX = startSpeedX;
					animator.Play(run_State);
					animator.SetBool("WallStop", false);
				}
			}
			else if (Input.GetMouseButtonDown(0) || Input.GetKeyDown((KeyCode)32))
			{
				if (RaycastFunction(Input.mousePosition) != "Pause Button" && RaycastFunction(Input.mousePosition) != "Play Button_Pause" && RaycastFunction(Input.mousePosition) != "Buy Button" && RaycastFunction(Input.mousePosition) != "ButtonMain_NoMoreLives" && RaycastFunction(Input.mousePosition) != "Restart Button_Pause" && !RaycastFunction(Input.mousePosition).Contains("Tutorial") && !RaycastFunction(Input.mousePosition).Contains("Power_") && RaycastFunction(Input.mousePosition) != "Menu Button_Pause" && !inAir)
				{
					state = State.climbUp;
					jumpSpeedX = 5f;
					if (PlaySounds.soundOn)
					{
						PlaySounds.Stop_Run();
						PlaySounds.Play_Jump();
					}
					jump = true;
					animator.Play(jump_State);
					animator.SetBool("Landing", false);
					animator.SetBool("WallStop", false);
					inAir = true;
					particleSkok.Emit(20);
				}
			}
			else if (Input.GetMouseButtonUp(0) && usporavanje && Zaustavljanje)
			{
				usporavanje = false;
				state = State.running;
				maxSpeedX = startSpeedX;
				animator.Play(run_State);
				animator.SetBool("WallStop", false);
			}
		}
		if (state == State.running)
		{
			if (KontrolisaniSkok)
			{
				if (((Input.GetMouseButtonDown(0) && Input.mousePosition.x > (float)povrsinaZaClick) || Input.GetKeyDown((KeyCode)32)) && RaycastFunction(Input.mousePosition) != "Pause Button" && RaycastFunction(Input.mousePosition) != "Play Button_Pause" && RaycastFunction(Input.mousePosition) != "Buy Button" && RaycastFunction(Input.mousePosition) != "ButtonMain_NoMoreLives" && RaycastFunction(Input.mousePosition) != "Restart Button_Pause" && !RaycastFunction(Input.mousePosition).Contains("Tutorial") && !RaycastFunction(Input.mousePosition).Contains("Power_") && RaycastFunction(Input.mousePosition) != "Menu Button_Pause")
				{
					jumpHolding = true;
					grounded = false;
					proveraGround = 16;
					korakce = 3f;
					((Component)this).GetComponent<Rigidbody2D>().velocity = new Vector2(maxSpeedX, maxSpeedY);
					neTrebaDaProdje = false;
					duzinaPritiskaZaSkok = Time.time;
					state = State.jumped;
					if (PlaySounds.soundOn)
					{
						PlaySounds.Stop_Run();
						PlaySounds.Play_Jump();
						((MonoBehaviour)this).Invoke("PustiVoiceJump", 0.35f);
					}
					jumpControlled = true;
					animator.Play(jump_State);
					animator.SetBool("Landing", false);
					inAir = true;
					particleSkok.Emit(20);
				}
				if ((Input.GetMouseButtonUp(0) || Time.time - duzinaPritiskaZaSkok > 2f) && jumpControlled)
				{
					jumpControlled = false;
					tempForce = jumpForce;
				}
			}
			if (Zaustavljanje && povrsinaZaClick != 0 && Input.GetMouseButton(0))
			{
				usporavanje = true;
				maxSpeedX = 0f;
				state = State.wallhit;
				animator.SetBool("WallStop", true);
			}
			if (!trava.isPlaying)
			{
				trava.Play();
			}
			if (!runParticle.isPlaying)
			{
				runParticle.Play();
			}
		}
		if (state == State.lijana)
		{
			povrsinaZaClick = 0;
			if (Input.GetMouseButtonDown(0) && Input.mousePosition.x > (float)povrsinaZaClick && heCanJump && RaycastFunction(Input.mousePosition) != "Pause Button" && RaycastFunction(Input.mousePosition) != "Play Button_Pause" && RaycastFunction(Input.mousePosition) != "Buy Button" && RaycastFunction(Input.mousePosition) != "ButtonMain_NoMoreLives" && RaycastFunction(Input.mousePosition) != "Restart Button_Pause" && !RaycastFunction(Input.mousePosition).Contains("Tutorial") && !RaycastFunction(Input.mousePosition).Contains("Power_") && RaycastFunction(Input.mousePosition) != "Menu Button_Pause")
			{
				if (Input.mousePosition.x < (float)(Screen.width / 2))
				{
					startY = Input.mousePosition.y;
				}
				else
				{
					animator.Play(jump_State);
					OtkaciMajmuna();
				}
			}
			if (SlideNaDole)
			{
				if (Input.GetMouseButton(0))
				{
					endY = Input.mousePosition.y;
					if (SlideNaDole && startY - endY > 0.125f * Camera.main.orthographicSize * ((float)Screen.height / (2f * Camera.main.orthographicSize)))
					{
						SpustiMajmunaSaLijaneBrzo();
						swoosh = true;
						isSliding = true;
						animator.Play(swoosh_State);
					}
				}
				if (Input.GetMouseButtonUp(0))
				{
					startY = (endY = 0f);
				}
			}
		}
		if (state == State.saZidaNaZid)
		{
			if (Input.GetMouseButtonDown(0) && heCanJump && RaycastFunction(Input.mousePosition) != "Pause Button" && RaycastFunction(Input.mousePosition) != "Play Button_Pause" && RaycastFunction(Input.mousePosition) != "Buy Button" && RaycastFunction(Input.mousePosition) != "ButtonMain_NoMoreLives" && RaycastFunction(Input.mousePosition) != "Restart Button_Pause" && !RaycastFunction(Input.mousePosition).Contains("Tutorial") && !RaycastFunction(Input.mousePosition).Contains("Power_") && RaycastFunction(Input.mousePosition) != "Menu Button_Pause" && !inAir)
			{
				neTrebaDaProdje = false;
				if (PlaySounds.soundOn)
				{
					PlaySounds.Stop_Run();
					PlaySounds.Play_Jump();
				}
				jump = true;
				animator.Play(jump_State);
				animator.SetBool("Landing", false);
				inAir = true;
				if (klizanje.isPlaying)
				{
					klizanje.Stop();
				}
				particleSkok.Emit(20);
				jumpSpeedX = 0f - jumpSpeedX;
				((Component)this).GetComponent<Rigidbody2D>().drag = 0f;
				moveForce = 0f - moveForce;
				maxSpeedX = 0f - maxSpeedX;
				majmun.localScale = new Vector3(majmun.localScale.x, majmun.localScale.y, 0f - majmun.localScale.z);
			}
		}
		else if (state == State.preNegoDaSeOdbije && saZidaNaZid && (Input.GetMouseButtonDown(0) || Input.GetKeyDown((KeyCode)32)))
		{
			jump = true;
			((Component)this).GetComponent<Rigidbody2D>().drag = 0f;
			state = State.saZidaNaZid;
			animator.Play(jump_State);
			animator.SetBool("Landing", false);
			animator.SetBool("WallStop", false);
			particleSkok.Emit(20);
		}
	}

	private void FixedUpdate()
	{
		//IL_0008: Unknown result type (might be due to invalid IL or missing references)
		//IL_000d: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d7: Unknown result type (might be due to invalid IL or missing references)
		//IL_003e: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_01bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0112: Unknown result type (might be due to invalid IL or missing references)
		//IL_0081: Unknown result type (might be due to invalid IL or missing references)
		//IL_0056: Unknown result type (might be due to invalid IL or missing references)
		//IL_0071: Unknown result type (might be due to invalid IL or missing references)
		//IL_0155: Unknown result type (might be due to invalid IL or missing references)
		//IL_012a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0145: Unknown result type (might be due to invalid IL or missing references)
		//IL_00aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_021f: Unknown result type (might be due to invalid IL or missing references)
		//IL_017e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0305: Unknown result type (might be due to invalid IL or missing references)
		//IL_025a: Unknown result type (might be due to invalid IL or missing references)
		//IL_03b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_03c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_0340: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_023f: Unknown result type (might be due to invalid IL or missing references)
		//IL_024a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0405: Unknown result type (might be due to invalid IL or missing references)
		//IL_0420: Unknown result type (might be due to invalid IL or missing references)
		//IL_0325: Unknown result type (might be due to invalid IL or missing references)
		//IL_0330: Unknown result type (might be due to invalid IL or missing references)
		//IL_0291: Unknown result type (might be due to invalid IL or missing references)
		//IL_029b: Unknown result type (might be due to invalid IL or missing references)
		//IL_053c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0557: Unknown result type (might be due to invalid IL or missing references)
		//IL_0458: Unknown result type (might be due to invalid IL or missing references)
		//IL_0473: Unknown result type (might be due to invalid IL or missing references)
		//IL_0377: Unknown result type (might be due to invalid IL or missing references)
		//IL_0381: Unknown result type (might be due to invalid IL or missing references)
		//IL_0582: Unknown result type (might be due to invalid IL or missing references)
		//IL_059e: Unknown result type (might be due to invalid IL or missing references)
		//IL_065d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0667: Unknown result type (might be due to invalid IL or missing references)
		//IL_05da: Unknown result type (might be due to invalid IL or missing references)
		//IL_060e: Unknown result type (might be due to invalid IL or missing references)
		//IL_05fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_0631: Unknown result type (might be due to invalid IL or missing references)
		//IL_0641: Unknown result type (might be due to invalid IL or missing references)
		currentBaseState = animator.GetCurrentAnimatorStateInfo(0);
		if (state == State.saZidaNaZid)
		{
			if (jump)
			{
				hasJumped = true;
				mozeDaSkociOpet = true;
				if (((Component)this).GetComponent<Rigidbody2D>().velocity.y < maxSpeedY)
				{
					((Component)this).GetComponent<Rigidbody2D>().velocity = Vector2.zero;
					((Component)this).GetComponent<Rigidbody2D>().AddForce(new Vector2(maxSpeedX, 2500f));
				}
				if (Mathf.Abs(((Component)this).GetComponent<Rigidbody2D>().velocity.y) > maxSpeedY)
				{
					((Component)this).GetComponent<Rigidbody2D>().velocity = new Vector2(maxSpeedX, maxSpeedY);
				}
				jump = false;
			}
			((Component)this).GetComponent<Rigidbody2D>().velocity = new Vector2(jumpSpeedX, ((Component)this).GetComponent<Rigidbody2D>().velocity.y);
		}
		else if (state == State.preNegoDaSeOdbije)
		{
			if (jump)
			{
				hasJumped = true;
				mozeDaSkociOpet = true;
				if (((Component)this).GetComponent<Rigidbody2D>().velocity.y < maxSpeedY)
				{
					((Component)this).GetComponent<Rigidbody2D>().velocity = Vector2.zero;
					((Component)this).GetComponent<Rigidbody2D>().AddForce(new Vector2(maxSpeedX, 2500f));
				}
				if (Mathf.Abs(((Component)this).GetComponent<Rigidbody2D>().velocity.y) > maxSpeedY)
				{
					((Component)this).GetComponent<Rigidbody2D>().velocity = new Vector2(maxSpeedX, maxSpeedY);
				}
				jump = false;
			}
			((Component)this).GetComponent<Rigidbody2D>().velocity = new Vector2(jumpSpeedX, ((Component)this).GetComponent<Rigidbody2D>().velocity.y);
			if (((Component)this).GetComponent<Rigidbody2D>().velocity.y < -0.01f)
			{
				((Component)this).GetComponent<Rigidbody2D>().drag = 5f;
			}
		}
		else if (state == State.wasted && measureDistance)
		{
			measureDistance = false;
		}
		if (state == State.running)
		{
			if (!PlaySounds.Run.isPlaying && PlaySounds.soundOn)
			{
				PlaySounds.Play_Run();
			}
			if (((Component)this).GetComponent<Rigidbody2D>().velocity.x < maxSpeedX && !stop)
			{
				((Component)this).GetComponent<Rigidbody2D>().AddForce(Vector2.right * moveForce);
			}
			if (Mathf.Abs(((Component)this).GetComponent<Rigidbody2D>().velocity.x) > maxSpeedX && !stop)
			{
				((Component)this).GetComponent<Rigidbody2D>().velocity = new Vector2(maxSpeedX, ((Component)this).GetComponent<Rigidbody2D>().velocity.y);
			}
		}
		else if (state == State.completed)
		{
			if (dancing)
			{
				proveraGround = 0;
				if (grounded)
				{
					((Component)this).GetComponent<Rigidbody2D>().velocity = new Vector2(0f, 0f);
				}
				else
				{
					((Component)this).GetComponent<Rigidbody2D>().velocity = new Vector2(0f, -30f);
				}
				return;
			}
			if (((Component)this).GetComponent<Rigidbody2D>().velocity.x < maxSpeedX && !stop)
			{
				((Component)this).GetComponent<Rigidbody2D>().AddForce(Vector2.right * moveForce);
			}
			if (Mathf.Abs(((Component)this).GetComponent<Rigidbody2D>().velocity.x) > maxSpeedX && !stop)
			{
				((Component)this).GetComponent<Rigidbody2D>().velocity = new Vector2(maxSpeedX, ((Component)this).GetComponent<Rigidbody2D>().velocity.y);
			}
		}
		else if (state == State.jumped)
		{
			if (jumpHolding)
			{
				((Component)this).GetComponent<Rigidbody2D>().velocity = new Vector2(maxSpeedX, ((Component)this).GetComponent<Rigidbody2D>().velocity.y + korakce);
				if (korakce > 0f)
				{
					korakce -= 0.085f;
				}
			}
			if (swoosh)
			{
				((Component)this).GetComponent<Rigidbody2D>().velocity = new Vector2(maxSpeedX, 0f);
				((Component)this).GetComponent<Rigidbody2D>().AddForce(new Vector2(maxSpeedX, -4500f));
				swoosh = false;
			}
			if (doubleJump)
			{
				dupliSkokOblaci.Emit(25);
				jumpSpeedX = startJumpSpeedX;
				((Component)this).GetComponent<Rigidbody2D>().velocity = Vector2.zero;
				((Component)this).GetComponent<Rigidbody2D>().AddForce(new Vector2(2000f, doubleJumpForce));
				doubleJump = false;
				hasJumped = false;
			}
			if (powerfullImpact)
			{
				if (!PlaySounds.Glide_NEW.isPlaying && PlaySounds.soundOn)
				{
					PlaySounds.Play_Glide();
				}
			}
			else if (jumpControlled)
			{
				hasJumped = true;
				mozeDaSkociOpet = true;
			}
		}
		else if (state == State.wallhit)
		{
			if (trava.isPlaying)
			{
				trava.Stop();
			}
			if (runParticle.isPlaying)
			{
				runParticle.Stop();
			}
		}
		else
		{
			if (state != State.climbUp)
			{
				return;
			}
			if (doubleJump)
			{
				dupliSkokOblaci.Emit(25);
				jumpSpeedX = startJumpSpeedX;
				((Component)this).GetComponent<Rigidbody2D>().velocity = Vector2.zero;
				((Component)this).GetComponent<Rigidbody2D>().AddForce(new Vector2(2000f, doubleJumpForce));
				doubleJump = false;
				hasJumped = false;
			}
			else if (jump)
			{
				((Component)this).GetComponent<Rigidbody2D>().velocity = Vector2.zero;
				((Component)this).GetComponent<Rigidbody2D>().AddForce(new Vector2(maxSpeedX, jumpForce));
				hasJumped = true;
				jump = false;
			}
			else if (jumpControlled)
			{
				hasJumped = true;
				mozeDaSkociOpet = true;
				if (((Component)this).GetComponent<Rigidbody2D>().velocity.y < maxSpeedY)
				{
					((Component)this).GetComponent<Rigidbody2D>().AddForce(new Vector2(maxSpeedX, tempForce));
				}
				if (Mathf.Abs(((Component)this).GetComponent<Rigidbody2D>().velocity.y) > maxSpeedY)
				{
					((Component)this).GetComponent<Rigidbody2D>().velocity = new Vector2(((Component)this).GetComponent<Rigidbody2D>().velocity.x, maxSpeedY);
				}
			}
			((Component)this).GetComponent<Rigidbody2D>().velocity = new Vector2(jumpSpeedX, ((Component)this).GetComponent<Rigidbody2D>().velocity.y);
		}
	}

	private void OnTriggerExit2D(Collider2D col)
	{
		//IL_0061: Unknown result type (might be due to invalid IL or missing references)
		//IL_0033: Unknown result type (might be due to invalid IL or missing references)
		//IL_004e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0081: Unknown result type (might be due to invalid IL or missing references)
		//IL_009a: Unknown result type (might be due to invalid IL or missing references)
		if (((Component)col).tag == "Footer")
		{
			float num;
			float y;
			if (((Component)col).transform.childCount > 0)
			{
				num = ((Component)col).transform.Find("TriggerPositionUp").position.y;
				y = ((Component)col).transform.Find("TriggerPositionDown").position.y;
			}
			else
			{
				num = (y = ((Component)col).transform.position.y);
			}
			if (((Component)this).GetComponent<Collider2D>().isTrigger && (groundCheck.position.y + 0.2f > num || ceilingCheck.position.y < y))
			{
				((Component)this).GetComponent<Collider2D>().isTrigger = false;
				neTrebaDaProdje = false;
			}
		}
	}

	private void OnCollisionEnter2D(Collision2D col)
	{
		//IL_0279: Unknown result type (might be due to invalid IL or missing references)
		//IL_0243: Unknown result type (might be due to invalid IL or missing references)
		//IL_02cf: Unknown result type (might be due to invalid IL or missing references)
		//IL_02df: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ef: Unknown result type (might be due to invalid IL or missing references)
		//IL_02fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_04c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_0802: Unknown result type (might be due to invalid IL or missing references)
		if (state == State.completed && inAir)
		{
			if (powerfullImpact)
			{
				((Component)((Component)this).transform.Find("Impact")).gameObject.SetActive(true);
				((MonoBehaviour)this).Invoke("DisableImpact", 0.25f);
				powerfullImpact = false;
				if (PlaySounds.Glide_NEW.isPlaying)
				{
					PlaySounds.Stop_Glide();
				}
				((Component)zutiGlowSwooshVisoki).gameObject.SetActive(false);
				((Component)Camera.main).GetComponent<Animator>().Play("CameraShakeTrasGround");
				izrazitiPad.Play();
				animator.Play("Landing");
			}
			else
			{
				animator.SetBool("Landing", true);
				grounded = true;
				oblak.Play();
			}
			inAir = false;
			if (isSliding)
			{
				isSliding = false;
			}
			Finish();
		}
		else
		{
			if (state == State.completed && state == State.wasted)
			{
				return;
			}
			if (col.gameObject.tag == "ZidZaOdbijanje")
			{
				if (state == State.running && CheckWallHitNear)
				{
					if (klizanje.isPlaying)
					{
						klizanje.Stop();
					}
					animator.SetBool("WallStop", true);
					animator.Play(wall_stop_State);
					state = State.preNegoDaSeOdbije;
					if (trava.isPlaying)
					{
						trava.Stop();
					}
					if (runParticle.isPlaying)
					{
						runParticle.Stop();
					}
				}
				else if (state != State.preNegoDaSeOdbije)
				{
					inAir = false;
					heCanJump = true;
					((Component)this).GetComponent<Rigidbody2D>().drag = 5f;
					klizanje.Play();
					animator.Play("Klizanje");
					state = State.saZidaNaZid;
				}
				wallHitGlide = true;
			}
			else if (col.gameObject.tag == "Footer")
			{
				startPenjanje = false;
				startSpustanje = false;
				if ((Object)(object)cameraTarget_down.transform.parent == (Object)null)
				{
					cameraTarget_down.transform.position = cameraTarget.transform.position;
					cameraTarget_down.transform.parent = ((Component)this).transform;
				}
				cameraTarget_down.transform.position = cameraTarget.transform.position;
				if (state == State.saZidaNaZid)
				{
					moveForce = Mathf.Abs(moveForce);
					jumpSpeedX = Mathf.Abs(jumpSpeedX);
					maxSpeedX = Mathf.Abs(maxSpeedX);
					majmun.localScale = new Vector3(majmun.localScale.x, majmun.localScale.y, Mathf.Abs(majmun.localScale.z));
					if (klizanje.isPlaying)
					{
						klizanje.Stop();
					}
					if (CheckWallHitNear || CheckWallHitNear_low)
					{
						animator.SetBool("WallStop", true);
						animator.Play(wall_stop_from_jump_State);
						state = State.preNegoDaSeOdbije;
						if (trava.isPlaying)
						{
							trava.Stop();
						}
					}
					else if (!CheckWallHitNear && !CheckWallHitNear_low)
					{
						mozeDaSkociOpet = false;
						animator.SetBool("Jump", false);
						animator.SetBool("DoubleJump", false);
						animator.SetBool("Glide", false);
						disableGlide = false;
						animator.Play(run_State);
						((Component)this).GetComponent<Rigidbody2D>().drag = 0f;
						state = State.running;
						canGlide = false;
						if (PlaySounds.Glide_NEW.isPlaying)
						{
							PlaySounds.Stop_Glide();
						}
						if (trail.time > 0f)
						{
							((MonoBehaviour)this).StartCoroutine(nestaniTrail(2f));
						}
						if (PlaySounds.soundOn)
						{
							PlaySounds.Play_Run();
						}
						hasJumped = false;
						startY = (endY = 0f);
						inAir = false;
					}
				}
				if (state == State.jumped)
				{
					oblak.Play();
					if (proveraGround < 12)
					{
						if (startSpustanje)
						{
							startSpustanje = false;
							cameraTarget_down.transform.parent = ((Component)this).transform;
							cameraTarget_down.transform.position = cameraTarget.transform.position;
						}
						if (powerfullImpact)
						{
							((Component)((Component)this).transform.Find("Impact")).gameObject.SetActive(true);
							((MonoBehaviour)this).Invoke("DisableImpact", 0.25f);
							powerfullImpact = false;
							if (PlaySounds.Glide_NEW.isPlaying)
							{
								PlaySounds.Stop_Glide();
							}
							((Component)zutiGlowSwooshVisoki).gameObject.SetActive(false);
							((Component)Camera.main).GetComponent<Animator>().Play("CameraShakeTrasGround");
							izrazitiPad.Play();
							if (PlaySounds.soundOn)
							{
								PlaySounds.Play_Landing_Strong();
							}
						}
						else if (PlaySounds.soundOn)
						{
							PlaySounds.Play_Landing();
						}
						if (lijana)
						{
							lijana = false;
						}
						jumpSpeedX = startJumpSpeedX;
						mozeDaSkociOpet = false;
						animator.SetBool("Jump", false);
						animator.SetBool("DoubleJump", false);
						animator.SetBool("Glide", false);
						disableGlide = false;
						animator.SetBool("Landing", true);
						((Component)this).GetComponent<Rigidbody2D>().drag = 0f;
						state = State.running;
						grounded = true;
						canGlide = false;
						if (PlaySounds.Glide_NEW.isPlaying)
						{
							PlaySounds.Stop_Glide();
						}
						if (trail.time > 0f)
						{
							((MonoBehaviour)this).StartCoroutine(nestaniTrail(2f));
						}
						if (PlaySounds.soundOn)
						{
							PlaySounds.Play_Run();
						}
						hasJumped = false;
						startY = (endY = 0f);
						inAir = false;
						if (isSliding)
						{
							isSliding = false;
						}
					}
				}
				if (state == State.wasted && utepanULetu)
				{
					if (magnet)
					{
						manage.ApplyPowerUp(-1);
					}
					if (doublecoins)
					{
						manage.ApplyPowerUp(-2);
					}
					((MonoBehaviour)this).StartCoroutine(AfterFallDown());
				}
			}
			else if (col.gameObject.tag == "Enemy")
			{
				if (activeShield)
				{
					if (PlaySounds.soundOn)
					{
						PlaySounds.Play_LooseShield();
					}
					enemyCollider = ((Component)col.transform).GetComponent<Collider2D>();
					((Behaviour)enemyCollider).enabled = false;
					((MonoBehaviour)this).Invoke("EnableColliderBackOnEnemy", 1f);
					activeShield = false;
					((Component)((Component)this).transform.Find("Particles/ShieldDestroyParticle")).GetComponent<ParticleSystem>().Play();
					((Component)((Component)this).transform.Find("Particles/ShieldDestroyParticle").GetChild(0)).GetComponent<ParticleSystem>().Play();
					((Component)manage).SendMessage("ApplyPowerUp", (object)(-3));
					_ = state;
				}
				else
				{
					if (killed || invincible)
					{
						return;
					}
					if (((Object)col.gameObject).name.Contains("Biljka"))
					{
						if (PlaySounds.soundOn)
						{
							PlaySounds.Play_BiljkaUgriz();
						}
						((Component)col.transform.Find("Biljka_mesozder")).GetComponent<Animator>().Play("Attack");
					}
					else if (PlaySounds.soundOn)
					{
						PlaySounds.Play_Siljci();
					}
					((Component)((Component)this).transform.Find("Particles/OblakKill")).GetComponent<ParticleSystem>().Play();
					if (state == State.running)
					{
						((Component)this).GetComponent<Rigidbody2D>().velocity = Vector2.zero;
						killed = true;
						oblak.Play();
						majmunUtepan();
					}
					else
					{
						majmunUtepanULetu();
					}
				}
			}
			else if (col.gameObject.tag.Equals("WallHit"))
			{
				wallHitGlide = true;
			}
		}
	}

	private void EnableColliderBackOnEnemy()
	{
		((Behaviour)enemyCollider).enabled = true;
		enemyCollider = null;
	}

	public void majmunUtepanULetu()
	{
		((MonoBehaviour)this).StartCoroutine(FallDownAfterSpikes());
	}

	private IEnumerator FallDownAfterSpikes()
	{
		if (((Component)this).GetComponent<Rigidbody2D>().drag != 0f)
		{
			((Component)this).GetComponent<Rigidbody2D>().drag = 0f;
			canGlide = false;
			if (PlaySounds.Glide_NEW.isPlaying)
			{
				PlaySounds.Stop_Glide();
			}
			if (trail.time != 0f)
			{
				((MonoBehaviour)this).StartCoroutine(nestaniTrail(2f));
			}
		}
		if (powerfullImpact)
		{
			powerfullImpact = false;
			if (PlaySounds.Glide_NEW.isPlaying)
			{
				PlaySounds.Stop_Glide();
			}
			((Component)zutiGlowSwooshVisoki).gameObject.SetActive(false);
		}
		((Component)majmun).GetComponent<Animator>().speed = 1.5f;
		((Component)this).gameObject.layer = LayerMask.NameToLayer("MonkeyShadow");
		utepanULetu = true;
		canRespawnThings = false;
		killed = true;
		oblak.Play();
		animator.Play(spikedeath_State);
		state = State.wasted;
		((Component)((Component)manage).transform.Find("Gameplay Scena Interface/_TopRight/Pause Button")).GetComponent<Collider>().enabled = false;
		if (trava.isPlaying)
		{
			trava.Stop();
		}
		if (runParticle.isPlaying)
		{
			runParticle.Stop();
		}
		((Component)this).GetComponent<Rigidbody2D>().velocity = Vector2.zero;
		((Component)this).GetComponent<Rigidbody2D>().velocity = new Vector2(-8f, 30f);
		yield return (object)new WaitForSeconds(1f);
	}

	private IEnumerator AfterFallDown()
	{
		utepanULetu = false;
		animator.SetTrigger("ToLand");
		((Component)this).GetComponent<Rigidbody2D>().velocity = new Vector2(-4f, 22f);
		yield return (object)new WaitForSeconds(0.65f);
		while (!grounded)
		{
			yield return null;
		}
		((Component)this).GetComponent<Rigidbody2D>().isKinematic = true;
		yield return (object)new WaitForSeconds(0.35f);
		((Component)this).GetComponent<Rigidbody2D>().velocity = Vector2.zero;
		if (manage.keepPlayingCount >= 1)
		{
			((Component)manage).SendMessage("showFailedScreen");
		}
		else
		{
			((Component)manage).SendMessage("ShowKeepPlayingScreen");
		}
		cameraFollow.stopFollow = true;
		if (!invincible)
		{
			((Component)((Component)this).transform.Find("Particles/HolderKillStars")).gameObject.SetActive(true);
		}
	}

	private void OnCollisionExit2D(Collision2D col)
	{
		//IL_0032: Unknown result type (might be due to invalid IL or missing references)
		//IL_0046: Unknown result type (might be due to invalid IL or missing references)
		//IL_004b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0050: Unknown result type (might be due to invalid IL or missing references)
		//IL_005b: Unknown result type (might be due to invalid IL or missing references)
		//IL_006f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0074: Unknown result type (might be due to invalid IL or missing references)
		//IL_0079: Unknown result type (might be due to invalid IL or missing references)
		//IL_009d: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00db: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ef: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f9: Unknown result type (might be due to invalid IL or missing references)
		//IL_011d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0173: Unknown result type (might be due to invalid IL or missing references)
		//IL_0187: Unknown result type (might be due to invalid IL or missing references)
		//IL_018c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0191: Unknown result type (might be due to invalid IL or missing references)
		//IL_019c: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e4: Unknown result type (might be due to invalid IL or missing references)
		//IL_0217: Unknown result type (might be due to invalid IL or missing references)
		//IL_022d: Unknown result type (might be due to invalid IL or missing references)
		if (col.gameObject.tag == "Footer")
		{
			if (state != 0)
			{
				return;
			}
			neTrebaDaProdje = false;
			if (!RaycastHit2D.op_Implicit(Physics2D.Linecast(Vector2.op_Implicit(((Component)this).transform.position + new Vector3(4.4f, 1.2f, 0f)), Vector2.op_Implicit(((Component)this).transform.position + new Vector3(4.4f, -3.2f, 0f)), (1 << LayerMask.NameToLayer("Ground")) | (1 << LayerMask.NameToLayer("Platform")))) && !RaycastHit2D.op_Implicit(Physics2D.Linecast(Vector2.op_Implicit(((Component)this).transform.position + new Vector3(0.2f, 0.1f, 0f)), Vector2.op_Implicit(((Component)this).transform.position + new Vector3(0.2f, -0.65f, 0f)), (1 << LayerMask.NameToLayer("Ground")) | (1 << LayerMask.NameToLayer("Platform")))))
			{
				state = State.jumped;
				animator.SetBool("Landing", false);
				animator.Play(fall_State);
				if (runParticle.isPlaying)
				{
					runParticle.Stop();
				}
				if (!RaycastHit2D.op_Implicit(Physics2D.Linecast(Vector2.op_Implicit(((Component)this).transform.position + new Vector3(2.3f, 1.25f, 0f)), Vector2.op_Implicit(((Component)this).transform.position + new Vector3(2.3f, 0f - Camera.main.orthographicSize, 0f)), (1 << LayerMask.NameToLayer("Ground")) | (1 << LayerMask.NameToLayer("Platform")))))
				{
					startSpustanje = true;
					cameraTarget_down.transform.parent = null;
					pocetniY_spustanje = cameraTarget.transform.position.y;
					cameraTarget_down_y = ((Component)this).transform.position.y - 7.5f;
					cameraFollow.cameraTarget = cameraTarget_down;
				}
			}
		}
		else if (col.gameObject.tag == "ZidZaOdbijanje")
		{
			if (klizanje.isPlaying)
			{
				klizanje.Stop();
			}
			((Component)this).GetComponent<Rigidbody2D>().drag = 0f;
			if (trava.isPlaying)
			{
				trava.Stop();
			}
			if (state != State.wasted)
			{
				animator.Play(fall_State);
			}
			animator.SetBool("Landing", false);
			wallHitGlide = false;
		}
		else if (col.gameObject.tag.Equals("WallHit"))
		{
			wallHitGlide = false;
		}
	}

	private void NotifyManagerForFinish()
	{
		((Component)manage).SendMessage("ShowWinScreen");
	}

	private IEnumerator TutorialPlay(Transform obj, string ime, int next)
	{
		((MonoBehaviour)this).StartCoroutine(((Component)obj).GetComponent<Animation>().Play(ime, useTimeScale: false, delegate
		{
			helpBool = true;
		}));
		while (!helpBool)
		{
			yield return null;
		}
		helpBool = false;
		switch (next)
		{
		case 1:
			((MonoBehaviour)this).StartCoroutine(TutorialPlay(obj, "TutorialIdle1_A", -1));
			break;
		case 2:
			((MonoBehaviour)this).StartCoroutine(TutorialPlay(obj, "TutorialIdle2_A", -1));
			break;
		}
	}

	private void OnTriggerEnter2D(Collider2D col)
	{
		//IL_06ff: Unknown result type (might be due to invalid IL or missing references)
		//IL_06ec: Unknown result type (might be due to invalid IL or missing references)
		//IL_0710: Unknown result type (might be due to invalid IL or missing references)
		//IL_08b9: Unknown result type (might be due to invalid IL or missing references)
		if (state == State.completed && state == State.wasted)
		{
			return;
		}
		if (((Object)col).name == "Magnet_collect")
		{
			if (PlaySounds.soundOn)
			{
				PlaySounds.Play_CollectPowerUp();
			}
			LevelFactory.instance.magnetCollected = true;
			magnet = true;
			((Component)manage).SendMessage("ApplyPowerUp", (object)1);
			((Component)col).gameObject.SetActive(false);
			collectItem.Play();
			((MonoBehaviour)this).StartCoroutine(reappearItem(((Component)col).gameObject));
		}
		else if (((Object)col).name == "BananaCoinX2_collect")
		{
			if (PlaySounds.soundOn)
			{
				PlaySounds.Play_CollectPowerUp();
			}
			doublecoins = true;
			((Component)manage).SendMessage("ApplyPowerUp", (object)2);
			((Component)col).gameObject.SetActive(false);
			collectItem.Play();
			((MonoBehaviour)this).StartCoroutine(reappearItem(((Component)col).gameObject));
		}
		else if (((Object)col).name == "Shield_collect")
		{
			if (PlaySounds.soundOn)
			{
				PlaySounds.Play_CollectPowerUp();
			}
			LevelFactory.instance.shieldCollected = true;
			((Component)manage).SendMessage("ApplyPowerUp", (object)3);
			((Component)col).gameObject.SetActive(false);
			activeShield = true;
			collectItem.Play();
			((MonoBehaviour)this).StartCoroutine(reappearItem(((Component)col).gameObject));
		}
		else if (((Object)col).name == "Banana_collect")
		{
			if (PlaySounds.soundOn)
			{
				PlaySounds.Play_CollectBanana();
			}
			((Component)col).gameObject.SetActive(false);
			Manage.points += 200;
			Manage.pointsText.text = Manage.points.ToString();
			Manage.pointsEffects.RefreshTextOutline(adjustTextSize: false, hasWhiteSpaces: true);
			collectItem.Play();
			((MonoBehaviour)this).StartCoroutine(reappearItem(((Component)col).gameObject));
			StagesParser.currentBananas++;
			PlayerPrefs.SetInt("TotalBananas", StagesParser.currentBananas);
			PlayerPrefs.Save();
		}
		else if (((Object)col).name == "Srce_collect")
		{
			((Component)col).gameObject.SetActive(false);
			GameObject.Find("LifeManager").SendMessage("AddLife");
			((MonoBehaviour)this).StartCoroutine(reappearItem(((Component)col).gameObject));
		}
		else if (((Object)col).name.Contains("Diamond_collect"))
		{
			if (PlaySounds.soundOn)
			{
				PlaySounds.Play_CollectDiamond();
			}
			((Component)col).gameObject.SetActive(false);
			Manage.points += 50;
			Manage.pointsText.text = Manage.points.ToString();
			Manage.pointsEffects.RefreshTextOutline(adjustTextSize: false, hasWhiteSpaces: true);
			collectItem.Play();
			((MonoBehaviour)this).StartCoroutine(reappearItem(((Component)col).gameObject));
			if (((Object)col).name.Contains("Red"))
			{
				Manage.redDiamonds++;
				MissionManager.Instance.RedDiamondEvent(Manage.redDiamonds);
			}
			else if (((Object)col).name.Contains("Blue"))
			{
				Manage.blueDiamonds++;
				MissionManager.Instance.BlueDiamondEvent(Manage.blueDiamonds);
			}
			else if (((Object)col).name.Contains("Green"))
			{
				Manage.greenDiamonds++;
				MissionManager.Instance.GreenDiamondEvent(Manage.greenDiamonds);
			}
			MissionManager.Instance.DiamondEvent(Manage.redDiamonds + Manage.greenDiamonds + Manage.blueDiamonds);
		}
		else if (((Object)col).name == "BananaFog")
		{
			((Component)((Component)col).transform.GetChild(0)).GetComponent<Renderer>().enabled = false;
			((Component)((Component)Camera.main).transform.Find("FogOfWar")).GetComponent<Renderer>().enabled = true;
			((MonoBehaviour)this).StartCoroutine(pojaviMaglu(col));
		}
		else if (((Object)col).name.Contains("CoinsBagBig"))
		{
			TextMesh component = ((Component)((Component)col).transform.Find("+3CoinsHolder/+3Coins")).GetComponent<TextMesh>();
			string text2 = (((Component)((Component)((Component)col).transform).transform.Find("+3CoinsHolder/+3Coins/+3CoinsShadow")).GetComponent<TextMesh>().text = "+500");
			component.text = text2;
			((Component)((Component)col).transform.Find("+3CoinsHolder")).GetComponent<Animator>().Play("FadeOutCoins");
			((Component)((Component)col).transform.Find("AnimationHolder")).GetComponent<Animation>().Play("CoinsBagCollect");
			Manage.coinsCollected += 500;
			Manage.Instance.coinsCollectedText.text = Manage.coinsCollected.ToString();
			((Component)Manage.Instance.coinsCollectedText).GetComponent<TextMeshEffects>().RefreshTextOutline(adjustTextSize: false, hasWhiteSpaces: true);
			collectItem.Play();
		}
		else if (((Object)col).name.Contains("CoinsBagMedium"))
		{
			TextMesh component2 = ((Component)((Component)col).transform.Find("+3CoinsHolder/+3Coins")).GetComponent<TextMesh>();
			string text2 = (((Component)((Component)((Component)col).transform).transform.Find("+3CoinsHolder/+3Coins/+3CoinsShadow")).GetComponent<TextMesh>().text = "+250");
			component2.text = text2;
			((Component)((Component)col).transform.Find("+3CoinsHolder")).GetComponent<Animator>().Play("FadeOutCoins");
			((Component)((Component)col).transform.Find("AnimationHolder")).GetComponent<Animation>().Play("CoinsBagCollect");
			Manage.coinsCollected += 250;
			Manage.Instance.coinsCollectedText.text = Manage.coinsCollected.ToString();
			((Component)Manage.Instance.coinsCollectedText).GetComponent<TextMeshEffects>().RefreshTextOutline(adjustTextSize: false, hasWhiteSpaces: true);
			collectItem.Play();
		}
		else if (((Object)col).name.Contains("CoinsBagSmall"))
		{
			TextMesh component3 = ((Component)((Component)col).transform.Find("+3CoinsHolder/+3Coins")).GetComponent<TextMesh>();
			string text2 = (((Component)((Component)((Component)col).transform).transform.Find("+3CoinsHolder/+3Coins/+3CoinsShadow")).GetComponent<TextMesh>().text = "+100");
			component3.text = text2;
			((Component)((Component)col).transform.Find("+3CoinsHolder")).GetComponent<Animator>().Play("FadeOutCoins");
			((Component)((Component)col).transform.Find("AnimationHolder")).GetComponent<Animation>().Play("CoinsBagCollect");
			Manage.coinsCollected += 100;
			Manage.Instance.coinsCollectedText.text = Manage.coinsCollected.ToString();
			((Component)Manage.Instance.coinsCollectedText).GetComponent<TextMeshEffects>().RefreshTextOutline(adjustTextSize: false, hasWhiteSpaces: true);
			collectItem.Play();
		}
		if (((Component)col).tag == "Finish")
		{
			((Behaviour)((Component)col).GetComponent<Collider2D>()).enabled = false;
			Finish();
		}
		else if (((Component)col).tag == "Footer" && ((Component)col).gameObject.layer == LayerMask.NameToLayer("Platform"))
		{
			float num = ((((Component)col).transform.childCount <= 0) ? ((Component)col).transform.position.y : ((Component)col).transform.Find("TriggerPositionUp").position.y);
			if (((Component)this).transform.position.y + 0.25f > num && triggerCheckDownTrigger && triggerCheckDownBehind && ((Component)this).GetComponent<Collider2D>().isTrigger)
			{
				((Component)this).GetComponent<Collider2D>().isTrigger = false;
			}
		}
		else if (((Component)col).tag == "Enemy")
		{
			if (activeShield)
			{
				if (PlaySounds.soundOn)
				{
					PlaySounds.Play_LooseShield();
				}
				enemyCollider = ((Component)((Component)col).transform).GetComponent<Collider2D>();
				((Behaviour)enemyCollider).enabled = false;
				((MonoBehaviour)this).Invoke("EnableColliderBackOnEnemy", 1f);
				activeShield = false;
				((Component)((Component)this).transform.Find("Particles/ShieldDestroyParticle")).GetComponent<ParticleSystem>().Play();
				((Component)((Component)this).transform.Find("Particles/ShieldDestroyParticle").GetChild(0)).GetComponent<ParticleSystem>().Play();
				((Component)manage).SendMessage("ApplyPowerUp", (object)(-3));
				if (((Object)col).name.Equals("Koplje"))
				{
					((Component)col).GetComponent<DestroySpearGorilla>().DestroyGorilla();
				}
			}
			else
			{
				if (killed || invincible)
				{
					return;
				}
				if (((Object)((Component)col).gameObject).name.Contains("Biljka"))
				{
					if (PlaySounds.soundOn)
					{
						PlaySounds.Play_BiljkaUgriz();
					}
					((Component)((Component)col).transform.Find("Biljka_mesozder")).GetComponent<Animator>().Play("Attack");
				}
				else if (PlaySounds.soundOn)
				{
					PlaySounds.Play_Siljci();
				}
				((Component)((Component)this).transform.Find("Particles/OblakKill")).GetComponent<ParticleSystem>().Play();
				if (state == State.running)
				{
					((Component)this).GetComponent<Rigidbody2D>().velocity = Vector2.zero;
					killed = true;
					oblak.Play();
					majmunUtepan();
				}
				else
				{
					((MonoBehaviour)this).StartCoroutine(FallDownAfterSpikes());
				}
			}
		}
		else if (((Component)col).tag == "0_Plan")
		{
			((Behaviour)((Component)col).GetComponent<RunWithSpeed>()).enabled = true;
		}
		else if (((Component)col).tag == "SaZidaNaZid")
		{
			if (saZidaNaZid)
			{
				saZidaNaZid = false;
				state = State.jumped;
			}
			else
			{
				saZidaNaZid = true;
			}
		}
		else
		{
			if (!(((Component)col).tag == "Lijana"))
			{
				return;
			}
			if (((Component)this).GetComponent<Rigidbody2D>().drag != 0f)
			{
				((Component)this).GetComponent<Rigidbody2D>().drag = 0f;
				canGlide = false;
				if (PlaySounds.Glide_NEW.isPlaying)
				{
					PlaySounds.Stop_Glide();
				}
				if (trail.time != 0f)
				{
					((MonoBehaviour)this).StartCoroutine(nestaniTrail(2f));
				}
			}
			if (powerfullImpact)
			{
				powerfullImpact = false;
				if (PlaySounds.Glide_NEW.isPlaying)
				{
					PlaySounds.Stop_Glide();
				}
				((Component)zutiGlowSwooshVisoki).gameObject.SetActive(false);
			}
			animator.SetBool("LianaLetGo", false);
			grabLianaTransform = ((Component)col).GetComponent<LianaAnimationEvent>().lijanaTarget;
			if (!lijana)
			{
				lijana = true;
				animator.Play(lijana_State);
			}
			else
			{
				animator.Play("Lijana_mirror");
				lijana = false;
			}
			state = State.lijana;
			((Behaviour)col).enabled = false;
			((Component)this).GetComponent<Rigidbody2D>().isKinematic = true;
			maxSpeedX = 0f;
			jumpSpeedX = 0f;
			((Component)((Component)col).transform.GetChild(0)).GetComponent<Animator>().Play("Glide_liana");
			((MonoBehaviour)this).Invoke("OtkaciMajmuna", 0.6f);
			((MonoBehaviour)this).StartCoroutine("pratiLijanaTarget", (object)grabLianaTransform);
		}
	}

	public void Finish()
	{
		canRespawnThings = false;
		state = State.completed;
		if (!inAir)
		{
			if (trail.time != 0f)
			{
				((MonoBehaviour)this).StartCoroutine(nestaniTrail(2f));
			}
			cameraFollow.cameraFollowX = false;
			((MonoBehaviour)this).StartCoroutine(ReduceMaxSpeedGradually());
			((Component)((Component)manage).transform.Find("Gameplay Scena Interface/_TopRight/Pause Button")).GetComponent<Collider>().enabled = false;
			((Component)this).gameObject.layer = LayerMask.NameToLayer("MonkeyShadow");
			((MonoBehaviour)this).Invoke("TurnOnKinematic", 8f);
		}
		else if (canGlide)
		{
			((Component)this).GetComponent<Rigidbody2D>().drag = 0f;
			animator.SetBool("Glide", false);
			disableGlide = false;
			canGlide = false;
			if (PlaySounds.Glide_NEW.isPlaying)
			{
				PlaySounds.Stop_Glide();
			}
		}
	}

	private IEnumerator ReduceMaxSpeedGradually()
	{
		while (maxSpeedX > 0.1f)
		{
			yield return null;
			maxSpeedX = Mathf.Lerp(maxSpeedX, 0f, 0.2f);
		}
		maxSpeedX = 0f;
		dancing = true;
		((Component)this).GetComponent<Collider2D>().sharedMaterial = finishDontMove;
		if (((AnimatorStateInfo)(ref currentBaseState)).nameHash == run_State)
		{
			runParticle.Stop();
			trava.Stop();
			animator.SetTrigger("Finished");
		}
		else
		{
			animator.Play("Dancing");
		}
		((MonoBehaviour)this).Invoke("RestoreMaxSpeed", 2.15f);
	}

	private void RestoreMaxSpeed()
	{
		dancing = false;
		((Component)this).GetComponent<Rigidbody2D>().mass = 1.25f;
		animator.SetTrigger("DancingDone");
		maxSpeedX = 16f;
		((MonoBehaviour)this).Invoke("NotifyManagerForFinish", 1.25f);
	}

	private void TurnOnKinematic()
	{
		((Component)this).GetComponent<Rigidbody2D>().isKinematic = true;
	}

	private IEnumerator pratiLijanaTarget(Transform target)
	{
		while (true)
		{
			yield return null;
			((Component)this).transform.position = Vector3.Lerp(((Component)this).transform.position, new Vector3(target.position.x, target.position.y, ((Component)this).transform.position.z), 0.25f);
		}
	}

	public void OtkaciMajmuna()
	{
		//IL_0054: Unknown result type (might be due to invalid IL or missing references)
		//IL_0070: Unknown result type (might be due to invalid IL or missing references)
		//IL_008a: Unknown result type (might be due to invalid IL or missing references)
		if (state == State.lijana)
		{
			((MonoBehaviour)this).StopCoroutine("pratiLijanaTarget");
			state = State.jumped;
			cameraFollow.cameraFollowX = true;
			maxSpeedX = startSpeedX;
			jumpSpeedX = startJumpSpeedX;
			((Component)this).transform.parent = null;
			((Component)this).transform.rotation = Quaternion.identity;
			((Component)this).GetComponent<Rigidbody2D>().isKinematic = false;
			((Component)this).GetComponent<Rigidbody2D>().velocity = Vector2.zero;
			((Component)this).GetComponent<Rigidbody2D>().AddForce(new Vector2(1000f, 2500f));
			animator.SetBool("LianaLetGo", true);
		}
	}

	private void SpustiMajmunaSaLijaneBrzo()
	{
		//IL_0048: Unknown result type (might be due to invalid IL or missing references)
		((MonoBehaviour)this).StopCoroutine("pratiLijanaTarget");
		state = State.jumped;
		cameraFollow.cameraFollowX = true;
		maxSpeedX = startSpeedX;
		jumpSpeedX = startJumpSpeedX;
		((Component)this).transform.parent = null;
		((Component)this).transform.rotation = Quaternion.identity;
		((Component)this).GetComponent<Rigidbody2D>().isKinematic = false;
		animator.Play(jump_State);
	}

	private IEnumerator Bounce(float time)
	{
		yield return (object)new WaitForSeconds(time);
		stop = false;
	}

	public void majmunUtepan()
	{
		if (magnet)
		{
			manage.ApplyPowerUp(-1);
		}
		if (doublecoins)
		{
			manage.ApplyPowerUp(-2);
		}
		canRespawnThings = false;
		((Component)majmun).GetComponent<Animator>().speed = 1.5f;
		((Component)this).gameObject.layer = LayerMask.NameToLayer("MonkeyShadow");
		state = State.wasted;
		((Component)((Component)manage).transform.Find("Gameplay Scena Interface/_TopRight/Pause Button")).GetComponent<Collider>().enabled = false;
		animator.Play("DeathStart");
		((MonoBehaviour)this).StartCoroutine(slowDown());
		((MonoBehaviour)this).StartCoroutine(checkGrounded());
		if (trava.isPlaying)
		{
			trava.Stop();
		}
		if (runParticle.isPlaying)
		{
			runParticle.Stop();
		}
		maxSpeedX = 0f;
	}

	private IEnumerator checkGrounded()
	{
		yield return (object)new WaitForSeconds(0.5f);
		while (!grounded)
		{
			yield return null;
		}
		animator.SetTrigger("Grounded");
		if (manage.keepPlayingCount >= 1)
		{
			((Component)manage).SendMessage("showFailedScreen");
		}
		else
		{
			((Component)manage).SendMessage("ShowKeepPlayingScreen");
		}
		cameraFollow.stopFollow = true;
		((Component)this).GetComponent<Rigidbody2D>().isKinematic = true;
		yield return (object)new WaitForSeconds(1.75f);
		if (!invincible)
		{
			((Component)((Component)this).transform.Find("Particles/HolderKillStars")).gameObject.SetActive(true);
		}
	}

	private IEnumerator slowDown()
	{
		float finish = ((Component)this).transform.position.x - 5f;
		float t = 0f;
		while (t < 0.5f)
		{
			((Component)this).transform.position = Vector3.Lerp(((Component)this).transform.position, new Vector3(finish, ((Component)this).transform.position.y, ((Component)this).transform.position.z), t);
			t += Time.deltaTime / 2f;
			yield return null;
		}
	}

	private string RaycastFunction(Vector3 vector)
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_0027: Unknown result type (might be due to invalid IL or missing references)
		//IL_0028: Unknown result type (might be due to invalid IL or missing references)
		RaycastHit val = default(RaycastHit);
		if (Physics.Raycast(guiCamera.ScreenPointToRay(vector), ref val))
		{
			return ((Object)((RaycastHit)(ref val)).collider).name;
		}
		if (Physics.Raycast(Camera.main.ScreenPointToRay(vector), ref val))
		{
			return ((Object)((RaycastHit)(ref val)).collider).name;
		}
		return "";
	}

	private IEnumerator turnHead(float step)
	{
		float t2 = 0f;
		while (t2 < 0.175f)
		{
			animator.SetLookAtWeight(lookWeight);
			animator.SetLookAtPosition(lookAtPos.position);
			lookWeight = Mathf.Lerp(lookWeight, 1f, t2);
			t2 += step * Time.deltaTime;
			yield return null;
		}
		t2 = 0f;
		while (t2 < 0.175f)
		{
			animator.SetLookAtWeight(lookWeight);
			animator.SetLookAtPosition(lookAtPos.position);
			lookWeight = Mathf.Lerp(lookWeight, 0f, t2);
			t2 += step * Time.deltaTime;
			yield return null;
		}
	}

	private IEnumerator easyStop()
	{
		while (maxSpeedX > 0f && usporavanje)
		{
			if (maxSpeedX < 0.5f)
			{
				maxSpeedX = 0f;
			}
			maxSpeedX = Mathf.Lerp(maxSpeedX, 0f, 10f * Time.deltaTime);
			yield return null;
		}
	}

	private IEnumerator easyGo()
	{
		while (maxSpeedX < startSpeedX && !usporavanje)
		{
			if (maxSpeedX > startSpeedX - 0.5f)
			{
				maxSpeedX = startSpeedX;
			}
			maxSpeedX = Mathf.Lerp(maxSpeedX, startSpeedX, 10f * Time.deltaTime);
			yield return null;
		}
	}

	private IEnumerator reappearItem(GameObject obj)
	{
		if (canRespawnThings)
		{
			yield return (object)new WaitForSeconds(5.5f);
			obj.SetActive(true);
		}
	}

	private IEnumerator nestaniTrail(float step)
	{
		float t = 0f;
		while (t < 0.4f)
		{
			trail.time = Mathf.Lerp(trail.time, 0f, t);
			t += Time.deltaTime * 5f;
			yield return null;
		}
		trail.time = 0f;
	}

	private void PustiVoiceJump()
	{
		PlaySounds.Play_VoiceJump();
	}

	public void SetInvincible()
	{
		//IL_003f: Unknown result type (might be due to invalid IL or missing references)
		//IL_004f: Unknown result type (might be due to invalid IL or missing references)
		//IL_005f: Unknown result type (might be due to invalid IL or missing references)
		//IL_006e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0119: Unknown result type (might be due to invalid IL or missing references)
		jumpSpeedX = Mathf.Abs(jumpSpeedX);
		moveForce = Mathf.Abs(moveForce);
		maxSpeedX = Mathf.Abs(maxSpeedX);
		majmun.localScale = new Vector3(majmun.localScale.x, majmun.localScale.y, Mathf.Abs(majmun.localScale.z));
		if (misijaSaDistance)
		{
			measureDistance = true;
		}
		invincible = true;
		((MonoBehaviour)this).StartCoroutine(blink());
		((MonoBehaviour)this).StopCoroutine(slowDown());
		killed = false;
		((Component)((Component)this).transform.Find("Particles/HolderKillStars")).gameObject.SetActive(false);
		animator.Play(jump_State);
		grounded = false;
		proveraGround = 16;
		maxSpeedX = startSpeedX;
		state = State.jumped;
		((Component)this).GetComponent<Rigidbody2D>().isKinematic = false;
		((Component)this).GetComponent<Rigidbody2D>().velocity = new Vector2(maxSpeedX, 50f);
		cameraFollow.stopFollow = false;
	}

	private IEnumerator blink()
	{
		float t = 0f;
		int i = 0;
		bool radi = false;
		Renderer meshrenderer = ((Component)majmun.Find("PorinceGorilla_LP")).GetComponent<Renderer>();
		Renderer usi = ((Component)majmun.Find("Usi")).GetComponent<Renderer>();
		Renderer kosa = ((Component)majmun.Find("Kosa")).GetComponent<Renderer>();
		Renderer glava = null;
		Renderer majica = null;
		Renderer ledja = null;
		((Component)this).gameObject.layer = LayerMask.NameToLayer("Monkey3D");
		if (StagesParser.glava != -1)
		{
			glava = ((Component)majmun.Find("ROOT/Hip/Spine/Chest/Neck/Head/" + StagesParser.glava).GetChild(0)).GetComponent<Renderer>();
		}
		if (StagesParser.majica != -1)
		{
			majica = ((Component)majmun.Find("custom_Majica")).GetComponent<Renderer>();
		}
		if (StagesParser.ledja != -1)
		{
			ledja = ((Component)majmun.Find("ROOT/Hip/Spine/" + StagesParser.ledja).GetChild(0)).GetComponent<Renderer>();
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
			i++;
			t += Time.deltaTime / 3f;
			yield return null;
		}
		canRespawnThings = true;
		meshrenderer.enabled = true;
		usi.enabled = true;
		kosa.enabled = true;
		if ((Object)(object)glava != (Object)null)
		{
			glava.enabled = true;
		}
		if ((Object)(object)majica != (Object)null)
		{
			majica.enabled = true;
		}
		if ((Object)(object)ledja != (Object)null)
		{
			ledja.enabled = true;
		}
		invincible = false;
	}

	private IEnumerator RemoveFog(float time, Collider2D col)
	{
		yield return (object)new WaitForSeconds(time);
		((Behaviour)col).enabled = true;
		((Component)((Component)col).transform.GetChild(0)).GetComponent<Renderer>().enabled = true;
		((Component)Camera.main).GetComponent<Animator>().Play("FogOfWar_Remove");
	}

	private void DisableImpact()
	{
		((Component)((Component)this).transform.Find("Impact")).gameObject.SetActive(false);
	}

	public void cancelPowerfullImpact()
	{
		powerfullImpact = false;
		if (PlaySounds.Glide_NEW.isPlaying)
		{
			PlaySounds.Stop_Glide();
		}
		((Component)zutiGlowSwooshVisoki).gameObject.SetActive(false);
	}

	private void reaktivirajVoiceJump()
	{
		pustenVoiceJump = false;
	}

	private IEnumerator pojaviMaglu(Collider2D col)
	{
		Transform fogOfWar = ((Component)Camera.main).transform.Find("FogOfWar");
		float value = fogOfWar.localScale.x;
		float target = 12f;
		float t2 = 0f;
		while (t2 < 1f)
		{
			fogOfWar.localScale = new Vector3(Mathf.Lerp(fogOfWar.localScale.x, target, t2), fogOfWar.localScale.y, fogOfWar.localScale.z);
			t2 += Time.deltaTime / 1.2f;
			yield return null;
		}
		((Behaviour)col).enabled = true;
		yield return (object)new WaitForSeconds(5f);
		t2 = 0f;
		while (t2 < 1f)
		{
			fogOfWar.localScale = new Vector3(Mathf.Lerp(fogOfWar.localScale.x, value, t2), fogOfWar.localScale.y, fogOfWar.localScale.z);
			t2 += Time.deltaTime / 1.2f;
			yield return null;
		}
		((Component)((Component)col).transform.GetChild(0)).GetComponent<Renderer>().enabled = true;
		((Component)fogOfWar).GetComponent<Renderer>().enabled = false;
	}

	private IEnumerator Spusti2DFollow()
	{
		Vector3 target = cameraTarget.transform.localPosition - new Vector3(0f, 4.5f, 0f);
		while (cameraTarget.transform.localPosition.y >= target.y + 0.1f)
		{
			yield return null;
			cameraTarget.transform.localPosition = new Vector3(cameraTarget.transform.localPosition.x, Mathf.MoveTowards(cameraTarget.transform.localPosition.y, target.y, 0.2f), cameraTarget.transform.localPosition.z);
		}
	}

	private IEnumerator Podigni2DFollow()
	{
		while (cameraTarget.transform.localPosition.y <= originalCameraTargetPosition - 0.1f)
		{
			yield return null;
			cameraTarget.transform.localPosition = new Vector3(cameraTarget.transform.localPosition.x, Mathf.MoveTowards(cameraTarget.transform.localPosition.y, originalCameraTargetPosition, 0.1f), cameraTarget.transform.localPosition.z);
		}
		cameraTarget.transform.localPosition = new Vector3(cameraTarget.transform.localPosition.x, originalCameraTargetPosition, cameraTarget.transform.localPosition.z);
	}

	private void ReturnFromMushroom()
	{
		//IL_0012: Unknown result type (might be due to invalid IL or missing references)
		((Component)this).GetComponent<Rigidbody2D>().isKinematic = false;
		((Component)this).GetComponent<Rigidbody2D>().velocity = Vector2.zero;
	}
}
