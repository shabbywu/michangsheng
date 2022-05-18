using System;
using System.Security.Cryptography;
using UnityEngine;

// Token: 0x020007B0 RID: 1968
[CreateAssetMenu(menuName = "RandomFaceAsset/Create RandomFaceAsset Instance")]
public class RandomFace : ScriptableObject
{
	// Token: 0x1700045D RID: 1117
	// (get) Token: 0x06003203 RID: 12803 RVA: 0x00024801 File Offset: 0x00022A01
	public RandomFaceInfo[] RandomFaces
	{
		get
		{
			return this.m_RandomFace;
		}
	}

	// Token: 0x06003204 RID: 12804 RVA: 0x00024809 File Offset: 0x00022A09
	public Sprite getPartByID(int PartID)
	{
		return this.m_RandomFace[PartID].m_Icon;
	}

	// Token: 0x06003205 RID: 12805 RVA: 0x000AF784 File Offset: 0x000AD984
	public int getRandom()
	{
		byte[] array = new byte[8];
		new RNGCryptoServiceProvider().GetBytes(array);
		return Math.Abs(BitConverter.ToInt32(array, 0));
	}

	// Token: 0x06003206 RID: 12806 RVA: 0x000042DD File Offset: 0x000024DD
	public static void onEnter()
	{
	}

	// Token: 0x06003207 RID: 12807 RVA: 0x000042DD File Offset: 0x000024DD
	public void cratfToJson()
	{
	}

	// Token: 0x06003208 RID: 12808 RVA: 0x0018D1F4 File Offset: 0x0018B3F4
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

	// Token: 0x04002E2F RID: 11823
	[SerializeField]
	private RandomFaceInfo[] m_RandomFace;

	// Token: 0x04002E30 RID: 11824
	[SerializeField]
	private string path = "";
}
