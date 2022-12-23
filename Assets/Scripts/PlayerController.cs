using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public Rigidbody2D RBObject;
    public float moveSpeed;

    public Animator myAnim;

    public static PlayerController instance;

    public string areaTransitionName;
    private Vector3 bottomLeftLimit;
    private Vector3 topRightLimit;

    public bool canMove = true;

    // Start is called before the first frame update
    void Start()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            if (instance != this)
                Destroy(gameObject);
            else
            {
                if (SceneManager.GetActiveScene().name == "Countryside")
                    this.transform.position = new Vector3(1.5f, -4.5f, 0);
            }     
        }
        
        DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        if (canMove)
        {
            RBObject.velocity = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")) * moveSpeed;

        }
        else
        {
            RBObject.velocity = Vector2.zero;
        }

        myAnim.SetFloat("moveX", RBObject.velocity.x);
        myAnim.SetFloat("moveY", RBObject.velocity.y);

        if (Input.GetAxisRaw("Horizontal") == 1 || Input.GetAxisRaw("Horizontal") == -1 || Input.GetAxisRaw("Vertical") == 1 || Input.GetAxisRaw("Vertical") == -1)
        {
            if (canMove)
            {
                myAnim.SetFloat("lastMoveX", Input.GetAxisRaw("Horizontal"));
                myAnim.SetFloat("lastMoveY", Input.GetAxisRaw("Vertical"));
            }
        }

        transform.position = new Vector3(Mathf.Clamp(transform.position.x, bottomLeftLimit.x, topRightLimit.x),
                                        Mathf.Clamp(transform.position.y, bottomLeftLimit.y, topRightLimit.y),
                                        transform.position.z);
    }

    public void SetBounds(Vector3 botLeft, Vector3 topRight)
    {
        bottomLeftLimit = botLeft + new Vector3(.5f, .5f, 0f);
        topRightLimit = topRight + new Vector3(-.5f, -.5f, 0f);
    }
}
