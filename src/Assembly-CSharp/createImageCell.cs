using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x0200058F RID: 1423
public class createImageCell : MonoBehaviour
{
	// Token: 0x06002411 RID: 9233 RVA: 0x000042DD File Offset: 0x000024DD
	private void Start()
	{
	}

	// Token: 0x06002412 RID: 9234 RVA: 0x00127134 File Offset: 0x00125334
	public void resetAvatarFace()
	{
		CreateAvatarMag component = GameObject.Find("CreatAvatar").GetComponent<CreateAvatarMag>();
		List<int> suijiList = jsonData.instance.getSuijiList(this.SkinType, "AvatarSex" + CreateAvatarMag.inst.faceUI.faceDatabase.ListType);
		jsonData.instance.AvatarRandomJsonData["1"].SetField(this.SkinType, suijiList[this.SkinIndex]);
		component.player.randomAvatar(1);
	}

	// Token: 0x06002413 RID: 9235 RVA: 0x001271BC File Offset: 0x001253BC
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

	// Token: 0x06002414 RID: 9236 RVA: 0x0001D1B1 File Offset: 0x0001B3B1
	public void SetColor(Color color)
	{
		this.image1.color = color;
	}

	// Token: 0x06002415 RID: 9237 RVA: 0x0001D1BF File Offset: 0x0001B3BF
	public Color GetColor()
	{
		return this.image1.color;
	}

	// Token: 0x04001F12 RID: 7954
	public int SkinIndex;

	// Token: 0x04001F13 RID: 7955
	public string SkinType;

	// Token: 0x04001F14 RID: 7956
	[SerializeField]
	private Image image1;

	// Token: 0x04001F15 RID: 7957
	[SerializeField]
	private Image image2;

	// Token: 0x04001F16 RID: 7958
	[SerializeField]
	private Image image3;

	// Token: 0x04001F17 RID: 7959
	public Toggle toggle;
}
