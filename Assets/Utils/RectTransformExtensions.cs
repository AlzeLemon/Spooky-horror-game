using UnityEngine;


// Da, am creat o clasa de extensii pentru ca ma dispera modul in care se lucreaza cu UI in Unity
// Da, sintaxa asta este parte din C#
// Da, am fost si eu la fel de socat sa aflu
public static class MyUIExtensions
{
    public static RectTransform GetRectTransform(this GameObject obj)
    {
        return obj.GetComponent<RectTransform>();
    }

    public static void SetX(this RectTransform rect, float x)
    {
        rect.anchoredPosition = new Vector2(x, rect.anchoredPosition.y);
    }

    public static void SetY(this RectTransform rect, float y)
    {
        rect.anchoredPosition = new Vector2(rect.anchoredPosition.x, y);
    }

    public static void SetLeft(this RectTransform rect, float left)
    {
        rect.offsetMin = new Vector2(left, rect.offsetMin.y);
    }

    public static void SetRight(this RectTransform rect, float right)
    {
        rect.offsetMax = new Vector2(-right, rect.offsetMax.y);
    }
}
