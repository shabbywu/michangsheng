using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020001F0 RID: 496
[RequireComponent(typeof(Image))]
public class ModImage : MonoBehaviour
{
	// Token: 0x06001473 RID: 5235 RVA: 0x00083559 File Offset: 0x00081759
	private void Awake()
	{
		this.image = base.GetComponent<Image>();
	}

	// Token: 0x06001474 RID: 5236 RVA: 0x00083567 File Offset: 0x00081767
	public void Refresh()
	{
		this.image.sprite = ModResources.LoadSprite(this.SpritePath);
	}

	// Token: 0x06001475 RID: 5237 RVA: 0x0008357F File Offset: 0x0008177F
	private void Start()
	{
		this.Refresh();
	}

	// Token: 0x04000F36 RID: 3894
	private Image image;

	// Token: 0x04000F37 RID: 3895
	public string SpritePath;
}
