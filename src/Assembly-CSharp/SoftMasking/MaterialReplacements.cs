using System;
using System.Collections.Generic;
using UnityEngine;

namespace SoftMasking;

internal class MaterialReplacements
{
	private class MaterialOverride
	{
		private int _useCount;

		public Material original { get; private set; }

		public Material replacement { get; private set; }

		public MaterialOverride(Material original, Material replacement)
		{
			this.original = original;
			this.replacement = replacement;
			_useCount = 1;
		}

		public Material Get()
		{
			_useCount++;
			return replacement;
		}

		public bool Release()
		{
			return --_useCount == 0;
		}
	}

	private readonly IMaterialReplacer _replacer;

	private readonly Action<Material> _applyParameters;

	private readonly List<MaterialOverride> _overrides = new List<MaterialOverride>();

	public MaterialReplacements(IMaterialReplacer replacer, Action<Material> applyParameters)
	{
		_replacer = replacer;
		_applyParameters = applyParameters;
	}

	public Material Get(Material original)
	{
		for (int i = 0; i < _overrides.Count; i++)
		{
			MaterialOverride materialOverride = _overrides[i];
			if (materialOverride.original == original)
			{
				Material val = materialOverride.Get();
				if (Object.op_Implicit((Object)(object)val))
				{
					val.CopyPropertiesFromMaterial(original);
					_applyParameters(val);
				}
				return val;
			}
		}
		Material val2 = _replacer.Replace(original);
		if (Object.op_Implicit((Object)(object)val2))
		{
			((Object)val2).hideFlags = (HideFlags)61;
			_applyParameters(val2);
		}
		_overrides.Add(new MaterialOverride(original, val2));
		return val2;
	}

	public void Release(Material replacement)
	{
		for (int i = 0; i < _overrides.Count; i++)
		{
			MaterialOverride materialOverride = _overrides[i];
			if ((Object)(object)materialOverride.replacement == (Object)(object)replacement && materialOverride.Release())
			{
				Object.DestroyImmediate((Object)(object)replacement);
				_overrides.RemoveAt(i);
				break;
			}
		}
	}

	public void ApplyAll()
	{
		for (int i = 0; i < _overrides.Count; i++)
		{
			Material replacement = _overrides[i].replacement;
			if (Object.op_Implicit((Object)(object)replacement))
			{
				_applyParameters(replacement);
			}
		}
	}

	public void DestroyAllAndClear()
	{
		for (int i = 0; i < _overrides.Count; i++)
		{
			Object.DestroyImmediate((Object)(object)_overrides[i].replacement);
		}
		_overrides.Clear();
	}
}
