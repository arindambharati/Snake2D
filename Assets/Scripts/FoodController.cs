using UnityEngine;

/// <summary>
/// This script is implements randomization of the food
/// Bounds from BoxCollider2D is used to randomize the food positions each time the snake collides with it.
/// </summary>
public class FoodController : MonoBehaviour
{
    [SerializeField] private BoxCollider2D gridArea;

    private void Start()
    {
        RandomizeFoodPosition();
    }

    private void RandomizeFoodPosition()
    {
        Bounds bounds = this.gridArea.bounds;

        float x_Position = Random.Range(bounds.min.x, bounds.max.x);
        float y_Position = Random.Range(bounds.min.y, bounds.max.y);

        this.transform.position = new Vector2(Mathf.Round(x_Position),Mathf.Round(y_Position));
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            RandomizeFoodPosition();
        }
    }
}
