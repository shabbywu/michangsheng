using System.Collections;
using UnityEngine;

namespace UltimateSurvival;

public class PlayerSleepHandler : PlayerBehaviour
{
	[SerializeField]
	private float m_SleepSpeed = 0.33f;

	[SerializeField]
	[Tooltip("How much time to wait after the sleep is done, before getting up.")]
	private float m_SleepFinishPause = 2f;

	[SerializeField]
	[Range(4f, 12f)]
	private int m_GetUpHour = 8;

	[Header("Stuff To Disable When Sleeping")]
	[SerializeField]
	private Collider[] m_CollidersToDisable;

	[SerializeField]
	private GameObject[] m_ObjectsToDisable;

	[SerializeField]
	private Behaviour[] m_BehavioursToDisable;

	private Vector3 m_BeforeSleepPosition;

	private Quaternion m_BeforeSleepRotation;

	private void Awake()
	{
		base.Player.StartSleeping.SetTryer(Try_StartSleeping);
	}

	private bool Try_StartSleeping(SleepingBag bag)
	{
		if (base.Player.Sleep.Active)
		{
			return false;
		}
		((MonoBehaviour)this).StartCoroutine(C_Sleep(bag));
		return true;
	}

	private IEnumerator C_Sleep(SleepingBag bag)
	{
		m_BeforeSleepPosition = ((Component)this).transform.position;
		m_BeforeSleepRotation = ((Component)this).transform.rotation;
		EnableStuff(enable: false);
		float hoursSlept = 0f;
		float speed2 = m_SleepSpeed;
		int currentHour = MonoSingleton<TimeOfDay>.Instance.CurrentHour;
		int hoursToSleep = ((currentHour > 24 || currentHour <= 18) ? (m_GetUpHour - currentHour) : (24 - currentHour + m_GetUpHour));
		while (Mathf.Abs(MonoSingleton<TimeOfDay>.Instance.CurrentHour - m_GetUpHour) > 0)
		{
			MonoSingleton<TimeOfDay>.Instance.NormalizedTime += Time.deltaTime * speed2;
			hoursSlept += Time.deltaTime * speed2 * 24f;
			speed2 = Mathf.Lerp(m_SleepSpeed, 0f, hoursSlept / (float)hoursToSleep);
			speed2 = Mathf.Max(speed2, 0.001f);
			((Component)this).transform.position = Vector3.Lerp(((Component)this).transform.position, bag.SleepPosition, Time.deltaTime * 10f);
			((Component)this).transform.rotation = Quaternion.Lerp(((Component)this).transform.rotation, bag.SleepRotation, Time.deltaTime * 10f);
			yield return null;
		}
		yield return (object)new WaitForSeconds(m_SleepFinishPause);
		while (true)
		{
			Vector3 val = ((Component)this).transform.position - m_BeforeSleepPosition;
			if (!(((Vector3)(ref val)).sqrMagnitude > 0.0001f) || !(Quaternion.Angle(((Component)this).transform.rotation, m_BeforeSleepRotation) > 0.001f))
			{
				break;
			}
			((Component)this).transform.position = Vector3.Lerp(((Component)this).transform.position, m_BeforeSleepPosition, Time.deltaTime * 10f);
			((Component)this).transform.rotation = Quaternion.Lerp(((Component)this).transform.rotation, m_BeforeSleepRotation, Time.deltaTime * 10f);
			yield return null;
		}
		((Component)this).transform.position = m_BeforeSleepPosition;
		((Component)this).transform.rotation = m_BeforeSleepRotation;
		EnableStuff(enable: true);
		base.Player.LastSleepPosition.Set(bag.SpawnPosOffset);
		base.Player.Sleep.ForceStop();
	}

	private void EnableStuff(bool enable)
	{
		GameObject[] objectsToDisable = m_ObjectsToDisable;
		for (int i = 0; i < objectsToDisable.Length; i++)
		{
			objectsToDisable[i].SetActive(enable);
		}
		Behaviour[] behavioursToDisable = m_BehavioursToDisable;
		for (int i = 0; i < behavioursToDisable.Length; i++)
		{
			behavioursToDisable[i].enabled = enable;
		}
		Collider[] collidersToDisable = m_CollidersToDisable;
		for (int i = 0; i < collidersToDisable.Length; i++)
		{
			collidersToDisable[i].enabled = enable;
		}
	}
}
