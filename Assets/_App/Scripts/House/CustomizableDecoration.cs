using UnityEngine;

public class CustomizableDecoration : MonoBehaviour
{
    protected MeshRenderer[] renderers;
    public AudioClip[] enterSounds;
    public float volume = .25f;

    protected void Awake()
    {
        renderers = GetComponentsInChildren<MeshRenderer>();
    }


    public void PlaySound()
    {
        if (enterSounds != null && enterSounds.Length > 0)
        {
            AudioSource.PlayClipAtPoint(enterSounds[UnityEngine.Random.Range(0, enterSounds.Length)], transform.position, volume);
        }
    }

    public void SetMaterial(Material mat)
    {
        SwapMaterials(renderers, mat);
    }

    protected void SwapMaterials(MeshRenderer[] renderers, Material mat)
    {
        if (renderers == null) return;
        foreach (MeshRenderer rend in renderers)
        {
            rend.material = mat;
        }
    }
}