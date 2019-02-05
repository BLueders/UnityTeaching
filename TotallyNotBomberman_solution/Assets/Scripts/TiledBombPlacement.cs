using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TiledBombPlacement : BombPlacement
{
    public LayerMask bombLayers;

    public override void PlaceBomb()
    {
        Vector3 target = transform.position;

        target = new Vector3(Mathf.Round(target.x), Mathf.Round(target.y), target.z);

        RaycastHit2D hit = Physics2D.Linecast(transform.position, target, bombLayers);

        if (hit.collider == null) {
            Instantiate(bomb, target, Quaternion.identity);
        }
    }
}
