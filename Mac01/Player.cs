using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
	//前に進むスピード
	public float speed = 5.0f;
	//横に進むスピード
	public float slideSpeed = 2.0f;

	//アニメーション
	Animator animator;
	//UIを管理するスクリプト
	UIManager uiscript;
	//上半身のコライダー用
	GameObject headCollider;

	Rigidbody rig;


	void Start()
	{
		//変数に必要なデータを格納
		animator = GetComponent<Animator>();
		uiscript = GameObject.Find("Canvas").GetComponent<UIManager>();
		rig = GetComponent<Rigidbody>();
		headCollider = GameObject.Find("HeadCollider");

	}



	void Update()
	{
		//前に進む
		transform.position += new Vector3(0, 0, speed) * Time.deltaTime;

		//現在のX軸の位置を取得
		float posX = transform.position.x;

		//右アローキーを押した時
		if(Input.GetKey(KeyCode.RightArrow))
        {
			if(posX < 2.0f)
            {
				//右に進む
				transform.position += new Vector3(slideSpeed, 0, 0) * Time.deltaTime;
            }
        }

		//左アローキーを押した時
		if (Input.GetKey(KeyCode.LeftArrow))
		{
			if (posX > -2.0f)
			{
				//左に進む
				transform.position -= new Vector3(slideSpeed, 0, 0) * Time.deltaTime;
			}
		}

		//アニメーション


		//現在再生されているアニメーション情報を取得
		var stateInfo = animator.GetCurrentAnimatorStateInfo(0);
		//取得したアニメーションの名前が一致指定ればtrue
		bool isJump = stateInfo.IsName("Base Layer.Jump");
		bool isSlide = stateInfo.IsName("Base Layer.Slide");

		//ジャンプ


		//スライディングしていたら頭の判定をなくす
		

		//落下時のGameOver判定
		

	}

	// Triggerである障害物にぶつかったとき
	void OnTriggerEnter(Collider colider)
	{
		//ゴールした時
		if (colider.gameObject.tag == "Goal")
		{
			//速度を0にして進むのを止める
			speed = 0;

			//横移動する速度を0にして左右移動できなくする
			slideSpeed = 0;

			//ゴールをしたら正面を向くようにする
			Vector3 lockpos = Camera.main.transform.position;
			lockpos.y = transform.position.y;
			transform.LookAt(lockpos);

			//アニメーション
			animator.SetBool("Win", true);

			//UIの表示
			uiscript.Goal();
			
		}
	}


    //Triggerでない障害物にぶつかったとき
    void OnCollisionEnter(Collision collision)
    {
        //ぶつかったゲームオブジェクトの「タグ」が「Barrier」だったら
		if(collision.gameObject.tag == "Barrier")
        {
			//速度を0にして進むのを止める
			speed = 0;

			//横移動する速度を0にして左右移動できなくする
			slideSpeed = 0;

			//アニメーション
			animator.SetBool("Dead", true);
			//UIの表示
			uiscript.Gameover();
        }
    }
}
