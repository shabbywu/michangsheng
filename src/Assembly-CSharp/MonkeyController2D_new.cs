using System;
using System.Collections;
using UnityEngine;

public class MonkeyController2D_new : MonoBehaviour
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

	private Transform groundCheck;

	private Transform majmun;

	public ParticleSystem trava;

	public ParticleSystem oblak;

	public ParticleSystem particleSkok;

	public ParticleSystem dupliSkokOblaci;

	public ParticleSystem runParticle;

	public ParticleSystem klizanje;

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

	private int run_State = Animator.StringToHash("Base Layer.Running");

	private int jump_State = Animator.StringToHash("Base Layer.Jump_Start");

	private int fall_State = Animator.StringToHash("Base Layer.Jump_Falling");

	private int landing_State = Animator.StringToHash("Base Layer.Landing");

	private int doublejump_State = Animator.StringToHash("Base Layer.DoubleJump_Start");

	private int glide_start_State = Animator.StringToHash("Base Layer.Glide_Start");

	private int glide_loop_State = Animator.StringToHash("Base Layer.Glide_Loop");

	private int grab_State = Animator.StringToHash("Base Layer.Grab");

	private int wall_stop_State = Animator.StringToHash("Base Layer.WallStop");

	private int wall_stop_from_jump_State = Animator.StringToHash("Base Layer.WallStop_From_Jump");

	private int swoosh_State = Animator.StringToHash("Base Layer.Swoosh_Down");

	private int spikedeath_State = Animator.StringToHash("Base Layer.Spike_death");

	private int lijana_State = Animator.StringToHash("Base Layer.Lijana");

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

	private bool canGlide;

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
		//IL_0272: Unknown result type (might be due to invalid IL or missing references)
		//IL_028d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0297: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_0191: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01db: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01fa: Unknown result type (might be due to invalid IL or missing references)
		//IL_020f: Unknown result type (might be due to invalid IL or missing references)
		//IL_039f: Unknown result type (might be due to invalid IL or missing references)
		//IL_03a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_03b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_03d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_03e4: Unknown result type (might be due to invalid IL or missing references)
		//IL_03e9: Unknown result type (might be due to invalid IL or missing references)
		//IL_03ee: Unknown result type (might be due to invalid IL or missing references)
		//IL_03f9: Unknown result type (might be due to invalid IL or missing references)
		//IL_040d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0412: Unknown result type (might be due to invalid IL or missing references)
		//IL_0417: Unknown result type (might be due to invalid IL or missing references)
		//IL_042b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0441: Unknown result type (might be due to invalid IL or missing references)
		//IL_0455: Unknown result type (might be due to invalid IL or missing references)
		//IL_045a: Unknown result type (might be due to invalid IL or missing references)
		//IL_045f: Unknown result type (might be due to invalid IL or missing references)
		//IL_046a: Unknown result type (might be due to invalid IL or missing references)
		//IL_047e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0483: Unknown result type (might be due to invalid IL or missing references)
		//IL_0488: Unknown result type (might be due to invalid IL or missing references)
		//IL_049c: Unknown result type (might be due to invalid IL or missing references)
		//IL_04b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_04c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_04cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_04d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_04db: Unknown result type (might be due to invalid IL or missing references)
		//IL_04ef: Unknown result type (might be due to invalid IL or missing references)
		//IL_04f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_04f9: Unknown result type (might be due to invalid IL or missing references)
		//IL_051d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0533: Unknown result type (might be due to invalid IL or missing references)
		//IL_0547: Unknown result type (might be due to invalid IL or missing references)
		//IL_054c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0551: Unknown result type (might be due to invalid IL or missing references)
		//IL_055c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0570: Unknown result type (might be due to invalid IL or missing references)
		//IL_0575: Unknown result type (might be due to invalid IL or missing references)
		//IL_057a: Unknown result type (might be due to invalid IL or missing references)
		//IL_059e: Unknown result type (might be due to invalid IL or missing references)
		//IL_05b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_05c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_05cd: Unknown result type (might be due to invalid IL or missing references)
		//IL_05d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_05dd: Unknown result type (might be due to invalid IL or missing references)
		//IL_05f1: Unknown result type (might be due to invalid IL or missing references)
		//IL_05f6: Unknown result type (might be due to invalid IL or missing references)
		//IL_05fb: Unknown result type (might be due to invalid IL or missing references)
		//IL_060f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0625: Unknown result type (might be due to invalid IL or missing references)
		//IL_0639: Unknown result type (might be due to invalid IL or missing references)
		//IL_063e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0643: Unknown result type (might be due to invalid IL or missing references)
		//IL_064e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0662: Unknown result type (might be due to invalid IL or missing references)
		//IL_0667: Unknown result type (might be due to invalid IL or missing references)
		//IL_066c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0690: Unknown result type (might be due to invalid IL or missing references)
		//IL_06a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_06ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_06bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_06c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_06cf: Unknown result type (might be due to invalid IL or missing references)
		//IL_06e3: Unknown result type (might be due to invalid IL or missing references)
		//IL_06e8: Unknown result type (might be due to invalid IL or missing references)
		//IL_06ed: Unknown result type (might be due to invalid IL or missing references)
		//IL_0711: Unknown result type (might be due to invalid IL or missing references)
		//IL_0727: Unknown result type (might be due to invalid IL or missing references)
		//IL_073b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0740: Unknown result type (might be due to invalid IL or missing references)
		//IL_0745: Unknown result type (might be due to invalid IL or missing references)
		//IL_0750: Unknown result type (might be due to invalid IL or missing references)
		//IL_076a: Unknown result type (might be due to invalid IL or missing references)
		//IL_076f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0774: Unknown result type (might be due to invalid IL or missing references)
		//IL_0798: Unknown result type (might be due to invalid IL or missing references)
		//IL_07cf: Unknown result type (might be due to invalid IL or missing references)
		//IL_0913: Unknown result type (might be due to invalid IL or missing references)
		//IL_09e5: Unknown result type (might be due to invalid IL or missing references)
		//IL_086d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0872: Unknown result type (might be due to invalid IL or missing references)
		//IL_0875: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d8c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ba4: Unknown result type (might be due to invalid IL or missing references)
		//IL_12d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_0da6: Unknown result type (might be due to invalid IL or missing references)
		//IL_0bc4: Unknown result type (might be due to invalid IL or missing references)
		//IL_112c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0f49: Unknown result type (might be due to invalid IL or missing references)
		//IL_0dc0: Unknown result type (might be due to invalid IL or missing references)
		//IL_0cf3: Unknown result type (might be due to invalid IL or missing references)
		//IL_0bde: Unknown result type (might be due to invalid IL or missing references)
		//IL_098d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0992: Unknown result type (might be due to invalid IL or missing references)
		//IL_0995: Unknown result type (might be due to invalid IL or missing references)
		//IL_099b: Invalid comparison between Unknown and I4
		//IL_0ace: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ade: Unknown result type (might be due to invalid IL or missing references)
		//IL_0aef: Unknown result type (might be due to invalid IL or missing references)
		//IL_14a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_13f9: Unknown result type (might be due to invalid IL or missing references)
		//IL_12f6: Unknown result type (might be due to invalid IL or missing references)
		//IL_114c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0f69: Unknown result type (might be due to invalid IL or missing references)
		//IL_0dda: Unknown result type (might be due to invalid IL or missing references)
		//IL_0bf8: Unknown result type (might be due to invalid IL or missing references)
		//IL_14bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_1310: Unknown result type (might be due to invalid IL or missing references)
		//IL_1166: Unknown result type (might be due to invalid IL or missing references)
		//IL_10c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_0f83: Unknown result type (might be due to invalid IL or missing references)
		//IL_0df4: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c12: Unknown result type (might be due to invalid IL or missing references)
		//IL_14d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_132a: Unknown result type (might be due to invalid IL or missing references)
		//IL_1180: Unknown result type (might be due to invalid IL or missing references)
		//IL_0f9d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e0e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c2c: Unknown result type (might be due to invalid IL or missing references)
		//IL_14f3: Unknown result type (might be due to invalid IL or missing references)
		//IL_1344: Unknown result type (might be due to invalid IL or missing references)
		//IL_119a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0fb7: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c46: Unknown result type (might be due to invalid IL or missing references)
		//IL_150d: Unknown result type (might be due to invalid IL or missing references)
		//IL_135e: Unknown result type (might be due to invalid IL or missing references)
		//IL_11b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_0fd1: Unknown result type (might be due to invalid IL or missing references)
		//IL_1527: Unknown result type (might be due to invalid IL or missing references)
		//IL_1375: Unknown result type (might be due to invalid IL or missing references)
		//IL_11cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0feb: Unknown result type (might be due to invalid IL or missing references)
		//IL_1541: Unknown result type (might be due to invalid IL or missing references)
		//IL_138c: Unknown result type (might be due to invalid IL or missing references)
		//IL_100b: Unknown result type (might be due to invalid IL or missing references)
		//IL_1031: Unknown result type (might be due to invalid IL or missing references)
		//IL_1048: Unknown result type (might be due to invalid IL or missing references)
		//IL_155b: Unknown result type (might be due to invalid IL or missing references)
		//IL_13a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_13b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_13ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_1636: Unknown result type (might be due to invalid IL or missing references)
		//IL_1646: Unknown result type (might be due to invalid IL or missing references)
		//IL_1656: Unknown result type (might be due to invalid IL or missing references)
		//IL_1661: Unknown result type (might be due to invalid IL or missing references)
		hit = Physics2D.Linecast(Vector2.op_Implicit(((Component)this).transform.position + new Vector3(0.8f, 2.5f, 0f)), Vector2.op_Implicit(((Component)this).transform.position + new Vector3(0.8f, -15.5f, 0f)), (1 << LayerMask.NameToLayer("Ground")) | (1 << LayerMask.NameToLayer("Platform")));
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
		if (snappingToClimb)
		{
			((MonoBehaviour)this).StartCoroutine(snappingProcess());
			snappingToClimb = false;
		}
		if (grab)
		{
			((MonoBehaviour)this).StartCoroutine(ProceduraPenjanja(null));
			grab = false;
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
		if (((AnimatorStateInfo)(ref currentBaseState)).nameHash == doublejump_State)
		{
			if (animator.GetBool("DoubleJump"))
			{
				animator.SetBool("DoubleJump", false);
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
		grounded = Object.op_Implicit((Object)(object)Physics2D.OverlapCircle(Vector2.op_Implicit(groundCheck.position), groundedRadius, LayerMask.op_Implicit(whatIsGround)));
		CheckWallHitNear = RaycastHit2D.op_Implicit(Physics2D.Linecast(Vector2.op_Implicit(((Component)this).transform.position + new Vector3(0.8f, 2.5f, 0f)), Vector2.op_Implicit(((Component)this).transform.position + new Vector3(3.5f, 2.5f, 0f)), 1 << LayerMask.NameToLayer("WallHit")));
		CheckWallHitNear_low = RaycastHit2D.op_Implicit(Physics2D.Linecast(Vector2.op_Implicit(((Component)this).transform.position + new Vector3(0.8f, 0f, 0f)), Vector2.op_Implicit(((Component)this).transform.position + new Vector3(3.5f, 0f, 0f)), 1 << LayerMask.NameToLayer("WallHit")));
		triggerCheckDown = RaycastHit2D.op_Implicit(Physics2D.Linecast(Vector2.op_Implicit(((Component)this).transform.position + new Vector3(0.8f, 2.5f, 0f)), Vector2.op_Implicit(((Component)this).transform.position + new Vector3(0.8f, -0.5f, 0f)), (1 << LayerMask.NameToLayer("Ground")) | (1 << LayerMask.NameToLayer("Platform"))));
		triggerCheckDownTrigger = RaycastHit2D.op_Implicit(Physics2D.Linecast(Vector2.op_Implicit(((Component)this).transform.position + new Vector3(0.8f, 2.5f, 0f)), Vector2.op_Implicit(((Component)this).transform.position + new Vector3(0.8f, -4.5f, 0f)), (1 << LayerMask.NameToLayer("Ground")) | (1 << LayerMask.NameToLayer("Platform"))));
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
					}
				}
			}
			if (Input.GetMouseButton(0))
			{
				endY = Input.mousePosition.y;
				if (SlideNaDole && startY - endY > 0.25f * Camera.main.orthographicSize * ((float)Screen.height / (2f * Camera.main.orthographicSize)))
				{
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
					((Component)this).GetComponent<Rigidbody2D>().drag = 7.5f;
				}
			}
			if (KontrolisaniSkok)
			{
				if (Input.GetMouseButton(0) && jumpControlled)
				{
					((Component)this).GetComponent<Rigidbody2D>().velocity = new Vector2(((Component)this).GetComponent<Rigidbody2D>().velocity.x, ((Component)this).GetComponent<Rigidbody2D>().velocity.y + korakce);
					if (korakce > 0f)
					{
						korakce -= 0.085f;
					}
				}
				if ((Input.GetMouseButtonUp(0) || Time.time - duzinaPritiskaZaSkok > 1f) && jumpControlled)
				{
					jumpControlled = false;
					tempForce = jumpForce;
					canGlide = false;
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
					startVelY = ((Component)this).GetComponent<Rigidbody2D>().velocity.y;
					korakce = 3f;
					((Component)this).GetComponent<Rigidbody2D>().velocity = new Vector2(((Component)this).GetComponent<Rigidbody2D>().velocity.x, startVelY + maxSpeedY);
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
		//IL_00e1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00eb: Unknown result type (might be due to invalid IL or missing references)
		//IL_003e: Unknown result type (might be due to invalid IL or missing references)
		//IL_021e: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e3: Unknown result type (might be due to invalid IL or missing references)
		//IL_0126: Unknown result type (might be due to invalid IL or missing references)
		//IL_008b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0056: Unknown result type (might be due to invalid IL or missing references)
		//IL_006c: Unknown result type (might be due to invalid IL or missing references)
		//IL_007b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0259: Unknown result type (might be due to invalid IL or missing references)
		//IL_0173: Unknown result type (might be due to invalid IL or missing references)
		//IL_013e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0154: Unknown result type (might be due to invalid IL or missing references)
		//IL_0163: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_00be: Unknown result type (might be due to invalid IL or missing references)
		//IL_0364: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_023e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0249: Unknown result type (might be due to invalid IL or missing references)
		//IL_0196: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_039f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0302: Unknown result type (might be due to invalid IL or missing references)
		//IL_028a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0294: Unknown result type (might be due to invalid IL or missing references)
		//IL_0425: Unknown result type (might be due to invalid IL or missing references)
		//IL_0434: Unknown result type (might be due to invalid IL or missing references)
		//IL_0384: Unknown result type (might be due to invalid IL or missing references)
		//IL_038f: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e7: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f2: Unknown result type (might be due to invalid IL or missing references)
		//IL_046c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0487: Unknown result type (might be due to invalid IL or missing references)
		//IL_03d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_03da: Unknown result type (might be due to invalid IL or missing references)
		//IL_06e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_06f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_06f9: Unknown result type (might be due to invalid IL or missing references)
		//IL_06fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_0709: Unknown result type (might be due to invalid IL or missing references)
		//IL_071d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0722: Unknown result type (might be due to invalid IL or missing references)
		//IL_0727: Unknown result type (might be due to invalid IL or missing references)
		//IL_073b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0339: Unknown result type (might be due to invalid IL or missing references)
		//IL_0343: Unknown result type (might be due to invalid IL or missing references)
		//IL_056b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0586: Unknown result type (might be due to invalid IL or missing references)
		//IL_04bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_05b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_05c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_05d7: Unknown result type (might be due to invalid IL or missing references)
		//IL_06a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_06aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_06ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_0613: Unknown result type (might be due to invalid IL or missing references)
		//IL_0651: Unknown result type (might be due to invalid IL or missing references)
		//IL_0631: Unknown result type (might be due to invalid IL or missing references)
		//IL_0641: Unknown result type (might be due to invalid IL or missing references)
		//IL_0674: Unknown result type (might be due to invalid IL or missing references)
		//IL_0684: Unknown result type (might be due to invalid IL or missing references)
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
					((Component)this).GetComponent<Rigidbody2D>().AddForce(new Vector2(((Component)this).GetComponent<Rigidbody2D>().velocity.x, 2500f));
				}
				if (Mathf.Abs(((Component)this).GetComponent<Rigidbody2D>().velocity.y) > maxSpeedY)
				{
					((Component)this).GetComponent<Rigidbody2D>().velocity = new Vector2(((Component)this).GetComponent<Rigidbody2D>().velocity.x, maxSpeedY);
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
					((Component)this).GetComponent<Rigidbody2D>().AddForce(new Vector2(((Component)this).GetComponent<Rigidbody2D>().velocity.x, 2500f));
				}
				if (Mathf.Abs(((Component)this).GetComponent<Rigidbody2D>().velocity.y) > maxSpeedY)
				{
					((Component)this).GetComponent<Rigidbody2D>().velocity = new Vector2(((Component)this).GetComponent<Rigidbody2D>().velocity.x, maxSpeedY);
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
			if (swoosh)
			{
				((Component)this).GetComponent<Rigidbody2D>().AddForce(new Vector2(((Component)this).GetComponent<Rigidbody2D>().velocity.x, -3000f));
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
				((Component)this).GetComponent<Rigidbody2D>().AddForce(new Vector2(((Component)this).GetComponent<Rigidbody2D>().velocity.x, jumpForce));
				hasJumped = true;
				jump = false;
			}
			else if (jumpControlled)
			{
				hasJumped = true;
				mozeDaSkociOpet = true;
				if (((Component)this).GetComponent<Rigidbody2D>().velocity.y < maxSpeedY)
				{
					((Component)this).GetComponent<Rigidbody2D>().AddForce(new Vector2(((Component)this).GetComponent<Rigidbody2D>().velocity.x, tempForce));
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
		//IL_0025: Unknown result type (might be due to invalid IL or missing references)
		//IL_0035: Unknown result type (might be due to invalid IL or missing references)
		if (((Component)col).tag == "Footer" && ((Component)this).GetComponent<Collider2D>().isTrigger && ((Component)this).transform.position.y > ((Component)col).transform.position.y)
		{
			((Component)this).GetComponent<Collider2D>().isTrigger = false;
			neTrebaDaProdje = false;
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
		//IL_04df: Unknown result type (might be due to invalid IL or missing references)
		//IL_04ee: Unknown result type (might be due to invalid IL or missing references)
		//IL_050f: Unknown result type (might be due to invalid IL or missing references)
		//IL_03be: Unknown result type (might be due to invalid IL or missing references)
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
			if (!((Component)this).GetComponent<Collider2D>().isTrigger && triggerCheckDownTrigger)
			{
				if (startSpustanje)
				{
					startSpustanje = false;
					cameraTarget_down.transform.parent = ((Component)this).transform;
					cameraTarget_down.transform.position = cameraTarget.transform.position;
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
				canGlide = false;
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
					((Component)this).GetComponent<Rigidbody2D>().AddForce(new Vector2(((Component)this).GetComponent<Rigidbody2D>().velocity.x, 1100f));
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
		//IL_0169: Unknown result type (might be due to invalid IL or missing references)
		//IL_017f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0234: Unknown result type (might be due to invalid IL or missing references)
		//IL_0243: Unknown result type (might be due to invalid IL or missing references)
		//IL_0267: Unknown result type (might be due to invalid IL or missing references)
		if (state == State.completed && state == State.wasted)
		{
			return;
		}
		if (((Component)col).tag == "Barrel")
		{
			((Component)((Component)col).transform.GetChild(0)).GetComponent<Animator>().Play("BarrelBoom");
		}
		else if (((Object)col).name == "Magnet")
		{
			GameObject.Find("_GameManager").SendMessage("ApplyPowerUp", (object)1);
			((Component)col).gameObject.SetActive(false);
		}
		else if (((Object)col).name == "Banana_Coin_X2")
		{
			GameObject.Find("_GameManager").SendMessage("ApplyPowerUp", (object)2);
			((Component)col).gameObject.SetActive(false);
		}
		else if (((Object)col).name == "Banana_Shield")
		{
			GameObject.Find("_GameManager").SendMessage("ApplyPowerUp", (object)3);
			((Component)col).gameObject.SetActive(false);
			activeShield = true;
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
			if (((Component)this).transform.position.y + 0.5f > ((Component)col).transform.position.y && triggerCheckDownTrigger && triggerCheckDownBehind && ((Component)this).GetComponent<Collider2D>().isTrigger)
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
					((Component)this).GetComponent<Rigidbody2D>().AddForce(new Vector2(((Component)this).GetComponent<Rigidbody2D>().velocity.x, 1100f));
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
		((Component)((Component)this).transform.Find("HolderKillStars")).gameObject.SetActive(true);
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
}
