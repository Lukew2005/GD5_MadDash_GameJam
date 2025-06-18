using UnityEngine;

public class BackgroundScrolling : MonoBehaviour
{
    private Vector3 startpos;
    private float repeatWidth;
    public float speed = 8;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        startpos = transform.position;
        repeatWidth = GetComponent<BoxCollider>().size.x / 2;

    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.left * Time.deltaTime * speed);

        if (transform.position.x < startpos.x - repeatWidth)
        {
            transform.position = startpos;
        }
    }
}
