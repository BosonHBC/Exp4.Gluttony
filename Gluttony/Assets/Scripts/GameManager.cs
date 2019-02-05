using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System;
public class GameManager : MonoBehaviour
{

    public static GameManager instance;
    private void Awake()
    {
        if (instance != this || instance == null)
            instance = this;
    }

    public bool bGameOver;
    [SerializeField]
    private Text endText;

    [SerializeField]
    private Text currentWeightText;

    [SerializeField]
    private BodyController bCtrl;

    [SerializeField]Transform dataParent;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {

            Restart();
        }
        float currentWeight = bCtrl.fOverallMass;
        currentWeight = (int)(currentWeight * 10);

        currentWeightText.text = currentWeight/10f + "KG";
    }

    public void Restart()
    {
        endText.text = "";
        bGameOver = false;
        SceneManager.LoadScene(0);
    }
    public void GameOver()
    {
        endText.text = "YOU STARVED";
        float[] _data = bCtrl.GetData();
        dataParent.parent.gameObject.SetActive(true);
        for (int i = 0; i < dataParent.childCount; i++)
        {
            float data = _data[i];
            data = (int)(data * 10);
            dataParent.GetChild(i).GetComponent<Text>().text = (data / 10f).ToString() + "KG";
        }
        
    }
}
