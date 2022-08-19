using System;
using System.Collections;
using UnityEngine;

namespace UltimateSurvival
{
	// Token: 0x02000614 RID: 1556
	public class PlayerSleepHandler : PlayerBehaviour
	{
		// Token: 0x060031B5 RID: 12725 RVA: 0x00160DE4 File Offset: 0x0015EFE4
		private void Awake()
		{
			base.Player.StartSleeping.SetTryer(new Attempt<SleepingBag>.GenericTryerDelegate(this.Try_StartSleeping));
		}

		// Token: 0x060031B6 RID: 12726 RVA: 0x00160E02 File Offset: 0x0015F002
		private bool Try_StartSleeping(SleepingBag bag)
		{
			if (base.Player.Sleep.Active)
			{
				return false;
			}
			base.StartCoroutine(this.C_Sleep(bag));
			return true;
		}

		// Token: 0x060031B7 RID: 12727 RVA: 0x00160E27 File Offset: 0x0015F027
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

		// Token: 0x060031B8 RID: 12728 RVA: 0x00160E40 File Offset: 0x0015F040
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

		// Token: 0x04002C0A RID: 11274
		[SerializeField]
		private float m_SleepSpeed = 0.33f;

		// Token: 0x04002C0B RID: 11275
		[SerializeField]
		[Tooltip("How much time to wait after the sleep is done, before getting up.")]
		private float m_SleepFinishPause = 2f;

		// Token: 0x04002C0C RID: 11276
		[SerializeField]
		[Range(4f, 12f)]
		private int m_GetUpHour = 8;

		// Token: 0x04002C0D RID: 11277
		[Header("Stuff To Disable When Sleeping")]
		[SerializeField]
		private Collider[] m_CollidersToDisable;

		// Token: 0x04002C0E RID: 11278
		[SerializeField]
		private GameObject[] m_ObjectsToDisable;

		// Token: 0x04002C0F RID: 11279
		[SerializeField]
		private Behaviour[] m_BehavioursToDisable;

		// Token: 0x04002C10 RID: 11280
		private Vector3 m_BeforeSleepPosition;

		// Token: 0x04002C11 RID: 11281
		private Quaternion m_BeforeSleepRotation;
	}
}
