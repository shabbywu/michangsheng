using System.Collections;
using UnityEngine;

public class BabunDogadjaji : MonoBehaviour
{
	public bool IdleUdaranje;

	private Animator anim;

	private Animator parentAnim;

	public GameObject bureRaketa;

	public bool patrol;

	public bool jump;

	public bool fly;

	public bool shooting;

	public bool run;

	public bool runAndJump;

	public bool animateInstantly;

	private BoxCollider2D[] colliders;

	private bool fireEvent = true;

	private bool canJump;

	private bool canShoot;

	private bool proveraJednom = true;

	private bool jumpNow;

	private bool skocioOdmah;

	public ParticleSystem oblak;

	private MonkeyController2D player;

	private Transform babun;

	private Transform reqHeight;

	private int idle_state = Animator.StringToHash("Base Layer.Idle");

	private int idle_udaranje_state = Animator.StringToHash("Base Layer.Idle_Udaranje");

	private int death_state = Animator.StringToHash("Base Layer.Death");

	private int deathJump_state = Animator.StringToHash("Base Layer.DeathJump");

	private int strike_state = Animator.StringToHash("Base Layer.Strike_1");

	private float maxSpeedX = 15f;

	public float distance;

	private Vector3 baboonLocPos;

	private void Awake()
	{
		player = GameObject.FindGameObjectWithTag("Monkey").GetComponent<MonkeyController2D>();
		babun = ((Component)this).transform;
		reqHeight = ((Component)this).transform.Find("BabunHeight");
		anim = ((Component)babun).GetComponent<Animator>();
		colliders = ((Component)this).GetComponents<BoxCollider2D>();
		((Behaviour)anim).enabled = false;
		fireEvent = false;
	}

	private void Start()
	{
		//IL_0033: Unknown result type (might be due to invalid IL or missing references)
		//IL_0038: Unknown result type (might be due to invalid IL or missing references)
		if (!IdleUdaranje)
		{
			anim.Play(idle_state);
		}
		else
		{
			anim.Play(idle_udaranje_state);
		}
		baboonLocPos = ((Component)this).transform.localPosition;
	}

	private void Update()
	{
		//IL_0081: Unknown result type (might be due to invalid IL or missing references)
		//IL_0090: Unknown result type (might be due to invalid IL or missing references)
		//IL_0095: Unknown result type (might be due to invalid IL or missing references)
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		//IL_001d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0022: Unknown result type (might be due to invalid IL or missing references)
		if (runAndJump && ((Component)this).transform.position.x < Camera.main.ViewportToWorldPoint(Vector3.one).x - 7.5f && !skocioOdmah)
		{
			anim.Play("Jump");
			parentAnim.Play("BaboonJumpOnce");
			jumpNow = true;
			skocioOdmah = true;
			anim.SetBool("Land", false);
		}
		if ((((Component)this).transform.position.x < Camera.main.ViewportToWorldPoint(Vector3.one).x + distance || animateInstantly) && proveraJednom)
		{
			proveraJednom = false;
			((Behaviour)anim).enabled = true;
			fireEvent = true;
		}
		if (patrol)
		{
			if (fireEvent)
			{
				fireEvent = false;
				anim.Play("Walking_left");
			}
		}
		else if (run)
		{
			if (fireEvent)
			{
				fireEvent = false;
				anim.Play("Running");
				((Component)this).GetComponent<Rigidbody2D>().isKinematic = false;
			}
		}
		else if (jump)
		{
			if (fireEvent)
			{
				((Component)this).GetComponent<Rigidbody2D>().isKinematic = true;
				fireEvent = false;
				anim.Play("Jump");
				parentAnim.Play("BaboonJump");
			}
		}
		else if (fly)
		{
			if (fireEvent)
			{
				bureRaketa.SetActive(true);
				anim.SetBool("Land", false);
				fireEvent = false;
				((Component)this).GetComponent<Rigidbody2D>().isKinematic = true;
				parentAnim.Play("BaboonFly");
				anim.Play("Fly");
			}
		}
		else if (runAndJump && fireEvent)
		{
			fireEvent = false;
			anim.Play("Running");
			((Component)this).GetComponent<Rigidbody2D>().isKinematic = false;
		}
	}

	private void FixedUpdate()
	{
		//IL_0020: Unknown result type (might be due to invalid IL or missing references)
		//IL_0030: Unknown result type (might be due to invalid IL or missing references)
		//IL_005a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0064: Unknown result type (might be due to invalid IL or missing references)
		if (run || runAndJump)
		{
			((Component)this).GetComponent<Rigidbody2D>().AddForce(new Vector2(-3500f, 0f));
			if (Mathf.Abs(((Component)this).GetComponent<Rigidbody2D>().velocity.x) > maxSpeedX)
			{
				((Component)this).GetComponent<Rigidbody2D>().velocity = new Vector2(0f - maxSpeedX, ((Component)this).GetComponent<Rigidbody2D>().velocity.y);
			}
		}
	}

	private void SobaliJump()
	{
		canJump = false;
	}

	private void OdvaliJump()
	{
		fireEvent = true;
	}

	private IEnumerator Patrol()
	{
		yield return null;
	}

	private void OnTriggerEnter2D(Collider2D col)
	{
		if (((Component)col).gameObject.tag == "Monkey" && player.state != MonkeyController2D.State.wasted)
		{
			Interakcija();
		}
	}

	private void OnCollisionEnter2D(Collision2D col)
	{
		if (runAndJump && col.gameObject.tag == "Footer" && jumpNow)
		{
			anim.SetBool("Land", true);
			jumpNow = false;
			((Component)this).transform.parent = ((Component)this).transform.parent.parent;
			anim.Play("Running");
		}
		if (col.gameObject.tag == "Monkey" && player.state != MonkeyController2D.State.wasted)
		{
			Debug.Log((object)"OVDEJUSO");
			Interakcija();
		}
	}

	private IEnumerator destroyBabun()
	{
		((Component)((Component)this).transform.parent.parent.Find("+3CoinsHolder")).GetComponent<Animator>().Play("FadeOutCoins");
		Manage.coinsCollected += 3;
		GameObject.Find("CoinsGamePlayText").GetComponent<TextMesh>().text = Manage.coinsCollected.ToString();
		yield return (object)new WaitForSeconds(1.2f);
		if (MonkeyController2D.canRespawnThings)
		{
			if (!IdleUdaranje)
			{
				anim.Play(idle_state);
			}
			else
			{
				anim.Play(idle_udaranje_state);
			}
			((Behaviour)colliders[0]).enabled = true;
			((Behaviour)colliders[1]).enabled = true;
			((Component)reqHeight).GetComponent<KillTheBaboon>().turnOnColliders();
			((Component)this).transform.localPosition = baboonLocPos;
		}
	}

	public void killBaboonStuff()
	{
		//IL_0087: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a6: Unknown result type (might be due to invalid IL or missing references)
		((Component)this).GetComponent<Rigidbody2D>().isKinematic = true;
		if (PlaySounds.soundOn)
		{
			PlaySounds.Play_SmashBaboon();
		}
		anim.applyRootMotion = true;
		if (anim.GetBool("Land"))
		{
			anim.Play(death_state);
		}
		else
		{
			jump = false;
			anim.Play(death_state);
		}
		oblak.Play();
		((Component)player).GetComponent<Rigidbody2D>().velocity = new Vector2(player.maxSpeedX, 0f);
		((Component)player).GetComponent<Rigidbody2D>().AddForce(new Vector2(0f, 1500f));
		((Component)player).GetComponent<Rigidbody2D>().drag = 0f;
		player.canGlide = false;
		player.animator.Play(player.jump_State);
		((MonoBehaviour)this).StartCoroutine(destroyBabun());
	}

	private void Interakcija()
	{
		//IL_01d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cd: Unknown result type (might be due to invalid IL or missing references)
		//IL_018d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0130: Unknown result type (might be due to invalid IL or missing references)
		if (player.activeShield)
		{
			Debug.Log((object)"Stitulj");
			((Component)reqHeight).GetComponent<KillTheBaboon>().turnOffColliders();
			((Component)this).GetComponent<Rigidbody2D>().isKinematic = true;
			if (PlaySounds.soundOn)
			{
				PlaySounds.Play_SmashBaboon();
			}
			anim.applyRootMotion = true;
			if (anim.GetBool("Land"))
			{
				anim.Play(death_state);
			}
			else
			{
				jump = false;
				anim.Play(death_state);
			}
			oblak.Play();
			((Behaviour)colliders[0]).enabled = false;
			((Behaviour)colliders[1]).enabled = false;
			((Component)player).GetComponent<Rigidbody2D>().velocity = new Vector2(player.maxSpeedX, 0f);
			if (player.activeShield)
			{
				player.activeShield = false;
				GameObject.Find("_GameManager").SendMessage("ApplyPowerUp", (object)(-3));
				if (player.state != 0)
				{
					((Component)player).GetComponent<Rigidbody2D>().AddForce(new Vector2(0f, 1500f));
					((Component)player).GetComponent<Rigidbody2D>().drag = 0f;
					player.canGlide = false;
					player.animator.Play(player.jump_State);
				}
			}
			else
			{
				((Component)player).GetComponent<Rigidbody2D>().AddForce(new Vector2(0f, 1500f));
			}
			((MonoBehaviour)this).StartCoroutine(destroyBabun());
		}
		else if (!player.killed)
		{
			Debug.Log((object)"nema stitulj");
			if (PlaySounds.soundOn)
			{
				PlaySounds.Play_SmashBaboon();
			}
			((Component)player).GetComponent<Rigidbody2D>().velocity = Vector2.zero;
			player.killed = true;
			maxSpeedX = 0f;
			run = false;
			((Component)this).GetComponent<Rigidbody2D>().isKinematic = true;
			if (anim.GetBool("Land"))
			{
				anim.Play(strike_state);
			}
			oblak.Play();
			if (player.state == MonkeyController2D.State.running)
			{
				player.majmunUtepan();
			}
			else
			{
				player.majmunUtepanULetu();
			}
		}
	}
}
