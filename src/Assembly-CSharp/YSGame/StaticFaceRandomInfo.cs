using System;
using UnityEngine;

namespace YSGame;

[Serializable]
public class StaticFaceRandomInfo
{
	[Tooltip("随机的部位")]
	public SetAvatarFaceRandomInfo.InfoName SkinTypeName;

	[Tooltip("随机部位的值")]
	public int SkinTypeScope;
}
