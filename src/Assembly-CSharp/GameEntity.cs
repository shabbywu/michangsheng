using KBEngine;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class GameEntity : MonoBehaviour
{
	public bool isPlayer;

	public bool isTestOffLine;

	private Vector3 _position = Vector3.zero;

	private Vector3 _eulerAngles = Vector3.zero;

	private Vector3 _scale = Vector3.zero;

	public Vector3 destPosition = Vector3.zero;

	public Vector3 destDirection = Vector3.zero;

	private float _speed = 50f;

	private byte jumpState;

	private float currY = 1f;

	private float zhentime;

	private int fps;

	private Camera playerCamera;

	public string entity_name;

	public int hp;

	public int hpMax;

	public int sp;

	public int spMax;

	public bool canAttack;

	private float npcHeight = 3f;

	public CharacterController characterController;

	public bool isOnGround = true;

	public bool entityEnabled = true;

	public Text headName;

	private Animator animator;

	private CharacterController controller;

	private float last_angleY;

	private Vector3 last_position;

	public Vector3 position
	{
		get
		{
			//IL_0001: Unknown result type (might be due to invalid IL or missing references)
			return _position;
		}
		set
		{
			//IL_0001: Unknown result type (might be due to invalid IL or missing references)
			//IL_0002: Unknown result type (might be due to invalid IL or missing references)
			//IL_0021: Unknown result type (might be due to invalid IL or missing references)
			_position = value;
			if ((Object)(object)((Component)this).gameObject != (Object)null)
			{
				((Component)this).gameObject.transform.position = _position;
			}
		}
	}

	public Vector3 eulerAngles
	{
		get
		{
			//IL_0001: Unknown result type (might be due to invalid IL or missing references)
			return _eulerAngles;
		}
		set
		{
			//IL_0001: Unknown result type (might be due to invalid IL or missing references)
			//IL_0002: Unknown result type (might be due to invalid IL or missing references)
			//IL_0021: Unknown result type (might be due to invalid IL or missing references)
			_eulerAngles = value;
			if ((Object)(object)((Component)this).gameObject != (Object)null)
			{
				((Component)this).gameObject.transform.eulerAngles = _eulerAngles;
			}
		}
	}

	public Quaternion rotation
	{
		get
		{
			//IL_0001: Unknown result type (might be due to invalid IL or missing references)
			//IL_0006: Unknown result type (might be due to invalid IL or missing references)
			return Quaternion.Euler(_eulerAngles);
		}
		set
		{
			//IL_0003: Unknown result type (might be due to invalid IL or missing references)
			eulerAngles = ((Quaternion)(ref value)).eulerAngles;
		}
	}

	public Vector3 scale
	{
		get
		{
			//IL_0001: Unknown result type (might be due to invalid IL or missing references)
			return _scale;
		}
		set
		{
			//IL_0001: Unknown result type (might be due to invalid IL or missing references)
			//IL_0002: Unknown result type (might be due to invalid IL or missing references)
			//IL_0021: Unknown result type (might be due to invalid IL or missing references)
			_scale = value;
			if ((Object)(object)((Component)this).gameObject != (Object)null)
			{
				((Component)this).gameObject.transform.localScale = _scale;
			}
		}
	}

	public float speed
	{
		get
		{
			return _speed;
		}
		set
		{
			_speed = value;
		}
	}

	private void Awake()
	{
	}

	private void Start()
	{
		characterController = ((Component)this).gameObject.GetComponent<CharacterController>();
		animator = ((Component)((Component)this).transform.GetChild(0)).GetComponent<Animator>();
		controller = ((Component)this).GetComponent<CharacterController>();
	}

	public void entityEnable()
	{
		entityEnabled = true;
	}

	public void entityDisable()
	{
		entityEnabled = false;
	}

	private void FixedUpdate()
	{
		//IL_0039: Unknown result type (might be due to invalid IL or missing references)
		//IL_0056: Unknown result type (might be due to invalid IL or missing references)
		//IL_0073: Unknown result type (might be due to invalid IL or missing references)
		//IL_0090: Unknown result type (might be due to invalid IL or missing references)
		//IL_0095: Unknown result type (might be due to invalid IL or missing references)
		//IL_0098: Unknown result type (might be due to invalid IL or missing references)
		if (entityEnabled && (isPlayer || KBEngineApp.app == null) && !isTestOffLine)
		{
			object[] obj = new object[4]
			{
				((Component)this).gameObject.transform.position.x,
				((Component)this).gameObject.transform.position.y,
				((Component)this).gameObject.transform.position.z,
				null
			};
			Quaternion val = ((Component)this).gameObject.transform.rotation;
			obj[3] = ((Quaternion)(ref val)).eulerAngles.y;
			Event.fireIn("updatePlayer", obj);
		}
	}

	private void Update()
	{
		//IL_007d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0083: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_016c: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_013c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0141: Unknown result type (might be due to invalid IL or missing references)
		//IL_0150: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ca: Unknown result type (might be due to invalid IL or missing references)
		if ((Object)(object)headName != (Object)null)
		{
			headName.text = entity_name;
		}
		if (!entityEnabled)
		{
			return;
		}
		_ = speed;
		_ = Time.deltaTime;
		if (isPlayer)
		{
			if (isOnGround != characterController.isGrounded)
			{
				KBEngineApp.app.player().isOnGround = characterController.isGrounded;
				isOnGround = characterController.isGrounded;
			}
			return;
		}
		if (Vector3.Distance(eulerAngles, destDirection) > 0.0004f)
		{
			((Component)this).transform.rotation = Quaternion.Slerp(((Component)this).transform.rotation, Quaternion.Euler(destDirection), 1f);
		}
		float num = Vector3.Distance(destPosition, position);
		if (jumpState > 0)
		{
			if (jumpState == 1)
			{
				currY += 0.05f;
				if (currY > 2f)
				{
					jumpState = 2;
				}
			}
			else
			{
				currY -= 0.05f;
				if (currY < 1f)
				{
					jumpState = 0;
					currY = 1f;
				}
			}
			Vector3 val = position;
			val.y = currY;
			position = val;
		}
		if (num > 0.01f)
		{
			((Component)this).gameObject.GetComponent<NavMeshAgent>().SetDestination(destPosition);
			if ((Object)(object)animator == (Object)null)
			{
				animator = ((Component)((Component)this).transform.GetChild(0)).GetComponent<Animator>();
			}
			if (destPosition.x != ((Component)this).transform.position.x || destPosition.z != ((Component)this).transform.position.z)
			{
				zhentime = 0.2f;
				animator.speed = 1f;
				animator.SetFloat("Speed", 1f);
			}
			else if (zhentime <= 0f)
			{
				animator.speed = 1f;
				animator.SetFloat("Speed", 0f);
			}
			else
			{
				zhentime -= Time.deltaTime;
			}
		}
	}

	public void OnJump()
	{
		Debug.Log((object)("jumpState: " + jumpState));
		if (jumpState == 0)
		{
			jumpState = 1;
		}
	}
}
