using UnityEngine;

namespace Bag;

public interface ISkill
{
	Sprite GetIconSprite();

	Sprite GetQualitySprite();

	Sprite GetQualityUpSprite();
}
