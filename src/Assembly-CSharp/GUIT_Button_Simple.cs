using System;
using UnityEngine;

// Token: 0x02000224 RID: 548
[RequireComponent(typeof(GUITexture))]
public class GUIT_Button_Simple : MonoBehaviour
{
	// Token: 0x06001100 RID: 4352 RVA: 0x0001092B File Offset: 0x0000EB2B
	private void Awake()
	{
		base.GetComponentInChildren<GUIText>().material.color = this.labelColor;
		this.UpdateImage();
	}

	// Token: 0x06001101 RID: 4353 RVA: 0x000AA864 File Offset: 0x000A8A64
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

	// Token: 0x06001102 RID: 4354 RVA: 0x00010949 File Offset: 0x0000EB49
	private void OnClick()
	{
		this.callbackObject.SendMessage(this.callback);
	}

	// Token: 0x06001103 RID: 4355 RVA: 0x0001095C File Offset: 0x0000EB5C
	private void OnOver()
	{
		this.over = true;
		this.UpdateImage();
	}

	// Token: 0x06001104 RID: 4356 RVA: 0x0001096B File Offset: 0x0000EB6B
	private void OnOut()
	{
		this.over = false;
		this.UpdateImage();
	}

	// Token: 0x06001105 RID: 4357 RVA: 0x0001097A File Offset: 0x0000EB7A
	private void UpdateImage()
	{
		if (this.over)
		{
			base.GetComponent<GUITexture>().texture = this.text_over;
			return;
		}
		base.GetComponent<GUITexture>().texture = this.text;
	}

	// Token: 0x04000DAC RID: 3500
	public Color labelColor;

	// Token: 0x04000DAD RID: 3501
	public Texture text;

	// Token: 0x04000DAE RID: 3502
	public Texture text_over;

	// Token: 0x04000DAF RID: 3503
	public GameObject callbackObject;

	// Token: 0x04000DB0 RID: 3504
	public string callback;

	// Token: 0x04000DB1 RID: 3505
	private bool over;
}
