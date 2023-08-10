using System;
using UnityEngine;
using UnityEngine.UI;

namespace UltimateSurvival.GUISystem;

[Serializable]
public class CrosshairData
{
	[SerializeField]
	private string m_ItemName;

	[SerializeField]
	private bool m_HideWhenAiming = true;

	[SerializeField]
	private Color m_NormalColor = Color.white;

	[SerializeField]
	private Color m_OnEntityColor = Color.red;

	[SerializeField]
	private CrosshairType m_Type;

	[SerializeField]
	private Image m_Image;

	[SerializeField]
	private Sprite m_Sprite;

	[SerializeField]
	private Vector2 m_Size = new Vector2(64f, 64f);

	[SerializeField]
	private DynamicCrosshair m_Crosshair;

	[SerializeField]
	[Clamp(0f, 256f)]
	private float m_IdleDistance = 32f;

	[SerializeField]
	[Clamp(0f, 256f)]
	private float m_CrouchDistance = 24f;

	[SerializeField]
	[Clamp(0f, 256f)]
	private float m_WalkDistance = 36f;

	[SerializeField]
	[Clamp(0f, 256f)]
	private float m_RunDistance = 48f;

	[SerializeField]
	[Clamp(0f, 256f)]
	private float m_JumpDistance = 54f;

	public string ItemName => m_ItemName;

	public bool HideWhenAiming => m_HideWhenAiming;

	public static implicit operator bool(CrosshairData cd)
	{
		return cd != null;
	}

	public void Update(PlayerEventHandler player)
	{
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		//IL_001c: Unknown result type (might be due to invalid IL or missing references)
		//IL_002c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0031: Unknown result type (might be due to invalid IL or missing references)
		//IL_0075: Unknown result type (might be due to invalid IL or missing references)
		//IL_006d: Unknown result type (might be due to invalid IL or missing references)
		//IL_012b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0123: Unknown result type (might be due to invalid IL or missing references)
		RaycastData raycastData = player.RaycastData.Get();
		bool flag = false;
		if ((bool)raycastData)
		{
			RaycastHit hitInfo = raycastData.HitInfo;
			if (Object.op_Implicit((Object)(object)((RaycastHit)(ref hitInfo)).collider))
			{
				hitInfo = raycastData.HitInfo;
				if (Object.op_Implicit((Object)(object)((Component)((RaycastHit)(ref hitInfo)).collider).GetComponent<HitBox>()))
				{
					flag = true;
				}
			}
		}
		if (m_Type == CrosshairType.Dynamic && Object.op_Implicit((Object)(object)m_Crosshair))
		{
			m_Crosshair.SetColor(flag ? m_OnEntityColor : m_NormalColor);
			float num = m_IdleDistance;
			if (player.Crouch.Active)
			{
				num = m_CrouchDistance;
			}
			else if (player.Walk.Active)
			{
				num = m_WalkDistance;
			}
			else if (player.Run.Active)
			{
				num = m_RunDistance;
			}
			else if (player.Jump.Active)
			{
				num = m_JumpDistance;
			}
			m_Crosshair.SetDistance(Mathf.Lerp(m_Crosshair.Distance, num, Time.deltaTime * 10f));
		}
		else if (m_Type == CrosshairType.Simple && Object.op_Implicit((Object)(object)m_Image))
		{
			((Graphic)m_Image).color = (flag ? m_OnEntityColor : m_NormalColor);
		}
	}

	public void SetActive(bool active)
	{
		//IL_0061: Unknown result type (might be due to invalid IL or missing references)
		if (m_Type == CrosshairType.Dynamic && Object.op_Implicit((Object)(object)m_Crosshair))
		{
			m_Crosshair.SetActive(active);
		}
		else if (m_Type == CrosshairType.Simple && Object.op_Implicit((Object)(object)m_Image))
		{
			((Behaviour)m_Image).enabled = active;
			m_Image.sprite = m_Sprite;
			((Graphic)m_Image).rectTransform.sizeDelta = m_Size;
		}
	}
}
