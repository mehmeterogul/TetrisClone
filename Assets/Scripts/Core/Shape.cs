using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shape : MonoBehaviour
{
    public bool m_canRotate = true;

    void Move(Vector3 moveDirection)
    {
        transform.position += moveDirection;
    }

    void MoveLeft()
    {
        Move(new Vector3(-1, 0, 0));
    }

    void MoveRight()
    {
        Move(new Vector3(1, 0, 0));
    }

    void MoveUp()
    {
        Move(new Vector3(0, 1, 0));
    }

    void MoveDown()
    {
        Move(new Vector3(0, -1, 0));
    }

    void RotateRight()
    {
        if(m_canRotate)
        {
            transform.Rotate(0, 0, -90);
        }
    }

    void RotateLeft()
    {
        if (m_canRotate)
        {
            transform.Rotate(0, 0, 90);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        // InvokeRepeating("MoveDown", 0, 0.5f);
        // InvokeRepeating("RotateRight", 0, 1f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
