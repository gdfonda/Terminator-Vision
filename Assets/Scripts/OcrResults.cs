using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace CognitiveServices
{
    [Serializable]
    public class OcrResults
    {
        public string language;
        public string orientation;
        public double? textAngle;
        public Region[] regions;
    }

  
    [Serializable]
    public class Region
    {

        public string boundingBox;
        public Line[] lines;
    }
    [Serializable]
    public class Line
    {

        public string boundingBox;
        public Word[] words;
    }
    [Serializable]
    public class Word
    {
        public string boundingBox;
        public string text;
    }
    [Serializable]
    public class Rectangle
    {

        public int height;
        public int left;
        public int top;
        public int width;

    }
}

