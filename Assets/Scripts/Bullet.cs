using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Player player;
    private Transform sprite;
    private bool direction;
    public float speed;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        sprite = player.GetComponent<Transform>();
        direction = sprite.transform;

        Destroy(gameObject, 5);
    }

    void Update()
    {
        Atirar();
    }

    void Atirar()
    {
        if(sprite.transform.position.x < 0)
        {
            transform.Translate(Vector2.left * Time.deltaTime * speed);
            //transform.eulerAngles = new Vector3(0, player.point.position.x, 0);
        }

        if (sprite.transform.position.x > 0)
        {
            transform.Translate(Vector2.right * Time.deltaTime * speed);
            //transform.eulerAngles = new Vector3(0, -player.point_Back.position.x, 0);
        }
    }
}
