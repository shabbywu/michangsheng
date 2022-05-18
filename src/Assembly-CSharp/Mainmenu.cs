using System;
using UnityEngine;

// Token: 0x0200015C RID: 348
public class Mainmenu : MonoBehaviour
{
	// Token: 0x06000C58 RID: 3160 RVA: 0x000042DD File Offset: 0x000024DD
	private void Start()
	{
	}

	// Token: 0x06000C59 RID: 3161 RVA: 0x00097188 File Offset: 0x00095388
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

	// Token: 0x06000C5A RID: 3162 RVA: 0x000042DD File Offset: 0x000024DD
	private void Update()
	{
	}

	// Token: 0x04000971 RID: 2417
	public Texture2D LogoGame;

	// Token: 0x04000972 RID: 2418
	public GUISkin skin;
}
