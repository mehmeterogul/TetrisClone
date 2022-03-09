using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour
{
    [Header("Board Components")]
    [SerializeField] Transform m_emptySprite;
    [SerializeField] int m_height = 30;
    [SerializeField] int m_width = 10;
    [SerializeField] int m_header = 8;

    Transform[,] m_grid;

    void Awake()
    {
        m_grid = new Transform[m_width, m_height];
    }

    // Start is called before the first frame update
    void Start()
    {
        DrawEmptyCells();
    }

    bool IsWithinBoard(int x, int y)
    {
        return (x >= 0 && x < m_width && y >= 0);
    }

    public bool IsValidPosition(Shape shape)
    {
        foreach(Transform child in shape.transform)
        {
            Vector2 pos = Vectorf.Round(child.position);
            
            if(!IsWithinBoard((int)pos.x, (int)pos.y))
            {
                return false;
            }
        }

        return true;
    }

    // create board with empty cell sprites
    void DrawEmptyCells()
    {
        if(m_emptySprite)
        {
            for (int y = 0; y < m_height - m_header; y++)
            {
                for (int x = 0; x < m_width; x++)
                {
                    Transform clone;
                    clone = Instantiate(m_emptySprite, new Vector3(x, y, 0), Quaternion.identity);
                    clone.name = "Board Space { x = " + x.ToString() + " , y = " + y.ToString() + " }";
                    clone.transform.parent = transform;
                }
            }
        }
        else
        {
            Debug.LogError("Please assign the emptySprite object!");
        }
    }
}
