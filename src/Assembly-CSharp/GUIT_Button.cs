using System;
using UnityEngine;

// Token: 0x02000223 RID: 547
[RequireComponent(typeof(GUITexture))]
public class GUIT_Button : MonoBehaviour
{
	// Token: 0x060010F8 RID: 4344 RVA: 0x000108B8 File Offset: 0x0000EAB8
	private void Awake()
	{
		base.GetComponentInChildren<GUIText>().material.color = this.labelColor;
		this.UpdateImage();
	}

	// Token: 0x060010F9 RID: 4345 RVA: 0x000AA7B8 File Offset: 0x000A89B8
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

	// Token: 0x060010FA RID: 4346 RVA: 0x000108D6 File Offset: 0x0000EAD6
	private void OnClick()
	{
		this.on = !this.on;
		this.callbackObject.SendMessage(this.callback);
		this.UpdateImage();
	}

	// Token: 0x060010FB RID: 4347 RVA: 0x000108FE File Offset: 0x0000EAFE
	private void OnOver()
	{
		this.over = true;
		this.UpdateImage();
	}

	// Token: 0x060010FC RID: 4348 RVA: 0x0001090D File Offset: 0x0000EB0D
	private void OnOut()
	{
		this.over = false;
		this.UpdateImage();
	}

	// Token: 0x060010FD RID: 4349 RVA: 0x000AA80C File Offset: 0x000A8A0C
	private void UpdateImage()
	{
		if (this.over)
		{
			base.GetComponent<GUITexture>().texture = (this.on ? this.t_on_over : this.t_off_over);
			return;
		}
		base.GetComponent<GUITexture>().texture = (this.on ? this.t_on : this.t_off);
	}

	// Token: 0x060010FE RID: 4350 RVA: 0x0001091C File Offset: 0x0000EB1C
	public void UpdateState(bool b)
	{
		this.on = b;
		this.UpdateImage();
	}

	// Token: 0x04000DA3 RID: 3491
	public Color labelColor;

	// Token: 0x04000DA4 RID: 3492
	public Texture t_on;

	// Token: 0x04000DA5 RID: 3493
	public Texture t_off;

	// Token: 0x04000DA6 RID: 3494
	public Texture t_on_over;

	// Token: 0x04000DA7 RID: 3495
	public Texture t_off_over;

	// Token: 0x04000DA8 RID: 3496
	public GameObject callbackObject;

	// Token: 0x04000DA9 RID: 3497
	public string callback;

	// Token: 0x04000DAA RID: 3498
	private bool over;

	// Token: 0x04000DAB RID: 3499
	public bool on;
}
