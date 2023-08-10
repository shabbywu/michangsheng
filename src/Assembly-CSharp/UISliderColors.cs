using UnityEngine;

[RequireComponent(typeof(UIProgressBar))]
[AddComponentMenu("NGUI/Examples/Slider Colors")]
public class UISliderColors : MonoBehaviour
{
	public UISprite sprite;

	public Color[] colors = (Color[])(object)new Color[3]
	{
		Color.red,
		Color.yellow,
		Color.green
	};

	private UIProgressBar mBar;

	private void Start()
	{
		mBar = ((Component)this).GetComponent<UIProgressBar>();
		Update();
	}

	private void Update()
	{
		//IL_0040: Unknown result type (might be due to invalid IL or missing references)
		//IL_0045: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0063: Unknown result type (might be due to invalid IL or missing references)
		//IL_0071: Unknown result type (might be due to invalid IL or missing references)
		//IL_0077: Unknown result type (might be due to invalid IL or missing references)
		//IL_007c: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_0091: Unknown result type (might be due to invalid IL or missing references)
		//IL_0096: Unknown result type (might be due to invalid IL or missing references)
		if ((Object)(object)sprite == (Object)null || colors.Length == 0)
		{
			return;
		}
		float value = mBar.value;
		value *= (float)(colors.Length - 1);
		int num = Mathf.FloorToInt(value);
		Color color = colors[0];
		if (num >= 0)
		{
			if (num + 1 >= colors.Length)
			{
				color = ((num >= colors.Length) ? colors[colors.Length - 1] : colors[num]);
			}
			else
			{
				float num2 = value - (float)num;
				color = Color.Lerp(colors[num], colors[num + 1], num2);
			}
		}
		color.a = sprite.color.a;
		sprite.color = color;
	}
}
