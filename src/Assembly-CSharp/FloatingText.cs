using UnityEngine;

public class FloatingText : MonoBehaviour
{
	public GUISkin CustomSkin;

	public string Text = "";

	public float LifeTime = 1f;

	public bool FadeEnd;

	public Color TextColor = Color.white;

	public bool Position3D;

	public Vector2 Position;

	private float alpha = 1f;

	private float timeTemp;

	private void Start()
	{
		//IL_002f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0034: Unknown result type (might be due to invalid IL or missing references)
		//IL_0039: Unknown result type (might be due to invalid IL or missing references)
		//IL_003b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0047: Unknown result type (might be due to invalid IL or missing references)
		//IL_004e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0053: Unknown result type (might be due to invalid IL or missing references)
		timeTemp = Time.time;
		Object.Destroy((Object)(object)((Component)this).gameObject, LifeTime);
		if (Position3D)
		{
			Vector3 val = Camera.main.WorldToScreenPoint(((Component)this).transform.position);
			Position = new Vector2(val.x, (float)Screen.height - val.y);
		}
	}

	private void Update()
	{
		//IL_0081: Unknown result type (might be due to invalid IL or missing references)
		//IL_0086: Unknown result type (might be due to invalid IL or missing references)
		//IL_008b: Unknown result type (might be due to invalid IL or missing references)
		//IL_008d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0099: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a5: Unknown result type (might be due to invalid IL or missing references)
		if (FadeEnd)
		{
			if (Time.time >= timeTemp + LifeTime - 1f)
			{
				alpha = 1f - (Time.time - (timeTemp + LifeTime - 1f));
			}
		}
		else
		{
			alpha = 1f - 1f / LifeTime * (Time.time - timeTemp);
		}
		if (Position3D)
		{
			Vector3 val = Camera.main.WorldToScreenPoint(((Component)this).transform.position);
			Position = new Vector2(val.x, (float)Screen.height - val.y);
		}
	}

	private void OnGUI()
	{
		//IL_0000: Unknown result type (might be due to invalid IL or missing references)
		//IL_000a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0014: Unknown result type (might be due to invalid IL or missing references)
		//IL_0024: Unknown result type (might be due to invalid IL or missing references)
		//IL_0056: Unknown result type (might be due to invalid IL or missing references)
		//IL_0060: Expected O, but got Unknown
		//IL_005b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0060: Unknown result type (might be due to invalid IL or missing references)
		//IL_006c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0084: Unknown result type (might be due to invalid IL or missing references)
		//IL_008a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0090: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a5: Unknown result type (might be due to invalid IL or missing references)
		GUI.color = new Color(GUI.color.r, GUI.color.g, GUI.color.b, alpha);
		if (Object.op_Implicit((Object)(object)CustomSkin))
		{
			GUI.skin = CustomSkin;
		}
		Vector2 val = GUI.skin.label.CalcSize(new GUIContent(Text));
		Rect val2 = new Rect(Position.x - val.x / 2f, Position.y, val.x, val.y);
		GUI.skin.label.normal.textColor = TextColor;
		GUI.Label(val2, Text);
	}
}
