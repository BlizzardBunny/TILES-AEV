using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour
{
    private bool isMoving;
    private Vector3 origPos, targetPos;
    private float timeToMove;

    Animator myAnimator;

    private void Start()
    {
        myAnimator = GetComponent<Animator>();
        timeToMove = 0.2f;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.LeftShift))
        {
            timeToMove = 0.1f;
        }
        else
        {
            timeToMove = 0.2f;
        }

        if (Input.GetKey(KeyCode.W) && !isMoving)
        {
            RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.TransformDirection(Vector2.up),1f);
            myAnimator.SetBool("Upward", true);
            if (!hit)
            {                
                StartCoroutine(MovePlayer(Vector3.up));
            }            
        }
        else
        {
            myAnimator.SetBool("Upward", false);
        }
        
        if (Input.GetKey(KeyCode.A) && !isMoving)
        {
            RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.TransformDirection(Vector2.left), 1f);
            myAnimator.SetBool("Left", true);
            if (!hit)
            {                
                StartCoroutine(MovePlayer(Vector3.left));
            }
        }
        else
        {
            myAnimator.SetBool("Left", false);
        }

        if (Input.GetKey(KeyCode.S) && !isMoving)
        {
            RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.TransformDirection(Vector2.down), 1f);
            myAnimator.SetBool("Downward", true);
            if (!hit)
            {                
                StartCoroutine(MovePlayer(Vector3.down));
            }
        }
        else
        {
            myAnimator.SetBool("Downward", false);
        }

        if (Input.GetKey(KeyCode.D) && !isMoving)
        {
            RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.TransformDirection(Vector2.right), 1f);
            myAnimator.SetBool("Right", true);
            if (!hit)
            {                
                StartCoroutine(MovePlayer(Vector3.right));
            }   
        }
        else
        {
            myAnimator.SetBool("Right", false);
        }
    }

    private IEnumerator MovePlayer(Vector3 direction)
    {
        isMoving = true;

        float elapsedTime = 0;

        origPos = transform.position;
        targetPos = origPos + direction;

        while(elapsedTime < timeToMove)
        {
            transform.position = Vector3.Lerp(origPos, targetPos, (elapsedTime / timeToMove));
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.position = targetPos;

        isMoving = false;
    }
}
