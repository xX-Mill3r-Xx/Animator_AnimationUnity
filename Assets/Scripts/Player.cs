using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    /*Modulo sobre Animação do curso CSJ-Academy*/

    #region Info
    /*Este modulo do curso de desenvolvimento de games é sobre animaçoes e movimentaçoes.
     Embora eu tenha seguido todo o Modulo e tenha aprendido uma nova forma de animar e movimentar o personagem,
    decidi fazer este codigo do Player por conta propia, com as mecanicas de movimentação e animação das quais 
    ja tenho relativo dominio;*/
    #endregion

    #region Variaveis Globais
    private Rigidbody2D rig;                  // Declaração do componente rigidbody2D
    private Animator anim;                    // Declaração do Animator
    private bool isJumping;                   // Variavel que verifica se player esta pulando ou não
    private bool isAtacking;                  // Variavel que verifica se player esta atacando ou não
    private bool isGround;                    // variavel que verifica se player esta no chão ou não
    public float speed;                       // Velocidade do player
    public float jump;                        // Força do pulo do player
    #endregion

    #region Not Function
    //public Transform point;
    //public Transform point_Back;
    //public GameObject bullet;
    #endregion

    void Start()
    {
        #region Chamado na inicialização da cena
        rig = GetComponent<Rigidbody2D>();    //Passando o componente a variavel rig
        anim = GetComponent<Animator>();      //Passando o componente a variavel anim
        #endregion
    }

    void Update()
    {
        #region Chamado a cada frame
        Jump();                               //Chamando o metodo de pulo do personagem
        Atack();                              //Chamando o metodo de ataque do personagem
        #endregion
    }

    private void FixedUpdate()
    {
        #region Chamado quando trabalhamos com fisica
        /*Quando trabalhamos com componentes de fisica como por exemplo o rigidBody2d, devemos tratar os metodos
         que utilizam deste componente dentro do FixedUpdate*/
        Move();
        #endregion
    }

    void Move()
    {
        #region Movimentação
        float v = Input.GetAxis("Horizontal");

        if (v < 0 && !isJumping && !isAtacking)
        {
            anim.SetInteger("transicao", 1);
            rig.velocity = new Vector2(-speed, rig.velocity.y);
            transform.eulerAngles = new Vector3(0, 180, 0);
        }

        if (v > 0 && !isJumping && !isAtacking)
        {
            anim.SetInteger("transicao", 1);
            rig.velocity = new Vector2(speed, rig.velocity.y);
            transform.eulerAngles = new Vector3(0, 0, 0);
        }

        if (v == 0 && !isJumping && !isAtacking)
        {
            anim.SetInteger("transicao", 0);
        }
        #endregion
    }

    void Jump()
    {
        #region Jump
        if (Input.GetButtonDown("Jump") && isGround && !isJumping)
        {
            anim.SetInteger("transicao", 2);
            rig.AddForce(Vector2.up * jump, ForceMode2D.Impulse);
            isJumping = true;
            isGround = false;
        }
        #endregion
    }

    void Atack()
    {
        #region Ataque
        if (Input.GetButtonDown("Fire1"))
        {
            isAtacking = true;
            anim.SetInteger("transicao", 3);

            #region Bullet Not Function
            //GameObject missele = Instantiate(bullet);
            //missele.transform.position = point.transform.position;
            //if(point.transform.position.x < 0)
            //{
            //    missele.transform.position = point.transform.position;
            //}

            //if (point.transform.position.x > 0)
            //{
            //    missele.transform.position = -point.transform.position;
            //}
            #endregion
        }
        StartCoroutine(OnAtack());  //Chama a corrotina para tirar a animação de ataque
        #endregion
    }

    #region Corrotina que normaliza o efeito de ataque do player
    IEnumerator OnAtack()
    {
        while (isAtacking)
        {
            yield return new WaitForSeconds(0.25f);
            isAtacking = false;
        }
    }
    #endregion

    private void OnCollisionEnter2D(Collision2D collision)
    {
        #region check se personagem esta ou não em contato com a layer
        if (collision.gameObject.layer == 6)
        {
            isGround = true;
            isJumping = false;
            
        }

        if(collision.gameObject.tag == "Enemy")
        {
            collision.gameObject.GetComponent<Animator>().SetTrigger("hit");
            Destroy(collision.gameObject, 2f);
            rig.AddForce(Vector2.up * jump,ForceMode2D.Impulse);
            AudioController.current.PlayMusic(AudioController.current.anoterSfx);
        }
        #endregion
    }
}
