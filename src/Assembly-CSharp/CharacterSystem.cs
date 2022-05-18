using System;
using UnityEngine;

// Token: 0x02000150 RID: 336
[RequireComponent(typeof(CharacterMotor))]
[RequireComponent(typeof(CharacterStatus))]
[RequireComponent(typeof(CharacterAttack))]
[RequireComponent(typeof(CharacterInventory))]
public class CharacterSystem : MonoBehaviour
{
	// Token: 0x06000C28 RID: 3112 RVA: 0x0000E2E9 File Offset: 0x0000C4E9
	private void Start()
	{
		this.motor = base.gameObject.GetComponent<CharacterMotor>();
		base.gameObject.GetComponent<Animation>().CrossFade(this.PoseIdle);
		this.attacking = false;
	}

	// Token: 0x06000C29 RID: 3113 RVA: 0x000964C0 File Offset: 0x000946C0
	private void Update()
	{
		if (this.ComboAttackLists.Length == 0)
		{
			return;
		}
		this.comboList = this.ComboAttackLists[this.WeaponType].Split(new char[]
		{
			","[0]
		});
		if (this.comboList.Length > this.attackStep)
		{
			int num = int.Parse(this.comboList[this.attackStep]);
			if (num < this.PoseAttackNames.Length)
			{
				AnimationState animationState = base.gameObject.GetComponent<Animation>()[this.PoseAttackNames[num]];
				animationState.layer = 2;
				animationState.blendMode = 0;
				animationState.speed = this.SpeedAttack;
				if (animationState.time >= animationState.length * 0.1f)
				{
					this.attacking = true;
				}
				if (animationState.time >= this.PoseAttackTime[num] && !this.diddamaged)
				{
					base.gameObject.GetComponent<CharacterAttack>().DoDamage();
				}
				if (animationState.time >= animationState.length * 0.8f)
				{
					animationState.normalizedTime = animationState.length;
					this.diddamaged = true;
					this.attacking = false;
					this.attackStep++;
					if (this.attackStack > 1)
					{
						this.fightAnimation();
					}
					else if (this.attackStep >= this.comboList.Length)
					{
						this.resetCombo();
						base.gameObject.GetComponent<Animation>().Play(this.PoseIdle);
					}
					base.gameObject.GetComponent<CharacterAttack>().StartDamage();
				}
			}
		}
		if (this.hited)
		{
			if (this.frozetime > 0f)
			{
				this.frozetime -= 1f;
			}
			else
			{
				this.hited = false;
				base.gameObject.GetComponent<Animation>().Play(this.PoseIdle);
			}
		}
		if (Time.time > this.attackStackTimeTemp + 2f)
		{
			this.resetCombo();
		}
	}

	// Token: 0x06000C2A RID: 3114 RVA: 0x00096698 File Offset: 0x00094898
	public void GotHit(float time)
	{
		if (!this.IsHero)
		{
			if (this.PoseHitNames.Length != 0)
			{
				base.gameObject.GetComponent<Animation>().Play(this.PoseHitNames[Random.Range(0, this.PoseHitNames.Length)], 4);
			}
			this.frozetime = time * Time.deltaTime;
			this.hited = true;
		}
	}

	// Token: 0x06000C2B RID: 3115 RVA: 0x0000E319 File Offset: 0x0000C519
	private void resetCombo()
	{
		this.attackStep = 0;
		this.attackStack = 0;
	}

	// Token: 0x06000C2C RID: 3116 RVA: 0x000966F4 File Offset: 0x000948F4
	private void fightAnimation()
	{
		this.attacking = false;
		if (this.attackStep >= this.comboList.Length)
		{
			this.resetCombo();
		}
		int num = int.Parse(this.comboList[this.attackStep]);
		if (num < this.PoseAttackNames.Length)
		{
			if (base.gameObject.GetComponent<CharacterAttack>())
			{
				base.gameObject.GetComponent<Animation>().Play(this.PoseAttackNames[num], 4);
			}
			this.diddamaged = false;
		}
	}

	// Token: 0x06000C2D RID: 3117 RVA: 0x0000E329 File Offset: 0x0000C529
	public void Attack()
	{
		if (this.frozetime <= 0f)
		{
			this.attackStackTimeTemp = Time.time;
			this.fightAnimation();
			this.attackStack++;
		}
	}

	// Token: 0x06000C2E RID: 3118 RVA: 0x0000E357 File Offset: 0x0000C557
	public void Move(Vector3 dir)
	{
		if (!this.attacking)
		{
			this.moveDirection = dir;
			return;
		}
		this.moveDirection = dir / 2f;
	}

	// Token: 0x170001ED RID: 493
	// (get) Token: 0x06000C2F RID: 3119 RVA: 0x0000E37A File Offset: 0x0000C57A
	// (set) Token: 0x06000C30 RID: 3120 RVA: 0x00096770 File Offset: 0x00094970
	private Vector3 moveDirection
	{
		get
		{
			return this.direction;
		}
		set
		{
			this.direction = value;
			if (this.direction.magnitude > 0.1f)
			{
				Quaternion quaternion = Quaternion.LookRotation(this.direction);
				base.transform.rotation = Quaternion.Slerp(base.transform.rotation, quaternion, Time.deltaTime * this.TurnSpeed);
			}
			this.direction *= this.Speed * 0.5f * (Vector3.Dot(base.gameObject.transform.forward, this.direction) + 1f);
			if (this.direction.magnitude > 0.001f)
			{
				float num = this.direction.magnitude * 3f;
				base.gameObject.GetComponent<Animation>().CrossFade(this.PoseRun);
				if (num < 1f)
				{
					num = 1f;
				}
				base.gameObject.GetComponent<Animation>()[this.PoseRun].speed = num;
			}
			else
			{
				base.gameObject.GetComponent<Animation>().CrossFade(this.PoseIdle);
			}
			if (this.motor)
			{
				this.motor.inputMoveDirection = this.direction;
			}
		}
	}

	// Token: 0x06000C31 RID: 3121 RVA: 0x000968A4 File Offset: 0x00094AA4
	private void OnControllerColliderHit(ControllerColliderHit hit)
	{
		Rigidbody attachedRigidbody = hit.collider.attachedRigidbody;
		if (attachedRigidbody == null || attachedRigidbody.isKinematic)
		{
			return;
		}
		if ((double)hit.moveDirection.y < -0.3)
		{
			return;
		}
		Vector3 vector = Vector3.Scale(hit.moveDirection, new Vector3(1f, 0f, 1f));
		attachedRigidbody.velocity = vector * this.pushPower;
	}

	// Token: 0x04000938 RID: 2360
	public float Speed = 2f;

	// Token: 0x04000939 RID: 2361
	public float SpeedAttack = 1.5f;

	// Token: 0x0400093A RID: 2362
	public float TurnSpeed = 5f;

	// Token: 0x0400093B RID: 2363
	public float[] PoseAttackTime;

	// Token: 0x0400093C RID: 2364
	public string[] PoseAttackNames;

	// Token: 0x0400093D RID: 2365
	public string[] ComboAttackLists;

	// Token: 0x0400093E RID: 2366
	public string[] PoseHitNames;

	// Token: 0x0400093F RID: 2367
	public int WeaponType;

	// Token: 0x04000940 RID: 2368
	public string PoseIdle = "Idle";

	// Token: 0x04000941 RID: 2369
	public string PoseRun = "Run";

	// Token: 0x04000942 RID: 2370
	public bool IsHero;

	// Token: 0x04000943 RID: 2371
	private bool diddamaged;

	// Token: 0x04000944 RID: 2372
	private int attackStep;

	// Token: 0x04000945 RID: 2373
	private string[] comboList;

	// Token: 0x04000946 RID: 2374
	private int attackStack;

	// Token: 0x04000947 RID: 2375
	private float attackStackTimeTemp;

	// Token: 0x04000948 RID: 2376
	private float frozetime;

	// Token: 0x04000949 RID: 2377
	private bool hited;

	// Token: 0x0400094A RID: 2378
	private bool attacking;

	// Token: 0x0400094B RID: 2379
	private CharacterMotor motor;

	// Token: 0x0400094C RID: 2380
	private Vector3 direction;

	// Token: 0x0400094D RID: 2381
	private float pushPower = 2f;
}
