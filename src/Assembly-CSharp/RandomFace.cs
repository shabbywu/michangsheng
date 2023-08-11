using System;
using System.Security.Cryptography;
using UnityEngine;

[CreateAssetMenu(menuName = "RandomFaceAsset/Create RandomFaceAsset Instance")]
public class RandomFace : ScriptableObject
{
	[SerializeField]
	private RandomFaceInfo[] m_RandomFace;

	[SerializeField]
	private string path = "";

	public RandomFaceInfo[] RandomFaces => m_RandomFace;

	public Sprite getPartByID(int PartID)
	{
		return m_RandomFace[PartID].m_Icon;
	}

	public int getRandom()
	{
		byte[] array = new byte[8];
		new RNGCryptoServiceProvider().GetBytes(array);
		return Math.Abs(BitConverter.ToInt32(array, 0));
	}

	public static void onEnter()
	{
	}

	public void cratfToJson()
	{
	}

	private void OnValidate()
	{
		int num = 0;
		RandomFaceInfo[] randomFace = m_RandomFace;
		foreach (RandomFaceInfo randomFaceInfo in randomFace)
		{
			string text = "";
			if (num < 10)
			{
				text = "0";
			}
			randomFaceInfo.RandomID = num;
			randomFaceInfo.m_Icon = Resources.Load<Sprite>("Effect/AvatarFace/RandomFace/" + path + "/" + path + "_" + text + num);
			if ((Object)(object)randomFaceInfo.m_Icon == (Object)null)
			{
				randomFaceInfo.m_Icon = Resources.Load<Sprite>("Effect/AvatarFace/RandomFace/" + path + "/" + path + "_01");
			}
			num++;
		}
	}
}
