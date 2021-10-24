using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{

    private Rigidbody2D rd2d;
    public float speed;
    public Text score;
    public Text lives;
    public GameObject winTextObject;
    public GameObject loseTextObject;
    public AudioClip musicClipOne;
    public AudioClip musicClipTwo;
    public AudioSource musicSource;
    private int scoreValue = 0;
    private int livesValue = 3;

    // Start is called before the first frame update
    void Start()
    {
        rd2d = GetComponent<Rigidbody2D>();
        winTextObject.SetActive(false);
        loseTextObject.SetActive(false);
        score.text = "Score: " + scoreValue.ToString();
        lives.text = "Lives: " + livesValue.ToString();  
        musicSource.clip = musicClipOne;
        musicSource.Play();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float hozMovement = Input.GetAxis("Horizontal");
        float verMovement = Input.GetAxis("Vertical");

        rd2d.AddForce(new Vector2(hozMovement * speed, verMovement * speed));

        if (Input.GetKey("escape"))
        {
            Application.Quit();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.tag == "Coin")
        {
            scoreValue += 1;
            score.text = "Score: " + scoreValue.ToString();
            Destroy(collision.collider.gameObject);
            if (scoreValue == 4)
            {
                transform.position = new Vector3(33.0f, 1.0f, 0f);
                livesValue = 3;
                lives.text = "Lives: " + livesValue.ToString();
            }
            if (scoreValue == 8)
            {
                winTextObject.SetActive(true);
                musicSource.Pause();
                
                musicSource.clip = musicClipTwo;
                musicSource.Play();
            }
        }
        else if(collision.collider.tag == "Enemy")
        {
            livesValue = livesValue - 1;
            lives.text = "Lives: " + livesValue.ToString();
            Destroy(collision.collider.gameObject);
        }
        else if(livesValue == 0)
        {
            loseTextObject.SetActive(true);
            Destroy(gameObject);
        }

    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if(collision.collider.tag == "Ground")
        {
            if(Input.GetKey(KeyCode.W))
            {
                rd2d.AddForce(new Vector2(0, 4), ForceMode2D.Impulse);
            }
        }
    }

}
