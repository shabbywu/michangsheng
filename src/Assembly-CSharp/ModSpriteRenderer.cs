using System;
using UnityEngine;

// Token: 0x02000306 RID: 774
[RequireComponent(typeof(SpriteRenderer))]
public class ModSpriteRenderer : MonoBehaviour
{
	// Token: 0x0600172D RID: 5933 RVA: 0x00014786 File Offset: 0x00012986
	private void Awake()
	{
		this.sr = base.GetComponent<SpriteRenderer>();
	}

	// Token: 0x0600172E RID: 5934 RVA: 0x00014794 File Offset: 0x00012994
	public void Refresh()
	{
		this.sr.sprite = ModResources.LoadSprite(this.SpritePath);
	}

	// Token: 0x0600172F RID: 5935 RVA: 0x000147AC File Offset: 0x000129AC
	private void Start()
	{
		this.Refresh();
	}

	// Token: 0x04001280 RID: 4736
	private SpriteRenderer sr;

	// Token: 0x04001281 RID: 4737
	public string SpritePath;
}
