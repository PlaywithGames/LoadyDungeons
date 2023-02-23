using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    // Reference to Instace of Remote Config
    private ApplyRemoteConfigSettings rcInstance;

    [SerializeField]
    private float m_MovementSpeed = 5.0f, m_CharacterSize = 1.0f;

    [SerializeField]
    private Animator m_AnimatorController;

    [SerializeField]
    private LayerMask m_InputCollisionLayer;

    [SerializeField] private InputReader inputReader;

    // [SerializeField]
    // private GameManager m_GameManager;

    private bool m_HasKey = false;

    private Rigidbody m_Rigidbody;

    private int m_VelocityHash = Animator.StringToHash("Velocity");

    private Camera m_MainCamera;

    const float k_MinMovementDistance = 1.2f;

    void Awake()
    {
        rcInstance = ApplyRemoteConfigSettings.Instance;
        
        SetMovementSpeed(rcInstance.characterSpeed);
        SetCharacterSize(rcInstance.characterSize);
    }

    private void OnEnable()
    {
        inputReader.onMoveVector2 += MoveToPosition;
    }

    private void OnDisable()
    {
        inputReader.onMoveVector2 -= MoveToPosition;
    }

    void Start()
    {   
        m_Rigidbody = GetComponent<Rigidbody>();
        m_MainCamera = Camera.main;
    }

    private void KeyCollected()
    {
        m_HasKey = true;

        //TODO: Put this outside of the PlayerController
        GameObject.FindObjectOfType<GameplayUI>().KeyCollected();
    }

    private void OnTriggerEnter(Collider other)
    {
        //TODO: Cache the string value
        if (other.CompareTag("Chest"))
        {
            // TODO: Maybe cache the getcomponent read, although it is only read once
            other.gameObject.GetComponent<Chest>().Open();

            KeyCollected();
        }

        if (other.CompareTag("Door"))
        {
            Debug.Log("Triggered by a door");

            if(m_HasKey)
            {
                Debug.Log("Opened the door");

                other.gameObject.GetComponent<Door>().Open();

                // TODO: Change this number to a member variable
                StartCoroutine(LevelCompleted());
            }
        }
    }

    private IEnumerator LevelCompleted()
    {
        yield return new WaitForSeconds(2.15f);

        GameManager.LevelCompleted();
    }

    private void Update()
    {
        m_AnimatorController.SetFloat(m_VelocityHash, m_Rigidbody.velocity.magnitude);
    }

    void MoveToPosition(Vector2 moveVector)
    {
        Debug.Log(moveVector);
        m_Rigidbody.transform.eulerAngles = new Vector3(0, CalulateRotation(moveVector), 0);
        Vector3 direction = new Vector3(moveVector.x, 0, moveVector.y);
        m_Rigidbody.velocity = direction * m_MovementSpeed;
    }

    public void SetMovementSpeed(float speed)
    {
        m_MovementSpeed = speed;
        Debug.Log("Movement Speed Set! " + m_MovementSpeed);
    }

    public void SetCharacterSize(float size)
    {
        m_CharacterSize = size;
        gameObject.transform.localScale = new Vector3(m_CharacterSize,m_CharacterSize,m_CharacterSize);
        Debug.Log("Local Scale Set! " + gameObject.transform.localScale);
    }

    private float CalulateRotation(Vector2 moveVector)
    {
        if (moveVector.x == 0f && moveVector.y == 0f)
        {
            return 0;
        }
        else if (moveVector.x == 0f && moveVector.y == 1.0f)
        {
            return 0;
        }
        else if (moveVector.x >= 0.7f && moveVector.y >= 0.7f)
        {
            return 45;
        }
        else if (moveVector.x == 1.0f && moveVector.y == 0)
        {
            return 90;
        }
        else if (moveVector.x >= 0.7f && moveVector.y <= -0.7f)
        {
            return 135;
        }
        else if (moveVector.x == 0 && moveVector.y == -1.0f)
        {
            return 180;
        }
        else if (moveVector.x <= -0.7f && moveVector.y <= -0.7f)
        {
            return 225;
        }
        else if (moveVector.x == -1.0f && moveVector.y == 0f)
        {
            return 270;
        }
        else if (moveVector.x <= -0.7f && moveVector.y >= 0.7f)
        {
            return 315;
        }
        return 0;
    }
}
