using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UltimateSurvival;

[Serializable]
public class ReorderableGenericList<T> : IEnumerable<T>, IEnumerable
{
	[SerializeField]
	private List<T> m_List;

	public T this[int key]
	{
		get
		{
			return m_List[key];
		}
		set
		{
			m_List[key] = value;
		}
	}

	public int Count => m_List.Count;

	public List<T> List => m_List;

	public IEnumerator<T> GetEnumerator()
	{
		return m_List.GetEnumerator();
	}

	IEnumerator IEnumerable.GetEnumerator()
	{
		return GetEnumerator();
	}
}
