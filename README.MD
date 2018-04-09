# Terminator Vision

This is a hololens application that uses the [Microsoft Computer Vision API](https://www.microsoft.com/cognitive-services/en-us/computer-vision-api) and overlays the result in a style similar to the movie Terminator. It can detect objects, faces, and OCR text.  

See the Windows Blog [post](https://blogs.windows.com/buildingapps/2017/03/06/building-terminator-vision-hud-hololens/) for more information  

![Screenshot](screenshot.jpg)

*Some setup instructions*

1. [Sign up](https://www.microsoft.com/cognitive-services) for a  Cognitive Services account and generate a subscription key for the Computer Vision API.   
If you already have an Azure account you can skip that and setup a Computer Vision App directly in your Azure subscription. To do that go directly to the [Azure portal](https://portal.azure.com).

2. Clone this repository with the **--recursive** flag. This will also get the appropriate version of the MRTK in the root folder for this project.

3. Open a command prompt, navigate to the root folder for this project and execute this command:

   ```shell
   mklink /j .\Assets\HoloToolkit .\MixedRealityToolkit-Unity\Assets\HoloToolkit
   ```

   This will create a linked folder to the HoloToolkit in the Assets folder of the project. Now the MRTK is included in the project.

4. Open the root folder of the project in Unity

5. Open the **main** scene, which you kind find in the root of the Assets folder

6. Select the **HUD** game object in the hierarchy and enter these fields for the *Hud* script:
	* **Computer Vision key**  
      Get a key from the Keys page under Resource Management for your Computer Vision app in the Azure portal you created in step 1.
	* **Computer Vision Endpoint**  
     The endpoint for your Computer Vision app in the Azure portal created in step 1. This can be found on the Overview page.

7. Set the project settings using the Mixed Reality Toolkit menu *Configure - Apply Mixed Reality Project Settings*. Keep the defaults selected and click Apply.

8. Open the Build Settings dialog *File | Build Settings*. Click on Add Open Scenes to add the **main** scene to your build.

9. Build the UWP app from the Build Settings dialog. Create a new folder called Builds and select it.

10. Open the solution in the Builds folder with Visual Studio.

11. Set the configuration in the top bar to Debug and x86. Select Device if your Hololens is connected with a cable, Remote Machine if it's connected over WiFi or the Hololens Emulator.

12. Build the solution in Visual Studio and deploy to your Hololens.
