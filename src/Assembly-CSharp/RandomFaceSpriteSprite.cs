using System;
using UnityEngine;

// Token: 0x02000242 RID: 578
public class RandomFaceSpriteSprite : MonoBehaviour
{
	// Token: 0x060011CF RID: 4559 RVA: 0x000042DD File Offset: 0x000024DD
	private void Start()
	{
	}

	// Token: 0x060011D0 RID: 4560 RVA: 0x000ACB80 File Offset: 0x000AAD80
	public void setFace(int avatarID)
	{
		JSONObject jsonobject = jsonData.instance.AvatarRandomJsonData[string.Concat(avatarID)];
		this.body.mainTexture = jsonData.instance.body.getPartByID((int)jsonobject["body"].n).texture;
		this.PartIsNull(this.body);
		this.eye.mainTexture = jsonData.instance.eye.getPartByID((int)jsonobject["eye"].n).texture;
		this.PartIsNull(this.eye);
		this.hair.mainTexture = jsonData.instance.hair.getPartByID((int)jsonobject["hair"].n).texture;
		this.PartIsNull(this.hair);
		this.mouth.mainTexture = jsonData.instance.mouth.getPartByID((int)jsonobject["mouth"].n).texture;
		this.PartIsNull(this.mouth);
		this.eyebrow.mainTexture = jsonData.instance.eyebrow.getPartByID((int)jsonobject["eyebrow"].n).texture;
		this.PartIsNull(this.eyebrow);
		this.nose.mainTexture = jsonData.instance.nose.getPartByID((int)jsonobject["nose"].n).texture;
		this.PartIsNull(this.nose);
		this.eyebrow.mainTexture = jsonData.instance.eyebrow.getPartByID((int)jsonobject["eyebrow"].n).texture;
		this.PartIsNull(this.eyebrow);
		this.hair2.mainTexture = jsonData.instance.hair2.getPartByID((int)jsonobject["hair"].n).texture;
		this.PartIsNull(this.hair2);
	}

	// Token: 0x060011D1 RID: 4561 RVA: 0x0001126F File Offset: 0x0000F46F
	public void PartIsNull(UITexture Part)
	{
		if (Part.mainTexture == null)
		{
			Part.gameObject.SetActive(false);
		}
	}

	// Token: 0x060011D2 RID: 4562 RVA: 0x000042DD File Offset: 0x000024DD
	private void Update()
	{
	}

	// Token: 0x04000E63 RID: 3683
	public UITexture body;

	// Token: 0x04000E64 RID: 3684
	public UITexture eye;

	// Token: 0x04000E65 RID: 3685
	public UITexture eyebrow;

	// Token: 0x04000E66 RID: 3686
	public UITexture face;

	// Token: 0x04000E67 RID: 3687
	public UITexture Facefold;

	// Token: 0x04000E68 RID: 3688
	public UITexture hair;

	// Token: 0x04000E69 RID: 3689
	public UITexture hair2;

	// Token: 0x04000E6A RID: 3690
	public UITexture mouth;

	// Token: 0x04000E6B RID: 3691
	public UITexture mustache;

	// Token: 0x04000E6C RID: 3692
	public UITexture nose;

	// Token: 0x04000E6D RID: 3693
	public UITexture ornament;
}
