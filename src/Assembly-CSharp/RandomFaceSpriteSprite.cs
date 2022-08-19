using System;
using UnityEngine;

// Token: 0x02000166 RID: 358
public class RandomFaceSpriteSprite : MonoBehaviour
{
	// Token: 0x06000F71 RID: 3953 RVA: 0x00004095 File Offset: 0x00002295
	private void Start()
	{
	}

	// Token: 0x06000F72 RID: 3954 RVA: 0x0005CF70 File Offset: 0x0005B170
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

	// Token: 0x06000F73 RID: 3955 RVA: 0x0005D178 File Offset: 0x0005B378
	public void PartIsNull(UITexture Part)
	{
		if (Part.mainTexture == null)
		{
			Part.gameObject.SetActive(false);
		}
	}

	// Token: 0x06000F74 RID: 3956 RVA: 0x00004095 File Offset: 0x00002295
	private void Update()
	{
	}

	// Token: 0x04000B93 RID: 2963
	public UITexture body;

	// Token: 0x04000B94 RID: 2964
	public UITexture eye;

	// Token: 0x04000B95 RID: 2965
	public UITexture eyebrow;

	// Token: 0x04000B96 RID: 2966
	public UITexture face;

	// Token: 0x04000B97 RID: 2967
	public UITexture Facefold;

	// Token: 0x04000B98 RID: 2968
	public UITexture hair;

	// Token: 0x04000B99 RID: 2969
	public UITexture hair2;

	// Token: 0x04000B9A RID: 2970
	public UITexture mouth;

	// Token: 0x04000B9B RID: 2971
	public UITexture mustache;

	// Token: 0x04000B9C RID: 2972
	public UITexture nose;

	// Token: 0x04000B9D RID: 2973
	public UITexture ornament;
}
