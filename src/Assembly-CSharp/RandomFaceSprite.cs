using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000165 RID: 357
public class RandomFaceSprite : MonoBehaviour
{
	// Token: 0x06000F6C RID: 3948 RVA: 0x00004095 File Offset: 0x00002295
	private void Start()
	{
	}

	// Token: 0x06000F6D RID: 3949 RVA: 0x0005CCCC File Offset: 0x0005AECC
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

	// Token: 0x06000F6E RID: 3950 RVA: 0x0005CF51 File Offset: 0x0005B151
	public void PartIsNull(Image Part)
	{
		if (Part.sprite == null)
		{
			Part.gameObject.SetActive(false);
		}
	}

	// Token: 0x06000F6F RID: 3951 RVA: 0x00004095 File Offset: 0x00002295
	private void Update()
	{
	}

	// Token: 0x04000B88 RID: 2952
	public Image body;

	// Token: 0x04000B89 RID: 2953
	public Image eye;

	// Token: 0x04000B8A RID: 2954
	public Image eyebrow;

	// Token: 0x04000B8B RID: 2955
	public Image face;

	// Token: 0x04000B8C RID: 2956
	public Image Facefold;

	// Token: 0x04000B8D RID: 2957
	public Image hair;

	// Token: 0x04000B8E RID: 2958
	public Image hair2;

	// Token: 0x04000B8F RID: 2959
	public Image mouth;

	// Token: 0x04000B90 RID: 2960
	public Image mustache;

	// Token: 0x04000B91 RID: 2961
	public Image nose;

	// Token: 0x04000B92 RID: 2962
	public Image ornament;
}
