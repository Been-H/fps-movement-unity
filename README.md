# fps-movement-unity

This is a first person player controller I made for Unity in C#. It uses rigidbody velocities for better collision detection and less clipping than transform.position. Feel free to use it if you'd like!

*Features*

- walking
- sprinting
- crouching (toggled)
- jumping
- different speeds for crouching, walking, sprinting etc.
- looking around and up and down with the mouse
- looking up and down is clamped
- no slipperly movement
- slower movement while jumping and extra gravity for more realistic jumping

*How to setup the character*

1. Make a capsule with a Rigidbody
2. create an empty game object and put it at eye level
3. parent the main camera to it and put it at 0 0 0
4. The parent object of the main camera is the "cameraHolder" in the script


