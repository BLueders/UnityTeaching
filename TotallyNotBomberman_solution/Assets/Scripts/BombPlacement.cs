using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombPlacement : MonoBehaviour
{
    public GameObject bomb;

    public virtual void PlaceBomb()
    {
        Instantiate(bomb, transform.position, Quaternion.identity);
    }
}
