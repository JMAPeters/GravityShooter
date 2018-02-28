using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public Rigidbody2D rbPlayer;
    public float jumpSpeed;
    public float moveSpeed;
    public float maxSpeed;
    bool onPlanet;
    public float spaceDrag = 0.01f;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && onPlanet)
        {
            var jumpDirection = Quaternion.AngleAxis(transform.eulerAngles.z, Vector3.forward) * Vector3.up;
            Vector2 jumpForce = jumpDirection.normalized * jumpSpeed * 1000 * Time.deltaTime;
            rbPlayer.AddForce(jumpForce);
        }

        if (Input.GetKey(KeyCode.A))
        {
            var moveDirection = Quaternion.AngleAxis(transform.eulerAngles.z, Vector3.forward) * new Vector3(-1,-1,0); //naar links en omlaag
            Vector2 moveForce = moveDirection.normalized * moveSpeed * 1000 * spaceDrag * Time.deltaTime;
            rbPlayer.angularVelocity = 0;

            if (rbPlayer.velocity.magnitude < maxSpeed)
            {
                rbPlayer.AddForce(moveForce);
            }
        }
        else
        if (Input.GetKey(KeyCode.D))
        {
            var moveDirection = Quaternion.AngleAxis(transform.eulerAngles.z, Vector3.forward) * new Vector3(1,-1,0); //naar rechts en omlaag
            Vector2 moveForce = moveDirection.normalized * moveSpeed * 1000 * spaceDrag * Time.deltaTime;
            rbPlayer.angularVelocity = 0;

            if (rbPlayer.velocity.magnitude < maxSpeed)
            {
                rbPlayer.AddForce(moveForce);
            }
        }
        else
            if (onPlanet)
                    rbPlayer.angularVelocity = 0;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        onPlanet = true;
        spaceDrag = 1f;
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        onPlanet = false;
        spaceDrag = 0.01f;
    }
}
