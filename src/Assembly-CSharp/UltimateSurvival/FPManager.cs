using System.Collections;
using UnityEngine;

namespace UltimateSurvival;

public class FPManager : PlayerBehaviour
{
	[SerializeField]
	private Camera m_WorldCamera;

	[SerializeField]
	private Camera m_FPCamera;

	[Header("Aiming")]
	[SerializeField]
	[Range(0f, 100f)]
	private float m_NormalFOV = 75f;

	[SerializeField]
	[Range(0f, 100f)]
	private float m_AimFOV = 45f;

	[SerializeField]
	[Clamp(0f, 9999f)]
	private float m_FOVSetSpeed = 30f;

	[SerializeField]
	[Range(0.1f, 1f)]
	private float m_AimSpeedMultiplier = 0.6f;

	[Header("Equipping")]
	[SerializeField]
	[Range(0f, 3f)]
	private float m_DrawTime = 0.7f;

	[SerializeField]
	[Range(0f, 3f)]
	private float m_HolsterTime = 0.5f;

	private FPObject[] m_Objects;

	private FPObject m_EquippedObject;

	private FPWeaponBase m_EquippedWeapon;

	private float m_NextTimeCanEquip;

	private bool m_WaitingToEquip;

	private bool m_WaitingToDisable;

	private float m_NextTimeCanDisable;

	private Coroutine m_FOVSetter;

	private void Awake()
	{
		base.Player.ChangeEquippedItem.SetTryer(Try_ChangeEquippedItem);
		base.Player.AttackOnce.SetTryer(() => OnTry_Attack(continuously: false));
		base.Player.AttackContinuously.SetTryer(() => OnTry_Attack(continuously: true));
		base.Player.Aim.AddStartTryer(TryStart_Aim);
		base.Player.Aim.AddStopListener(OnStop_Aim);
		base.Player.Sleep.AddStopListener(OnStop_Sleep);
		base.Player.IsCloseToAnObject.AddChangeListener(OnChanged_IsCloseToAnObject);
		m_Objects = ((Component)this).GetComponentsInChildren<FPObject>(true);
		FPObject[] objects = m_Objects;
		foreach (FPObject fPObject in objects)
		{
			fPObject.On_Holster();
			TryDisableObject(((Component)fPObject).gameObject);
		}
	}

	private void Update()
	{
		if (m_WaitingToDisable && Time.time > m_NextTimeCanDisable)
		{
			TryDisableObject(((Component)m_EquippedObject).gameObject, isCurrent: true);
			m_WaitingToDisable = false;
		}
		if (m_WaitingToEquip && Time.time > m_NextTimeCanEquip)
		{
			TryEquipItem();
			m_WaitingToEquip = false;
		}
	}

	private bool Try_ChangeEquippedItem(SavableItem item, bool instantly)
	{
		if (base.Player.EquippedItem.Get() == item)
		{
			return true;
		}
		m_WaitingToEquip = true;
		m_NextTimeCanEquip = Time.time;
		if (!instantly && (Object)(object)m_EquippedObject != (Object)null)
		{
			m_NextTimeCanEquip += m_HolsterTime;
		}
		if ((Object)(object)m_EquippedObject != (Object)null)
		{
			m_EquippedObject.On_Holster();
			m_WaitingToDisable = true;
			m_NextTimeCanDisable = Time.time;
			if (!instantly)
			{
				m_NextTimeCanDisable += m_HolsterTime;
			}
		}
		base.Player.EquippedItem.Set(item);
		return true;
	}

	private void TryEquipItem()
	{
		SavableItem savableItem = base.Player.EquippedItem.Get();
		if (savableItem == null)
		{
			return;
		}
		FPObject[] objects = m_Objects;
		foreach (FPObject fPObject in objects)
		{
			if (fPObject.ObjectName == savableItem.Name)
			{
				((Component)fPObject).gameObject.SetActive(true);
				fPObject.On_Draw(savableItem);
				m_EquippedObject = fPObject;
				m_EquippedWeapon = m_EquippedObject as FPWeaponBase;
				m_FPCamera.fieldOfView = m_EquippedObject.TargetFOV;
				break;
			}
		}
	}

	private void TryDisableObject(GameObject obj, bool isCurrent = false)
	{
		if (!((Object)(object)obj == (Object)null))
		{
			obj.gameObject.SetActive(true);
			obj.gameObject.SetActive(false);
			if (isCurrent)
			{
				m_EquippedObject = null;
				m_EquippedWeapon = null;
			}
		}
	}

	private void OnStop_Sleep()
	{
		if (Object.op_Implicit((Object)(object)m_EquippedObject))
		{
			m_EquippedObject.On_Draw(base.Player.EquippedItem.Get());
		}
	}

	private void OnChanged_IsCloseToAnObject()
	{
		if ((Object)(object)m_EquippedWeapon != (Object)null && base.Player.IsCloseToAnObject.Get() && !m_EquippedWeapon.UseWhileNearObjects && base.Player.Aim.Active)
		{
			base.Player.Aim.ForceStop();
		}
	}

	private bool TryStart_Aim()
	{
		int num;
		if (base.Player.NearLadders.Count == 0 && (!base.Player.IsCloseToAnObject.Get() || (Object.op_Implicit((Object)(object)m_EquippedWeapon) && m_EquippedWeapon.UseWhileNearObjects)) && Object.op_Implicit((Object)(object)m_EquippedObject) && Time.time > m_NextTimeCanEquip + m_DrawTime && MonoSingleton<InventoryController>.Instance.IsClosed)
		{
			num = ((!base.Player.Run.Active) ? 1 : 0);
			if (num != 0 && Object.op_Implicit((Object)(object)(m_EquippedObject as FPHitscan)))
			{
				if (m_FOVSetter != null)
				{
					((MonoBehaviour)this).StopCoroutine(m_FOVSetter);
				}
				m_FOVSetter = ((MonoBehaviour)this).StartCoroutine(C_SetFOV(m_AimFOV));
			}
		}
		else
		{
			num = 0;
		}
		if (num != 0)
		{
			base.Player.MovementSpeedFactor.Set(m_AimSpeedMultiplier);
		}
		return (byte)num != 0;
	}

	private void OnStop_Aim()
	{
		if (m_FOVSetter != null)
		{
			((MonoBehaviour)this).StopCoroutine(m_FOVSetter);
		}
		m_FOVSetter = ((MonoBehaviour)this).StartCoroutine(C_SetFOV(m_NormalFOV));
		base.Player.MovementSpeedFactor.Set(1f);
	}

	private IEnumerator C_SetFOV(float targetFOV)
	{
		while (Mathf.Abs(m_WorldCamera.fieldOfView - targetFOV) > Mathf.Epsilon)
		{
			m_WorldCamera.fieldOfView = Mathf.MoveTowards(m_WorldCamera.fieldOfView, targetFOV, Time.deltaTime * m_FOVSetSpeed);
			yield return null;
		}
	}

	private bool OnTry_Attack(bool continuously)
	{
		if ((Object)(object)m_EquippedWeapon == (Object)null)
		{
			return false;
		}
		bool flag = base.Player.IsCloseToAnObject.Get() && !m_EquippedWeapon.UseWhileNearObjects;
		if (base.Player.NearLadders.Count == 0 && !flag && !base.Player.Run.Active && MonoSingleton<InventoryController>.Instance.IsClosed && Time.time > m_EquippedObject.LastDrawTime + m_DrawTime)
		{
			if (continuously)
			{
				return m_EquippedWeapon.TryAttackContinuously(m_WorldCamera);
			}
			return m_EquippedWeapon.TryAttackOnce(m_WorldCamera);
		}
		return false;
	}
}
