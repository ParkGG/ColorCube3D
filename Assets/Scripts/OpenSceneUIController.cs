using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class OpenSceneUIController : MonoBehaviour
{
    [Header("Main")]
    public Text highScoreText;
    public Text starCoinText;
    public Button startButton;
    public Button storeButton;
    public Button adsButton;
    public GameObject mainScreen;


    [Header("Store")]
    public GameObject colorPickerScreen;
    public Button applyButton;
    public Button backButton;
    public List<GameObject> colorList;


    private GameObject selectObject;
    private MyColorPicker myColorPicker;
    private Vector3 selectPos = new Vector3(11, 0, 11);
    private Vector3 originPos;


    private void Start()
    {
        highScoreText.text = ScoreManager.Instance.HighScore.ToString();

        starCoinText.text = PlayerData.Instance.StarCoin.ToString();

        startButton.onClick.AddListener( () => { SceneController.Instance.LoadScene("GameScene"); } );

        storeButton.onClick.AddListener(() => {

            mainScreen.SetActive(!mainScreen.activeSelf);
            foreach(var color in colorList)
            {
                color.SetActive(!color.activeSelf);
            }

        });

        adsButton.onClick.AddListener(() => { AdMobRewardedAd.Instance.starcoindAdShow(); });

        applyButton.onClick.AddListener(() => { if(PlayerData.Instance.StarCoin >= 100) PlayerData.Instance.StarCoin -= 100; });

        backButton.onClick.AddListener(() =>
        {
            mainScreen.SetActive(!mainScreen.activeSelf);
            colorPickerScreen.SetActive(!colorPickerScreen.activeSelf);

            if (selectObject) selectObject.transform.position = originPos;

            foreach (var color in colorList) { color.SetActive(false); }

        });

        myColorPicker = colorPickerScreen.GetComponent<MyColorPicker>();
    }


    void Update()
    {
        RaycastHit hit;

        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            Physics.Raycast(ray, out hit);

            if (hit.collider)
            {
                selectObject = hit.collider.gameObject;

                if (colorList.Contains(selectObject))
                {
                    foreach (var cube in colorList)
                        cube.SetActive(false);

                    selectObject.SetActive(true);
                    colorPickerScreen.SetActive(true);

                    originPos = selectObject.transform.position;
                    selectObject.transform.position = selectPos;

                    myColorPicker.NotifyColor(selectObject);
                }

            }
        }

        starCoinText.text = PlayerData.Instance.StarCoin.ToString();

    }





}
