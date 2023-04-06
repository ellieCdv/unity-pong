using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Ball : MonoBehaviour
{

    public Rigidbody2D rigidBody2D;
    public float speed;
    public Vector2 velocity;
    public bool canPressSpace;
    public int leftPlayerScore = 0;
    public int rightPlayerScore = 0;
    public TextMeshProUGUI leftPlayerText;
    public TextMeshProUGUI rightPlayerText;

    void Start()
    {
        canPressSpace = true;
        rigidBody2D = GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.Space) && (canPressSpace || rigidBody2D.velocity == Vector2.zero))
        {
            ResetAndSetRandomVelocity();
            canPressSpace = false;
        }
    }
    private void ResetBall()
    {
        rigidBody2D.velocity = Vector2.zero;
        transform.position = Vector2.zero;
    }
    private void ResetAndSetRandomVelocity()
    {
        ResetBall();
        rigidBody2D.velocity = GenerateRandomVector2WOut0(true) * speed;
        velocity = rigidBody2D.velocity;
    }
    private Vector2 GenerateRandomVector2WOut0(bool returnNormalized)
    {
        Vector2 newRandomVector = new Vector2();
        bool shouldXBeLessThen0 = Random.Range(0, 100) % 2 == 0;
        newRandomVector.x = shouldXBeLessThen0 ? Random.Range(-.8f, -0.1f) : Random.Range(.1f, 0.8f);
        bool shouldYBeLessThen0 = Random.Range(0, 100) % 2 == 0;
        newRandomVector.y = shouldYBeLessThen0 ? Random.Range(-.8f, -0.1f) : Random.Range(.1f, 0.8f);
        return returnNormalized ? newRandomVector.normalized : newRandomVector;
    }
    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        rigidBody2D.velocity = Vector2.Reflect(velocity, collision.contacts[0].normal);
        velocity = rigidBody2D.velocity;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (transform.position.x > 0)
        {
            leftPlayerScore += 1;
            leftPlayerText.text = leftPlayerScore.ToString();
            //Debug.Log(leftPlayerScore + " : " + rightPlayerScore);
        } else if (transform.position.x < 0)
        {
            // Debug.Log("Right Pl +1");
            rightPlayerScore += 1;
            rightPlayerText.text = rightPlayerScore.ToString();
            //Debug.Log(leftPlayerScore + " : " + rightPlayerScore);
        }
        canPressSpace = true;
        ResetBall();
    }
}
