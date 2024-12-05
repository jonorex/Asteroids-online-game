using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class GameController : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject AsterMed1, AsterSmall1;

    public GameObject explosionBuff, x3Buff, rocketBuff;

    public GameObject gameOverImg, pausePainel;
    public Text scoreGM;

    public Text scoreText, textWave, gameResult;

    private int score;

    Dictionary<string, GameObject> dict = new Dictionary<string, GameObject>();

    public static GameController instance;

    public bool isRewarded = false; 

    public bool isLoaded;

    public float mapRadius = 50f;



    private void Awake()
    {
        instance = this;
        Time.timeScale = 1;

    }
    void Start()
    {
        AdsManager.Instance.rewardedAdsButton.LoadAd();
        dict.Add("AsterMed1", AsterMed1);
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(isRewarded);
    }

    public void AsteroidsChildGenerator(Vector3 position, Quaternion rotate, string tag, bool color)
    {

        float w;
        GameObject go;
        GameObject go1;
        Vector3 newP;

        switch (tag)
        {
            case "AsterBig1":
                go = Instantiate(AsterMed1);
                go.transform.position = position;
                go.transform.rotation = rotate;
                //Destroy(go, 5f);

                go1 = Instantiate(AsterMed1);
                //float width = go1.GetComponent<MeshRenderer>().bounds.size.x;
                //float a = go1.transform.position.x + width;
                w = go1.transform.GetComponent<PolygonCollider2D>().bounds.size.x;
                newP = new Vector3(go1.transform.position.x + w, go1.transform.position.y, go1.transform.position.z);
                go1.transform.rotation = rotate;
                go1.transform.position = newP;
                //spawnAsteroids.instance.countAsteroids+=2;
                //Destroy(go1, 5f);
                if (color)
                {
                    go.GetComponent<SpriteRenderer>().color = new Color(1f, 0f, 0f, 1f);
                    go1.GetComponent<SpriteRenderer>().color = new Color(1f, 0f, 0f, 1f);
                }
                break;
            case "AsterMed1":
                go = Instantiate(AsterSmall1, position, rotate);
                go.transform.position = position;
                go.transform.rotation = rotate;
                //Destroy(go, 5f);

                go1 = Instantiate(AsterSmall1, position, rotate);
                //float width = go1.GetComponent<MeshRenderer>().bounds.size.x;
                //float a = go1.transform.position.x + width;
                w = go1.transform.GetComponent<PolygonCollider2D>().bounds.size.x;
                newP = new Vector3(go1.transform.position.x + w, go1.transform.position.y, go1.transform.position.z);
                go1.transform.rotation = rotate;
                go1.transform.position = newP;
                //spawnAsteroids.instance.countAsteroids+=2;
                if (color)
                {
                    go.GetComponent<SpriteRenderer>().color = new Color(1f, 0f, 0f, 1f);
                    go1.GetComponent<SpriteRenderer>().color = new Color(1f, 0f, 0f, 1f);
                }
                break;


        }



    }

    public void GenereateBuff(Vector3 position)
    {
        int r = UnityEngine.Random.Range(0, 100);
        GameObject go = new GameObject();
        Vector3 newPos = new Vector3(position.x, position.y + 2, position.z);

        if (r > 80)
        {
            go = Instantiate(explosionBuff);
        }
        else if (r > 50)
        {
            go = Instantiate(x3Buff);
        }
        else
        {
            go = Instantiate(rocketBuff);
        }
        go.transform.position = newPos;

        Destroy(go, 6f);
    }

    public void SumScore(string tag)
    {

        switch (tag)
        {
            case "AsterSmall1":
                score += 10;
                break;
            case "AsterMed1":
                score += 50;
                break;
            case "AsterBig1":
                score += 100;
                break;
            case "Enemy":
                score += 150;
                break;
        }
        scoreText.text = score.ToString();
    }

    public void SumScoreTcp(int scoreInc)
    {
        score += scoreInc;
    }


    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void ShowGameOver(string result)
    {
        PlayerController.instance.gameObject.layer = 6;
        if (PlayerPrefs.GetInt("score") < score)
        {
            PlayerPrefs.SetInt("score", score);
        }

        gameOverImg.SetActive(true);

        //RectTransform gOver =  gameOverImg.transform as RectTransform;
        //gOver.SetAsFirstSibling();
        scoreGM.text = score.ToString();
        gameResult.text = result;
        Time.timeScale = 0;
        StartCoroutine(EndGhoast());
        //AdsManager.Instance.rewardedAdsButton.ShowAd();

    }

    public IEnumerator EndGhoast()
    {
        yield return new WaitForSeconds(4f);
        ShipAnimatorController.Instance.GhoastMode();
        PlayerController.instance.gameObject.layer = 8;
    }

    public void GotoHome() {
        SceneManager.LoadSceneAsync(0);
    }

    public void ShowAds()
    {
        //AdsManager.Instance.rewardedAdsButton.ShowAd();
    }

    public void UpdateWave(int w)
    {
        textWave.text = w.ToString();
    }
}
