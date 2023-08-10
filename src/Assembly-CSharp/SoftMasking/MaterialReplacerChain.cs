using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace SoftMasking;

public class MaterialReplacerChain : IMaterialReplacer
{
	private readonly List<IMaterialReplacer> _replacers;

	public int order { get; private set; }

	public MaterialReplacerChain(IEnumerable<IMaterialReplacer> replacers, IMaterialReplacer yetAnother)
	{
		_replacers = replacers.ToList();
		_replacers.Add(yetAnother);
		Initialize();
	}

	public Material Replace(Material material)
	{
		for (int i = 0; i < _replacers.Count; i++)
		{
			Material val = _replacers[i].Replace(material);
			if ((Object)(object)val != (Object)null)
			{
				return val;
			}
		}
		return null;
	}

	private void Initialize()
	{
		_replacers.Sort((IMaterialReplacer a, IMaterialReplacer b) => a.order.CompareTo(b.order));
		order = _replacers[0].order;
	}
}
