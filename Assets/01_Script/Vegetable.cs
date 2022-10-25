using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;
using DG.Tweening;

public class Vegetable : MonoBehaviour
{
    private const float DOUBLE_CLİCK_TIME = .2f;
    private float lastClickTime;
    private float distance = 10;
    private bool isDoubleClick = false;
    private GameObject Pan;
    public AudioClip correctClip;
    public AudioClip wrongClip;

    private void Awake()
    {
        Pan = GameObject.Find("FryingPan");
    }

    private void Update()
   {
       if (Input.GetMouseButtonDown(0))
       {
           float timeSinceLastClick = Time.time - lastClickTime;
           if (timeSinceLastClick<= DOUBLE_CLİCK_TIME)
           {
               isDoubleClick = true;
           }
           else
           {
               isDoubleClick = false;
           }
           lastClickTime = Time.time;
       }
   }

   private void OnMouseUp()
   {
       if (GameController.Instance.GameStage == GameStage.Started)
       {
           if (isDoubleClick)
           {
               if (gameObject.name ==OrderManager.Instance.OrdersList[OrderManager.Instance.OrderIndex].order1Image.name)
               {
                   DoubleClickVege();
                   OrderManager.Instance.order1List.Add(gameObject);
               }
               else if (gameObject.name == OrderManager.Instance.OrdersList[OrderManager.Instance.OrderIndex].order2Image.name)
               {
                   DoubleClickVege();
                   OrderManager.Instance.order2List.Add(gameObject);
               }
               else if (gameObject.name == OrderManager.Instance.OrdersList[OrderManager.Instance.OrderIndex].order3Image.name)
               {
                   DoubleClickVege();
                   OrderManager.Instance.order3List.Add(gameObject);
               }
               else
               {
                   Vector3 firstPos = transform.position;
                   gameObject.transform.DOScale(Vector3.one / 2, 0.5f)
                       .OnComplete(() => gameObject.transform.DOScale(Vector3.one, 0.2f));
                   transform.DOMove(GameController.Instance.panTransforms.position, 0.5f).OnComplete(() =>transform.DOMove(firstPos,0.2f) );
                   gameObject.transform.localScale = Vector3.one;
                   SoundManager.Instance.ActiveSound(wrongClip);
                   if (GameController.Instance.isVibrate == true)
                   {
                       Vibrator.Vibrate(500);
                   }
               }
           }
       }
   }
   private void OnMouseDrag()
   {
       if (GameController.Instance.GameStage == GameStage.Started)
       {
           Vector3 mousePosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, distance);
           Vector3 objPosition = Camera.main.ScreenToWorldPoint(mousePosition);

           transform.position = objPosition;
       }
   }

   public void DoubleClickVege()
   {
       gameObject.transform.DOMove(GameController.Instance.panTransforms.position, 0.5f)
           .OnComplete(() => gameObject.SetActive(false));
       gameObject.transform.DOScale(Vector3.one / 2, 0.5f);
       gameObject.transform.SetParent(Pan.transform);
       gameObject.GetComponent<Vegetable>().enabled = false;
       OrderManager.Instance.pickedVegetable.Add(gameObject);
       SoundManager.Instance.ActiveSound(correctClip);
       if (GameController.Instance.isVibrate == true)
       {
           Vibrator.Vibrate(500);
       }
   }
}
