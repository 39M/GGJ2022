using UnityEngine;

public class WaterMove : MonoBehaviour
{

    public float deltMove;
    public float speed;
    private float startX;

    private void Start()
    {
        startX = transform.position.x;
    }

    void FixedUpdate()
    {
        transform.position += Vector3.right * speed;
        if (transform.position.x >= startX + deltMove)
        {
            transform.position += Vector3.left * deltMove;
        }
    }
}