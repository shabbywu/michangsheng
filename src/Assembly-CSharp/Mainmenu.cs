using System;
using UnityEngine;

// Token: 0x020000E5 RID: 229
public class Mainmenu : MonoBehaviour
{
	// Token: 0x06000B69 RID: 2921 RVA: 0x00004095 File Offset: 0x00002295
	private void Start()
	{
	}

	// Token: 0x06000B6A RID: 2922 RVA: 0x00045658 File Offset: 0x00043858
	private void OnGUI()
	{
		Screen.lockCursor = false;
		if (this.skin)
		{
			GUI.skin = this.skin;
		}
		GUI.DrawTexture(new Rect((float)(Screen.width / 2) - (float)this.LogoGame.width * 0.5f / 2f, (float)(Screen.height / 2 - 200), (float)this.LogoGame.width * 0.5f, (float)this.LogoGame.height * 0.5f), this.LogoGame);
		if (GUI.Button(new Rect((float)(Screen.width / 2 - 80), (float)(Screen.height / 2 + 20), 160f, 30f), "Start Demo"))
		{
			Application.LoadLevel("Demo");
		}
		GUI.Button(new Rect((float)(Screen.width / 2 - 80), (float)(Screen.height / 2 + 60), 160f, 30f), "Purchase");
		GUI.skin.label.fontSize = 12;
		GUI.skin.label.alignment = 4;
		GUI.skin.label.normal.textColor = Color.white;
		GUI.Label(new Rect(0f, (float)(Screen.height - 50), (float)Screen.width, 30f), "Dungeon Breaker Starter Kit beta.  By Rachan Neamprasert | www.hardworkerstudio.com");
	}

	// Token: 0x06000B6B RID: 2923 RVA: 0x00004095 File Offset: 0x00002295
	private void Update()
	{
	}

	// Token: 0x04000796 RID: 1942
	public Texture2D LogoGame;

	// Token: 0x04000797 RID: 1943
	public GUISkin skin;
}
