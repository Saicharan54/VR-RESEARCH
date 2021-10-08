using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public enum PlayMode
{
    Ready,
    Playing,
    Pause,
    End
}



public class MainManager : MonoBehaviour
{
    public static MainManager instance;

    //Model
    public GameObject objYou;
    public GameObject objOppo;

    //UI
    public List<GameObject> PanelList;
    public GameObject objPause;

    public Text txtYourName1;
    public Text txtOppoName1;
    public Image imgYou;
    public Image imgOppo;

    public Text txtYourName2;
    public Text txtYourHr;
    public Text txtYourPr;
    public Text txtYourCal;

    public Text txtOppoName2;
    public Text txtOppoHr;
    public Text txtOppoPr;
    public Text txtOppoCal;


    public Text txtTime;

    public Image imgWin;
    public Image imgLoss;

    public List<Sprite> m_AvatarList;
    public List<string> m_OppoNameList = new List<string>();

    //public 
    public float m_TotalTimer;
    public PlayMode m_PlayMode;
    public PlayerData m_YourData;
    public PlayerData m_OppoData;

    public float m_FindTIme = 8f;
    public float m_ShowTime = 3f;

    float m_showtmp;

    //private
    float m_tmptime;
    int m_PanelIndex;


    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(this);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        Screen.sleepTimeout = SleepTimeout.NeverSleep;

        Init();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            ExitGame();
        }

        if(m_PlayMode == PlayMode.Playing)
        {
            DisplayTIme();

            if(m_tmptime <= 0)
            {
                m_PlayMode = PlayMode.End;

                objYou.SetActive(false);
                objOppo.SetActive(false);

                ShowPanel(3);
                ShowResult();
            }
            else
            {
                m_tmptime -= Time.deltaTime;
            }

            if (m_showtmp < 0)
            {
                DisplayData();
                m_showtmp = m_ShowTime;
            }
            else
            {
                m_showtmp -= Time.deltaTime;
            }

        }
        else if(m_PlayMode == PlayMode.Pause)
        {

        }
    }






    public void PlayButtonClick()
    {
        ShowPanel(1);
    }

    public void ExitButtonClick()
    {
        ExitGame();
    }

    public void StartButtonClick()
    {
        m_showtmp = m_ShowTime;

        m_tmptime = m_TotalTimer;
        m_PlayMode = PlayMode.Playing;

        objYou.SetActive(true);
        objOppo.SetActive(true);

        ShowPanel(2);
    }

    public void PauseButtonClick()
    {
        m_PlayMode = PlayMode.Pause;
        objPause.SetActive(true);
    }

    public void ResumeButtonClick()
    {
        objPause.SetActive(false);
        m_PlayMode = PlayMode.Playing;
    }

    public void EndButtonClick()
    {
        objPause.SetActive(false);
        m_PlayMode = PlayMode.End;

        objYou.SetActive(false);
        objOppo.SetActive(false);

        ShowPanel(0);
    }

    public void ReplayButtonClick()
    {
        objYou.SetActive(false);
        objOppo.SetActive(false);

        ShowPanel(1);
    }



    public void Init()
    {
        m_tmptime = 0;
        m_PlayMode = PlayMode.Ready;
        m_PanelIndex = 0;

        int m_NameIndex = Random.RandomRange(0, m_OppoNameList.Count);
        m_YourData.m_Name = m_OppoNameList[m_NameIndex];
        txtYourName1.text = m_YourData.m_Name;

        m_YourData.m_Avatar = Random.RandomRange(0, m_AvatarList.Count);
        imgYou.sprite = MainManager.instance.m_AvatarList[m_YourData.m_Avatar];

        objYou.SetActive(false);
        objOppo.SetActive(false);

        ShowPanel(0);
    }

    public void ExitGame()
    {
        Screen.sleepTimeout = SleepTimeout.SystemSetting;
        Application.Quit();
    }

    public void DisplayTIme()
    {
        int m_min = (int)(m_tmptime / 60f);
        int m_sec = (int)(m_tmptime - (float)m_min * 60f);

        txtTime.text = string.Format("{0} : {1}", m_min.ToString("00"), m_sec.ToString("00"));
    }

    public void DisplayData()
    {
        m_YourData.m_Hr = Random.RandomRange(60, 100);
        m_YourData.m_Pr = Random.RandomRange(60, 100);
        m_YourData.m_Cal += (int)(m_YourData.m_Hr * 0.5f);

        m_OppoData.m_Hr = Random.RandomRange(60, 100);
        m_OppoData.m_Pr = Random.RandomRange(60, 100);
        m_OppoData.m_Cal += (int)(m_OppoData.m_Hr * 0.5f);


        txtYourName1.text = m_YourData.m_Name;
        txtYourName2.text = m_YourData.m_Name;
        txtYourHr.text = m_YourData.m_Hr.ToString();
        txtYourPr.text = m_YourData.m_Pr.ToString();
        txtYourCal.text = m_YourData.m_Cal.ToString();

        txtOppoName1.text = m_OppoData.m_Name;
        txtOppoName2.text = m_OppoData.m_Name;
        txtOppoHr.text = m_OppoData.m_Hr.ToString();
        txtOppoPr.text = m_OppoData.m_Pr.ToString();
        txtOppoCal.text = m_OppoData.m_Cal.ToString();
    }

    public void ShowResult()
    {
        if (CompareData())
        {
            imgWin.gameObject.SetActive(true);
            imgLoss.gameObject.SetActive(false);
        }
        else
        {
            imgWin.gameObject.SetActive(false);
            imgLoss.gameObject.SetActive(true);
        }
    }

    public bool CompareData()
    {
        if (m_YourData.m_Cal > m_OppoData.m_Cal)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void ShowPanel(int index)
    {
        m_PanelIndex = index;

        for (int i = 0; i < PanelList.Count; i++)
        {
            if (m_PanelIndex == i)
            {
                PanelList[i].SetActive(true);
            }
            else
            {
                PanelList[i].SetActive(false);
            }
        }
    }
}

[System.Serializable]
public class PlayerData
{
    public string m_Name;
    public int m_Avatar;
    public int m_Hr;
    public int m_Pr;
    public int m_Cal;

    public PlayerData()
    {
        m_Name = "Guest";
        m_Avatar = 0;
        m_Hr = 0;
        m_Pr = 0;
        m_Cal = 0;
    }
}