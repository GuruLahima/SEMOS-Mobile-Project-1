using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Rigidbody2D))]
public class BirdController : MonoBehaviour
{
    public GameObject GameOverScreen;
    public float jumpForce;
    public TextMeshProUGUI pointsCounter;
    public TextMeshProUGUI PreviousHighscoreCounter;
    public Transform birdSprite;
    public float maxRot;
    public float minRot;
    public float velocityOffset;
    public float maxVelocity;
    public float spriteRotationFactor;

    public UnityEvent OnHitObstacle;
    public UnityEvent OnGetPoint;
    public UnityEvent OnJump;

    private Rigidbody2D rBody;
    private int points;
    public int Points
    {
        get
        {
            return points;
        }
        set
        {
            if (value > 0)
            {
                points = value;

                // update highscore if necessary
                if (points > previousHighscore)
                {
                    PlayerPrefs.SetInt("HighScore", points);
                }

                // play sound
                OnGetPoint?.Invoke();

                pointsCounter.text = points.ToString();
            }
            else
            {
                Debug.LogWarning("Points should never be negative");
            }
        }
    }
    private int previousHighscore;


    // Start is called before the first frame update
    void Start()
    {
        int someInt = Points;
        rBody = GetComponent<Rigidbody2D>();

        // unpause game
        Time.timeScale = 1;

        previousHighscore = PlayerPrefs.GetInt("HighScore", 0);
        PreviousHighscoreCounter.text = previousHighscore.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        Jump();
        RotateSprite();
    }

    void Jump()
    {
        // when player clicks the left mouse button
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("bird jumped");
            // make the bird jump
            rBody.velocity = Vector2.zero;
            rBody.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);

            OnJump?.Invoke();
        }
    }

    void RotateSprite()
    {

        // Quaternion maxRotation = Quaternion.Euler(0, 0, maxRot);
        // Quaternion minRotation = Quaternion.Euler(0, 0, minRot);
        // birdSprite.localRotation = Quaternion.Lerp(maxRotation, minRotation, Mathf.Abs(rBody.velocity.y - velocityOffset) / maxVelocity);
        // Debug.Log("rBody.velocity.y " + rBody.velocity.y);

        birdSprite.localRotation = Quaternion.Euler(0, 0, rBody.velocity.y * spriteRotationFactor);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {

        // pause the game
        Time.timeScale = 0;

        // show game over screen
        GameOverScreen.SetActive(true);

        OnHitObstacle?.Invoke();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Points++;
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(0);
    }
}
