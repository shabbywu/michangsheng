using System;
using System.Collections;
using UnityEngine;

namespace UltimateSurvival
{
	// Token: 0x020008BD RID: 2237
	public class FPManager : PlayerBehaviour
	{
		// Token: 0x0600398C RID: 14732 RVA: 0x001A5EB4 File Offset: 0x001A40B4
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

		// Token: 0x0600398D RID: 14733 RVA: 0x001A5FC0 File Offset: 0x001A41C0
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

		// Token: 0x0600398E RID: 14734 RVA: 0x001A6020 File Offset: 0x001A4220
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

		// Token: 0x0600398F RID: 14735 RVA: 0x001A60CC File Offset: 0x001A42CC
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

		// Token: 0x06003990 RID: 14736 RVA: 0x00029C6F File Offset: 0x00027E6F
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

		// Token: 0x06003991 RID: 14737 RVA: 0x00029CA4 File Offset: 0x00027EA4
		private void OnStop_Sleep()
		{
			if (this.m_EquippedObject)
			{
				this.m_EquippedObject.On_Draw(base.Player.EquippedItem.Get());
			}
		}

		// Token: 0x06003992 RID: 14738 RVA: 0x001A6160 File Offset: 0x001A4360
		private void OnChanged_IsCloseToAnObject()
		{
			if (this.m_EquippedWeapon != null && base.Player.IsCloseToAnObject.Get() && !this.m_EquippedWeapon.UseWhileNearObjects && base.Player.Aim.Active)
			{
				base.Player.Aim.ForceStop();
			}
		}

		// Token: 0x06003993 RID: 14739 RVA: 0x001A61BC File Offset: 0x001A43BC
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

		// Token: 0x06003994 RID: 14740 RVA: 0x001A62A4 File Offset: 0x001A44A4
		private void OnStop_Aim()
		{
			if (this.m_FOVSetter != null)
			{
				base.StopCoroutine(this.m_FOVSetter);
			}
			this.m_FOVSetter = base.StartCoroutine(this.C_SetFOV(this.m_NormalFOV));
			base.Player.MovementSpeedFactor.Set(1f);
		}

		// Token: 0x06003995 RID: 14741 RVA: 0x00029CCE File Offset: 0x00027ECE
		private IEnumerator C_SetFOV(float targetFOV)
		{
			while (Mathf.Abs(this.m_WorldCamera.fieldOfView - targetFOV) > Mathf.Epsilon)
			{
				this.m_WorldCamera.fieldOfView = Mathf.MoveTowards(this.m_WorldCamera.fieldOfView, targetFOV, Time.deltaTime * this.m_FOVSetSpeed);
				yield return null;
			}
			yield break;
		}

		// Token: 0x06003996 RID: 14742 RVA: 0x001A62F4 File Offset: 0x001A44F4
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

		// Token: 0x040033B4 RID: 13236
		[SerializeField]
		private Camera m_WorldCamera;

		// Token: 0x040033B5 RID: 13237
		[SerializeField]
		private Camera m_FPCamera;

		// Token: 0x040033B6 RID: 13238
		[Header("Aiming")]
		[SerializeField]
		[Range(0f, 100f)]
		private float m_NormalFOV = 75f;

		// Token: 0x040033B7 RID: 13239
		[SerializeField]
		[Range(0f, 100f)]
		private float m_AimFOV = 45f;

		// Token: 0x040033B8 RID: 13240
		[SerializeField]
		[Clamp(0f, 9999f)]
		private float m_FOVSetSpeed = 30f;

		// Token: 0x040033B9 RID: 13241
		[SerializeField]
		[Range(0.1f, 1f)]
		private float m_AimSpeedMultiplier = 0.6f;

		// Token: 0x040033BA RID: 13242
		[Header("Equipping")]
		[SerializeField]
		[Range(0f, 3f)]
		private float m_DrawTime = 0.7f;

		// Token: 0x040033BB RID: 13243
		[SerializeField]
		[Range(0f, 3f)]
		private float m_HolsterTime = 0.5f;

		// Token: 0x040033BC RID: 13244
		private FPObject[] m_Objects;

		// Token: 0x040033BD RID: 13245
		private FPObject m_EquippedObject;

		// Token: 0x040033BE RID: 13246
		private FPWeaponBase m_EquippedWeapon;

		// Token: 0x040033BF RID: 13247
		private float m_NextTimeCanEquip;

		// Token: 0x040033C0 RID: 13248
		private bool m_WaitingToEquip;

		// Token: 0x040033C1 RID: 13249
		private bool m_WaitingToDisable;

		// Token: 0x040033C2 RID: 13250
		private float m_NextTimeCanDisable;

		// Token: 0x040033C3 RID: 13251
		private Coroutine m_FOVSetter;
	}
}
