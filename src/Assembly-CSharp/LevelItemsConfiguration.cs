using System;
using UnityEngine;

[Serializable]
public class LevelItemsConfiguration
{
	public string levelName;

	public bool hasSubLevel;

	public string subLevelName;

	public bool isLocked;

	public string levelToLoad;

	public Sprite levelImage;

	public int PlayerID;
}
