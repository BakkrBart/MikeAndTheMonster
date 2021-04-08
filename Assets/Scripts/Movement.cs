using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public float moveSpeed = 2f;
    private float horizontalMove = 0f;
    private float move;
    [Range(0, .3f)] [SerializeField] private float movementSmoothing = .05f;

    public enum StateEnum { playing, cutscene }
    private StateEnum state;

    private Vector3 velocity = Vector3.zero;

    Rigidbody2D rigidbody2D;
    Animator animator;
    public DialogueTrigger dialogueTrigger;

    private bool m_FacingRight = true;
    public bool triggered = false;
    public bool started = false;

    void Awake()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {        
        CheckState();
    }

    void CheckState()
    {
        switch (state)
        {
            case StateEnum.playing: PlayingBehaviour(); break;
            case StateEnum.cutscene: CutsceneBehaviour(); break;
        }
    }

    private void PlayingBehaviour()
    {
        checkInput();
        movement();

        if (triggered)
        {
            state = (StateEnum.cutscene);
        }
    }

    private void CutsceneBehaviour()
    {
        checkInput();
        StartCutscene();
    }

    void movement()
    {
        animator.SetFloat("Speed", Mathf.Abs(horizontalMove));
        move = horizontalMove * Time.fixedDeltaTime;
        Vector3 targetVelocity = new Vector2(move * 10f, rigidbody2D.velocity.y);
        rigidbody2D.velocity = Vector3.SmoothDamp(rigidbody2D.velocity, targetVelocity, ref velocity, movementSmoothing);

        if (move > 0 && !m_FacingRight)
        {
            Flip();
        }
        else if (move < 0 && m_FacingRight)
        {
            Flip();
        }
    }

    void Flip()
    {
        m_FacingRight = !m_FacingRight;

        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }

    void checkInput()
    {
        horizontalMove = Input.GetAxisRaw("Horizontal") * moveSpeed;
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        Debug.Log("trigger");
        triggered = true;
    }

    void StartCutscene()
    {
        if (!started)
        {
            dialogueTrigger.TriggerDialogue();
            started = true;
        }
        else
        {
            return;
        }
    }
}
