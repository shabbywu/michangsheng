using System;
using UnityEngine;

// Token: 0x0200014E RID: 334
[RequireComponent(typeof(GUITexture))]
public class GUIT_Button_Simple : MonoBehaviour
{
	// Token: 0x06000EDA RID: 3802 RVA: 0x0005A5F7 File Offset: 0x000587F7
	private void Awake()
	{
		base.GetComponentInChildren<GUIText>().material.color = this.labelColor;
		this.UpdateImage();
	}

	// Token: 0x06000EDB RID: 3803 RVA: 0x0005A618 File Offset: 0x00058818
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

	// Token: 0x06000EDC RID: 3804 RVA: 0x0005A66A File Offset: 0x0005886A
	private void OnClick()
	{
		this.callbackObject.SendMessage(this.callback);
	}

	// Token: 0x06000EDD RID: 3805 RVA: 0x0005A67D File Offset: 0x0005887D
	private void OnOver()
	{
		this.over = true;
		this.UpdateImage();
	}

	// Token: 0x06000EDE RID: 3806 RVA: 0x0005A68C File Offset: 0x0005888C
	private void OnOut()
	{
		this.over = false;
		this.UpdateImage();
	}

	// Token: 0x06000EDF RID: 3807 RVA: 0x0005A69B File Offset: 0x0005889B
	private void UpdateImage()
	{
		if (this.over)
		{
			base.GetComponent<GUITexture>().texture = this.text_over;
			return;
		}
		base.GetComponent<GUITexture>().texture = this.text;
	}

	// Token: 0x04000B0E RID: 2830
	public Color labelColor;

	// Token: 0x04000B0F RID: 2831
	public Texture text;

	// Token: 0x04000B10 RID: 2832
	public Texture text_over;

	// Token: 0x04000B11 RID: 2833
	public GameObject callbackObject;

	// Token: 0x04000B12 RID: 2834
	public string callback;

	// Token: 0x04000B13 RID: 2835
	private bool over;
}
