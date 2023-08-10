using UnityEngine;

public class Mainmenu : MonoBehaviour
{
	public Texture2D LogoGame;

	public GUISkin skin;

	private void Start()
	{
	}

	private void OnGUI()
	{
		//IL_0071: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_011c: Unknown result type (might be due to invalid IL or missing references)
		//IL_013f: Unknown result type (might be due to invalid IL or missing references)
		Screen.lockCursor = false;
		if (Object.op_Implicit((Object)(object)skin))
		{
			GUI.skin = skin;
		}
		GUI.DrawTexture(new Rect((float)(Screen.width / 2) - (float)((Texture)LogoGame).width * 0.5f / 2f, (float)(Screen.height / 2 - 200), (float)((Texture)LogoGame).width * 0.5f, (float)((Texture)LogoGame).height * 0.5f), (Texture)(object)LogoGame);
		if (GUI.Button(new Rect((float)(Screen.width / 2 - 80), (float)(Screen.height / 2 + 20), 160f, 30f), "Start Demo"))
		{
			Application.LoadLevel("Demo");
		}
		GUI.Button(new Rect((float)(Screen.width / 2 - 80), (float)(Screen.height / 2 + 60), 160f, 30f), "Purchase");
		GUI.skin.label.fontSize = 12;
		GUI.skin.label.alignment = (TextAnchor)4;
		GUI.skin.label.normal.textColor = Color.white;
		GUI.Label(new Rect(0f, (float)(Screen.height - 50), (float)Screen.width, 30f), "Dungeon Breaker Starter Kit beta.  By Rachan Neamprasert | www.hardworkerstudio.com");
	}

	private void Update()
	{
	}
}
