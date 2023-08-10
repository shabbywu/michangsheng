using System.Collections.Generic;
using UnityEngine;

namespace EIU;

public class EasyInputUtility : MonoBehaviour
{
	public static EasyInputUtility instance;

	private EIU_ControlsMenu menu;

	[Header("Define All Axes and Buttons Here")]
	[Space(5f)]
	public List<EIU_AxisBase> Axes = new List<EIU_AxisBase>();

	private void Awake()
	{
		menu = Object.FindObjectOfType<EIU_ControlsMenu>();
		if ((Object)(object)instance != (Object)null)
		{
			Object.Destroy((Object)(object)((Component)this).gameObject);
		}
		else
		{
			instance = this;
			Object.DontDestroyOnLoad((Object)(object)((Component)this).gameObject);
		}
		if (Object.op_Implicit((Object)(object)menu))
		{
			menu.Axes = Axes;
			menu.Init();
		}
		LoadAllAxes();
	}

	private void FixedUpdate()
	{
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		//IL_0024: Unknown result type (might be due to invalid IL or missing references)
		for (int i = 0; i < Axes.Count; i++)
		{
			EIU_AxisBase eIU_AxisBase = Axes[i];
			eIU_AxisBase.negative = Input.GetKey(eIU_AxisBase.negativeKey);
			eIU_AxisBase.positive = Input.GetKey(eIU_AxisBase.positiveKey);
			eIU_AxisBase.targetAxis = (eIU_AxisBase.negative ? (-1) : (eIU_AxisBase.positive ? 1 : 0));
			eIU_AxisBase.axis = Mathf.MoveTowards(eIU_AxisBase.axis, eIU_AxisBase.targetAxis, Time.deltaTime * eIU_AxisBase.sensitivity);
		}
	}

	public float GetAxis(string name)
	{
		float result = 0f;
		for (int i = 0; i < Axes.Count; i++)
		{
			if (string.Equals(Axes[i].axisName, name))
			{
				result = Axes[i].axis;
			}
		}
		return result;
	}

	public bool GetButton(string name)
	{
		//IL_003d: Unknown result type (might be due to invalid IL or missing references)
		bool result = false;
		for (int i = 0; i < Axes.Count; i++)
		{
			if (string.Equals(Axes[i].axisName, name))
			{
				result = Axes[i].positive;
				result = Input.GetKey(Axes[i].positiveKey);
			}
		}
		return result;
	}

	public bool GetButtonDown(string name)
	{
		//IL_002b: Unknown result type (might be due to invalid IL or missing references)
		bool result = false;
		for (int i = 0; i < Axes.Count; i++)
		{
			if (string.Equals(Axes[i].axisName, name))
			{
				result = Input.GetKeyDown(Axes[i].positiveKey);
			}
		}
		return result;
	}

	private void LoadAllAxes()
	{
		//IL_003e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0044: Unknown result type (might be due to invalid IL or missing references)
		for (int i = 0; i < Axes.Count; i++)
		{
			EIU_AxisBase eIU_AxisBase = Axes[i];
			int @int = PlayerPrefs.GetInt(eIU_AxisBase.axisName + "pKey");
			int int2 = PlayerPrefs.GetInt(eIU_AxisBase.axisName + "nKey");
			eIU_AxisBase.positiveKey = (KeyCode)@int;
			eIU_AxisBase.negativeKey = (KeyCode)int2;
		}
	}
}
