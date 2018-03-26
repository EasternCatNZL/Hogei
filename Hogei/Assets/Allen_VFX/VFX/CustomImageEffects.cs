using UnityEngine;

[ExecuteInEditMode]
public class CustomImageEffects : MonoBehaviour
{
	public Material EffectMaterial;
	public Vector4 cie;

	void OnRenderImage(RenderTexture src, RenderTexture dst)
	{
		EffectMaterial.SetVector ("_ChromaOffset", cie);
		if (EffectMaterial != null)
			Graphics.Blit(src, dst, EffectMaterial);
	}
}

