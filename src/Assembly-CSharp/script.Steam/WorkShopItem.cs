using System;
using System.Collections.Generic;
using Steamworks;

namespace script.Steam;

[Serializable]
public class WorkShopItem
{
	public PublishedFileId_t PublishedFileId;

	public string Title;

	public string Des;

	public ulong SteamID;

	public List<string> Tags;

	public int Visibility;

	public string ModPath;

	public string ImgPath;

	public bool IsNeedNext = true;

	public List<ulong> Dependencies;

	public List<ulong> LastDependencies;

	public WorkShopItem()
	{
		Tags = new List<string>();
		Dependencies = new List<ulong>();
		LastDependencies = new List<ulong>();
		Visibility = 2;
	}

	public void AddTags(string tag)
	{
		if (!Tags.Contains(tag))
		{
			Tags.Add(tag);
		}
	}

	public void RemoveTag(string tag)
	{
		if (Tags.Contains(tag))
		{
			Tags.Remove(tag);
		}
	}
}
