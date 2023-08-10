using UnityEngine;

namespace UltimateSurvival;

[RequireComponent(typeof(FPObject))]
public class FPMotion : PlayerBehaviour
{
	[Header("Setup")]
	[SerializeField]
	private Transform m_Model;

	[SerializeField]
	private Transform m_Pivot;

	[Header("Sway")]
	[SerializeField]
	private Sway m_MovementSway;

	[SerializeField]
	private Sway m_RotationSway;

	[Header("Bob")]
	[SerializeField]
	private TrigonometricBob m_WalkBob;

	[SerializeField]
	private TrigonometricBob m_AimBob;

	[SerializeField]
	private TrigonometricBob m_RunBob;

	[SerializeField]
	private LerpControlledBob m_LandBob;

	[SerializeField]
	private float m_MaxLandSpeed = 12f;

	[Header("Offset")]
	[SerializeField]
	private TransformOffset m_IdleOffset;

	[SerializeField]
	private TransformOffset m_RunOffset;

	[SerializeField]
	private TransformOffset m_AimOffset;

	[SerializeField]
	private TransformOffset m_OnLadderOffset;

	[SerializeField]
	private TransformOffset m_JumpOffset;

	[SerializeField]
	[Tooltip("The object position and rotation offset, when the character is too close to an object. NOTE: Will not be taken into consideration if the object can be used when near other objects (see the 'CanUseWhileNearObjects' setting).")]
	private TransformOffset m_TooCloseOffset;

	private Transform m_Root;

	private FPObject m_Object;

	private FPWeaponBase m_Weapon;

	private TransformOffset m_CurrentOffset;

	private bool m_HolsterActive;

	private void Awake()
	{
		m_Object = ((Component)this).GetComponent<FPObject>();
		m_Weapon = m_Object as FPWeaponBase;
		m_Object.Draw.AddListener(On_Draw);
		m_Object.Holster.AddListener(On_Holster);
		SetupTransforms();
		base.Player.Land.AddListener(On_Land);
		m_CurrentOffset = m_IdleOffset;
	}

	private void On_Draw()
	{
		m_IdleOffset.Reset();
		m_CurrentOffset = m_IdleOffset;
		m_HolsterActive = false;
	}

	private void On_Holster()
	{
		m_HolsterActive = true;
	}

	private void On_Land(float landSpeed)
	{
		if (m_Object.IsEnabled && ((Component)this).gameObject.activeInHierarchy)
		{
			((MonoBehaviour)this).StartCoroutine(m_LandBob.DoBobCycle(landSpeed / m_MaxLandSpeed));
		}
	}

	private void SetupTransforms()
	{
		//IL_0005: Unknown result type (might be due to invalid IL or missing references)
		//IL_0028: Unknown result type (might be due to invalid IL or missing references)
		//IL_0039: Unknown result type (might be due to invalid IL or missing references)
		Transform transform = new GameObject("Root").transform;
		((Component)transform).transform.SetParent(((Component)this).transform);
		transform.position = m_Pivot.position;
		transform.rotation = m_Pivot.rotation;
		m_Pivot.SetParent(transform, true);
		m_Model.SetParent(m_Pivot, true);
	}

	private void Update()
	{
		//IL_001f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0024: Unknown result type (might be due to invalid IL or missing references)
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		//IL_001c: Unknown result type (might be due to invalid IL or missing references)
		//IL_002b: Unknown result type (might be due to invalid IL or missing references)
		//IL_002c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0041: Unknown result type (might be due to invalid IL or missing references)
		//IL_0047: Unknown result type (might be due to invalid IL or missing references)
		//IL_004e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0069: Unknown result type (might be due to invalid IL or missing references)
		//IL_006e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0084: Unknown result type (might be due to invalid IL or missing references)
		//IL_0089: Unknown result type (might be due to invalid IL or missing references)
		//IL_008e: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ec: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fd: Unknown result type (might be due to invalid IL or missing references)
		//IL_0102: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00df: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0141: Unknown result type (might be due to invalid IL or missing references)
		//IL_014d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0152: Unknown result type (might be due to invalid IL or missing references)
		//IL_0157: Unknown result type (might be due to invalid IL or missing references)
		//IL_0127: Unknown result type (might be due to invalid IL or missing references)
		//IL_0134: Unknown result type (might be due to invalid IL or missing references)
		//IL_0139: Unknown result type (might be due to invalid IL or missing references)
		//IL_013e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0184: Unknown result type (might be due to invalid IL or missing references)
		//IL_0190: Unknown result type (might be due to invalid IL or missing references)
		//IL_0195: Unknown result type (might be due to invalid IL or missing references)
		//IL_019a: Unknown result type (might be due to invalid IL or missing references)
		//IL_016a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0177: Unknown result type (might be due to invalid IL or missing references)
		//IL_017c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0181: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_01be: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_0327: Unknown result type (might be due to invalid IL or missing references)
		//IL_032c: Unknown result type (might be due to invalid IL or missing references)
		//IL_032e: Unknown result type (might be due to invalid IL or missing references)
		//IL_033f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0344: Unknown result type (might be due to invalid IL or missing references)
		//IL_0346: Unknown result type (might be due to invalid IL or missing references)
		Vector2 val = ((!MonoSingleton<InventoryController>.Instance.IsClosed) ? Vector2.zero : base.Player.LookInput.Get());
		m_MovementSway.CalculateSway(-val, Time.deltaTime);
		m_RotationSway.CalculateSway(new Vector2(val.y, 0f - val.x), Time.deltaTime);
		m_Pivot.localPosition = Vector2.op_Implicit(m_MovementSway.Value);
		m_Pivot.localRotation = Quaternion.Euler(Vector2.op_Implicit(m_RotationSway.Value));
		Vector3 val2 = base.Player.Velocity.Get();
		float magnitude = ((Vector3)(ref val2)).magnitude;
		Vector3 zero = Vector3.zero;
		zero = ((!base.Player.Aim.Active || !(magnitude > 1f)) ? (zero + m_AimBob.Cooldown(Time.deltaTime)) : (zero + m_AimBob.CalculateBob(magnitude, Time.deltaTime)));
		zero = ((!base.Player.Walk.Active || base.Player.Aim.Active) ? (zero + m_WalkBob.Cooldown(Time.deltaTime)) : (zero + m_WalkBob.CalculateBob(magnitude, Time.deltaTime)));
		zero = ((!base.Player.Run.Active) ? (zero + m_RunBob.Cooldown(Time.deltaTime)) : (zero + m_RunBob.CalculateBob(magnitude, Time.deltaTime)));
		Transform pivot = m_Pivot;
		pivot.localPosition += zero;
		Transform pivot2 = m_Pivot;
		pivot2.localPosition += Vector3.up * m_LandBob.Value;
		bool flag = Object.op_Implicit((Object)(object)m_Weapon) && !m_Weapon.UseWhileNearObjects && base.Player.IsCloseToAnObject.Get() && base.Player.RaycastData.Get().GameObject.layer != LayerMask.NameToLayer("Hitbox") && !base.Player.RaycastData.Get().GameObject.CompareTag("Ladder");
		if (m_HolsterActive)
		{
			TryChangeOffset(m_IdleOffset);
		}
		else if (base.Player.NearLadders.Count > 0)
		{
			TryChangeOffset(m_OnLadderOffset);
		}
		else if (flag)
		{
			TryChangeOffset(m_TooCloseOffset);
		}
		else if (base.Player.Run.Active)
		{
			TryChangeOffset(m_RunOffset);
		}
		else if (base.Player.Aim.Active)
		{
			TryChangeOffset(m_AimOffset);
		}
		else if (!base.Player.IsGrounded.Get())
		{
			TryChangeOffset(m_JumpOffset);
		}
		else
		{
			TryChangeOffset(m_IdleOffset);
		}
		m_CurrentOffset.Update(Time.deltaTime, out var position, out var rotation);
		Transform pivot3 = m_Pivot;
		pivot3.localPosition += position;
		Transform pivot4 = m_Pivot;
		pivot4.localRotation *= rotation;
	}

	private void TryChangeOffset(TransformOffset newOffset)
	{
		if (m_CurrentOffset != newOffset)
		{
			newOffset.ContinueFrom(m_CurrentOffset);
			m_CurrentOffset = newOffset;
		}
	}
}
