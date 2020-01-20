using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerScript : MonoBehaviour
{
    public AudioSource musicSource;
    public AudioClip musicClipFour;
    public AudioClip musicClipFive;
    public float speed;
    public Text winText;
    public Text scoreText;
    public Text timeText;

    public GameObject player;
    public GameObject sparkle;
    public GameObject box;
    public GameObject loser;


    public static float timeLeft;
    private Rigidbody2D rd2d;
    private bool facingRight = true;
    public static int score;
    Animator anim;

    private bool gamePaused;


    void Start()
    {
        StartCoroutine(ATime(2.0f));
        rd2d = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        winText.text = "";
        gamePaused = true;
        timeLeft = 10;
        score = 0;
        SetScoreText();
    }

    IEnumerator ATime(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        box.SetActive(false);
        gamePaused = false;
    }

    void Update()
    {
        SetTimeText();
        InOrder();
        if (Input.GetKeyDown(KeyCode.D))
        {
            anim.SetInteger("State", 1);
        }

        if (Input.GetKeyUp(KeyCode.D))
        {
            anim.SetInteger("State", 0);
        }

        if (Input.GetKeyDown(KeyCode.A))
        {

            anim.SetInteger("State", 1);
        }

        if (Input.GetKeyUp(KeyCode.A))
        {
            anim.SetInteger("State", 0);
        }

        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            anim.SetInteger("State", 1);
        }

        if (Input.GetKeyUp(KeyCode.RightArrow))
        {
            anim.SetInteger("State", 0);
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            anim.SetInteger("State", 1);
        }

        if (Input.GetKeyUp(KeyCode.LeftArrow))
        {
            anim.SetInteger("State", 0);
        }

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            anim.SetInteger("State", 2);
        }

        if (Input.GetKeyUp(KeyCode.UpArrow))
        {
            anim.SetInteger("State", 0);
        }

        if (Input.GetKeyDown(KeyCode.W))
        {
            anim.SetInteger("State", 2);
        }

        if (Input.GetKeyUp(KeyCode.W))
        {
            anim.SetInteger("State", 0);
        }
    }

    void Flip()
    {
        facingRight = !facingRight;
        Vector2 Scaler = transform.localScale;
        Scaler.x = Scaler.x * -1;
        transform.localScale = Scaler;
    }

    void FixedUpdate()
    {
        float hozMovement = Input.GetAxis("Horizontal");
        float vertMovement = Input.GetAxis("Vertical");
        Vector2 movement = new Vector2(hozMovement, vertMovement).normalized;
        rd2d.velocity = movement * speed;
        if (facingRight == false && hozMovement > 0)
        {
            Flip();
        }
        else if (facingRight == true && hozMovement < 0)
        {
            Flip();
        }
        if (Input.GetKey("escape"))
        {
            Application.Quit();
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {

        if (other.gameObject.CompareTag("PickUp"))
        {
            other.gameObject.SetActive(false);
            score = score + 1;
            SetScoreText();
            musicSource.clip = musicClipFour;
            musicSource.Play();
        }
    }

    void InOrder()
    {
        if(score <= 0)
        {
            Physics2D.IgnoreLayerCollision(8, 10, true);
            Physics2D.IgnoreLayerCollision(8, 11, true);
            Physics2D.IgnoreLayerCollision(8, 12, true);
        }

        if (score >= 1)
        {
            Physics2D.IgnoreLayerCollision(8, 10, false);
        }

        if (score >=2)
        {
            Physics2D.IgnoreLayerCollision(8, 11, false);
        }

        if (score >= 3)
        {
            Physics2D.IgnoreLayerCollision(8, 12, false);
        }

    }

    void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.collider.tag == "Floor")
        {
            if (Input.GetKey(KeyCode.UpArrow))
            {
                rd2d.AddForce(new Vector2(0, 3), ForceMode2D.Impulse);
                musicSource.clip = musicClipFive;
                musicSource.Play();
            }
            if (Input.GetKey(KeyCode.W))
            {
                rd2d.AddForce(new Vector2(0, 3), ForceMode2D.Impulse);
                musicSource.clip = musicClipFive;
                musicSource.Play();
            }
            if (anim.GetCurrentAnimatorStateInfo(0).IsName("Chao-Jump"))
            {
                anim.SetInteger("State", 0);
            }
        }
    }



    void SetScoreText()
    {
        scoreText.text = "Score: " + score.ToString();
        if (score >= 4)
        {
            winText.text = "You win! Game created by Paulina Weintraub!";
            sparkle.SetActive(true);

        }
    }

    void SetTimeText()
    {
        if (score != 4)
        {
            if (gamePaused == false)
            {
                timeLeft -= Time.deltaTime;
            }
            timeText.text = "Time: " + timeLeft.ToString("F0");
            if (timeLeft < 0)
            {
                Destroy(player);
                loser.SetActive(true);
                winText.text = "You lose! Game created by Paulina Weintraub!";
            }
        }
    }

}
