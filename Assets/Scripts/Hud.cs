
using CognitiveServices;
using HoloToolkit.Unity;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.VR.WSA.WebCam;
using UnityEngine.Windows;

public class Hud : MonoBehaviour
{

    public Text InfoPanel;
    public Text AnalysisPanel;
    public Text ThreatAssessmentPanel;
    public Text DiagnosticPanel;

    PhotoCapture _photoCaptureObject = null;
    System.Threading.Timer _timer;
    IEnumerator coroutine;

    string _subscriptionKey = "< Computer Vision Key goes here !!!>";
    string _computerVisionEndpoint = "https://westus.api.cognitive.microsoft.com/vision/v1.0/analyze?visualFeatures=Tags,Faces";
    string _ocrEndpoint = "https://westus.api.cognitive.microsoft.com/vision/v1.0/ocr";

    public TextToSpeechManager textToSpeechManager;

    void Start()
    {

        if (AnalysisPanel == null || ThreatAssessmentPanel == null || InfoPanel == null)
            return;

        AnalysisPanel.text = "ANALYSIS:\n**************\ntest\ntest\ntest";
        ThreatAssessmentPanel.text = "SCAN MODE XXXXX\nINITIALIZE";
        InfoPanel.text = "CONNECTING";
        int secondsInterval = 20;
        _timer = new System.Threading.Timer(Tick, null, 0, secondsInterval * 1000);

    }

    private void Tick(object state)
    {
        AnalyzeScene();
    }


    void OnPhotoCaptureCreated(PhotoCapture captureObject)
    {
        _photoCaptureObject = captureObject;

        Resolution cameraResolution = PhotoCapture.SupportedResolutions.OrderByDescending((res) => res.width * res.height).First();

        CameraParameters c = new CameraParameters();
        c.hologramOpacity = 0.0f;
        c.cameraResolutionWidth = cameraResolution.width;
        c.cameraResolutionHeight = cameraResolution.height;
        c.pixelFormat = CapturePixelFormat.BGRA32;

        captureObject.StartPhotoModeAsync(c, OnPhotoModeStarted);

    }

    void OnStoppedPhotoMode(PhotoCapture.PhotoCaptureResult result)
    {
        _photoCaptureObject.Dispose();
        _photoCaptureObject = null;
    }

    private void OnPhotoModeStarted(PhotoCapture.PhotoCaptureResult result)
    {
        if (result.success)
        {
            string filename = string.Format(@"terminator_analysis.jpg");
            string filePath = System.IO.Path.Combine(Application.persistentDataPath, filename);

            //doing this to get formatted image
            _photoCaptureObject.TakePhotoAsync(filePath, PhotoCaptureFileOutputFormat.JPG, OnCapturedPhotoToDisk);

        }
        else
        {
            DiagnosticPanel.text = "Say: Unable to start photo mode! Hasta la vista, baby.";

        }
    }

    void OnCapturedPhotoToDisk(PhotoCapture.PhotoCaptureResult result)
    {
        if (result.success)
        {
            string filename = string.Format(@"terminator_analysis.jpg");
            string filePath = System.IO.Path.Combine(Application.persistentDataPath, filename);

            byte[] image = File.ReadAllBytes(filePath);
            GetTagsAndFaces(image);
            ReadWords(image);

        }
        else
        {
            DiagnosticPanel.text = "DIAGNOSTIC\n**************\n\nFailed to save Photo to disk.";
            InfoPanel.text = "ABORT";
        }
        _photoCaptureObject.StopPhotoModeAsync(OnStoppedPhotoMode);
    }


    // Update is called once per frame
    void Update()
    {

    }

    void AnalyzeScene()
    {
        InfoPanel.text = "CALCULATION PENDING";
        PhotoCapture.CreateAsync(false, OnPhotoCaptureCreated);
    }

    public void GetTagsAndFaces(byte[] image)
    {

        try
        {
            coroutine = RunComputerVision(image);
            StartCoroutine(coroutine);
        }
        catch (Exception)
        {

            DiagnosticPanel.text = "DIAGNOSTIC\n**************\n\nGet Tags failed.";
            InfoPanel.text = "ABORT";
        }
    }

    public void ReadWords(byte[] image)
    {

        try
        {
            coroutine = Read(image);
            StartCoroutine(coroutine);
        }
        catch (Exception)
        {

            DiagnosticPanel.text = "DIAGNOSTIC\n**************\n\nRead Words failed.";
            InfoPanel.text = "ABORT";
        }
    }

    IEnumerator RunComputerVision(byte[] image)
    {
        var headers = new Dictionary<string, string>() {
            { "Ocp-Apim-Subscription-Key", _subscriptionKey },
            { "Content-Type", "application/octet-stream" }
        };

        WWW www = new WWW(_computerVisionEndpoint, image, headers);
        yield return www;

        List<string> tags = new List<string>();
        var jsonResults = www.text;
        var myObject = JsonUtility.FromJson<AnalysisResult>(jsonResults);
        foreach (var tag in myObject.tags)
        {
            tags.Add(tag.name);
        }
        AnalysisPanel.text = "ANALYSIS:\n***************\n\n" + string.Join("\n", tags.ToArray());

        List<string> faces = new List<string>();
        foreach (var face in myObject.faces)
        {
            faces.Add(string.Format("{0} scanned: age {1}.", face.gender, face.age));
        }
        if(faces.Count > 0)
        {
            InfoPanel.text = "MATCH";
        }else
        {
            InfoPanel.text = "ACTIVE SPATIAL MAPPING";
        }
        ThreatAssessmentPanel.text = "SCAN MODE 43984\nTHREAT ASSESSMENT\n\n" + string.Join("\n", faces.ToArray());
    }

    IEnumerator Read(byte[] image)
    {
        var headers = new Dictionary<string, string>() {
            { "Ocp-Apim-Subscription-Key", _subscriptionKey },
            { "Content-Type", "application/octet-stream" }
        };

        WWW www = new WWW(_ocrEndpoint, image, headers);
        yield return www;

        List<string> words = new List<string>();
        var jsonResults = www.text;
        var myObject = JsonUtility.FromJson<OcrResults>(jsonResults);
        foreach (var region in myObject.regions)
        foreach (var line in region.lines)
        foreach (var word in line.words)
        {
            words.Add(word.text);
        }

        string textToRead = string.Join(" ", words.ToArray());
        if (textToRead.Length > 0)
        {
            DiagnosticPanel.text = "(language=" + myObject.language + ")\n" + textToRead;
        }
       

    }
}
