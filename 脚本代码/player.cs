using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class player : MonoBehaviour {
    private Rigidbody rd;
    private float startTime;
    private float jumpTime;
    public float factor;  //倍数
    public GameObject Stage;  //跳台
    public float Distance = 5;  //跳台距离
    private GameObject currentStage;  //当前跳台
    private Collider lastCollider;  //上次碰撞
    public Text t_gameover;
    public Button b_restart;
    public Text t_best;
    public Text t_score;
    private int score = 0;
    private static int best_score = 0;
    bool isStop = true;
	// Use this for initialization
	void Start () {
        rd = GetComponent<Rigidbody>();
        rd.centerOfMass = Vector3.zero;  //将重心置于中心
        currentStage = Stage;
        lastCollider = currentStage.GetComponent<Collider>();
        CreateStage();
        t_score.text = "分数:" + score;
        t_best.text = "最高分:" + best_score;
        b_restart.onClick.AddListener(delegate()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        });
        t_gameover.gameObject.SetActive(false);  //隐藏
        b_restart.gameObject.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
        if (isStop)
        {
            /*
            if (Input.GetKeyDown(KeyCode.Space))
            {
                startTime = Time.time;
            }
            if (Input.GetKeyUp(KeyCode.Space))
            {
                jumpTime = Time.time - startTime;
                OnJump(jumpTime);
            }
             * */
            if(Input.GetMouseButtonDown(0))
            {
                startTime = Time.time;
            }
            if(Input.GetMouseButtonUp(0))
            {
                jumpTime = Time.time - startTime;
                OnJump(jumpTime);
            }
        }
       
	}

    protected void OnJump(float jumpTime)
    {
        rd.AddForce(new Vector3(1, 1, 0) * jumpTime*factor, ForceMode.Impulse);  //瞬间作用力
    }

    void CreateStage()
    {
        var stage = Instantiate(Stage);
        stage.transform.position = currentStage.transform.position + new Vector3(Random.Range(1, Distance), 0, 0);
    }

    void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.tag=="stage"&&collision.collider!=lastCollider)
        {
            score++;
            t_score.text = "分数:"+score.ToString();
            if (score > best_score)
            {
                best_score = score;
                t_best.text = "最高分:" + score.ToString();
            }
            lastCollider = collision.collider;
            currentStage = collision.gameObject;
            CreateStage();
        }
        if(collision.collider.tag=="plane")
        {
            t_gameover.gameObject.SetActive(true);
            b_restart.gameObject.SetActive(true);
            isStop = false;
        }

    }

}
