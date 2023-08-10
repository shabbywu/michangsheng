using System;
using System.Collections.Generic;
using UnityEngine;

namespace YSGame;

[Serializable]
public class AvatarFaceInfo
{
	public string name;

	[Tooltip("武将随机的范围")]
	public List<Vector2> AvatarScope;

	[Tooltip("随机的部位")]
	public SetAvatarFaceRandomInfo.InfoName SkinTypeName;

	[Tooltip("随机部位的范围")]
	public List<Vector2> SkinTypeScope;
}
