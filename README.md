# Fps-movement-unity

This is a first person player controller I made for Unity in C#. It uses rigidbody velocities for better collision detection and less clipping than transform.position. Feel free to use it if you'd like!

*Features*

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

*How to setup the character*

1. Make a capsule with a Rigidbody (set its weight to 10 *OPTIONAL*)
2. Drag this script onto the capsule
3. Create a camera (child of the capsule) and put it at eye level
4. Done

*Changes in fork*

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
13. Added ComponentMenu header for inspector
14. Combined crouch and uncrouch together into one function
