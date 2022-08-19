using System;
using UnityEngine;

namespace UltimateSurvival
{
	// Token: 0x020005F0 RID: 1520
	[RequireComponent(typeof(FPObject))]
	public class FPMotion : PlayerBehaviour
	{
		// Token: 0x060030DE RID: 12510 RVA: 0x0015D324 File Offset: 0x0015B524
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

		// Token: 0x060030DF RID: 12511 RVA: 0x0015D3B4 File Offset: 0x0015B5B4
		private void On_Draw()
		{
			this.m_IdleOffset.Reset();
			this.m_CurrentOffset = this.m_IdleOffset;
			this.m_HolsterActive = false;
		}

		// Token: 0x060030E0 RID: 12512 RVA: 0x0015D3D4 File Offset: 0x0015B5D4
		private void On_Holster()
		{
			this.m_HolsterActive = true;
		}

		// Token: 0x060030E1 RID: 12513 RVA: 0x0015D3DD File Offset: 0x0015B5DD
		private void On_Land(float landSpeed)
		{
			if (this.m_Object.IsEnabled && base.gameObject.activeInHierarchy)
			{
				base.StartCoroutine(this.m_LandBob.DoBobCycle(landSpeed / this.m_MaxLandSpeed));
			}
		}

		// Token: 0x060030E2 RID: 12514 RVA: 0x0015D414 File Offset: 0x0015B614
		private void SetupTransforms()
		{
			Transform transform = new GameObject("Root").transform;
			transform.transform.SetParent(base.transform);
			transform.position = this.m_Pivot.position;
			transform.rotation = this.m_Pivot.rotation;
			this.m_Pivot.SetParent(transform, true);
			this.m_Model.SetParent(this.m_Pivot, true);
		}

		// Token: 0x060030E3 RID: 12515 RVA: 0x0015D484 File Offset: 0x0015B684
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

		// Token: 0x060030E4 RID: 12516 RVA: 0x0015D7E1 File Offset: 0x0015B9E1
		private void TryChangeOffset(TransformOffset newOffset)
		{
			if (this.m_CurrentOffset != newOffset)
			{
				newOffset.ContinueFrom(this.m_CurrentOffset);
				this.m_CurrentOffset = newOffset;
			}
		}

		// Token: 0x04002B0D RID: 11021
		[Header("Setup")]
		[SerializeField]
		private Transform m_Model;

		// Token: 0x04002B0E RID: 11022
		[SerializeField]
		private Transform m_Pivot;

		// Token: 0x04002B0F RID: 11023
		[Header("Sway")]
		[SerializeField]
		private Sway m_MovementSway;

		// Token: 0x04002B10 RID: 11024
		[SerializeField]
		private Sway m_RotationSway;

		// Token: 0x04002B11 RID: 11025
		[Header("Bob")]
		[SerializeField]
		private TrigonometricBob m_WalkBob;

		// Token: 0x04002B12 RID: 11026
		[SerializeField]
		private TrigonometricBob m_AimBob;

		// Token: 0x04002B13 RID: 11027
		[SerializeField]
		private TrigonometricBob m_RunBob;

		// Token: 0x04002B14 RID: 11028
		[SerializeField]
		private LerpControlledBob m_LandBob;

		// Token: 0x04002B15 RID: 11029
		[SerializeField]
		private float m_MaxLandSpeed = 12f;

		// Token: 0x04002B16 RID: 11030
		[Header("Offset")]
		[SerializeField]
		private TransformOffset m_IdleOffset;

		// Token: 0x04002B17 RID: 11031
		[SerializeField]
		private TransformOffset m_RunOffset;

		// Token: 0x04002B18 RID: 11032
		[SerializeField]
		private TransformOffset m_AimOffset;

		// Token: 0x04002B19 RID: 11033
		[SerializeField]
		private TransformOffset m_OnLadderOffset;

		// Token: 0x04002B1A RID: 11034
		[SerializeField]
		private TransformOffset m_JumpOffset;

		// Token: 0x04002B1B RID: 11035
		[SerializeField]
		[Tooltip("The object position and rotation offset, when the character is too close to an object. NOTE: Will not be taken into consideration if the object can be used when near other objects (see the 'CanUseWhileNearObjects' setting).")]
		private TransformOffset m_TooCloseOffset;

		// Token: 0x04002B1C RID: 11036
		private Transform m_Root;

		// Token: 0x04002B1D RID: 11037
		private FPObject m_Object;

		// Token: 0x04002B1E RID: 11038
		private FPWeaponBase m_Weapon;

		// Token: 0x04002B1F RID: 11039
		private TransformOffset m_CurrentOffset;

		// Token: 0x04002B20 RID: 11040
		private bool m_HolsterActive;
	}
}
