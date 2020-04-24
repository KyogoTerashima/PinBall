using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FripperController : MonoBehaviour {

    //HingeJointのコンポーネントを入れる
    private HingeJoint myHingeJoint;

    //初期の傾き
    private float defaultAngle = 20;
    // 弾いた時の傾き
    private float flickAngle = -20;

    //タッチクラスのインスタンス作成
    private Touch touch = new Touch();


	// Use this for initialization
	void Start () {
        //HingeJointコンポーネントの取得
        this.myHingeJoint = GetComponent<HingeJoint>();

        //フリッパーの傾きを設定
        SetAngle(this.defaultAngle);

        
	}

    private int leftid = -1;
    private int rightid = -1;

    // Update is called once per frame
    void Update()
    {

        //左矢印キーを押したとき左フリッパーを動かす
        if (Input.GetKeyDown(KeyCode.LeftArrow) && tag == "LeftFripperTag")
        {
            SetAngle(this.flickAngle);
        }

        //右矢印キーを押したとき右フリッパーを動かす
        if (Input.GetKeyDown(KeyCode.RightArrow) && tag == "RightFripperTag")
        {
            SetAngle(this.flickAngle);
        }

        //矢印キーを離した時左フリッパーを元に戻す
        if (Input.GetKeyUp(KeyCode.LeftArrow) && tag == "LeftFripperTag")
        {
            SetAngle(this.defaultAngle);
        }

        //矢印キーを離した時右フリッパーを元に戻す
        if (Input.GetKeyUp(KeyCode.RightArrow) && tag == "RightFripperTag")
        {
            SetAngle(this.defaultAngle);
        }

        //タッチしたとき
     //  if (OnTouchDown())
     //  {
     //
     //      SetAngle(this.flickAngle);
     //      Debug.Log("タッチされました");
     //  }
     //  else
     //  {
     //
     //      SetAngle(this.defaultAngle);
     //      Debug.Log("タッチされていません。");
     //  }

        for (int i = 0; i < Input.touchCount; i++)
        {
            //タッチ情報をコピー
            Touch t = Input.GetTouch(i);
            if (t.phase == TouchPhase.Began)
            {
                // タッチしたとき
                Input.GetTouch(i);
                // タッチした座標に応じて左右のフリッパーを上下する
                if(t.position.x < Screen.width / 2.0f)
                {
                    if(tag == "LeftFripperTag" && leftid == -1)
                    {
                        leftid = t.fingerId;
                        SetAngle(this.flickAngle);
                    }
                }
                else
                {
                    if(tag == "RightFripperTag" && rightid == -1)
                    {
                        rightid = t.fingerId;
                        SetAngle(this.flickAngle);
                    }
                }
            }
            else if (t.phase == TouchPhase.Ended)
            {
                // 離した時
                if(t.fingerId == leftid && tag == "LeftFripperTag")
                {
                    leftid = -1;
                    SetAngle(this.defaultAngle);
                }else if(t.fingerId == rightid && tag == "RightFripperTag")
                {
                    SetAngle(this.defaultAngle);
                    rightid = -1;
                }
            }


        }
    }

    public void SetAngle (float angle)
    {
        JointSpring jointSpr = this.myHingeJoint.spring;
        jointSpr.targetPosition = angle;
        this.myHingeJoint.spring = jointSpr;

    }

    bool OnTouchDown()
    {
        //タッチされている時
        if( 0 < Input.touchCount)
        {
            //タッチされている指の数だけ処理
            for(int i = 0; i < Input.touchCount; i++)
            {
                //タッチ情報をコピー
                Touch t = Input.GetTouch(i);
                //タッチした位置からRayを飛ばす
                Ray ray = Camera.main.ScreenPointToRay(t.position);
                RaycastHit hit = new RaycastHit();
                if(Physics.Raycast(ray, out hit))
                {
                    //Rayを飛ばしてあたったオブジェクトが自分自身だったら
                    if(hit.collider.gameObject == this.gameObject)
                    {
                        return true;
                    }
                }
            }

        }
        return false;
    }

}
