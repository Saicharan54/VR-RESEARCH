using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FindOppo : MonoBehaviour
{
    public GameObject objFind;
    public Button btnStart;

    public float m_ShowTime = 0.3f;

    float m_tmpTIme;
    float ftmptime;

    bool bFind;

    public int m_NameIndex = 0;
    int m_AvatarIndex = 0;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(bFind)
        {
            if(ftmptime < 0)
            {
                while(MainManager.instance.m_YourData.m_Name == MainManager.instance.m_OppoNameList[m_NameIndex])
                {
                    m_NameIndex = Random.RandomRange(0, MainManager.instance.m_OppoNameList.Count);

                    //m_NameIndex++;
                    //if(m_NameIndex >= MainManager.instance.m_OppoNameList.Count)
                    //{
                    //    m_NameIndex = 0;
                    //}
                }

                MainManager.instance.m_OppoData.m_Name = MainManager.instance.m_OppoNameList[m_NameIndex];
                MainManager.instance.m_OppoData.m_Avatar = m_AvatarIndex;

                MainManager.instance.DisplayData();

                objFind.SetActive(false);
                btnStart.gameObject.SetActive(true);

                bFind = false;
            }
            else
            {
                if(m_tmpTIme < 0)
                {
                    ShowRandomAvatar();
                    m_tmpTIme = m_ShowTime;
                }
                else
                {
                    m_tmpTIme -= Time.deltaTime;
                }

                ftmptime -= Time.deltaTime;
            }
        }
    }

    private void OnEnable()
    {
        objFind.SetActive(true);
        btnStart.gameObject.SetActive(false);

        ftmptime = MainManager.instance.m_FindTIme;
        bFind = true;
        m_tmpTIme = m_ShowTime;

        MainManager.instance.m_YourData.m_Cal = 0;
        MainManager.instance.m_OppoData.m_Cal = 0;

        MainManager.instance.txtOppoName1.text = "";
    }

    private void OnDisable()
    {
        objFind.SetActive(true);
        btnStart.gameObject.SetActive(false);

        ftmptime = MainManager.instance.m_FindTIme;
        bFind = false;
        m_NameIndex = 0;
        m_AvatarIndex = 0;
        m_tmpTIme = m_ShowTime;

        MainManager.instance.txtOppoName1.text = "";
    }

    public void ShowRandomAvatar()
    {
        m_AvatarIndex = Random.RandomRange(0, MainManager.instance.m_AvatarList.Count);
        m_NameIndex = Random.RandomRange(0, MainManager.instance.m_OppoNameList.Count);

        MainManager.instance.m_OppoData.m_Name = MainManager.instance.m_OppoNameList[m_NameIndex];
        MainManager.instance.m_OppoData.m_Avatar = m_AvatarIndex;

        MainManager.instance.imgOppo.sprite = MainManager.instance.m_AvatarList[m_AvatarIndex];
    }
}
