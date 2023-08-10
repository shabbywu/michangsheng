using System.Collections.Generic;
using UnityEngine;

namespace UltimateSurvival;

public class MouseLook : PlayerBehaviour
{
	[Header("General")]
	[SerializeField]
	[Tooltip("The camera root which will be rotated up & down (on the X axis).")]
	private Transform m_LookRoot;

	[SerializeField]
	private Transform m_PlayerRoot;

	[SerializeField]
	[Tooltip("The up & down rotation will be inverted, if checked.")]
	private bool m_Invert;

	[SerializeField]
	[Tooltip("If checked, a button will show up which can lock the cursor.")]
	private bool m_ShowLockButton = true;

	[SerializeField]
	[Tooltip("If checked, you can unlock the cursor by pressing the Escape / Esc key.")]
	private bool m_CanUnlock = true;

	[Header("Motion")]
	[SerializeField]
	[Tooltip("The higher it is, the faster the camera will rotate.")]
	private float m_Sensitivity = 5f;

	[SerializeField]
	[Range(0f, 20f)]
	private int m_SmoothSteps = 10;

	[SerializeField]
	[Range(0f, 1f)]
	private float m_SmoothWeight = 0.4f;

	[SerializeField]
	private float m_RollAngle = 10f;

	[SerializeField]
	private float m_RollSpeed = 3f;

	[Header("Rotation Limits")]
	[SerializeField]
	private Vector2 m_DefaultLookLimits = new Vector2(-60f, 90f);

	private float m_CurrentRollAngle;

	private bool m_InventoryIsOpen;

	private Vector2 m_LookAngles;

	private int m_LastLookFrame;

	private Vector2 m_CurrentMouseLook;

	private Vector2 m_SmoothMove;

	private List<Vector2> m_SmoothBuffer = new List<Vector2>();

	private void Start()
	{
		if (!Object.op_Implicit((Object)(object)m_LookRoot))
		{
			Debug.LogErrorFormat((Object)(object)this, "Assign the look root in the inspector!", new object[1] { ((Object)this).name });
			((Behaviour)this).enabled = false;
		}
		MonoSingleton<InventoryController>.Instance.State.AddChangeListener(OnChanged_InventoryState);
		Cursor.visible = false;
		Cursor.lockState = (CursorLockMode)1;
	}

	private void OnChanged_InventoryState()
	{
		m_InventoryIsOpen = !MonoSingleton<InventoryController>.Instance.IsClosed;
		if (m_InventoryIsOpen)
		{
			Cursor.visible = true;
			Cursor.lockState = (CursorLockMode)0;
		}
		else
		{
			Cursor.visible = false;
			Cursor.lockState = (CursorLockMode)1;
		}
	}

	private void OnGUI()
	{
		//IL_001f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0025: Invalid comparison between Unknown and I4
		//IL_0049: Unknown result type (might be due to invalid IL or missing references)
		//IL_0034: Unknown result type (might be due to invalid IL or missing references)
		//IL_003b: Invalid comparison between Unknown and I4
		//IL_0064: Unknown result type (might be due to invalid IL or missing references)
		//IL_0076: Unknown result type (might be due to invalid IL or missing references)
		//IL_007c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0082: Unknown result type (might be due to invalid IL or missing references)
		if (m_ShowLockButton)
		{
			Vector2 val = default(Vector2);
			((Vector2)(ref val))._002Ector(256f, 24f);
			if ((int)Event.current.type == 4 && m_CanUnlock && (int)Event.current.keyCode == 27)
			{
				Cursor.visible = true;
				Cursor.lockState = (CursorLockMode)0;
			}
			if ((int)Cursor.lockState == 0 && !m_InventoryIsOpen && GUI.Button(new Rect((float)Screen.width * 0.5f - val.x / 2f, 16f, val.x, val.y), "Lock Cursor (Hit 'Esc' to unlock)"))
			{
				Cursor.visible = false;
				Cursor.lockState = (CursorLockMode)1;
			}
		}
	}

	private void Update()
	{
		//IL_0055: Unknown result type (might be due to invalid IL or missing references)
		//IL_005b: Invalid comparison between Unknown and I4
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		//IL_0019: Invalid comparison between Unknown and I4
		if (base.Player.ViewLocked.Is(value: false) && (int)Cursor.lockState == 1 && !base.Player.Sleep.Active && base.Player.Health.Get() > 0f)
		{
			LookAround();
		}
		base.Player.ViewLocked.Set((int)Cursor.lockState != 1 || base.Player.SelectBuildable.Active);
	}

	private void LookAround()
	{
		//IL_00a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e9: Unknown result type (might be due to invalid IL or missing references)
		//IL_010e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0129: Unknown result type (might be due to invalid IL or missing references)
		CalculateMouseInput(Time.deltaTime);
		m_LookAngles.x += m_CurrentMouseLook.x * m_Sensitivity * (m_Invert ? 1f : (-1f));
		m_LookAngles.y += m_CurrentMouseLook.y * m_Sensitivity;
		m_LookAngles.x = ClampAngle(m_LookAngles.x, m_DefaultLookLimits.x, m_DefaultLookLimits.y);
		m_CurrentRollAngle = Mathf.Lerp(m_CurrentRollAngle, base.Player.LookInput.Get().x * m_RollAngle, Time.deltaTime * m_RollSpeed);
		m_LookRoot.localRotation = Quaternion.Euler(m_LookAngles.x, 0f, m_CurrentRollAngle);
		m_PlayerRoot.localRotation = Quaternion.Euler(0f, m_LookAngles.y, 0f);
		base.Player.LookDirection.Set(m_LookRoot.forward);
	}

	private float ClampAngle(float angle, float min, float max)
	{
		if (angle > 360f)
		{
			angle -= 360f;
		}
		else if (angle < -360f)
		{
			angle += 360f;
		}
		return Mathf.Clamp(angle, min, max);
	}

	private void CalculateMouseInput(float deltaTime)
	{
		//IL_002e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0033: Unknown result type (might be due to invalid IL or missing references)
		//IL_0085: Unknown result type (might be due to invalid IL or missing references)
		//IL_0095: Unknown result type (might be due to invalid IL or missing references)
		//IL_009a: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fb: Unknown result type (might be due to invalid IL or missing references)
		if (m_LastLookFrame != Time.frameCount)
		{
			m_LastLookFrame = Time.frameCount;
			m_SmoothMove = new Vector2(Input.GetAxisRaw("Mouse Y"), Input.GetAxisRaw("Mouse X"));
			m_SmoothSteps = Mathf.Clamp(m_SmoothSteps, 1, 20);
			m_SmoothWeight = Mathf.Clamp01(m_SmoothWeight);
			while (m_SmoothBuffer.Count > m_SmoothSteps)
			{
				m_SmoothBuffer.RemoveAt(0);
			}
			m_SmoothBuffer.Add(m_SmoothMove);
			float num = 1f;
			Vector2 val = Vector2.zero;
			float num2 = 0f;
			for (int num3 = m_SmoothBuffer.Count - 1; num3 > 0; num3--)
			{
				val += m_SmoothBuffer[num3] * num;
				num2 += num;
				num *= m_SmoothWeight / (deltaTime * 60f);
			}
			num2 = Mathf.Max(1f, num2);
			m_CurrentMouseLook = val / num2;
		}
	}
}
