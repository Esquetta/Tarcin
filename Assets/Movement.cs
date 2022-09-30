using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Movement : MonoBehaviour
{
    public Sprite[] waitanim;
    public Sprite[] jumpanim;
    public Sprite[] runanim;

    int WaitAnimCd=0;
    int MoveAnimCd = 0;
    public Text LifeText;
    public Text youdied;
    public Image EndScreen;
   int Life = 100;

    


    float EndScreenTimer=0;
    float horizontal = 0;
    float WaitAnimTime = 0;
    float MoveAnimTime = 0;
    float BackToMainMenu = 0;

    Rigidbody2D Rigidbody2D;
    SpriteRenderer SpriteRenderer;


    Vector3 vec3;
    Vector3 FirstCamPos;
    Vector3 LastCamPost;
    bool NoDoubleJump = true;
    GameObject Cam;
    
    void Start()
    {
        Time.timeScale = 1;
        SpriteRenderer = GetComponent<SpriteRenderer>();
        Rigidbody2D = GetComponent<Rigidbody2D>();
        Cam = GameObject.FindGameObjectWithTag("MainCamera");
        if (SceneManager.GetActiveScene().buildIndex>PlayerPrefs.GetInt("Level"))
        {
            PlayerPrefs.SetInt("Level", SceneManager.GetActiveScene().buildIndex);
        }
        FirstCamPos = Cam.transform.position - transform.position;
        LifeText.text = "LIFE: " + Life;
        youdied.enabled = true;
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (NoDoubleJump)
            {
                Rigidbody2D.AddForce(new Vector2(0,550));
                NoDoubleJump = false;
            }
            
        }
    }
    void LateUpdate()
    {
        GetCameraForTarcın();
    }
    void OnCollisionEnter2D(Collision2D collision2D)
    {
        NoDoubleJump = true;
    }
    void OnTriggerEnter2D(Collider2D collider2D)
    {
        if (collider2D.gameObject.tag == "Bullet")
        {
            Life -= 5;
            LifeText.text = "LIFE: " + Life;
        }
        if (collider2D.gameObject.tag == "Enemy")
        {
            Life =0;
            LifeText.text = "LIFE: " + Life;
        }
        if (collider2D.gameObject.tag == "Saw")
        {
            Life -= 10;
            LifeText.text = "LIFE: " + Life;
        }
        if (collider2D.gameObject.tag=="Portal")
        {
            if (SceneManager.GetActiveScene().buildIndex<=2)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            }
            else
            {
                SceneManager.LoadScene(0);
            }
            
        }
        if (collider2D.gameObject.tag == "Chest")
        {
            Life += 5;
            LifeText.text = "LIFE: " + Life;
            collider2D.GetComponent<BoxCollider2D>().enabled = false;
            collider2D.GetComponent<LifeBox>().enabled = true;
            Destroy(collider2D.gameObject,1);
        }
        if (collider2D.gameObject.tag=="DeathArea")
        {
            Life = 0;
        }
    }




    void FixedUpdate()
    {
        Animation();
        Hareket();
        if (Life <= 0)
        {
            Time.timeScale = 0.5f;
            LifeText.enabled = false;
            EndScreenTimer += 0.03f;
            EndScreen.color = new Color(0,0,0,EndScreenTimer);
            youdied.color = new Color(255,0,0,Time.deltaTime*20);
            BackToMainMenu += Time.deltaTime;
            if (BackToMainMenu>1)
            {
                SceneManager.LoadScene("MainMenu");
            }
        }
    }

    void Hareket()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        vec3 = new Vector3(horizontal * 10, Rigidbody2D.velocity.y,0);
        Rigidbody2D.velocity = vec3;
    }

    void Animation()
    {
        if (NoDoubleJump==true)
        {
            if (horizontal == 0)
            {

                Idle();
            }
            else if (horizontal > 0)
            {
                runright();
            }
            else if (horizontal < 0)
            {
                runLeft();

            }
        }
        else
        {
            jump();
        }
    
    }

    void runright()
    {
        MoveAnimTime += Time.deltaTime;
        if (MoveAnimTime > 0.01f)
        {
            SpriteRenderer.sprite = runanim[MoveAnimCd++];
            if (MoveAnimCd == runanim.Length)
            {
                MoveAnimCd = 0;
            }
            MoveAnimTime = 0;
        }
        transform.localScale = new Vector3(1, 1, 1);
    }
    void runLeft()
    {
        MoveAnimTime += Time.deltaTime;

        if (MoveAnimTime > 0.01f)
        {
            SpriteRenderer.sprite = runanim[MoveAnimCd++];
            if (MoveAnimCd == runanim.Length)
            {
                MoveAnimCd = 0;
            }
            MoveAnimTime = 0;
        }
        transform.localScale = new Vector3(-1, 1, 1);
    }
    void Idle()
    {
        WaitAnimTime += Time.deltaTime;
        if (WaitAnimTime > 0.05f)
        {
            SpriteRenderer.sprite = waitanim[WaitAnimCd++];
            if (WaitAnimCd == waitanim.Length)
            {
                WaitAnimCd = 0;
            }
            WaitAnimTime = 0;
        }
    }
    void jump()
    {

        if (Rigidbody2D.velocity.y > 0)
        {
            SpriteRenderer.sprite = jumpanim[0];
        }
        else
        {
            if (horizontal<0)
            {
                transform.localScale = new Vector3(-1,1,1);
                SpriteRenderer.sprite = jumpanim[1];
            }
            else if (horizontal>0)
            {
                transform.localScale = new Vector3(1, 1, 1);
                SpriteRenderer.sprite = jumpanim[1];
            }
            
        }
        
    }

    void GetCameraForTarcın()
    {
        LastCamPost = FirstCamPos + transform.position;
        Cam.transform.position = Vector3.Lerp(Cam.transform.position,LastCamPost,0.01f);
    }


}
