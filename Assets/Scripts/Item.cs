using System;
using UnityEngine;

public class Item : MonoBehaviour
{
    // поля
    internal bool isDragging = false;
    private Camera mainCamera;
    private Rigidbody2D itemRb;
    private RaycastHit hit;
    void Awake()
    {
        mainCamera = Camera.main; // Получаем ссылку на основную камеру
        itemRb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (isDragging)
        {
         
            // Перемещаем предмет с учетом мыши
            Vector3 mouseWorldPosition = GetMouseWorldPosition();
         
            transform.position = mouseWorldPosition;
        }
        

    }

    void OnMouseDown()
    {
        // Начало перетаскивания
        isDragging = true;
        TakeOffFloor();
    }

 
    void OnMouseUp()
    {
        // Завершение перетаскивания
        isDragging = false;

       
        if (hit.collider && hit.collider.transform.tag.Equals("Floor")) 
        {
            PutOnFloor();
        }


    }

    private Vector3 GetMouseWorldPosition()
    {
     
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
       
        if (Physics.Raycast(ray, out hit, Mathf.Infinity))
        {

            return hit.point;
        }
 
        return Vector3.zero; // Возвращаем нулевую позицию, если ничего не найдено
    }

    private void PutOnFloor()
    {
        itemRb.bodyType = RigidbodyType2D.Static; 

    }

    private void TakeOffFloor()
    {

        itemRb.bodyType = RigidbodyType2D.Dynamic;

    }


}
