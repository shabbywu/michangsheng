using System;
using UnityEngine;

// Token: 0x0200015A RID: 346
public class GameManager : MonoBehaviour
{
	// Token: 0x06000C51 RID: 3153 RVA: 0x0000E53E File Offset: 0x0000C73E
	private void Start()
	{
		this.Playing = true;
	}

	// Token: 0x06000C52 RID: 3154 RVA: 0x000042DD File Offset: 0x000024DD
	private void Update()
	{
	}

	// Token: 0x06000C53 RID: 3155 RVA: 0x0000E547 File Offset: 0x0000C747
	public void GameEvent(string message)
	{
		if (message == "endgame")
		{
			Time.timeScale = 0f;
			this.Playing = false;
		}
	}

	// Token: 0x06000C54 RID: 3156 RVA: 0x00096FF4 File Offset: 0x000951F4
	private void OnGUI()
	{
		if (this.Skin != null)
		{
			GUI.skin = this.Skin;
		}
		if (this.Playing)
		{
			GUI.DrawTexture(new Rect(40f, 40f, 64f, 64f), this.IconZombie);
			GUI.skin.label.normal.textColor = Color.white;
			GUI.skin.label.fontSize = 35;
			GUI.skin.label.alignment = 3;
			GUI.Label(new Rect(125f, 40f, 200f, 64f), this.Score.ToString());
			return;
		}
		Screen.lockCursor = false;
		if (GUI.Button(new Rect((float)(Screen.width / 2 - 80), (float)(Screen.height / 2), 160f, 30f), "Exit"))
		{
			Application.LoadLevel("Mainmenu");
		}
		GUI.skin.label.fontSize = 25;
		GUI.skin.label.alignment = 4;
		GUI.skin.label.normal.textColor = Color.white;
		GUI.Label(new Rect(0f, (float)(Screen.height / 2 - 100), (float)Screen.width, 30f), "Game Over");
	}

	// Token: 0x0400096D RID: 2413
	public GUISkin Skin;

	// Token: 0x0400096E RID: 2414
	public Texture2D IconZombie;

	// Token: 0x0400096F RID: 2415
	[NonSerialized]
	public int Score;

	// Token: 0x04000970 RID: 2416
	[NonSerialized]
	public bool Playing;
}
