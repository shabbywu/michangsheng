using System;
using KBEngine;
using UnityEngine;

namespace UltimateSurvival
{
	// Token: 0x02000887 RID: 2183
	public class TimeOfDay : MonoSingleton<TimeOfDay>
	{
		// Token: 0x170005BC RID: 1468
		// (get) Token: 0x0600384F RID: 14415 RVA: 0x00028EB4 File Offset: 0x000270B4
		// (set) Token: 0x06003850 RID: 14416 RVA: 0x00028EBC File Offset: 0x000270BC
		public float NormalizedTime
		{
			get
			{
				return this.m_NormalizedTime;
			}
			set
			{
				this.m_NormalizedTime = (float.IsNaN(Mathf.Repeat(value, 1f)) ? 0f : Mathf.Repeat(value, 1f));
				this.m_CurrentHour = (int)(this.m_NormalizedTime * 24f);
			}
		}

		// Token: 0x170005BD RID: 1469
		// (get) Token: 0x06003851 RID: 14417 RVA: 0x00028EFB File Offset: 0x000270FB
		// (set) Token: 0x06003852 RID: 14418 RVA: 0x00028F03 File Offset: 0x00027103
		public int CurrentHour
		{
			get
			{
				return this.m_CurrentHour;
			}
			set
			{
				this.m_CurrentHour = value;
			}
		}

		// Token: 0x170005BE RID: 1470
		// (get) Token: 0x06003853 RID: 14419 RVA: 0x00028F0C File Offset: 0x0002710C
		public int DayDuration
		{
			get
			{
				return this.m_DayDuration;
			}
		}

		// Token: 0x06003854 RID: 14420 RVA: 0x001A2A54 File Offset: 0x001A0C54
		private void Awake()
		{
			if (!this.m_Sun || !this.m_Moon)
			{
				Debug.LogError("The moon or sun are not assigned in the inspector! please assign them and restart the game.", this);
				base.enabled = false;
				return;
			}
			this.m_SunTransform = this.m_Sun.transform;
			this.m_MoonTransform = this.m_Moon.transform;
			this.AccommodateEditorChanges();
			this.m_InternalState = (this.NormalizedTime.IsInRangeLimitsExcluded(0.25f, 0.75f) ? ET.TimeOfDay.Day : ET.TimeOfDay.Night);
			this.State.Set(this.m_InternalState);
			base.InvokeRepeating("DayZombie", 2f, 3f);
		}

		// Token: 0x06003855 RID: 14421 RVA: 0x001A2B00 File Offset: 0x001A0D00
		public void DayZombie()
		{
			if (this.m_InternalState == ET.TimeOfDay.Day)
			{
				try
				{
					if (KBEngineApp.app.player().className == "Avatar")
					{
						Avatar avatar = (Avatar)KBEngineApp.app.player();
						if (avatar != null)
						{
							string index = string.Concat(avatar.roleTypeCell);
							jsonData.instance.heroJsonData[index]["heroType"].str == "Zombie";
						}
					}
				}
				catch (Exception ex)
				{
					Debug.LogError(ex);
				}
			}
			if (this.m_InternalState == ET.TimeOfDay.Night && this.CurrentHour < 22 && this.CurrentHour > 16)
			{
				this.useResteRole(jsonData.instance.ZombieBossID);
			}
			this.useResteRole(51);
		}

		// Token: 0x06003856 RID: 14422 RVA: 0x001A2BCC File Offset: 0x001A0DCC
		private void OnGUI()
		{
			if (this.m_ShowGUI)
			{
				Rect rect;
				rect..ctor(8f, 8f, 128f, 20f);
				this.m_StopTime = GUI.Toggle(rect, this.m_StopTime, "Stop Time?");
				string text = "Time: " + this.m_CurrentHour + " ";
				if (this.m_NormalizedTime.IsInRangeLimitsIncluded(0.5f, 1f))
				{
					text += "PM";
				}
				else
				{
					text += "AM";
				}
				rect.y = rect.yMax + 4f;
				GUI.Label(rect, text);
				rect.y = rect.yMax;
				if (this.m_StopTime)
				{
					this.m_NormalizedTime = GUI.HorizontalSlider(rect, this.m_NormalizedTime, 0f, 1f);
				}
				else
				{
					GUI.HorizontalSlider(rect, this.m_NormalizedTime, 0f, 1f);
				}
				rect.y = rect.yMax;
				rect.width = 256f;
				GUI.Label(rect, "Day Duration: " + this.m_DayDuration + " seconds");
				rect.y = rect.yMax;
				rect.width = 128f;
				this.m_DayDuration = (int)GUI.HorizontalSlider(rect, (float)this.m_DayDuration, 0f, 1000f);
				this.m_TimeIncrement = 1f / (float)this.m_DayDuration;
			}
		}

		// Token: 0x06003857 RID: 14423 RVA: 0x001A2D50 File Offset: 0x001A0F50
		private void Update()
		{
			if (!this.m_StopTime)
			{
				this.NormalizedTime += Time.deltaTime * this.m_TimeIncrement;
			}
			this.m_CurrentHour = (int)(this.NormalizedTime * 24f);
			RenderSettings.ambientIntensity = Mathf.Clamp01(this.m_Sun.intensity);
			RenderSettings.fogDensity = this.m_FogIntensity.Evaluate(this.NormalizedTime);
			RenderSettings.fogColor = this.m_FogColor.Evaluate(this.NormalizedTime);
			this.m_SunTransform.rotation = Quaternion.Euler(this.m_RotationAxis * (this.NormalizedTime * 360f - 90f));
			this.m_Sun.intensity = this.m_SunIntensity.Evaluate(this.NormalizedTime);
			this.m_Sun.color = this.m_SunColor.Evaluate(this.NormalizedTime);
			this.m_Sun.enabled = (this.m_Sun.intensity > 0f);
			this.m_MoonTransform.rotation = Quaternion.Euler(-this.m_RotationAxis * (this.NormalizedTime * 360f - 90f));
			this.m_Moon.intensity = this.m_MoonIntensity.Evaluate(this.NormalizedTime);
			this.m_Moon.color = this.m_MoonColor.Evaluate(this.NormalizedTime);
			this.m_Moon.enabled = (this.m_Moon.intensity > 0f);
			if (this.m_Skybox)
			{
				this.m_Skybox.SetFloat("_Blend", this.m_SkyboxBlend.Evaluate(this.m_NormalizedTime));
			}
			ET.TimeOfDay internalState = this.m_InternalState;
			this.m_InternalState = (this.NormalizedTime.IsInRangeLimitsExcluded(0.25f, 0.75f) ? ET.TimeOfDay.Day : ET.TimeOfDay.Night);
			if (internalState != this.m_InternalState)
			{
				this.State.Set(this.m_InternalState);
				ET.TimeOfDay internalState2 = this.m_InternalState;
			}
			GameController.NormalizedTime = this.NormalizedTime;
		}

		// Token: 0x06003858 RID: 14424 RVA: 0x001A2F60 File Offset: 0x001A1160
		public void useResteRole(int itemid)
		{
			try
			{
				if (KBEngineApp.app.player().className == "Avatar")
				{
					Avatar avatar = (Avatar)KBEngineApp.app.player();
					if (avatar != null)
					{
						Inventory component = ((GameObject)avatar.renderObj).transform.GetComponent<PlayerInventory>().inventory.GetComponent<Inventory>();
						ulong num = 0UL;
						component.updateItemList();
						foreach (Item item in component.ItemsInInventory)
						{
							if (item.itemID == itemid)
							{
								num = item.itemUUID;
							}
						}
						if (num != 0UL)
						{
							avatar.useItemRequest(num);
						}
					}
				}
			}
			catch (Exception)
			{
			}
		}

		// Token: 0x06003859 RID: 14425 RVA: 0x00028F14 File Offset: 0x00027114
		private void OnValidate()
		{
			this.AccommodateEditorChanges();
			this.m_SunTransform = this.m_Sun.transform;
			this.m_MoonTransform = this.m_Moon.transform;
			this.Update();
		}

		// Token: 0x0600385A RID: 14426 RVA: 0x00028F44 File Offset: 0x00027144
		private void AccommodateEditorChanges()
		{
			this.m_TimeIncrement = 1f / (float)this.m_DayDuration;
			this.m_NormalizedTime = (float)this.m_CurrentHour / 24f;
			RenderSettings.fogMode = this.m_FogMode;
		}

		// Token: 0x040032A5 RID: 12965
		public Value<ET.TimeOfDay> State = new Value<ET.TimeOfDay>(ET.TimeOfDay.Day);

		// Token: 0x040032A6 RID: 12966
		[Header("Setup")]
		[SerializeField]
		private Light m_Sun;

		// Token: 0x040032A7 RID: 12967
		[SerializeField]
		private Light m_Moon;

		// Token: 0x040032A8 RID: 12968
		[Header("General")]
		[SerializeField]
		private bool m_StopTime;

		// Token: 0x040032A9 RID: 12969
		[SerializeField]
		private bool m_ShowGUI;

		// Token: 0x040032AA RID: 12970
		[SerializeField]
		[Range(0f, 24f)]
		[Tooltip("The current hour (00:00 AM to 12:00 PM to 24:00 PM)")]
		private int m_CurrentHour = 6;

		// Token: 0x040032AB RID: 12971
		[SerializeField]
		[Tooltip("How many seconds are in a day.")]
		private int m_DayDuration = 900;

		// Token: 0x040032AC RID: 12972
		[SerializeField]
		[Tooltip("On which axis should the moon and sun rotate?")]
		private Vector3 m_RotationAxis = Vector2.right;

		// Token: 0x040032AD RID: 12973
		[Header("Fog")]
		[SerializeField]
		private FogMode m_FogMode = 3;

		// Token: 0x040032AE RID: 12974
		[SerializeField]
		[Tooltip("Fog intensity variation over the whole day & night cycle.")]
		private AnimationCurve m_FogIntensity;

		// Token: 0x040032AF RID: 12975
		[SerializeField]
		[Tooltip("Fog color variation over the whole day & night cycle.")]
		private Gradient m_FogColor;

		// Token: 0x040032B0 RID: 12976
		[Header("Sun")]
		[SerializeField]
		[Tooltip("Sun intensity variation over the whole day & night cycle.")]
		private AnimationCurve m_SunIntensity;

		// Token: 0x040032B1 RID: 12977
		[SerializeField]
		[Tooltip("Sun color variation over the whole day & night cycle.")]
		private Gradient m_SunColor;

		// Token: 0x040032B2 RID: 12978
		[Header("Moon")]
		[SerializeField]
		[Tooltip("Moon intensity variation over the whole day & night cycle.")]
		private AnimationCurve m_MoonIntensity;

		// Token: 0x040032B3 RID: 12979
		[SerializeField]
		[Tooltip("Moon color variation over the whole day & night cycle.")]
		private Gradient m_MoonColor;

		// Token: 0x040032B4 RID: 12980
		[Header("Skybox")]
		[SerializeField]
		private Material m_Skybox;

		// Token: 0x040032B5 RID: 12981
		[SerializeField]
		private AnimationCurve m_SkyboxBlend;

		// Token: 0x040032B6 RID: 12982
		private ET.TimeOfDay m_InternalState;

		// Token: 0x040032B7 RID: 12983
		private Transform m_SunTransform;

		// Token: 0x040032B8 RID: 12984
		private Transform m_MoonTransform;

		// Token: 0x040032B9 RID: 12985
		private float m_NormalizedTime;

		// Token: 0x040032BA RID: 12986
		private float m_TimeIncrement;
	}
}
