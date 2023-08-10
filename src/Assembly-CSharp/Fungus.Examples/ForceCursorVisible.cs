using UnityEngine;

namespace Fungus.Examples;

public class ForceCursorVisible : MonoBehaviour
{
	public bool CursorLocked = true;

	private void Update()
	{
		Cursor.visible = !CursorLocked;
		Cursor.lockState = (CursorLockMode)(CursorLocked ? 1 : 0);
	}
}
