using UnityEngine;

public class RenderersProvider : MonoBehaviour
{
    [SerializeField] SkinnedMeshRenderer[] skinnedMeshRenderers;

    public void RandomizeColorOfRenderers()
    {
        foreach (var renderer in skinnedMeshRenderers)
        {
            if (renderer != null)
            {
                Color randomColor = new Color(Random.value, Random.value, Random.value);
                renderer.material.color = randomColor;
            }
        }
    }
}
