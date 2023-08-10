using System;
using System.Collections.Generic;
using UnityEngine;

namespace UltimateSurvival;

[Serializable]
public class ItemData
{
	[SerializeField]
	private string m_Name;

	[SerializeField]
	private string m_DisplayName;

	[SerializeField]
	private int m_Id;

	[SerializeField]
	private string m_Category;

	[SerializeField]
	private Sprite m_Icon;

	[SerializeField]
	private GameObject m_WorldObject;

	[SerializeField]
	[Multiline]
	private string[] m_Descriptions;

	[SerializeField]
	private int m_StackSize = 1;

	[SerializeField]
	private List<ItemProperty.Value> m_PropertyValues;

	[SerializeField]
	private bool m_IsBuildable;

	[SerializeField]
	private bool m_IsCraftable;

	[SerializeField]
	private Recipe m_Recipe;

	public string Name => m_Name;

	public string DisplayName => m_DisplayName;

	public int Id
	{
		get
		{
			return m_Id;
		}
		set
		{
			m_Id = value;
		}
	}

	public string Category
	{
		get
		{
			return m_Category;
		}
		set
		{
			m_Category = value;
		}
	}

	public Sprite Icon => m_Icon;

	public GameObject WorldObject => m_WorldObject;

	public string[] Descriptions => m_Descriptions;

	public int StackSize => m_StackSize;

	public List<ItemProperty.Value> PropertyValues => m_PropertyValues;

	public bool IsBuildable => m_IsBuildable;

	public bool IsCraftable => m_IsCraftable;

	public Recipe Recipe => m_Recipe;
}
