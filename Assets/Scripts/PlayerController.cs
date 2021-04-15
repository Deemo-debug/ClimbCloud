using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{

    Rigidbody2D rigid2D;
    Animator animator;
    float jumpForce = 700.0f;
    float walkForce = 30.0f;
    float maxWalkSpeed = 2.0f;
    float threshold = 0.2f;

    // Start is called before the first frame update
    void Start()
    {
        this.rigid2D = GetComponent<Rigidbody2D>();
        this.animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && this.rigid2D.velocity.y == 0)  //手机触碰控制
        //if (Input.GetKeyDown(KeyCode.Space) && this.rigid2D.velocity.y == 0)  //电脑触碰控制
        {
            this.rigid2D.AddForce(transform.up * this.jumpForce);
            animator.SetTrigger("JumpTrigger");
        }

        if(this.rigid2D.velocity.y != 0)
        {
            this.animator.speed = 1.0f;
        }

        int key = 0;

        if(Input.acceleration.x > this.threshold)  //手机移动
        //if(Input.GetKey(KeyCode.RightArrow))  //电脑移动
        {
            key = 1;
        }
        if(Input.acceleration.x < this.threshold)  //手机移动
        //if(Input.GetKey(KeyCode.LeftArrow))  //电脑移动
        {
            key = -1;
        }

        float speedx = Mathf.Abs(this.rigid2D.velocity.x);

        if(speedx < this.maxWalkSpeed)
        {
            this.rigid2D.AddForce(transform.right * key * this.walkForce);
        }

        if(key != 0)
        {
            transform.localScale = new Vector3(key * 0.1716968f, 0.1704724f, 1);
        }

        if(this.rigid2D.velocity.y == 0)
        {
            this.animator.speed = speedx / 2.0f;
        }
        else
        {
            this.animator.speed = 1.0f;
        }

        if(transform.position.y < -10)
        {
            SceneManager.LoadScene("GameScene");
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("到达目的地");
        SceneManager.LoadScene("GameOverScene");
    }
}
