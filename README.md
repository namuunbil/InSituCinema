# InSituCinema

This prototype is based on Unity using Bibcam\footnote{https://github.com/keijiro/Bibcam, last used 29. Juli} by Keijiro Takahashi as foundation. The code of this project was expanded on in order to fit the requirements of this work, mainly the playback function. With regards to Bibcam the prototype used Unity 2021 LTS and a LiDAR-enabled iOS device for recording.


The first scene after opening the application is called the "BIBCIN". The opening scene asked what the user wants to do: "What do you want to do?" The options are "Record" or "Play Video". With a press on either button the user is directed to the corresponding scene. 

If the option "Record" is choosen the next scene is "Encoder". The Encoder films a video with metadata via the button "Record" and saves it into the camera roll. "Stop" appears when the camera is actively filming and can stop the recording. The "Back to Menu" button returns the user to the initial "BIBCIN" scene.


If the option "Pick Video" has been selected in the first scene it opens the "Decoder" scene. An AR camera shows the current camera view. A white cube is in the center with a blue capsule in front of it. "Select Video" is the first button and in accordacne to its name it allows the user to access their camera roll and select a video to be played back. This video will be displayed on the cube. The capsule and the video move together according to the metadata containing the 3D space data of the original, the camera that recorded the video initially, camera movement. As the cube moves in all directions the user can follow along. The "Stop" button stops the video and the movement entirely. "Pause" halts the video but can be continued with the button "Resume".  Regardless of the current video playing or not playing the "Pick Video" button can always be used.

The last button "Calibrate" allows the user to use the current frame of a selected video and match with the real background. The current frame is extracted and semi-transperent as it is overlayed on the camera view. The user can move the device till the background aligns with the frame and then go back to the "Decoder" scene with "Done". "Back to Menu" has the same function again and returns the user to the BIBCIN scene.

