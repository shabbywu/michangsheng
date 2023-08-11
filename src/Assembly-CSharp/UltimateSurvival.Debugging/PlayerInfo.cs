using System;
using UnityEngine;

namespace UltimateSurvival.Debugging;

public class PlayerInfo : MonoBehaviour
{
	private PlayerEventHandler m_Player;

	private bool m_Toggle;

	private void Start()
	{
		m_Player = ((Component)GameController.LocalPlayer).GetComponent<PlayerEventHandler>();
	}

	private void OnGUI()
	{
		//IL_016b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0036: Unknown result type (might be due to invalid IL or missing references)
		m_Toggle = GUILayout.Toggle(m_Toggle, "Player Info", Array.Empty<GUILayoutOption>());
		if (m_Toggle)
		{
			GUILayout.Label("Velocity: " + m_Player.Velocity.Get(), Array.Empty<GUILayoutOption>());
			GUILayout.Label("Grounded: " + m_Player.IsGrounded.Get(), Array.Empty<GUILayoutOption>());
			GUILayout.Label("Jumping: " + m_Player.Jump.Active, Array.Empty<GUILayoutOption>());
			GUILayout.Label("Walking: " + m_Player.Walk.Active, Array.Empty<GUILayoutOption>());
			GUILayout.Label("Sprinting: " + m_Player.Run.Active, Array.Empty<GUILayoutOption>());
			GUILayout.Label("Crouching: " + m_Player.Crouch.Active, Array.Empty<GUILayoutOption>());
			GUILayout.Label("Is Close To An Object: " + m_Player.IsCloseToAnObject.Get(), Array.Empty<GUILayoutOption>());
		}
		Time.timeScale = GUI.HorizontalSlider(new Rect(16f, 64f, 64f, 16f), Time.timeScale, 0f, 1f);
		Time.fixedDeltaTime = 0.02f * Time.timeScale;
	}
}
