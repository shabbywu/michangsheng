using System;
using UnityEngine;

// Token: 0x020000D9 RID: 217
[RequireComponent(typeof(CharacterMotor))]
[RequireComponent(typeof(CharacterStatus))]
[RequireComponent(typeof(CharacterAttack))]
[RequireComponent(typeof(CharacterInventory))]
public class CharacterSystem : MonoBehaviour
{
	// Token: 0x06000B39 RID: 2873 RVA: 0x00044713 File Offset: 0x00042913
	private void Start()
	{
		this.motor = base.gameObject.GetComponent<CharacterMotor>();
		base.gameObject.GetComponent<Animation>().CrossFade(this.PoseIdle);
		this.attacking = false;
	}

	// Token: 0x06000B3A RID: 2874 RVA: 0x00044744 File Offset: 0x00042944
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

	// Token: 0x06000B3B RID: 2875 RVA: 0x0004491C File Offset: 0x00042B1C
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

	// Token: 0x06000B3C RID: 2876 RVA: 0x00044975 File Offset: 0x00042B75
	private void resetCombo()
	{
		this.attackStep = 0;
		this.attackStack = 0;
	}

	// Token: 0x06000B3D RID: 2877 RVA: 0x00044988 File Offset: 0x00042B88
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

	// Token: 0x06000B3E RID: 2878 RVA: 0x00044A03 File Offset: 0x00042C03
	public void Attack()
	{
		if (this.frozetime <= 0f)
		{
			this.attackStackTimeTemp = Time.time;
			this.fightAnimation();
			this.attackStack++;
		}
	}

	// Token: 0x06000B3F RID: 2879 RVA: 0x00044A31 File Offset: 0x00042C31
	public void Move(Vector3 dir)
	{
		if (!this.attacking)
		{
			this.moveDirection = dir;
			return;
		}
		this.moveDirection = dir / 2f;
	}

	// Token: 0x170001D2 RID: 466
	// (get) Token: 0x06000B40 RID: 2880 RVA: 0x00044A54 File Offset: 0x00042C54
	// (set) Token: 0x06000B41 RID: 2881 RVA: 0x00044A5C File Offset: 0x00042C5C
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

	// Token: 0x06000B42 RID: 2882 RVA: 0x00044B90 File Offset: 0x00042D90
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

	// Token: 0x0400075D RID: 1885
	public float Speed = 2f;

	// Token: 0x0400075E RID: 1886
	public float SpeedAttack = 1.5f;

	// Token: 0x0400075F RID: 1887
	public float TurnSpeed = 5f;

	// Token: 0x04000760 RID: 1888
	public float[] PoseAttackTime;

	// Token: 0x04000761 RID: 1889
	public string[] PoseAttackNames;

	// Token: 0x04000762 RID: 1890
	public string[] ComboAttackLists;

	// Token: 0x04000763 RID: 1891
	public string[] PoseHitNames;

	// Token: 0x04000764 RID: 1892
	public int WeaponType;

	// Token: 0x04000765 RID: 1893
	public string PoseIdle = "Idle";

	// Token: 0x04000766 RID: 1894
	public string PoseRun = "Run";

	// Token: 0x04000767 RID: 1895
	public bool IsHero;

	// Token: 0x04000768 RID: 1896
	private bool diddamaged;

	// Token: 0x04000769 RID: 1897
	private int attackStep;

	// Token: 0x0400076A RID: 1898
	private string[] comboList;

	// Token: 0x0400076B RID: 1899
	private int attackStack;

	// Token: 0x0400076C RID: 1900
	private float attackStackTimeTemp;

	// Token: 0x0400076D RID: 1901
	private float frozetime;

	// Token: 0x0400076E RID: 1902
	private bool hited;

	// Token: 0x0400076F RID: 1903
	private bool attacking;

	// Token: 0x04000770 RID: 1904
	private CharacterMotor motor;

	// Token: 0x04000771 RID: 1905
	private Vector3 direction;

	// Token: 0x04000772 RID: 1906
	private float pushPower = 2f;
}
