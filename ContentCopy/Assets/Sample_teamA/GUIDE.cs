/*シーン切り替えについて説明
* 各シーン切り替えは設置出来ましたので作業を行ってください。
* 
* Title→TitleSelect        =2秒ほど放置
* 
* TitleSelect→StageSelect  =0
* TitleSelect→Rule         =9
* TitleSelect→Credit       =8
* TitleSelect→Title        =return
* 
* Rule　   → Title　       =何らかのキー操作
* Credit   → Title　       =何らかのキー操作
* 
* StageSelect→Stage1       =0
* StageSelect→Stage2       =9
* StageSelect→Stage3       =8
* StageSelect→TitleSelect  =return
* 
* Stage1→Stage2            =StageそれぞれのCrearFlagがtrue
* Stage2→Stage3            =StageそれぞれのCrearFlagがtrue
* Stage3→Result            =StageそれぞれのCrearFlagがtrue
* 
* Result→TitleSlect        =何らかのキー操作
*/
/*
 * Sample_teamAフォルダの中の各スクリプトについて。
 * 動作させたいオブジェクトにスクリプトをつけるだけで
 * コライダーとか含めて全部自動で作ってくれます。
 * 
 * FallDown　は落下床。秒数は手動で設定してください
 * FPSCameraController　はカメラへつけてください。
 * GameStageSetting　は画面を暗くする処理です。ヒエラルキーのCreateEmptyへ付けてください。
 * MoveUpDownFloor　はエレベーターです。
 * OneWayFloorController　は一方通行の床です。
 * PlayerController　はPlayer用です。
 * rotate_horizonal は縦回転ブロック用です
 * rotate_vertical は横回転ブロック用です
 * 
 * Pendulum は振り子の動きをする足場を生成するものです。
 *  ＞バグが発生するので、角度変更はしないでください。スケール変更はYをいじらないでください。
 * 下記はアタッチする必要のないものです
 * OnOffCollide
 * ParentMesh
 * WayTrigger
 */

 //カメラfar1600