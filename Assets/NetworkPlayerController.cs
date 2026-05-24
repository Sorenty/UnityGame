using UnityEngine;
using Mirror;

public class NetworkPlayerController : NetworkBehaviour
{
    public float speed = 1000f;
    public float jumpForce = 15f;

    private Rigidbody2D rb;
    private Animator anim;
    private Camera playerCamera;
    private bool isGrounded;

    [SyncVar]
    private string animState = "Stoit";

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

        // Берём камеру из дочерних объектов, даже если она выключена
        playerCamera = GetComponentInChildren<Camera>(true);

        if (playerCamera != null)
            playerCamera.gameObject.SetActive(false);
    }

    public override void OnStartLocalPlayer()
    {
        base.OnStartLocalPlayer();

        Debug.Log("OnStartLocalPlayer: local player spawned");

        if (playerCamera != null)
            playerCamera.gameObject.SetActive(true);

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public override void OnStartClient()
    {
        base.OnStartClient();

        if (!isLocalPlayer && playerCamera != null)
            playerCamera.gameObject.SetActive(false);
    }

    void Update()
    {
        // Для чужих игроков просто проигрываем то, что пришло по сети
        if (!isLocalPlayer)
        {
            UpdateAnim();
            return;
        }

        float move = Input.GetAxis("Horizontal");

        Vector2 vel = rb.linearVelocity;
        vel.x = move * speed * 10f;
        rb.linearVelocity = vel;

        string state;

        if (move > 0f)
        {
            transform.localScale = new Vector3(1f, 1f, 1f);
            state = "Vpravo";
        }
        else if (move < 0f)
        {
            transform.localScale = new Vector3(1f, 1f, 1f);
            state = "Vlevo";
        }
        else
        {
            state = "Stoit";
        }

        if (state != animState)
        {
            CmdSetAnimState(state);
        }

        UpdateAnim();

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.AddForce(Vector2.up * jumpForce * 2f, ForceMode2D.Impulse);
        }
    }

    [Command]
    void CmdSetAnimState(string state)
    {
        animState = state;
    }

    void UpdateAnim()
    {
        if (anim == null) return;

        anim.Play(animState);
    }

    void OnCollisionStay2D(Collision2D coll)
    {
        if (coll.gameObject.CompareTag("Map"))
            isGrounded = true;
    }

    void OnCollisionExit2D(Collision2D coll)
    {
        if (coll.gameObject.CompareTag("Map"))
            isGrounded = false;
    }
}