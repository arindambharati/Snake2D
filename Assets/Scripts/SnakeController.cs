using System.Collections.Generic;
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
    private List<Transform> _bodySegments;
    [SerializeField] private Transform bodySegmentPrefab;
    private int initialSize;
    private void Awake()
    {
        initialSize = 4;
        _bodySegments = new List<Transform>();
    }

    private void SnakeSize()
    {
        _bodySegments.Add(this.transform);
        for (int i = 1; i < this.initialSize; i++){
            _bodySegments.Add(Instantiate(this.bodySegmentPrefab));
        }

        this.transform.position = Vector2.zero;
    }

    private void Start()
    {
        SnakeSize();
    }


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
        for(int i = _bodySegments.Count - 1; i > 0; i--){
            _bodySegments[i].position = _bodySegments[i - 1].position;
        }

        SnakeMovement();
    }

    private void SnakeMovement()
    {
        this.transform.position = new Vector2(
            Mathf.Round(this.transform.position.x) + _direction.x ,
            Mathf.Round(this.transform.position.y) + _direction.y
            ) ;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Food")){
            Grow();
        }
    }

    private void Grow()
    {
        Transform segment = Instantiate(this.bodySegmentPrefab);
        //grab the position of the last segment prefab
        segment.position = _bodySegments[_bodySegments.Count - 1].position;
        _bodySegments.Add(segment);
    }

}
