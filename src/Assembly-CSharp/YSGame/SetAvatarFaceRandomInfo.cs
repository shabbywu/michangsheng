using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

namespace YSGame;

public class SetAvatarFaceRandomInfo : MonoBehaviour
{
	public enum InfoName
	{
		[Description("头发")]
		hair,
		[Description("嘴巴")]
		mouth,
		[Description("鼻子")]
		nose,
		[Description("眼睛")]
		eyes,
		[Description("眉毛")]
		eyebrow,
		[Description("头部")]
		head,
		[Description("服饰")]
		a_suit,
		[Description("上胡")]
		a_hair,
		[Description("下胡")]
		b_hair,
		[Description("面部特征")]
		characteristic,
		[Description("发色")]
		hairColorR,
		[Description("眼珠颜色")]
		yanzhuColor,
		[Description("特征颜色")]
		tezhengColor,
		[Description("眉毛颜色")]
		eyebrowColor,
		[Description("女修特征")]
		feature,
		[Description("女修特征")]
		yanying
	}

	public List<AvatarFaceInfo> RandomInfo = new List<AvatarFaceInfo>();

	public List<StaticFaceInfo> StaticRandomInfo = new List<StaticFaceInfo>();

	public static SetAvatarFaceRandomInfo inst;

	private void Awake()
	{
		inst = this;
		UpdateNPCDate();
		Object.DontDestroyOnLoad((Object)(object)this);
	}

	public List<AvatarFaceInfo> findType(InfoName type)
	{
		List<AvatarFaceInfo> list = new List<AvatarFaceInfo>();
		foreach (AvatarFaceInfo item in RandomInfo)
		{
			if (item.SkinTypeName == type)
			{
				list.Add(item);
			}
		}
		return list;
	}

	public int findStatic(int avatarID, InfoName type)
	{
		foreach (StaticFaceInfo item in StaticRandomInfo)
		{
			if (item.AvatarScope != avatarID)
			{
				continue;
			}
			foreach (StaticFaceRandomInfo faceinfo in item.faceinfoList)
			{
				if (faceinfo.SkinTypeName == type)
				{
					return faceinfo.SkinTypeScope;
				}
			}
		}
		return -100;
	}

	public void UpdateNPCDate()
	{
		StaticRandomInfo = (List<StaticFaceInfo>)ResManager.inst.LoadObject("Data/StaticRandomInfo.bin");
	}

	public AvatarFaceInfo FindAvatarType(int AvatarID, InfoName type)
	{
		//IL_0027: Unknown result type (might be due to invalid IL or missing references)
		//IL_002c: Unknown result type (might be due to invalid IL or missing references)
		//IL_002e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0035: Unknown result type (might be due to invalid IL or missing references)
		foreach (AvatarFaceInfo item in findType(type))
		{
			foreach (Vector2 item2 in item.AvatarScope)
			{
				if (NumIn((int)item2.x, (int)item2.y, AvatarID))
				{
					return item;
				}
			}
		}
		return null;
	}

	public bool NumIn(int min, int max, int num)
	{
		if (num > min && num < max)
		{
			return true;
		}
		return false;
	}

	public StaticFaceInfo getStaticFace(int AvatarID)
	{
		foreach (StaticFaceInfo item in StaticRandomInfo)
		{
			if (item.AvatarScope == AvatarID)
			{
				return item;
			}
		}
		return null;
	}

	public int getFace(int AvatarID, InfoName type)
	{
		//IL_0035: Unknown result type (might be due to invalid IL or missing references)
		//IL_003a: Unknown result type (might be due to invalid IL or missing references)
		//IL_003c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0058: Unknown result type (might be due to invalid IL or missing references)
		AvatarFaceInfo avatarFaceInfo = FindAvatarType(AvatarID, type);
		int num = findStatic(AvatarID, type);
		List<int> list = new List<int>();
		if (num != -100)
		{
			return num;
		}
		if (avatarFaceInfo == null)
		{
			return -100;
		}
		foreach (Vector2 item in avatarFaceInfo.SkinTypeScope)
		{
			for (int i = (int)item.x; i <= (int)item.y; i++)
			{
				list.Add(i);
			}
		}
		if (list.Count == 0)
		{
			return -100;
		}
		return list[jsonData.instance.QuikeGetRandom() % list.Count];
	}
}
