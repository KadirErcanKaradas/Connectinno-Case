using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class OrderManager : MonoBehaviour
{
    public static OrderManager Instance { get; private set; }

    public UIController uiController;
    
    public List<Orders> OrdersList = new List<Orders>();
    [SerializeField] private GameObject order1,order2,order3;
    [SerializeField] private TMP_Text cookName;
    public List<GameObject> pickedVegetable = new List<GameObject>();
    public List<GameObject> order1List = new List<GameObject>();
    public List<GameObject> order2List = new List<GameObject>();
    public List<GameObject> order3List = new List<GameObject>();
    public List<GameObject> readyCook = new List<GameObject>();
    public int OrderIndex = 0;
    public bool isOrderComplate = false;
    private GameObject food;
    public int cookIndex = 0;
    public Transform coinTransform;
    private Camera cam;
    public GameObject moneyUI;
    public AudioClip orderSound,levelCompSound;
    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    private void Start()
    {
        OrderNameControl();
        cam = Camera.main;
        food = GameObject.Find("food");
    }
    private void Update()
    {
        order1.GetComponent<Image>().sprite = OrdersList[OrderIndex].order1Image;
        order1.transform.GetChild(0).GetComponent<TMP_Text>().text = OrdersList[OrderIndex].order1Count.ToString();
        order2.GetComponent<Image>().sprite = OrdersList[OrderIndex].order2Image;
        order2.transform.GetChild(0).GetComponent<TMP_Text>().text = OrdersList[OrderIndex].order2Count.ToString();
        order3.GetComponent<Image>().sprite = OrdersList[OrderIndex].order3Image;
        order3.transform.GetChild(0).GetComponent<TMP_Text>().text = OrdersList[OrderIndex].order3Count.ToString();

        cookName.text = OrdersList[OrderIndex].orderName;
    
        CurrentOrder();
        StartCoroutine(OrderReady());
    }
    public void CurrentOrder()
    {
        if (!isOrderComplate)
        {
            if (order1List.Count == OrdersList[OrderIndex].order1Count)
            {
                OrdersList[OrderIndex].order1Image.name = "";
            }
            if (order2List.Count == OrdersList[OrderIndex].order2Count)
            {
                OrdersList[OrderIndex].order2Image.name = "";
            }
            if (order3List.Count == OrdersList[OrderIndex].order3Count)
            {
                OrdersList[OrderIndex].order3Image.name = "";
            }
            if (pickedVegetable.Count == OrdersList[OrderIndex].order1Count+OrdersList[OrderIndex].order2Count+OrdersList[OrderIndex].order3Count)
            {
                for (int i = 0; i < pickedVegetable.Count-1; i++)
                {
                    pickedVegetable[i].SetActive(false);
                }
                pickedVegetable.Clear();
                isOrderComplate = true;
                StartCoroutine(Okey());
            }
        }
    }

    public IEnumerator OrderReady()
    {
        if (isOrderComplate == true)
        {
            order1List.Clear();
            order2List.Clear();   
            order3List.Clear();
            food.GetComponent<Renderer>().material.DOColor(Color.red, 1f);
            readyCook[cookIndex].GetComponent<Image>().sprite = OrdersList[cookIndex].orderImage;
            isOrderComplate = false;
            StartCoroutine(MoneyUIGo());
            yield return new WaitForSeconds(1f);
            food.GetComponent<Renderer>().material.DOColor(Color.white, 1f);
            PlayerPrefs.SetInt("Money", PlayerPrefs.GetInt("Money") + 10);
            uiController.moneyText.text = "" + PlayerPrefs.GetInt("Money");
        }
    }

    public void OrderNameControl()
    {
        OrdersList[OrderIndex].order1Image.name = OrdersList[OrderIndex].order1Name;
        OrdersList[OrderIndex].order2Image.name = OrdersList[OrderIndex].order2Name;
        OrdersList[OrderIndex].order3Image.name = OrdersList[OrderIndex].order3Name;
    }

    public IEnumerator Okey()
    {
        yield return new WaitForSeconds(1f);
        if (OrdersList.Count-1 == OrderIndex)
        {
            GameController.Instance.SetGameStage(GameStage.Win);
            SoundManager.Instance.ActiveSound(levelCompSound);
        }
        else
        {
            OrderIndex+=1;
            cookIndex++;
            OrderNameControl();
            SoundManager.Instance.ActiveSound(orderSound);
        }
    }

    public IEnumerator MoneyUIGo()
    {
        GameObject coin = ObjectPool.Instance.GetPooledObject();
        for (int i = 0; i < ObjectPool.Instance.pooledObject.Count; i++)
        {
            if (coin != null)
            {
                ObjectPool.Instance.pooledObject[i].transform.position = coinTransform.position;
                ObjectPool.Instance.pooledObject[i].SetActive(true);
                ObjectPool.Instance.pooledObject[i].transform.DOScale(Vector3.one / 10, 2f);
                ObjectPool.Instance.pooledObject[i].transform.DOMove(moneyUI.transform.position, 2f)
                    .OnComplete(() =>
                    {
                        ObjectPool.Instance.pooledObject[0].SetActive(false);
                        ObjectPool.Instance.pooledObject[1].SetActive(false);
                        ObjectPool.Instance.pooledObject[2].SetActive(false);
                        ObjectPool.Instance.pooledObject[3].SetActive(false);
                        ObjectPool.Instance.pooledObject[4].SetActive(false);
                        ObjectPool.Instance.pooledObject[0].transform.localScale = Vector3.one;
                        ObjectPool.Instance.pooledObject[1].transform.localScale = Vector3.one;
                        ObjectPool.Instance.pooledObject[2].transform.localScale = Vector3.one;
                        ObjectPool.Instance.pooledObject[3].transform.localScale = Vector3.one;
                        ObjectPool.Instance.pooledObject[4].transform.localScale = Vector3.one;
                    });
                yield return new WaitForSeconds(0.2f);
            }
        }
    }
    
}
