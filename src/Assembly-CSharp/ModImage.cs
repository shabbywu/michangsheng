using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000304 RID: 772
[RequireComponent(typeof(Image))]
public class ModImage : MonoBehaviour
{
	// Token: 0x0600171D RID: 5917 RVA: 0x00014694 File Offset: 0x00012894
	private void Awake()
	{
		this.image = base.GetComponent<Image>();
	}

	// Token: 0x0600171E RID: 5918 RVA: 0x000146A2 File Offset: 0x000128A2
	public void Refresh()
	{
		this.image.sprite = ModResources.LoadSprite(this.SpritePath);
	}

	// Token: 0x0600171F RID: 5919 RVA: 0x000146BA File Offset: 0x000128BA
	private void Start()
	{
		this.Refresh();
	}

	// Token: 0x04001278 RID: 4728
	private Image image;

	// Token: 0x04001279 RID: 4729
	public string SpritePath;
}
