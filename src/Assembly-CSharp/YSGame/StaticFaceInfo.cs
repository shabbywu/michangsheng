using System;
using System.Collections.Generic;
using UnityEngine;

namespace YSGame;

[Serializable]
public class StaticFaceInfo
{
	public string name;

	[Tooltip("武将ID")]
	public int AvatarScope;

	public List<StaticFaceRandomInfo> faceinfoList;
}
