using System;
using KBEngine;
using UnityEngine;

namespace UltimateSurvival;

public class TimeOfDay : MonoSingleton<TimeOfDay>
{
	public Value<ET.TimeOfDay> State = new Value<ET.TimeOfDay>(ET.TimeOfDay.Day);

	[Header("Setup")]
	[SerializeField]
	private Light m_Sun;

	[SerializeField]
	private Light m_Moon;

	[Header("General")]
	[SerializeField]
	private bool m_StopTime;

	[SerializeField]
	private bool m_ShowGUI;

	[SerializeField]
	[Range(0f, 24f)]
	[Tooltip("The current hour (00:00 AM to 12:00 PM to 24:00 PM)")]
	private int m_CurrentHour = 6;

	[SerializeField]
	[Tooltip("How many seconds are in a day.")]
	private int m_DayDuration = 900;

	[SerializeField]
	[Tooltip("On which axis should the moon and sun rotate?")]
	private Vector3 m_RotationAxis = Vector2.op_Implicit(Vector2.right);

	[Header("Fog")]
	[SerializeField]
	private FogMode m_FogMode = (FogMode)3;

	[SerializeField]
	[Tooltip("Fog intensity variation over the whole day & night cycle.")]
	private AnimationCurve m_FogIntensity;

	[SerializeField]
	[Tooltip("Fog color variation over the whole day & night cycle.")]
	private Gradient m_FogColor;

	[Header("Sun")]
	[SerializeField]
	[Tooltip("Sun intensity variation over the whole day & night cycle.")]
	private AnimationCurve m_SunIntensity;

	[SerializeField]
	[Tooltip("Sun color variation over the whole day & night cycle.")]
	private Gradient m_SunColor;

	[Header("Moon")]
	[SerializeField]
	[Tooltip("Moon intensity variation over the whole day & night cycle.")]
	private AnimationCurve m_MoonIntensity;

	[SerializeField]
	[Tooltip("Moon color variation over the whole day & night cycle.")]
	private Gradient m_MoonColor;

	[Header("Skybox")]
	[SerializeField]
	private Material m_Skybox;

	[SerializeField]
	private AnimationCurve m_SkyboxBlend;

	private ET.TimeOfDay m_InternalState;

	private Transform m_SunTransform;

	private Transform m_MoonTransform;

	private float m_NormalizedTime;

	private float m_TimeIncrement;

	public float NormalizedTime
	{
		get
		{
			return m_NormalizedTime;
		}
		set
		{
			m_NormalizedTime = (float.IsNaN(Mathf.Repeat(value, 1f)) ? 0f : Mathf.Repeat(value, 1f));
			m_CurrentHour = (int)(m_NormalizedTime * 24f);
		}
	}

	public int CurrentHour
	{
		get
		{
			return m_CurrentHour;
		}
		set
		{
			m_CurrentHour = value;
		}
	}

	public int DayDuration => m_DayDuration;

	private void Awake()
	{
		if (!Object.op_Implicit((Object)(object)m_Sun) || !Object.op_Implicit((Object)(object)m_Moon))
		{
			Debug.LogError((object)"The moon or sun are not assigned in the inspector! please assign them and restart the game.", (Object)(object)this);
			((Behaviour)this).enabled = false;
			return;
		}
		m_SunTransform = ((Component)m_Sun).transform;
		m_MoonTransform = ((Component)m_Moon).transform;
		AccommodateEditorChanges();
		m_InternalState = ((!NormalizedTime.IsInRangeLimitsExcluded(0.25f, 0.75f)) ? ET.TimeOfDay.Night : ET.TimeOfDay.Day);
		State.Set(m_InternalState);
		((MonoBehaviour)this).InvokeRepeating("DayZombie", 2f, 3f);
	}

	public void DayZombie()
	{
		if (m_InternalState == ET.TimeOfDay.Day)
		{
			try
			{
				if (KBEngineApp.app.player().className == "Avatar")
				{
					Avatar avatar = (Avatar)KBEngineApp.app.player();
					if (avatar != null)
					{
						string index = string.Concat(avatar.roleTypeCell);
						_ = jsonData.instance.heroJsonData[index]["heroType"].str == "Zombie";
					}
				}
			}
			catch (Exception ex)
			{
				Debug.LogError((object)ex);
			}
		}
		if (m_InternalState == ET.TimeOfDay.Night && CurrentHour < 22 && CurrentHour > 16)
		{
			useResteRole(jsonData.instance.ZombieBossID);
		}
		useResteRole(51);
	}

	private void OnGUI()
	{
		//IL_0027: Unknown result type (might be due to invalid IL or missing references)
		//IL_009d: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0109: Unknown result type (might be due to invalid IL or missing references)
		//IL_0144: Unknown result type (might be due to invalid IL or missing references)
		if (m_ShowGUI)
		{
			Rect val = default(Rect);
			((Rect)(ref val))._002Ector(8f, 8f, 128f, 20f);
			m_StopTime = GUI.Toggle(val, m_StopTime, "Stop Time?");
			string text = "Time: " + m_CurrentHour + " ";
			text = ((!m_NormalizedTime.IsInRangeLimitsIncluded(0.5f, 1f)) ? (text + "AM") : (text + "PM"));
			((Rect)(ref val)).y = ((Rect)(ref val)).yMax + 4f;
			GUI.Label(val, text);
			((Rect)(ref val)).y = ((Rect)(ref val)).yMax;
			if (m_StopTime)
			{
				m_NormalizedTime = GUI.HorizontalSlider(val, m_NormalizedTime, 0f, 1f);
			}
			else
			{
				GUI.HorizontalSlider(val, m_NormalizedTime, 0f, 1f);
			}
			((Rect)(ref val)).y = ((Rect)(ref val)).yMax;
			((Rect)(ref val)).width = 256f;
			GUI.Label(val, "Day Duration: " + m_DayDuration + " seconds");
			((Rect)(ref val)).y = ((Rect)(ref val)).yMax;
			((Rect)(ref val)).width = 128f;
			m_DayDuration = (int)GUI.HorizontalSlider(val, (float)m_DayDuration, 0f, 1000f);
			m_TimeIncrement = 1f / (float)m_DayDuration;
		}
	}

	private void Update()
	{
		//IL_006b: Unknown result type (might be due to invalid IL or missing references)
		//IL_007c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0093: Unknown result type (might be due to invalid IL or missing references)
		//IL_0098: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_0103: Unknown result type (might be due to invalid IL or missing references)
		//IL_011a: Unknown result type (might be due to invalid IL or missing references)
		//IL_011f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0157: Unknown result type (might be due to invalid IL or missing references)
		if (!m_StopTime)
		{
			NormalizedTime += Time.deltaTime * m_TimeIncrement;
		}
		m_CurrentHour = (int)(NormalizedTime * 24f);
		RenderSettings.ambientIntensity = Mathf.Clamp01(m_Sun.intensity);
		RenderSettings.fogDensity = m_FogIntensity.Evaluate(NormalizedTime);
		RenderSettings.fogColor = m_FogColor.Evaluate(NormalizedTime);
		m_SunTransform.rotation = Quaternion.Euler(m_RotationAxis * (NormalizedTime * 360f - 90f));
		m_Sun.intensity = m_SunIntensity.Evaluate(NormalizedTime);
		m_Sun.color = m_SunColor.Evaluate(NormalizedTime);
		((Behaviour)m_Sun).enabled = m_Sun.intensity > 0f;
		m_MoonTransform.rotation = Quaternion.Euler(-m_RotationAxis * (NormalizedTime * 360f - 90f));
		m_Moon.intensity = m_MoonIntensity.Evaluate(NormalizedTime);
		m_Moon.color = m_MoonColor.Evaluate(NormalizedTime);
		((Behaviour)m_Moon).enabled = m_Moon.intensity > 0f;
		if (Object.op_Implicit((Object)(object)m_Skybox))
		{
			m_Skybox.SetFloat("_Blend", m_SkyboxBlend.Evaluate(m_NormalizedTime));
		}
		ET.TimeOfDay internalState = m_InternalState;
		m_InternalState = ((!NormalizedTime.IsInRangeLimitsExcluded(0.25f, 0.75f)) ? ET.TimeOfDay.Night : ET.TimeOfDay.Day);
		if (internalState != m_InternalState)
		{
			State.Set(m_InternalState);
			_ = m_InternalState;
			_ = 1;
		}
		GameController.NormalizedTime = NormalizedTime;
	}

	public void useResteRole(int itemid)
	{
		//IL_0037: Unknown result type (might be due to invalid IL or missing references)
		try
		{
			if (!(KBEngineApp.app.player().className == "Avatar"))
			{
				return;
			}
			Avatar avatar = (Avatar)KBEngineApp.app.player();
			if (avatar == null)
			{
				return;
			}
			Inventory component = ((Component)((GameObject)avatar.renderObj).transform).GetComponent<PlayerInventory>().inventory.GetComponent<Inventory>();
			ulong num = 0uL;
			component.updateItemList();
			foreach (Item item in component.ItemsInInventory)
			{
				if (item.itemID == itemid)
				{
					num = item.itemUUID;
				}
			}
			if (num != 0L)
			{
				avatar.useItemRequest(num);
				_ = 50;
			}
		}
		catch (Exception)
		{
		}
	}

	private void OnValidate()
	{
		AccommodateEditorChanges();
		m_SunTransform = ((Component)m_Sun).transform;
		m_MoonTransform = ((Component)m_Moon).transform;
		Update();
	}

	private void AccommodateEditorChanges()
	{
		//IL_0027: Unknown result type (might be due to invalid IL or missing references)
		m_TimeIncrement = 1f / (float)m_DayDuration;
		m_NormalizedTime = (float)m_CurrentHour / 24f;
		RenderSettings.fogMode = m_FogMode;
	}
}
