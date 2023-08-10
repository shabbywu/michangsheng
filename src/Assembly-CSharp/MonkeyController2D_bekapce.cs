using System;
using System.Collections;
using UnityEngine;

public class MonkeyController2D_bekapce : MonoBehaviour
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

	private bool jumpSafetyCheck;

	private bool proveriTerenIspredY;

	private bool downHit;

	[HideInInspector]
	public bool zgaziEnemija;

	[HideInInspector]
	public bool killed;

	[HideInInspector]
	public bool stop;

	private bool triggerCheckDown;

	[HideInInspector]
	public bool triggerCheckDownTrigger;

	[HideInInspector]
	public bool triggerCheckDownBehind;

	private bool CheckWallHitNear;

	private bool CheckWallHitNear_low;

	private bool startSpustanje;

	private bool startPenjanje;

	private bool spustanjeRastojanje;

	private float pocetniY_spustanje;

	[HideInInspector]
	public float collisionAngle;

	private float duzinaPritiskaZaSkok;

	private bool mozeDaSkociOpet;

	private float startY;

	private float endX;

	private float endY;

	private bool swoosh;

	private bool grab;

	private bool snappingToClimb;

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

	private Transform majmun;

	public ParticleSystem trava;

	public ParticleSystem oblak;

	public ParticleSystem particleSkok;

	public ParticleSystem dupliSkokOblaci;

	public ParticleSystem runParticle;

	public ParticleSystem klizanje;

	public ParticleSystem izrazitiPad;

	public Transform zutiGlowSwooshVisoki;

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

	private Animator parentAnim;

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

	private bool helper_disableMoveAfterGrab;

	private bool usporavanje;

	private bool sudarioSeSaZidom;

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

	private bool grounded;

	private float startVelY;

	private float korakce;

	[HideInInspector]
	public bool canGlide;

	private int proveraGround = 16;

	private bool jumpHolding;

	private TrailRenderer trail;

	private bool powerfullImpact;

	public float trailTime = 0.5f;

	private void Awake()
	{
		majmun = GameObject.Find("PrinceGorilla").transform;
		animator = ((Component)majmun).GetComponent<Animator>();
		cameraTarget = ((Component)((Component)this).transform.Find("PlayerFocus2D")).gameObject;
		cameraTarget_down = ((Component)((Component)this).transform.Find("PlayerFocus2D_down")).gameObject;
		cameraFollow = ((Component)((Component)Camera.main).transform.parent).GetComponent<CameraFollow2D_new>();
		lookAtPos = ((Component)this).transform.Find("LookAtPos");
		Input.multiTouchEnabled = true;
		parentAnim = ((Component)majmun.parent).GetComponent<Animator>();
	}

	private void Start()
	{
		//IL_003b: Unknown result type (might be due to invalid IL or missing references)
		//IL_004d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0052: Unknown result type (might be due to invalid IL or missing references)
		//IL_0084: Unknown result type (might be due to invalid IL or missing references)
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
		//IL_0239: Unknown result type (might be due to invalid IL or missing references)
		//IL_0254: Unknown result type (might be due to invalid IL or missing references)
		//IL_025e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0273: Unknown result type (might be due to invalid IL or missing references)
		//IL_0288: Unknown result type (might be due to invalid IL or missing references)
		//IL_0158: Unknown result type (might be due to invalid IL or missing references)
		//IL_0187: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_0338: Unknown result type (might be due to invalid IL or missing references)
		//IL_033d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0349: Unknown result type (might be due to invalid IL or missing references)
		//IL_0379: Unknown result type (might be due to invalid IL or missing references)
		//IL_038d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0392: Unknown result type (might be due to invalid IL or missing references)
		//IL_0397: Unknown result type (might be due to invalid IL or missing references)
		//IL_03a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_03b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_03bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_03c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_03d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_03ea: Unknown result type (might be due to invalid IL or missing references)
		//IL_03fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_0403: Unknown result type (might be due to invalid IL or missing references)
		//IL_0408: Unknown result type (might be due to invalid IL or missing references)
		//IL_0413: Unknown result type (might be due to invalid IL or missing references)
		//IL_0427: Unknown result type (might be due to invalid IL or missing references)
		//IL_042c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0431: Unknown result type (might be due to invalid IL or missing references)
		//IL_0445: Unknown result type (might be due to invalid IL or missing references)
		//IL_045b: Unknown result type (might be due to invalid IL or missing references)
		//IL_046f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0474: Unknown result type (might be due to invalid IL or missing references)
		//IL_0479: Unknown result type (might be due to invalid IL or missing references)
		//IL_0484: Unknown result type (might be due to invalid IL or missing references)
		//IL_0498: Unknown result type (might be due to invalid IL or missing references)
		//IL_049d: Unknown result type (might be due to invalid IL or missing references)
		//IL_04a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_04c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_04dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_04f0: Unknown result type (might be due to invalid IL or missing references)
		//IL_04f5: Unknown result type (might be due to invalid IL or missing references)
		//IL_04fa: Unknown result type (might be due to invalid IL or missing references)
		//IL_0505: Unknown result type (might be due to invalid IL or missing references)
		//IL_0519: Unknown result type (might be due to invalid IL or missing references)
		//IL_051e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0523: Unknown result type (might be due to invalid IL or missing references)
		//IL_0537: Unknown result type (might be due to invalid IL or missing references)
		//IL_054d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0561: Unknown result type (might be due to invalid IL or missing references)
		//IL_0566: Unknown result type (might be due to invalid IL or missing references)
		//IL_056b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0576: Unknown result type (might be due to invalid IL or missing references)
		//IL_058a: Unknown result type (might be due to invalid IL or missing references)
		//IL_058f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0594: Unknown result type (might be due to invalid IL or missing references)
		//IL_05a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_05be: Unknown result type (might be due to invalid IL or missing references)
		//IL_05d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_05d7: Unknown result type (might be due to invalid IL or missing references)
		//IL_05dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_05e7: Unknown result type (might be due to invalid IL or missing references)
		//IL_05fb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0600: Unknown result type (might be due to invalid IL or missing references)
		//IL_0605: Unknown result type (might be due to invalid IL or missing references)
		//IL_0629: Unknown result type (might be due to invalid IL or missing references)
		//IL_063f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0653: Unknown result type (might be due to invalid IL or missing references)
		//IL_0658: Unknown result type (might be due to invalid IL or missing references)
		//IL_065d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0668: Unknown result type (might be due to invalid IL or missing references)
		//IL_067c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0681: Unknown result type (might be due to invalid IL or missing references)
		//IL_0686: Unknown result type (might be due to invalid IL or missing references)
		//IL_06aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_06c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_06d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_06d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_06de: Unknown result type (might be due to invalid IL or missing references)
		//IL_06e9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0703: Unknown result type (might be due to invalid IL or missing references)
		//IL_0708: Unknown result type (might be due to invalid IL or missing references)
		//IL_070d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0731: Unknown result type (might be due to invalid IL or missing references)
		//IL_0768: Unknown result type (might be due to invalid IL or missing references)
		//IL_08ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_09c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0806: Unknown result type (might be due to invalid IL or missing references)
		//IL_080b: Unknown result type (might be due to invalid IL or missing references)
		//IL_080e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0de4: Unknown result type (might be due to invalid IL or missing references)
		//IL_0bfc: Unknown result type (might be due to invalid IL or missing references)
		//IL_1331: Unknown result type (might be due to invalid IL or missing references)
		//IL_0dfe: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c1c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a20: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a25: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a30: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a35: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a3f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a44: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a49: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a4f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a59: Unknown result type (might be due to invalid IL or missing references)
		//IL_1189: Unknown result type (might be due to invalid IL or missing references)
		//IL_0fa1: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e18: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d4b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c36: Unknown result type (might be due to invalid IL or missing references)
		//IL_094d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0952: Unknown result type (might be due to invalid IL or missing references)
		//IL_0955: Unknown result type (might be due to invalid IL or missing references)
		//IL_095b: Invalid comparison between Unknown and I4
		//IL_1502: Unknown result type (might be due to invalid IL or missing references)
		//IL_1456: Unknown result type (might be due to invalid IL or missing references)
		//IL_1353: Unknown result type (might be due to invalid IL or missing references)
		//IL_11a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0fc1: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e32: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c50: Unknown result type (might be due to invalid IL or missing references)
		//IL_151c: Unknown result type (might be due to invalid IL or missing references)
		//IL_136d: Unknown result type (might be due to invalid IL or missing references)
		//IL_11c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_1125: Unknown result type (might be due to invalid IL or missing references)
		//IL_0fdb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e4c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c6a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0af9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b03: Unknown result type (might be due to invalid IL or missing references)
		//IL_1536: Unknown result type (might be due to invalid IL or missing references)
		//IL_1387: Unknown result type (might be due to invalid IL or missing references)
		//IL_11dd: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ff5: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e66: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c84: Unknown result type (might be due to invalid IL or missing references)
		//IL_1550: Unknown result type (might be due to invalid IL or missing references)
		//IL_13a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_11f7: Unknown result type (might be due to invalid IL or missing references)
		//IL_100f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c9e: Unknown result type (might be due to invalid IL or missing references)
		//IL_156a: Unknown result type (might be due to invalid IL or missing references)
		//IL_13bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_1211: Unknown result type (might be due to invalid IL or missing references)
		//IL_1029: Unknown result type (might be due to invalid IL or missing references)
		//IL_1584: Unknown result type (might be due to invalid IL or missing references)
		//IL_13d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_1228: Unknown result type (might be due to invalid IL or missing references)
		//IL_1043: Unknown result type (might be due to invalid IL or missing references)
		//IL_159e: Unknown result type (might be due to invalid IL or missing references)
		//IL_13e9: Unknown result type (might be due to invalid IL or missing references)
		//IL_1079: Unknown result type (might be due to invalid IL or missing references)
		//IL_10a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_15b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_1400: Unknown result type (might be due to invalid IL or missing references)
		//IL_1416: Unknown result type (might be due to invalid IL or missing references)
		//IL_142b: Unknown result type (might be due to invalid IL or missing references)
		//IL_1693: Unknown result type (might be due to invalid IL or missing references)
		//IL_16a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_16b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_16be: Unknown result type (might be due to invalid IL or missing references)
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
		else if (((AnimatorStateInfo)(ref currentBaseState)).nameHash == fall_State)
		{
			if (animator.GetBool("Falling"))
			{
				animator.SetBool("Falling", false);
			}
		}
		else if (((AnimatorStateInfo)(ref currentBaseState)).nameHash == glide_loop_State && Time.frameCount % 60 == 0 && Random.Range(1, 100) <= 10)
		{
			((MonoBehaviour)this).StartCoroutine(turnHead(0.1f));
		}
		if (proveraGround == 0)
		{
			grounded = Object.op_Implicit((Object)(object)Physics2D.OverlapCircle(Vector2.op_Implicit(groundCheck.position), groundedRadius, LayerMask.op_Implicit(whatIsGround)));
		}
		else
		{
			proveraGround--;
		}
		CheckWallHitNear = RaycastHit2D.op_Implicit(Physics2D.Linecast(Vector2.op_Implicit(((Component)this).transform.position + new Vector3(0.8f, 2.5f, 0f)), Vector2.op_Implicit(((Component)this).transform.position + new Vector3(3.5f, 2.5f, 0f)), 1 << LayerMask.NameToLayer("WallHit")));
		CheckWallHitNear_low = RaycastHit2D.op_Implicit(Physics2D.Linecast(Vector2.op_Implicit(((Component)this).transform.position + new Vector3(0.8f, 0f, 0f)), Vector2.op_Implicit(((Component)this).transform.position + new Vector3(3.5f, 0f, 0f)), 1 << LayerMask.NameToLayer("WallHit")));
		triggerCheckDown = RaycastHit2D.op_Implicit(Physics2D.Linecast(Vector2.op_Implicit(((Component)this).transform.position + new Vector3(0.8f, 2.5f, 0f)), Vector2.op_Implicit(((Component)this).transform.position + new Vector3(0.8f, -0.5f, 0f)), (1 << LayerMask.NameToLayer("Ground")) | (1 << LayerMask.NameToLayer("Platform"))));
		triggerCheckDownTrigger = RaycastHit2D.op_Implicit(Physics2D.Linecast(Vector2.op_Implicit(((Component)this).transform.position + new Vector3(0.8f, 2.5f, 0f)), Vector2.op_Implicit(((Component)this).transform.position + new Vector3(0.8f, -4.5f, 0f)), 1 << LayerMask.NameToLayer("Platform")));
		triggerCheckDownBehind = RaycastHit2D.op_Implicit(Physics2D.Linecast(Vector2.op_Implicit(((Component)this).transform.position + new Vector3(-0.8f, 2.5f, 0f)), Vector2.op_Implicit(((Component)this).transform.position + new Vector3(-0.8f, -4.5f, 0f)), 1 << LayerMask.NameToLayer("Platform")));
		proveriTerenIspredY = RaycastHit2D.op_Implicit(Physics2D.Linecast(Vector2.op_Implicit(((Component)this).transform.position + new Vector3(4.4f, 1.2f, 0f)), Vector2.op_Implicit(((Component)this).transform.position + new Vector3(4.4f, -3.2f, 0f)), (1 << LayerMask.NameToLayer("Ground")) | (1 << LayerMask.NameToLayer("Platform"))));
		downHit = RaycastHit2D.op_Implicit(Physics2D.Linecast(Vector2.op_Implicit(((Component)this).transform.position + new Vector3(0.2f, 0.1f, 0f)), Vector2.op_Implicit(((Component)this).transform.position + new Vector3(0.2f, -0.65f, 0f)), (1 << LayerMask.NameToLayer("Ground")) | (1 << LayerMask.NameToLayer("Platform"))));
		spustanjeRastojanje = RaycastHit2D.op_Implicit(Physics2D.Linecast(Vector2.op_Implicit(((Component)this).transform.position + new Vector3(2.3f, 1.25f, 0f)), Vector2.op_Implicit(((Component)this).transform.position + new Vector3(2.3f, 0f - Camera.main.orthographicSize, 0f)), (1 << LayerMask.NameToLayer("Ground")) | (1 << LayerMask.NameToLayer("Platform"))));
		if (state == State.jumped || state == State.climbUp)
		{
			Touch touch;
			if (DupliSkok)
			{
				if ((Input.GetMouseButtonDown(0) && Input.mousePosition.x > (float)povrsinaZaClick) || Input.GetKeyDown((KeyCode)32))
				{
					if (mozeDaSkociOpet && hasJumped)
					{
						parentAnim.Play("DoubleJumpRotate");
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
						parentAnim.Play("DoubleJumpRotate");
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
				}
				else if (Input.GetMouseButtonUp(0) || Input.GetKeyUp((KeyCode)32))
				{
					startY = (endY = 0f);
					((Component)this).GetComponent<Rigidbody2D>().drag = 0f;
					animator.SetBool("Glide", false);
					disableGlide = false;
					canGlide = false;
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
				if (SlideNaDole && startY - endY > 0.25f * Camera.main.orthographicSize * ((float)Screen.height / (2f * Camera.main.orthographicSize)))
				{
					if (!RaycastHit2D.op_Implicit(Physics2D.Linecast(Vector2.op_Implicit(groundCheck.position), Vector2.op_Implicit(groundCheck.position - Vector3.up * 20f), LayerMask.op_Implicit(whatIsGround))))
					{
						powerfullImpact = true;
						((Component)zutiGlowSwooshVisoki).gameObject.SetActive(true);
					}
					swoosh = true;
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
				}
			}
			if (KontrolisaniSkok)
			{
				if (Input.GetMouseButton(0))
				{
					_ = jumpControlled;
				}
				if ((Input.GetMouseButtonUp(0) || Time.time - duzinaPritiskaZaSkok > 1f) && jumpControlled)
				{
					jumpControlled = false;
					jumpHolding = false;
					tempForce = jumpForce;
					canGlide = false;
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
				if (((Input.GetMouseButtonDown(0) && Input.mousePosition.x > (float)povrsinaZaClick) || Input.GetKeyDown((KeyCode)32)) && RaycastFunction(Input.mousePosition) != "Pause" && RaycastFunction(Input.mousePosition) != "HoleButtonsPlay_Pause" && RaycastFunction(Input.mousePosition) != "Buy Button" && RaycastFunction(Input.mousePosition) != "ButtonMain_NoMoreLives" && RaycastFunction(Input.mousePosition) != "HoleButtonsRestart_Pause" && !RaycastFunction(Input.mousePosition).Contains("Tutorial"))
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
						jumpSafetyCheck = true;
						animator.SetBool("WallStop", false);
						inAir = true;
						tempForce = jumpForce;
						particleSkok.Emit(20);
					}
				}
				if ((!Input.GetMouseButton(0) || !(Input.mousePosition.x > (float)povrsinaZaClick)) && !Input.GetKey((KeyCode)32) && Input.GetMouseButtonUp(0) && usporavanje && Zaustavljanje)
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
				if (RaycastFunction(Input.mousePosition) != "Pause" && RaycastFunction(Input.mousePosition) != "HoleButtonsPlay_Pause" && RaycastFunction(Input.mousePosition) != "Buy Button" && RaycastFunction(Input.mousePosition) != "ButtonMain_NoMoreLives" && RaycastFunction(Input.mousePosition) != "HoleButtonsRestart_Pause" && !RaycastFunction(Input.mousePosition).Contains("Tutorial") && !inAir)
				{
					state = State.climbUp;
					jumpSpeedX = 5f;
					if (PlaySounds.soundOn)
					{
						PlaySounds.Stop_Run();
						PlaySounds.Play_Jump();
					}
					jump = true;
					jumpSafetyCheck = true;
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
			if (Time.frameCount % 300 == 0 && Random.Range(1, 100) <= 25)
			{
				((MonoBehaviour)this).StartCoroutine(turnHead(0.1f));
			}
			if (KontrolisaniSkok)
			{
				if (((Input.GetMouseButtonDown(0) && Input.mousePosition.x > (float)povrsinaZaClick) || Input.GetKeyDown((KeyCode)32)) && RaycastFunction(Input.mousePosition) != "Pause" && RaycastFunction(Input.mousePosition) != "HoleButtonsPlay_Pause" && RaycastFunction(Input.mousePosition) != "Buy Button" && RaycastFunction(Input.mousePosition) != "ButtonMain_NoMoreLives" && RaycastFunction(Input.mousePosition) != "HoleButtonsRestart_Pause" && !RaycastFunction(Input.mousePosition).Contains("Tutorial"))
				{
					jumpHolding = true;
					grounded = false;
					proveraGround = 16;
					startVelY = ((Component)this).GetComponent<Rigidbody2D>().velocity.y;
					korakce = 3f;
					((Component)this).GetComponent<Rigidbody2D>().velocity = new Vector2(maxSpeedX, maxSpeedY);
					neTrebaDaProdje = false;
					duzinaPritiskaZaSkok = Time.time;
					state = State.jumped;
					if (PlaySounds.soundOn)
					{
						PlaySounds.Stop_Run();
						PlaySounds.Play_Jump();
					}
					jumpControlled = true;
					animator.Play(jump_State);
					animator.SetBool("Landing", false);
					jumpSafetyCheck = true;
					inAir = true;
					particleSkok.Emit(20);
				}
				if (!Input.GetMouseButton(0) || !(Input.mousePosition.x > (float)povrsinaZaClick))
				{
					Input.GetKey((KeyCode)32);
				}
				if ((Input.GetMouseButtonUp(0) || Time.time - duzinaPritiskaZaSkok > 2f) && jumpControlled)
				{
					jumpControlled = false;
					tempForce = jumpForce;
				}
			}
			else if (((Input.GetMouseButtonDown(0) && Input.mousePosition.x > (float)povrsinaZaClick) || Input.GetKeyDown((KeyCode)32)) && RaycastFunction(Input.mousePosition) != "Pause" && RaycastFunction(Input.mousePosition) != "HoleButtonsPlay_Pause" && RaycastFunction(Input.mousePosition) != "Buy Button" && RaycastFunction(Input.mousePosition) != "ButtonMain_NoMoreLives" && RaycastFunction(Input.mousePosition) != "HoleButtonsRestart_Pause" && !RaycastFunction(Input.mousePosition).Contains("Tutorial"))
			{
				neTrebaDaProdje = false;
				state = State.jumped;
				if (PlaySounds.soundOn)
				{
					PlaySounds.Stop_Run();
					PlaySounds.Play_Jump();
				}
				jump = true;
				animator.Play(jump_State);
				animator.SetBool("Landing", false);
				jumpSafetyCheck = true;
				inAir = true;
				particleSkok.Emit(20);
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
			if (Input.GetMouseButtonDown(0) && Input.mousePosition.x > (float)povrsinaZaClick && heCanJump && RaycastFunction(Input.mousePosition) != "ButtonPause" && RaycastFunction(Input.mousePosition) != "PauseHolePlay" && RaycastFunction(Input.mousePosition) != "PauseHoleShop" && RaycastFunction(Input.mousePosition) != "PauseHoleFreeCoins" && RaycastFunction(Input.mousePosition) != "PowersCardShield" && RaycastFunction(Input.mousePosition) != "PowersCardMagnet" && RaycastFunction(Input.mousePosition) != "PowersCardCoinx2" && !RaycastFunction(Input.mousePosition).Contains("Tutorial"))
			{
				if (Input.mousePosition.x < (float)(Screen.width / 2))
				{
					startY = Input.mousePosition.y;
				}
				else
				{
					OtkaciMajmuna();
				}
			}
			if (SlideNaDole)
			{
				if (Input.GetMouseButton(0))
				{
					endY = Input.mousePosition.y;
					if (SlideNaDole && startY - endY > 0.25f * Camera.main.orthographicSize * ((float)Screen.height / (2f * Camera.main.orthographicSize)))
					{
						SpustiMajmunaSaLijaneBrzo();
						swoosh = true;
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
			if (Input.GetMouseButtonDown(0) && heCanJump && RaycastFunction(Input.mousePosition) != "ButtonPause" && RaycastFunction(Input.mousePosition) != "PauseHolePlay" && RaycastFunction(Input.mousePosition) != "PauseHoleShop" && RaycastFunction(Input.mousePosition) != "PauseHoleFreeCoins" && RaycastFunction(Input.mousePosition) != "PowersCardShield" && RaycastFunction(Input.mousePosition) != "PowersCardMagnet" && RaycastFunction(Input.mousePosition) != "PowersCardCoinx2" && !RaycastFunction(Input.mousePosition).Contains("Tutorial") && !inAir)
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
				jumpSafetyCheck = true;
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
			jumpSafetyCheck = true;
		}
	}

	private void FixedUpdate()
	{
		//IL_0008: Unknown result type (might be due to invalid IL or missing references)
		//IL_000d: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d7: Unknown result type (might be due to invalid IL or missing references)
		//IL_003e: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_01bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0112: Unknown result type (might be due to invalid IL or missing references)
		//IL_0081: Unknown result type (might be due to invalid IL or missing references)
		//IL_0056: Unknown result type (might be due to invalid IL or missing references)
		//IL_0071: Unknown result type (might be due to invalid IL or missing references)
		//IL_0231: Unknown result type (might be due to invalid IL or missing references)
		//IL_0155: Unknown result type (might be due to invalid IL or missing references)
		//IL_012a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0145: Unknown result type (might be due to invalid IL or missing references)
		//IL_00aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_033c: Unknown result type (might be due to invalid IL or missing references)
		//IL_029f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0216: Unknown result type (might be due to invalid IL or missing references)
		//IL_0221: Unknown result type (might be due to invalid IL or missing references)
		//IL_017e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0377: Unknown result type (might be due to invalid IL or missing references)
		//IL_02da: Unknown result type (might be due to invalid IL or missing references)
		//IL_0262: Unknown result type (might be due to invalid IL or missing references)
		//IL_026c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0403: Unknown result type (might be due to invalid IL or missing references)
		//IL_0414: Unknown result type (might be due to invalid IL or missing references)
		//IL_035c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0367: Unknown result type (might be due to invalid IL or missing references)
		//IL_02bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ca: Unknown result type (might be due to invalid IL or missing references)
		//IL_0456: Unknown result type (might be due to invalid IL or missing references)
		//IL_0471: Unknown result type (might be due to invalid IL or missing references)
		//IL_03a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_03b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_0709: Unknown result type (might be due to invalid IL or missing references)
		//IL_071d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0722: Unknown result type (might be due to invalid IL or missing references)
		//IL_0727: Unknown result type (might be due to invalid IL or missing references)
		//IL_0732: Unknown result type (might be due to invalid IL or missing references)
		//IL_0746: Unknown result type (might be due to invalid IL or missing references)
		//IL_074b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0750: Unknown result type (might be due to invalid IL or missing references)
		//IL_0764: Unknown result type (might be due to invalid IL or missing references)
		//IL_0311: Unknown result type (might be due to invalid IL or missing references)
		//IL_031b: Unknown result type (might be due to invalid IL or missing references)
		//IL_05a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_05c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_04a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_04c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_05ee: Unknown result type (might be due to invalid IL or missing references)
		//IL_060a: Unknown result type (might be due to invalid IL or missing references)
		//IL_06c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_06d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_06e3: Unknown result type (might be due to invalid IL or missing references)
		//IL_04fa: Unknown result type (might be due to invalid IL or missing references)
		//IL_0646: Unknown result type (might be due to invalid IL or missing references)
		//IL_067a: Unknown result type (might be due to invalid IL or missing references)
		//IL_066a: Unknown result type (might be due to invalid IL or missing references)
		//IL_069d: Unknown result type (might be due to invalid IL or missing references)
		//IL_06ad: Unknown result type (might be due to invalid IL or missing references)
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
		else if (state == State.wasted)
		{
			if (((Component)this).GetComponent<Rigidbody2D>().velocity.x < maxSpeedX && !stop)
			{
				((Component)this).GetComponent<Rigidbody2D>().AddForce(Vector2.right * moveForce);
			}
			if (Mathf.Abs(((Component)this).GetComponent<Rigidbody2D>().velocity.x) > maxSpeedX && !stop)
			{
				((Component)this).GetComponent<Rigidbody2D>().velocity = new Vector2(maxSpeedX, ((Component)this).GetComponent<Rigidbody2D>().velocity.y);
			}
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
			if (((Component)this).GetComponent<Rigidbody2D>().velocity.x < maxSpeedX && !stop)
			{
				((Component)this).GetComponent<Rigidbody2D>().AddForce(Vector2.right * moveForce);
			}
			if (Mathf.Abs(((Component)this).GetComponent<Rigidbody2D>().velocity.x) > maxSpeedX && !stop)
			{
				((Component)this).GetComponent<Rigidbody2D>().velocity = new Vector2(maxSpeedX, ((Component)this).GetComponent<Rigidbody2D>().velocity.y);
			}
			if (triggerCheckDown)
			{
				animator.Play(run_State);
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
			else if (jumpControlled)
			{
				hasJumped = true;
				mozeDaSkociOpet = true;
			}
			if (((Component)this).GetComponent<Rigidbody2D>().velocity.y < -0.01f)
			{
				_ = triggerCheckDown;
			}
			if (!triggerCheckDown && jumpSafetyCheck)
			{
				jumpSafetyCheck = false;
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
		else if (state == State.climbUp)
		{
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
			if (((Component)this).GetComponent<Rigidbody2D>().velocity.y < -0.01f)
			{
				_ = triggerCheckDown;
			}
		}
		if ((!triggerCheckDown || RaycastHit2D.op_Implicit(Physics2D.Linecast(Vector2.op_Implicit(((Component)this).transform.position + new Vector3(1f, 0f, 0f)), Vector2.op_Implicit(((Component)this).transform.position + new Vector3(1f, 2f, 0f)), 1 << LayerMask.NameToLayer("Ground")))) && jumpSafetyCheck)
		{
			jumpSafetyCheck = false;
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
		//IL_0190: Unknown result type (might be due to invalid IL or missing references)
		//IL_015a: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f6: Unknown result type (might be due to invalid IL or missing references)
		//IL_0206: Unknown result type (might be due to invalid IL or missing references)
		//IL_0215: Unknown result type (might be due to invalid IL or missing references)
		//IL_0561: Unknown result type (might be due to invalid IL or missing references)
		//IL_0582: Unknown result type (might be due to invalid IL or missing references)
		//IL_03d4: Unknown result type (might be due to invalid IL or missing references)
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
			if (state != State.jumped)
			{
				return;
			}
			if (PlaySounds.soundOn)
			{
				PlaySounds.Play_Landing();
			}
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
					powerfullImpact = false;
					((Component)zutiGlowSwooshVisoki).gameObject.SetActive(false);
					((Component)Camera.main).GetComponent<Animator>().Play("CameraShakeTrasGround");
					izrazitiPad.Play();
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
		else
		{
			if (!(col.gameObject.tag == "Enemy"))
			{
				return;
			}
			if (activeShield)
			{
				((Behaviour)((Component)col.transform).GetComponent<Collider2D>()).enabled = false;
				activeShield = false;
				GameObject.Find("_GameManager").SendMessage("ApplyPowerUp", (object)(-3));
				if (state != 0)
				{
					((Component)this).GetComponent<Rigidbody2D>().AddForce(new Vector2(maxSpeedX, 1100f));
				}
			}
			else
			{
				if (killed)
				{
					return;
				}
				if (state == State.running)
				{
					((Component)this).GetComponent<Rigidbody2D>().velocity = Vector2.zero;
					killed = true;
					oblak.Play();
					majmunUtepan();
					return;
				}
				majmunUtepanULetu();
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

	public void majmunUtepanULetu()
	{
		((MonoBehaviour)this).StartCoroutine(FallDownAfterSpikes());
	}

	private IEnumerator FallDownAfterSpikes()
	{
		canRespawnThings = false;
		GameObject.Find("PrinceGorilla").GetComponent<Animator>().speed = 1.5f;
		if (PlaySounds.BackgroundMusic_Gameplay.isPlaying)
		{
			PlaySounds.Stop_BackgroundMusic_Gameplay();
		}
		if (PlaySounds.soundOn)
		{
			PlaySounds.Play_Level_Failed_Popup();
		}
		((Component)this).GetComponent<Rigidbody2D>().velocity = Vector2.zero;
		killed = true;
		oblak.Play();
		animator.Play(spikedeath_State);
		parentAnim.Play("FallDown");
		state = State.wasted;
		GameObject.Find("Pause").GetComponent<Collider>().enabled = false;
		if (trava.isPlaying)
		{
			trava.Stop();
		}
		if (runParticle.isPlaying)
		{
			runParticle.Stop();
		}
		maxSpeedX = 0f;
		((Component)this).GetComponent<Rigidbody2D>().isKinematic = true;
		yield return (object)new WaitForSeconds(0.5f);
		GameObject.Find("_GameManager").SendMessage("showFailedScreen");
		cameraFollow.stopFollow = true;
	}

	private IEnumerator ProceduraPenjanja(GameObject obj)
	{
		yield return (object)new WaitForSeconds(0.01f);
		while (((AnimatorStateInfo)(ref currentBaseState)).nameHash == grab_State)
		{
			yield return null;
			if (((AnimatorStateInfo)(ref currentBaseState)).normalizedTime > 0.82f && !helper_disableMoveAfterGrab)
			{
				helper_disableMoveAfterGrab = true;
				animator.CrossFade(run_State, 0.01f);
				yield return (object)new WaitForEndOfFrame();
				((Component)this).transform.position = GameObject.Find("GrabLanding").transform.position;
			}
		}
		state = State.running;
		animator.applyRootMotion = false;
		((Behaviour)((Component)this).GetComponent<Collider2D>()).enabled = true;
		((Component)this).GetComponent<Rigidbody2D>().isKinematic = false;
		helper_disableMoveAfterGrab = false;
		((Component)this).GetComponent<Rigidbody2D>().isKinematic = false;
		mozeDaSkociOpet = false;
		animator.SetBool("Jump", false);
		animator.SetBool("DoubleJump", false);
		animator.SetBool("Glide", false);
		animator.SetBool("Landing", true);
		((Component)this).GetComponent<Rigidbody2D>().drag = 0f;
		maxSpeedX = startSpeedX;
		state = State.running;
		if (PlaySounds.soundOn)
		{
			PlaySounds.Play_Run();
		}
		animator.SetBool("WallStop", false);
		inAir = false;
		hasJumped = false;
		((Behaviour)((Component)this).GetComponent<Collider2D>()).enabled = true;
	}

	private IEnumerator snappingProcess()
	{
		float t = 0f;
		float step = 0.25f;
		while (t < 0.02f && Mathf.Abs(((Component)this).transform.position.x - colliderForClimb.x) > 0.01f && Mathf.Abs(((Component)this).transform.position.y - colliderForClimb.y) > 0.01f)
		{
			((Component)this).transform.position = Vector3.Lerp(((Component)this).transform.position, new Vector3(colliderForClimb.x, colliderForClimb.y, ((Component)this).transform.position.z), step);
			t += Time.deltaTime * step;
			yield return null;
		}
		grab = true;
		animator.Play("Grab");
	}

	private void OnCollisionExit2D(Collision2D col)
	{
		//IL_00b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c8: Unknown result type (might be due to invalid IL or missing references)
		if (col.gameObject.tag == "Footer")
		{
			if (state != 0)
			{
				return;
			}
			neTrebaDaProdje = false;
			if (!proveriTerenIspredY && !downHit)
			{
				state = State.jumped;
				animator.SetBool("Landing", false);
				animator.Play(fall_State);
				if (runParticle.isPlaying)
				{
					runParticle.Stop();
				}
				if (!spustanjeRastojanje)
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
			((Component)this).GetComponent<Rigidbody2D>().drag = 0f;
			if (trava.isPlaying)
			{
				trava.Stop();
			}
			animator.Play(fall_State);
			animator.SetBool("Landing", false);
		}
	}

	private void NotifyManagerForFinish()
	{
		GameObject.Find("_GameManager").SendMessage("ShowWinScreen");
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
		//IL_02a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_0293: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_0378: Unknown result type (might be due to invalid IL or missing references)
		//IL_039c: Unknown result type (might be due to invalid IL or missing references)
		if (state == State.completed && state == State.wasted)
		{
			return;
		}
		if (((Component)col).tag == "Barrel")
		{
			((Component)((Component)col).transform.GetChild(0)).GetComponent<Animator>().Play("BarrelBoom");
		}
		else if (((Object)col).name == "Magnet_collect")
		{
			GameObject.Find("_GameManager").SendMessage("ApplyPowerUp", (object)1);
			((Component)col).gameObject.SetActive(false);
			((MonoBehaviour)this).StartCoroutine(reappearItem(((Component)col).gameObject));
		}
		else if (((Object)col).name == "BananaCoinX2_collect")
		{
			GameObject.Find("_GameManager").SendMessage("ApplyPowerUp", (object)2);
			((Component)col).gameObject.SetActive(false);
			((MonoBehaviour)this).StartCoroutine(reappearItem(((Component)col).gameObject));
		}
		else if (((Object)col).name == "Shield_collect")
		{
			GameObject.Find("_GameManager").SendMessage("ApplyPowerUp", (object)3);
			((Component)col).gameObject.SetActive(false);
			activeShield = true;
			((MonoBehaviour)this).StartCoroutine(reappearItem(((Component)col).gameObject));
		}
		else if (((Object)col).name == "Banana_collect")
		{
			((Component)col).gameObject.SetActive(false);
			Manage.points += 200;
			((MonoBehaviour)this).StartCoroutine(reappearItem(((Component)col).gameObject));
		}
		else if (((Object)col).name == "Srce_collect")
		{
			((Component)col).gameObject.SetActive(false);
			GameObject.Find("LifeManager").SendMessage("AddLife");
			((MonoBehaviour)this).StartCoroutine(reappearItem(((Component)col).gameObject));
		}
		else if (((Object)col).name == "Dijamant_collect")
		{
			((Component)col).gameObject.SetActive(false);
			Manage.points += 50;
			((MonoBehaviour)this).StartCoroutine(reappearItem(((Component)col).gameObject));
		}
		if (((Component)col).tag == "Finish")
		{
			((Behaviour)((Component)col).GetComponent<Collider2D>()).enabled = false;
			cameraFollow.cameraFollowX = false;
			((MonoBehaviour)this).Invoke("NotifyManagerForFinish", 1.25f);
			GameObject.Find("Pause").GetComponent<Collider>().enabled = false;
			state = State.completed;
		}
		else if (((Component)col).tag == "Footer")
		{
			float num = ((((Component)col).transform.childCount <= 0) ? ((Component)col).transform.position.y : ((Component)col).transform.Find("TriggerPositionUp").position.y);
			if (((Component)this).transform.position.y + 0.25f > num && triggerCheckDownTrigger && triggerCheckDownBehind && ((Component)this).GetComponent<Collider2D>().isTrigger)
			{
				((Component)this).GetComponent<Collider2D>().isTrigger = false;
			}
		}
		else if (((Component)col).tag == "Enemy")
		{
			((Behaviour)((Component)col).GetComponent<Collider2D>()).enabled = false;
			if (activeShield)
			{
				((Behaviour)((Component)((Component)col).transform).GetComponent<Collider2D>()).enabled = false;
				activeShield = false;
				GameObject.Find("_GameManager").SendMessage("ApplyPowerUp", (object)(-3));
				if (state != 0)
				{
					((Component)this).GetComponent<Rigidbody2D>().AddForce(new Vector2(maxSpeedX, 1100f));
				}
			}
			else if (!killed)
			{
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
		else if (((Component)col).tag == "GrabLedge" && !downHit)
		{
			((Behaviour)((Component)col).GetComponent<Collider2D>()).enabled = false;
		}
		else if (((Component)col).tag == "Lijana")
		{
			grabLianaTransform = ((Component)col).transform.GetChild(0);
			lijana = true;
			state = State.lijana;
			((Behaviour)col).enabled = false;
			((Component)this).GetComponent<Rigidbody2D>().isKinematic = true;
			maxSpeedX = 0f;
			jumpSpeedX = 0f;
			((Component)((Component)col).transform.parent).GetComponent<Animator>().Play("RotateLianaHolder");
			animator.Play(lijana_State);
			((MonoBehaviour)this).StartCoroutine("pratiLijanaTarget", (object)grabLianaTransform);
		}
	}

	private void OnTriggerStay2D(Collider2D col)
	{
		//IL_0023: Unknown result type (might be due to invalid IL or missing references)
		//IL_003c: Unknown result type (might be due to invalid IL or missing references)
		if (((Component)col).tag == "GrabLedge")
		{
			((Component)this).GetComponent<Rigidbody2D>().velocity = new Vector2(0f, Mathf.MoveTowards(((Component)this).GetComponent<Rigidbody2D>().velocity.y, 0f, 0.2f));
		}
	}

	private IEnumerator pratiLijanaTarget(Transform target)
	{
		while (lijana)
		{
			yield return null;
			((Component)this).transform.position = Vector3.Lerp(((Component)this).transform.position, new Vector3(target.position.x, target.position.y, ((Component)this).transform.position.z), 0.2f);
		}
	}

	public void OtkaciMajmuna()
	{
		//IL_004f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0075: Unknown result type (might be due to invalid IL or missing references)
		lijana = false;
		((MonoBehaviour)this).StopCoroutine("pratiLijanaTarget");
		state = State.jumped;
		cameraFollow.cameraFollowX = true;
		maxSpeedX = startSpeedX;
		jumpSpeedX = startJumpSpeedX;
		((Component)this).transform.parent = null;
		((Component)this).transform.rotation = Quaternion.identity;
		((Component)this).GetComponent<Rigidbody2D>().isKinematic = false;
		((Component)this).GetComponent<Rigidbody2D>().AddForce(new Vector2(200f, 2500f));
		animator.Play(jump_State);
	}

	private void SpustiMajmunaSaLijaneBrzo()
	{
		//IL_004f: Unknown result type (might be due to invalid IL or missing references)
		lijana = false;
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

	private void climb()
	{
		((MonoBehaviour)this).StartCoroutine(MoveUp(0.05f));
	}

	private IEnumerator ClimbLedge(Transform target, float time)
	{
		yield return null;
		if (((Component)this).transform.position.y > target.position.y)
		{
			((Component)this).GetComponent<Rigidbody2D>().velocity = new Vector2(0f, 0f);
			((Component)this).GetComponent<Rigidbody2D>().isKinematic = false;
			stop = false;
			mozeDaSkociOpet = false;
			animator.SetBool("Jump", false);
			animator.SetBool("DoubleJump", false);
			animator.SetBool("Glide", false);
			disableGlide = false;
			animator.SetBool("Landing", true);
			state = State.running;
			if (PlaySounds.soundOn)
			{
				PlaySounds.Play_Run();
			}
			hasJumped = false;
		}
	}

	private IEnumerator MoveUp(float time)
	{
		float target = ((Component)this).transform.position.y + 1.85f;
		float t = 0f;
		float num = 2f;
		float destX = ((Component)this).transform.position.x + num;
		float step = 0.03f;
		while (t < 1f)
		{
			((Component)this).transform.position = Vector2.op_Implicit(Vector2.MoveTowards(Vector2.op_Implicit(((Component)this).transform.position), Vector2.op_Implicit(new Vector3(destX, target, ((Component)this).transform.position.z)), t));
			if (Time.timeScale != 0f)
			{
			}
			t += step;
			yield return null;
		}
	}

	public void majmunUtepan()
	{
		canRespawnThings = false;
		GameObject.Find("PrinceGorilla").GetComponent<Animator>().speed = 1.5f;
		state = State.wasted;
		GameObject.Find("Pause").GetComponent<Collider>().enabled = false;
		animator.Play("Death1");
		((MonoBehaviour)this).StartCoroutine(slowDown());
		if (trava.isPlaying)
		{
			trava.Stop();
		}
		if (runParticle.isPlaying)
		{
			runParticle.Stop();
		}
		maxSpeedX = 0f;
		if (PlaySounds.BackgroundMusic_Gameplay.isPlaying)
		{
			PlaySounds.Stop_BackgroundMusic_Gameplay();
		}
		if (PlaySounds.soundOn)
		{
			PlaySounds.Play_Level_Failed_Popup();
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
		GameObject.Find("_GameManager").SendMessage("showFailedScreen");
		cameraFollow.stopFollow = true;
		((Component)this).GetComponent<Rigidbody2D>().isKinematic = true;
		yield return (object)new WaitForSeconds(1.75f);
		((Component)((Component)this).transform.Find("Particles/HolderKillStars")).gameObject.SetActive(true);
	}

	public void CallShake()
	{
	}

	private IEnumerator shakeCamera()
	{
		yield return null;
	}

	private string RaycastFunction(Vector3 vector)
	{
		//IL_0005: Unknown result type (might be due to invalid IL or missing references)
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		RaycastHit val = default(RaycastHit);
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
		yield return (object)new WaitForSeconds(1.5f);
		obj.SetActive(true);
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
}
