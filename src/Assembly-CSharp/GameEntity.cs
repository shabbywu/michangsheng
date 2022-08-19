using System;
using KBEngine;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

// Token: 0x020001E5 RID: 485
public class GameEntity : MonoBehaviour
{
	// Token: 0x0600143D RID: 5181 RVA: 0x00004095 File Offset: 0x00002295
	private void Awake()
	{
	}

	// Token: 0x0600143E RID: 5182 RVA: 0x000829F4 File Offset: 0x00080BF4
	private void Start()
	{
		this.characterController = base.gameObject.GetComponent<CharacterController>();
		this.animator = base.transform.GetChild(0).GetComponent<Animator>();
		this.controller = base.GetComponent<CharacterController>();
	}

	// Token: 0x1700022A RID: 554
	// (get) Token: 0x0600143F RID: 5183 RVA: 0x00082A2A File Offset: 0x00080C2A
	// (set) Token: 0x06001440 RID: 5184 RVA: 0x00082A32 File Offset: 0x00080C32
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

	// Token: 0x1700022B RID: 555
	// (get) Token: 0x06001441 RID: 5185 RVA: 0x00082A5F File Offset: 0x00080C5F
	// (set) Token: 0x06001442 RID: 5186 RVA: 0x00082A67 File Offset: 0x00080C67
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

	// Token: 0x1700022C RID: 556
	// (get) Token: 0x06001443 RID: 5187 RVA: 0x00082A94 File Offset: 0x00080C94
	// (set) Token: 0x06001444 RID: 5188 RVA: 0x00082AA1 File Offset: 0x00080CA1
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

	// Token: 0x1700022D RID: 557
	// (get) Token: 0x06001445 RID: 5189 RVA: 0x00082AB0 File Offset: 0x00080CB0
	// (set) Token: 0x06001446 RID: 5190 RVA: 0x00082AB8 File Offset: 0x00080CB8
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

	// Token: 0x1700022E RID: 558
	// (get) Token: 0x06001447 RID: 5191 RVA: 0x00082AE5 File Offset: 0x00080CE5
	// (set) Token: 0x06001448 RID: 5192 RVA: 0x00082AED File Offset: 0x00080CED
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

	// Token: 0x06001449 RID: 5193 RVA: 0x00082AF6 File Offset: 0x00080CF6
	public void entityEnable()
	{
		this.entityEnabled = true;
	}

	// Token: 0x0600144A RID: 5194 RVA: 0x00082AFF File Offset: 0x00080CFF
	public void entityDisable()
	{
		this.entityEnabled = false;
	}

	// Token: 0x0600144B RID: 5195 RVA: 0x00082B08 File Offset: 0x00080D08
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

	// Token: 0x0600144C RID: 5196 RVA: 0x00082BC4 File Offset: 0x00080DC4
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

	// Token: 0x0600144D RID: 5197 RVA: 0x00082E1D File Offset: 0x0008101D
	public void OnJump()
	{
		Debug.Log("jumpState: " + this.jumpState);
		if (this.jumpState != 0)
		{
			return;
		}
		this.jumpState = 1;
	}

	// Token: 0x04000F00 RID: 3840
	public bool isPlayer;

	// Token: 0x04000F01 RID: 3841
	public bool isTestOffLine;

	// Token: 0x04000F02 RID: 3842
	private Vector3 _position = Vector3.zero;

	// Token: 0x04000F03 RID: 3843
	private Vector3 _eulerAngles = Vector3.zero;

	// Token: 0x04000F04 RID: 3844
	private Vector3 _scale = Vector3.zero;

	// Token: 0x04000F05 RID: 3845
	public Vector3 destPosition = Vector3.zero;

	// Token: 0x04000F06 RID: 3846
	public Vector3 destDirection = Vector3.zero;

	// Token: 0x04000F07 RID: 3847
	private float _speed = 50f;

	// Token: 0x04000F08 RID: 3848
	private byte jumpState;

	// Token: 0x04000F09 RID: 3849
	private float currY = 1f;

	// Token: 0x04000F0A RID: 3850
	private float zhentime;

	// Token: 0x04000F0B RID: 3851
	private int fps;

	// Token: 0x04000F0C RID: 3852
	private Camera playerCamera;

	// Token: 0x04000F0D RID: 3853
	public string entity_name;

	// Token: 0x04000F0E RID: 3854
	public int hp;

	// Token: 0x04000F0F RID: 3855
	public int hpMax;

	// Token: 0x04000F10 RID: 3856
	public int sp;

	// Token: 0x04000F11 RID: 3857
	public int spMax;

	// Token: 0x04000F12 RID: 3858
	public bool canAttack;

	// Token: 0x04000F13 RID: 3859
	private float npcHeight = 3f;

	// Token: 0x04000F14 RID: 3860
	public CharacterController characterController;

	// Token: 0x04000F15 RID: 3861
	public bool isOnGround = true;

	// Token: 0x04000F16 RID: 3862
	public bool entityEnabled = true;

	// Token: 0x04000F17 RID: 3863
	public Text headName;

	// Token: 0x04000F18 RID: 3864
	private Animator animator;

	// Token: 0x04000F19 RID: 3865
	private CharacterController controller;

	// Token: 0x04000F1A RID: 3866
	private float last_angleY;

	// Token: 0x04000F1B RID: 3867
	private Vector3 last_position;
}
