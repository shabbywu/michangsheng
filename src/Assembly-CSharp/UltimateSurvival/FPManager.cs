using System;
using System.Collections;
using UnityEngine;

namespace UltimateSurvival
{
	// Token: 0x020005EB RID: 1515
	public class FPManager : PlayerBehaviour
	{
		// Token: 0x060030B4 RID: 12468 RVA: 0x0015C720 File Offset: 0x0015A920
		private void Awake()
		{
			base.Player.ChangeEquippedItem.SetTryer(new Attempt<SavableItem, bool>.GenericTryerDelegate(this.Try_ChangeEquippedItem));
			base.Player.AttackOnce.SetTryer(() => this.OnTry_Attack(false));
			base.Player.AttackContinuously.SetTryer(() => this.OnTry_Attack(true));
			base.Player.Aim.AddStartTryer(new TryerDelegate(this.TryStart_Aim));
			base.Player.Aim.AddStopListener(new Action(this.OnStop_Aim));
			base.Player.Sleep.AddStopListener(new Action(this.OnStop_Sleep));
			base.Player.IsCloseToAnObject.AddChangeListener(new Action(this.OnChanged_IsCloseToAnObject));
			this.m_Objects = base.GetComponentsInChildren<FPObject>(true);
			foreach (FPObject fpobject in this.m_Objects)
			{
				fpobject.On_Holster();
				this.TryDisableObject(fpobject.gameObject, false);
			}
		}

		// Token: 0x060030B5 RID: 12469 RVA: 0x0015C82C File Offset: 0x0015AA2C
		private void Update()
		{
			if (this.m_WaitingToDisable && Time.time > this.m_NextTimeCanDisable)
			{
				this.TryDisableObject(this.m_EquippedObject.gameObject, true);
				this.m_WaitingToDisable = false;
			}
			if (this.m_WaitingToEquip && Time.time > this.m_NextTimeCanEquip)
			{
				this.TryEquipItem();
				this.m_WaitingToEquip = false;
			}
		}

		// Token: 0x060030B6 RID: 12470 RVA: 0x0015C88C File Offset: 0x0015AA8C
		private bool Try_ChangeEquippedItem(SavableItem item, bool instantly)
		{
			if (base.Player.EquippedItem.Get() == item)
			{
				return true;
			}
			this.m_WaitingToEquip = true;
			this.m_NextTimeCanEquip = Time.time;
			if (!instantly && this.m_EquippedObject != null)
			{
				this.m_NextTimeCanEquip += this.m_HolsterTime;
			}
			if (this.m_EquippedObject != null)
			{
				this.m_EquippedObject.On_Holster();
				this.m_WaitingToDisable = true;
				this.m_NextTimeCanDisable = Time.time;
				if (!instantly)
				{
					this.m_NextTimeCanDisable += this.m_HolsterTime;
				}
			}
			base.Player.EquippedItem.Set(item);
			return true;
		}

		// Token: 0x060030B7 RID: 12471 RVA: 0x0015C938 File Offset: 0x0015AB38
		private void TryEquipItem()
		{
			SavableItem savableItem = base.Player.EquippedItem.Get();
			if (savableItem == null)
			{
				return;
			}
			foreach (FPObject fpobject in this.m_Objects)
			{
				if (fpobject.ObjectName == savableItem.Name)
				{
					fpobject.gameObject.SetActive(true);
					fpobject.On_Draw(savableItem);
					this.m_EquippedObject = fpobject;
					this.m_EquippedWeapon = (this.m_EquippedObject as FPWeaponBase);
					this.m_FPCamera.fieldOfView = (float)this.m_EquippedObject.TargetFOV;
					return;
				}
			}
		}

		// Token: 0x060030B8 RID: 12472 RVA: 0x0015C9C9 File Offset: 0x0015ABC9
		private void TryDisableObject(GameObject obj, bool isCurrent = false)
		{
			if (obj == null)
			{
				return;
			}
			obj.gameObject.SetActive(true);
			obj.gameObject.SetActive(false);
			if (isCurrent)
			{
				this.m_EquippedObject = null;
				this.m_EquippedWeapon = null;
			}
		}

		// Token: 0x060030B9 RID: 12473 RVA: 0x0015C9FE File Offset: 0x0015ABFE
		private void OnStop_Sleep()
		{
			if (this.m_EquippedObject)
			{
				this.m_EquippedObject.On_Draw(base.Player.EquippedItem.Get());
			}
		}

		// Token: 0x060030BA RID: 12474 RVA: 0x0015CA28 File Offset: 0x0015AC28
		private void OnChanged_IsCloseToAnObject()
		{
			if (this.m_EquippedWeapon != null && base.Player.IsCloseToAnObject.Get() && !this.m_EquippedWeapon.UseWhileNearObjects && base.Player.Aim.Active)
			{
				base.Player.Aim.ForceStop();
			}
		}

		// Token: 0x060030BB RID: 12475 RVA: 0x0015CA84 File Offset: 0x0015AC84
		private bool TryStart_Aim()
		{
			bool flag = base.Player.NearLadders.Count == 0 && (!base.Player.IsCloseToAnObject.Get() || (this.m_EquippedWeapon && this.m_EquippedWeapon.UseWhileNearObjects)) && this.m_EquippedObject && Time.time > this.m_NextTimeCanEquip + this.m_DrawTime && MonoSingleton<InventoryController>.Instance.IsClosed && !base.Player.Run.Active;
			if (flag && this.m_EquippedObject as FPHitscan)
			{
				if (this.m_FOVSetter != null)
				{
					base.StopCoroutine(this.m_FOVSetter);
				}
				this.m_FOVSetter = base.StartCoroutine(this.C_SetFOV(this.m_AimFOV));
			}
			if (flag)
			{
				base.Player.MovementSpeedFactor.Set(this.m_AimSpeedMultiplier);
			}
			return flag;
		}

		// Token: 0x060030BC RID: 12476 RVA: 0x0015CB6C File Offset: 0x0015AD6C
		private void OnStop_Aim()
		{
			if (this.m_FOVSetter != null)
			{
				base.StopCoroutine(this.m_FOVSetter);
			}
			this.m_FOVSetter = base.StartCoroutine(this.C_SetFOV(this.m_NormalFOV));
			base.Player.MovementSpeedFactor.Set(1f);
		}

		// Token: 0x060030BD RID: 12477 RVA: 0x0015CBBA File Offset: 0x0015ADBA
		private IEnumerator C_SetFOV(float targetFOV)
		{
			while (Mathf.Abs(this.m_WorldCamera.fieldOfView - targetFOV) > Mathf.Epsilon)
			{
				this.m_WorldCamera.fieldOfView = Mathf.MoveTowards(this.m_WorldCamera.fieldOfView, targetFOV, Time.deltaTime * this.m_FOVSetSpeed);
				yield return null;
			}
			yield break;
		}

		// Token: 0x060030BE RID: 12478 RVA: 0x0015CBD0 File Offset: 0x0015ADD0
		private bool OnTry_Attack(bool continuously)
		{
			if (this.m_EquippedWeapon == null)
			{
				return false;
			}
			bool flag = base.Player.IsCloseToAnObject.Get() && !this.m_EquippedWeapon.UseWhileNearObjects;
			if (base.Player.NearLadders.Count == 0 && !flag && !base.Player.Run.Active && MonoSingleton<InventoryController>.Instance.IsClosed && Time.time > this.m_EquippedObject.LastDrawTime + this.m_DrawTime)
			{
				bool result;
				if (continuously)
				{
					result = this.m_EquippedWeapon.TryAttackContinuously(this.m_WorldCamera);
				}
				else
				{
					result = this.m_EquippedWeapon.TryAttackOnce(this.m_WorldCamera);
				}
				return result;
			}
			return false;
		}

		// Token: 0x04002AE5 RID: 10981
		[SerializeField]
		private Camera m_WorldCamera;

		// Token: 0x04002AE6 RID: 10982
		[SerializeField]
		private Camera m_FPCamera;

		// Token: 0x04002AE7 RID: 10983
		[Header("Aiming")]
		[SerializeField]
		[Range(0f, 100f)]
		private float m_NormalFOV = 75f;

		// Token: 0x04002AE8 RID: 10984
		[SerializeField]
		[Range(0f, 100f)]
		private float m_AimFOV = 45f;

		// Token: 0x04002AE9 RID: 10985
		[SerializeField]
		[Clamp(0f, 9999f)]
		private float m_FOVSetSpeed = 30f;

		// Token: 0x04002AEA RID: 10986
		[SerializeField]
		[Range(0.1f, 1f)]
		private float m_AimSpeedMultiplier = 0.6f;

		// Token: 0x04002AEB RID: 10987
		[Header("Equipping")]
		[SerializeField]
		[Range(0f, 3f)]
		private float m_DrawTime = 0.7f;

		// Token: 0x04002AEC RID: 10988
		[SerializeField]
		[Range(0f, 3f)]
		private float m_HolsterTime = 0.5f;

		// Token: 0x04002AED RID: 10989
		private FPObject[] m_Objects;

		// Token: 0x04002AEE RID: 10990
		private FPObject m_EquippedObject;

		// Token: 0x04002AEF RID: 10991
		private FPWeaponBase m_EquippedWeapon;

		// Token: 0x04002AF0 RID: 10992
		private float m_NextTimeCanEquip;

		// Token: 0x04002AF1 RID: 10993
		private bool m_WaitingToEquip;

		// Token: 0x04002AF2 RID: 10994
		private bool m_WaitingToDisable;

		// Token: 0x04002AF3 RID: 10995
		private float m_NextTimeCanDisable;

		// Token: 0x04002AF4 RID: 10996
		private Coroutine m_FOVSetter;
	}
}
