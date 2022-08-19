using System;
using UnityEngine;

// Token: 0x02000050 RID: 80
[RequireComponent(typeof(UIProgressBar))]
[AddComponentMenu("NGUI/Examples/Slider Colors")]
public class UISliderColors : MonoBehaviour
{
	// Token: 0x06000478 RID: 1144 RVA: 0x00018C9F File Offset: 0x00016E9F
	private void Start()
	{
		this.mBar = base.GetComponent<UIProgressBar>();
		this.Update();
	}

	// Token: 0x06000479 RID: 1145 RVA: 0x00018CB4 File Offset: 0x00016EB4
	private void Update()
	{
		if (this.sprite == null || this.colors.Length == 0)
		{
			return;
		}
		float num = this.mBar.value;
		num *= (float)(this.colors.Length - 1);
		int num2 = Mathf.FloorToInt(num);
		Color color = this.colors[0];
		if (num2 >= 0)
		{
			if (num2 + 1 < this.colors.Length)
			{
				float num3 = num - (float)num2;
				color = Color.Lerp(this.colors[num2], this.colors[num2 + 1], num3);
			}
			else if (num2 < this.colors.Length)
			{
				color = this.colors[num2];
			}
			else
			{
				color = this.colors[this.colors.Length - 1];
			}
		}
		color.a = this.sprite.color.a;
		this.sprite.color = color;
	}

	// Token: 0x040002A9 RID: 681
	public UISprite sprite;

	// Token: 0x040002AA RID: 682
	public Color[] colors = new Color[]
	{
		Color.red,
		Color.yellow,
		Color.green
	};

	// Token: 0x040002AB RID: 683
	private UIProgressBar mBar;
}
