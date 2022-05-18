using System;
using UnityEngine;

namespace UltimateSurvival.Debugging
{
	// Token: 0x02000960 RID: 2400
	public class PlayerInfo : MonoBehaviour
	{
		// Token: 0x06003D4D RID: 15693 RVA: 0x0002C36E File Offset: 0x0002A56E
		private void Start()
		{
			this.m_Player = GameController.LocalPlayer.GetComponent<PlayerEventHandler>();
		}

		// Token: 0x06003D4E RID: 15694 RVA: 0x001B3B5C File Offset: 0x001B1D5C
		private void OnGUI()
		{
			this.m_Toggle = GUILayout.Toggle(this.m_Toggle, "Player Info", Array.Empty<GUILayoutOption>());
			if (this.m_Toggle)
			{
				GUILayout.Label("Velocity: " + this.m_Player.Velocity.Get(), Array.Empty<GUILayoutOption>());
				GUILayout.Label("Grounded: " + this.m_Player.IsGrounded.Get().ToString(), Array.Empty<GUILayoutOption>());
				GUILayout.Label("Jumping: " + this.m_Player.Jump.Active.ToString(), Array.Empty<GUILayoutOption>());
				GUILayout.Label("Walking: " + this.m_Player.Walk.Active.ToString(), Array.Empty<GUILayoutOption>());
				GUILayout.Label("Sprinting: " + this.m_Player.Run.Active.ToString(), Array.Empty<GUILayoutOption>());
				GUILayout.Label("Crouching: " + this.m_Player.Crouch.Active.ToString(), Array.Empty<GUILayoutOption>());
				GUILayout.Label("Is Close To An Object: " + this.m_Player.IsCloseToAnObject.Get().ToString(), Array.Empty<GUILayoutOption>());
			}
			Time.timeScale = GUI.HorizontalSlider(new Rect(16f, 64f, 64f, 16f), Time.timeScale, 0f, 1f);
			Time.fixedDeltaTime = 0.02f * Time.timeScale;
		}

		// Token: 0x0400377F RID: 14207
		private PlayerEventHandler m_Player;

		// Token: 0x04003780 RID: 14208
		private bool m_Toggle;
	}
}
