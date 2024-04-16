using UnityEngine;

public class ExplosiveBarrel : MonoBehaviour
{
    public float explosionForce = 1000f;
    public float explosionRadius = 5f;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Car")) // Make sure your car object has a tag named "Car"
        {
            Explode();
        }
    }

    void Explode()
    {
        // Get all colliders around the barrel
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, explosionRadius);
        foreach (var hit in colliders)
        {
            Rigidbody2D rb = hit.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                // Apply an explosion force
                if (rb != null && hit.gameObject.CompareTag("Car")) // Ensure we're applying force to the car
                {
                    Vector2 direction = rb.position - (Vector2)transform.position;
                    float distance = direction.magnitude;
                    // Adjust the explosion force calculation as needed. This is a very basic approach.
                    Vector2 force = direction.normalized * (explosionForce / (distance == 0 ? 1 : distance)); // Prevent division by zero
                    rb.AddForce(force);
                }
            }
        }

        // Optionally destroy the barrel object after explosion
        Destroy(gameObject);
    }
}
