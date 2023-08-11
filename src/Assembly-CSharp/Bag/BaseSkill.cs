using System;
using System.Collections.Generic;
using JSONClass;
using UnityEngine;

namespace Bag;

[Serializable]
public abstract class BaseSkill : ISkill
{
	public int Id;

	public int SkillId;

	public int Level;

	public int Quality;

	public string Name;

	public int PinJie;

	public CanSlotType CanPutSlotType;

	public abstract void SetSkill(int id, int level);

	public abstract BaseSkill Clone();

	public virtual Sprite GetIconSprite()
	{
		return null;
	}

	public int GetImgQuality()
	{
		return Quality * 2;
	}

	public virtual Sprite GetQualitySprite()
	{
		return BagMag.Inst.QualityDict[GetImgQuality().ToString()];
	}

	public virtual Sprite GetQualityUpSprite()
	{
		return BagMag.Inst.QualityUpDict[GetImgQuality().ToString()];
	}

	public abstract string GetDesc1();

	public virtual string GetDesc2()
	{
		foreach (_ItemJsonData data in _ItemJsonData.DataList)
		{
			if (data.desc.Replace(".0", "") == SkillId.ToString())
			{
				return data.desc2;
			}
		}
		return "暂无";
	}

	public abstract string GetTypeName();

	public string GetQualityName()
	{
		return GetPinJie() + GetPinJieName();
	}

	private string GetPinJie()
	{
		return Quality switch
		{
			1 => "人阶", 
			2 => "地阶", 
			3 => "天阶", 
			_ => "无", 
		};
	}

	private string GetPinJieName()
	{
		return PinJie switch
		{
			1 => "下", 
			2 => "中", 
			3 => "上", 
			_ => "无", 
		};
	}

	public abstract List<int> GetCiZhuiList();

	public abstract bool SkillTypeIsEqual(int skIllType);
}
