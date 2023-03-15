using Godot;
using System;

public partial class player : CharacterBody2D
{
    // Movement variables, can be set in the Inspector
    // Movement speed
    [Export]
    public int speed = 400;
    // Force of jump
    [Export]
    public int jumpForce = 1500;
    // "Gravity"
    [Export]
    public int gravity = 4000;

    // Motion for player, will later be changed into built-in Velocity
    private Vector2 motion;
    // Ceiling for motion.Y * JumpForce
    private int jumpCeil =  -1050;
    // When jumpSwitch == false, jump key does nothing (on until peak acceleration)
    private bool jumping;

    // When jump timer runs out
    public void OnJumpTimerTimeout()
    {
        // jumping is false
        jumping = false;
    }

    // Handling movement and physics
    public override void _PhysicsProcess(double delta)
    {
        // Setting ground motion to zero if not pressing button and grounded
        motion.X = 0;
       
        // Rotation is always 0 degrees
        Rotation = 0;

       
        // Taking input and changing ground motion accordingly
        if (Input.IsActionPressed("move_left"))
        {
            // X negative direction
            motion.X -= 1;
        }
        if (Input.IsActionPressed("move_right"))
        {
            // X positive direction
            motion.X += 1;
        }
        // Setting ground motion with X direction * Speed
        motion.X += motion.X * speed;
        

        // Reset Y velocity and jumping is false when grounded
        if (IsOnFloor())
        {
            motion.Y = 0;
            jumping = false;
        }
        // If not jumping currently
        if (!jumping)
        {
            // You can jump from the floor
            if (Input.IsActionPressed("jump") && IsOnFloor())
            {
                // Jumping
                jumping = true;
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
        if (jumping)
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
                jumping = false;
            }
            // Else jumping and below jumpCeil
            else
            {
                // Turn off jumping
                jumping = false;
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
