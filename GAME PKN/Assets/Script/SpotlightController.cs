using UnityEngine;

public class SpotlightController : MonoBehaviour
{
    [Header("References")]
    public Transform player;
    public Material spotlightMaterial;
    
    [Header("Settings")]
    public float spotRadius;
    public float falloff = 2f;
    public float pulseSpeed = 1f;
    public float pulseAmount = 0.5f;

    [Header("Visibility Settings")]
    public LayerMask visibleLayers;
    public LayerMask obstacleLayers;
    public float minAlpha = 0.3f;

    void Update()
    {
        if (player != null && spotlightMaterial != null)
        {
            // Update shader properties
            spotlightMaterial.SetVector("_PlayerPos", player.position);
            
            float pulse = Mathf.PingPong(Time.time * pulseSpeed, pulseAmount);
            spotlightMaterial.SetFloat("_SpotRadius", spotRadius + pulse);
            spotlightMaterial.SetFloat("_Falloff", falloff);

            // Update object visibility
            UpdateObjectVisibility();
        }
    }

    void UpdateObjectVisibility()
    {
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(player.position, spotRadius * 1.5f, visibleLayers);
        
        foreach (var collider in hitColliders)
        {
            SpriteRenderer sr = collider.GetComponent<SpriteRenderer>();
            if (sr != null)
            {
                // Hitung jarak dan cek obstacle
                float dist = Vector2.Distance(player.position, collider.transform.position);
                RaycastHit2D hit = Physics2D.Linecast(player.position, collider.transform.position, obstacleLayers);
                
                // Hitung alpha
                float spot = 1 - Mathf.Clamp01(dist / spotRadius);
                float alpha = (hit.collider == null) ? spot : minAlpha;
                
                // Update warna sprite
                Color newColor = sr.color;
                newColor.a = Mathf.Max(alpha, minAlpha);
                sr.color = newColor;
            }
        }
    }
}