using System;
using UnityEngine;
using UnityEngine.UI;

namespace UltimateSurvival.GUISystem;

[Serializable]
public class MessageForPlayer
{
	[SerializeField]
	private GameObject m_Root;

	[SerializeField]
	private Text m_Text;

	public void Toggle(bool toggle)
	{
		m_Root.SetActive(toggle);
	}

	public void SetText(string message)
	{
		m_Text.text = message;
	}
}
