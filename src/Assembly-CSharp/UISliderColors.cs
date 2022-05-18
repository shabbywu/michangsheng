using System;
using UnityEngine;

// Token: 0x02000069 RID: 105
[RequireComponent(typeof(UIProgressBar))]
[AddComponentMenu("NGUI/Examples/Slider Colors")]
public class UISliderColors : MonoBehaviour
{
	// Token: 0x060004C6 RID: 1222 RVA: 0x000082D4 File Offset: 0x000064D4
	private void Start()
	{
		this.mBar = base.GetComponent<UIProgressBar>();
		this.Update();
	}

	// Token: 0x060004C7 RID: 1223 RVA: 0x0006FF40 File Offset: 0x0006E140
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

	// Token: 0x0400031C RID: 796
	public UISprite sprite;

	// Token: 0x0400031D RID: 797
	public Color[] colors = new Color[]
	{
		Color.red,
		Color.yellow,
		Color.green
	};

	// Token: 0x0400031E RID: 798
	private UIProgressBar mBar;
}
