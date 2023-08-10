using System.Collections.Generic;
using KBEngine;
using UnityEngine;
using UnityEngine.UI;

namespace YSGame.Fight;

public class UIFightBuffItem : MonoBehaviour
{
	public Image BuffIconImage;

	public Text BuffCountText;

	[HideInInspector]
	public int BuffID;

	[HideInInspector]
	public int BuffCount;

	[HideInInspector]
	public int BuffRound;

	[HideInInspector]
	public List<int> AvatarBuff;

	[HideInInspector]
	public Avatar Avatar;
}
