using System.Collections;
using UnityEngine;

public class BabunDogadjaji_new : MonoBehaviour
{
	public bool IdleUdaranje;

	private Animator anim;

	private Animator parentAnim;

	public GameObject bureRaketa;

	public Transform senka;

	public bool patrol;

	public bool jump;

	public bool fly;

	public bool shooting;

	public bool run;

	public bool runAndJump;

	public bool animateInstantly;

	private CircleCollider2D[] colliders;

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

	public float maxSpeedX = 17f;

	public float distance;

	private Vector3 baboonLocPos;

	private Vector2 pravac;

	private Vector2 pravacFly;

	private bool patrolinjo;

	private bool flyinjo;

	private bool runinjo;

	private Rigidbody2D parentRigidbody2D;

	private Transform baboonShadow;

	private RaycastHit2D hit;

	public bool canDo;

	public Vector3 baboonRealOrgPos;

	private bool kontrolaZaBrzinuY;

	private float smanjivac;

	private bool ugasen = true;

	private bool impact;

	private TextMesh coinsCollectedText;

	public GameObject Koplje;

	public GameObject Boomerang;

	public bool koplje;

	public bool udaranjePoGrudi;

	public bool boomerang;

	private bool isGorilla;

	private int kolicinaPoena;

	private bool runTurnedOff;

	private void Awake()
	{
		//IL_0091: Unknown result type (might be due to invalid IL or missing references)
		//IL_0096: Unknown result type (might be due to invalid IL or missing references)
		//IL_009b: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00da: Unknown result type (might be due to invalid IL or missing references)
		//IL_00df: Unknown result type (might be due to invalid IL or missing references)
		player = GameObject.FindGameObjectWithTag("Monkey").GetComponent<MonkeyController2D>();
		coinsCollectedText = GameObject.Find("CoinsGamePlayText").GetComponent<TextMesh>();
		babun = ((Component)this).transform;
		reqHeight = ((Component)this).transform.parent.Find("_BabunNadrlja");
		anim = ((Component)babun.parent).GetComponent<Animator>();
		colliders = ((Component)this).GetComponents<CircleCollider2D>();
		fireEvent = false;
		parentRigidbody2D = ((Component)((Component)this).transform.parent).GetComponent<Rigidbody2D>();
		pravac = -Vector2.right;
		pravacFly = Vector2.up;
		if (runAndJump)
		{
			baboonShadow = ((Component)this).transform.parent.Find("shadow");
		}
		baboonRealOrgPos = ((Component)this).transform.parent.localPosition;
		if (((Object)((Component)this).transform.parent.parent).name.Contains("Gorilla"))
		{
			isGorilla = true;
		}
		PogasiBabuna();
	}

	private void Start()
	{
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
		//IL_00d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ea: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ef: Unknown result type (might be due to invalid IL or missing references)
		//IL_0095: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d7: Unknown result type (might be due to invalid IL or missing references)
		//IL_01dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0121: Unknown result type (might be due to invalid IL or missing references)
		//IL_0130: Unknown result type (might be due to invalid IL or missing references)
		//IL_0135: Unknown result type (might be due to invalid IL or missing references)
		//IL_0177: Unknown result type (might be due to invalid IL or missing references)
		//IL_0199: Unknown result type (might be due to invalid IL or missing references)
		hit = Physics2D.Linecast(Vector2.op_Implicit(((Component)this).transform.position + new Vector3(0.8f, 1f, 0f)), Vector2.op_Implicit(((Component)this).transform.position + new Vector3(0.8f, -35.5f, 0f)), (1 << LayerMask.NameToLayer("Ground")) | (1 << LayerMask.NameToLayer("Platform")));
		if (RaycastHit2D.op_Implicit(hit))
		{
			senka.position = new Vector3(senka.position.x, ((RaycastHit2D)(ref hit)).point.y - 0f, senka.position.z);
		}
		if (((Component)this).transform.position.x + 5f < Camera.main.ViewportToWorldPoint(Vector3.zero).x && !ugasen)
		{
			ugasen = true;
			PogasiBabuna();
		}
		if (runAndJump && ((Component)this).transform.position.x < Camera.main.ViewportToWorldPoint(Vector3.one).x - 30f && !skocioOdmah)
		{
			anim.Play("Jump");
			parentRigidbody2D.velocity = new Vector2((0f - maxSpeedX) * 0.7f, 0f);
			parentRigidbody2D.velocity = new Vector2((0f - maxSpeedX) * 0.7f, 43f);
			jumpNow = true;
			skocioOdmah = true;
			anim.SetBool("Land", false);
		}
		if ((((Component)this).transform.position.x < Camera.main.ViewportToWorldPoint(Vector3.one).x + distance || animateInstantly) && proveraJednom)
		{
			proveraJednom = false;
			((Behaviour)anim).enabled = true;
			fireEvent = true;
			ugasen = false;
		}
		((Object)((Component)this).transform.parent).name.Equals("BaboonRealll");
		if (patrol)
		{
			if (fireEvent)
			{
				fireEvent = false;
				parentRigidbody2D.isKinematic = false;
				anim.Play("Walking_left");
				patrolinjo = true;
				((MonoBehaviour)this).InvokeRepeating("ObrniSe", 2.65f, 2.65f);
			}
		}
		else if (run)
		{
			if (fireEvent)
			{
				fireEvent = false;
				anim.Play("Running");
				parentRigidbody2D.isKinematic = false;
				runinjo = true;
			}
		}
		else if (jump)
		{
			if (fireEvent)
			{
				fireEvent = false;
				anim.Play("Jump2");
				anim.SetBool("Land", false);
				anim.applyRootMotion = true;
			}
		}
		else if (fly)
		{
			if (fireEvent)
			{
				bureRaketa.SetActive(true);
				anim.SetBool("Land", false);
				fireEvent = false;
				anim.Play("Fly");
				flyinjo = true;
				((MonoBehaviour)this).InvokeRepeating("ObrniSeVertikalno", 1.5f, 1.5f);
			}
		}
		else if (runAndJump)
		{
			if (fireEvent)
			{
				fireEvent = false;
				anim.Play("Running");
				parentRigidbody2D.isKinematic = false;
				runinjo = true;
			}
		}
		else if (IdleUdaranje)
		{
			if (fireEvent)
			{
				fireEvent = false;
				if (Random.Range(1, 3) > 1)
				{
					anim.Play(idle_udaranje_state);
				}
				else
				{
					anim.Play(idle_state);
				}
			}
		}
		else if (udaranjePoGrudi)
		{
			if (fireEvent)
			{
				fireEvent = false;
				anim.Play("Chest_drum");
			}
		}
		else if (koplje)
		{
			if (fireEvent)
			{
				fireEvent = false;
				anim.Play("koplje");
			}
		}
		else if (boomerang && fireEvent)
		{
			fireEvent = false;
			anim.Play("Boomerang");
		}
	}

	private void FixedUpdate()
	{
		//IL_0028: Unknown result type (might be due to invalid IL or missing references)
		//IL_0032: Unknown result type (might be due to invalid IL or missing references)
		//IL_0042: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ef: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_008b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0070: Unknown result type (might be due to invalid IL or missing references)
		//IL_007a: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_0171: Unknown result type (might be due to invalid IL or missing references)
		//IL_017b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0148: Unknown result type (might be due to invalid IL or missing references)
		//IL_0152: Unknown result type (might be due to invalid IL or missing references)
		//IL_0197: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_01de: Unknown result type (might be due to invalid IL or missing references)
		//IL_0202: Unknown result type (might be due to invalid IL or missing references)
		//IL_0207: Unknown result type (might be due to invalid IL or missing references)
		//IL_020d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0225: Unknown result type (might be due to invalid IL or missing references)
		//IL_0235: Unknown result type (might be due to invalid IL or missing references)
		//IL_024b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0255: Unknown result type (might be due to invalid IL or missing references)
		if (patrolinjo)
		{
			parentRigidbody2D.velocity = new Vector2(pravac.x * 5f, parentRigidbody2D.velocity.y);
			if (parentRigidbody2D.velocity.y > 2f)
			{
				parentRigidbody2D.velocity = new Vector2(pravac.x * 6.25f, parentRigidbody2D.velocity.y);
			}
			else if (parentRigidbody2D.velocity.y < -2f)
			{
				parentRigidbody2D.velocity = new Vector2(pravac.x * 3.75f, parentRigidbody2D.velocity.y);
			}
		}
		else if (flyinjo)
		{
			((Component)this).transform.parent.Translate(Vector2.op_Implicit(pravacFly * Time.deltaTime * 3f));
		}
		else
		{
			if ((!run && !runAndJump) || !runinjo)
			{
				return;
			}
			if (jumpNow)
			{
				parentRigidbody2D.velocity = new Vector2((0f - maxSpeedX) * 0.45f, parentRigidbody2D.velocity.y);
			}
			else
			{
				parentRigidbody2D.velocity = new Vector2(0f - maxSpeedX, parentRigidbody2D.velocity.y);
			}
			if (runAndJump)
			{
				hit = Physics2D.Linecast(Vector2.op_Implicit(((Component)this).transform.position + new Vector3(0.8f, 1f, 0f)), Vector2.op_Implicit(((Component)this).transform.position + new Vector3(0.8f, -35.5f, 0f)), (1 << LayerMask.NameToLayer("Ground")) | (1 << LayerMask.NameToLayer("Platform")));
				if (RaycastHit2D.op_Implicit(hit))
				{
					baboonShadow.position = new Vector3(baboonShadow.position.x, ((RaycastHit2D)(ref hit)).point.y + 0.3f, baboonShadow.position.z);
				}
			}
		}
	}

	private void ObrniSe()
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		pravac = -pravac;
		anim.SetBool("changeSide", !anim.GetBool("changeSide"));
	}

	private void ObrniSeVertikalno()
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		pravacFly = -pravacFly;
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
		else if (((Object)col).name == "Impact")
		{
			impact = true;
			((Component)reqHeight).GetComponent<KillTheBaboon>().turnOffColliders();
			killBaboonStuff();
		}
	}

	private void OnCollisionEnter2D(Collision2D col)
	{
		if (runAndJump && col.gameObject.tag == "Footer" && jumpNow)
		{
			anim.SetBool("Land", true);
			jumpNow = false;
			anim.Play("Running");
		}
		if (col.gameObject.tag == "Monkey" && player.state != MonkeyController2D.State.wasted)
		{
			Interakcija();
		}
	}

	private IEnumerator destroyBabun()
	{
		if (isGorilla)
		{
			Manage.gorillasKilled++;
			MissionManager.Instance.GorillaEvent(Manage.gorillasKilled);
			kolicinaPoena = 40;
			if (fly)
			{
				Manage.fly_GorillasKilled++;
				MissionManager.Instance.Fly_GorillaEvent(Manage.fly_GorillasKilled);
				kolicinaPoena = 60;
			}
			else if (koplje)
			{
				Manage.koplje_GorillasKilled++;
				MissionManager.Instance.Koplje_GorillaEvent(Manage.koplje_GorillasKilled);
				kolicinaPoena = 70;
				((Behaviour)Koplje.GetComponent<Collider2D>()).enabled = false;
			}
			else if (jump)
			{
				kolicinaPoena = 50;
			}
			else if (patrol)
			{
				kolicinaPoena = 40;
			}
			else if (run)
			{
				kolicinaPoena = 70;
			}
			else if (runAndJump)
			{
				kolicinaPoena = 80;
			}
		}
		else
		{
			Manage.baboonsKilled++;
			MissionManager.Instance.BaboonEvent(Manage.baboonsKilled);
			kolicinaPoena = 40;
			if (fly)
			{
				Manage.fly_BaboonsKilled++;
				MissionManager.Instance.Fly_BaboonEvent(Manage.fly_BaboonsKilled);
				kolicinaPoena = 60;
			}
			else if (boomerang)
			{
				Manage.boomerang_BaboonsKilled++;
				MissionManager.Instance.Boomerang_BaboonEvent(Manage.boomerang_BaboonsKilled);
				kolicinaPoena = 70;
				Boomerang.SetActive(false);
			}
			else if (jump)
			{
				kolicinaPoena = 50;
			}
			else if (patrol)
			{
				kolicinaPoena = 40;
			}
			else if (run)
			{
				kolicinaPoena = 70;
			}
			else if (runAndJump)
			{
				kolicinaPoena = 80;
			}
		}
		if (fly || run)
		{
			((Component)senka).GetComponent<Renderer>().enabled = false;
		}
		if (PlaySounds.soundOn)
		{
			PlaySounds.Play_SmashBaboon();
		}
		int num = 3;
		if (jump)
		{
			num = 4;
		}
		else if (fly)
		{
			num = 4;
		}
		else if (patrol)
		{
			num = 3;
		}
		else if (run)
		{
			num = 4;
		}
		else if (runAndJump)
		{
			num = 5;
		}
		else if (boomerang || koplje)
		{
			num = 6;
		}
		Transform val = ((Component)this).transform.parent.Find("+3CoinsHolder");
		TextMesh component = ((Component)val.Find("+3Coins")).GetComponent<TextMesh>();
		string text2 = (((Component)val.Find("+3Coins/+3CoinsShadow")).GetComponent<TextMesh>().text = "+" + num);
		component.text = text2;
		val.parent = ((Component)this).transform.parent.parent;
		((Component)val).GetComponent<Animator>().Play("FadeOutCoins");
		Manage.coinsCollected += num;
		MissionManager.Instance.CoinEvent(Manage.coinsCollected);
		coinsCollectedText.text = Manage.coinsCollected.ToString();
		((Component)coinsCollectedText).GetComponent<TextMeshEffects>().RefreshTextOutline(adjustTextSize: false, hasWhiteSpaces: true);
		Manage.points += kolicinaPoena;
		Manage.pointsText.text = Manage.points.ToString();
		Manage.pointsEffects.RefreshTextOutline(adjustTextSize: false, hasWhiteSpaces: true);
		yield return (object)new WaitForSeconds(1.2f);
		((Component)this).transform.parent.parent.Find("+3CoinsHolder").parent = ((Component)this).transform.parent;
	}

	public void killBaboonStuff()
	{
		if (player.powerfullImpact)
		{
			player.cancelPowerfullImpact();
		}
		if (patrolinjo)
		{
			patrolinjo = false;
		}
		else if (flyinjo)
		{
			flyinjo = false;
		}
		else if (runinjo)
		{
			runinjo = false;
		}
		if ((Object)(object)parentRigidbody2D != (Object)null)
		{
			parentRigidbody2D.isKinematic = true;
		}
		anim.applyRootMotion = true;
		if (anim.GetBool("Land"))
		{
			if (!runAndJump)
			{
				anim.Play(death_state);
			}
			else
			{
				anim.Play(deathJump_state);
				((MonoBehaviour)this).Invoke("UgasiBabunaPoslePada", 1f);
			}
		}
		else
		{
			anim.Play(deathJump_state);
			((MonoBehaviour)this).Invoke("UgasiBabunaPoslePada", 1f);
		}
		oblak.Play();
		((Behaviour)colliders[0]).enabled = false;
		((Behaviour)colliders[1]).enabled = false;
		if (!impact)
		{
			((MonoBehaviour)this).StartCoroutine(bounceOffEnemy());
		}
		else
		{
			impact = false;
		}
		((Component)player).GetComponent<Rigidbody2D>().drag = 0f;
		player.canGlide = false;
		((MonoBehaviour)this).StartCoroutine(destroyBabun());
	}

	private IEnumerator bounceOffEnemy()
	{
		yield return (object)new WaitForFixedUpdate();
		if (!player.isSliding)
		{
			((Component)player).GetComponent<Rigidbody2D>().velocity = new Vector2(player.maxSpeedX, 0f);
			((Component)player).GetComponent<Rigidbody2D>().AddForce(new Vector2(0f, 1500f));
			player.animator.Play(player.jump_State);
		}
		else if (player.state != 0)
		{
			((Component)player).GetComponent<Rigidbody2D>().velocity = new Vector2(player.maxSpeedX, 0f);
			((Component)player).GetComponent<Rigidbody2D>().AddForce(new Vector2(0f, -2500f));
		}
	}

	private void Interakcija()
	{
		//IL_0117: Unknown result type (might be due to invalid IL or missing references)
		//IL_0261: Unknown result type (might be due to invalid IL or missing references)
		//IL_0222: Unknown result type (might be due to invalid IL or missing references)
		if (patrolinjo)
		{
			patrolinjo = false;
		}
		else if (flyinjo)
		{
			flyinjo = false;
		}
		if (player.activeShield || player.invincible || player.powerfullImpact)
		{
			if (runinjo)
			{
				runinjo = false;
			}
			((Component)reqHeight).GetComponent<KillTheBaboon>().turnOffColliders();
			if ((Object)(object)parentRigidbody2D != (Object)null)
			{
				parentRigidbody2D.isKinematic = true;
			}
			anim.applyRootMotion = true;
			if (anim.GetBool("Land"))
			{
				anim.Play(death_state);
			}
			else
			{
				anim.Play(deathJump_state);
				((MonoBehaviour)this).Invoke("UgasiBabunaPoslePada", 1f);
			}
			oblak.Play();
			((Behaviour)colliders[0]).enabled = false;
			((Behaviour)colliders[1]).enabled = false;
			((Component)player).GetComponent<Rigidbody2D>().velocity = new Vector2(player.maxSpeedX, 0f);
			if (player.activeShield)
			{
				if (PlaySounds.soundOn)
				{
					PlaySounds.Play_LooseShield();
				}
				player.activeShield = false;
				((Component)((Component)player).transform.Find("Particles/ShieldDestroyParticle")).GetComponent<ParticleSystem>().Play();
				((Component)((Component)player).transform.Find("Particles/ShieldDestroyParticle").GetChild(0)).GetComponent<ParticleSystem>().Play();
				GameObject.Find("_GameManager").SendMessage("ApplyPowerUp", (object)(-3));
				if (player.state != 0)
				{
					((Component)player).GetComponent<Rigidbody2D>().drag = 0f;
					player.canGlide = false;
					player.animator.Play(player.jump_State);
				}
			}
			else if (!player.isSliding && player.state != 0)
			{
				((Component)player).GetComponent<Rigidbody2D>().AddForce(new Vector2(0f, 1500f));
			}
			((MonoBehaviour)this).StartCoroutine(destroyBabun());
		}
		else
		{
			if (player.killed)
			{
				return;
			}
			if (PlaySounds.soundOn)
			{
				PlaySounds.Play_SmashBaboon();
			}
			((Component)player).GetComponent<Rigidbody2D>().velocity = Vector2.zero;
			player.killed = true;
			if (run)
			{
				run = false;
				runTurnedOff = true;
			}
			((Component)reqHeight).GetComponent<KillTheBaboon>().turnOffColliders();
			((Behaviour)colliders[0]).enabled = false;
			((Behaviour)colliders[1]).enabled = false;
			if (anim.GetBool("Land"))
			{
				if (!run)
				{
					anim.Play(strike_state);
					if ((Object)(object)parentRigidbody2D != (Object)null)
					{
						parentRigidbody2D.isKinematic = true;
					}
					maxSpeedX = 0f;
				}
			}
			else
			{
				((MonoBehaviour)this).Invoke("UkljuciCollidereOpet", 0.35f);
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
			kontrolaZaBrzinuY = true;
		}
	}

	private void UkljuciCollidereOpet()
	{
		((Behaviour)colliders[0]).enabled = true;
		((Behaviour)colliders[1]).enabled = true;
	}

	private void resetujKontroluZaBrzinu()
	{
		kontrolaZaBrzinuY = false;
	}

	private void PogasiBabuna()
	{
		//IL_0063: Unknown result type (might be due to invalid IL or missing references)
		//IL_0068: Unknown result type (might be due to invalid IL or missing references)
		//IL_006d: Unknown result type (might be due to invalid IL or missing references)
		if (fly)
		{
			if (bureRaketa.activeSelf)
			{
				bureRaketa.SetActive(false);
			}
			((MonoBehaviour)this).CancelInvoke("ObrniSeVertikalno");
		}
		else if (patrol)
		{
			((MonoBehaviour)this).CancelInvoke("ObrniSe");
			anim.Play("New State");
			anim.SetBool("changeSide", false);
			pravac = -Vector2.right;
		}
		else if (jump)
		{
			anim.Play("New State");
		}
		if ((Object)(object)parentRigidbody2D != (Object)null)
		{
			parentRigidbody2D.isKinematic = true;
		}
		((Behaviour)colliders[0]).enabled = false;
		((Behaviour)colliders[1]).enabled = false;
		((Component)((Component)this).transform.parent.Find("Baboon")).GetComponent<Renderer>().enabled = false;
		((Behaviour)anim).enabled = false;
		patrolinjo = false;
		flyinjo = false;
		runinjo = false;
		anim.applyRootMotion = false;
		((Behaviour)this).enabled = false;
	}

	private void UgasiBabunaPoslePada()
	{
		oblak.Play();
		((Component)((Component)this).transform.parent.Find("Baboon")).GetComponent<Renderer>().enabled = false;
		if (fly)
		{
			bureRaketa.SetActive(false);
		}
	}

	public void ResetujBabuna()
	{
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		((Component)this).transform.parent.localPosition = baboonRealOrgPos;
		((Behaviour)colliders[0]).enabled = true;
		((Behaviour)colliders[1]).enabled = true;
		((Component)reqHeight).GetComponent<KillTheBaboon>().turnOnColliders();
		((Component)((Component)this).transform.parent.Find("Baboon")).GetComponent<Renderer>().enabled = true;
		((Behaviour)anim).enabled = true;
		((Behaviour)this).enabled = true;
		ugasen = false;
		proveraJednom = true;
		skocioOdmah = false;
		if (fly)
		{
			bureRaketa.SetActive(true);
		}
		if (boomerang)
		{
			Boomerang.SetActive(true);
		}
		if (koplje)
		{
			((Behaviour)Koplje.GetComponent<Collider2D>()).enabled = true;
		}
		if (runTurnedOff)
		{
			runTurnedOff = false;
			run = true;
		}
		if (fly || run)
		{
			((Component)senka).GetComponent<Renderer>().enabled = true;
		}
		maxSpeedX = 17f;
	}
}
