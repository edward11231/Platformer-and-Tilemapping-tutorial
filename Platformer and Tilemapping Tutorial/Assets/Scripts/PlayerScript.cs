using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerScript : MonoBehaviour
{
    private Rigidbody2D rd2d;

    public float speed;
    public float jumpForce;
    public int playerLives = 3;

    public Text score;
    public Text win;
    public Text lives;

    Animator animator;

    private int scoreValue = 0;
    private bool facingRight = true;


    // Start is called before the first frame update
    void Start()
    {
        rd2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        SetScoreText();
        win.text = "";
        lives.text = "Lives: " + playerLives.ToString();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float hozMovement = Input.GetAxis("Horizontal") * speed;
        float vertMovement = Input.GetAxis("Vertical");
        rd2d.AddForce(new Vector2(hozMovement, vertMovement));

        if(Input.GetKeyDown(KeyCode.A))
        {
            if (facingRight == false && hozMovement > 0)
            {
                Flip();                  
            }
            else if (facingRight == true && hozMovement < 0)
            {
                Flip();
            }
            animator.SetInteger("Move", 1);
        }

        else if(Input.GetKeyDown(KeyCode.D))
        {
            if (facingRight == false && hozMovement > 0)
            {
                Flip();
            }

            else if (facingRight == true && hozMovement < 0)
            {
                Flip();
            }
                animator.SetInteger("Move", 1);
        }       
        else if(hozMovement == 0)
            animator.SetInteger("Move", 0);
    }
    private void Update()
    {
        if(scoreValue == 8)
        {
            win.text = "You Win! Game created by: Edward Tavarez";
            this.gameObject.SetActive(false);
        }

        if(playerLives == 0)
        {
            win.text = "You Lose. Game Over";
            this.gameObject.SetActive(false);
        }

        if(Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
            Debug.Log("Game Quit");
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Coin")
        {
            scoreValue += 1;
            SetScoreText();
            Destroy(collision.collider.gameObject);

            if(scoreValue == 4)
            {
                transform.position = new Vector2(80, 1);
                playerLives = 3;
                SetLivesText();
            }
                
        }

        if (collision.collider.tag == "Enemy")
        {
            Debug.Log("hello dude");
            playerLives -= 1;
            SetLivesText();
            Destroy(collision.collider.gameObject);
        }

    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.collider.tag == "Ground")
        {
            if (Input.GetKeyDown(KeyCode.W))
            {
                animator.SetBool("isGrounded", false);
                rd2d.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
                //rd2d.velocity = Vector2.up * jumpForce;
            }
            else
                animator.SetBool("isGrounded", true);
        }
    }

    void SetScoreText()
    {
        score.text = "Score: " + scoreValue.ToString();
    }
    void SetLivesText()
    {
        lives.text = "Lives: " + playerLives.ToString();
    }

    void Flip()
    {
        facingRight = !facingRight;
        Vector2 Scaler = transform.localScale;
        Scaler.x = Scaler.x * -1;
        transform.localScale = Scaler;
    }
}