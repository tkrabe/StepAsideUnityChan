using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnityChanController : MonoBehaviour
{
    //アニメーションするためのコンポーネントを入れる
    private Animator myAnimator;

    //Unityちゃんを移動させるコンポーネントを入れる(前進１)
    private Rigidbody myRigidbody;

    //前方向の速度(前進１)
    private float velocityZ = 16f;

    //横方向の速度（追加）
    private float velocityX = 10f;

    //上方向の速度（ジャンプに関する事）
    private float velocityY = 10f;

    //左右の移動できる範囲（追加）
    private float movableRange = 3.4f;

    // Start is called before the first frame update
    void Start()
    {
        //Animatorコンポーネントを取得
        this.myAnimator = GetComponent<Animator>();

        //走るアニメーションを開始  ("Speed", 0.2)歩く;
        this.myAnimator.SetFloat("Speed", 1);

        //Rigidbodyコンポーネントを取得（前進１）
        this.myRigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        //横方向の入力による速度（追加）
        float inputVelocityX = 0;

        //上方向の入力による速度（ジャンプに関する事）
        float inputVelocityY = 0;

        //Unityちゃんを矢印キーまたはボタンに応じて左右に移動させる（追加）
        if (Input.GetKey(KeyCode.LeftArrow) && -this.movableRange < this.transform.position.x)
        {
            //左方向への速度を代入（追加）
            inputVelocityX = -this.velocityX;
        }
        else if (Input.GetKey(KeyCode.RightArrow) && this.transform.position.x < this.movableRange)
        {
            //右方向への速度を代入（追加）
            inputVelocityX = this.velocityX;
        }

        //ジャンプしていない時にスペースが押されたらジャンプする（ジャンプに関する事）
        if (Input.GetKeyDown(KeyCode.Space) && this.transform.position.y < 0.5f)
        {
            //ジャンプアニメを再生（ジャンプに関する事）
            this.myAnimator.SetBool("Jump", true);
            //上方向への速度を代入（ジャンプに関する事）
            inputVelocityY = this.velocityY;
        }
        else
        {
            //現在のY軸の速度を代入（ジャンプに関する事）
            inputVelocityY = this.myRigidbody.velocity.y;
        }
        //Jumpステートの場合はJumpにfalseをセットする（ジャンプに関する事）
        if (this.myAnimator.GetCurrentAnimatorStateInfo(0).IsName("Jump"))
        {
            this.myAnimator.SetBool("Jump", false);
        }
        //Unityちゃんに速度を与える（前進１）
        //x要素に数字を入れると左右斜め、ｙ要素に入れると上下に進
        //(変更ｘにinputVelocityXを入力)
        //ジャンプに関する事(inputVelocityY)
        this.myRigidbody.velocity = new Vector3(inputVelocityX, inputVelocityY, velocityZ);
    }
}
