# fps-movement-unity

This is a first person player controller I made for Unity in C#. It uses rigidbody velocities for better collision detection and less clipping than transform.position. Feel free to use it if you'd like!

*Features*

- Walking
- Sprinting
- Crouching (toggle or hold) (midair or on ground)
- Jumping
- Configurable speeds for crouching, walking, sprinting etc.
- Smoothing of the camera, controlled by the mouse
- Clamped look directions
- No slipperly movement
- Slower movement while jumping and extra gravity

*How to setup the character*

1. Make a capsule with a Rigidbody (set its weight to 10 *OPTIONAL*)
2. Drag this script onto the capsule
3. Create a camera (child of the capsule) and put it at eye level
4. Done


