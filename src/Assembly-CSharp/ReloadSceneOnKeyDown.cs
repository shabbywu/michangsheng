using UnityEngine;
using UnityEngine.SceneManagement;

public class ReloadSceneOnKeyDown : MonoBehaviour
{
	public KeyCode reloadKey = (KeyCode)114;

	private void Update()
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_000d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0012: Unknown result type (might be due to invalid IL or missing references)
		if (Input.GetKeyDown(reloadKey))
		{
			Scene activeScene = SceneManager.GetActiveScene();
			SceneManager.LoadScene(((Scene)(ref activeScene)).buildIndex, (LoadSceneMode)0);
		}
	}
}
