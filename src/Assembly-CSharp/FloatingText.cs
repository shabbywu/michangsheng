using System;
using UnityEngine;

// Token: 0x02000153 RID: 339
public class FloatingText : MonoBehaviour
{
	// Token: 0x06000C3A RID: 3130 RVA: 0x00096A4C File Offset: 0x00094C4C
	private void Start()
	{
		this.timeTemp = Time.time;
		Object.Destroy(base.gameObject, this.LifeTime);
		if (this.Position3D)
		{
			Vector3 vector = Camera.main.WorldToScreenPoint(base.transform.position);
			this.Position = new Vector2(vector.x, (float)Screen.height - vector.y);
		}
	}

	// Token: 0x06000C3B RID: 3131 RVA: 0x00096AB4 File Offset: 0x00094CB4
	private void Update()
	{
		if (this.FadeEnd)
		{
			if (Time.time >= this.timeTemp + this.LifeTime - 1f)
			{
				this.alpha = 1f - (Time.time - (this.timeTemp + this.LifeTime - 1f));
			}
		}
		else
		{
			this.alpha = 1f - 1f / this.LifeTime * (Time.time - this.timeTemp);
		}
		if (this.Position3D)
		{
			Vector3 vector = Camera.main.WorldToScreenPoint(base.transform.position);
			this.Position = new Vector2(vector.x, (float)Screen.height - vector.y);
		}
	}

	// Token: 0x06000C3C RID: 3132 RVA: 0x00096B6C File Offset: 0x00094D6C
	private void OnGUI()
	{
		GUI.color = new Color(GUI.color.r, GUI.color.g, GUI.color.b, this.alpha);
		if (this.CustomSkin)
		{
			GUI.skin = this.CustomSkin;
		}
		Vector2 vector = GUI.skin.label.CalcSize(new GUIContent(this.Text));
		Rect rect = new Rect(this.Position.x - vector.x / 2f, this.Position.y, vector.x, vector.y);
		GUI.skin.label.normal.textColor = this.TextColor;
		GUI.Label(rect, this.Text);
	}

	// Token: 0x04000955 RID: 2389
	public GUISkin CustomSkin;

	// Token: 0x04000956 RID: 2390
	public string Text = "";

	// Token: 0x04000957 RID: 2391
	public float LifeTime = 1f;

	// Token: 0x04000958 RID: 2392
	public bool FadeEnd;

	// Token: 0x04000959 RID: 2393
	public Color TextColor = Color.white;

	// Token: 0x0400095A RID: 2394
	public bool Position3D;

	// Token: 0x0400095B RID: 2395
	public Vector2 Position;

	// Token: 0x0400095C RID: 2396
	private float alpha = 1f;

	// Token: 0x0400095D RID: 2397
	private float timeTemp;
}
