using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
	public GUISkin Skin;

	public Texture2D IconZombie;

	[NonSerialized]
	public int Score;

	[NonSerialized]
	public bool Playing;

	private void Start()
	{
		Playing = true;
	}

	private void Update()
	{
	}

	public void GameEvent(string message)
	{
		if (message == "endgame")
		{
			Time.timeScale = 0f;
			Playing = false;
		}
	}

	private void OnGUI()
	{
		//IL_00cf: Unknown result type (might be due to invalid IL or missing references)
		//IL_0038: Unknown result type (might be due to invalid IL or missing references)
		//IL_0057: Unknown result type (might be due to invalid IL or missing references)
		//IL_0096: Unknown result type (might be due to invalid IL or missing references)
		//IL_011a: Unknown result type (might be due to invalid IL or missing references)
		//IL_013f: Unknown result type (might be due to invalid IL or missing references)
		if ((Object)(object)Skin != (Object)null)
		{
			GUI.skin = Skin;
		}
		if (Playing)
		{
			GUI.DrawTexture(new Rect(40f, 40f, 64f, 64f), (Texture)(object)IconZombie);
			GUI.skin.label.normal.textColor = Color.white;
			GUI.skin.label.fontSize = 35;
			GUI.skin.label.alignment = (TextAnchor)3;
			GUI.Label(new Rect(125f, 40f, 200f, 64f), Score.ToString());
			return;
		}
		Screen.lockCursor = false;
		if (GUI.Button(new Rect((float)(Screen.width / 2 - 80), (float)(Screen.height / 2), 160f, 30f), "Exit"))
		{
			Application.LoadLevel("Mainmenu");
		}
		GUI.skin.label.fontSize = 25;
		GUI.skin.label.alignment = (TextAnchor)4;
		GUI.skin.label.normal.textColor = Color.white;
		GUI.Label(new Rect(0f, (float)(Screen.height / 2 - 100), (float)Screen.width, 30f), "Game Over");
	}
}
