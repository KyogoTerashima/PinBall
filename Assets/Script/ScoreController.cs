using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreController : MonoBehaviour {

    //smallstarのスコア
    private int smallStarSore;
    //Largestarのスコア
    private int largeStarSore;
    //smallCloudのスコア
    private int SmallCloudSore;
    //LargeCloudのスコア
    private int largeCloudSore;
    //スコアを表示するテキスト
    private GameObject scoreText = null;
    //スコアを入れる変数
    private int sum = 0;

    // Use this for initialization
    void Start () {
        //シーン中のScoreテキストを取得
        this.scoreText = GameObject.Find("Score");
		
	}
	
	// Update is called once per frame
	void Update () {
        Text score_text = this.scoreText.GetComponent<Text>();
        score_text.text = this.sum.ToString();
        Debug.Log(sum);
		
	}

    void OnCollisionEnter(Collision collision)
    {
        string yourTag = collision.gameObject.tag;

        //タグによってスコアを入れる
        if (yourTag == "SmallStarTag")
        {
            this.smallStarSore = 5;
            this.sum += this.smallStarSore;
        }
        else if (yourTag == "LargeStarTag")
        {
            this.largeStarSore = 20;
            this.sum += this.largeStarSore;
        }
        else if (yourTag == "SmallCloudTag")
        {
            this.SmallCloudSore = 5;
            this.sum += this.SmallCloudSore;
        }
        else if (yourTag == "LargeCloud")
        {
            this.largeCloudSore = 10;
            this.sum += this.largeCloudSore;
        }

    }
}
