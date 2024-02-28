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

    public UnityEvent OnHitObstacle;
    public UnityEvent OnGetPoint;
    public UnityEvent OnJump;

    private Rigidbody2D rBody;
    private int points;
    // Start is called before the first frame update
    void Start()
    {
        rBody = GetComponent<Rigidbody2D>();

        // unpause game
        Time.timeScale = 1;
    }

    // Update is called once per frame
    void Update()
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
        points++;

        // update the points counter in the UI
        pointsCounter.text = points.ToString();

        OnGetPoint?.Invoke();
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(0);
    }
}
