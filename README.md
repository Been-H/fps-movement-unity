# Fps-movement-unity
This is a first person player controller I made for Unity in C#. It uses rigidbody velocities for better collision detection and less clipping than transform.position. Feel free to use it if you'd like!

### Features
- Walking
- Sprinting
- Crouching (toggle or hold) (midair or on ground)
- Jumping
- Configurable speeds for crouching, walking, sprinting etc.
- Smoothing of the camera, controlled by the mouse
- Clamped look directions
- No slippery movement
- Slower movement while jumping and extra gravity
- Configurable keys for actions
- Fast setup in one button button push
  - Create and setup rigidbody
  - Create and setup camera 
  - Set gravity to -19


### Controls
This script is currently setup with the following controls.
These are all configurabe in the editor (don't require coding)

| Key(s) | Action |
| ------ | ------ |
| WSAD | Simple movement |
| Space | Jump |
| Z | Crouch |
| Q | Lock/Unlock Mouse |

### How to setup the character

Download and add the files to your assets

![alt text](https://raw.githubusercontent.com/B0N3head/fps-movement-unity/main/assets/1.PNG)

Add a capsule to your scene
![alt text](https://raw.githubusercontent.com/B0N3head/fps-movement-unity/main/assets/2.png)

Add the Player Movement script through Component -> Player Movement and Camera Controller
![alt text](https://raw.githubusercontent.com/B0N3head/fps-movement-unity/main/assets/3.PNG)

Click the "Setup Player" button
![alt text](https://raw.githubusercontent.com/B0N3head/fps-movement-unity/main/assets/4.PNG)

Done, this is what you should be left with
![alt text](https://raw.githubusercontent.com/B0N3head/fps-movement-unity/main/assets/5.PNG)

### Changes made in this fork

1. Fixed jump resting move speed when crouching
2. Added mouse smoothing for camera movement
3. Added crouching in midair
4. Added hold crouching (instead of toggle)
5. Changed default settings to be used with a gravity of -19
6. Added mouse lock toggle
7. Removed need to add camera to script (more noob friendly)
8. Removed need to create separate gameobject for camera (camera holder)
9. Added configurable keys for sprinting/crouching/jumping
10. Added headers to the inspector
11. Moved movement code to fixedUpdate
12. Renamed/moved/removed some functions
12. Renamed/moved/removed some variables
14. Combined crouch and uncrouch together into one function
15. Added pictures for setup
16. Added script to component menu
17. Created auto setup script