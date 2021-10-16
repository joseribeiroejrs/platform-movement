using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private Transform groundCheckTransform = null;
	[SerializeField] private LayerMask playerMask;
	private int coinLayer = 9;
    private bool jumpKeyWasPressed = false;
    private float horizontalInput;
    private Rigidbody rigidBody;

	// Start is called before the first frame update
	void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isSpaceKeyDown())
		{
            jumpKeyWasPressed = isSpaceKeyDown();
		}
        updateHorizontalInput();
    }

	private void FixedUpdate()
	{
        horizontalMove();
        if (Physics.OverlapSphere(groundCheckTransform.position, 0.1f, playerMask).Length == 0)
		{
            return;
		}
        shouldJump();
    }

	private void OnTriggerEnter(Collider other)
	{
        Debug.Log("CoinLayer: " + coinLayer);
        Debug.Log("Layer: " + other.gameObject.layer);
        if (other.gameObject.layer == coinLayer)
		{
            Destroy(other.gameObject);
		}
	}

	bool isSpaceKeyDown ()
	{
        return Input.GetKeyDown(KeyCode.Space);
	}

    void shouldJump ()
	{
        if (jumpKeyWasPressed)
		{
            rigidBody.AddForce(Vector3.up * 5, ForceMode.VelocityChange);
            jumpKeyWasPressed = false;
		}
    }

    void updateHorizontalInput()
	{
        horizontalInput = Input.GetAxis("Horizontal");
	}

    void horizontalMove()
	{
        rigidBody.velocity = new Vector3(horizontalInput * 2, rigidBody.velocity.y, 0);
	}
}
