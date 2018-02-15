# Terminator Vision

This is a hololens application that uses the [Microsoft Computer Vision API](https://www.microsoft.com/cognitive-services/en-us/computer-vision-api) and overlays the result in a style similar to the movie Terminator. It can detect objects, faces, and OCR text.  

See the Windows Blog [post](https://blogs.windows.com/buildingapps/2017/03/06/building-terminator-vision-hud-hololens/) for more information  

![Screenshot](screenshot.jpg)

*Some setup instructions*

(When you open the project in Unity the first time, you are probably going to see errors like HoloToolkit cannot be found or File type unknown. Here's how you're going to fix that.)

1. Make sure you cloned this repository with the --recursive flag. This will also get the appropriate version of the MRTK.

2. Open a command prompt, navigate to the project folder and execute this command:

   mklink /j .\Assets\HoloToolkit .\MixedRealityToolkit-Unity\Assets\HoloToolkit

   This is create a linked folder to the HoloToolkit in the Assets folder of the project.

3. Set the project settings using the Mixed Reality Toolkit menu [Configure - Apply Mixed Reality Project Settings]

4. Open the Build Settings dialog (File | Build Settings)

5. Click on Add Open Scenes to add the *main* scene to your build

6. Build the UWP app. Create a new folder called Builds and select it.

7. Errors should be gone now, but if they aren't just shut down and re-open your Unity project

8. [Sign up](https://www.microsoft.com/cognitive-services) for a  Cognitive Services account and generate a subscription key for the Computer Vision API

9. In the Hud game object you must set 2 settings:
	* Computer Vision key
	* Your Azure endpoint for Computer Vision

10. Rebuild and deploy the project to your Hololens  


	

