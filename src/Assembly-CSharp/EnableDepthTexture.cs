using UnityEngine;

[ExecuteInEditMode]
[RequireComponent(typeof(Camera))]
public class EnableDepthTexture : MonoBehaviour
{
	public bool EnableInEditor = true;

	private void OnEnable()
	{
		((Component)this).GetComponent<Camera>().depthTextureMode = (DepthTextureMode)1;
	}

	private void OnDisable()
	{
		((Component)this).GetComponent<Camera>().depthTextureMode = (DepthTextureMode)0;
	}

	private void Update()
	{
	}
}
