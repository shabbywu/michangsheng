using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020003EE RID: 1006
public class createImageCell : MonoBehaviour
{
	// Token: 0x0600207E RID: 8318 RVA: 0x00004095 File Offset: 0x00002295
	private void Start()
	{
	}

	// Token: 0x0600207F RID: 8319 RVA: 0x000E4E28 File Offset: 0x000E3028
	public void resetAvatarFace()
	{
		CreateAvatarMag component = GameObject.Find("CreatAvatar").GetComponent<CreateAvatarMag>();
		List<int> suijiList = jsonData.instance.getSuijiList(this.SkinType, "AvatarSex" + CreateAvatarMag.inst.faceUI.faceDatabase.ListType);
		jsonData.instance.AvatarRandomJsonData["1"].SetField(this.SkinType, suijiList[this.SkinIndex]);
		component.player.randomAvatar(1);
	}

	// Token: 0x06002080 RID: 8320 RVA: 0x000E4EB0 File Offset: 0x000E30B0
	public void SetImage(Sprite sprite1 = null, Sprite sprite2 = null, Sprite sprite3 = null)
	{
		if (sprite1 == null)
		{
			this.image1.enabled = false;
		}
		else
		{
			this.image1.sprite = sprite1;
		}
		if (sprite2 == null)
		{
			this.image2.enabled = false;
		}
		else
		{
			this.image2.sprite = sprite2;
		}
		if (sprite3 == null)
		{
			this.image3.enabled = false;
			return;
		}
		this.image3.sprite = sprite3;
	}

	// Token: 0x06002081 RID: 8321 RVA: 0x000E4F25 File Offset: 0x000E3125
	public void SetColor(Color color)
	{
		this.image1.color = color;
	}

	// Token: 0x06002082 RID: 8322 RVA: 0x000E4F33 File Offset: 0x000E3133
	public Color GetColor()
	{
		return this.image1.color;
	}

	// Token: 0x04001A6A RID: 6762
	public int SkinIndex;

	// Token: 0x04001A6B RID: 6763
	public string SkinType;

	// Token: 0x04001A6C RID: 6764
	[SerializeField]
	private Image image1;

	// Token: 0x04001A6D RID: 6765
	[SerializeField]
	private Image image2;

	// Token: 0x04001A6E RID: 6766
	[SerializeField]
	private Image image3;

	// Token: 0x04001A6F RID: 6767
	public Toggle toggle;
}
