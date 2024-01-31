using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField][Range(.1f, 1f)] float speed;
    [SerializeField] Vector2 box;
    [SerializeField] Vector2 offset;
    SpriteRenderer renderer;
    Animator animator;

    private void Start()
    {
        renderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        float dist = Vector2.Distance(transform.position, Camera.main.ScreenToWorldPoint(Input.mousePosition));
        if (dist > .1f)
        {
            Vector2 dir = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
            dir.Normalize();
            Vector3 pos = transform.position + (Vector3)dir * speed * Time.deltaTime;
            pos.x = Mathf.Clamp(pos.x, offset.x - box.x / 2, offset.x + box.x / 2);
            pos.y = Mathf.Clamp(pos.y, offset.y - box.y / 2, offset.y + box.y / 2);
            transform.position = pos;
            renderer.flipX = dir.x < 0;
            animator.SetBool("IsWalking", true);
        }
        else
        {
            animator.SetBool("IsWalking", false);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireCube(offset, box);
    }
}
