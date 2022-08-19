using System;
using System.Security.Cryptography;
using UnityEngine;

// Token: 0x0200051C RID: 1308
[CreateAssetMenu(menuName = "RandomFaceAsset/Create RandomFaceAsset Instance")]
public class RandomFace : ScriptableObject
{
	// Token: 0x170002BE RID: 702
	// (get) Token: 0x060029F0 RID: 10736 RVA: 0x0013FFA2 File Offset: 0x0013E1A2
	public RandomFaceInfo[] RandomFaces
	{
		get
		{
			return this.m_RandomFace;
		}
	}

	// Token: 0x060029F1 RID: 10737 RVA: 0x0013FFAA File Offset: 0x0013E1AA
	public Sprite getPartByID(int PartID)
	{
		return this.m_RandomFace[PartID].m_Icon;
	}

	// Token: 0x060029F2 RID: 10738 RVA: 0x0013FFBC File Offset: 0x0013E1BC
	public int getRandom()
	{
		byte[] array = new byte[8];
		new RNGCryptoServiceProvider().GetBytes(array);
		return Math.Abs(BitConverter.ToInt32(array, 0));
	}

	// Token: 0x060029F3 RID: 10739 RVA: 0x00004095 File Offset: 0x00002295
	public static void onEnter()
	{
	}

	// Token: 0x060029F4 RID: 10740 RVA: 0x00004095 File Offset: 0x00002295
	public void cratfToJson()
	{
	}

	// Token: 0x060029F5 RID: 10741 RVA: 0x0013FFE8 File Offset: 0x0013E1E8
	private void OnValidate()
	{
		int num = 0;
		foreach (RandomFaceInfo randomFaceInfo in this.m_RandomFace)
		{
			string text = "";
			if (num < 10)
			{
				text = "0";
			}
			randomFaceInfo.RandomID = num;
			randomFaceInfo.m_Icon = Resources.Load<Sprite>(string.Concat(new object[]
			{
				"Effect/AvatarFace/RandomFace/",
				this.path,
				"/",
				this.path,
				"_",
				text,
				num
			}));
			if (randomFaceInfo.m_Icon == null)
			{
				randomFaceInfo.m_Icon = Resources.Load<Sprite>(string.Concat(new string[]
				{
					"Effect/AvatarFace/RandomFace/",
					this.path,
					"/",
					this.path,
					"_01"
				}));
			}
			num++;
		}
	}

	// Token: 0x0400263F RID: 9791
	[SerializeField]
	private RandomFaceInfo[] m_RandomFace;

	// Token: 0x04002640 RID: 9792
	[SerializeField]
	private string path = "";
}
