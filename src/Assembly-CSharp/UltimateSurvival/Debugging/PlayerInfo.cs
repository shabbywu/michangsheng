using System;
using UnityEngine;

namespace UltimateSurvival.Debugging
{
	// Token: 0x0200065D RID: 1629
	public class PlayerInfo : MonoBehaviour
	{
		// Token: 0x060033BF RID: 13247 RVA: 0x0016AC63 File Offset: 0x00168E63
		private void Start()
		{
			this.m_Player = GameController.LocalPlayer.GetComponent<PlayerEventHandler>();
		}

		// Token: 0x060033C0 RID: 13248 RVA: 0x0016AC78 File Offset: 0x00168E78
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

		// Token: 0x04002DFA RID: 11770
		private PlayerEventHandler m_Player;

		// Token: 0x04002DFB RID: 11771
		private bool m_Toggle;
	}
}
