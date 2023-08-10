using System;
using UnityEngine;

namespace UltimateSurvival;

[Serializable]
public class RequiredItem
{
	[SerializeField]
	private string m_Name;

	[SerializeField]
	private int m_Amount;

	public string Name => m_Name;

	public int Amount => m_Amount;
}
