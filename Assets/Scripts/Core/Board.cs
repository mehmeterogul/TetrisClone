using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour
{
    [SerializeField] Transform m_emptySprite;
    [SerializeField] int m_height = 30;
    [SerializeField] int m_width = 10;
    [SerializeField] int m_header = 8;

    // Start is called before the first frame update
    void Start()
    {
        DrawEmptyCells();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

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