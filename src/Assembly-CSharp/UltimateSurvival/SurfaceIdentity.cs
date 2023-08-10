using UnityEngine;

namespace UltimateSurvival;

[RequireComponent(typeof(Collider))]
public class SurfaceIdentity : MonoBehaviour
{
	[SerializeField]
	[Tooltip("The texture for this surface (useful when there's no renderer attached to check for textures).")]
	private Texture m_Texture;

	public Texture Texture
	{
		get
		{
			return m_Texture;
		}
		set
		{
			m_Texture = value;
		}
	}
}
