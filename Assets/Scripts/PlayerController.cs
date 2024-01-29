using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField][Range(.1f, 1f)] float speed;

    void Update()
    {
        float dist = Vector2.Distance(transform.position, Input.mousePosition);
        if (dist > .1f)
        {
            Vector2 dir = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
            dir.Normalize();
            transform.position += (Vector3)dir * speed * Time.deltaTime;
        }
    }
}
