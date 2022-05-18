using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000241 RID: 577
public class RandomFaceSprite : MonoBehaviour
{
	// Token: 0x060011CA RID: 4554 RVA: 0x000042DD File Offset: 0x000024DD
	private void Start()
	{
	}

	// Token: 0x060011CB RID: 4555 RVA: 0x000AC8F8 File Offset: 0x000AAAF8
	public void setFace(int avatarID)
	{
		JSONObject jsonobject = jsonData.instance.AvatarRandomJsonData[string.Concat(avatarID)];
		this.body.sprite = jsonData.instance.body.getPartByID((int)jsonobject["body"].n);
		this.PartIsNull(this.body);
		this.eye.sprite = jsonData.instance.eye.getPartByID((int)jsonobject["eye"].n);
		this.PartIsNull(this.eye);
		this.face.sprite = jsonData.instance.face.getPartByID((int)jsonobject["face"].n);
		this.PartIsNull(this.face);
		this.Facefold.sprite = jsonData.instance.Facefold.getPartByID((int)jsonobject["Facefold"].n);
		this.PartIsNull(this.Facefold);
		this.hair.sprite = jsonData.instance.hair.getPartByID((int)jsonobject["hair"].n);
		this.PartIsNull(this.hair);
		this.mouth.sprite = jsonData.instance.mouth.getPartByID((int)jsonobject["mouth"].n);
		this.PartIsNull(this.mouth);
		this.eyebrow.sprite = jsonData.instance.eyebrow.getPartByID((int)jsonobject["eyebrow"].n);
		this.PartIsNull(this.eyebrow);
		this.nose.sprite = jsonData.instance.nose.getPartByID((int)jsonobject["nose"].n);
		this.PartIsNull(this.nose);
		this.ornament.sprite = jsonData.instance.ornament.getPartByID((int)jsonobject["ornament"].n);
		this.PartIsNull(this.ornament);
		this.eyebrow.sprite = jsonData.instance.eyebrow.getPartByID((int)jsonobject["eyebrow"].n);
		this.PartIsNull(this.eyebrow);
		this.hair2.sprite = jsonData.instance.hair2.getPartByID((int)jsonobject["hair"].n);
		this.PartIsNull(this.hair2);
	}

	// Token: 0x060011CC RID: 4556 RVA: 0x00011253 File Offset: 0x0000F453
	public void PartIsNull(Image Part)
	{
		if (Part.sprite == null)
		{
			Part.gameObject.SetActive(false);
		}
	}

	// Token: 0x060011CD RID: 4557 RVA: 0x000042DD File Offset: 0x000024DD
	private void Update()
	{
	}

	// Token: 0x04000E58 RID: 3672
	public Image body;

	// Token: 0x04000E59 RID: 3673
	public Image eye;

	// Token: 0x04000E5A RID: 3674
	public Image eyebrow;

	// Token: 0x04000E5B RID: 3675
	public Image face;

	// Token: 0x04000E5C RID: 3676
	public Image Facefold;

	// Token: 0x04000E5D RID: 3677
	public Image hair;

	// Token: 0x04000E5E RID: 3678
	public Image hair2;

	// Token: 0x04000E5F RID: 3679
	public Image mouth;

	// Token: 0x04000E60 RID: 3680
	public Image mustache;

	// Token: 0x04000E61 RID: 3681
	public Image nose;

	// Token: 0x04000E62 RID: 3682
	public Image ornament;
}
