using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//シーン管理に使用
using UnityEngine.SceneManagement;

// 環境光のモード定義のため利用
using UnityEngine.Rendering;

[DefaultExecutionOrder(-5)]//スクリプト実行順
public class GameStageSetting : MonoBehaviour
{
    AudioSource audioSource;
    public AudioClip yarinaosiSE;
    //マウス操作かキー操作か
    public bool MouseMode;
    //ライト強さ
    public float LightStrong = 0.3f;
    //リセット能力及び画面外用
    static public bool ResetFlag;
    static public int ResetStageDiv;
    public bool ResetStatus;
    //プレイヤーの能力変更メニュー開くために必要な時間。０以上であること
    public float AbilityMenuTime;
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
    public int NowStageTimeLimit;
    //ストームエリア加速度設定
    public float StormForce; //100で移動力と拮抗
    //各種エフェクト
    public GameObject Explotion;
    public GameObject Warpload;
    public GameObject Warpgeat;
    //リロード用
    public static int ReLoadPlayerPos;
    //ステージクリアからリザルトへ飛ぶときの待ち時間
    public int ClearLoadWaitTime;
    public int GameOverLoadWaitTime;


  
    //デバッグ用
    public bool DGoalFlag;
    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        ResetStatus = ResetFlag;
        if (ResetFlag)
        {
            //やり直し音声を書いておく
            audioSource.PlayOneShot(yarinaosiSE);
        }    
        ResetFlag = false;
        //Destroy(GameObject.Find("Directional Light"));//デフォルト名の環境光を消去
        RenderSetting_Gradient();//環境光設定
        Ability();//能力設定
        Setting();

        switch (SceneManager.GetActiveScene().name)
        {
            case "Stage1":
                NowStageTimeLimit = Stage1TimeLimit;
                this.transform.gameObject.AddComponent<Stage1TimeManager>().LimitTime(Stage1TimeLimit, SceneManager.GetActiveScene().name);
                break;
            case "Stage2":
                NowStageTimeLimit = Stage2TimeLimit;
                this.transform.gameObject.AddComponent<Stage2TimeManager>().LimitTime(Stage2TimeLimit, SceneManager.GetActiveScene().name);
                break;
            case "Stage3":
                NowStageTimeLimit = Stage3TimeLimit;
                this.transform.gameObject.AddComponent<Stage3TimeManager>().LimitTime(Stage3TimeLimit, SceneManager.GetActiveScene().name);
                break;
            case "Sample_TeamA":
                NowStageTimeLimit = 200;
                this.transform.gameObject.AddComponent<SamPleTimeManager>().LimitTime(NowStageTimeLimit, SceneManager.GetActiveScene().name);
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
        if (GameObject.Find("StormEria"))
        {
            GameObject Storm = GameObject.Find("StormEria");
            Storm.transform.GetComponent<StormScript>().StormForce = StormForce;
        }
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
        RenderSettings.ambientEquatorColor = Color.HSVToRGB(0.05f, 0.3f, LightStrong, false);
        // 環境光の下方向からの色を指定する
        RenderSettings.ambientGroundColor = Color.HSVToRGB(0.05f, 0.3f, LightStrong, false);

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
        if (!this.gameObject.GetComponent<RayAbility>())
            this.gameObject.AddComponent<RayAbility>();
        this.gameObject.GetComponent<RayAbility>().AbilityChengeMenuTime = AbilityMenuTime;      
        this.gameObject.GetComponent<RayAbility>().AbilityNum = 4;
        
        
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
    public void ClearTimeStop()

    {
        switch (SceneManager.GetActiveScene().name)
        {
            case "Sample_TeamA":
                this.transform.gameObject.GetComponent<SamPleTimeManager>().TimeCountEnd();
                break;
            case "Stage1":
                this.transform.gameObject.GetComponent<Stage1TimeManager>().TimeCountEnd();
                break;
            case "Stage2":
                this.transform.gameObject.GetComponent<Stage2TimeManager>().TimeCountEnd();
                break;
            case "Stage3":
                this.transform.gameObject.GetComponent<Stage3TimeManager>().TimeCountEnd();
                break;
        }
    }
    public void ClearLoad()

    {
        StartCoroutine("LoadScene"); 
    }


    void GameOverLoad() {
        StartCoroutine("LoadSceneGameOver");
    }



    protected IEnumerator LoadScene()
    {
        var async = SceneManager.LoadSceneAsync("Result");
        
        async.allowSceneActivation = false;   
        yield return new WaitForSeconds(ClearLoadWaitTime);
        async.allowSceneActivation = true;
    }



    protected IEnumerator LoadSceneGameOver()
    {
        var async = SceneManager.LoadSceneAsync("Title");

        async.allowSceneActivation = false;
        yield return new WaitForSeconds(GameOverLoadWaitTime);
        async.allowSceneActivation = true;
    }

    /*
    void DGoal()
    {
        GameObject.Find("GameObjectMaker").gameObject.GetComponent<StegeMakerScript>().StageMaker1();
        GameObject.Find("GameObjectMaker").gameObject.GetComponent<StegeMakerScript>().StageMaker2();
        GameObject.Find("GameObjectMaker").gameObject.GetComponent<StegeMakerScript>().StageMaker3();
        GameObject Picture = GameObject.Find("picture");
        Picture.transform.position = new Vector3(765, 1180, 0);
        Picture.transform.rotation = new Quaternion(-0.3f, 0.7f, 0.3f, 0.7f);
    }
    */


    public void GAMEOVER()
    {
        ClearTimeStop();
        GameOverLoad();
        GameObject Player = GameObject.FindGameObjectWithTag("Player");
        Player.GetComponent<PlayerController>().enabled = false;
        Destroy(GameObject.Find("footCanvas"));
        GameObject cam = GameObject.Find("FPSCamera");
        Destroy(cam.GetComponent<FPSCameraController>());
        cam.AddComponent<GameOverCam>();
        GameObject.Find("UICanvas").transform.GetChild(12).GetChild(0).gameObject.SetActive(false);
        GameObject.Find("UICanvas").GetComponent<Canvas>().enabled = false;
        gameObject.GetComponent<CursorColtroll>().enabled = false;
        gameObject.GetComponent<RayAbility>().enabled = false;
        Destroy(GameObject.Find("CursolCanvas"));
    }



}
