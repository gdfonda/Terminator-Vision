using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace CognitiveServices {
    [Serializable]
    public class AnalysisResult
    {
        public Tag[] tags;
        public Face[] faces;

    }

    [Serializable]
    public class Tag
    {
        public double confidence;
        public string hint;
        public string name;
    }

    [Serializable]
    public class Face
    {
        public int age;
        public FaceRectangle facerectangle;
        public string gender;
    }

    [Serializable]
    public class FaceRectangle
    {
        public int height;
        public int left;
        public int top;
        public int width;
    }

    [Serializable]
    public class Adult
    {
        public double adultscore;
        public bool isadultcontent;
        public bool isracycontent;
        public double racyscore;
    }

    [Serializable]
    public class Category
    {
        public object detail;
    }

    [Serializable]
    public class Color
    {
        public string accentcolor;
        public string dominantcolorbackground;
        public string dominantcolorforeground;
        public string[] dominantcolors;
        public bool isbwimg;
    }

    [Serializable]
    public class Description
    {
        public Caption[] captions;
        public string[] tags;
    }

    [Serializable]
    public class Caption
    {
        public double confidence;
        public string text;
    }



    [Serializable]
    public class ImageType
    {
        public int cliparttype;
        public int linedrawingtype;
    }

    [Serializable]
    public class Metadata
    {
        public string format;
        public int height;
        public int width;
    }
}