# InSituCinema
Immersive In Situ Cinema


This is a Unity Project using Bibcam by Keijiro Takahashi. The code was expanded.
The requirments are: UNity 2021 LTS and a LiDAR-enabled iOS device for recording.

While building, the scene have to be organised as BIBCIN 0, Encoder 1 and Decoder 2.

The first scene after opening is the BIBCIN.
There is asked what the user wants to do: What do you want to do?
The options are "Record" or "Play Video".

For the option "Record", on click it moves to the next scene Encoder.
Encoder allows on click of "Record" to film a video via Bibcam and saves into the camera roll.
"Stop" can be clicked to stop the recording. Or "Back to Menu" to return to the first scene.

For the option "Play Video", it opens the Decoder scene.
An AR camera shows the camera view. A white plane and a blue capsule should be visible.
A video from the camera roll can be selected with "Select Video". This video will be played on the plane. The capsule shows the way..
But careful, both the capsule and plane move according to the video metadata. The plane might have to be found again as it moves in space.
If a blue cube is visible, the plane is displayed on the other side. The user has to move.
The metadata is given with Bibcam, so it is not adjusted. The plane is often found on the ceiling.
The plane moves according to the metadata.
The video can be paused and then resumed. The "Stop" button brings the user back to the first scene.
At any point a new video can be selected which then is displayed.

