using System;
using System.Collections;
using UnityEngine;

namespace UltimateSurvival
{
	// Token: 0x020008F8 RID: 2296
	public class PlayerSleepHandler : PlayerBehaviour
	{
		// Token: 0x06003AD8 RID: 15064 RVA: 0x0002AB6A File Offset: 0x00028D6A
		private void Awake()
		{
			base.Player.StartSleeping.SetTryer(new Attempt<SleepingBag>.GenericTryerDelegate(this.Try_StartSleeping));
		}

		// Token: 0x06003AD9 RID: 15065 RVA: 0x0002AB88 File Offset: 0x00028D88
		private bool Try_StartSleeping(SleepingBag bag)
		{
			if (base.Player.Sleep.Active)
			{
				return false;
			}
			base.StartCoroutine(this.C_Sleep(bag));
			return true;
		}

		// Token: 0x06003ADA RID: 15066 RVA: 0x0002ABAD File Offset: 0x00028DAD
		private IEnumerator C_Sleep(SleepingBag bag)
		{
			this.m_BeforeSleepPosition = base.transform.position;
			this.m_BeforeSleepRotation = base.transform.rotation;
			this.EnableStuff(false);
			int hoursToSleep = 0;
			float hoursSlept = 0f;
			float speed = this.m_SleepSpeed;
			int currentHour = MonoSingleton<TimeOfDay>.Instance.CurrentHour;
			if (currentHour <= 24 && currentHour > 18)
			{
				hoursToSleep = 24 - currentHour + this.m_GetUpHour;
			}
			else
			{
				hoursToSleep = this.m_GetUpHour - currentHour;
			}
			while (Mathf.Abs(MonoSingleton<TimeOfDay>.Instance.CurrentHour - this.m_GetUpHour) > 0)
			{
				MonoSingleton<TimeOfDay>.Instance.NormalizedTime += Time.deltaTime * speed;
				hoursSlept += Time.deltaTime * speed * 24f;
				speed = Mathf.Lerp(this.m_SleepSpeed, 0f, hoursSlept / (float)hoursToSleep);
				speed = Mathf.Max(speed, 0.001f);
				base.transform.position = Vector3.Lerp(base.transform.position, bag.SleepPosition, Time.deltaTime * 10f);
				base.transform.rotation = Quaternion.Lerp(base.transform.rotation, bag.SleepRotation, Time.deltaTime * 10f);
				yield return null;
			}
			yield return new WaitForSeconds(this.m_SleepFinishPause);
			while ((base.transform.position - this.m_BeforeSleepPosition).sqrMagnitude > 0.0001f && Quaternion.Angle(base.transform.rotation, this.m_BeforeSleepRotation) > 0.001f)
			{
				base.transform.position = Vector3.Lerp(base.transform.position, this.m_BeforeSleepPosition, Time.deltaTime * 10f);
				base.transform.rotation = Quaternion.Lerp(base.transform.rotation, this.m_BeforeSleepRotation, Time.deltaTime * 10f);
				yield return null;
			}
			base.transform.position = this.m_BeforeSleepPosition;
			base.transform.rotation = this.m_BeforeSleepRotation;
			this.EnableStuff(true);
			base.Player.LastSleepPosition.Set(bag.SpawnPosOffset);
			base.Player.Sleep.ForceStop();
			yield break;
		}

		// Token: 0x06003ADB RID: 15067 RVA: 0x001AA50C File Offset: 0x001A870C
		private void EnableStuff(bool enable)
		{
			GameObject[] objectsToDisable = this.m_ObjectsToDisable;
			for (int i = 0; i < objectsToDisable.Length; i++)
			{
				objectsToDisable[i].SetActive(enable);
			}
			Behaviour[] behavioursToDisable = this.m_BehavioursToDisable;
			for (int i = 0; i < behavioursToDisable.Length; i++)
			{
				behavioursToDisable[i].enabled = enable;
			}
			Collider[] collidersToDisable = this.m_CollidersToDisable;
			for (int i = 0; i < collidersToDisable.Length; i++)
			{
				collidersToDisable[i].enabled = enable;
			}
		}

		// Token: 0x04003521 RID: 13601
		[SerializeField]
		private float m_SleepSpeed = 0.33f;

		// Token: 0x04003522 RID: 13602
		[SerializeField]
		[Tooltip("How much time to wait after the sleep is done, before getting up.")]
		private float m_SleepFinishPause = 2f;

		// Token: 0x04003523 RID: 13603
		[SerializeField]
		[Range(4f, 12f)]
		private int m_GetUpHour = 8;

		// Token: 0x04003524 RID: 13604
		[Header("Stuff To Disable When Sleeping")]
		[SerializeField]
		private Collider[] m_CollidersToDisable;

		// Token: 0x04003525 RID: 13605
		[SerializeField]
		private GameObject[] m_ObjectsToDisable;

		// Token: 0x04003526 RID: 13606
		[SerializeField]
		private Behaviour[] m_BehavioursToDisable;

		// Token: 0x04003527 RID: 13607
		private Vector3 m_BeforeSleepPosition;

		// Token: 0x04003528 RID: 13608
		private Quaternion m_BeforeSleepRotation;
	}
}
