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


    public enum SnakeID
    {
        Snake1,
        Snake2
    }
    
    private int initialBodySize;
    private Direction moveDirection;

    private Vector2 _direction;
    private Vector2Int gridPosition;
    private Vector2Int gridMoveDirection;

    private List<Transform> _bodySegments = new List<Transform>();
    [SerializeField] private Transform bodySegmentPrefab;
    [SerializeField] private float gridMoveTimer;
    [SerializeField] private float gridMoveTimerMax;
    [SerializeField] private TextMeshProUGUI player1Score;
    [SerializeField] private TextMeshProUGUI winnerText;
    [SerializeField] private GameObject gameOverUI;
    private SnakeID snakeID;

    private bool hasInput;
    private int i;
    private int score;


    private void Awake()
    {
        initialBodySize = 1;
        gridPosition = new Vector2Int(0, 0);
       // _bodySegments = new List<Transform>();
        gridMoveTimerMax = 0.1f;
        gridMoveTimer = gridMoveTimerMax;
        hasInput = false;
        i = 0;
        //SnakeSize();
    }


    private void SnakeSize()
    {
        _bodySegments.Add(this.transform);
        this.transform.position = Vector2.zero;
    }


    private void Start()
    {
        SnakeSize();
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Food"))
        {
            SoundManager.Instance.PlaySound(Sound.Eat);
            Grow();
        }
        if (collision.gameObject.CompareTag("Frog"))
        {
            SoundManager.Instance.PlaySound(Sound.Eat);
            DecreaseSize();
            collision.gameObject.SetActive(false);
        }
        if (collision.gameObject.CompareTag("Walls"))
        {
            ScreenSwrap();
            SoundManager.Instance.PlaySound(Sound.Teleport);

        }
        if (collision.gameObject.CompareTag("Body") && isShiledActive == false)
        {

            GameOver(gameObject.name);
        }
  
    }

    private void GameOver(string looserName)
    {
        string winner = null;
        this.enabled = false;
        SoundManager.Instance.PlayBGM(Sound.GameOver);
        gameOverUI.SetActive(true);
        if (looserName == "Snake1")
        {
            winner = "Snake2";
        }
        if (looserName == "Snake2")
        {
            winner = "Snake1";
        }
        winnerText.text = winner + " Won";
        Destroy(gameObject, 2f);
    }

    private void ScreenSwrap()
    {
        Vector3 newPos = transform.position;
        if (up == false || down == false)
        {
            newPos.y = -(transform.position.y);
        }
        if (left == false || right == false)
        {
            newPos.x = -(transform.position.x);
        }
        transform.position = newPos;
    }


    private void Update()
    {
       
        PlayerInputs();
        SnakeMovement();
    }

    private void PlayerInputs()
    {
        switch (snakeID)
        {
            case SnakeID.Snake1:
                {
                    Player1Input();
                    break;
                }

            case SnakeID.Snake2:
                {
                    player2Input();
                    break;
                }


        }
    }

    private void Player1_Input()
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

    private void Player2_Input()
    {
        if (Input.GetKeyDown(KeyCode.W) && (moveDirection != Direction.Down))
        {
            moveDirection = Direction.Up;
            gridPosition.y += 1;
            hasInput = true;
        }
        else if (Input.GetKeyDown(KeyCode.S) && (moveDirection != Direction.Up))
        {
            moveDirection = Direction.Down;
            gridPosition.y -= 1;
            hasInput = true;
        }
        else if (Input.GetKeyDown(KeyCode.A) && (moveDirection != Direction.Right))
        {
            moveDirection = Direction.Left;
            gridPosition.x -= 1;
            hasInput = true;
        }
        else if (Input.GetKeyDown(KeyCode.D) && (moveDirection != Direction.Left))
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
              //  _bodySegments.Add(transform);

                UpdateGridPosition();

                gridMoveTimer -= gridMoveTimerMax; //we can also set gridMoveTimer to 0 instead of gridMoveTimerMax as it will keep reseting to 0 and then increase to 1 and repeat

                /*                if (_bodySegments.Count >= initialBodySize + 1)
                                {
                                    _bodySegments.RemoveAt(_bodySegments.Count - initialBodySize);
                                }
                */


                /*                for (int i = _bodySegments.Count; i > 0; i--)
                                {

                                    _bodySegments[i].position = _bodySegments[i - 1].position;
                                    Debug.Log("i= " + i);
                                }
                */

                /*                if (i == 0)
                                {
                                    i = 0;
                                }
                                else
                                {
                                    i = _bodySegments.Count;
                                }*/
                //i = 0;
                // i = 0;
                Vector3 swapPosition = new Vector3(0, 0, 0);
                foreach ( var segment in _bodySegments)
                {
                    if (segment == _bodySegments[0])
                    {
                        // i++;
                        swapPosition = segment.position;
                        continue;
                    }

                    Vector3 swapPosition2 = segment.position;
                    segment.position = swapPosition;
                    swapPosition = swapPosition2;
                   // var i = _bodySegments.IndexOf(segment);
                   // segment.position = _bodySegments[i-1].position;
                   // i++;
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


    private void DecreaseSize()
    {
        score--;
        player1Score.text = " Score:" + score;
        int bodyCount = body.Count - 1;
        if (bodyCount >= 1)
        {
            Destroy(body[bodyCount].gameObject);
            body.RemoveAt(bodyCount);

        }
        else
        {
            return;
        }

    }
}
