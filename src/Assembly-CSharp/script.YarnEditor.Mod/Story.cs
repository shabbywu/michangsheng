using System;
using System.Collections.Generic;

namespace script.YarnEditor.Mod;

[Serializable]
public class Story
{
	public string ModName;

	public Dictionary<string, Yarn> YarnDict = new Dictionary<string, Yarn>();
}
