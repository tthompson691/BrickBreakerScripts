using UnityEngine;

public class Ball : MonoBehaviour
{
    // config params
    [SerializeField] Paddle paddle1;
    [SerializeField] float xPush;
    [SerializeField] float yPush;
    [SerializeField] AudioClip[] ballSounds;
    [SerializeField] float randomFactor = 0.2f;


    //state
    Vector2 paddleToBallVector;
    bool hasStarted;
    

    // Cached component references
    AudioSource myAudioSource;
    Rigidbody2D myRigidBody2D;
    Level level;

    

    // Start is called before the first frame update
    void Start()
    {
        hasStarted = FindObjectOfType<Level>().hasStarted;
        countAllBalls();
        paddleToBallVector = transform.position - paddle1.transform.position;
        myAudioSource = GetComponent<AudioSource>();
        myRigidBody2D = GetComponent<Rigidbody2D>();

        if (hasStarted)
        {
            StartBall();
        }

    }

    private void countAllBalls()
    {
        level = FindObjectOfType<Level>();
        level.countBalls();
    }

    // Update is called once per frame
    void Update()
    {
        hasStarted = FindObjectOfType<Level>().hasStarted;
        

        if (!hasStarted)
        {
            LockBallToPaddle();
            FindObjectOfType<Level>().StartOnMouseClick();
            StartBall();
        }

    }

    private void StartBall()
    {         
            myRigidBody2D.velocity = new Vector2(xPush, yPush);
    }


    private void LockBallToPaddle()
    {
        Vector2 paddlePos = new Vector2(paddle1.transform.position.x, paddle1.transform.position.y);
        transform.position = paddlePos + paddleToBallVector;

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Vector2 velocityTweak = new Vector2
            (Random.Range(0f, randomFactor), 
            Random.Range(0f, randomFactor)); 

        if (hasStarted)
        {
            AudioClip clip = ballSounds[UnityEngine.Random.Range(0, ballSounds.Length)];
            myAudioSource.PlayOneShot(clip);
            myRigidBody2D.velocity += velocityTweak;
        }

    }
}
