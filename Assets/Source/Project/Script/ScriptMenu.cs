using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;

public class ScriptMenu : ScriptGeneric
{
    private int cre;
    public Text totalcre;

    public Text hpLv;
    public Text spLv;
    public Text atLv;
    public Text disLv;

    public Text hpPrice;
    public Text spPrice;
    public Text atPrice;
    public Text disPrice;

    private int priceHP;
    private int priceSP;
    private int priceAT;
    private int priceDIS;

    private int lvHP;
    private int lvSP;
    private int lvAT;
    private int lvDIS;

    private GameObject load;
    public GameObject canvas;
    public GameObject btnGo;
    public EventSystem es;

    public AudioSource erro;
    public AudioSource up;

    void Awake()
    {
        load = GameObject.Find("Load");

        cre = PlayerPrefs.GetInt("CRE", 0);
        if(cre > 100000)
            cre = 10000;
        PlayerPrefs.SetInt("CRE", cre);
        totalcre.text = "CRE:"+cre.ToString();

        lvHP = PlayerPrefs.GetInt("HP", 0);
        lvSP = PlayerPrefs.GetInt("SP", 0);
        lvAT = PlayerPrefs.GetInt("AT", 0);

        UpdateText(lvHP, hpLv, hpPrice);
        UpdateText(lvSP, spLv, spPrice);
        UpdateText(lvAT, atLv, atPrice);

        priceHP = 110 * (1 + PlayerPrefs.GetInt("HP", 0));
        priceSP = 110 * (1 + PlayerPrefs.GetInt("SP", 0));
        priceAT = 110 * (1 + PlayerPrefs.GetInt("AT", 0));

        disPrice.text = 500.ToString();
        disLv.text = "";
        priceDIS = 500;
        lvDIS = 0;

        PlayerPrefs.SetInt("DIS", 0);

        Invoke("ActiveCanvas", 2.5f);
    }

    private void ActiveCanvas()
    {
        canvas.SetActive(true);
        es.SetSelectedGameObject(btnGo);
    }

    public void Go()
    {
        up.Play();
        canvas.SetActive(false);
        load.GetComponent<ScriptLoad>().FadeOut("Game");
    }

    public void Out()
    {
        erro.Play();
        canvas.SetActive(false);
        load.GetComponent<ScriptLoad>().FadeOut("Splash");
    }

    public void HPUP()
    {
        if(cre >= priceHP && lvHP != 3)
        {
            cre -= priceHP;
            priceHP += 110;
            ++lvHP;

            UpdateText(lvHP, hpLv, hpPrice);
            totalcre.text = "CRE:" + cre;

            PlayerPrefs.SetInt("CRE", cre);
            PlayerPrefs.SetInt("HP", lvHP);

            up.Play();
        }
        else
            erro.Play();

    }

    public void SPUP()
    {
        if (cre >= priceSP && lvSP != 3)
        {
            cre -= priceSP;
            priceSP += 110;
            ++lvSP;

            UpdateText(lvSP, spLv, spPrice);
            totalcre.text = "CRE:" + cre;

            PlayerPrefs.SetInt("CRE", cre);
            PlayerPrefs.SetInt("SP", lvSP);
            
            up.Play();
        }
        else
            erro.Play();
    }

    public void ATUP()
    {
        if (cre >= priceAT && lvAT != 3)
        {
            cre -= priceAT;
            priceAT += 110;
            ++lvAT;

            UpdateText(lvAT, atLv, atPrice);
            totalcre.text = "CRE:" + cre;

            PlayerPrefs.SetInt("CRE", cre);
            PlayerPrefs.SetInt("AT", lvAT);

            up.Play();
        }
        else
            erro.Play();
    }

    public void DISUP()
    {
        if (cre >= priceDIS && lvDIS != 2)
        {
            cre -= priceDIS;
            disPrice.text = priceDIS.ToString();
            disLv.text = "!";
            ++lvDIS;

            if (lvDIS == 2)
            {
                disPrice.text = "";
                disLv.text = "!!";
            }
            totalcre.text = "CRE:" + cre;

            PlayerPrefs.SetInt("CRE", cre);
            PlayerPrefs.SetInt("DIS", lvDIS);

            up.Play();
        }
        else
            erro.Play();
    }

    public void UpdateText(int lv, Text text, Text price)
    {
        string textLv = "";
        string textPrice = (110 * (1 + lv)).ToString();
        switch(lv)
        {
            case 1:
                textLv = "M";
                break;
            case 2:
                textLv = "MA";
                break;
            case 3:
                textLv = "MAX";
                textPrice = "";
                break;
            case 0:
                textLv = "";
                break;
        }
        text.text = textLv;
        price.text = textPrice;
    }
}
