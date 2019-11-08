using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//シーン管理に使用
using UnityEngine.SceneManagement;

// 環境光のモード定義のため利用
using UnityEngine.Rendering;


public class GameStageSetting : MonoBehaviour
{
    //リセット能力及び画面外用
    static public bool ResetFlag = false;
    public bool ResetStatus;
    //プレイヤーの能力変更メニュー開くために必要な時間。０以上であること
    public float PlayerAbilityChengeMenuTime;
    //破壊能力及びリセット能力ペナルティー数。（秒）
    public int BreakAbilityPenalty; //仮数10
    public int ResetAbilityPenalty; //仮数10
    //プレイヤー設定
    public float PlayerSizeX, PlayerSizeY, PlayerSizeZ;  //仮数50,150,50
    //プレイヤー速度設定
    public float MoveSpeed;//仮数10
    public float JampForce;//仮数145
    //各ステージ制限時間
    public int Stage1TimeLimit;
    public int Stage2TimeLimit;
    public int Stage3TimeLimit;
    //ストームエリア加速度設定
    public float StormForce; //100で移動力と拮抗
    //各種エフェクト
    public GameObject Explotion;
    public GameObject Warpload;
    public GameObject Warpgeat;

    public static int ReLoadPlayerPos;
    void Start()
    {
        ResetStatus = ResetFlag;
        ResetFlag = false;
        //Destroy(GameObject.Find("Directional Light"));//デフォルト名の環境光を消去
        RenderSetting_Gradient();//環境光設定
        Ability();//能力設定
        Setting();
        switch (SceneManager.GetActiveScene().name)
        {
            case "Stage1":
                this.transform.gameObject.AddComponent<Stage1TimeManager>().LimitTime(Stage1TimeLimit);
                break;
            case "Stage2":
                this.transform.gameObject.AddComponent<Stage2TimeManager>().LimitTime(Stage2TimeLimit);
                break;
            case "Stage3":
                this.transform.gameObject.AddComponent<Stage3TimeManager>().LimitTime(Stage3TimeLimit);
                break;
        }
    }

    void Setting()
    {
        transform.gameObject.GetComponent<RayAbility>().BreakAbilityPenaltyTime = BreakAbilityPenalty;
        transform.gameObject.GetComponent<RayAbility>().ResetAbilityPenaltyTime = ResetAbilityPenalty;
        gameObject.GetComponent<RayAbility>().exploision = Explotion;
        gameObject.GetComponent<RayAbility>().WarpLoad = Warpload;
        gameObject.GetComponent<RayAbility>().WarpGeat = Warpgeat;

        GameObject Player = GameObject.FindGameObjectWithTag("Player");
        Player.gameObject.transform.localScale = new Vector3(PlayerSizeX, PlayerSizeY, PlayerSizeZ);
        Player.gameObject.GetComponent<PlayerController>().moveSpeed = MoveSpeed;
        Player.gameObject.GetComponent<PlayerController>().JumpForce = JampForce;
        GameObject Storm = GameObject.Find("StormEria");
        Storm.transform.GetComponent<StormScript>().StormForce = StormForce;
    }

    public void RenderSetting_Gradient()
    {
        //スカイボックスを消去している
        RenderSettings.skybox = null;
        // 環境光のライティング設定
        // ソースをFlatに変更する
        RenderSettings.ambientMode = AmbientMode.Trilight;
        // 環境光の上方向からの色を指定する
        RenderSettings.ambientSkyColor = Color.black;
        // 環境光の横方向からの色を指定する
        RenderSettings.ambientEquatorColor = Color.black;
        // 環境光の下方向からの色を指定する
        RenderSettings.ambientGroundColor = Color.black;
        //RenderSettings.ambientGroundColor = Color.HSVToRGB(1,10,100,false);//デバッグ用。ちょっと赤にするとき

        // 環境光の反射設定
        // ソースをCustomに変更する
        RenderSettings.defaultReflectionMode = DefaultReflectionMode.Custom;
        // 反射に利用するキューブマップの指定を外す
        RenderSettings.customReflection = null;
        // 反射の回数を1に設定する
        RenderSettings.reflectionBounces = 1;
    }

    void Ability()
    {
        this.gameObject.AddComponent<RayAbility>().AbilityChengeMenuTime = PlayerAbilityChengeMenuTime;
        if (SceneManager.GetActiveScene().name == "Stage1")
        {
            this.gameObject.GetComponent<RayAbility>().AbilityNum = 0;
        }
        else
        {
            this.gameObject.GetComponent<RayAbility>().AbilityNum = 4;
        }
        
    }

    public void ReStartScene()
    {
        ResetFlag = ResetStatus;
        switch (SceneManager.GetActiveScene().name)
        {
            case "Sample_TeamA":
                SceneManager.LoadScene("Sample_TeamA");
                break;
            case "Stage1":
                SceneManager.LoadScene("Stage1");
                break;
            case "Stage2":
                SceneManager.LoadScene("Stage2");
                break;
            case "Stage3":
                SceneManager.LoadScene("Stage3");
                break;
        }
    }
}
