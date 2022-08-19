using System;
using UnityEngine;

// Token: 0x020000E3 RID: 227
public class GameManager : MonoBehaviour
{
	// Token: 0x06000B62 RID: 2914 RVA: 0x00045498 File Offset: 0x00043698
	private void Start()
	{
		this.Playing = true;
	}

	// Token: 0x06000B63 RID: 2915 RVA: 0x00004095 File Offset: 0x00002295
	private void Update()
	{
	}

	// Token: 0x06000B64 RID: 2916 RVA: 0x000454A1 File Offset: 0x000436A1
	public void GameEvent(string message)
	{
		if (message == "endgame")
		{
			Time.timeScale = 0f;
			this.Playing = false;
		}
	}

	// Token: 0x06000B65 RID: 2917 RVA: 0x000454C4 File Offset: 0x000436C4
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

	// Token: 0x04000792 RID: 1938
	public GUISkin Skin;

	// Token: 0x04000793 RID: 1939
	public Texture2D IconZombie;

	// Token: 0x04000794 RID: 1940
	[NonSerialized]
	public int Score;

	// Token: 0x04000795 RID: 1941
	[NonSerialized]
	public bool Playing;
}
