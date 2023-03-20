using Godot;
using System;
using System.IO.IsolatedStorage;

public partial class player : CharacterBody2D
{
    // Movement variables, can be set in the Inspector
    // Movement speed
    [Export]
    public int walkSpeed = 400;
    [Export]
    public int sprintSpeed = 800;
    // Force of jump
    [Export]
    public int jumpForce = 1500;
    // "Gravity"
    [Export]
    public int gravity = 4000;

    // Motion for player, will later be changed into built-in Velocity
    private Vector2 motion;

    // When isJumping == false, jump key does nothing (on until peak acceleration)
    private bool isJumping;

    // When jump timer runs out
    public void OnJumpTimerTimeout()
    {
        // jumping is false
        isJumping = false;
    }

    // Handling movement and physics
    public override void _PhysicsProcess(double delta)
    {
        // Ceiling for motion.Y * JumpForce
        int jumpCeil = -1050;

        // Init movement bools to false
        bool isWalking = false;
        bool isSprinting = false;

        // Rotation is always 0 degrees
        Rotation = 0;

        if (IsOnFloor())
        {
            // Init ground motion to zero
            motion.X = 0;

            // Reset Y velocity and jumping is false when grounded
            motion.Y = 0;
            isJumping = false;

            // Take input and change ground motion accordingly
            if (Input.IsActionPressed("walk_left") || Input.IsActionPressed("walk_right")) // If walking
            {
                isWalking = true;
            }
            if (Input.IsActionPressed("sprint")) // If sprinting
            {
                isSprinting = true;
                isWalking = false;
            }

            // If walking
            if (isWalking)
            {
                if (Input.IsActionPressed("walk_left"))
                {
                    // Go walkSpeed
                    motion.X -= walkSpeed;
                }
                else if (Input.IsActionPressed("walk_right"))
                {
                    motion.X += walkSpeed;
                }
            }
            // If sprinting
            if (isSprinting)
            {
                if (Input.IsActionPressed("walk_left"))
                {
                    // Go sprintSpeed
                    motion.X -= sprintSpeed;
                }
                else if (Input.IsActionPressed("walk_right"))
                {
                    motion.X += sprintSpeed;
                }
            }
        }

 
        // If not jumping currently
        if (!isJumping)
        {
            // You can jump from the floor
            if (Input.IsActionPressed("jump") && IsOnFloor())
            {
                // Jumping
                isJumping = true;
                // Start JumpTimer
                GetNode<Timer>("JumpTimer").Start();
                // Go up fast
                motion.Y = (jumpForce * (float)delta) * -23;
            }
            // Else you can't jump
            else
            {
                // Gravity is applied
                motion.Y += (gravity * (float)delta) * 2;
            }
        }
        // If jumping
        if (isJumping)
        {
            // If jump is pressed and motion.Y is above jumpCeil velocity
            if (Input.IsActionPressed("jump") && motion.Y > jumpCeil)
            {
                // Go up
                motion.Y -= jumpForce * (float)delta;
            }
            // Else if jump is released
            else if (Input.IsActionJustReleased("jump"))
            {
                // Turn off jumping
                isJumping = false;
            }
            // Else jumping and below jumpCeil
            else
            {
                // Turn off jumping
                isJumping = false;
                // Gravity is applied
                motion.Y += (gravity * (float)delta) * 2;
            }
        }

        // Setting built-in Velocity to motion
        Velocity = motion;
        // Moving the player (using built-in Velocity)
        MoveAndSlide();
    }

}
