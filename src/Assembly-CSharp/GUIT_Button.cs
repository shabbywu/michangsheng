using System;
using UnityEngine;

// Token: 0x0200014D RID: 333
[RequireComponent(typeof(GUITexture))]
public class GUIT_Button : MonoBehaviour
{
	// Token: 0x06000ED2 RID: 3794 RVA: 0x0005A4D8 File Offset: 0x000586D8
	private void Awake()
	{
		base.GetComponentInChildren<GUIText>().material.color = this.labelColor;
		this.UpdateImage();
	}

	// Token: 0x06000ED3 RID: 3795 RVA: 0x0005A4F8 File Offset: 0x000586F8
	private void Update()
	{
		if (base.GetComponent<GUITexture>().GetScreenRect().Contains(Input.mousePosition))
		{
			if (!this.over)
			{
				this.OnOver();
			}
			if (Input.GetMouseButtonDown(0))
			{
				this.OnClick();
				return;
			}
		}
		else if (this.over)
		{
			this.OnOut();
		}
	}

	// Token: 0x06000ED4 RID: 3796 RVA: 0x0005A54A File Offset: 0x0005874A
	private void OnClick()
	{
		this.on = !this.on;
		this.callbackObject.SendMessage(this.callback);
		this.UpdateImage();
	}

	// Token: 0x06000ED5 RID: 3797 RVA: 0x0005A572 File Offset: 0x00058772
	private void OnOver()
	{
		this.over = true;
		this.UpdateImage();
	}

	// Token: 0x06000ED6 RID: 3798 RVA: 0x0005A581 File Offset: 0x00058781
	private void OnOut()
	{
		this.over = false;
		this.UpdateImage();
	}

	// Token: 0x06000ED7 RID: 3799 RVA: 0x0005A590 File Offset: 0x00058790
	private void UpdateImage()
	{
		if (this.over)
		{
			base.GetComponent<GUITexture>().texture = (this.on ? this.t_on_over : this.t_off_over);
			return;
		}
		base.GetComponent<GUITexture>().texture = (this.on ? this.t_on : this.t_off);
	}

	// Token: 0x06000ED8 RID: 3800 RVA: 0x0005A5E8 File Offset: 0x000587E8
	public void UpdateState(bool b)
	{
		this.on = b;
		this.UpdateImage();
	}

	// Token: 0x04000B05 RID: 2821
	public Color labelColor;

	// Token: 0x04000B06 RID: 2822
	public Texture t_on;

	// Token: 0x04000B07 RID: 2823
	public Texture t_off;

	// Token: 0x04000B08 RID: 2824
	public Texture t_on_over;

	// Token: 0x04000B09 RID: 2825
	public Texture t_off_over;

	// Token: 0x04000B0A RID: 2826
	public GameObject callbackObject;

	// Token: 0x04000B0B RID: 2827
	public string callback;

	// Token: 0x04000B0C RID: 2828
	private bool over;

	// Token: 0x04000B0D RID: 2829
	public bool on;
}
