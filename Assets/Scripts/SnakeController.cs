using UnityEngine;

/// <summary>
/// This script is implementing the Snake Movement.
/// Taking input in the update() with Vector2 to represent the positions of the snake
/// this.transform.position.x || this.transform.position.y is actually taking the current position of the snake gameobject
/// Round() is used as the snake game is grid based hence, rounding off the numbers of the position actually helps in doing that.
/// </summary>
public class SnakeController : MonoBehaviour
{
    private Vector2 _direction;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow)) {
            _direction = Vector2.up;
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow)) {
            _direction = Vector2.down;
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow)) { 
            _direction = Vector2.left;
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow)) {
            _direction = Vector2.right;
        }
    }

    private void FixedUpdate()
    {
        SnakeMovement();
    }

    private void SnakeMovement()
    {
        this.transform.position = new Vector2(
            Mathf.Round(this.transform.position.x) + _direction.x,
            Mathf.Round(this.transform.position.y) + _direction.y
            );
    }
}
