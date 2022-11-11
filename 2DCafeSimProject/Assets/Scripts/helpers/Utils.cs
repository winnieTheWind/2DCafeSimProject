/* 
    ------------------- Code Monkey -------------------

    Thank you for downloading the Code Monkey Utilities
    I hope you find them useful in your projects
    If you have any questions use the contact form
    Cheers!

               unitycodemonkey.com
    --------------------------------------------------
 */

using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using TMPro;

namespace Game.Utils
{

    /*
     * Various assorted utilities functions
     * */
    public static class Utils
    {

        public const int sortingOrderDefault = 5000;

        // Get Sorting order to set SpriteRenderer sortingOrder, higher position = lower sortingOrder
        public static int GetSortingOrder(Vector3 position, int offset, int baseSortingOrder = sortingOrderDefault)
        {
            return (int)(baseSortingOrder - position.y) + offset;
        }

        // Create Text in the World
        public static TextMeshPro CreateWorldText(string text, Transform parent = null, Vector3 localPosition = default(Vector3), int fontSize = 40, Color? color = null, TextAnchor textAnchor = TextAnchor.UpperLeft, TextAlignment textAlignment = TextAlignment.Left, int sortingOrder = sortingOrderDefault, Font font = null)
        {
            if (color == null) color = Color.white;
            return CreateWorldText(parent, text, localPosition, fontSize, (Color)color, textAnchor, textAlignment, sortingOrder, font);
        }

        // Create Text in the World
        public static TextMeshPro CreateWorldText(Transform parent, string text, Vector3 localPosition, int fontSize, Color color, TextAnchor textAnchor, TextAlignment textAlignment, int sortingOrder, Font font)
        {
            GameObject gameObject = new GameObject("World_Text", typeof(TextMeshPro));
        
            Transform transform = gameObject.transform;
            transform.SetParent(parent, false);
            transform.localPosition = localPosition;
            
            TextMeshPro textMesh = gameObject.GetComponent<TextMeshPro>();
            textMesh.alignment = TextAlignmentOptions.Center;
            textMesh.text = text;
            textMesh.fontSize = fontSize;
            
            textMesh.color = color;
            textMesh.GetComponent<MeshRenderer>().sortingOrder = sortingOrder;
            return textMesh;
        }

    }

}