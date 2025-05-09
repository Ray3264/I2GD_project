using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Random = UnityEngine.Random;

public class QTESystem : MonoBehaviour
{
    public GameObject DisplayBox;
    public GameObject PassBox;
    public int QTEGen;
    public int WaintinfForKey;
    public int CorrectKey;
    public int CountingDown;
    
    public GameObject qteUI; 
    private bool isActive = false;

    private void Awake()
    {
        qteUI.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            isActive = !isActive;
            qteUI.SetActive(isActive);
        }
        
        if (WaintinfForKey == 0)
        {
            QTEGen = Random.Range(1, 4);
            CountingDown = 1;
            StartCoroutine(CountDown());
            if (QTEGen == 1)
            {
                WaintinfForKey = 1;
                DisplayBox.GetComponent<TMP_Text>().text = "[E]";
            }
            if (QTEGen == 2)
            {
                WaintinfForKey = 2;
                DisplayBox.GetComponent<TMP_Text>().text = "[R]";
            }
            if (QTEGen == 3)
            {
                WaintinfForKey = 3;
                DisplayBox.GetComponent<TMP_Text>().text = "[T]";
            }
        }

        if (QTEGen == 1)
        {
            if (Input.anyKeyDown)
            {
                if (Input.GetButtonDown("Ekey"))
                {
                    CorrectKey = 1;
                    StartCoroutine(KeyPressing());
                } 
                else
                {
                    CorrectKey = 2;
                    StartCoroutine(KeyPressing());
                }
            }
        }
        if (QTEGen == 2)
        {
            if (Input.anyKeyDown)
            {
                if (Input.GetButtonDown("Rkey"))
                {
                    CorrectKey = 1;
                    StartCoroutine(KeyPressing());
                } 
                else
                {
                    CorrectKey = 2;
                    StartCoroutine(KeyPressing());
                }
            }
        }
        if (QTEGen == 3)
        {
            if (Input.anyKeyDown)
            {
                if (Input.GetButtonDown("Tkey"))
                {
                    CorrectKey = 1;
                    StartCoroutine(KeyPressing());
                } 
                else
                {
                    CorrectKey = 2;
                    StartCoroutine(KeyPressing());
                }
            }
        }
    }

    IEnumerator KeyPressing()
    {
        QTEGen = 4;
        if (CorrectKey == 1)
        {
            CountingDown = 2;
            PassBox.GetComponent<TMP_Text>().text = "PASS!";
            PlayerAnxiety.Instance.ReduceAnxiety(15f);
            yield return new WaitForSeconds(1.5f);
            CorrectKey = 0;
            PassBox.GetComponent<TMP_Text>().text = "";
            DisplayBox.GetComponent<TMP_Text>().text = "";
            yield return new WaitForSeconds(1.5f);
            WaintinfForKey = 0;
            CountingDown = 1;
        }

        if (CorrectKey == 2)
        {
            CountingDown = 2;
            PassBox.GetComponent<TMP_Text>().text = "FAIL!";
            PlayerAnxiety.Instance.IncreaseAnxiety(5f);
            yield return new WaitForSeconds(1.5f);
            CorrectKey = 0;
            PassBox.GetComponent<TMP_Text>().text = "";
            DisplayBox.GetComponent<TMP_Text>().text = "";
            yield return new WaitForSeconds(1.5f);
            WaintinfForKey = 0;
            CountingDown = 1;
        }
    }

    IEnumerator CountDown()
    {
        yield return new WaitForSeconds(350f * Time.deltaTime);
        if (CountingDown == 1)
        {
            QTEGen = 4;
            CountingDown = 2;
            PassBox.GetComponent<TMP_Text>().text = "FAIL!";
            yield return new WaitForSeconds(1.5f);
            CorrectKey = 0;
            PassBox.GetComponent<TMP_Text>().text = "";
            DisplayBox.GetComponent<TMP_Text>().text = "";
            yield return new WaitForSeconds(1.5f);
            WaintinfForKey = 0;
            CountingDown = 1;
        }
    }
}
