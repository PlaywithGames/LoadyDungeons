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
        // rotation
        Vector3 direction = new Vector3(moveVector.x, 0, moveVector.y);
        m_Rigidbody.transform.LookAt(moveVector);
        // lock rotation to y 
        Vector3 eulerAngle = m_Rigidbody.transform.eulerAngles;
        m_Rigidbody.transform.eulerAngles = new Vector3(0, eulerAngle.y, 0);
        m_Rigidbody.velocity = moveVector * m_MovementSpeed;
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
}
