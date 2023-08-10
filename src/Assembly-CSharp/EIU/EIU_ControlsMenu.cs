using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace EIU;

public class EIU_ControlsMenu : MonoBehaviour
{
	[HideInInspector]
	public List<EIU_AxisBase> Axes = new List<EIU_AxisBase>();

	private Dictionary<string, int> defaultAxes = new Dictionary<string, int>();

	[Header("UI References")]
	[Space(5f)]
	public GameObject axisPrefab;

	public Transform controlsList;

	public EIU_RebindButton rebBtn;

	[HideInInspector]
	public bool rebinding;

	private bool negativeKey;

	private EIU_AxisBase targetAxis;

	private bool initOnce;

	public static EIU_ControlsMenu instance;

	public void Init()
	{
		if (!initOnce)
		{
			SaveAllAxes();
			initOnce = true;
		}
		((Component)rebBtn).gameObject.SetActive(false);
		LoadAllAxes();
		CreateAxisButtons();
		ResetAxes();
		EasyInputUtility.instance.Axes = Axes;
	}

	private void ResetAxes()
	{
		//IL_001a: Unknown result type (might be due to invalid IL or missing references)
		//IL_006c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0043: Unknown result type (might be due to invalid IL or missing references)
		//IL_0095: Unknown result type (might be due to invalid IL or missing references)
		foreach (EIU_AxisBase axis in Axes)
		{
			if ((int)axis.positiveKey == 0)
			{
				int value = 0;
				defaultAxes.TryGetValue(axis.axisName + "pKey", out value);
				axis.positiveKey = (KeyCode)value;
				axis.pUIButton.ChangeKeyText(((object)(KeyCode)(ref axis.positiveKey)).ToString());
				SaveAxis(axis);
			}
			if ((int)axis.negativeKey == 0)
			{
				int value2 = 0;
				defaultAxes.TryGetValue(axis.axisName + "nKey", out value2);
				axis.negativeKey = (KeyCode)value2;
				if (Object.op_Implicit((Object)(object)axis.nUIButton))
				{
					axis.nUIButton.ChangeKeyText(((object)(KeyCode)(ref axis.negativeKey)).ToString());
				}
				SaveAxis(axis);
			}
		}
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

	private void OnGUI()
	{
		//IL_0039: Unknown result type (might be due to invalid IL or missing references)
		//IL_0078: Unknown result type (might be due to invalid IL or missing references)
		//IL_004d: Unknown result type (might be due to invalid IL or missing references)
		//IL_008e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0097: Unknown result type (might be due to invalid IL or missing references)
		//IL_0096: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a6: Unknown result type (might be due to invalid IL or missing references)
		if (!rebinding)
		{
			return;
		}
		((Component)controlsList).gameObject.SetActive(false);
		Event current = Event.current;
		if (current == null)
		{
			return;
		}
		if (current.isKey && !current.isMouse && (int)current.keyCode != 0)
		{
			ChangeInputKey(targetAxis.axisName, current.keyCode, negativeKey);
			CloseRebindingDialog();
		}
		if (current.isMouse && !EventSystem.current.IsPointerOverGameObject())
		{
			KeyCode val = (KeyCode)0;
			switch (current.button)
			{
			case 0:
				val = (KeyCode)323;
				break;
			case 1:
				val = (KeyCode)324;
				break;
			}
			if ((int)val != 0)
			{
				ChangeInputKey(targetAxis.axisName, val, negativeKey);
				CloseRebindingDialog();
			}
		}
	}

	public void ResetAllAxes()
	{
		//IL_003b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0062: Unknown result type (might be due to invalid IL or missing references)
		foreach (EIU_AxisBase axis in Axes)
		{
			int value = 0;
			defaultAxes.TryGetValue(axis.axisName + "pKey", out value);
			axis.positiveKey = (KeyCode)value;
			int value2 = 0;
			defaultAxes.TryGetValue(axis.axisName + "nKey", out value2);
			axis.negativeKey = (KeyCode)value2;
			axis.pUIButton.ChangeKeyText(((object)(KeyCode)(ref axis.positiveKey)).ToString());
			if (Object.op_Implicit((Object)(object)axis.nUIButton))
			{
				axis.nUIButton.ChangeKeyText(((object)(KeyCode)(ref axis.negativeKey)).ToString());
			}
			SaveAxis(axis);
		}
	}

	public void ChangeInputKey(string name, KeyCode newKey, bool negative = false)
	{
		//IL_003f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0040: Unknown result type (might be due to invalid IL or missing references)
		//IL_001a: Unknown result type (might be due to invalid IL or missing references)
		//IL_001b: Unknown result type (might be due to invalid IL or missing references)
		EIU_AxisBase eIU_AxisBase = ReturnAxis(name);
		if (eIU_AxisBase == null)
		{
			Debug.Log((object)"Doesn't exist!");
			return;
		}
		if (negative)
		{
			eIU_AxisBase.negativeKey = newKey;
			eIU_AxisBase.nUIButton.ChangeKeyText(((object)(KeyCode)(ref eIU_AxisBase.negativeKey)).ToString());
		}
		else
		{
			eIU_AxisBase.positiveKey = newKey;
			eIU_AxisBase.pUIButton.ChangeKeyText(((object)(KeyCode)(ref eIU_AxisBase.positiveKey)).ToString());
		}
		SaveAxis(eIU_AxisBase);
	}

	private void CreateAxisButtons()
	{
		//IL_003b: Unknown result type (might be due to invalid IL or missing references)
		//IL_004b: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ca: Unknown result type (might be due to invalid IL or missing references)
		foreach (EIU_AxisBase axis in Axes)
		{
			GameObject obj = Object.Instantiate<GameObject>(axisPrefab);
			obj.transform.SetParent(controlsList);
			obj.transform.localScale = Vector3.one;
			obj.transform.localPosition = Vector3.zero;
			EIU_AxisButton component = obj.GetComponent<EIU_AxisButton>();
			component.init(axis.axisName, axis.pKeyDescription, ((object)(KeyCode)(ref axis.positiveKey)).ToString());
			axis.pUIButton = component;
			if (axis.nKeyDescription != "")
			{
				GameObject obj2 = Object.Instantiate<GameObject>(axisPrefab);
				obj2.transform.SetParent(controlsList);
				obj2.transform.localScale = Vector3.one;
				obj2.transform.localPosition = Vector3.zero;
				EIU_AxisButton component2 = obj2.GetComponent<EIU_AxisButton>();
				component2.init(axis.axisName, axis.nKeyDescription, ((object)(KeyCode)(ref axis.negativeKey)).ToString(), nKey: true);
				axis.nUIButton = component2;
			}
		}
	}

	private EIU_AxisBase ReturnAxis(string name)
	{
		EIU_AxisBase result = null;
		for (int i = 0; i < Axes.Count; i++)
		{
			if (string.Equals(name, Axes[i].axisName))
			{
				result = Axes[i];
			}
		}
		return result;
	}

	public void OpenRebindButtonDialog(string axisName, bool negative)
	{
		targetAxis = ReturnAxis(axisName);
		rebinding = true;
		if (!negative)
		{
			rebBtn.init(targetAxis.pKeyDescription);
		}
		else
		{
			rebBtn.init(targetAxis.nKeyDescription);
		}
		((Component)rebBtn).gameObject.SetActive(true);
		negativeKey = negative;
	}

	private void CloseRebindingDialog()
	{
		rebinding = false;
		((Component)rebBtn).gameObject.SetActive(false);
		((Component)controlsList).gameObject.SetActive(true);
	}

	public void CancelRebinding()
	{
		CloseRebindingDialog();
	}

	private void SaveAllAxes()
	{
		for (int i = 0; i < Axes.Count; i++)
		{
			SaveAxesDefault(Axes[i]);
		}
	}

	private void SaveAxis(EIU_AxisBase axis)
	{
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		//IL_001b: Expected I4, but got Unknown
		//IL_002c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0036: Expected I4, but got Unknown
		PlayerPrefs.SetInt(axis.axisName + "pKey", (int)axis.positiveKey);
		PlayerPrefs.SetInt(axis.axisName + "nKey", (int)axis.negativeKey);
	}

	private void SaveAxesDefault(EIU_AxisBase axis)
	{
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		//IL_0021: Expected I4, but got Unknown
		//IL_0038: Unknown result type (might be due to invalid IL or missing references)
		//IL_0042: Expected I4, but got Unknown
		defaultAxes.Add(axis.axisName + "pKey", (int)axis.positiveKey);
		defaultAxes.Add(axis.axisName + "nKey", (int)axis.negativeKey);
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

	public static EIU_ControlsMenu Instance()
	{
		return instance;
	}

	private void Awake()
	{
		instance = this;
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
		bool result = false;
		for (int i = 0; i < Axes.Count; i++)
		{
			if (string.Equals(Axes[i].axisName, name))
			{
				result = Axes[i].positive;
			}
		}
		return result;
	}
}
