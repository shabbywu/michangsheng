using UnityEngine;

namespace Fungus;

[EventHandlerInfo("MonoBehaviour", "CharacterCollider", "The block will execute when tag filtered OnCharacterColliderHit is received")]
[AddComponentMenu("")]
public class CharacterControllerCollide : TagFilteredEventHandler
{
	private void OnControllerColliderHit(ControllerColliderHit hit)
	{
		ProcessTagFilter(hit.gameObject.tag);
	}
}
