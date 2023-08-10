using UnityEngine;

[RequireComponent(typeof(CharacterMotor))]
[RequireComponent(typeof(CharacterStatus))]
[RequireComponent(typeof(CharacterAttack))]
[RequireComponent(typeof(CharacterInventory))]
public class CharacterSystem : MonoBehaviour
{
	public float Speed = 2f;

	public float SpeedAttack = 1.5f;

	public float TurnSpeed = 5f;

	public float[] PoseAttackTime;

	public string[] PoseAttackNames;

	public string[] ComboAttackLists;

	public string[] PoseHitNames;

	public int WeaponType;

	public string PoseIdle = "Idle";

	public string PoseRun = "Run";

	public bool IsHero;

	private bool diddamaged;

	private int attackStep;

	private string[] comboList;

	private int attackStack;

	private float attackStackTimeTemp;

	private float frozetime;

	private bool hited;

	private bool attacking;

	private CharacterMotor motor;

	private Vector3 direction;

	private float pushPower = 2f;

	private Vector3 moveDirection
	{
		get
		{
			//IL_0001: Unknown result type (might be due to invalid IL or missing references)
			return direction;
		}
		set
		{
			//IL_0001: Unknown result type (might be due to invalid IL or missing references)
			//IL_0002: Unknown result type (might be due to invalid IL or missing references)
			//IL_004f: Unknown result type (might be due to invalid IL or missing references)
			//IL_006b: Unknown result type (might be due to invalid IL or missing references)
			//IL_0071: Unknown result type (might be due to invalid IL or missing references)
			//IL_0082: Unknown result type (might be due to invalid IL or missing references)
			//IL_0087: Unknown result type (might be due to invalid IL or missing references)
			//IL_001a: Unknown result type (might be due to invalid IL or missing references)
			//IL_001f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0024: Unknown result type (might be due to invalid IL or missing references)
			//IL_0031: Unknown result type (might be due to invalid IL or missing references)
			//IL_0036: Unknown result type (might be due to invalid IL or missing references)
			//IL_0043: Unknown result type (might be due to invalid IL or missing references)
			//IL_011c: Unknown result type (might be due to invalid IL or missing references)
			//IL_0121: Unknown result type (might be due to invalid IL or missing references)
			direction = value;
			if (((Vector3)(ref direction)).magnitude > 0.1f)
			{
				Quaternion val = Quaternion.LookRotation(direction);
				((Component)this).transform.rotation = Quaternion.Slerp(((Component)this).transform.rotation, val, Time.deltaTime * TurnSpeed);
			}
			direction *= Speed * 0.5f * (Vector3.Dot(((Component)this).gameObject.transform.forward, direction) + 1f);
			if (((Vector3)(ref direction)).magnitude > 0.001f)
			{
				float num = ((Vector3)(ref direction)).magnitude * 3f;
				((Component)this).gameObject.GetComponent<Animation>().CrossFade(PoseRun);
				if (num < 1f)
				{
					num = 1f;
				}
				((Component)this).gameObject.GetComponent<Animation>()[PoseRun].speed = num;
			}
			else
			{
				((Component)this).gameObject.GetComponent<Animation>().CrossFade(PoseIdle);
			}
			if (Object.op_Implicit((Object)(object)motor))
			{
				motor.inputMoveDirection = direction;
			}
		}
	}

	private void Start()
	{
		motor = ((Component)this).gameObject.GetComponent<CharacterMotor>();
		((Component)this).gameObject.GetComponent<Animation>().CrossFade(PoseIdle);
		attacking = false;
	}

	private void Update()
	{
		if (ComboAttackLists.Length == 0)
		{
			return;
		}
		comboList = ComboAttackLists[WeaponType].Split(new char[1] { ","[0] });
		if (comboList.Length > attackStep)
		{
			int num = int.Parse(comboList[attackStep]);
			if (num < PoseAttackNames.Length)
			{
				AnimationState val = ((Component)this).gameObject.GetComponent<Animation>()[PoseAttackNames[num]];
				val.layer = 2;
				val.blendMode = (AnimationBlendMode)0;
				val.speed = SpeedAttack;
				if (val.time >= val.length * 0.1f)
				{
					attacking = true;
				}
				if (val.time >= PoseAttackTime[num] && !diddamaged)
				{
					((Component)this).gameObject.GetComponent<CharacterAttack>().DoDamage();
				}
				if (val.time >= val.length * 0.8f)
				{
					val.normalizedTime = val.length;
					diddamaged = true;
					attacking = false;
					attackStep++;
					if (attackStack > 1)
					{
						fightAnimation();
					}
					else if (attackStep >= comboList.Length)
					{
						resetCombo();
						((Component)this).gameObject.GetComponent<Animation>().Play(PoseIdle);
					}
					((Component)this).gameObject.GetComponent<CharacterAttack>().StartDamage();
				}
			}
		}
		if (hited)
		{
			if (frozetime > 0f)
			{
				frozetime -= 1f;
			}
			else
			{
				hited = false;
				((Component)this).gameObject.GetComponent<Animation>().Play(PoseIdle);
			}
		}
		if (Time.time > attackStackTimeTemp + 2f)
		{
			resetCombo();
		}
	}

	public void GotHit(float time)
	{
		if (!IsHero)
		{
			if (PoseHitNames.Length != 0)
			{
				((Component)this).gameObject.GetComponent<Animation>().Play(PoseHitNames[Random.Range(0, PoseHitNames.Length)], (PlayMode)4);
			}
			frozetime = time * Time.deltaTime;
			hited = true;
		}
	}

	private void resetCombo()
	{
		attackStep = 0;
		attackStack = 0;
	}

	private void fightAnimation()
	{
		attacking = false;
		if (attackStep >= comboList.Length)
		{
			resetCombo();
		}
		int num = int.Parse(comboList[attackStep]);
		if (num < PoseAttackNames.Length)
		{
			if (Object.op_Implicit((Object)(object)((Component)this).gameObject.GetComponent<CharacterAttack>()))
			{
				((Component)this).gameObject.GetComponent<Animation>().Play(PoseAttackNames[num], (PlayMode)4);
			}
			diddamaged = false;
		}
	}

	public void Attack()
	{
		if (frozetime <= 0f)
		{
			attackStackTimeTemp = Time.time;
			fightAnimation();
			attackStack++;
		}
	}

	public void Move(Vector3 dir)
	{
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		//IL_0009: Unknown result type (might be due to invalid IL or missing references)
		if (!attacking)
		{
			moveDirection = dir;
		}
		else
		{
			moveDirection = dir / 2f;
		}
	}

	private void OnControllerColliderHit(ControllerColliderHit hit)
	{
		//IL_001f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0037: Unknown result type (might be due to invalid IL or missing references)
		//IL_004b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0050: Unknown result type (might be due to invalid IL or missing references)
		//IL_0055: Unknown result type (might be due to invalid IL or missing references)
		//IL_0057: Unknown result type (might be due to invalid IL or missing references)
		//IL_005e: Unknown result type (might be due to invalid IL or missing references)
		Rigidbody attachedRigidbody = hit.collider.attachedRigidbody;
		if (!((Object)(object)attachedRigidbody == (Object)null) && !attachedRigidbody.isKinematic && !((double)hit.moveDirection.y < -0.3))
		{
			Vector3 val = Vector3.Scale(hit.moveDirection, new Vector3(1f, 0f, 1f));
			attachedRigidbody.velocity = val * pushPower;
		}
	}
}
