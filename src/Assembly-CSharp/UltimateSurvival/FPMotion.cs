using System;
using UnityEngine;

namespace UltimateSurvival
{
	// Token: 0x020008C4 RID: 2244
	[RequireComponent(typeof(FPObject))]
	public class FPMotion : PlayerBehaviour
	{
		// Token: 0x060039BC RID: 14780 RVA: 0x001A695C File Offset: 0x001A4B5C
		private void Awake()
		{
			this.m_Object = base.GetComponent<FPObject>();
			this.m_Weapon = (this.m_Object as FPWeaponBase);
			this.m_Object.Draw.AddListener(new Action(this.On_Draw));
			this.m_Object.Holster.AddListener(new Action(this.On_Holster));
			this.SetupTransforms();
			base.Player.Land.AddListener(new Action<float>(this.On_Land));
			this.m_CurrentOffset = this.m_IdleOffset;
		}

		// Token: 0x060039BD RID: 14781 RVA: 0x00029E80 File Offset: 0x00028080
		private void On_Draw()
		{
			this.m_IdleOffset.Reset();
			this.m_CurrentOffset = this.m_IdleOffset;
			this.m_HolsterActive = false;
		}

		// Token: 0x060039BE RID: 14782 RVA: 0x00029EA0 File Offset: 0x000280A0
		private void On_Holster()
		{
			this.m_HolsterActive = true;
		}

		// Token: 0x060039BF RID: 14783 RVA: 0x00029EA9 File Offset: 0x000280A9
		private void On_Land(float landSpeed)
		{
			if (this.m_Object.IsEnabled && base.gameObject.activeInHierarchy)
			{
				base.StartCoroutine(this.m_LandBob.DoBobCycle(landSpeed / this.m_MaxLandSpeed));
			}
		}

		// Token: 0x060039C0 RID: 14784 RVA: 0x001A69EC File Offset: 0x001A4BEC
		private void SetupTransforms()
		{
			Transform transform = new GameObject("Root").transform;
			transform.transform.SetParent(base.transform);
			transform.position = this.m_Pivot.position;
			transform.rotation = this.m_Pivot.rotation;
			this.m_Pivot.SetParent(transform, true);
			this.m_Model.SetParent(this.m_Pivot, true);
		}

		// Token: 0x060039C1 RID: 14785 RVA: 0x001A6A5C File Offset: 0x001A4C5C
		private void Update()
		{
			Vector2 vector;
			if (MonoSingleton<InventoryController>.Instance.IsClosed)
			{
				vector = base.Player.LookInput.Get();
			}
			else
			{
				vector = Vector2.zero;
			}
			this.m_MovementSway.CalculateSway(-vector, Time.deltaTime);
			this.m_RotationSway.CalculateSway(new Vector2(vector.y, -vector.x), Time.deltaTime);
			this.m_Pivot.localPosition = this.m_MovementSway.Value;
			this.m_Pivot.localRotation = Quaternion.Euler(this.m_RotationSway.Value);
			float magnitude = base.Player.Velocity.Get().magnitude;
			Vector3 vector2 = Vector3.zero;
			if (base.Player.Aim.Active && magnitude > 1f)
			{
				vector2 += this.m_AimBob.CalculateBob(magnitude, Time.deltaTime);
			}
			else
			{
				vector2 += this.m_AimBob.Cooldown(Time.deltaTime);
			}
			if (base.Player.Walk.Active && !base.Player.Aim.Active)
			{
				vector2 += this.m_WalkBob.CalculateBob(magnitude, Time.deltaTime);
			}
			else
			{
				vector2 += this.m_WalkBob.Cooldown(Time.deltaTime);
			}
			if (base.Player.Run.Active)
			{
				vector2 += this.m_RunBob.CalculateBob(magnitude, Time.deltaTime);
			}
			else
			{
				vector2 += this.m_RunBob.Cooldown(Time.deltaTime);
			}
			this.m_Pivot.localPosition += vector2;
			this.m_Pivot.localPosition += Vector3.up * this.m_LandBob.Value;
			bool flag = this.m_Weapon && !this.m_Weapon.UseWhileNearObjects && base.Player.IsCloseToAnObject.Get() && base.Player.RaycastData.Get().GameObject.layer != LayerMask.NameToLayer("Hitbox") && !base.Player.RaycastData.Get().GameObject.CompareTag("Ladder");
			if (this.m_HolsterActive)
			{
				this.TryChangeOffset(this.m_IdleOffset);
			}
			else if (base.Player.NearLadders.Count > 0)
			{
				this.TryChangeOffset(this.m_OnLadderOffset);
			}
			else if (flag)
			{
				this.TryChangeOffset(this.m_TooCloseOffset);
			}
			else if (base.Player.Run.Active)
			{
				this.TryChangeOffset(this.m_RunOffset);
			}
			else if (base.Player.Aim.Active)
			{
				this.TryChangeOffset(this.m_AimOffset);
			}
			else if (!base.Player.IsGrounded.Get())
			{
				this.TryChangeOffset(this.m_JumpOffset);
			}
			else
			{
				this.TryChangeOffset(this.m_IdleOffset);
			}
			Vector3 vector3;
			Quaternion quaternion;
			this.m_CurrentOffset.Update(Time.deltaTime, out vector3, out quaternion);
			this.m_Pivot.localPosition += vector3;
			this.m_Pivot.localRotation *= quaternion;
		}

		// Token: 0x060039C2 RID: 14786 RVA: 0x00029EDF File Offset: 0x000280DF
		private void TryChangeOffset(TransformOffset newOffset)
		{
			if (this.m_CurrentOffset != newOffset)
			{
				newOffset.ContinueFrom(this.m_CurrentOffset);
				this.m_CurrentOffset = newOffset;
			}
		}

		// Token: 0x040033E3 RID: 13283
		[Header("Setup")]
		[SerializeField]
		private Transform m_Model;

		// Token: 0x040033E4 RID: 13284
		[SerializeField]
		private Transform m_Pivot;

		// Token: 0x040033E5 RID: 13285
		[Header("Sway")]
		[SerializeField]
		private Sway m_MovementSway;

		// Token: 0x040033E6 RID: 13286
		[SerializeField]
		private Sway m_RotationSway;

		// Token: 0x040033E7 RID: 13287
		[Header("Bob")]
		[SerializeField]
		private TrigonometricBob m_WalkBob;

		// Token: 0x040033E8 RID: 13288
		[SerializeField]
		private TrigonometricBob m_AimBob;

		// Token: 0x040033E9 RID: 13289
		[SerializeField]
		private TrigonometricBob m_RunBob;

		// Token: 0x040033EA RID: 13290
		[SerializeField]
		private LerpControlledBob m_LandBob;

		// Token: 0x040033EB RID: 13291
		[SerializeField]
		private float m_MaxLandSpeed = 12f;

		// Token: 0x040033EC RID: 13292
		[Header("Offset")]
		[SerializeField]
		private TransformOffset m_IdleOffset;

		// Token: 0x040033ED RID: 13293
		[SerializeField]
		private TransformOffset m_RunOffset;

		// Token: 0x040033EE RID: 13294
		[SerializeField]
		private TransformOffset m_AimOffset;

		// Token: 0x040033EF RID: 13295
		[SerializeField]
		private TransformOffset m_OnLadderOffset;

		// Token: 0x040033F0 RID: 13296
		[SerializeField]
		private TransformOffset m_JumpOffset;

		// Token: 0x040033F1 RID: 13297
		[SerializeField]
		[Tooltip("The object position and rotation offset, when the character is too close to an object. NOTE: Will not be taken into consideration if the object can be used when near other objects (see the 'CanUseWhileNearObjects' setting).")]
		private TransformOffset m_TooCloseOffset;

		// Token: 0x040033F2 RID: 13298
		private Transform m_Root;

		// Token: 0x040033F3 RID: 13299
		private FPObject m_Object;

		// Token: 0x040033F4 RID: 13300
		private FPWeaponBase m_Weapon;

		// Token: 0x040033F5 RID: 13301
		private TransformOffset m_CurrentOffset;

		// Token: 0x040033F6 RID: 13302
		private bool m_HolsterActive;
	}
}
