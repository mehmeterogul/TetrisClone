using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    // reference to the game board
    Board m_gameBoard;

    // reference to the shape spawner
    Spawner m_spawner;

    // currently active shape
    Shape m_activeShape;

    // time interval that moving down of active shape
    [SerializeField] float m_dropInterval = 1f;
    float m_timeToDrop;

    [SerializeField] [Range(0.02f, 1f)] float m_leftRightKeyRepeatRate = 0.25f;
    float m_timeToNextLeftRightKey;

    [SerializeField] [Range(0.02f, 1f)] float m_rotateKeyRepeatRate = 0.25f;
    float m_timeToNextRotateKey;

    [SerializeField] [Range(0.02f, 1f)] float m_downKeyRepeatRate = 0.25f;
    float m_timeToNextDownKey;

    // Start is called before the first frame update
    void Start()
    {
        m_timeToNextLeftRightKey = Time.time + m_leftRightKeyRepeatRate;
        m_timeToNextRotateKey = Time.time + m_rotateKeyRepeatRate;
        m_timeToNextDownKey = Time.time + m_downKeyRepeatRate;

        // find board and spawner
        m_gameBoard = FindObjectOfType<Board>();
        m_spawner = FindObjectOfType<Spawner>();

        if(!m_gameBoard)
        {
            Debug.LogWarning("WARNING! There is no game board defined!");
        }

        if(!m_spawner)
        {
            Debug.LogWarning("WARNING! There is no spawner defined!");
        }
        else
        {
            // m_spawner.transform.position = Vector3Int.RoundToInt(m_spawner.transform.position);
            m_spawner.transform.position = Vectorf.Round(m_spawner.transform.position);

            if (!m_activeShape)
            {
                m_activeShape = m_spawner.SpawnShape();
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        // if there is no game board, spawner object or active shape, then don't run the game
        if (!m_gameBoard || !m_spawner || !m_activeShape)
        {
            return;
        }

        PlayerInput();
    }

    private void PlayerInput()
    {
        if (Input.GetButton("MoveRight") && Time.time > m_timeToNextLeftRightKey || Input.GetButtonDown("MoveRight"))
        {
            m_activeShape.MoveRight();
            m_timeToNextLeftRightKey = Time.time + m_leftRightKeyRepeatRate;

            if (!m_gameBoard.IsValidPosition(m_activeShape))
            {
                m_activeShape.MoveLeft();
            }
        }
        else if (Input.GetButton("MoveLeft") && (Time.time > m_timeToNextLeftRightKey) || Input.GetButtonDown("MoveLeft"))
        {
            m_activeShape.MoveLeft();
            m_timeToNextLeftRightKey = Time.time + m_leftRightKeyRepeatRate;

            if (!m_gameBoard.IsValidPosition(m_activeShape))
            {
                m_activeShape.MoveRight();
            }
        }
        else if (Input.GetButtonDown("Rotate") && (Time.time > m_timeToNextRotateKey))
        {
            m_activeShape.RotateRight();
            m_timeToNextRotateKey = Time.time + m_rotateKeyRepeatRate;

            if (!m_gameBoard.IsValidPosition(m_activeShape))
            {
                m_activeShape.RotateLeft();
            }
        }
        else if (Input.GetButton("MoveDown") && (Time.time > m_timeToNextDownKey) || Time.time > m_timeToDrop)
        {
            m_timeToDrop = Time.time + m_dropInterval;
            m_timeToNextDownKey = Time.time + m_downKeyRepeatRate;

            m_activeShape.MoveDown();

            if (!m_gameBoard.IsValidPosition(m_activeShape))
            {
                LandShape();
            }
        }
    }

    private void LandShape()
    {
        // shape lands here
        m_timeToNextLeftRightKey = Time.time;
        m_timeToNextRotateKey = Time.time;
        m_timeToNextDownKey = Time.time;

        m_activeShape.MoveUp();
        m_gameBoard.StoreShapeInGrid(m_activeShape);
        m_activeShape = m_spawner.SpawnShape();
    }
}
