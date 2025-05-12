using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class ObjectVisibilityController : MonoBehaviour
{
    [Header("Spotlight Settings")]
    public Transform player;
    public float spotRadius = 5f;
    public float fadeSpeed = 5f;
    public LayerMask obstacleLayers;

    [Header("Visibility Settings")]
    [Range(0, 10)] public float minVisibility = 0.1f;
    [Range(0, 10)] public float maxVisibility = 10f;

    private SpriteRenderer spriteRenderer;
    private float currentVisibility;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        currentVisibility = minVisibility;
        UpdateVisibility();
    }

    void Update()
    {
        if (player == null) return;

        float targetVisibility = CalculateTargetVisibility();
        currentVisibility = Mathf.Lerp(currentVisibility, targetVisibility, fadeSpeed * Time.deltaTime);
        UpdateVisibility();
    }

    float CalculateTargetVisibility()
    {
        float dist = Vector2.Distance(transform.position, player.position);
        
        // Jika di luar radius, tetap minimal visibility
        if (dist > spotRadius * 1.2f) return minVisibility;

        // Cek apakah ada penghalang
        RaycastHit2D hit = Physics2D.Linecast(player.position, transform.position, obstacleLayers);
        if (hit.collider != null && hit.collider.gameObject != gameObject) return minVisibility;

        // Hitung visibility berdasarkan jarak
        float visibility = 1 - Mathf.Clamp01(dist / spotRadius);
        return Mathf.Lerp(minVisibility, maxVisibility, visibility);
    }

    void UpdateVisibility()
    {
        Color color = spriteRenderer.color;
        color.a = currentVisibility;
        spriteRenderer.color = color;
    }

    void OnDrawGizmosSelected()
    {
        if (player != null)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawLine(player.position, transform.position);
        }
    }
}