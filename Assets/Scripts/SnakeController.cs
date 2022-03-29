using System;
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
    public enum Direction
    {
        Up,
        Down,
        Left,
        Right
    }

    private int initialBodySize;
    private Direction moveDirection;

    private Vector2 _direction;
    private Vector2Int gridPosition;
    private Vector2Int gridMoveDirection;

    private List<Transform> _bodySegments;
    [SerializeField] private Transform bodySegmentPrefab;
   // public Transform myTransform;
    private float gridMoveTimer;
    private float gridMoveTimerMax;

    private bool hasInput;

    private void Awake()
    {
        initialBodySize = 1;
        gridPosition = new Vector2Int(0, 0);
        _bodySegments = new List<Transform>();
        gridMoveTimerMax = 0.05f;
        gridMoveTimer = gridMoveTimerMax;
        hasInput = false;
        //SnakeSize();
    }

    private void SnakeSize()
    {
         _bodySegments.Add(this.transform);
        for (int i = 1; i < this.initialBodySize; i++)
        {
            _bodySegments.Add(Instantiate(this.bodySegmentPrefab));
            Debug.Log("i= " + i);
        }

        this.transform.position = Vector2.zero;
    }

/*    private void Start()
    {

        SnakeSize();
    }*/


    private void Update()
    {
       
        PlayerInput();
        SnakeMovement();

    }

    private void PlayerInput()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow) && (moveDirection != Direction.Down))
        {
            moveDirection = Direction.Up;
            gridPosition.y += 1;
            hasInput = true;
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow) && (moveDirection != Direction.Up))
        {
            moveDirection = Direction.Down;
            gridPosition.y -= 1;
            hasInput = true;
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow) && (moveDirection != Direction.Right))
        {
            moveDirection = Direction.Left;
            gridPosition.x -= 1;
            hasInput = true;
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow) && (moveDirection != Direction.Left))
        {
            moveDirection = Direction.Right;
            gridPosition.x += 1;
            hasInput = true;
        }
    }

    private void SnakeMovement()
    {
        if (hasInput == true)
        {
            gridMoveTimer += Time.deltaTime;
            if (gridMoveTimer >= gridMoveTimerMax)
            {
                //we can also set gridMoveTimer to 0 instead of gridMoveTimerMax as it will keep reseting to 0 and then increase to 1 and repeat
                _bodySegments.Add(transform);

                UpdateGridPosition();

                gridMoveTimer -= gridMoveTimerMax;

/*                if (_bodySegments.Count >= initialBodySize + 1)
                {
                    _bodySegments.RemoveAt(_bodySegments.Count - initialBodySize);
                }
*/
/*                for (int i = 0; i < _bodySegments.Count; i++)
                {
                    //Vector2Int snakeMovePosition = _bodySegments[i];

                    _bodySegments[i].transform.position = _bodySegments[i + 1].transform.position;
                    Debug.Log("i= " + i);
                }*/

                for (int i = _bodySegments.Count; i > 0; i--)
                {
                    
                    _bodySegments[i].position = _bodySegments[i - 1].position;
                    Debug.Log("i= " + i);
                }


            }
        }
        
        transform.position = new Vector2(gridPosition.x, gridPosition.y);
    }


    private void UpdateGridPosition()
    {
        
        switch (moveDirection)
        {
            default:
            case Direction.Up: 
                gridMoveDirection = new Vector2Int(0, +1);
                break;
            case Direction.Down: 
                gridMoveDirection = new Vector2Int(0, -1); 
                break;
            case Direction.Right: 
                gridMoveDirection = new Vector2Int(+1, 0);
                break;
            case Direction.Left:
                gridMoveDirection = new Vector2Int(-1, 0);
                break;
        }

        gridPosition += gridMoveDirection;
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
