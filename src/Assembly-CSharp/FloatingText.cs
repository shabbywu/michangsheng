using System;
using UnityEngine;

// Token: 0x020000DC RID: 220
public class FloatingText : MonoBehaviour
{
	// Token: 0x06000B4B RID: 2891 RVA: 0x00044DA0 File Offset: 0x00042FA0
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

	// Token: 0x06000B4C RID: 2892 RVA: 0x00044E08 File Offset: 0x00043008
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

	// Token: 0x06000B4D RID: 2893 RVA: 0x00044EC0 File Offset: 0x000430C0
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

	// Token: 0x0400077A RID: 1914
	public GUISkin CustomSkin;

	// Token: 0x0400077B RID: 1915
	public string Text = "";

	// Token: 0x0400077C RID: 1916
	public float LifeTime = 1f;

	// Token: 0x0400077D RID: 1917
	public bool FadeEnd;

	// Token: 0x0400077E RID: 1918
	public Color TextColor = Color.white;

	// Token: 0x0400077F RID: 1919
	public bool Position3D;

	// Token: 0x04000780 RID: 1920
	public Vector2 Position;

	// Token: 0x04000781 RID: 1921
	private float alpha = 1f;

	// Token: 0x04000782 RID: 1922
	private float timeTemp;
}
