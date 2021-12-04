using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public LayerMask whatIsGround;
    Rigidbody rb;

    public float moveSpeed;
    public float jumpForce;

    public bool grounded;

    public Animator anim;

    public bool isHurt;
    public AnimationClip hurtAnim;
    public float multiplier = 1f;

    public float immoTimerDur = 200f;
    public float immoTimer;

    float x;
    public bool askJump;

    public AnimationCurve hurtSpeedCurve;
    private float hurtSpeed;
    public Vector2 speedHurtyMinMax;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        speedHurtyMinMax.y = GameHandler.Instance.moveObjectSpd;
        // anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

        if (!isHurt)
        {
            x = Input.GetAxis("Horizontal");


            if (Input.GetKeyDown(KeyCode.Space) && grounded)
                askJump = true;

            anim.SetBool("isGrounded", grounded);

            anim.SetFloat("Velocity", x);
            anim.SetFloat("VelocityY", rb.velocity.y);

            if (immoTimer >= 0)
                immoTimer -= Time.deltaTime;
        }
    }

    public void FixedUpdate()
    {

        if (isHurt) return;

        rb.AddForce(Vector3.down * Time.deltaTime * 10);
        Vector2 velocity = new Vector2((x * moveSpeed * Time.deltaTime) - (GameHandler.Instance.moveObjectSpd * Time.deltaTime), rb.velocity.y);

        if (grounded)
            rb.velocity = velocity;
        else
        {
            rb.velocity = new Vector2((x * moveSpeed * Time.deltaTime) / 2, rb.velocity.y);
        }

        if (askJump)
        {
            Jump();
        }

        clampPlayerMovement();
    }

    IEnumerator Hurt()
    {
        //isHurt = true;
        anim.SetTrigger("Hurt");
        rb.velocity = Vector2.zero;
        hurtSpeed = speedHurtyMinMax.x;
        GameHandler.Instance.moveObjectSpd = 0;
        immoTimer = immoTimerDur;

        while (hurtSpeed < speedHurtyMinMax.y)
        {
            hurtSpeed += Time.deltaTime;
            GameHandler.Instance.moveObjectSpd = hurtSpeed;
           yield return new WaitForEndOfFrame();
        }
        //yield return new WaitForSeconds(hurtAnim.length* multiplier);
        GameHandler.Instance.moveObjectSpd = speedHurtyMinMax.y;
    }

    private void Jump()
    {
        grounded = false;
        anim.SetTrigger("Jump");
        askJump = false;
        //Add jump forces
        rb.AddForce(Vector2.up * jumpForce * 1.5f);

        //If jumping while falling, reset y velocity.
        Vector3 vel = rb.velocity;
        if (rb.velocity.y < 0.5f)
            rb.velocity = new Vector3(vel.x, 0, vel.z);
        else if (rb.velocity.y > 0)
            rb.velocity = new Vector3(vel.x, vel.y / 2, vel.z);

    }
    private void OnCollisionEnter(Collision other)
    {
        int layer = other.gameObject.layer;
        if (whatIsGround == (whatIsGround | (1 << layer)))
            grounded = true;

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Obstacle" && immoTimer <= 0)
        {
            StartCoroutine(Hurt());
        }
    }

    void clampPlayerMovement()
    {
        Vector3 position = transform.position;

        float distance = transform.position.z - Camera.main.transform.position.z;

        float leftBorder = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, distance)).x;
        float rightBorder = Camera.main.ViewportToWorldPoint(new Vector3(1, 0, distance)).x;

        position.x = Mathf.Clamp(position.x, leftBorder, rightBorder);
        transform.position = position;
    }


}
