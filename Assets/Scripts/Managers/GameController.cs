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
    [SerializeField] float dropInterval = 1f;
    float timeToDrop;

    // Start is called before the first frame update
    void Start()
    {
        // find board and spawner
        m_gameBoard = FindObjectOfType<Board>();
        m_spawner = FindObjectOfType<Spawner>();

        if(m_spawner)
        {
            // m_spawner.transform.position = Vector3Int.RoundToInt(m_spawner.transform.position);
            m_spawner.transform.position = Vectorf.Round(m_spawner.transform.position);

            if (m_activeShape == null)
            {
                m_activeShape = m_spawner.SpawnShape();
            }
        }

        if(!m_gameBoard)
        {
            Debug.LogWarning("WARNING! There is no game board defined!");
        }

        if(!m_spawner)
        {
            Debug.LogWarning("WARNING! There is no spawner defined!");
        }
    }

    // Update is called once per frame
    void Update()
    {
        // if there is no game board or spawner object, then don't run the game
        if(!m_gameBoard || !m_spawner)
        {
            return;
        }

        if(Time.time > timeToDrop)
        {
            timeToDrop = Time.time + dropInterval;

            if (m_activeShape)
            {
                m_activeShape.MoveDown();

                if(!m_gameBoard.IsValidPosition(m_activeShape))
                {
                    m_activeShape.MoveUp();

                    m_activeShape = m_spawner.SpawnShape();
                }
            }
        }
    }
}
