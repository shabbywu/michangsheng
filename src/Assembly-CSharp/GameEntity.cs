using System;
using KBEngine;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

// Token: 0x020002F8 RID: 760
public class GameEntity : MonoBehaviour
{
	// Token: 0x060016E1 RID: 5857 RVA: 0x000042DD File Offset: 0x000024DD
	private void Awake()
	{
	}

	// Token: 0x060016E2 RID: 5858 RVA: 0x000143FC File Offset: 0x000125FC
	private void Start()
	{
		this.characterController = base.gameObject.GetComponent<CharacterController>();
		this.animator = base.transform.GetChild(0).GetComponent<Animator>();
		this.controller = base.GetComponent<CharacterController>();
	}

	// Token: 0x17000272 RID: 626
	// (get) Token: 0x060016E3 RID: 5859 RVA: 0x00014432 File Offset: 0x00012632
	// (set) Token: 0x060016E4 RID: 5860 RVA: 0x0001443A File Offset: 0x0001263A
	public Vector3 position
	{
		get
		{
			return this._position;
		}
		set
		{
			this._position = value;
			if (base.gameObject != null)
			{
				base.gameObject.transform.position = this._position;
			}
		}
	}

	// Token: 0x17000273 RID: 627
	// (get) Token: 0x060016E5 RID: 5861 RVA: 0x00014467 File Offset: 0x00012667
	// (set) Token: 0x060016E6 RID: 5862 RVA: 0x0001446F File Offset: 0x0001266F
	public Vector3 eulerAngles
	{
		get
		{
			return this._eulerAngles;
		}
		set
		{
			this._eulerAngles = value;
			if (base.gameObject != null)
			{
				base.gameObject.transform.eulerAngles = this._eulerAngles;
			}
		}
	}

	// Token: 0x17000274 RID: 628
	// (get) Token: 0x060016E7 RID: 5863 RVA: 0x0001449C File Offset: 0x0001269C
	// (set) Token: 0x060016E8 RID: 5864 RVA: 0x000144A9 File Offset: 0x000126A9
	public Quaternion rotation
	{
		get
		{
			return Quaternion.Euler(this._eulerAngles);
		}
		set
		{
			this.eulerAngles = value.eulerAngles;
		}
	}

	// Token: 0x17000275 RID: 629
	// (get) Token: 0x060016E9 RID: 5865 RVA: 0x000144B8 File Offset: 0x000126B8
	// (set) Token: 0x060016EA RID: 5866 RVA: 0x000144C0 File Offset: 0x000126C0
	public Vector3 scale
	{
		get
		{
			return this._scale;
		}
		set
		{
			this._scale = value;
			if (base.gameObject != null)
			{
				base.gameObject.transform.localScale = this._scale;
			}
		}
	}

	// Token: 0x17000276 RID: 630
	// (get) Token: 0x060016EB RID: 5867 RVA: 0x000144ED File Offset: 0x000126ED
	// (set) Token: 0x060016EC RID: 5868 RVA: 0x000144F5 File Offset: 0x000126F5
	public float speed
	{
		get
		{
			return this._speed;
		}
		set
		{
			this._speed = value;
		}
	}

	// Token: 0x060016ED RID: 5869 RVA: 0x000144FE File Offset: 0x000126FE
	public void entityEnable()
	{
		this.entityEnabled = true;
	}

	// Token: 0x060016EE RID: 5870 RVA: 0x00014507 File Offset: 0x00012707
	public void entityDisable()
	{
		this.entityEnabled = false;
	}

	// Token: 0x060016EF RID: 5871 RVA: 0x000CBB2C File Offset: 0x000C9D2C
	private void FixedUpdate()
	{
		if (!this.entityEnabled)
		{
			return;
		}
		if ((!this.isPlayer && KBEngineApp.app != null) || this.isTestOffLine)
		{
			return;
		}
		Event.fireIn("updatePlayer", new object[]
		{
			base.gameObject.transform.position.x,
			base.gameObject.transform.position.y,
			base.gameObject.transform.position.z,
			base.gameObject.transform.rotation.eulerAngles.y
		});
	}

	// Token: 0x060016F0 RID: 5872 RVA: 0x000CBBE8 File Offset: 0x000C9DE8
	private void Update()
	{
		if (this.headName != null)
		{
			this.headName.text = this.entity_name;
		}
		if (!this.entityEnabled)
		{
			return;
		}
		float speed = this.speed;
		float deltaTime = Time.deltaTime;
		if (this.isPlayer)
		{
			if (this.isOnGround != this.characterController.isGrounded)
			{
				KBEngineApp.app.player().isOnGround = this.characterController.isGrounded;
				this.isOnGround = this.characterController.isGrounded;
			}
			return;
		}
		if (Vector3.Distance(this.eulerAngles, this.destDirection) > 0.0004f)
		{
			base.transform.rotation = Quaternion.Slerp(base.transform.rotation, Quaternion.Euler(this.destDirection), 1f);
		}
		float num = Vector3.Distance(this.destPosition, this.position);
		if (this.jumpState > 0)
		{
			if (this.jumpState == 1)
			{
				this.currY += 0.05f;
				if (this.currY > 2f)
				{
					this.jumpState = 2;
				}
			}
			else
			{
				this.currY -= 0.05f;
				if (this.currY < 1f)
				{
					this.jumpState = 0;
					this.currY = 1f;
				}
			}
			Vector3 position = this.position;
			position.y = this.currY;
			this.position = position;
		}
		if (num > 0.01f)
		{
			base.gameObject.GetComponent<NavMeshAgent>().SetDestination(this.destPosition);
			if (this.animator == null)
			{
				this.animator = base.transform.GetChild(0).GetComponent<Animator>();
			}
			if (this.destPosition.x != base.transform.position.x || this.destPosition.z != base.transform.position.z)
			{
				this.zhentime = 0.2f;
				this.animator.speed = 1f;
				this.animator.SetFloat("Speed", 1f);
				return;
			}
			if (this.zhentime <= 0f)
			{
				this.animator.speed = 1f;
				this.animator.SetFloat("Speed", 0f);
				return;
			}
			this.zhentime -= Time.deltaTime;
		}
	}

	// Token: 0x060016F1 RID: 5873 RVA: 0x00014510 File Offset: 0x00012710
	public void OnJump()
	{
		Debug.Log("jumpState: " + this.jumpState);
		if (this.jumpState != 0)
		{
			return;
		}
		this.jumpState = 1;
	}

	// Token: 0x0400123E RID: 4670
	public bool isPlayer;

	// Token: 0x0400123F RID: 4671
	public bool isTestOffLine;

	// Token: 0x04001240 RID: 4672
	private Vector3 _position = Vector3.zero;

	// Token: 0x04001241 RID: 4673
	private Vector3 _eulerAngles = Vector3.zero;

	// Token: 0x04001242 RID: 4674
	private Vector3 _scale = Vector3.zero;

	// Token: 0x04001243 RID: 4675
	public Vector3 destPosition = Vector3.zero;

	// Token: 0x04001244 RID: 4676
	public Vector3 destDirection = Vector3.zero;

	// Token: 0x04001245 RID: 4677
	private float _speed = 50f;

	// Token: 0x04001246 RID: 4678
	private byte jumpState;

	// Token: 0x04001247 RID: 4679
	private float currY = 1f;

	// Token: 0x04001248 RID: 4680
	private float zhentime;

	// Token: 0x04001249 RID: 4681
	private int fps;

	// Token: 0x0400124A RID: 4682
	private Camera playerCamera;

	// Token: 0x0400124B RID: 4683
	public string entity_name;

	// Token: 0x0400124C RID: 4684
	public int hp;

	// Token: 0x0400124D RID: 4685
	public int hpMax;

	// Token: 0x0400124E RID: 4686
	public int sp;

	// Token: 0x0400124F RID: 4687
	public int spMax;

	// Token: 0x04001250 RID: 4688
	public bool canAttack;

	// Token: 0x04001251 RID: 4689
	private float npcHeight = 3f;

	// Token: 0x04001252 RID: 4690
	public CharacterController characterController;

	// Token: 0x04001253 RID: 4691
	public bool isOnGround = true;

	// Token: 0x04001254 RID: 4692
	public bool entityEnabled = true;

	// Token: 0x04001255 RID: 4693
	public Text headName;

	// Token: 0x04001256 RID: 4694
	private Animator animator;

	// Token: 0x04001257 RID: 4695
	private CharacterController controller;

	// Token: 0x04001258 RID: 4696
	private float last_angleY;

	// Token: 0x04001259 RID: 4697
	private Vector3 last_position;
}
