using System;
using KBEngine;
using UnityEngine;

namespace UltimateSurvival
{
	// Token: 0x020005C5 RID: 1477
	public class TimeOfDay : MonoSingleton<TimeOfDay>
	{
		// Token: 0x170003FF RID: 1023
		// (get) Token: 0x06002FBF RID: 12223 RVA: 0x001589CC File Offset: 0x00156BCC
		// (set) Token: 0x06002FC0 RID: 12224 RVA: 0x001589D4 File Offset: 0x00156BD4
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

		// Token: 0x17000400 RID: 1024
		// (get) Token: 0x06002FC1 RID: 12225 RVA: 0x00158A13 File Offset: 0x00156C13
		// (set) Token: 0x06002FC2 RID: 12226 RVA: 0x00158A1B File Offset: 0x00156C1B
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

		// Token: 0x17000401 RID: 1025
		// (get) Token: 0x06002FC3 RID: 12227 RVA: 0x00158A24 File Offset: 0x00156C24
		public int DayDuration
		{
			get
			{
				return this.m_DayDuration;
			}
		}

		// Token: 0x06002FC4 RID: 12228 RVA: 0x00158A2C File Offset: 0x00156C2C
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

		// Token: 0x06002FC5 RID: 12229 RVA: 0x00158AD8 File Offset: 0x00156CD8
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

		// Token: 0x06002FC6 RID: 12230 RVA: 0x00158BA4 File Offset: 0x00156DA4
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

		// Token: 0x06002FC7 RID: 12231 RVA: 0x00158D28 File Offset: 0x00156F28
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

		// Token: 0x06002FC8 RID: 12232 RVA: 0x00158F38 File Offset: 0x00157138
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

		// Token: 0x06002FC9 RID: 12233 RVA: 0x0015900C File Offset: 0x0015720C
		private void OnValidate()
		{
			this.AccommodateEditorChanges();
			this.m_SunTransform = this.m_Sun.transform;
			this.m_MoonTransform = this.m_Moon.transform;
			this.Update();
		}

		// Token: 0x06002FCA RID: 12234 RVA: 0x0015903C File Offset: 0x0015723C
		private void AccommodateEditorChanges()
		{
			this.m_TimeIncrement = 1f / (float)this.m_DayDuration;
			this.m_NormalizedTime = (float)this.m_CurrentHour / 24f;
			RenderSettings.fogMode = this.m_FogMode;
		}

		// Token: 0x04002A0F RID: 10767
		public Value<ET.TimeOfDay> State = new Value<ET.TimeOfDay>(ET.TimeOfDay.Day);

		// Token: 0x04002A10 RID: 10768
		[Header("Setup")]
		[SerializeField]
		private Light m_Sun;

		// Token: 0x04002A11 RID: 10769
		[SerializeField]
		private Light m_Moon;

		// Token: 0x04002A12 RID: 10770
		[Header("General")]
		[SerializeField]
		private bool m_StopTime;

		// Token: 0x04002A13 RID: 10771
		[SerializeField]
		private bool m_ShowGUI;

		// Token: 0x04002A14 RID: 10772
		[SerializeField]
		[Range(0f, 24f)]
		[Tooltip("The current hour (00:00 AM to 12:00 PM to 24:00 PM)")]
		private int m_CurrentHour = 6;

		// Token: 0x04002A15 RID: 10773
		[SerializeField]
		[Tooltip("How many seconds are in a day.")]
		private int m_DayDuration = 900;

		// Token: 0x04002A16 RID: 10774
		[SerializeField]
		[Tooltip("On which axis should the moon and sun rotate?")]
		private Vector3 m_RotationAxis = Vector2.right;

		// Token: 0x04002A17 RID: 10775
		[Header("Fog")]
		[SerializeField]
		private FogMode m_FogMode = 3;

		// Token: 0x04002A18 RID: 10776
		[SerializeField]
		[Tooltip("Fog intensity variation over the whole day & night cycle.")]
		private AnimationCurve m_FogIntensity;

		// Token: 0x04002A19 RID: 10777
		[SerializeField]
		[Tooltip("Fog color variation over the whole day & night cycle.")]
		private Gradient m_FogColor;

		// Token: 0x04002A1A RID: 10778
		[Header("Sun")]
		[SerializeField]
		[Tooltip("Sun intensity variation over the whole day & night cycle.")]
		private AnimationCurve m_SunIntensity;

		// Token: 0x04002A1B RID: 10779
		[SerializeField]
		[Tooltip("Sun color variation over the whole day & night cycle.")]
		private Gradient m_SunColor;

		// Token: 0x04002A1C RID: 10780
		[Header("Moon")]
		[SerializeField]
		[Tooltip("Moon intensity variation over the whole day & night cycle.")]
		private AnimationCurve m_MoonIntensity;

		// Token: 0x04002A1D RID: 10781
		[SerializeField]
		[Tooltip("Moon color variation over the whole day & night cycle.")]
		private Gradient m_MoonColor;

		// Token: 0x04002A1E RID: 10782
		[Header("Skybox")]
		[SerializeField]
		private Material m_Skybox;

		// Token: 0x04002A1F RID: 10783
		[SerializeField]
		private AnimationCurve m_SkyboxBlend;

		// Token: 0x04002A20 RID: 10784
		private ET.TimeOfDay m_InternalState;

		// Token: 0x04002A21 RID: 10785
		private Transform m_SunTransform;

		// Token: 0x04002A22 RID: 10786
		private Transform m_MoonTransform;

		// Token: 0x04002A23 RID: 10787
		private float m_NormalizedTime;

		// Token: 0x04002A24 RID: 10788
		private float m_TimeIncrement;
	}
}
