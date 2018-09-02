using UnityEngine;
using System.Collections;
using InControl;
using UnityEngine.SceneManagement;


public class PlayerController : MonoBehaviour {

    public float speed;             //Floating point variable to store the player's movement speed.
    public Vector2 jumpHeight = new Vector2(0, 1600);
    public int playerNumber;
    private Rigidbody2D rb2d;       //Store a reference to the Rigidbody2D component required to use 2D Physics.
    public float maxSpeed = 20;
    bool grounded = false;
    public float jumpForce = 1600f;
    public Transform groundCheck;
    float groundRadius = 0.2f;
    public LayerMask whatIsGround;
    private Animator anim;
    public GameObject hat;
    public GameObject sick;
    public GameObject healthy;
    public SpriteRenderer healthBar;
    public float health =100f;
    public float maxHealth = 100f;
    public bool isSick = false;
	public AudioClip ouchSound;

	public AudioSource mysource;
    
	// Use this for initialization
	void Start()
	{
		rb2d = GetComponent<Rigidbody2D> ();
        anim = GetComponent<Animator>();
		mysource.playOnAwake = false;
		mysource.clip = ouchSound;
	}

	//FixedUpdate is called at a fixed interval and is independent of frame rate. Put physics code here.
	void FixedUpdate(){
		grounded = Physics2D.OverlapCircle(groundCheck.position, groundRadius, whatIsGround);
		var inputDevice = InputManager.Devices[playerNumber];
		if (inputDevice != null) 
		{
			if (grounded && Input.GetKeyDown (KeyCode.Space) || grounded && inputDevice.Action1.WasPressed) {  //makes player jump
				rb2d.AddForce(new Vector2(0, jumpForce));
			}
			float moveHorizontal = inputDevice.LeftStickX;
			if (moveHorizontal != 0) {
				rb2d.velocity = new Vector2 (moveHorizontal * maxSpeed, rb2d.velocity.y);
              
            }

            anim.SetFloat("Speed", Mathf.Abs(rb2d.velocity.x));

        }

	}
    
    public void setInfected(bool infected)
    {
        
        isSick = infected;
        if (infected)
        {
            healthy.SetActive(false);
            sick.SetActive(true);
            GameController.instance.infectados++;
        }
        else
        {
            healthy.SetActive(true);
            sick.SetActive(false);
        }

        if (GameController.instance.infectados == 4)
        {
            LoadByIndex(4);
        }
    }

    public void LoadByIndex(int sceneIndex)
    {
        SceneManager.LoadScene(sceneIndex);
    }

    void Update()
    {
        
         healthBar.color = Color.green;

        if (health>50){
            healthBar.color = Color.green;
        }else if(health>30){
            healthBar.color = Color.yellow;
        }
        else
        {
            healthBar.color = Color.red;
        }
        healthBar.transform.localScale =  new Vector3 (health / maxHealth, 1, 1);
        
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
		
        if (collision.gameObject.tag=="Player")
        {
			
            PlayerController otherPlayer = collision.gameObject.GetComponent<PlayerController>();
            if (otherPlayer.isSick)
            {
				
                if (health>0)
                {
					mysource.PlayOneShot(ouchSound);
                    health -= 10;
                    //Debug.Log("Sano " + playerNumber + " Infectado " + otherPlayer.playerNumber);
                    if (health == 0)
                    {
                        setInfected(true);
						maxSpeed = 25;
						isSick = true;
                    }
                }
                
            }



            
        }  
    }
}
