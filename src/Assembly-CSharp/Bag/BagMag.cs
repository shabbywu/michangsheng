using System.Collections.Generic;
using UnityEngine;

namespace Bag;

public class BagMag
{
	private static BagMag _inst;

	public Dictionary<string, Sprite> QualityDict = new Dictionary<string, Sprite>();

	public Dictionary<string, Sprite> QualityUpDict = new Dictionary<string, Sprite>();

	public Dictionary<string, Sprite> JiaoBiaoDict = new Dictionary<string, Sprite>();

	public static BagMag Inst
	{
		get
		{
			if (_inst == null)
			{
				_inst = new BagMag();
			}
			return _inst;
		}
	}

	public BagMag()
	{
		QualityDict = ResManager.inst.LoadSpriteAtlas("Bag/QualityBg");
		QualityUpDict = ResManager.inst.LoadSpriteAtlas("Bag/QualityLine");
		JiaoBiaoDict = ResManager.inst.LoadSpriteAtlas("Bag/JiaoBiao");
	}
}
