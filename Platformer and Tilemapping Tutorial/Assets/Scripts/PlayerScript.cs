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

    private int scoreValue = 0;

    // Start is called before the first frame update
    void Start()
    {
        rd2d = GetComponent<Rigidbody2D>();
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
                SetScoreText();
            }
                
        }

        if(collision.collider.tag == "Enemy")
        {
            playerLives -= 1;
            SetScoreText();
            Destroy(collision.collider.gameObject);
        }

    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.collider.tag == "Ground")
        {
            if (Input.GetKeyDown(KeyCode.W))
            {
                rd2d.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
            }
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
}