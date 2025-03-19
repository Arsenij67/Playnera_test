using System.Linq;
using UnityEngine;

public class СameraScroll : MonoBehaviour
{
    public float scrollSpeed = 1f; // Скорость скроллинга
    [SerializeField] private float MinPointX = -1.7f;
    [SerializeField] private float MaxPointX = 4.3f;
    private bool canScroll = true;

    void Update()
    {
        Debug.Log(canScroll);
        float ScrollXOffset = 0;

#if UNITY_EDITOR
     ScrollXOffset = GetScrollPk().x;
#elif UNITY_ANDROID
     ScrollXOffset = GetScrollAndroid().x;
#endif

        if (canScroll) // если не передвигаем вещи, то можем скроллить
        {
            transform.position = ClampVectorX(transform.position + new Vector3(ScrollXOffset, 0, 0));
        }
       

    }

    private Vector2 GetScrollPk()
    {
        Vector2 moveDelta = Input.mousePositionDelta *scrollSpeed * Time.deltaTime;
   
        return moveDelta;
    }

    private Vector2 GetScrollAndroid()
    {
        Vector2 moveDelta = Vector2.zero;
        // Проверяем, есть ли касания
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            moveDelta = touch.deltaPosition * scrollSpeed * Time.deltaTime;
             
        }
        return moveDelta;
     
    }

    private Vector3 ClampVectorX(Vector3 coord)
    {
        float clamedX = Mathf.Clamp(coord.x, MinPointX, MaxPointX);
        coord.x = clamedX;

        return coord;
    }
 
}

